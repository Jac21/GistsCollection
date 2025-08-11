package main

import (
	"fmt"
	"time"
)

// run this in a goroutine
func worker(done chan bool) {
	fmt.Println("working...")

	time.Sleep(time.Second)

	fmt.Println("fin")

	// notify goroutine that this work is done
	done <- true
}

func main() {
	// create notification channel, start worker
	done := make(chan bool, 1)
	go worker(done)

	// block until we recieve a notification
	<-done
}
