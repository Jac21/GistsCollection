package main

import (
	"crypto/rand"
	"fmt"
)

/*
Create a new Ring Buffer with capacity m, using k hash functions
*/
func NewRingBuffer(size int) *RingBuffer {
	return &RingBuffer{
		read:  1,
		write: 1,
		array: make([]any, size),
	}
}

type RingBuffer struct {
	read  uint32
	write uint32

	array []any
}

func (rb *RingBuffer) Mask(val uint32) uint32 {
	return val & (uint32(len(rb.array)) - 1)
}

func (rb *RingBuffer) Push(val uint32) {
	rb.write++
	rb.array[rb.Mask(rb.write)] = val
}

func (rb *RingBuffer) Shift() any {
	rb.read++
	return rb.array[rb.Mask(rb.read)]
}

func (rb *RingBuffer) Empty() bool {
	return rb.read == rb.write
}

func (rb *RingBuffer) Full() bool {
	return rb.Size() == uint32(len(rb.array))
}

func (rb *RingBuffer) Size() uint32 {
	return rb.write - rb.read
}

func main() {
	fmt.Println("Creating Ring Buffer")

	size := 128
	fmt.Printf("Asserting size '%d' is a power of two...", size)
	fmt.Println()

	if size&(size-1) != 0 {
		panic("Size must be a power of two")
	}

	rb := NewRingBuffer(size)

	fmt.Println("Filling Ring Buffer...")
	for i := 0; i < size; i++ {
		var b [4]byte
		rand.Read(b[:])
		val := uint32(b[0])<<24 | uint32(b[1])<<16 | uint32(b[2])<<8 | uint32(b[3])
		fmt.Println(val)
		rb.Push(val)
	}

	fmt.Printf("Is the ring buffer empty? %v\n", rb.Empty())
	fmt.Printf("Is the ring buffer full? %v\n", rb.Full())
	fmt.Printf("Size of the ring buffer: %d\n", rb.Size())

	fmt.Println("Emptying Ring Buffer")
	for !rb.Empty() {
		val := rb.Shift()
		fmt.Println(val)
		_ = val
	}

	fmt.Printf("Is the ring buffer empty? %v\n", rb.Empty())
	fmt.Printf("Is the ring buffer full? %v\n", rb.Full())
	fmt.Printf("Size of the ring buffer: %d\n", rb.Size())
}
