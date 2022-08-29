# Service Introduction

Amazon Aurora is a MySQL- and PostgreSQL-compatible relational database built for the cloud, that combines the performance and availability of traditional enterprise databases with the simplicity and cost-effectiveness of open-source databases.

Relational databases thrive because they are built to collect large volumes of data efficiently and then deliver it in a usable form without the need for complex programming.

**Aurora**, which is a relational database, is built for the cloud, is compatible with **MySQL** and **PostgreSQL**, and combines the speed and reliability of high-end enterprise databases with the simplicity and cost-effectiveness of open-source databases.

When you build your first Aurora database, you start by opening the Amazon RDS Management Console. Next, you choose Aurora as the database engine, and then select the database instance type.

One innovation you may notice in Aurora is the log structured distributed storage layer. This method is significantly faster than other storage methods.

**Aurora** is structured in the same way other relational database engines are. It stores data in the form of tables, records, and fields.

**Aurora** automatically maintains six copies of your data across three Availability Zones and will automatically attempt to recover the database in a healthy Availability Zones with no data loss. You can create up to 15 read replicas that can serve read-only traffic and failover.

**Aurora** is fully managed by Amazon RDS. You no longer need to worry about database management tasks such as hardware provisioning, software patching, setup, configuration, or backups. Aurora automatically backs up your database to Amazon S3, enabling granular point-in-time recovery.

**Aurora** is built for high performance and scalability. You can get five times the throughput of standard MySQL and three times the throughput of standard PostgreSQL databases with Amazon Aurora. This performance is on par with commercial databases, at a tenth of the cost.

**Aurora** provides multiple levels of security for your database, including isolation, encryption at rest, and encryption in transit.

**Amazon Aurora Serverless** is an on-demand, auto-scaling configuration for the MySQL-compatible edition of Aurora.  It was to designed to enable databases to run in the cloud without managing individual database instances.

## Use cases

Hosting thousands of websites and managing web servers can be a huge challenge. Unpredictable workloads and erratic resource consumption can quickly bring systems to a halt. Pagely, a company that provides a massively-scalable hosting platform for WordPress sites, uses **Aurora Serverless** to overcome these problems. **Aurora Serverless** gave them the ability to lower customer costs through Aurora’s high-performance and scalability.

## Billing

With Aurora, you pay as you go and there are no upfront fees. There are three parts to Aurora billing.

First, you pay for the instance hosting the database. There are three ways that you can pay for your instance. On-Demand Instance pricing lets you pay for compute by the hour. Reserved Instance pricing lets you secure a one- or three-year contract in exchange for discounts over the on-demand rates. Serverless pricing is based on capacity, because there are no instances to manage.

Second, you pay for the storage and input output or I/O consumed by your database. Storage is billed per gigabyte per month and I/O is billed per million-requests. There is no additional charge for the built-in backups.  User initiated backups, however, are billed per GB per month. 

Third, there is a charge for data transferred out to the internet and other AWS Regions. You never pay for data transfers between AWS services in the same region.
