# Kinesis Stream

1. A media company recently launched their newly created web application. Many users tried to visit the website, but they are receiving a 503 Service Unavailable Error. The system administrator tracked the EC2 instance status and saw the capacity is reaching its maximum limit and unable to process all the requests. To gain insights from the application's data, they need to launch a real-time analytics service.

Which of the following allows you to read records in batches?

[ ] Create a Kinesis Data Stream and use AWS Lambda to read records from the data stream.

[ ] Create a Kinesis Data Firehose and use AWS Lambda to read records from the data stream.

[ ] Create an Amazon S3 bucket to store the captured data and use Amazon Athena to analyze the data.

[ ] Create an Amazon S3 bucket to store the captured data and use Amazon Redshift Spectrum to analyze the data.

**Explanation**: **Amazon Kinesis Data Streams (KDS)** is a massively scalable and durable real-time data streaming service. KDS can continuously capture gigabytes of data per second from hundreds of thousands of sources. You can use an AWS Lambda function to process records in Amazon KDS. By default, Lambda invokes your function as soon as records are available in the stream. Lambda can process up to 10 batches in each shard simultaneously. If you increase the number of concurrent batches per shard, Lambda still ensures in-order processing at the partition-key level.

The first time you invoke your function, AWS Lambda creates an instance of the function and runs its handler method to process the event. When the function returns a response, it stays active and waits to process additional events. If you invoke the function again while the first event is being processed, Lambda initializes another instance, and the function processes the two events concurrently. As more events come in, Lambda routes them to available instances and creates new instances as needed. When the number of requests decreases, Lambda stops unused instances to free upscaling capacity for other functions.

Since the media company needs a real-time analytics service, you can use Kinesis Data Streams to gain insights from your data. The data collected is available in milliseconds. Use AWS Lambda to read records in batches and invoke your function to process records from the batch. If the batch that Lambda reads from the stream only has one record in it, Lambda sends only one record to the function.

Hence, the correct answer in this scenario is: **Create a Kinesis Data Stream and use AWS Lambda to read records from the data stream**.

> The option that says: **Create a Kinesis Data Firehose and use AWS Lambda to read records from the data stream** is incorrect. Although Amazon Kinesis Data Firehose captures and loads data in near real-time, AWS Lambda can't be set as its destination. You can write Lambda functions and integrate it with Kinesis Data Firehose to request additional, customized processing of the data before it is sent downstream. However, this integration is primarily used for stream processing and not the actual consumption of the data stream. You have to use a Kinesis Data Stream in this scenario.

> The options that say: **Create an Amazon S3 bucket to store the captured data and use Amazon Athena to analyze the data** and **Create an Amazon S3 bucket to store the captured data and use Amazon Redshift Spectrum to analyze the data** are both incorrect. As per the scenario, the company needs a real-time analytics service that can ingest and process data. You need to use Amazon Kinesis to process the data in real-time.

<br />

2. A Solutions Architect designed a real-time data analytics system based on Kinesis Data Stream and Lambda. A week after the system has been deployed, the users noticed that it performed slowly as the data rate increases. The Architect identified that the performance of the Kinesis Data Streams is causing this problem.

Which of the following should the Architect do to improve performance?

[ ] Increase the number of shards of the Kinesis stream by using the `UpdateShardCount` command.

[ ] Improve the performance of the stream by decreasing the number of its shards using the `MergeShard` command.

[ ] Implement Step Scaling to the Kinesis Data Stream.

[ ] Replace the data stream w/ Amazon Kinesis Data Firehose instead.

**Explanation**: Amazon Kinesis Data Streams supports *resharding*, which lets you adjust the number of shards in your stream to adapt to changes in the rate of data flow through the stream. Resharding is considered an advanced operation.

There are two types of resharding operations: shard split and shard merge. In a shard split, you divide a single shard into two shards. In a shard merge, you combine two shards into a single shard. Resharding is always *pairwise* in the sense that you cannot split into more than two shards in a single operation, and you cannot merge more than two shards in a single operation. The shard or pair of shards that the resharding operation acts on are referred to as *parent* shards. The shard or pair of shards that result from the resharding operation are referred to as *child* shards.

Splitting increases the number of shards in your stream and therefore increases the data capacity of the stream. Because you are charged on a per-shard basis, splitting increases the cost of your stream. Similarly, merging reduces the number of shards in your stream and therefore decreases the data capacity—and cost—of the stream.

If your data rate increases, you can also increase the number of shards allocated to your stream to maintain the application performance. You can reshard your stream using the **UpdateShardCount** API. The throughput of an Amazon Kinesis data stream is designed to scale without limits via increasing the number of shards within a data stream. Hence, the correct answer is to **increase the number of shards of the Kinesis stream by using the `UpdateShardCount` command.**

> **Replacing the data stream with Amazon Kinesis Data Firehose instead** is incorrect because the throughput of Kinesis Firehose is not exceptionally higher than Kinesis Data Streams. In fact, the throughput of an Amazon Kinesis data stream is designed to scale **without** limits via increasing the number of shards within a data stream.

> **Improving the performance of the stream by decreasing the number of its shards using the `MergeShard` command** is incorrect because merging the shards will effectively decrease the performance of the stream rather than improve it.

> **Implementing Step Scaling to the Kinesis Data Stream** is incorrect because there is no Step Scaling feature for Kinesis Data Streams. This is only applicable for EC2.

<br />
