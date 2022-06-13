# Streams

> * Time-ordered sequence of item-level changes in a table

![DynamoDB Streams](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/images/streams-terminology.png)

Your stream records appear in the same sequence as the item modifications.

Think of a first-in-first-out queue of your data.

> * Stored for **24 hours**
>
> * Inserts, updates, and deletes to DynamoDB table items

A stream consists of stream records. Each stream record represents a single data modification in the DynamoDB table to which the stream belongs. Each stream record is assigned a sequence number, reflecting the order in which the record was published to the stream and stream records are organized into groups or shards.

A shard acts as a container for multiple stream records and the shard contains information required for accessing and iterating through these records. The stream records within a shard are removed automatically after 24 hours.

> * Combine w/ Lambda functions for functionality like stored procedures

## Use cases

* Cross-region replication (which DynamoDB implements automatically though Global Tables)

* Establish relationships across tables, using streams

* Used for messaging or notification applications

* Aggregation or filtering

* Analytical reporting

* Archiving, auditing, search, etc.
