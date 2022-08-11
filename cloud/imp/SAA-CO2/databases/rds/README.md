# RDS

1. Which of the following statements are true of security in Amazon RDS? (Select three)

[ ] Amazon Cognito is used to control who can access the database instance.

[x] Amazon VPC is used to isolate your database from internet traffic.

[x] Connections to the database are secured using SSL.

[ ] Data is encrypted at rest using 256-bit RC6 algorithms.

[x] Security groups are used to control access to the database instance.

2. Which of the following can you use to create and modify an Amazon RDS instance?

[x] Amazon RDS API

[x] AWS Command Line Interface (CLI)

[x] AWS Management Console

[ ] Connect using the hostname and endpoint

[ ] Remote Desktop Protocol (RDP)

**Explanation**: You can create and modify database instances by using the AWS Management Console, AWS Command Line Interface (CLI), or the Amazon RDS Application Programming Interface (API). The AWS CLI and RDS API enable you to automate many tasks and integrations with your AWS environment.

3. Which of the following instance types are available to pay for Amazon RDS? (Select two)

[x] On-Demand

[ ] Provisioned

[x] Reserved

[ ] Spot

**Explanation**: You pay for the instance hosting the databases. There are two instance types to choose from: On-Demand and Reserved. On-Demand Instance pricing lets you pay for the compute capacity by the hour. This is great when your database runs intermittently or is a little unpredictable. Reserved Instance pricing is great when you have a good understanding of the resource consumption of your database. With this type of instance, you can secure a one- or three-year term and receive a significant discount over On-Demand pricing.

4. Which of the following security groups can control access to an Amazon RDS database instance? (Select three)

[ ] Amazon Cognito security groups

[x] Amazon EC2 security groups

[x] Amazon RDS security groups

[x] Amazon VPC security groups

[ ] Database security groups

**Explanation**: Amazon RDS and Amazon Cognito do not have their own security groups. Amazon RDS uses the other mentioned security groups.

5. A media company has an application that tracks user clicks on its websites and performs analytics to provide near-real-time recommendations. The application has a fleet of Amazon EC2 instances that receive data from the websites and send the data to an Amazon RDS DB instance for long-term retention. Another fleet of EC2 instances hosts the portion of the application that is continuously checking changes in the database and running SQL queries to provide recommendations. Management has requested a redesign to decouple the infrastructure. The solution must ensure that data analysts are writing SQL to analyze the new data only. No data can be lost during the deployment.

What should a solutions architect recommend to meet these requirements and to provide the FASTEST access to the user activity?

[ ] Use Amazon  Kinesis Data Streams to capture the data from the websites, Kinesis Data Firehose to persist data on Amazon S3, and Amazon Athena to query the data.

[x] Use Amazon Kinesis Data Streams to capture the data from the websites, Kinesis Data Analytics to query the data, and Kinesis Data Firehose to persist the data on Amazon S3.

[ ] Use Amazon Simple...

**Explanation**: Use of Data Firehose to persists that data on Amazon S3 is applicable, but not the fastest. Data cannot be accessed from the Firehose until in S3 → thus not the fastest access. Increasing EC2 instance size will still require us to wait for the data to reach S3 before we can read that data. SNS does not durably store messages. SQS and SNS are messaging services.
