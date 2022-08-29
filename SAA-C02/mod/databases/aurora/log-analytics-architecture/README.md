# Log analytics architecture

Knowing how your databases are being used and their health is an important part of maintaining well-running systems. Aurora regularly generates logs on activities of users and the database. You can analyze these logs to ensure that users are getting the responses they require and the database is running optimally. This architecture is one option for creating a log analytics system.

![Fig. 1 Log analytics architecture](../../../../../img/SAA-CO2/databases/aurora/log-analytics-architecture/diag01.png)

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

> ### Amazon Elasticsearch Service (Amazon ES)
>
> *Amazon ES is a fully-managed service that enables you to securely ingest data from any source and search, analyze, and visualize the data in real time.*
>
> **In this architecture*, Amazon ES gathers data from the CloudWatch logs, catalogs it, and makes it available for analysis and visualization.

> ### Amazon Quicksight
>
> *Amazon QuickSight is a fast, cloud-powered business intelligence service that makes it easy to deliver insights to everyone in your organization.*
>
> **In this architecture**, Amazon QuickSight can visualize data contained within Amazon ES. That data reflects usage of the Aurora database.

> ### Amazon S3
>
> *An Amazon S3 bucket is a storage location for many types of data.*
>
> **In this architecture**, you can store the log files from CloudWatch indefinitely for future analysis and to meet retention requirements.

> ### Amazon Athena
>
> *Amazon Athena is an interactive query service that makes it easy to analyze data in Amazon S3 using standard SQL.*
>
> **In this architecture**, you can use Athena to query the archived log files.
