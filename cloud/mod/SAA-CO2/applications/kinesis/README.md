# Kinesis

## What is Streaming Data?

> Streaming Data is data that is generated continuously thousands of data sources, which typically send in the data records simultaneously, and in small sizes (order of Kilobytes).
>
> * Purchases from online stores (think amazon.com)
>
> * Stock Prices
>
> * Game data (as the gamer plays)
>
> * Social network data
>
> * Geospatial data (think uber.com)
>
> * iOT sensor data

## What is Kinesis?

> Amazon Kinesis is a platform on AWS to send your streaming data to. Kinesis makes it easy to load and analyze streaming data, and also provides the ability for you to build your own custom applications for your business needs.

## Types of Kinesis

> **3 Different Types of Kinesis**
>
> * Kinesis Streams
>
> * Kinesis Firehouse
>
> * Kinesis Analytics

## Kinesis streams

> **Kinesis Streams Consist of Shards**:
>
> * 5 transactions per second for reads, up to a maximum total data read rate of 2 MB per second and up to 1,000 records per second for writes, up to a maximum total data write rate of 1 MB per second (including partition keys).
>
> * The data capacity of your stream is a function of the number of shards that you specify for the stream. The total capacity of the stream is the sum of the capacities of its shards.

## Kinesis Firehose - S3

> * No data persistence
>
> * Analyze streams on the fly using Lambda

## Kinesis Analytics

> * Analyze your data inside Kinesis

## Learning summary

> **Kinesis**
>
> * Know the difference between Kinesis Streams and Kinesis Firehose. You will be given scenario questions and you must choose the most relevant service.
