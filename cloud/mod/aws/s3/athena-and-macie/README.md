# Athena vs Macie

## What is Athena?

Interactive query service which enables you analyze and query data located in S3 using standard SQL

* Serverless, nothing to provision, pay per query / per TB scanned

* No need to set up complex Extract/Transform/Load (ETL) processes → processes that relate to how you extract your data out of S3, how to put the data into something like RDS. You don't need to worry about any of that. Athena will allow you to turn your S3 into a database and you can run SQL queries against it.

* Works directly w/ data stored in S3

> **What data formats does Amazon Athena support?**
>
> Amazon Athena supports a wide variety of data formats like CSV, TSV, JSON, or Textfile and also supports open source columnar formats such as Apache ORC and Apache Parquet. Athena also supports compressed data in Snappy, Zlib, LZO, and GZIP formats. By compressing, partitioning, and using columnar formats you can improve performance and reduce your costs.

## Athena use cases

What can Athena be used for?

* Can be used to query log files stored in S3, e.g. Elastic Load Balancer (ELB) logs, S3 access logs, etc

* Generate business reports on data stored in S3

    * For example, run queries against your payroll data 

* Analyze AWS cost and usage reports

* Run queries on click-stream data stored in S3 as well

## What is Macie?

What is Personally Identifiable Information (PII)?

* Personal data used to establish an individual identity

* This data could be exploited by criminals, used in identity theft and financial fraud

* Home address, email address, SSN

* Passport number, driver's license number

* D.O.B., phone number, bank account, credit card number

What is Macie?

Security service which uses Machine Learning and NLP (Natural Language Processing) to discover, classify, and protect sensitive data stored in S3

* Uses AI to recognize if your S3 objects contain sensitive data such as PII

* Dashboards, reporting and alerts

* Works directly w/ data stored in S3

* Can also analyze CloudTrail logs

* Great for PCI-DSS (Credit card payments used on your app) and preventing ID theft

While **Athena** allows you to query your data on S3 based off SQL queries, **Macie** also queries data on S3, but it's using machine learning and natural language processing to discover PII information.

## Learning summary

Remember what Athena is and what it allows you to do:

* Athena is an interactive query service

* It allows you to query data located in S3 using standard SQL

* Serverless

* Commonly used to analyze log data stored in S3

Remember what Macie is and what it allows you to do:

* Macie uses AI to analyze data in S3 and helps identify PII

* Can also be used to analyze CloudTrail logs for suspicious API activity

* Includes Dashboards, Reports and Alerting

* Great for PCI-DSS compliance and preventing ID theft

**Athena** is used for runningSQL queries. **Macie** is used as a security service to look for PII.
