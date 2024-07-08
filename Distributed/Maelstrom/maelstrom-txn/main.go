package main

import (
	"context"
	"encoding/json"
	"fmt"
	"log"
	"os"
	"sync"
	"sync/atomic"

	maelstrom "github.com/jepsen-io/maelstrom/demo/go"
)

type TransactionMessage struct {
	Type         string           `json:"type"`
	MessageId    float64          `json:"msg_id"`
	Transactions [][3]interface{} `json:"txn"`
}

type TransactionOkMessage struct {
	Type         string           `json:"type"`
	MessageId    float64          `json:"msg_id"`
	InReplyTo    float64          `json:"in_reply_to"`
	Transactions [][3]interface{} `json:"txn"`
}

func main() {
	// instantiating our node type
	n := maelstrom.NewNode()

	kv := map[string]interface{}{}
	mu := sync.Mutex{}

	var c count32

	// register callback function
	n.Handle("txn", func(msg maelstrom.Message) error {
		var body TransactionMessage
		if err := json.Unmarshal(msg.Body, &body); err != nil {
			return err
		}

		txns := [][3]interface{}{}
		mu.Lock()
		for _, txn := range body.Transactions {
			txnType := txn[0].(string)

			targetKey := txn[1].(float64)
			crr := [3]interface{}{txnType, targetKey, nil}
			var v interface{}

			switch txnType {
			case "r":
				v = kv[string(rune(targetKey))]

			case "w":
				v = txn[2]
				kv[string(rune(targetKey))] = v.(float64)
			}

			crr[2] = v
			txns = append(txns, crr)
		}

		mu.Unlock()

		go Replicate(txns, n)

		// update the message type to be replied
		c.inc()
		return n.Reply(msg, TransactionOkMessage{"txn_ok", float64(c.get()), body.MessageId, txns})
	})

	n.Handle("sync", func(msg maelstrom.Message) error {
		var body TransactionMessage
		if err := json.Unmarshal(msg.Body, &body); err != nil {
			return err
		}

		mu.Lock()
		for _, txn := range body.Transactions {
			txnType := txn[0].(string)

			targetKey := txn[1].(float64)

			if txnType == "w" {
				kv[string(rune(targetKey))] = txn[2].(float64)
			}

		}
		mu.Unlock()

		return n.Reply(msg, map[string]any{"type": "sync_ok"})
	})

	if err := n.Run(); err != nil {
		log.Fatal(err)
	}
}

func Replicate(txns [][3]interface{}, n *maelstrom.Node) {
	body := map[string]any{
		"type": "sync",
		"txn":  txns,
	}

	for _, dest := range n.NodeIDs() {
		_, err := n.SyncRPC(context.TODO(), dest, body)

		if err != nil {
			fmt.Fprint(os.Stderr, "Error message: ", err, "\n")
		}
	}
}

type count32 int32

func (c *count32) inc() int32 {
	return atomic.AddInt32((*int32)(c), 1)
}

func (c *count32) get() int32 {
	return atomic.LoadInt32((*int32)(c))
}
