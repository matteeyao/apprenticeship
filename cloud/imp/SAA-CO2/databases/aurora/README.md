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
