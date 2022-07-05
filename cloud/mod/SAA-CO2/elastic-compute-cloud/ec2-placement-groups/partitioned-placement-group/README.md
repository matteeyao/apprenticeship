# Partitioned Placement Groups

> When using **Partition Placement Groups**, Amazon EC2 divides each group into logical segments called partitions. Amazon EC2 ensures that each partition within a placement group has its own set of racks. Each rack has its own network and power source. No two partitions within a placement group share the same racks, allowing you to isolate the impact of hardware failure within your application.
>
> *Think multiple instances*

* Similar to **Spread Placement Groups**, except you can have multiple EC2 instances in a **Partitioned Placement Group**

* You can have multiple instances on a rack, which is completely separate from another rack

* If we are dealing w/ individual EC2 instances, then that is a **Spread Placement Group** → A **Spread Placement Group** is for single instance, whereas **Partitioned Placement Groups** are for multiple instances, and then your **Clustered Placement Groups** are just a way of putting everything as close together as possible

**Partitioned Placement Groups** help reduce the likelihood of correlated hardware failures for your application. When using **Partitioned Placement Groups**, Amazon EC2 divides each group into logical segments called partitions. Amazon EC2 ensures that each partition within a placement group has its own set of racks. Each rack has its own network and power source. No two partitions within a placement group share the same racks, allowing you to isolate the impact of hardware failure within your application.

The following image is a simple visual representation of a partition placement group in a single Availability Zone. It shows instances that are placed into a partition placement group w/ three partitions-**Partition 1**, **Partition 2**, and **Partition 3**. Each partition comprises multiple instances. The instances in a partition do not share racks w/ the instances in the other partitions, allowing you to contain the impact of a single hardware failure to only the associated partition.

![Fig. 1 Placement Group Partition](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/images/placement-group-partition.png)

Partition placement groups can be used to deploy large distributed and replicated workloads, such as HDFS, HBase, and Cassandra, across distinct racks. When you launch instances into a **Partitioned Placement Group**, Amazon EC2 tries to distribute the instances evenly across the number of partitions that you specify. You can also launch instances into a specific partition to have more control over where the instances are placed.

A **Partitioned Placement Group** can have partitions in multiple Availability Zones in the same Region. A **Partitioned Placement Group** can have a maximum of seven partitions per Availability Zone. The number of instances that can be launched into a partition placement group is limited only by the limits of your account.

In addition, **Partitioned Placement Groups** offer visibility into the partitions-you can see which instances are in which partitions. You can share this information w/ topology-aware applications, such as HDFS, HBase, and Cassandra. These applications use this information to make intelligent data replication decisions for increasing data availability and durability.

If you start or launch an instance in a **Partitioned Placement Group** and there is insufficient unique hardware to fulfill the request, the request fails. Amazon EC2 makes more distinct hardware available over time, so you can try your request again later.
