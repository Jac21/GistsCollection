In [concurrent programming](https://en.wikipedia.org/wiki/Concurrent_programming "Concurrent programming"), an operation (or set of operations) is **linearizable** if it consists of an ordered list of [invocation](https://en.wikipedia.org/wiki/Execution_(computing) "Execution (computing)") and response [events](https://en.wikipedia.org/wiki/Event_(computing) "Event (computing)"), that may be extended by adding response events such that:

1. The extended list can be re-expressed as a sequential history (is [serializable](https://en.wikipedia.org/wiki/Serializability "Serializability")).
2. That sequential history is a subset of the original unextended list.

Informally, this means that the unmodified list of events is linearizable [if and only if](https://en.wikipedia.org/wiki/If_and_only_if "If and only if") its invocations were serializable, but some of the responses of the serial schedule have yet to return.

In a concurrent system, processes can access a shared [object](https://en.wikipedia.org/wiki/Object_(computer_science) "Object (computer science)") at the same time. Because multiple processes are accessing a single object, a situation may arise in which while one process is accessing the object, another process changes its contents. Making a system linearizable is one solution to this problem. In a linearizable system, although operations overlap on a shared object, each operation appears to take place instantaneously. Linearizability is a strong correctness condition, which constrains what outputs are possible when an object is accessed by multiple processes concurrently. It is a safety property which ensures that operations do not complete unexpectedly or unpredictably. If a system is linearizable it allows a programmer to reason about the system