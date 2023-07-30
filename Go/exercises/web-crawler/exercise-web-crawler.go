package main

import (
	"fmt"
	"sync"
	"time"
)

type SafeMap struct {
	v   map[string]bool
	mux sync.Mutex
}

func (m *SafeMap) SetVal(key string, val bool) {
	m.mux.Lock()
	m.v[key] = val
	m.mux.Unlock()
}

func (m *SafeMap) GetVal(key string) bool {
	m.mux.Lock()
	defer m.mux.Unlock()
	return m.v[key]
}

type Fetcher interface {
	// Fetch returns the body of URL and
	// a slice of URLs found on that page.
	Fetch(url string) (body string, urls []string, err error)
}

// Crawl uses fetcher to recursively crawl
// pages starting with url, to a maximum of depth.
func Crawl(url string, depth int, fetcher Fetcher, statusChannel chan bool, urlMap SafeMap) {
	// guard clause for potentially previously fetched URLs
	if fetchedUrl := urlMap.GetVal(url); fetchedUrl {
		fmt.Println("Already fetched URL:", url)

		statusChannel <- true

		return
	}

	// add URL to map
	urlMap.SetVal(url, true)

	if depth <= 0 {
		statusChannel <- false

		return
	}

	body, urls, err := fetcher.Fetch(url)

	if err != nil {
		fmt.Println(err)

		statusChannel <- false

		return
	}

	fmt.Printf("found: %s %q\n", url, body)

	statuses := make([]chan bool, len(urls))

	for i, u := range urls {
		statuses[i] = make(chan bool)

		go Crawl(u, depth-1, fetcher, statuses[i], urlMap)
	}

	// wait for child routines
	for _, childStatus := range statuses {
		<-childStatus
	}

	// finish routine
	statusChannel <- true
}

func main() {
	fmt.Println("Starting at:", time.Now())

	urlMap := SafeMap{v: make(map[string]bool)}

	statusChannel := make(chan bool)

	go Crawl("https://golang.org/", 4, fetcher, statusChannel, urlMap)

	<-statusChannel

	fmt.Println("Finishing at:", time.Now())
}

// fakeFetcher is Fetcher that returns canned results.
type fakeFetcher map[string]*fakeResult

type fakeResult struct {
	body string
	urls []string
}

func (f fakeFetcher) Fetch(url string) (string, []string, error) {
	if res, ok := f[url]; ok {
		return res.body, res.urls, nil
	}
	return "", nil, fmt.Errorf("not found: %s", url)
}

// fetcher is a populated fakeFetcher.
var fetcher = fakeFetcher{
	"https://golang.org/": &fakeResult{
		"The Go Programming Language",
		[]string{
			"https://golang.org/pkg/",
			"https://golang.org/cmd/",
		},
	},
	"https://golang.org/pkg/": &fakeResult{
		"Packages",
		[]string{
			"https://golang.org/",
			"https://golang.org/cmd/",
			"https://golang.org/pkg/fmt/",
			"https://golang.org/pkg/os/",
		},
	},
	"https://golang.org/pkg/fmt/": &fakeResult{
		"Package fmt",
		[]string{
			"https://golang.org/",
			"https://golang.org/pkg/",
		},
	},
	"https://golang.org/pkg/os/": &fakeResult{
		"Package os",
		[]string{
			"https://golang.org/",
			"https://golang.org/pkg/",
		},
	},
}
