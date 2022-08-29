# Redshift

1. Amazon Redshift is a purpose-built data warehouse. Which of the following statements are true of data warehouses? (Select two)

[x] They are designed as analytical repositories.

[ ] They store and maintain highly detailed records.

[x] They can store aggregate values from transactional databases.

[ ] They are not compatible w/ semistructured or unstructured data.

[ ] They can only import data from relational databases.

**Explanation**: Amazon Redshift databases are designed as analytical repositories. They can store aggregate values from transactional databases and dozens of other source locations.

2. Which of the following statements are true of security in Amazon Redshift? (Select three)

[ ] You can configure Amazon Redshift nodes w/ their own security settings.

[x] Connections to the database are secured using HTTPS.

[ ] You cannot run Amazon Redshift inside of a VPC; you use a VPN.

[x] You can encrypt data using keys you manage through AWS Key Management Service (AWS KMS).

[x] All authentication and authorization is done using AWS Identity and Access Management (IAM).

3. Which of the following are features you would be billed for with Amazon Redshift? (Select two)

[ ] Parallel Processing

[x] Concurrency Scaling

[ ] Columnar Indexing

[x] Amazon Redshift Spectrum

[ ] Burstable performance

**Explanation**: Concurrency Scaling and Amazon Redshift Spectrum are optional features of Amazon Redshift that you will be billed for if enabled.

4. Which of the following statements are true of Amazon Redshift? (Select three)

[ ] There are three types of nodes: leader, compute, and slice.

[x] Slices have assigned memory and disk space.

[x] Clusters are comprised of nodes.

[x] You connect to the cluster using a SQL endpoint.

[ ] Amazon Redshift communicates using OLE DB drivers.

**Explanation**: Amazon Redshift clusters are comprised of nodes. Compute nodes divide work among slices. Each slice is assigned a portion of the node’s memory and drive space. When you connect to an Amazon Redshift cluster, you use the SQL endpoint.

5. Which of the following are benefits of using Amazon Redshift? (Select three)

[x] Queries execute using parallel processing.

[x] Data is indexed using columnar indexing.

[ ] Data is organized into documents.

[ ] Data is stored in key-value pairs.

[x] You can query Amazon S3 w/o moving data into Amazon Redshift.

**Explanation**: Amazon Redshift can achieve its massive speed improvements by implementing columnar indexing of the data and parallel processing. With Amazon Redshift Spectrum, you can query data within an Amazon S3 bucket or data lake without having to move the data into the Amazon Redshift cluster.
