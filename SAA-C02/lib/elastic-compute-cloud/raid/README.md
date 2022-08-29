# RAID

1. A company plans to deploy an application in an Amazon EC2 instance. The application will perform the following tasks:

* Read large datasets from an Amazon S3 bucket.

* Execute multi-stage analysis on the datasets.

* Save the results to Amazon RDS.

During multi-stage analysis, the application will store a large number of temporary files in the instance storage. As the Solutions Architect, you need to recommend the fastest storage option with high I/O performance for the temporary files.

Which of the following options fulfills this requirement?

[ ] Attach multiple Provisioned IOPS SSD volumes in the instance.

[ ] Configure RAID 1 in multiple instance store volumes.

[ ] Configure RAID 0 in multiple instance store volumes.

[ ] Enable Transfer Acceleration in Amazon S3.

**Explanation**: **Amazon Elastic Compute Cloud (Amazon EC2)** provides scalable computing capacity in the Amazon Web Services (AWS) Cloud. You can use Amazon EC2 to launch as many or as few virtual servers as you need, configure security and networking, and manage storage. Amazon EC2 enables you to scale up or down to handle changes in requirements or spikes in popularity, reducing your need to forecast traffic.

RAID 0 configuration enables you to improve your storage volumes' performance by distributing the I/O across the volumes in a stripe. Therefore, if you add a storage volume, you get the straight addition of throughput and IOPS. This configuration can be implemented on both EBS or instance store volumes. Since the main requirement in the scenario is storage performance, you need to use an instance store volume. It uses NVMe or SATA-based SSD to deliver high random I/O performance. This type of storage is a good option when you need storage with very low latency, and you don't need the data to persist when the instance terminates.

Hence, the correct answer is: **Configure RAID 0 in multiple instance store volumes.**

> The option that says: **Enable Transfer Acceleration in Amazon S3** is incorrect because S3 Transfer Acceleration is mainly used to speed up the transfer of gigabytes or terabytes of data between clients and an S3 bucket.

> The option that says: **Configure RAID 1 in multiple instance volumes** is incorrect because RAID 1 configuration is used for data mirroring. You need to configure RAID 0 to improve the performance of your storage volumes.

> The option that says: **Attach multiple Provisioned IOPS SSD volumes in the instance** is incorrect because persistent storage is not needed in the scenario. Also, instance store volumes have greater I/O performance than EBS volumes.

<br />
