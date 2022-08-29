# AWS Glue

1. A data analytics startup is collecting clickstream data and stores them in an S3 bucket. You need to launch an AWS Lambda function to trigger the ETL jobs to run as soon as new data becomes available in Amazon S3.

Which of the following services can you use as an extract, transform, and load (ETL) service in this scenario?

[ ] AWS Step Functions

[ ] AWS Glue

[ ] S3 Select

[ ] Redshift Spectrum

**Explanation**: **AWS Glue** is a fully managed extract, transform, and load (ETL) service that makes it easy for customers to prepare and load their data for analytics. You can create and run an ETL job with a few clicks in the AWS Management Console. You simply point AWS Glue to your data stored on AWS, and AWS Glue discovers your data and stores the associated metadata (e.g. table definition and schema) in the AWS Glue Data Catalog. Once cataloged, your data is immediately searchable, queryable, and available for ETL. AWS Glue generates the code to execute your data transformations and data loading processes.

![Fig. 1 AWS Glue Overview](../../../img/databases/glue/fig01.png)

<br />
