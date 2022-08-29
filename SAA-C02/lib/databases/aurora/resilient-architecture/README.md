# Aurora Resilient Architecture

1. A top investment bank is in the process of building a new Forex trading platform. To ensure high availability and scalability, you designed the trading platform to use an Elastic Load Balancer in front of an Auto Scaling group of On-Demand EC2 instances across multiple Availability Zones. For its database tier, you chose to use a single Amazon Aurora instance to take advantage of its distributed, fault-tolerant, and self-healing storage system.

In the event of system failure on the primary database instance, what happens to Amazon Aurora during the failover?

[ ] Aurora will first attempt to create a new DB instance in a different Availability Zone of the original instance. If unable to do so, AUrora will attempt to create a new DB instance in the original Availability Zone in which the instance was first launched.

[ ] Aurora will attempt to create a new DB instance in the same Availability Zone as the original instance and is done on a best-effort basis.

[ ] Amazon Aurora flips the canonical name record (CNAME) for your DB Instance to point at the healthy replica, which in turn is promoted to become the new primary.

[ ] Amazon Aurora flips the A record of your DB instance to point at the healthy replica, which in turn is promoted to become the new primary.

**Explanation**: Failover is automatically handled by Amazon Aurora so that your applications can resume database operations as quickly as possible without manual administrative intervention.

If you have an Amazon Aurora Replica in the same or a different Availability Zone, when failing over, Amazon Aurora flips the canonical name record (CNAME) for your DB Instance to point at the healthy replica, which in turn is promoted to become the new primary. Start-to-finish, failover typically completes within 30 seconds.

If you are running Aurora Serverless and the DB instance or AZ become unavailable, Aurora will automatically recreate the DB instance in a different AZ.

If you do not have an Amazon Aurora Replica (i.e. single instance) and are not running Aurora Serverless, Aurora will attempt to create a new DB Instance in the same Availability Zone as the original instance. This replacement of the original instance is done on a best-effort basis and may not succeed, for example, if there is an issue that is broadly affecting the Availability Zone.

Hence, the correct answer is the option that says: **Aurora will attempt to create a new DB Instance in the same Availability Zone as the original instance and is done on a best-effort basis.**

> The options that say: **Amazon Aurora flips the canonical name record (CNAME) for your DB Instance to point at the healthy replica, which in turn is promoted to become the new primary** and **Amazon Aurora flips the A record of your DB Instance to point at the healthy replica, which in turn is promoted to become the new primary** are incorrect because this will only happen if you are using an Amazon Aurora Replica. In addition, Amazon Aurora flips the canonical name record (CNAME) and not the A record (IP address) of the instance.

> The option that says: **Aurora will first attempt to create a new DB Instance in a different Availability Zone of the original instance. If unable to do so, Aurora will attempt to create a new DB Instance in the original Availability Zone in which the instance was first launched** is incorrect because Aurora will first attempt to create a new DB Instance in the same Availability Zone as the original instance. If unable to do so, Aurora will attempt to create a new DB Instance in a different Availability Zone and not the other way around.

<br />
