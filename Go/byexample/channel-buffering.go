package main

import "fmt"

func main() {
	// create a new, string-based, buffered channel
	messages := make(chan string, 2)

	// because this channel is buffered, we
	// can send values without concurrent recieve
	messages <- "buffered"
	messages <- "channel"

	// recieve channel values
	fmt.Println(<-messages)
	fmt.Println(<-messages)
}
