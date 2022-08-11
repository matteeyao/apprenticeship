# Elastic Block Storage

1. A database is running on an Amazon EC2 instance. The database software has a backup feature that requires block storage. What storage option would be the lowest cost option for the backup data?

[ ] Amazon Glacier

[x] Amazon EBS Cold HDD Volume (sc1)

[ ] Amazon S3

[ ] Amazon EBS Throughput Optimized HDD Volume

**Explanation**: Cold HDD (sc1) is cheaper than Throughput Optimized HDD (st1)

2. A company runs a legacy application w/ a single-tier architecture on an Amazon EC2 instance. Disk I/O is low, w/ occasional small spikes during business hours. The company requires the instance to be stopped from 8 PM to 8 AM daily.

Which storage option is MOST appropriate for this workload?

[ ] Amazon EC2 instance storage

[x] Amazon EBS General Purpose SSD (gp2) storage

[ ] Amazon S3

[ ] Amazon EBS Provisioned IOPS SSD (io2) storage

**Explanation**: Instance storage is ephemeral. S3 is for object storage. Provisioned IOPS SSD (io2) is for applications w/ very high IOPS. General Purpose SSD (gp2) provides the balance between price and performance for an application. GP2 can burst up to 3000 IOPS.
