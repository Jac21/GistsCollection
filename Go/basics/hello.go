// go mod init main
// go run hello.go
package main

import (
	"fmt"
	"math"
	"math/cmplx"
	"time"
)

// basic function with consecutive types
func add(x, y int) int {
	return x + y
}

// multiple returned results
func swap(x, y string) (string, string) {
	return y, x
}

// named return values
func split(sum int) (x, y int) {
	x = sum * 4 / 9
	y = sum - x
	return
}

// basic types
var (
	ToBe   bool       = false
	MaxInt uint64     = 1<<64 - 1
	z      complex128 = cmplx.Sqrt(-5 + 12i)
)

// variables
var c, python, java bool

// constants
const Pi = 3.14

func main() {
	fmt.Println("Hello, Go!")

	fmt.Println("The time is", time.Now())

	fmt.Println("Adding test results in", add(9, 21))

	a, b := swap("Hello", "Go!")
	fmt.Println(a, b)

	var i int = 21
	j := 9
	fmt.Println(i, j, c, python, java)

	fmt.Printf("Type: %T Value: %v\n", ToBe, ToBe)
	fmt.Printf("Type: %T Value: %v\n", MaxInt, MaxInt)
	fmt.Printf("Type: %T Value: %v\n", z, z)

	// type conversions
	var x, y int = 3, 4
	var f float64 = math.Sqrt(float64(x*x + y*y))
	var z uint = uint(f)
	fmt.Println(x, y, z)
	fmt.Println(Pi)
}
