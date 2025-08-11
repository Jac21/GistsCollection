package main

import (
	"context"
	"encoding/json"
	"fmt"
	"log"
	"os"

	maelstrom "github.com/jepsen-io/maelstrom/demo/go"
)

type SendMessage struct {
	Type string  `json:"type"`
	Key  string  `json:"key"`
	Msg  float64 `json:"msg"`
}

type SendReplyMessage struct {
	Type   string `json:"type"`
	Offset int    `json:"offset"`
}

type OffsetsMessage struct {
	Type    string         `json:"type"`
	Offsets map[string]int `json:"offsets"`
}

type OffsetsReplyMessage struct {
	Type string              `json:"type"`
	Msgs map[string][][2]int `json:"msgs"`
}

type CommitOffsetsReplyMessage struct {
	Type string `json:"type"`
}

func main() {
	// instantiating our node type and key-value store wrapper
	n := maelstrom.NewNode()
	kv := maelstrom.NewSeqKV(n)

	logs := AppendOnlyLog{ctx: context.TODO(), kv: kv}

	// register callback function
	n.Handle("send", func(msg maelstrom.Message) error {
		var body SendMessage
		if err := json.Unmarshal(msg.Body, &body); err != nil {
			return err
		}

		tail, err := logs.Append(body.Key, body.Msg)

		if err != nil {
			return err
		}
		// update the message type to be replied
		return n.Reply(msg, SendReplyMessage{"send_ok", tail})
	})

	// register callback function
	n.Handle("poll", func(msg maelstrom.Message) error {
		var body OffsetsMessage
		if err := json.Unmarshal(msg.Body, &body); err != nil {
			return err
		}

		msgs := map[string][][2]int{}
		for key, startingOffset := range body.Offsets {
			for offset := startingOffset; offset < startingOffset+3; offset++ {
				message, exists, err := logs.Poll(key, offset)

				if err != nil {
					return err
				}

				if !exists {
					break
				}

				msgs[key] = append(msgs[key], [2]int{offset, message})
			}
		}

		// update the message type to be replied
		return n.Reply(msg, OffsetsReplyMessage{"poll_ok", msgs})
	})

	// register callback function
	n.Handle("commit_offsets", func(msg maelstrom.Message) error {
		var body OffsetsMessage
		if err := json.Unmarshal(msg.Body, &body); err != nil {
			return err
		}

		for key, offset := range body.Offsets {
			if err := logs.CommitOffset(key, offset); err != nil {
				return err
			}
		}

		// update the message type to be replied
		return n.Reply(msg, CommitOffsetsReplyMessage{"commit_offsets_ok"})
	})

	// register callback function
	n.Handle("list_committed_offsets", func(msg maelstrom.Message) error {
		// unmarshal the message body to map
		var body map[string]any
		if err := json.Unmarshal(msg.Body, &body); err != nil {
			return err
		}

		committed_offsets := map[string]int{}

		for _, key := range body["keys"].([]interface{}) {
			keyString := key.(string)
			committed_offsets[keyString] = logs.GetCommittedOffset(keyString)
		}

		// update the message type to be replied
		return n.Reply(msg, OffsetsMessage{"list_committed_offsets_ok", committed_offsets})
	})

	// execute, read from STDIN and handle
	if err := n.Run(); err != nil {
		log.Fatal(err)
		os.Exit(1)
	}
}

type AppendOnlyLog struct {
	ctx context.Context
	kv  *maelstrom.KV
}

func (appendOnlyLog *AppendOnlyLog) Append(key string, val float64) (int, error) {
	keyTail := fmt.Sprintf("tail_%s", key)

	offset, err := appendOnlyLog.kv.ReadInt(appendOnlyLog.ctx, keyTail)

	if err != nil {
		rpcErr := err.(*maelstrom.RPCError)

		if rpcErr.Code == maelstrom.KeyDoesNotExist {
			offset = 0
		} else {
			return -1, err
		}
	} else {
		offset++
	}

	for ; ; offset++ {
		if err := appendOnlyLog.kv.CompareAndSwap(appendOnlyLog.ctx, keyTail, offset-1, offset, true); err != nil {
			fmt.Fprintf(os.Stderr, "cas retry: %v\n", err)

			continue
		}

		break
	}

	return offset, appendOnlyLog.kv.Write(appendOnlyLog.ctx, fmt.Sprintf("entry_%s_%d", key, offset), val)
}

func (appendOnlyLog *AppendOnlyLog) Poll(key string, offset int) (int, bool, error) {
	val, err := appendOnlyLog.kv.ReadInt(appendOnlyLog.ctx, fmt.Sprintf("entry_%s_%d", key, offset))

	if err != nil {
		rpcErr := err.(*maelstrom.RPCError)

		if rpcErr.Code == maelstrom.KeyDoesNotExist {
			return 0, false, nil
		}

		return 0, false, err
	}

	return val, true, nil
}

func (appendOnlyLog *AppendOnlyLog) CommitOffset(key string, offset int) error {
	return appendOnlyLog.kv.Write(appendOnlyLog.ctx, fmt.Sprintf("commit_%s", key), offset)
}

func (appendOnlyLog *AppendOnlyLog) GetCommittedOffset(key string) int {
	val, err := appendOnlyLog.kv.ReadInt(appendOnlyLog.ctx, fmt.Sprintf("commit_%s", key))

	if err != nil {
		return 0
	}

	return val
}
