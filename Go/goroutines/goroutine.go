package main

import (
	"fmt"
	"time"
)

func say(s string) {
	for i := 0; i < 5; i++ {
		time.Sleep(100 * time.Millisecond)
		fmt.Println(s)
	}
}

// channels
func sum(s []int, c chan int) {
	sum := 0

	for _, v := range s {
		sum += v
	}

	c <- sum // send sum to c with channel operator '<-'
}

func main() {
	// A goroutine is a lightweight thread managed by the Go runtime.
	// e.g., go f(x, y, z)
	go say("world")

	say("hello")

	s := []int{7, 2, 8, -9, 4, 0}

	// explicitly create a channel
	c := make(chan int)

	// sums the number in a slice between two goroutines
	go sum(s[:len(s)/2], c)
	go sum(s[len(s)/2:], c)

	// blocks until completion without explicit synchronization
	x, y := <-c, <-c // receive from c

	fmt.Println(x, y, x+y)

	// buffered channels
	ch := make(chan int, 2)
	ch <- 1
	ch <- 2
	fmt.Println(<-ch)
	fmt.Println(<-ch)
}
