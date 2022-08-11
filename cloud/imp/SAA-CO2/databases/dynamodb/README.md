# DynamoDB

1. Which of the following are components of DynamoDB? (Select THREE).

[x] Attribute

[ ] File

[x] Item

[ ] Record

[x] Table

2. Which of the following are valid keys in DynamoDB tables? (Select TWO.)

[ ] Foreign key

[x] Partition key

[x] Primary key

[ ] Surrogate key

2. Which of the following statements are true of DynamoDB indexes? (Select three)

[x] You can have a table w/o an index.

[x] You can have more than one global secondary index on a table.

[x] You can have more than one local secondary index on a table.

[ ] You can have more than one non-clustered index on a table.

[ ] You can have only one clustered index per table.

**Explanation**: DynamoDB does not require you to configure indexing on tables, but it is recommended. DynamoDB allows you to add one or more secondary indexes to aid in query performance. You can add up to 20 global secondary indexes and up to 5 local secondary indexes per table.

3. Which of the following are valid capacity modes for DynamoDB (Select two)

[x] On-Demand

[x] Provisioned

[ ] Reserved

[ ] Spot

**Explanation**: DynamoDB has two capacity modes with specific billing options for processing reads and writes on your tables: on-demand and provisioned.

4. Which of the following statements are true of security in DynamoDB? (Select two)

[x] IAM is used to manage credentials for DynamoDB.

[ ] IAM policies are not applicable to users for DynamoDB.

[ ] Encryption at rest replaces encryption w/ AWS KMS.

[x] Fully managed encryption at rest is supported.

5. An application running in a private subnet accesses an Amazon DynamoDB table. The data cannot leave the AWS network to meet security requirements.

How should this requirement be met?

**Configure a VPC endpoint for DynamoDB and configure the endpoint policy.**

**Explanation**: The application can communicate w/ DynamoDB only within the Amazon network.

6. A company plans to deploy a new application in the AWS Cloud. The application reads and writes information to a database. The company will deploy the application in two different AWS Regions, and each application will write to a database in its Region. The databases in the two Regions need to keep the data synchronized w/ minimal latency. The databases must be eventually consistent. In case of data conflict, queries should return the most recent write.

Which solution will meet these requirements w/ the LEAST administrative work?

[ ] Use Amazon Athena w/ the data stored in Amazon S3 buckets in both regions. Use S3 Cross-Region Replication between the two S3 buckets.

[ ] Use AWS Data Migration Service (AWS DMS) change data capture (CDC) between an Amazon RDS for MySQL DB cluster in each region.

[x] Use Amazon DynamoDB. Configure the table as a global table.

[ ] Use an Amazon RDS for PostgreSQL DB cluster w/ a cross-Region read replica.

**Explanation**: Athena is a querying tool for data stored in S3, and is not a db, is more a db mgmt tool. CDC is a setting on Amazon RDS for MySQL DB cluster. There is no data conflict resolution solution for CDC. You can use read-replicas to offload queries, but do not address ability to write within region. Global table have built-in conflict resolution and is eventually consistent. Read shorter answers first for easiest method questions.

7. You are designing a leaderboard that expects to have millions of users. You want to ensure the lowest latency for this leaderboard. What datastore would be best suited?

[x] Amazon DynamoDB

[ ] Amazon RDS

[ ] AWS Glue

[ ] Amazon Redshift

**Explanation**: Redshift is designed for data warehousing and analytics. AWS Glue is a data cataloguing service. AWS Glue can pulling data from both Amazon RDS and Amazon S3, transforming the data into analytical results, then loading those results into the Amazon Redshift data warehouse. Low-latency and throughput should lead you to DynamoDB and Aurora Serverless. NoSQL databases (DynamoDB) are faster than relational (Aurora), however.

8. A company is using Amazon DynamoDB to stage its product catalog, which is 1 TB in size. A product entry consists of an average of 100 KB of data, and the average traffic is about 250 requests each second. A database administrator has provisioned 3,000 read capacity units (RCUs) of throughput. However, some products are popular among users. Users are experiencing delays or timeouts b/c of throttling. The popularity is expected to continue to increase, but the number of products will stay constant.

What should a solutions architect do as a long-term solution to the problem?

[ ] Increase the provisioned throughput to 6,000 RCUs.

[x] Use DynamoDB Accelerator (DAX) to maintain the frequently read items.

[ ] Augment DynamoDB by storing only the key product attributes, w/ the details stored in Amazon S3.

[ ] Change the partition key to consist of a hash of the product key and product type instead of just product key.

**Explanation**: Increasing the provisioned throughput to 6,000 RCUs would address the immediate solution, but would not address the long-term solution of popularity expected to increase. Augmenting DynamoDB by storing only the key product attributes, w/ the details stored in Amazon S3 would not address the long-term needs as querying S3 could increase in time, so the same problem would persist eventually. DAX provides increased throughput for repeated reads and reduced costs by eliminating the need to provision more RCUs.
