package main

import (
	"fmt"

	"golang.org/x/tour/tree"
)

// Walk walks the tree t sending all values
// from the tree to the channel ch.
func Walk(t *tree.Tree, ch chan int) {
	if t == nil {
		return
	}

	Walk(t.Left, ch)
	ch <- t.Value
	Walk(t.Right, ch)
}

// Same determines whether the trees
// t1 and t2 contain the same values.
func Same(t1, t2 *tree.Tree) bool {
	ch1 := make(chan int)
	ch2 := make(chan int)

	go func() {
		defer close(ch1)
		Walk(t1, ch1)
	}()

	go func() {
		defer close(ch2)
		Walk(t2, ch2)
	}()

	// receive values in range loop until channel closes
	for i := range ch1 {
		select {
		case j := <-ch2:
			if i != j {
				return false
			}
		default:
			break
		}
	}

	return true
}

func main() {
	fmt.Println(Same(tree.New(1), tree.New(1))) // true
	fmt.Println(Same(tree.New(1), tree.New(2))) // false
}
