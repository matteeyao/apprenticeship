# DynamoDB

**Amazon DynamoDB** is a fast and flexible NoSQL database service for all applications that need consistent, single-digit millisecond latency at any scale. It is a fully managed database and supports both document and key-value data models. Its flexible data model and reliable performance make it a great fit for mobile, web, gaming, ad-tech, IoT, and many other applications.

**Amazon DynamoDB** is a NoSQL database that delivers single-digit millisecond performance at any scale. W/ a few clicks in the AWS Management Console, customers can launch a new Amazon DynamoDB database table, scale their requested capacity w/o downtime or performance degradation, and gain visibility into resource usage and performance metrics.

## DynamoDB basics

> **The basics of DynamoDB** are as follows:
>
> * Stored on SSD storage
>
> * Spread across 3 geographically distinct data centres
>
> * Eventual Consistent Reads (Default)
>
> * Strongly Consistent Reads

* So if your application doesn't need to read your updated data within one second, then you want eventual consistency, and this is what you get by default.

* If, however, your application needs that data to be updated or always has to have a perfect copy of the data within less than one second, then use Strongly Consistent Read

## DynamoDB Reads

> **Eventual Consistent Reads**
>
> * Consistency across all copies of data is usually reached within a second. Repeating a read after a short time should return the updated data (Best Read Performance).

> **Strongly Consistent Reads**
>
> * A strongly consistent read returns a result that reflects all writes that received a successful response prior to the read.

## Learning summary

> **The basics of DynamoDB** are as follows:
>
> * Stored on SSD storage
>
> * Spread across 3 geographically distinct data centres
>
> * Eventual Consistent Reads (Default)
>
> * Strongly Consistent Reads
