package main

import (
	"encoding/json"
	"log"
	"os"

	maelstrom "github.com/jepsen-io/maelstrom/demo/go"
)

func main() {
	// instantiating our node type
	n := maelstrom.NewNode()

	// register callback function
	n.Handle("echo", func(msg maelstrom.Message) error {
		// unmarshal the message body to map
		var body map[string]any
		if err := json.Unmarshal(msg.Body, &body); err != nil {
			return err
		}

		// update the message type to be replied
		body["type"] = "echo_ok"
		return n.Reply(msg, body)
	})

	// execute, read from STDIN and handle
	if err := n.Run(); err != nil {
		log.Fatal(err)
		os.Exit(1)
	}
}
