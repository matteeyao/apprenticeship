# Service Introduction

Amazon Redshift is a fast, scalable data warehouse that makes it simple and cost-effective to analyze all your data across your data warehouse and data lake. Amazon Redshift delivers ten times faster performance than other data warehouses by using machine learning, massively parallel query execution, and columnar storage on high-performance disk.

Data warehouses are databases that are designed and used as repositories for analytical data. While data warehouses share many characteristics with relational databases, they serve different purposes.

A relational database, like Amazon Aurora, is used to store and maintain individual records. In contrast, data warehouses are used to store and maintain aggregate values generated from relational databases.

Think of it this way: when using a relational database, I can answer specific questions about individual inventory items. With a data warehouse, I can discover how inventory on-hand numbers have been different this year as compared to each of the previous five years.

Amazon Redshift is a fast, cloud-native, fully-managed, and secure data warehousing service that houses analytical data for use in complex queries, business intelligence reporting, and machine learning.

You can deploy a new data warehouse in minutes. Amazon Redshift automatically provisions the infrastructure and automates administrative tasks such as backups, replication, and fault tolerance.

With Concurrency Scaling, you can support virtually unlimited concurrent users and concurrent queries. When enabled, Amazon Redshift automatically adds additional cluster capacity when you need it to process an increase in concurrent read queries. When the demand decreases, the additional capacity is removed.

Amazon Redshift Spectrum is an optional feature that allows you to query all types of data stored in Amazon Simple Storage Service (Amazon S3) buckets without the need to first load the data into the Amazon Redshift database. This saves a tremendous amount of time and effort and can extend the size of your analytical reach well beyond the boundaries of your data warehouse’s local storage.

Remember I mentioned that Amazon Redshift is different from relational databases because it stores analytical data? One of the advantages of Amazon Redshift is that it uses a massively-parallel, columnar architecture. That means that data is indexed in a way that matches the way analytical queries are written.

So how does Amazon Redshift work? Well, internally Amazon Redshift is broken down into nodes. There is a single leader node and several compute nodes. Clients access Amazon Redshift via a SQL endpoint on the leader node. The client sends a query to the endpoint.

The leader node creates jobs based on the query logic and sends them in parallel to the compute nodes. The compute nodes contain the actual data the queries need. The compute nodes find the required data, perform operations, and return results to the leader node. The leader node then aggregates the results from all of the compute nodes and sends a report back to the client.

So now let’s discuss a couple ways you can use Amazon Redshift data warehouses.

You can use Amazon Redshift to build a unified data platform. Creating multiple copies of data is a huge waste of time and money; however, traditional data warehousing required that all data be loaded into the data warehouse. Amazon Redshift Spectrum can run queries across your data warehouse and Amazon S3 buckets simultaneously. You save time and money by leaving data where it is.

A popular mobile manufacturing company was struggling with sluggish query results. The company benefited from migrating their on-premises data warehouse to Amazon Redshift. After the migration, they were able to run queries twice as fast and were able to mine and analyze massive volumes of data at 50 percent of the cost.

Amazon Redshift pricing has been simplified to help you quickly determine your overall costs. You start by choosing the cluster node types that meet your needs. Each cluster node includes memory, storage, and I/O. The node type is billed per hour. There are four pricing types.

On-Demand pricing has no upfront costs. You simply pay an hourly rate based on the type and number of nodes in your cluster.

With Concurrency Scaling pricing, you simply pay a per-second on-demand rate for usage that exceeds the free daily credits. Each cluster earns up to one hour of free Concurrency Scaling credits per day, which is sufficient for most customers.

Reserved Instance pricing enables you to save up to 75 percent over On-Demand rates by committing to using Amazon Redshift for a 1- or 3-year term.

Amazon Redshift Spectrum pricing is applied when you begin using this feature. In addition to the cluster pricing, you pay for the number of bytes scanned on Amazon S3.

There is no charge for data transferred between Amazon Redshift and Amazon S3 within the same AWS Region for backup, restore, load, and unload operations. For all other data transfers, you are billed using the standard AWS data transfer rates.
