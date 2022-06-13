# Databases

## What is a relational database?

> Relational databases are what most of us are all used to. They have been around since the 70's. Think of a traditional spreadsheet.
>
> * Database
>
> * Tables
>
> * Rows
>
> * Fields (Columns)

> **Remember the following points**:
>
> * RDS runs on virtual machines
>
> * You cannot log in to these operating systems however.
>
> * Patching of the RDS Operating System and DB is Amazon's responsibility.
>
> * RDS is NOT Serverless
>
> * Aurora Serverless IS Serverless

> **There are two different types of Backups for RDS**:
>
> * Automated Backups
>
> * Database Snapshots

## Relational databases on AWS

> * SQL Server
>
> * Oracle
>
> * MySQL Server
>
> * PostgreSQL
>
> * Aurora
>
> * MariaDB

## Multi-AZ versus Read Replicas

> RDS has two key features:
>
> * Multi-AZ → for disaster recovery
>
> * Read Replicas → for performance

* To spread access to your databases across multiple availability zones, you want Multi-AZ

* If you are trying to boost the database's performance, go for Read Replicas

* Fail-over is integrated into *Multi-AZ*

* W/ *Read Replicas*, every time you do a write to that database, that write is replicated to another database

    * If you were to lose your primary database, there's no automatic fail-over from one db to the other. You'd have to go in and create a new URL, and then you'd have to update your EC2 instances to point to the *Read Replica*.

    * But, if you've got multiple EC2 instances, you can easily scale by replicating your db; you can point half of your EC2 instances to read from the *Read Replica* and then the other half to read from the primary database

    * You can have upt to 5 copies of *Read Replicas*

### Read Replicas

> * Can be Multi-AZ.
>
> * Used to increase performance.

* Primarily used to increase performance, which is a common exam scenario.

* Say we've got a struggling database, it's got a very heavy read traffic, maybe it's a WordPress blog. How would we increase the performance of the database? The answer would be to add Read Replicas and to point your EC2 instances to those Read Replicas.

> * Must have backups turned on.
>
> * Can be in different regions.

* Remember, to enable Read Replicas, we must have backups turned on and Read Replicas can be in different regions as well as the same region

> * Can be MSSQL, MySQL, PostgreSQL, MariaDB, Oracle, Aurora.
>
> * Can be promoted to master, this will break the Read Replica

### Multi-AZ

> * Used for Disaster Recovery (DR)
>
> * You can force a failover from one AZ to another by rebooting the RDS instance.

## Encryption

> Encryption at rest is supported for MySQL, Oracle, SQL Server, PostgreSQL, MariaDB & Aurora. Encryption is done using the AWS Key Management Service (KMS) service. Once your RDS instance is encrypted, the data stored at rest in the underlying storage is encrypted, as are its automated backups, read replicas, and snapshots.

## Non-relational databases

> * Collection      = Table
>
> * Document        = Row
>
> * Key Value Pairs = Fields

> 1. **DynamoDB**: Nonrelational database (NoSQL)
>
> 2. **DynamoDB provides managed database instances**
>
>   * No structured schema
>
>   * Tables, rows, key values, columns
>
>   * JSON
>
> 3. DynamoDB has its own in-memory cache: **DAX**
>
> 4. **DAX has two caches**:
>
>   * Item cache ▶︎ populated w/ individual results from Get Items and BatchGetItems. It also has a 5-minute default TTL.
>
>   * Query cache ▶︎ query and scan operations and caching the results on the parameters specified.

There is a DAX client that is usually stored on the application itself. So when you read items from DynamoDB, those items are stored inside DAX as well as returned. And then if that item is queried for another read, it is returned from DAX w/o actually calling DynamoDB itself. DAX refers to this as a cache hit. If a query is not stored in DAX and has to be queried from DynamoDB, then DAX will call this a cache miss, but that missed query is then stored in DAX and also sent to the application. DAX is designed for high read, heavy workloads and DAX has 2 caches (mentioned above).

## What is data warehousing?

> Used for business intelligence. Tools like Cognos, Jaspersoft, SQL Server Reporting Services, Oracle Hyperion, SAP Business Warehouse.

> Used to pull in very large and complex data sets. Usually used by management to do queries on data (such as current performance vs targets etc)

## OLTP versus OLAP

> Online Transaction Processing (OLTP) differs from OLAP Online Analytics Processing (OLAP) in terms of the types of queries you will run.

> **OLTP Example**:
>
> Order number 2120121
>
> Pulls up a row of data such as
>
> Name, Date, Address to Deliver to, Delivery Status, etc.

> **OLAP transaction Example**:
>
> * Net Profit for EMEA and Pacific for the Digital Radio Product.
>
> * Pulls in large numbers of records
>
>   * Sum of Radios Sold in EMEA
>
>   * Sum of Radios Sold in Pacific
>
>   * Unit Cost of Radio in each region
>
>   * Sales price of each radio
>
>   * Sales price - unit cost.

> Data Warehousing databases use different type of architecture both from a database perspective and infrastructure layer.

> Amazon's Data Warehouse Solution is called **Redshift**

## What is ElastiCache?

> ElastiCache is a web service that makes it easy to deploy, operate, and scale an in-memory cache in the cloud. The service improves the performance of web applications by allowing you to retrieve information from fast, managed, in-memory caches, instead of relying entirely on slower disk-based databases.

* A way of caching your most common web queries

> ElastiCache supports two open-source in-memory caching engines:
>
> * Memcached
>
> * Redis

## Learning summary

> **RDS → Online Transaction Processing (OLTP)**
>
> * SQL Server
>
> * Oracle
>
> * MySQL Server
>
> * PostgreSQL
>
> * Aurora
>
> * MariaDB

> **DynamoDB (No SQL)**
>
> **Redshift → Online Analytics Processing (OLAP)**

> **Elasticache**
>
> * Memcached
>
> * Redis

* Memcached is used if we want something simple, whereas Redis is used for more advanced data types, if more availability zones are needed, or if we need to be able to do backups.

> **Redshift for Business Intelligence or Data Warehousing**
>
> **Elasticache to speed up performance of existing databases (frequent identical queries).**
