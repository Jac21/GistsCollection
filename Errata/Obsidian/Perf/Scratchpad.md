# Producer Consumer Queues

![[Pasted image 20250809110811.png]]
## Shortcomings with C# BlockingCollection Structure

- Excessive contention (in our use-case, on the read-end)
- Backpressure blocking writing thread(s)
- Signaling overhead (need to dig into this further)
- Style of process might not be conducive to usage (CPU vs. I/O)
- Allocations (contributing to overall memory pressure on applications & garbage collections)

# Circular/Ring Buffers

- Constant, fixed-sized buffer
- Pointer that point to next empty position, incremented with new entries
- Utilized in real-time systems
- Implementations may involve overwriting data when buffer is full (a non-starter for our use-case), difficult to utilize in a parallel environment (also a non-starter), and not optimal for when a buffer is mostly empty (not an issue)

![[Pasted image 20250809110716.png]]

## LMAX Disruptor

- Zero allocations
- Potentially non-blocking

![[Pasted image 20250809111558.png]]

# Confluence Ideation

## Potential Ideas to Integrate in BlockingCollection

- Pre-allocated memory for "events"
	- Extensions on top of BlockingCollection to utilize an ArrayPool?

Pre-allocation is done in Java implementation by allocating a backing array - 

![[Pasted image 20250809123256.png]]

then immediately filling them prior to any event publishing - 

![[Pasted image 20250809123401.png]]

## References
https://learn.microsoft.com/en-us/dotnet/standard/collections/thread-safe/blockingcollection-overview

https://www.baeldung.com/cs/circular-buffer

https://lmax-exchange.github.io/disruptor/disruptor.html