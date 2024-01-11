package main

import (
	"context"
	"encoding/json"
	"log"
	"os"

	maelstrom "github.com/jepsen-io/maelstrom/demo/go"
)

type AddMessage struct {
	Type  string `json:"type"`
	Delta int    `json:"delta"`
}

func main() {
	// instantiating our node type and key-value store wrapper
	n := maelstrom.NewNode()
	kv := maelstrom.NewSeqKV(n)

	// threading and concurrency concerns
	ctx := context.Background()

	// register callback function
	n.Handle("add", func(msg maelstrom.Message) error {
		// unmarshal the message body to struct
		var body AddMessage
		if err := json.Unmarshal(msg.Body, &body); err != nil {
			return err
		}

		// read value from key-value store by Node ID, write with delta
		readValue, err := kv.ReadInt(ctx, n.ID())

		if err != nil {
			readValue = 0
		}

		kv.Write(ctx, n.ID(), readValue+body.Delta)

		// create the response message
		response := make(map[string]any)
		response["type"] = "add_ok"

		return n.Reply(msg, response)
	})

	// register callback function
	n.Handle("read", func(msg maelstrom.Message) error {
		// unmarshal the message body to map
		var body map[string]any
		if err := json.Unmarshal(msg.Body, &body); err != nil {
			return err
		}

		sum := 0

		for _, nodeId := range n.NodeIDs() {
			readValue, err := kv.ReadInt(ctx, nodeId)

			if err != nil {
				readValue = 0
			}

			sum += readValue
		}

		// update the message type to be replied
		body["type"] = "read_ok"
		body["value"] = sum

		return n.Reply(msg, body)
	})

	// execute, read from STDIN and handle
	if err := n.Run(); err != nil {
		log.Fatal(err)
		os.Exit(1)
	}

	ctx.Done()
}
