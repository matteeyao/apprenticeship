# EMR

> **Amazon Elastic Map Reduce (EMR)** is the industry-leading cloud big data platform for processing vast amounts of data using open-source tools such as Apache Spark, Apache Hive, Apache HBase, Apache Flink, Apache Hudi, and Presto. W/ EMR, you can run petabyte-scale analysis at **less than half the cost of traditional on-premises solutions** and over three times faster than standard Apache Spark.

* EMR is a solution for big data that's hosted within AWS.

> The central component of **Amazon EMR** is the cluster. A cluster is a collection of Amazon Elastic Compute Cloud (Amazon EC2) instances. Each instance in the cluster is called a node. Each node has a role within the cluster, referred to as the node type.
>
> Amazon EMR also installs **different software components on each node type**, giving each node a role in a distributed application like Apache Hadoop.

> The node types in Amazon EMR are as follows:
>
> **Master node**: A node that manages the cluster. The master node tracks the **status of tasks** and monitors the health of the cluster. Every cluster has a master node.
>
> **Core node**: A node w/ software components that **runs tasks and stores data** in the Hadoop Distributed File System (HDFS) on your cluster. Multi-node clusters have at least one core node.
>
> **Task node**: A node w/ software components that only runs tasks and **does not store data in HDFS**. Task nodes are **optional**.

* Logs are stored within the **Master node**, so if you lose the master node, you lose all of your log data

* e.g. You've got an EMR cluster and you need the logs data on your master node to persist. What should you do? → move your log data to S3.

> You can configure a cluster to **periodically archive the log files stored on the master node to Amazon S3**. This ensures the log files are available after the cluster terminates, whether this is through normal shutdown or due to an error. Amazon EMR archives the log files to Amazon S3 at **five-minute intervals**.

* You can only set this up when you're first creating the cluster.

* Thus, you can persists your log files to S3, but you need to do that when you set up the cluster.

## Learning summary

> * EMR is used for **big data processing**.
>
> * Consists of a **master node**, a **core node**, and (optionally) a **task node**.
>
> * By default, log data is **stored on the master node**.
>
> * You can configure replication to S3 on **five-minute intervals for all log data from the master node**; however, this can only be configured when creating the cluster for the first time.
