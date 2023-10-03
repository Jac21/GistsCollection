# Faults and Partial Failures

In a distributed system, there may well be some parts of the system that are broken in some unpredictable way, even though other parts of the system are working fine. This is known as a [partial failure], and are [nondeterministic].

## Cloud Computing and Supercomputing

High-performance-computing, i.e., thousands of CPUs used for computationally intensive tasks. 

Cloud computing, e.g., multi-tenant databases, elastic/on-demand resource allocation, etc.

Traditional enterprise lies somewhere in between.

## Unreliable Networks

[Shared-nothing systems] - a bunch of machines connected by a network. 

Usage of a [timeout] to handle faulty networks.

## Network Faults in Practice

When one part of the network is cut off from the rest due to a network fault, this is sometimes called a [network partition].

## Detecting Faults

The need to automatically detect faults - a load balancer needs to stop sending requests to dad nodes, or a distributed database with [single-leader replication] that has a failed leader that needs a new follower promoted. 



