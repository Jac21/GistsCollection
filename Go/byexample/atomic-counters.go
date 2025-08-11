package main

import (
	"fmt"
	"sync"
	"sync/atomic"
)

func main() {
	// atomic integer type to represent counter
	var ops atomic.Uint64

	// used to wait for all goroutines to finish work
	var wg sync.WaitGroup

	// start fifty goroutines that each increment counter
	// one thousand times
	for i := 0; i < 50; i++ {
		wg.Add(1)

		go func() {
			for c := 0; c < 1000; c++ {
				// atomically increment counter
				ops.Add(1)
			}

			wg.Done()
		}()
	}

	// wait until all goroutines are done
	wg.Wait()

	// should ouput 50000
	fmt.Println("ops:", ops.Load())
}
