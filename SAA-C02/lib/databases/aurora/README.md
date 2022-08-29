# Aurora

1. Which of the following statements are true of Aurora databases? (Select THREE.)

[x] Aurora can have up to 15 read replicas.

[ ] Aurora charges for data transferred in from and out to the internet.

[x] Aurora is managed by Amazon RDS.

[ ] Aurora requires primary keys to create uniqueness.

[x] Aurora Serverless manages database instances for you.

2. Which of the following are instance classes or features that Aurora supports? (Select two)

[ ] Accelerated computing

[x] Burstable performance

[ ] Cluster networking

[ ] Fixed performance

[x] Memory-optimized

**Explanation**: As with Amazon RDS, the basic building block of Aurora is the database instance class. This determines the amount of memory, CPU, and I/O capabilities available to the database engine. Aurora supports two types of instances: memory-optimized and burstable performance. Memory-optimized instances are suitable for most Aurora databases. Burstable performance instances are best when your database may experience short-lived bursts of high activity.

3. Which of the following pricing methods are available to pay for Aurora? (Select three)

[x] On-Demand

[ ] Provisioned

[x] Reserved

[x] Serverless

[ ] Spot

**Explanation**: So let’s talk a little bit about billing. With Aurora, you pay as you go, and there are no upfront fees. There are three parts to Aurora billing that you should be aware of.

First, you pay for the instance hosting the database. There are three ways that you can pay for your instance. On-Demand Instance pricing lets you pay for compute by the hour. Reserved Instance pricing lets you secure a one- or three-year contract in exchange for discounts over the On-Demand rates. Serverless pricing is based on capacity, because there are no instances to manage.

Second, you pay for the storage and input output, or I/O, consumed by your database. Storage is billed per gigabyte per month, and the I/O is billed per million requests. There is no additional charge for the built-in backups. User-initiated backups, however, are billed per GB per month.

Third, there is a charge for data transferred out to the internet and other AWS Regions. You never pay for data transfers between AWS services in the same Region.

3. Which of the following statements are true of security in Aurora? (Select three)

[x] Aurora requires both authentication and permissions for users to access tables.

[x] IAM policies can be used to assign permissions to users.

[x] Security groups are used to control access to the database instance.

[ ] Data is encrypted at rest using 256-bit RC6 algorithms.

[ ] Amazon Cognito is used to control who can access the database instance.

**Explanation**: There are four important considerations for security in Aurora databases.

First, its proximity to the internet. The best practice is to restrict access to your database by placing it in an Amazon Virtual Private Cloud, or VPC. There may be instances where you must accept requests from the internet. In this case, you should create an internet gateway to funnel that traffic.

Second, controlling access to the database instance. Security groups control access to a database instance. Amazon Aurora can use three types of security groups: database, VPC, and EC2.

Aurora utilizes AWS Identity and Access Management, or IAM, to create and manage credentials. The same users and roles that you have in IAM can also be used with Aurora. Aurora requires both authentication and permissions to access tables and data. IAM policies assign permissions that determine who can manage database resources.

Third, securing communications to and from the database instance. This is known as data in transit. This is accomplished by using HTTPS connections. These connections are encrypted using SSL.

Finally, protecting data in the database. Aurora uses the industry standard AES-256 bit encryption algorithm to encrypt the data and database snapshots while at rest.

4. Which of the following are true of security in Aurora? (Select three)

[x] IAM policies can be used to assign permission to users.

[x] Aurora requires both authentication and permission for users to access tables.

[x] Security groups are used to control access to the database instance.

[ ] Amazon Cognito is used to control who can access the database instance.

[ ] Data is encrypted at rest using 256-bit RC6 algorithms.

5. Which of the following database engines is Aurora compatible with? (Select two)

[x] MySQL

[ ] SQL Server

[ ] Oracle

[x] PostgreSQL

[ ] Spark SQL

<br />

6. An online shopping platform is hosted on an Auto Scaling group of Spot EC2 instances and uses Amazon Aurora PostgreSQL as its database. There is a requirement to optimize your database workloads in your cluster where you have to direct the write operations of the production traffic to your high-capacity instances and point the reporting queries sent by your internal staff to the low-capacity instances.

Which is the most suitable configuration for your application as well as your Aurora database cluster to achieve this requirement?

[ ] Configure your application to use the reader endpoint for both production traffic and reporting queries, which will enable your Aurora database to automatically perform load-balancing among all the Aurora Replicas.

[ ] In your application, use the instance endpoint of your Aurora database to handle the incoming production traffic and use the cluster endpoint to handle reporting queries.

[ ] Do nothing since by default, Aurora will automatically direct the production traffic to your high-capacity instances and the reporting queries to your low-capacity instances.

[x] Create a custom endpoint in Aurora based on the specified criteria for the production traffic and another custom endpoint to handle the reporting queries.

