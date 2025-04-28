package main

import (
	"fmt"
	"time"
)

func f(from string) {
	for i := 0; i < 3; i++ {
		fmt.Println(from, ":", i)
	}
}

func main() {
	// synchronous call
	f("direct")

	// execute in a goroutine (lightweight thread of execution)
	go f("goroutine")

	// anonymous goroutine call
	go func(msg string) {
		fmt.Println(msg)
	}("anon")

	time.Sleep(time.Second)
	fmt.Println("fin")
}
