# Kinesis Data Firehose

1. A data analytics company is setting up an innovative checkout-free grocery store. Their Solutions Architect developed a real-time monitoring application that uses smart sensors to collect the items that the customers are getting from the grocery’s refrigerators and shelves then automatically deduct it from their accounts. The company wants to analyze the items that are frequently being bought and store the results in S3 for durable storage to determine the purchase behavior of its customers.

What service must be used to easily capture, transform, and load streaming data into Amazon S3, Amazon Elasticsearch Service, and Splunk?

[ ] Amazon Kinesis Data Firehose

[ ] Amazon Kinesis

**Explanation**: Amazon Kinesis Data Firehose is the easiest way to load streaming data into data stores and analytics tools. It can capture, transform, and load streaming data into Amazon S3, Amazon Redshift, Amazon Elasticsearch Service, and Splunk, enabling near real-time analytics with existing business intelligence tools and dashboards you are already using today.

It is a fully managed service that automatically scales to match the throughput of your data and requires no ongoing administration. It can also batch, compress, and encrypt the data before loading it, minimizing the amount of storage used at the destination and increasing security.

In the diagram below, you gather the data from your smart refrigerators and use Kinesis Data firehouse to prepare and load the data. S3 will be used as a method of durably storing the data for analytics and the eventual ingestion of data for output using analytical tools.

![Fig. 1 Kinesis Data Firehose](../../../../img/applications/kinesis/kinesis-data-firehose/fig01.png)

You can use Amazon Kinesis Data Firehose in conjunction with Amazon Kinesis Data Streams if you need to implement real-time processing of streaming big data. Kinesis Data Streams provides an ordering of records, as well as the ability to read and/or replay records in the same order to multiple Amazon Kinesis Applications. The Amazon Kinesis Client Library (KCL) delivers all records for a given partition key to the same record processor, making it easier to build multiple applications reading from the same Amazon Kinesis data stream (for example, to perform counting, aggregation, and filtering).

Amazon Simple Queue Service (Amazon SQS) is different from Amazon Kinesis Data Firehose. SQS offers a reliable, highly scalable hosted queue for storing messages as they travel between computers. Amazon SQS lets you easily move data between distributed application components and helps you build applications in which messages are processed independently (with message-level ack/fail semantics), such as automated workflows. Amazon Kinesis Data Firehose is primarily used to load streaming data into data stores and analytics tools.

Hence, the correct answer is: **Amazon Kinesis Data Firehose**.

> **Amazon Kinesis** is incorrect because this is the streaming data platform of AWS and has four distinct services under it: Kinesis Data Firehose, Kinesis Data Streams, Kinesis Video Streams, and Amazon Kinesis Data Analytics. For the specific use case, just as asked in the scenario, use Kinesis Data Firehose.

<br />

2. A media company has an application that tracks user clicks on its websites and performs analytics to provide near-real-time recommendations. The application has a fleet of Amazon EC2 instances that receive data from the websites and send the data to an Amazon RDS DB instance for long-term retention. Another fleet of EC2 instances hosts the portion of the application that is continuously checking changes in the database and running SQL queries to provide recommendations. Management has requested a redesign to decouple the infrastructure. The solution must ensure that data analysts are writing SQL to analyze the new data only. No data can be lost during the deployment.

What should a solutions architect recommend to meet these requirements and to provide the FASTEST access to the user activity?

[ ] Use Amazon  Kinesis Data Streams to capture the data from the websites, Kinesis Data Firehose to persist data on Amazon S3, and Amazon Athena to query the data.

[x] Use Amazon Kinesis Data Streams to capture the data from the websites, Kinesis Data Analytics to query the data, and Kinesis Data Firehose to persist the data on Amazon S3.

[ ] Use Amazon Simple Notification...

**Explanation**: Use of Data Firehose to persists that data on Amazon S3 is applicable, but not the fastest. Data cannot be accessed from the Firehose until in S3 → thus not the fastest access. Increasing EC2 instance size will still require us to wait for the data to reach S3 before we can read that data. SNS does not durably store messages. SQS and SNS are messaging services.

<br />
