# DynamoDB

1. A Docker application, which is running on an Amazon ECS cluster behind a load balancer, is heavily using DynamoDB. You are instructed to improve the database performance by distributing the workload evenly and using the provisioned throughput efficiently.

Which of the following would you consider to implement for your DynamoDB table?

[ ] Reduce the number of partition keys in the DynamoDB table.

[ ] Use partition keys w/ low-cardinality attributes, which have a few number of distinct values for each item.

[ ] Avoid using a composite primary key, which is composed of a partition key and a sort key.

[x] Use partition keys w/ high-cardinality attributes, which have a large number of distinct values for each item.

**Explanation**: The partition key portion of a table's primary key determines the logical partitions in which a table's data is stored. This in turn affects the underlying physical partitions. Provisioned I/O capacity for the table is divided evenly among these physical partitions. Therefore a partition key design that doesn't distribute I/O requests evenly can create "hot" partitions that result in throttling and use your provisioned I/O capacity inefficiently.

The optimal usage of a table's provisioned throughput depends not only on the workload patterns of individual items, but also on the partition-key design. This doesn't mean that you must access all partition key values to achieve an efficient throughput level, or even that the percentage of accessed partition key values must be high. It does mean that the more distinct partition key values that your workload accesses, the more those requests will be spread across the partitioned space. In general, you will use your provisioned throughput more efficiently as the ratio of partition key values accessed to the total number of partition key values increases.

One example for this is the use of **partition keys with high-cardinality attributes, which have a large number of distinct values for each item**.

> **Reducing the number of partition keys in the DynamoDB table** is incorrect. Instead of doing this, you should actually add more to improve its performance to distribute the I/O requests evenly and not avoid "hot" partitions.

> **Using partition keys with low-cardinality attributes, which have a few number of distinct values for each item** is incorrect because this is the exact opposite of the correct answer. Remember that the more distinct partition key values your workload accesses, the more those requests will be spread across the partitioned space. Conversely, the less distinct partition key values, the less evenly spread it would be across the partitioned space, which effectively slows the performance.

> **Avoid using a composite primary key, which is composed of a partition key and a sort key** is incorrect because as mentioned, a composite primary key will provide more partition for the table and in turn, improves the performance. Hence, it should be used and not avoided.

<br />

2. You are designing a leaderboard that expects to have millions of users. You want to ensure the lowest latency for this leaderboard. What datastore would be best suited?

[x] Amazon DynamoDB

[ ] Amazon RDS

[ ] AWS Glue

[ ] Amazon Redshift

**Explanation**: Redshift is designed for data warehousing and analytics. AWS Glue is a data cataloguing service. AWS Glue can pulling data from both Amazon RDS and Amazon S3, transforming the data into analytical results, then loading those results into the Amazon Redshift data warehouse. Low-latency and throughput should lead you to DynamoDB and Aurora Serverless. NoSQL databases (DynamoDB) are faster than relational (Aurora), however.

<br />

3. A company is using Amazon DynamoDB to stage its product catalog, which is 1 TB in size. A product entry consists of an average of 100 KB of data, and the average traffic is about 250 requests each second. A database administrator has provisioned 3,000 read capacity units (RCUs) of throughput. However, some products are popular among users. Users are experiencing delays or timeouts b/c of throttling. The popularity is expected to continue to increase, but the number of products will stay constant.

What should a solutions architect do as a long-term solution to the problem?

[ ] Increase the provisioned throughput to 6,000 RCUs.

[x] Use DynamoDB Accelerator (DAX) to maintain the frequently read items.

[ ] Augment DynamoDB by storing only the key product attributes, w/ the details stored in Amazon S3.

[ ] Change the partition key to consist of a hash of the product key and product type instead of just product key.

**Explanation**: Increasing the provisioned throughput to 6,000 RCUs would address the immediate solution, but would not address the long-term solution of popularity expected to increase. Augmenting DynamoDB by storing only the key product attributes, w/ the details stored in Amazon S3 would not address the long-term needs as querying S3 could increase in time, so the same problem would persist eventually. DAX provides increased throughput for repeated reads and reduced costs by eliminating the need to provision more RCUs.

