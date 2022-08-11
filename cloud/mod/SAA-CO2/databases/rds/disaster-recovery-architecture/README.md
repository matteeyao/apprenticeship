# Disaster recovery architecture

Amazon RDS for Oracle commonly runs mission-critical databases. If anything were to happen to these databases, it would be devastating. This architecture is one option for creating a disaster recovery solution for the databases.

![Fig. 1 Disaster recovery architecture](../../../../../img/SAA-CO2/databases/relational-database-service/disaster-recovery-architecture/diag01.png)

> ### Amazon Relational Database Service (Amazon RDS) for Oracle
>
> **In this architecture**, Amazon RDS for Oracle automatically creates snapshots, which are stored in an Amazon S3 bucket.

> ### AWS Lambda function 1
>
> *Lambda lets you run code, called functions, w/o provisioning or managing servers*.

> ### Amazon S3 bucket 1
>
> *Amazon S3 is a file repository*.
>
> **In this architecture**, it stores the original Amazon RDS snapshots.

> ### Amazon Simple Notification Service (Amazon SNS)
>
> *Amazon SNS is a highly available messaging service.*
>
> **In this architecture**, it creates notifications when a new snapshot is added to the Amazon S3 bucket. This notification can be used as a trigger for a Lambda function.

> ### AWS Lambda function 2
>
> **In this architecture**, the second Lambda function is triggered by a notification from Amazon SNS. The function copies the new snapshot from the Amazon S3 bucket 1 into an Amazon S3 bucket in a different Availability Zone. Bucket 2 is the repository for all of the disaster recovery snapshots.

> ### Amazon S3 bucket 2
>
> **In this architecture**, this Amazon S3 bucket is a repository for the disaster recovery copies of the Amazon RDS snapshots.
