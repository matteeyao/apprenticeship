# Real-time data analytics architecture

Amazon RDS databases are busy places. When real-time data analytics are run directly against the Amazon RDS database, it can cause latency. One solution is to create an architecture that moves these records off the database for analysis.

![Fig. 1 Real-time data analytics architecture](../../../../../img/SAA-CO2/databases/relational-database-service/data-analytics-architecture/diag01.png)

> ### Amazon RDS
>
> **In this architecture**, Amazon RDS databases can use stored procedures to integrate w/ Lambda functions. The stored procedure in this architecture is configured to execute whenever a new record is inserted in the sales table.

> ### AWS Lambda function
>
> *Lambda lets you run code without provisioning or managing servers.*
>
> **In this architecture**, the Lambda function gathers the newly added record from the Amazon RDS stored procedure and passes it on to Kinesis Data Firehose.

> ### Amazon Kinesis Data Firehose
>
> *Kinesis Data Firehose is a service that captures, transforms, and loads data into storage services such as Amazon S3 data lakes.*
>
> **In this architecture**, the data received by Kinesis Data Firehose is sent to an Amazon S3 bucket for analysis.

> ### Amazon S3 bucket
>
> *Amazon S3 is a file repository.*
>
> **In this architecture**, it stores all records that are added to the sales table.

> ### Amazon Athena
>
> *Athena is an interactive query service that makes it easy to analyze data in Amazon S3.*
>
> **In this architecture**, Athena is used to execute queries against all of the records in the Amazon S3 bucket in real time.

> ### Amazon QuickSight**
>
> *Amazon QuickSight is a fast, cloud-powered business intelligence service that makes it easy to deliver insights to everyone in your organization.*
>
> **In this architecture**, Amazon QuickSight uses the results of Athena queries to build reports and dashboards. Amazon Quicksight can refresh and load all new records in real time.
