# Database Migration Service (DMS)

> **AWS Database Migration Service** (DMS) is a cloud service that makes it easy to migrate relational databases, data warehouses, NoSQL databases, and other types of data stores. You can use **AWS DMS** to migrate your data into the AWS Cloud, between on-premises instances (through an AWS Cloud setup), or between combinations of cloud and on-premises setups.

* Simply put, a way of migrating your databases and you can migrate your databases onto the cloud. You can migrate your databases off the cloud. You can migrate your databases between on premise setups as well.

![AWS Database Migration Service](https://d1.awsstatic.com/product-marketing/DMS/product-page-diagram_AWS-DMS_homogeneous-database-migrations-1.703dcf071fa58120bcd52b9194f9bf7d9a3d9110.png)

## How DMS works

> At its most basic level, **AWS DMS** is a server in the AWS Cloud that runs replication software. You create a source and target connection to tell AWS DMS where to extract from and load to. Then you schedule a task that runs on this server to move your data. AWS DMS creates the tables and associated primary keys if they don't exist on the target. You can **pre-create the target tables manually, or you can use AWS Schema Conversion Tool** (SCT) to create some or all of the target tables, indexes, views, triggers, etc.

## Types of DMS Migrations

> Supports **homogenous** migrations:
>
> * Oracle (DB on premise) → Oracle (DB on AWS)
>
> And supports **heterogeneous** migrations:
>
> * Microsoft SQL Server (DB on premise) → Amazon Aurora

## Sources and Targets

> **Sources**
>
> * On-premises and EC2 instances databases: Oracle, Microsoft SQL Server, MySQL, MariaDB, PostgreSQL, SAP, MongoDB, DB2
>
> * Azure **SQL** Database
>
> * Amazon **RDS** (including Aurora)
>
> * Amazon S3

> **Targets**
>
> * On-premises and EC2 instances databases: Oracle, Microsoft SQL Server, MySQL, MariaDB, PostgreSQL, SAP
>
> * RDS
>
> * Redshift
>
> * DynamoDB
>
> * S3
>
> * **Elasticsearch** service
>
> * **Kinesis** Data Streams
>
> * DocumentDB

## DMS in Action

### Solution Overview of DMS

> **Homogenous Migration**:
>
> On-Premises Database ←→ EC2 Instance Running DMS → RDS

## SCT in Action

### AWS Schema Conversion Tool

> **Heterogenous Migration**:
>
> On-Premises Database ←→ EC2 Instance Running DMS and Schema Conversion Tool (SCT) → RDS

* You will need AWS schema conversion tool if you're doing a heterogeneous migration.

> * You do not need SCT if you are migrating to identical databases.

## Learning summary

> * DMS allows you to **migrate databases** from one source to AWS.
>
> * You can do **homogenous** migrations (same DB engines) or **heterogenous** migrations.
>
> * The source can either be on-premises, or inside AWS itself or another cloud provider such as Azure.
>
> * If you do a heterogenous migration, you will need the **AWS Schema Conversion Tool** (SCT).
