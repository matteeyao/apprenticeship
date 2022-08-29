# RDS High Availability

1. An online events registration system is hosted in AWS and uses ECS to host its front-end tier and an RDS configured with Multi-AZ for its database tier. What are the events that will make Amazon RDS automatically perform a failover to the standby replica? (Select TWO.)

[ ] Storage failure on secondary DB instance

[ ] In the event of Read Replica failure

[ ] Compute unit failure on secondary DB instance

[x] Storage failure on primary

[x] Loss of availability in the primary Availability Zone

**Explanation**: **Amazon RDS** provides high availability and failover support for DB instances using Multi-AZ deployments. Amazon RDS uses several different technologies to provide failover support. Multi-AZ deployments for Oracle, PostgreSQL, MySQL, and MariaDB DB instances use Amazon's failover technology. SQL Server DB instances use SQL Server Database Mirroring (DBM).

In a Multi-AZ deployment, Amazon RDS automatically provisions and maintains a synchronous standby replica in a different Availability Zone. The primary DB instance is synchronously replicated across Availability Zones to a standby replica to provide data redundancy, eliminate I/O freezes, and minimize latency spikes during system backups. Running a DB instance with high availability can enhance availability during planned system maintenance and help protect your databases against DB instance failure and Availability Zone disruption.

Amazon RDS detects and automatically recovers from the most common failure scenarios for Multi-AZ deployments so that you can resume database operations as quickly as possible without administrative intervention.

The high-availability feature is not a scaling solution for read-only scenarios; you cannot use a standby replica to serve read traffic. To service read-only traffic, you should use a Read Replica.

Amazon RDS automatically performs a failover in the event of any of the following:

  1. Loss of availability in primary Availability Zone.

  2. Loss of network connectivity to primary.

  3. Compute unit failure on primary.

  4. Storage failure on primary.

Hence, the correct answers are: **Loss of availability in primary Availability Zone** and **Storage failure on primary**

The other options are incorrect because all these scenarios do not affect the primary database. Automatic failover only occurs if the primary database is the one that is affected.

<br />

2. An accounting application uses an RDS database configured with Multi-AZ deployments to improve availability. What would happen to RDS if the primary database instance fails?

[ ] The primary database instance will reboot.

[ ] A new database instance is created in the standby Availability Zone.

[x] The canonical name record (CNAME) is switched from the primary to the standby instance.

[ ] The IP address of the primary DB instance is switched to the standby DB instance.

**Explanation**: In **Amazon RDS**, failover is automatically handled so that you can resume database operations as quickly as possible without administrative intervention in the event that your primary database instance goes down. When failing over, Amazon RDS simply flips the canonical name record (CNAME) for your DB instance to point at the standby, which is in turn promoted to become the new primary.

> The option that says: **The IP address of the primary DB instance is switched to the standby DB instance** is incorrect since IP addresses are per subnet, and subnets cannot span multiple AZs.

> The option that says: **The primary database instance will reboot** is incorrect since in the event of a failure, there is no database to reboot with.

> The option that says: **A new database instance is created in the standby Availability Zone** is incorrect since with multi-AZ enabled, you already have a standby database in another AZ.

<br />

3. A cryptocurrency company wants to go global with its international money transfer app. Your project is to make sure that the database of the app is highly available in multiple regions.

What are the benefits of adding Multi-AZ deployments in Amazon RDS? (Select TWO.)

[ ] Creates a primary DB instance and asynchronously replicates the data to a standby instance in a different Availability Zone (AZ) in a different region.

[ ] Significantly increases the database performance.

[ ] Increased database availability in the case of system upgrades like OS patching or DB instance scaling.

[ ] Provides enhanced database durability in the event of a DB instance component failure or an Availability Zone outage.

[ ] Provides SQL optimization.

**Explanation**: **Amazon RDS Multi-AZ deployments** provide enhanced availability and durability for Database (DB) Instances, making them a natural fit for production database workloads. When you provision a Multi-AZ DB Instance, Amazon RDS automatically creates a primary DB Instance and synchronously replicates the data to a standby instance in a different Availability Zone (AZ). Each AZ runs on its own physically distinct, independent infrastructure, and is engineered to be highly reliable.

In case of an infrastructure failure, Amazon RDS performs an automatic failover to the standby (or to a read replica in the case of Amazon Aurora), so that you can resume database operations as soon as the failover is complete. Since the endpoint for your DB Instance remains the same after a failover, your application can resume database operation without the need for manual administrative intervention.

![Fig. 1 RDS Multi-Zone Diagram](../../../../img/databases/rds/resilient-architecture/fig01.png)

The chief benefits of running your DB instance as a Multi-AZ deployment are enhanced database durability and availability. The increased availability and fault tolerance offered by Multi-AZ deployments make them a natural fit for production environments.

Hence, the correct answers are the following options:

* **Increased database availability in the case of system upgrades like OS patching or DB Instance scaling.**

* **Provides enhanced database durability in the event of a DB instance component failure or an Availability Zone outage.**

> The option that says: **Creates a primary DB Instance and synchronously replicates the data to a standby instance in a different Availability Zone (AZ) in a different region** is almost correct. RDS synchronously replicates the data to a standby instance in a different Availability Zone (AZ) that is in the same region and not in a different one.

> The options that say: **Significantly increases the database performance** and **Provides SQL optimization** are incorrect as it does not affect the performance nor provide SQL optimization.

<br />