<br />

4. Which of the following are components of DynamoDB? (Select THREE).

[x] Attribute

[ ] File

[x] Item

[ ] Record

[x] Table

<br />

5. Which of the following are valid keys in DynamoDB tables? (Select TWO.)

[ ] Foreign key

[x] Partition key

[x] Primary key

[ ] Surrogate key

<br />

6. Which of the following statements are true of DynamoDB indexes? (Select three)

[x] You can have a table w/o an index.

[x] You can have more than one global secondary index on a table.

[x] You can have more than one local secondary index on a table.

[ ] You can have more than one non-clustered index on a table.

[ ] You can have only one clustered index per table.

**Explanation**: DynamoDB does not require you to configure indexing on tables, but it is recommended. DynamoDB allows you to add one or more secondary indexes to aid in query performance. You can add up to 20 global secondary indexes and up to 5 local secondary indexes per table.

7. Which of the following are valid capacity modes for DynamoDB (Select two)

[x] On-Demand

[x] Provisioned

[ ] Reserved

[ ] Spot

**Explanation**: DynamoDB has two capacity modes with specific billing options for processing reads and writes on your tables: on-demand and provisioned.

<br />

8. Which of the following statements are true of security in DynamoDB? (Select two)

[x] IAM is used to manage credentials for DynamoDB.

[ ] IAM policies are not applicable to users for DynamoDB.

[ ] Encryption at rest replaces encryption w/ AWS KMS.

[x] Fully managed encryption at rest is supported.

<br />

9. An application running in a private subnet accesses an Amazon DynamoDB table. The data cannot leave the AWS network to meet security requirements.

How should this requirement be met?

**Configure a VPC endpoint for DynamoDB and configure the endpoint policy.**

**Explanation**: The application can communicate w/ DynamoDB only within the Amazon network.

<br />

10. A company plans to deploy a new application in the AWS Cloud. The application reads and writes information to a database. The company will deploy the application in two different AWS Regions, and each application will write to a database in its Region. The databases in the two Regions need to keep the data synchronized w/ minimal latency. The databases must be eventually consistent. In case of data conflict, queries should return the most recent write.

Which solution will meet these requirements w/ the LEAST administrative work?

[ ] Use Amazon Athena w/ the data stored in Amazon S3 buckets in both regions. Use S3 Cross-Region Replication between the two S3 buckets.

[ ] Use AWS Data Migration Service (AWS DMS) change data capture (CDC) between an Amazon RDS for MySQL DB cluster in each region.

[x] Use Amazon DynamoDB. Configure the table as a global table.

[ ] Use an Amazon RDS for PostgreSQL DB cluster w/ a cross-Region read replica.

**Explanation**: Athena is a querying tool for data stored in S3, and is not a db, is more a db mgmt tool. CDC is a setting on Amazon RDS for MySQL DB cluster. There is no data conflict resolution solution for CDC. You can use read-replicas to offload queries, but do not address ability to write within region. Global table have built-in conflict resolution and is eventually consistent. Read shorter answers first for easiest method questions.

<br />

11. A popular social network is hosted in AWS and is using a DynamoDB table as its database. There is a requirement to implement a 'follow' feature where users can subscribe to certain updates made by a particular user and be notified via email. Which of the following is the most suitable solution that you should implement to meet the requirement?

[ ] Set up a DAX cluster to access the source DynamoDB table. Create a new DynamoDB trigger and a Lambda function. For every update made in the user data, the trigger will send data to the Lambda function which will then notify the subscribers via email using SNS.

[ ] Using the Kinesis Client Library (KCL), write an application that leverage on DynamoDB Streams Kinesis Adapter that will fetch data from the DynamoDB Streams endpoint. When there are updates made by a particular user, notify the subscribers via email using SNS.

[ ] Create a Lambda function that uses DynamoDB Streams Kinesis Adapter which will fetch data from the DynamoDB Streams endpoint. Set up an SNS Topic that will notify the subscribers via email when there is an update made by a particular user.

