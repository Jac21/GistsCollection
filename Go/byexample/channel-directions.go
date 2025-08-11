package main

import (
	"fmt"
)

// only accept a channel for sending values
func ping(pings chan<- string, msg string) {
	pings <- msg
}

// accepts one channel for recieving,
// another channel for sending
func pong(pings <-chan string, pongs chan<- string) {
	msg := <-pings
	pongs <- msg
}

func main() {
	pings := make(chan string, 1)
	pongs := make(chan string, 1)

	ping(pings, "passed message")
	pong(pings, pongs)

	fmt.Println(<-pongs)
}
