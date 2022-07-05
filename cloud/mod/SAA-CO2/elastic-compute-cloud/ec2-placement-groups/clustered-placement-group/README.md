# Clustered Placement Groups

> A **Clustered Placement Group** is a grouping of instances within a single Availability Zone (essentially a way of grouping EC2 instances close together within a single Availability Zone so that you have very low network latency). Placement groups are recommended for applications that need low network latency, high network throughput, or both

Only certain instances can be launched in to a **Clustered Placement Group**

A **Clustered Placement Group** can span peered VPCs in the same Region. Instances in the same cluster placement group enjoy a higher per-flow throughput limit for TCP/IP traffic and are placed in the same high-bisection bandwidth segment of the network.

**Clustered Placement Groups** are recommended for applications that benefit from low network latency, high network throughput, or both. They are also recommended when the majority of the network traffic is between the instances in the group. To provide the lowest latency and the highest packet-per-second network performance for your placement group, choose an instance type that supports enhanced networking.

We recommend that you launch your instances in the following way:

* Use a single launch request to launch the number of instances that you need in the placement group.

* Use the same instance type for all instances in the placement group.

If you try to add more instances to the placement group later, or if you try to launch more than one instance in the placement group, you increase your chances of getting an insufficient capacity error.

If you stop an instance in a placement group and then start it again, it still runs in the placement group. However, the start fails if there isn't enough capacity for the instance.

If you receive a capacity error when launching an instance in a placement group that already has running instances, stop and start all of the instances in the placement group, and try the launch again. Starting the instances may migrate them to hardware that has capacity for all of the requested instances.
