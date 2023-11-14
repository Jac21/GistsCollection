package main

import (
	"context"
	"encoding/json"
	"log"
	"os"
	"strings"
	"sync"

	maelstrom "github.com/jepsen-io/maelstrom/demo/go"
)

type BroadcastMessage struct {
	Src     string  `json:"src,omitempty"`
	Type    string  `json:"type"`
	Message float64 `json:"message"`
}

type SrcMessage struct {
	Src     string
	Message BroadcastMessage
}

func main() {
	// instantiating our node type
	n := maelstrom.NewNode()
	topology := n.NodeIDs()

	// message container
	neighbors := make(map[float64]bool)

	// threading and concurrency concerns
	messageQueue := make(chan SrcMessage, 3)
	ctx := context.Background()
	var mu sync.RWMutex

	for i := 0; i < 3; i++ {
		go func() {
			for {
				select {
				case <-ctx.Done():
					return
				case newMsg := <-messageQueue:
					// Propagate received broadcast message to neighbouring nodes
					for _, dest := range topology {
						if dest == newMsg.Src {
							continue
						}
						n.Send(dest, newMsg.Message)
					}
				}
			}
		}()
	}

	// register callback function
	n.Handle("broadcast", func(msg maelstrom.Message) error {
		// unmarshal the message body to struct
		var body BroadcastMessage
		if err := json.Unmarshal(msg.Body, &body); err != nil {
			return err
		}

		// extract message body, check if it's already been seen
		mu.RLock()
		if ok := neighbors[body.Message]; ok {
			mu.RUnlock()
			return nil
		}
		mu.RUnlock()

		mu.Lock()
		neighbors[body.Message] = true
		mu.Unlock()

		// gossip
		messageQueue <- SrcMessage{msg.Src, body}

		if strings.HasPrefix(msg.Src, "n") {
			return nil
		}

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
		mu.RLock()
		keys := make([]float64, 0, len(neighbors))
		for k := range neighbors {
			keys = append(keys, k)
		}
		mu.RUnlock()

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

		topology = []string{}
		for _, neighbour := range body["topology"].(map[string]interface{})[n.ID()].([]interface{}) {
			topology = append(topology, neighbour.(string))
		}

		return n.Reply(msg, response)
	})

	// execute, read from STDIN and handle
	if err := n.Run(); err != nil {
		log.Fatal(err)
		os.Exit(1)
	}

	ctx.Done()
}
