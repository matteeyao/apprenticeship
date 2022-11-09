# High Availability in a Swarm Cluster

1. We have seven Swarm manager nodes total. How should we distribute them across just three availability zones?

[x] 3-2-2

> This setup enables us to lose any of the availability zones and still maintain the quorum.

[ ] 6-1-0

[ ] 4-2-1

[ ] 5-1-1

2. Which of the following scenarios would still allow the quorum to complete maintenance in a swarm cluster? (Choose two)

[ ] A 3-node cluster with 2 nodes down.

[x] A 3-node cluster with 1 node down.

[ ] A 4-node cluster with 2 nodes down.

[x] A 7-node cluster with 3 nodes down.

> More than half of the nodes are still up, so the quorum is maintained in this scenario.

3. In terms of establishing a high level of availability, what is the benefit of having more manager nodes present in a swarm?

[ ] It would result in being more efficient with resources.

[x] It would help bolster fault-tolerance.

[ ] The cluster would be easier to set up.

[ ] It would allow for more worker nodes to be present.

> Increasing the number of manager nodes means that more nodes can be lost while still maintaining a functional cluster.
