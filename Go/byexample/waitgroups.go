package main

import (
	"fmt"
	"sync"
	"time"
)

func worker(id int) {
	fmt.Printf("Worker %d starting...\n", id)

	time.Sleep(time.Second)

	fmt.Printf("Worker %d done!\n", id)
}

func main() {
	var wg sync.WaitGroup

	// launch several goroutines
	for i := 1; i <= 5; i++ {
		// increment WaitGroup counter for each
		wg.Add(1)

		// wrap worker call in closure
		go func() {
			defer wg.Done()
			worker(i)
		}()
	}

	// block unitl the counter goes back to zero
	wg.Wait()
}
