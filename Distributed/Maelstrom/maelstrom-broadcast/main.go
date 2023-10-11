package main

import (
	"encoding/json"
	"log"
	"os"

	maelstrom "github.com/jepsen-io/maelstrom/demo/go"
)

func main() {
	// message container
	neighbors := make(map[float64]bool)

	// instantiating our node type
	n := maelstrom.NewNode()

	// register callback function
	n.Handle("broadcast", func(msg maelstrom.Message) error {
		// unmarshal the message body to map
		var body map[string]any
		if err := json.Unmarshal(msg.Body, &body); err != nil {
			return err
		}

		value := body["message"]
		neighbors[value.(float64)] = true

		// create the response message
		response := make(map[string]any)
		response["type"] = "broadcast_ok"

		return n.Reply(msg, response)
	})

	// register callback function
	n.Handle("read", func(msg maelstrom.Message) error {
		// unmarshal the message body to map
		var body map[string]any
		if err := json.Unmarshal(msg.Body, &body); err != nil {
			return err
		}

		// update the message type to be replied
		body["type"] = "read_ok"

		// create list of keys from neighbors map
		keys := make([]float64, 0, len(neighbors))
		for k := range neighbors {
			keys = append(keys, k)
		}

		body["messages"] = keys

		return n.Reply(msg, body)
	})

	// register callback function
	n.Handle("topology", func(msg maelstrom.Message) error {
		// unmarshal the message body to map
		var body map[string]any
		if err := json.Unmarshal(msg.Body, &body); err != nil {
			return err
		}

		// create the response message
		response := make(map[string]any)
		response["type"] = "topology_ok"

		return n.Reply(msg, response)
	})

	// execute, read from STDIN and handle
	if err := n.Run(); err != nil {
		log.Fatal(err)
		os.Exit(1)
	}
}
