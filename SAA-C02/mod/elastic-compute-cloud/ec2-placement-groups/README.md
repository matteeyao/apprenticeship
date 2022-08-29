# EC2 Placement Groups

> * Placement groups balance the tradeoff between risk tolerance and network performance when it comes to your fleet of EC2 instances. The more you care about risk, the more isolated you want your instances to be from each other. The more you care about performance, the more conjoined you want your instances to be w/ each other.

> There are three different types of placement groups:
>
> 1. **Clustered Placement Groups**
>
> * Clustered Placement Grouping is when you put all of your EC2 instances in a single availability zone. This is recommended for applications that need the lowest latency possible and require the highest network throughput.
>
> * Only certain instances can be launched into this group (compute optimized, GPU optimized, storage optimized, and memory optimized).
>
> 2. **Spread Placement Groups**
>
> *  Spread Placement Grouping is when you put each individual EC2 instance on top of its own distinct hardware so that failure is isolated.
>
> * Your VMs live on separate racks, w/ separate network inputs and separate power requirements. Spread placement groups are recommended for applications that have a small number of critical instances that should be kept separate from each other.
>
> 3. **Partitioned Placement Groups**
>
> * Partitioned Placement Grouping is similar to Spread Placement Grouping, but differs b/c you can have multiple EC2 instances within a single partition. Failure instead is isolated to a partition (say 3 or 4 instances instead of 1), yet you enjoy the benefits of close proximity for improved network performance.
>
> * W/ this placement group, you have multiple instance living together on the same hardware inside of different availability zones across one or more regions.
>
> * If you would like a balance of risk tolerance and network performance, use Partitioned Placement Groups.

When you launch a new EC2 instance, the EC2 service attempts to place the instance in such a way that all of your instances are spread out across underlying hardware to minimize correlated failures. You can use *placement groups* to influence the placement of a group of *interdependent* instances to meet the needs of your workload. Depending on the type of workload, you can create a placement group using one of the following placement strategies.

* **Cluster** ▶︎ packs instances close together inside an Availability Zone. This strategy enables workloads to achieve the low-latency network performance necessary for tightly-coupled node-to-node communication that is typical of HPC applications.

* **Partition** ▶︎ spreads your instances across logical partitions such that groups of instances in one partition do not share the underlying hardware w/ groups of instances in different partitions. This strategy is typically used by large distributed and replicated workloads, such as Hadoop, Cassandra, and Kafka.

* **Spread** ▶︎ strictly places a small group of instances across distinct underlying hardware to reduce correlated failures.

There is no charge for creating a placement group.

## Learning summary

> * Three types of Placement Groups:
>
>    * Clustered Placement Group
>
>        * Low Network Latency / High Network Throughput
>
>        * EC2 instances are close to each other as possible in the same availability zone in the same region
>
>    * Spread Placement Group
>
>        * Individual Critical EC2 instances...
>
>        * On separate pieces of hardware
>
>        * If one piece of hardware fails, it's going to isolate that EC2 instance; it's not going to knock out two EC2 instances at once; it's only going to be that one EC2 instance
>
>    * Partitioned
>
>        * Multiple EC2 instances HDFS, HBase, and Cassandra clusters
>
>        * This is where you have multiple EC2 instances in a partition, and each partition is always going to be on separate hardware or separate racks from the other partitions

> * A **Clustered Placement Group** can't span multiple Availability Zones.
>
> * A **Spread Placement** and **Partitioned Group** can (but they still have to be within the same region).
>
> * The name you specify for a placement group must be unique within your AWS account.
>
> * Only certain types of instances can be launched in a placement group (Compute Optimized, GPU, Memory Optimized, Storage Optimized)
>
> * AWS recommend homogenous instances within clustered placement groups
>
> * You can't merge placement groups
>
> * You can move an existing instance into a placement group. Before you move the instance, the instance must be in the stopped state. You can move or remove an instance using the AWS CLI or an AWS SDK, you can't do it via the console yet
