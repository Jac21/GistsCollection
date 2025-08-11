package main

import (
	"crypto/rand"
	"fmt"
	"hash/maphash"
	"math"
)

/*
Create a new Bloom Filter with capacity m, using k hash functions
*/
func New(m uint64, k uint64) *BloomFilter {
	return &BloomFilter{
		m:      m,
		k:      k,
		bitset: newBitset(m),
		seed1:  maphash.MakeSeed(),
		seed2:  maphash.MakeSeed(),
	}
}

type BloomFilter struct {
	m      uint64
	k      uint64
	bitset []uint64

	// seeds for double hashing scheme
	seed1, seed2 maphash.Seed
}

// Insertion method
func (bf *BloomFilter) Insert(data []byte) {
	h1 := maphash.Bytes(bf.seed1, data)
	h2 := maphash.Bytes(bf.seed2, data)

	for i := range bf.k {
		loc := (h1 + i*h2) % bf.m
		bitsetSet(bf.bitset, loc)
	}
}

/*
Testing function -
If the return value is false, the data is assuredly not in the filter.
If true, there's a probability of a false positive.
*/
func (bf *BloomFilter) Test(data []byte) bool {
	h1 := maphash.Bytes(bf.seed1, data)
	h2 := maphash.Bytes(bf.seed2, data)

	for i := range bf.k {
		loc := (h1 + i*h2) % bf.m
		if !bitsetTest(bf.bitset, loc) {
			return false
		}
	}

	return true
}

func newBitset(m uint64) []uint64 {
	blen := (m + 63) / 64
	return make([]uint64, blen)
}

func bitsetSet(bitset []uint64, i uint64) {
	word, offset := i/64, i%64
	bitset[word] |= (1 << offset)
}

func bitsetTest(bitset []uint64, i uint64) bool {
	word, offset := i/64, i%64
	return (bitset[word] & (1 << offset)) != 0
}

// CalculateParams calculates optimal parameters for a Bloom filter that's
// intended to contain n elements with error (false positive) rate eps.
func CalculateParams(n uint64, eps float64) (m uint64, k uint64) {
	// The formulae we derived are:
	// (m/n) = -ln(eps)/(ln(2)*ln(2))
	// k = (m/n)ln(2)

	ln2 := math.Log(2)
	mdivn := -math.Log(eps) / (ln2 * ln2)
	m = uint64(math.Ceil(float64(n) * mdivn))
	k = uint64(math.Ceil(mdivn * ln2))
	return
}

func main() {
	fmt.Println("initiating test...")

	// Test the operation of the Bloom filter. We set parameters to get
	// an extremely low error rate, and check that we don't get any false
	// answers.
	m := uint64(2000)
	n, k := CalculateParams(m, 1e-15)

	fmt.Println("Creating Bloom Filter")
	bf := New(n, k)

	// Insert m random items, also holding them in fullSet.
	// We assume that the chance of generating the same 256-byte random
	// value is negligible.
	fmt.Println("Inserting into Bloom Filter")
	buf := make([]byte, 256)
	fullSet := make(map[string]bool)
	for range m {
		rand.Read(buf)
		bf.Insert(buf)
		fullSet[string(buf)] = true
	}

	// Check that true is returned for all inserted items (this can never be
	// false due to the guarantees of the Bloom filter)
	fmt.Println("Testing inserted items...")
	for k := range fullSet {
		if !bf.Test([]byte(k)) {
			fmt.Printf("Test(%v)=false", k)
		} else {
			fmt.Printf("Test(%v)=true", k)
		}
	}

	// Now generate another 2000 random items; the chance of false positives is
	// so low that we expect Test to return true for all of these.
	fmt.Println("Testing inserted items, redux...")
	for range m {
		rand.Read(buf)
		if bf.Test(buf) {
			fmt.Printf("Test(%v)=true", buf)
		} else {
			fmt.Printf("Test(%v)=false", buf)
		}
	}
}
