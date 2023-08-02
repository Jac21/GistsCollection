// https://go.dev/talks/2012/concurrency.slide#1

package main

import (
	"fmt"
	"math/rand"
	"time"
)

// returns recieve-only channel of strings
func boring(msg string) <-chan string {
	c := make(chan string)

	// launch a goroutine from inside the function
	go func() {
		for i := 0; ; i++ {
			c <- fmt.Sprintf("%s %d", msg, i)
			time.Sleep(time.Duration(rand.Intn(1e3)) * time.Millisecond)
		}
	}()

	// return the channel to the caller
	return c
}

func fanIn(inputOne, inpuTwo <-chan string) <-chan string {
	c := make(chan string)

	timeout := time.After(80 * time.Millisecond)

	go func() {
		for {
			select {
			case s := <-inputOne:
				c <- s
			case s := <-inpuTwo:
				c <- s
			case <-timeout: // timeout using select
				fmt.Println("Too slow!")
				close(c)
				return
			}
		}
	}()

	return c
}

func main() {
	// function returning a channel
	c := boring("boring!")

	for i := 0; i < 5; i++ {
		fmt.Printf("You say: %q\n", <-c)
	}

	fmt.Println("You're boring, I'm leaving")

	// channels as a handle on a service
	alice := boring("Alice")
	bob := boring("Bob")

	for i := 0; i < 5; i++ {
		fmt.Println(<-alice)
		fmt.Println(<-bob)
	}

	fmt.Println("You're both boring, I'm leaving")

	// multiplexing
	m := fanIn(boring("Mallory"), boring("Ted"))

	for i := 0; i < 10; i++ {
		fmt.Println(<-m)
	}

	fmt.Println("You're all boring, I'm leaving")

}