[x] Enable DynamoDB Stream and create an AWS Lambda trigger, as well as the IAM role which contains all of the permissions that the Lambda function will need at runtime. The data from the stream record will be processed by the Lambda function which will then publish a message to SNS Topic that will notify the subscribers via email.

**Explanation**: A **DynamoDB** stream is an ordered flow of information about changes to items in an Amazon DynamoDB table. When you enable a stream on a table, DynamoDB captures information about every modification to data items in the table.

Whenever an application creates, updates, or deletes items in the table, DynamoDB Streams writes a stream record with the primary key attribute(s) of the items that were modified. A *stream record* contains information about a data modification to a single item in a DynamoDB table. You can configure the stream so that the stream records capture additional information, such as the "before" and "after" images of modified items.

Amazon DynamoDB is integrated with AWS Lambda so that you can create *triggers*—pieces of code that automatically respond to events in DynamoDB Streams. With triggers, you can build applications that react to data modifications in DynamoDB tables.

If you enable DynamoDB Streams on a table, you can associate the stream ARN with a Lambda function that you write. Immediately after an item in the table is modified, a new record appears in the table's stream. AWS Lambda polls the stream and invokes your Lambda function synchronously when it detects new stream records. The Lambda function can perform any actions you specify, such as sending a notification or initiating a workflow.

Hence, the correct answer in this scenario is the option that says: **Enable DynamoDB Stream and create an AWS Lambda trigger, as well as the IAM role which contains all of the permissions that the Lambda function will need at runtime. The data from the stream record will be processed by the Lambda function which will then publish a message to SNS Topic that will notify the subscribers via email**.

> The option that says: **Using the Kinesis Client Library (KCL), write an application that leverages on DynamoDB Streams Kinesis Adapter that will fetch data from the DynamoDB Streams endpoint. When there are updates made by a particular user, notify the subscribers via email using SNS** is incorrect. Although this is a valid solution, it is missing a vital step which is to enable DynamoDB Streams. With the DynamoDB Streams Kinesis Adapter in place, you can begin developing applications via the KCL interface, with the API calls seamlessly directed at the DynamoDB Streams endpoint. Remember that the DynamoDB Stream feature is not enabled by default.

> The option that says: **Create a Lambda function that uses DynamoDB Streams Kinesis Adapter which will fetch data from the DynamoDB Streams endpoint. Set up an SNS Topic that will notify the subscribers via email when there is an update made by a particular user** is incorrect because just like in the above, you have to manually enable DynamoDB Streams first before you can use its endpoint.

> The option that says: **Set up a DAX cluster to access the source DynamoDB table. Create a new DynamoDB trigger and a Lambda function. For every update made in the user data, the trigger will send data to the Lambda function which will then notify the subscribers via email using SNS** is incorrect because the DynamoDB Accelerator (DAX) feature is primarily used to significantly improve the in-memory read performance of your database, and not to capture the time-ordered sequence of item-level modifications. You should use DynamoDB Streams in this scenario instead.

<br />

12. An IT consultant is working for a large financial company. The role of the consultant is to help the development team build a highly available web application using stateless web servers.

In this scenario, which AWS services are suitable for storing session state data? (Select TWO.)

[ ] Redshift Spectrum

[x] DynamoDB

[ ] RDS

[x] Elasticache

[ ] Glacier

**Explanation**: **DynamoDB** and **ElastiCache** are the correct answers. You can store session state data on both DynamoDB and ElastiCache. These AWS services provide high-performance storage of key-value pairs which can be used to build a highly available web application.

> **Redshift Spectrum** is incorrect since this is a data warehousing solution where you can directly query data from your data warehouse. Redshift is not suitable for storing session state, but more on analytics and OLAP processes.

> **RDS** is incorrect as well since this is a relational database solution of AWS. This relational storage type might not be the best fit for session states, and it might not provide the performance you need compared to DynamoDB for the same cost.

> **S3 Glacier** is incorrect since this is a low-cost cloud storage service for data archiving and long-term backup. The archival and retrieval speeds of Glacier is too slow for handling session states.

<br />
