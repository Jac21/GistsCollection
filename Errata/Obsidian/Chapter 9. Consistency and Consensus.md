# [Consistency Guarantees]

Most replicated databases provide at least [eventual consistency] - a weak guarantee.

# [[Linearizability]]

Also known as [atomic consistency], [strong consistency], [immediate consistency], or [external consistency], a guarantee wherein the basic idea is to make the system appear as if there were only one copy of the data.

## [[Linearizability]] Versus [Serializability]

[Serializability] is an isolation property of [transactions] where every transaction may read and write multiple objects and guarantees transactions behave in some serial order. [[Linearizability]] is a recency guarantee on reads and writes of a register (i.e., an individual object).

## Relying on [Linearizability]

Locking and leader election, e.g., Apache Zookeeper and etcd for fault tolerance.

Constraints and uniqueness guarantee with atomic compare-and-sets to ensure concurrent operations are respected.

# Implementing [Linearizable] Systems

Use replication, e.g., Single-leader replication (leader has the primary copy of the data, potentially linearizable), Consensus algorithms (contain measures to prevent split-brain and stale replicas, linearizable), Multi-leader repliation (can produce conflicting writes, not linearizable), and Leaderless replication (not linearizable).

# The Cost of [Linearizability]

## The [CAP Theorem]

# Ordering Guarantees

[Single-leader replication] to determine the order of writes in the replication log, [Serializability] to ensure transactions behave as if they were executed in some sequential order, usage of [timestamps] and [clocks] to introduce order.

## Ordering and [Causality]

[Causality] imposes an ordering on events: cause comes before effect; a message is sent before that message is received; the question comes before the answer. If a system obeys the ordering imposed by [causality], we say it is [casually consistent].

### The causal order is not a total order

In a [linearizable] system, we have a total order of operations: if the system behaves as if there is only a single copy of the data, and every operation is atomic. [Causality] defines a partial order, not a total order: some operations are ordered with respect to each other, but some are incomparable.

### [Linearizability] is stronger than causal consistency

[Causal consistency] is the strongest possible consistency model that does not slow down due to network delays, and remains available in the face of network failures.

### Capturing causal dependencies

In order to determine the causal ordering, the database needs to know which version of the data was read by the application.
