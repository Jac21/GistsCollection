package main

import (
	"fmt"
	"sync"
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

// close the channel at the end of the function call
func fibonacci(n int, c chan int) {
	x, y := 0, 1
	for i := 0; i < n; i++ {
		c <- x
		x, y = y, x+y
	}

	close(c)
}

// select lets a goroutine wait on multiple communication ops
func fibonacci_select(c, quit chan int) {
	x, y := 0, 1
	for {
		select {
		case c <- x:
			x, y = y, x+y
		case <-quit:
			fmt.Println("quit")
			return
		default: // ran if no other case is ready
			fmt.Println("    .")
			time.Sleep(50 * time.Millisecond)
		}
	}
}

// safe to use concurrently due to mutex usage
type SafeCounter struct {
	mu sync.Mutex
	v  map[string]int
}

func (c *SafeCounter) Inc(key string) {
	c.mu.Lock()

	// locked, so only one goroutine at a time can
	// access c.v
	c.v[key]++
	c.mu.Unlock()
}

func (c *SafeCounter) Value(key string) int {
	c.mu.Lock()

	// ensure mutex will be unlocked via defer call
	defer c.mu.Unlock()
	return c.v[key]
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

	// receive values in range loop until channel closes
	cha := make(chan int, 10)
	go fibonacci(cap(cha), cha)
	for i := range cha {
		fmt.Println(i)
	}

	// select blocks until one of its cases are run
	select_ch := make(chan int)
	quit := make(chan int)
	go func() {
		for i := 0; i < 10; i++ {
			fmt.Println(<-select_ch)
		}
		quit <- 0
	}()
	fibonacci_select(select_ch, quit)

	// mutex work
	mc := SafeCounter{v: make(map[string]int)}
	for i := 0; i < 1001; i++ {
		go mc.Inc("somekey")
	}

	time.Sleep(time.Second)
	fmt.Println(mc.Value("somekey"))
}
