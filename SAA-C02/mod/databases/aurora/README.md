# Aurora

* Amazon's own proprietary database

* Deliberately architected to compete w/ Oracle and Microsoft's high-end databases while still having the flexibility and open source parameters of things like MySQL and PostgreSQL.

> Amazon Aurora is a MySQL and PostgreSQL-compatible relational database engine that combines the speed and availability of high-end commercial databases w/ the simplicity and cost-effectiveness of open source databases.

> Amazon Aurora provides up to five times better performance than MySQL and three times better than PostgreSQL databases at a much lower price point, whilst delivering similar performance and availability.

## Aurora essentials

> 1. Start with 10GB, Scales in 10GB increments to 64TB (Storage Autoscaling)
>
> 2. Compute resources can scale up to 32vCPUs and 244GB of Memory.
>
> 3. 2 copies of your data is contained in each availability zone, w/ minimum of 3 availability zones. 6 copies of your data.

## Scaling Aurora

> * Aurora is designed to transparently handle the loss of up to two copies of data w/o affecting database write availability and up to three copies w/o affecting read availability.

* So you can lose a couple of availability zones and still not have any issues on performance.

> * Aurora storage is also self-healing. Data blocks and disks are continuously scanned for errors and repaired automatically.

> **Three Types of Aurora Replicas are available**:
>
> * Aurora Replicas (currently 15)
>
> * MySQL Read Replicas (currently 5)
>
> * PostgreSQL (currently 1)

* These are Read Replicas on top of your normal Aurora production database.

## Backups w/ Aurora

> * Automated backups are always enabled on Amazon Aurora DB Instances. Backups do not impact database performance.
>
> * You can also take snapshots w/ Aurora. This also does not impact performance.
>
> * You can share Aurora Snapshots w/ other AWS accounts.

## Aurora Serverless

> Amazon Aurora Serverless is an on-demand, autoscaling configuration for the MySQL-compatible and PostgreSQL-compatible editions of Amazon Aurora. An Aurora Serverless DB cluster automatically starts up, shuts down, and scales capacity up or down based on your application's needs.

> Aurora Serverless provides a relatively simple, cost-effective option for infrequent, intermittent, or unpredictable workloads.

## Learning summary

> * 2 copies of your data are contained in each availability zone, w/ minimum of 3 availability zones. 6 copies of your data.
>
> * You can share Aurora Snapshots w/ other AWS accounts.
>
> * 3 types of replicas available. Aurora Replicas, MySQL replicas & PostgresQL replicas. Automated failover is only available w/ Aurora Replicas.
>
> * Aurora has automated backups turned on by default. You can also take snapshots w/ Aurora. You can share these snapshots w/ other AWS accounts.
>
> * Use Aurora Serverless if you want a simple, cost-effective option for infrequent, intermittent, or unpredictable workloads.
