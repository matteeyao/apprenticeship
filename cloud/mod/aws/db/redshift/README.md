# Redshift

> Amazon Redshift is a fast and powerful, fully managed, petabyte-scale data warehouse service in the cloud. Customers can start small for just $0.25 per hour w/ no commitments or upfront costs and scale to a petabyte or more for $1,000 per terabyte per year, less than a tenth of most other data warehousing solutions.

## OLTP versus OLAP

* Online Transaction Processing (OLTP): take a store, go in, and look up a particular order and you'd look up using a single order number; a single row of data

* Whereas Online Analytics Processing is where you're running queries against your database. To look up Net Profit for EMEA and for the Pacific for a digital radio product, for example:

> **OLAP transaction Example**:
> Net Profit for EMEA and Pacific for the Digital Radio Product.
> Pulls in large numbers of records
>
> Sum of Radios Sold in EMEA
> Sum of Radios Sold in Pacific
> Unit Cost of Radio in each region
> Sales price of each radio
> Sales price - unit cost.

* This will pull in numerous records. It's going to add up the sum of all radios sold in EMEA (Europe, Middle East and Africa). It's going to add up the sum of radios sold in the Pacific. It's going to grab the unit cost of the radios in each region. It's going to take the sales price of each region and then it's going to calculate the sales price minus the unit cost.

> Data Warehousing databases use different type of architecture both from a database and infrastructure layer.

> Amazon's Data Warehouse Solution is called **Redshift**

## Redshift configuration

> **Redshift can be configured as follows**
>
> * Single Node (160Gb)
>
> * Multi-Node
>
>   * Leader Node (manages client connections and receives queries).
>
>   * Compute Node (store data and perform queries and computations). Up to 128 Compute Nodes.

## Advanced Compression

> **Advanced Compression**: Columnar data stores can be compressed much more than row-based data stores b/c similar data is stored sequentially on disk. Amazon Redshift employs multiple compression techniques and can often achieve significant compression relative to traditional relational data stores. In addition, Amazon Redshift doesn't require indexes or materialized views, and so uses less space than traditional relational database systems. When loading data into an empty table, Amazon Redshift automatically samples your data and selects the most appropriate compression scheme.

## Massively Parallel Processing (MPP)

> **Massively Parallel Processing (MPP)**:
>
> Amazon Redshift automatically distributes data and query load across all nodes. Amazon Redshift makes it easy to add nodes to your data warehouse and enables you to maintain fast query performance as your data warehouse grows.

* Makes it possible to scale out w/ Redshift by adding more and more nodes behind your Leader Node.

## Backups

> * Enabled by default w/ a 1 day retention period.
>
> * Maximum retention period is 35 days.
>
> * Redshift always attempts to maintain at least three copies of your data (the original and replica on the compute nodes and a backup in Amazon S3).
>
> * Redshift can also asynchronously replicate your snapshots to S3 in another region for disaster recovery.

## Pricing

> **Redshift is priced as follows**:
>
> * Compute Node Hours (total number of hours you run across all your compute nodes for the billing period. You are billed for 1 unit per node per hour, so a 3-node data warehouse cluster running persistently for an entire month would incur 2,160 instance hours. You will not be charged for leader node hours, only compute nodes will incur charges).
>
> * Backup
>
> * Data transfer (only within a VPC, not outside it)

## Security

> **Security considerations**:
>
> * Encrypted in transit using SSL
>
> * Encrypted at rest using AES-256 encryption
>
> * By default RedShift takes care of key management.
>
>   * Manage your own keys through a Hardware Security Module (HSM).
>
>   * AWS Key Management Service

## Availability

> **Redshift Availability**:
>
> * Currently only available in 1 AZ
>
> * Can restore snapshots to new AZs in the event of an outage

## Learning summary

> * Redshift is used for business intelligence.
>
> * Available in only 1 AZ

> **Redshift Backups**
>
> * Enabled by default w/ a 1 day retention period.
>
> * Maximum retention period is 35 days.
>
> * Redshift always attempts to maintain at least three copies of your data (the original and replica on the compute nodes and a backup in Amazon S3).
>
> * Redshift can also asynchronously replicate your snapshots to S3 in another region for disaster recovery.
