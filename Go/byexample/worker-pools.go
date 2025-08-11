package main

import (
	"fmt"
	"time"
)

// run several concurrent instances of a job by receiving
// work on the jobs channel and send output to results
func worker(id int, jobs <-chan int, results chan<- int) {
	for j := range jobs {
		fmt.Println("Worker", id, "Started job...", j)

		time.Sleep(time.Second)

		fmt.Println("Worker", id, "Finished job!", j)

		results <- j * 2
	}
}

func main() {
	const numJobs = 5

	// create two channels
	jobs := make(chan int, numJobs)
	results := make(chan int, numJobs)

	// start up three blocked workers
	for w := 1; w <= 3; w++ {
		go worker(w, jobs, results)
	}

	// send five jobs and close channel
	for j := 1; j <= numJobs; j++ {
		jobs <- j
	}

	close(jobs)

	// poor man's WaitGroup
	for a := 1; a <= numJobs; a++ {
		<-results
	}
}
