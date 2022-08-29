# Database Migration

Managing on-premises and cloud-based databases to run at scale, with high availability and reliability is a difficult, time-consuming, and expensive undertaking. Migrating your databases and data warehouses to AWS allows you to take advantage of a portfolio of fully managed, high-performance, and cost-effective databases.

Common migration use case examples include:

* MongoDB to Amazon DocumentDB

* Oracle and SQL Server to Amazon Relational Database Service (Amazon RDS) and Amazon Aurora

* Cassandra to Amazon DynamoDB

* Terraform to Amazon Redshift

## Migrating data to AWS

There are several AWS tools and services to migrate data from an external database to AWS. AWS Database Migration Service (AWS DMS) helps you migrate databases to AWS efficiently and securely. The source database can remain fully operational during the migration, minimizing downtime to applications. At its most basic level, AWS DMS is an instance in the AWS Cloud that runs replication software.

## Migrating database schemas

There are many different migration strategies. Some common migrations include on-premises databases to the AWS Cloud, relational to non-relational databases, and databases hosted on Amazon EC2 to fully managed AWS databases services like Amazon Aurora. AWS DMS supports homogeneous migrations such as Oracle to Oracle as well as heterogeneous migrations between different database engines, such as Oracle to MySQL.

However, AWS DMS creates only those objects required to efficiently migrate the data. To migrate the remaining database elements and schema, you need to use other tools depending on the type of database migration. For example, if you are migrating an on-premises Microsoft SQL database, you can use native Microsoft SQL tools to migrate the database to Amazon RDS for Microsoft SQL.

**Homogeneous** migrations, where you migrate between same database engines, may require the use of native database tools to migrate database elements.

**Heterogenous** migrations, where you migrate between different database engines, require the use of the AWS Schema Conversion Tool (AWS SCT) to first translate your database schema to the new platform. You can then use AWS DMS to migrate the data. It is important to understand that AWS DMS and SCT are two different tools that serve different needs. 

<table style="width:100%;"><tbody><tr><td style="width:50%;background-color:rgb(243, 121, 52);text-align:center;"><span style="color:rgb(255, 255, 255);font-size:18px;"><strong>AWS Data Migration Service</strong></span><br><span style="color:rgb(255, 255, 255);font-size:18px;"><strong>(AWS DMS)</strong></span></td><td style="width:50%;background-color:rgb(243, 121, 52);text-align:center;"><span style="font-size:18px;color:rgb(255, 255, 255);"><strong>AWS Schema Conversion Tool</strong></span><br><span style="font-size:18px;color:rgb(255, 255, 255);"><strong>(AWS SCT)</strong></span><br></td></tr><tr><td rowspan="3" style="width:49.927%;text-align:center;"><span style="font-size:17px;">Loads the tables with data without any foreign keys or constraints<br></span></td><td style="width:50%;text-align:center;"><span style="font-size:17px;">Identifies the issues, limitations, and actions for the schema conversion<br></span></td></tr><tr><td style="width:50%;text-align:center;"><span style="font-size:17px;">Generates the target schema scripts, including foreign keys and constraints<br></span></td></tr><tr><td style="width:50%;text-align:center;"><span style="font-size:17px;">Converts code such as procedures and views from source to target and applies the code on the target.</span><br></td></tr></tbody></table>