**Explanation**: **Amazon Aurora** typically involves a cluster of DB instances instead of a single instance. Each connection is handled by a specific DB instance. When you connect to an Aurora cluster, the host name and port that you specify point to an intermediate handler called an *endpoint*. Aurora uses the endpoint mechanism to abstract these connections. Thus, you don't have to hardcode all the hostnames or write your own logic for load-balancing and rerouting connections when some DB instances aren't available.

For certain Aurora tasks, different instances or groups of instances perform different roles. For example, the primary instance handles all data definition language (DDL) and data manipulation language (DML) statements. Up to 15 Aurora Replicas handle read-only query traffic.

Using endpoints, you can map each connection to the appropriate instance or group of instances based on your use case. For example, to perform DDL statements you can connect to whichever instance is the primary instance. To perform queries, you can connect to the reader endpoint, with Aurora automatically performing load-balancing among all the Aurora Replicas. For clusters with DB instances of different capacities or configurations, you can connect to custom endpoints associated with different subsets of DB instances. For diagnosis or tuning, you can connect to a specific instance endpoint to examine details about a specific DB instance.

The custom endpoint provides load-balanced database connections based on criteria other than the read-only or read-write capability of the DB instances. For example, you might define a custom endpoint to connect to instances that use a particular AWS instance class or a particular DB parameter group. Then you might tell particular groups of users about this custom endpoint. For example, you might direct internal users to low-capacity instances for report generation or ad hoc (one-time) querying, and direct production traffic to high-capacity instances. Hence, **creating a custom endpoint in Aurora based on the specified criteria for the production traffic and another custom endpoint to handle the reporting queries** is the correct answer.

> **Configuring your application to use the reader endpoint for both production traffic and reporting queries, which will enable your Aurora database to automatically perform load-balancing among all the Aurora Replicas** is incorrect. Although it is true that a reader endpoint enables your Aurora database to automatically perform load-balancing among all the Aurora Replicas, it is quite limited to doing read operations only. You still need to use a custom endpoint to load-balance the database connections based on the specified criteria.

> The option that says: **In your application, use the instance endpoint of your Aurora database to handle the incoming production traffic and use the cluster endpoint to handle reporting queries** is incorrect because a cluster endpoint (also known as a writer endpoint) for an Aurora DB cluster simply connects to the current primary DB instance for that DB cluster. This endpoint can perform write operations in the database such as DDL statements, which is perfect for handling production traffic but not suitable for handling queries for reporting since there will be no write database operations that will be sent. Moreover, the endpoint does not point to lower-capacity or high-capacity instances as per the requirement. A better solution for this is to use a custom endpoint.

> The option that says: **Do nothing since by default, Aurora will automatically direct the production traffic to your high-capacity instances and the reporting queries to your low-capacity instances** is incorrect because Aurora does not do this by default. You have to create custom endpoints in order to accomplish this requirement.

<br />

7. A company has recently adopted a hybrid cloud architecture and is planning to migrate a database hosted on-premises to AWS. The database currently has over 50 TB of consumer data, handles highly transactional (OLTP) workloads, and is expected to grow. The Solutions Architect should ensure that the database is ACID-compliant and can handle complex queries of the application.

Which type of database service should the Architect use?

[ ] Amazon RDS

[ ] Amazon Aurora

**Explanation**: Amazon Aurora (Aurora) is a fully managed relational database engine that's compatible with MySQL and PostgreSQL. You already know how MySQL and PostgreSQL combine the speed and reliability of high-end commercial databases with the simplicity and cost-effectiveness of open-source databases. The code, tools, and applications you use today with your existing MySQL and PostgreSQL databases can be used with Aurora. With some workloads, Aurora can deliver up to five times the throughput of MySQL and up to three times the throughput of PostgreSQL without requiring changes to most of your existing applications.

Aurora includes a high-performance storage subsystem. Its MySQL- and PostgreSQL-compatible database engines are customized to take advantage of that fast distributed storage. The underlying storage grows automatically as needed, up to 64 tebibytes (TiB). Aurora also automates and standardizes database clustering and replication, which are typically among the most challenging aspects of database configuration and administration.

For Amazon RDS MariaDB DB instances, the maximum provisioned storage limit constrains the size of a table to a maximum size of 64 TB when using InnoDB file-per-table tablespaces. This limit also constrains the system tablespace to a maximum size of 16 TB. InnoDB file-per-table tablespaces (with tables each in their own tablespace) is set by default for Amazon RDS MariaDB DB instances.

Hence, the correct answer is **Amazon Aurora**.

> **Amazon RDS** is incorrect. Although this service can host an ACID-compliant relational database that can handle complex queries and transactional (OLTP) workloads, it is still not scalable to handle the growth of the database. Amazon Aurora is the better choice as its underlying storage can grow automatically as needed.

<br />
