# Databases questions

1. What data transfer charge is incurred when replicating data between Availability Zones for your Amazon RDS MySQL in a Multi-AZ deployment?

There is no charge associated w/ this action.

Data transferred between Availability Zones for replication of Multi-AZ deployments is free. Reference [Amazon RDS for MySQL Pricing: Data Transfer](https://aws.amazon.com/rds/mysql/pricing/).

2. How many copies of my data does RDS - Aurora store by default?

**6**. Amazon Aurora automatically maintains 6 copies of your data across 3 Availability Zones. Reference: [Amazon Aurora FAQs](https://aws.amazon.com/rds/aurora/faqs/#Backup_and_Restore])

3. Which of the following is most suitable for OLAP (Online Analytical Processing)?

**Redshift**. Redshift is the most suitable AWS database for OLAP (Online Analytical Processing).

4. Which of the following is NOT a feature supported by DynamoDB?

[x] The ability to perform operations by using a user-defined primary key

[ ] Amazon DynamoDB supports MongoDB workloads

[x] The primary key can be either a single-attribute or a composite partition-sort key

[x] Data reads that are either eventually consistent or strongly consistent

*The primary key can be either a single-attribute or a composite partition-sort key* is a feature supported by DynamoDB. A primary key can be either a single-attribute partition key or a composite partition-sort key. A single-attribute partition key could be, for example, `UserID`. Such a single attribute partition key would allow you to quickly read and write data for an item associated w/ a given user ID.

*Amazon DynamoDB supports MongoDB workloads* is not a feature supported by DynamoDB. Amazon DocumentDB (w/ MongoDB compatibility) is a fast, scalable, highly available, and fully managed document database service that supports MongoDB workloads.

5. Which of the following data formats does Amazon Athena support?

**JSON**, **Apache ORC**, **Apache Parquet**. Amazon Athena is an interactive query service that makes it easy to analyze data in Amazon S3, using standard SQL commands. It will work w/ a number of data formats including "JSON", "Apache Parquet", "Apache ORC" amongst others, but "XML" is not a format that is supported.

6. RDS Reserved instances are available for multi-AZ deployments.

**True**. Reserved DB instance benefits apply for both Multi-AZ and Single-AZ configurations. Reference: [Reserved DB instances for Amazon RDS](https://docs.aws.amazon.com/AmazonRDS/latest/UserGuide/USER_WorkingWithReservedDBInstances.html).

7. Which set of RDS database engines is currently available?

Amazon RDS supports Amazon Aurora, MySQL, MariaDB, Oracle, SQL Server, and PostgreSQL database engines.

8. AWS's NoSQL product offering is known as _.

**DynamoDB**

9. In RDS, when are changes to the backup window implemented?

During the next scheduled maintenance window or immediately.

When applying changes to the backup window, you can choose either to have the changes done during th next scheduled maintenance window or immediately. The schedule itself is modified right away.

10. MySQL installations default to port number _.

**3306**. The default endpoint port for MySQL installations is 3306.

11. Amazon's ElastiCache uses which two engines?

**Redis & Memcached**. 

12. W/ new RDS DB instances, automated backups are enabled by default?

**True**

13. What happens to the I/O operations of a single-AZ RDS instance during a database snapshot or backup?

I/O may be briefly suspended while the backup process initializes (typically under a few seconds), and you may experience a brief period of elevated latency.

14. Which AWS service is ideal for Business Intelligence Tools/Data Warehousing?

**Redshift**

15. You can SSH into and control the operating system where your Amazon RDS MySQL instance is running.

**False**. Amazon RDS provides a managed database offering, so you can't SSH and have control over the underlying operating system configurations where your Amazon RDS MySQL instance is running. You can only have such control when you deploy and manage your databases on EC2 instances.

16. If you are using Amazon RDS Provisioned IOPS storage w/ a Microsoft SQL Server database engine, what is the maximum size RDS volume you can have by default?

**16 TB**. You can create Amazon RDS for SQL Server database instance w/ up to 16TB of storage. The 16TB storage limit is available when using the Provisioned IOPS and General Purpose (SSD) storage types. Reference: [Amazon RDS for SQL Server Increases Maximum Database Storage Size to 16TB](https://aws.amazon.com/about-aws/whats-new/2017/08/amazon-rds-for-sql-server-increases-maxiumum-database-storage-size-to-16-tb/#:~:text=Amazon%20RDS%20for%20SQL%20Server%20Increases%20Maximum%20Database%20Storage%20Size%20to%2016TB,-Posted%20On%3A%20Aug&text=You%20can%20now%20create%20Amazon,Purpose%20(SSD)%20storage%20types.)

17. In RDS, what is the maximum volume I can set for my backup retention period?

**35 Days**

18. You are hosting a MySQL database on the root volume of an EC2 instance. The database is using a large number of IOPS, and you need to increase the number of IOPS available to it. What should you do?

[ ] Migrate the database to an S3 bucket.

[ ] Migrate the database to Glacier.

[ ] Use CloudFront to cache the database.

[x] Add 2 additional EBS SSD volumes and create a RAID 0 volume to host the database.

CloudFront does not increase IOPS, CloudFront is a Content Delivery Network that helps deliver content closer to the consumers of the content.

RAID 0 provides performance improvements compared w/ a single volume as data can be read and written to multiple disks simultaneously. 2 disks, each w/ a bandwidth of 4,000 IOPS will provide a combined bandwidth of 8,000 IOPS.

19. DB security groups are used w/ DB instances that are not in a VPC and on the EC2-Classic platform. When you create a DB security group, you need to specify a destination port number.

**False**

> You don't need to specify a destination port number when you create DB security group rules. The port number defined for the DB instance is used as the destination port number for all rules defined for the DB security group. DB security groups can be created using the Amazon RDS API operations or the Amazon RDS page of the AWS Management Console. Reference: [DB security groups](https://docs.aws.amazon.com/AmazonRDS/latest/UserGuide/Overview.RDSSecurityGroups.html)

20. Which SQL-based relational database is suitable for high-performance OLTP (Online Transactional Processing) workloads?

**Amazon RDS w/ Provisioned IOPS (SSD) Storage**. Amazon RDS w/ provisioned IOPS (SSD) storage allows you to implement a SQL-based relational database solution for your high-performance OLTP workloads. [Amazon RDS FAQs ▶︎ Hardware and Scaling](https://aws.amazon.com/rds/faqs/#Hardware_and_Scaling)

21. Which of the following AWS services is a non-relational database?

**Amazon DynamoDB**

Amazon DynamoDB is a non-relational database that delivers reliable performance at any scale. It's a fully managed, multi-region, multi-master database that provides consistent single-digit millisecond latency, and offers built-in security, backup and restore, and in-memory caching. Reference: [What Is a Key-Value Database?](https://aws.amazon.com/nosql/key-value/)

22. Which of the following DynamoDB features are chargeable, when using a single region?

[ ] Local secondary indexes

[x] Read and Write Capacity

[x] Storage of Data

[ ] Incoming Data Transfer
 
There will always be a charge for provisioning read and write capacity and the storage of data within DynamoDB, therefore these two answers are correct. There is no charge for the transfer of data into DynamoDB, providing you stay within a single region (if you cross regions, you will be charged at both ends of the transfer). There is no charge for local secondary indexes.

23. Under what circumstances would I choose provisioned IOPS over standard storage when creating and RDS instance?

If you need to run an I/O-intensive relational database for a mission-critical application in production.

Provisioned IOPS becomes important when you are running production environments requiring rapid responses, such as those which run e-commerce websites. W/o high performant responses from an RDS instance page loads of the website could suffer resulting in loss of business. If your workloads are not latency sensitive or you are running a test environment the additional cost of provisioned IOPS will not be cost beneficial to your project.

24. If I wanted to run a database on an EC2 instance, which of the following storage options would Amazon recommend?

**EBS**. Elastic Block Storage (EBS) is recommended block level storage for EC2 instances if you were running a database on an EC2 instance. If the question didn't focus on the solution being on the instance, RDS would be the preferred choice.

25. When creating a single-AZ Amazon RDS instance, you can select the Availability Zone into which you deploy it.

**True**. When you create a DB instance, you can choose an Availability Zone or have AWS choose one for you. An Availability Zone is represented by an AWS Region code followed by a letter identifier (for example, `us-east-1a`). Reference: [RDS - Regions, Availability Zones, and Local Zones](https://docs.aws.amazon.com/AmazonRDS/latest/UserGuide/Concepts.RegionsAndAvailabilityZones.html)

26. If you want your application to check RDS for an error, have it look for an __ node in the response from the Amazon RDS API.

**Error**. Typically, you want your application to check whether a request generated an error before you spend any time processing results. The easiest way to find out if an error occurred is to look for an `Error node` in the response from the Amazon RDS API. Reference: [Troubleshooting applications on Amazon RDS](https://docs.aws.amazon.com/AmazonRDS/latest/UserGuide/APITroubleshooting.html)
