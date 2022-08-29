# Service Technical Overview

**Amazon Aurora** is a fully managed relational database engine that's compatible with MySQL and PostgreSQL. You can use the code, tools, and applications with Aurora that you use today with your existing MySQL and PostgreSQL databases. With some workloads, Aurora can deliver up to five times the throughput of MySQL and up to three times the throughput of PostgreSQL without requiring changes to most of your existing applications.

As with **Amazon Relational Database Service** (Amazon RDS), the basic building block of Aurora is the database instance class. This determines the amount of memory, CPU, and I/O capabilities available to the database engine. Aurora supports two types of instances: memory-optimized and burstable performance. Memory-optimized instances are suitable for most Aurora databases. Burstable performance instances are best when your database may experience short-lived bursts of activity.

**Aurora** offers two database engines: MySQL and PostgreSQL. Once these choices are made, you can then begin loading data into your database.

**Aurora** can have up to 15 read replicas that can be used to improve response time for queries and provide enhanced performance as well as durability for database instances.

The Amazon Aurora Global Database is a feature available for Aurora MySQL that allows a single Aurora database to span multiple AWS Regions. Data is replicated with no impact on database performance. It enables fast local reads in each Region with typical latency of less than a second and provides disaster recovery from Region-wide outages.

## Security

There are four important considerations for security in Aurora databases.

First, its proximity to the internet. The best practice is to restrict access to your database by placing it in an **Amazon Virtual Private Cloud**, or **VPC**. There may be instances where you must accept requests from the internet. In this case, you must create an internet gateway.

Second, controlling access to the database instance. Security groups control access to a database instance. Amazon RDS can use three types of security groups: database, VPC, and EC2.

Aurora utilizes **AWS Identity and Access Management**, or **IAM**, to create and manage credentials. The same users and roles you have in IAM can also be used with Aurora. Aurora requires both authentication and permission to access tables and data. IAM policies assign permissions that determine who can manage database resources.

Third, securing communications to and from the database instance. This is known as data in transit. This is done by using HTTPS connections. These connections are encrypted using SSL.

Finally, protecting data in the database. **Aurora** uses the industry standard AES-256 bit encryption algorithm to encrypt the data and database snapshots while at rest.

## Integration w/ other services

Gathering data from public sources can be beneficial to many analytical processes. You can use **Amazon Kinesis Data Firehouse** to gather data from a weather website. This can trigger an **AWS Lambda** function that will transform the data into a consistent format and then store it in an **Amazon S3 bucket**. **AWS Database Migration Service**, or AWS DMS, can then gather new records from the Amazon S3 bucket and migrate them into an Aurora database.

Knowing how your databases are being used and their health is an important part of maintaining well-running systems. You can use **Amazon CloudWatch** to log the activities of users in the database as well as basic database operations. These **CloudWatch** logs can be searched using **Amazon Elasticsearch Service** and then visualized using **Amazon QuickSight**. Alternatively, you can export the CloudWatch logs to **Amazon S3** and use **Amazon Athena** to query them for important insights.

## Public source data ingestion architecture

Many applications rely on data from public sources to meet the needs of their users. This architecture is one way to accomplish this task.

![Fig. 1 Public source data ingestion architecture](../../../../../imp/SAA-CO2/databases/aurora/service-technical-overview/diag01.png)

> ### Public data source
>
> *Public data is everywhere. Using it to enhance the abilities of your applications and analysis can be the difference between having a functional app and having one that is a huge success.*
>
> **In this architecture**, data is being gathered from a website containing a public weather data source.

> ### Amazon Kinesis Data Firehose
>
> *Kinesis Data Firehose is a service that can capture, transform, and load streaming data into an Amazon S3 bucket. The result is called a data stream.*
>
> **In this architecture**, Kinesis Data Firehose gathers data from the weather website and sends it on to a Lambda function.

> ### AWS Lambda
>
> *Lambda lets you run code, called function, without provisioning or managing servers.*
>
> **In this architecture**, the Lambda function takes the data from the data stream and transforms it into a consistent format before storing it in an Amazon S3 bucket.

> ### Amazon Simple Storage Service (Amazon S3)
>
> *Amazon S3 is a data repository.*
>
> **In this architecture**, Amazon S3 holds the data gathered by the Lambda function.

> ### AWS Database Migration Service (AWS DMS)
>
> *AWS DMS is a service that migrates data from one source into an AWS database service.*
>
> **In this architecture**, AWS DMS takes the data from the Amazon S3 bucket, transforms it, and loads it into an Aurora table within the designated database.

> ### Amazon Aurora
>
> **In this architecture**, Aurora can now take the data from the table created by AWS DMS and use it to enrich the other data generated and used by applications using the database.

## Log analytics architecture

Knowing how your databases are being used and their health is an important part of maintaining well-running systems. Aurora regularly generates logs on activities of users and the database. You can analyze these logs to ensure that users are getting the responses they require and the database is running optimally. This architecture is one option for creating a log analytics system.

![Fig. 2 Log analytics architecture](../../../../../imp/SAA-CO2/databases/aurora/service-technical-overview/diag01.png)

> ### Amazon Aurora
>
> *Aurora is the database system.*
>
> **In this architecture**, users access the database through an application. Logging the activity of the application can help you to determine if it is running properly and meeting the needs of your users.

> ### Amazon CloudWatch
>
> *CloudWatch is a monitoring and management service that provides you w/ the data and actionable insights to monitor and understand what is going on in your applications.*
>
> **In this architecture**, CloudWatch gathers the log files containing the activities of users as well as basic database operations. You can also export these logs to Amazon S3.

> ### Amazon Elasticsearch Service
>
> *Amazon ES is a fully-managed service that enables you to securely ingest data from any source and search, analyze, and visualize the data in real time.*
>
> **In this architecture**, Amazon ES gathers data from the CloudWatch logs, catalogs it, and makes it available for analysis and visualization.

> ### Amazon QuickSight
>
> *Amazon QuickSight is a fast, cloud-powered business intelligence service that makes it easy to deliver insights to everyone in your organization.*
>
> **In this architecture**, Amazon QuickSight can visualize data contained within Amazon ES. That data reflects usage of the Aurora database.

> ### Amazon Simple Storage Service (Amazon S3)
>
> *An Amazon S3 bucket is a storage location for many types of data.*
>
> **In this architecture**, you can store the log files from CloudWatch indefinitely for future analysis and to meet retention requirements.

> ### Amazon Athena
>
> *Amazon Athena is an interactive query service that makes it easy to analyze data in Amazon S3 using standard SQL.*
>
> **In this architecture**, you can use Athena to query the archived log files.
