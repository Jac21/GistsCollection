package main

import (
	"fmt"
	"time"
)

func main() {
	c1 := make(chan string)
	c2 := make(chan string)

	// simulate blocking operations on two channels
	// in concurrent goroutines
	go func() {
		time.Sleep(1 * time.Second)
		c1 <- "one"
	}()

	go func() {
		time.Sleep(2 * time.Second)
		c2 <- "two"
	}()

	// await on both of these values simultaneously
	for i := 0; i < 2; i++ {
		select {
		case msg1 := <-c1:
			fmt.Println("recieved", msg1)
		case msg2 := <-c2:
			fmt.Println("recieved", msg2)
		}
	}
}
