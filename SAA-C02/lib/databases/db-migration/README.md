# Database Migration

1. A company has an on-premises MySQL database that needs to be replicated in Amazon S3 as CSV files. The database will eventually be launched to an Amazon Aurora Serverless cluster and be integrated with an RDS Proxy to allow the web applications to pool and share database connections. Once data has been fully copied, the ongoing changes to the on-premises database should be continually streamed into the S3 bucket. The company wants a solution that can be implemented with little management overhead yet still highly secure.

Which ingestion pattern should a solutions architect take?

[ ] Set up a full load replication task using AWS Database Migration Service (AWS DMS). Launch an AWS DMS endpoint w/ SSL using the AWS Network Firewall service.

[ ] Use an AWS Snowball Edge cluster to migrate data to Amazon S3 and AWS DataSync to capture ongoing changes. Create your own custom AWS KMS envelope encryption key for the associated AWS Snowball Edge job.

[ ] Use AWS Schema Conversion Tool (AWS SCT) to convert MySQL data to CSV files. Set up the AWS Server Migration Service (AWS MGN) to capture ongoing changes from the on-premises MySQL database and send them to Amazon S3.

[ ] Create a full load and change data capture (CDC) replication task using AWS Database Migration Service (AWS DMS). Add a new Certificate Authority (CA) certificate and create an AWS DMS endpoint w/ SSL.

**Explanation**: **AWS Database Migration Service (AWS DMS)** is a cloud service that makes it easy to migrate relational databases, data warehouses, NoSQL databases, and other types of data stores. You can use AWS DMS to migrate your data into the AWS Cloud, between on-premises instances (through an AWS Cloud setup) or between combinations of cloud and on-premises setups. With AWS DMS, you can perform one-time migrations, and you can replicate ongoing changes to keep sources and targets in sync.

You can migrate data to Amazon S3 using AWS DMS from any of the supported database sources. When using Amazon S3 as a target in an AWS DMS task, both full load and change data capture (CDC) data is written to comma-separated value (.csv) format by default.

The comma-separated value (.csv) format is the default storage format for Amazon S3 target objects. For more compact storage and faster queries, you can instead use Apache Parquet (.parquet) as the storage format.

You can encrypt connections for source and target endpoints by using Secure Sockets Layer (SSL). To do so, you can use the AWS DMS Management Console or AWS DMS API to assign a certificate to an endpoint. You can also use the AWS DMS console to manage your certificates.

Not all databases use SSL in the same way. Amazon Aurora MySQL-Compatible Edition uses the server name, the endpoint of the primary instance in the cluster, as the endpoint for SSL. An Amazon Redshift endpoint already uses an SSL connection and does not require an SSL connection set up by AWS DMS.

Hence, the correct answer is: **Create a full load and change data capture (CDC) replication task using AWS Database Migration Service (AWS DMS). Add a new Certificate Authority (CA) certificate and create an AWS DMS endpoint with SSL**.

> The option that says: **Set up a full load replication task using AWS Database Migration Service (AWS DMS). Launch an AWS DMS endpoint with SSL using the AWS Network Firewall service** is incorrect because a full load replication task alone won't capture ongoing changes to the database. You still need to implement a change data capture (CDC) replication to copy the recent changes after the migration. Moreover, the AWS Network Firewall service is not capable of creating an AWS DMS endpoint with SSL. The Certificate Authority (CA) certificate can be directly uploaded to the AWS DMS console without the AWS Network Firewall at all.

> The option that says: **Use an AWS Snowball Edge cluster to migrate data to Amazon S3 and AWS DataSync to capture ongoing changes** is incorrect. While this is doable, it's more suited to the migration of large databases which require the use of two or more Snowball Edge appliances. Also, the usage of AWS DataSync for replicating ongoing changes to Amazon S3 requires extra steps that can be simplified with AWS DMS.

> The option that says: **Use AWS Schema Conversion Tool (AWS SCT) to convert MySQL data to CSV files. Set up the AWS Application Migration Service (AWS MGN) to capture ongoing changes from the on-premises MySQL database and send them to Amazon S3** is incorrect. AWS SCT is not used for data replication; it just eases up the conversion of source databases to a format compatible with the target database when migrating. In addition, using the AWS Application Migration Service (AWS MGN) for this scenario is inappropriate. This service is primarily used for lift-and-shift migrations of applications from physical infrastructure, VMware vSphere, Microsoft Hyper-V, Amazon Elastic Compute Cloud (AmazonEC2), Amazon Virtual Private Cloud (Amazon VPC), and other clouds to AWS.

<br />
