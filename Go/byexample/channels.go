package main

import "fmt"

func main() {
	// create a new, string-based channel
	messages := make(chan string)

	go func() {
		// send a value into the channel
		messages <- "ping"
	}()

	// recieve channel value
	msg := <-messages
	fmt.Println(msg)
}
