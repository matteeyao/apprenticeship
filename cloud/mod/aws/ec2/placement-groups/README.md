# EC2 Placement Groups

Three types of placement groups:

* Clustered Placement Group

* Spread Placement Group

* Partitioned

## Clustered Placement Group

> A cluster placement group is a grouping of instances within a single Availability Zone (essentially a way of grouping EC2 instances close together within a single Availability Zone so that you have very low network latency). Placement groups are recommended for applications that need low network latency, high network throughput, or both

Only certain instances can be launched in to a Clustered Placement Group

## Spread Placement Group

> A spread placement group is a group of instances that are each placed on distinct underlying hardware

* Group of instances that are each placed on distinct underlying hardware

* These will be on separate racks w/ separate network inputs as well as separate power requirements

* If you have one rack that fails, it's only going to affect that one EC2 instance

> Spread placement groups are recommended for applications that have a small number of critical instances that should be kept separate from each other

* We can have spread placement groups inside different availability zones within one region

> Think individual instances

* W/ a spread placement group, think of individual instances

* So if a rack does fail, it's only going to affect one instance

* Opposite of Clustered Placement Group

    * Clustered placement groups put all your EC2 instances very close together so we can get really high performing network throughput and low latency

    * Spread placement groups are designed to protect your EC2 instances from hardware failure; it's individual instances put on individual racks either in the same availability zone or different availability zones depending on configuration

## Partitioned Placement Group

> When using partition placement groups, Amazon EC2 divides each group into logical segments called partitions. Amazon EC2 ensures that each partition within a placement group has its own set of racks. Each rack has its own network and power source. No two partitions within a placement group share the same racks, allowing you to isolate the impact of hardware failure within your application.

* Similar to Spread Placement Groups, except you can have multiple EC2 instances in a Partitioned Placement Group

* You can have multiple instances on a rack, which is completely separate from another rack

* If we are dealing w/ individual EC2 instances, then that is a Spread Placement Group → A Spread Placement group is for single instance, whereas Partition Placement Groups are for multiple instances, and then your Clustered Placement Groups are just a way of putting everything as close together as possible

> Think multiple instances

## Learning summary

> * Three types of Placement Groups:
>
>    * Clustered Placement Group
>
>        * Low Network Latency / High Network Throughput
>

* EC2 instances are close to each other as possible in the same availability zone in the same region

>    * Spread Placement Group
>
>        * Individual Critical EC2 instances...

* on separate pieces of hardware

* If one piece of hardware fails, it's going to isolate that EC2 instance; it's not going to knock out two EC2 instances at once; it's only going to be that one EC2 instance

>    * Partitioned
>
>        * Multiple EC2 instances HDFS, HBase, and Cassandra clusters

* This is where you have multiple EC2 instances in a partition, and each partition is always going to be on separate hardware or separate racks from the other partitions

> * A Clustered Placement Group can't span multiple Availability Zones.
>
> * A Spread Placement and Partitioned Group can (but they still have to be within the same region).
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
