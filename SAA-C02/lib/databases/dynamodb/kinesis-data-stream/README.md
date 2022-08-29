# DynamoDB table used by Kinesis Stream

1. A company has a data analytics application that updates a real-time foreign exchange dashboard and another separate application that archives data to Amazon Redshift. Both applications are configured to consume data from the same stream concurrently and independently by using Amazon Kinesis Data Streams. However, they noticed that there are a lot of occurrences where a shard iterator expires unexpectedly. Upon checking, they found out that the DynamoDB table used by Kinesis does not have enough capacity to store the lease data.

Which of the following is the most suitable solution to rectify this issue?

[ ] Upgrade the storage capacity of the DynamoDB table.

[ ] Increase the write capacity assigned to the shard table.

[ ] Use Amazon Kinesis Data Analytics to properly support the data analytics application instead of Kinesis Data Stream.

[ ] Enable In-Memory Acceleration w/ DynamoDB Accelerator (DAX).

**Explanation**: A new shard iterator is returned by every **GetRecords** request (as `NextShardIterator`), which you then use in the next **GetRecords** request (as `ShardIterator`). Typically, this shard iterator does not expire before you use it. However, you may find that shard iterators expire because you have not called **GetRecords** for more than 5 minutes or b/c you've performed a restart of your consumer application.

If the shard iterator expires immediately before you can use it, this might indicate that the DynamoDB table used by Kinesis does not have enough capacity to store the lease data. This situation is more likely to happen if you have a large number of shards. To solve this problem, increase the write capacity assigned to the shard table.

Hence, **increasing the write capacity assigned to the shard table** is the correct answer.

> **Upgrading the storage capacity of the DynamoDB table** is incorrect because DynamoDB is a fully managed service that automatically scales its storage without setting it up manually. The scenario refers to the **write capacity** of the shard table as it says that the DynamoDB table used by Kinesis does not have enough *capacity* to store the lease data.

> **Enabling In-Memory Acceleration with DynamoDB Accelerator (DAX)** is incorrect because the DAX feature is primarily used for read performance improvement of your DynamoDB table from *milliseconds* response time to *microseconds*. It does not have any relationship with Amazon Kinesis Data Stream in this scenario.

> **Using Amazon Kinesis Data Analytics to properly support the data analytics application instead of Kinesis Data Stream** is incorrect. Although Amazon Kinesis Data Analytics can support a data analytics application, it is still not a suitable solution for this issue. You simply need to increase the write capacity assigned to the shard table in order to rectify the problem, which is why switching to Amazon Kinesis Data Analytics is not necessary.

<br />
