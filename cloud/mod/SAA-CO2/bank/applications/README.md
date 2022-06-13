# Applications questions

1. What is the difference between SNS and SQS?

**SNS is a push notification service, whereas SQS is a message system that requires worker nodes to poll a queue**.

SNS is a Notification service for sending text based communication on different types to different destinations. SQS is a Queue system for asynchronously managing tasks (called messages).

2. Amazon SQS offers a standard as the default queue type. Standard queues ensure that messages are delivered in the same order as they're sent.

**False**

If you have an existing application that uses standard queues and you want to take advantage of the ordering or exactly-once processing features of FIFO queues, you need to configure the queue and your application correctly. You can't convert an existing standard queue into a FIFO queue. To make the move, you must either create a new FIFO queue for your application or delete your existing standard queue and recreate it as a FIFO queue. Reference: [Amazon SQS FIFO (First-In-First-Out queues)](https://docs.aws.amazon.com/AWSSimpleQueueService/latest/SQSDeveloperGuide/FIFO-queues.html).

3. Amazon's SQS service guarantees a message will be delivered at least once.

**True**

Standard queues provide at-least-once delivery, which means that each message is delivered at least once. Reference: [Amazon SQS FAQs](https://aws.amazon.com/sqs/faqs/).

4. Amazon Kinesis Data Firehose is used for _.

[ ] Generating analytics dashboards of your streaming data.

[x] Loading streaming data into data lakes, data stores, and analytics tools.

[x] Capturing, transforming, and loading streaming data into Amazon S3


Amazon Kinesis Data Firehose is the easiest way to load streaming data into data stores and analytics tools. It can capture, transform, and load streaming data into Amazon S3, Amazon Redshift, Amazon Elasticsearch Service, and Splunk, enabling near real-time analytics w/ existing business intelligence tools and dashboards you're already using today.

5. In SWF, what does a "domain" refer to?

**A collection of related workflows**

Domains provide a way of scoping Amazon SWF resources within your AWS account. All of the components of a workflow, such as the workflow type and activity types, must be specified in a domain. It is possible to have more than one workflow in a domain; however, workflows in different domains can't interact w/ each other.

6. Amazon SWF ensures that a task is assigned only once and is never duplicated.

**True**

One time only consumption is a key feature of SWF. At one time this was a key distinction from SQS, however w/ SQS FIFO queues, this is no longer a distinguishing feature. Reference: [Amazon SWF FAQs](https://aws.amazon.com/swf/faqs/).

7. How can you prevent an application behind Amazon API Gateway from being overwhelmed by too many requests and improve overall performance across the APIs in your account?

**Enable caching and set throttling limits**.

If a cache is configured, then Amazon API Gateway will return a cached response for duplicate requests for a customizable time, but only if under configured throttling limits. This balance between the backend and client ensures optimal performance of the APIS for the applications that it supports. [Throttle API requests for better throughput](https://docs.aws.amazon.com/apigateway/latest/developerguide/api-gateway-request-throttling.html).

You can set throttling limits to improve overall performance across APIs in your account. You can also enable API caching in Amazon API Gateway to cache your endpoint's responses. W/ caching, you can reduce the number of calls made to your endpoints and also improve the latency of requests to your API.

8. What happens when you create a topic on Amazon SNS?

**An Amazon Resource Name is created**.

When a topic is created, Amazon SNS will assign a unique ARN (Amazon Resource Name) to the topic, which will include the service name (SNS), region, AWS ID of the user and the topic name. The ARN will be returned as part of the API call to create the topic. Whenever a publisher or subscriber needs to perform any action on the topic, they should reference the unique topic ARN.

Reference: [Amazon SNS FAQs](https://aws.amazon.com/sns/faqs/).

9. What is Amazon Kinesis Data Streams?

**A service on AWS that ingests and stores data streams for processing**.

Amazon Kinesis Data Streams (KDS) is a massively scalable and durable real-time data streaming service. KDS can continuously capture gigabytes of data per second from hundreds of thousands of sources such as website click-streams, database event streams, financial transactions, social media feeds, IT logs, and location-tracking events. The data collected is available in milliseconds to enable real-time analytics use cases such as real-time dashboards, real-time anomaly detection, dynamic pricing, and more.

Amazon Athena is a service for analyzing data, creating dashboards, and storing data in S3.

10. Does the Standard SQS message queue preserve message order and guarantee messages are only delivered once?

**No**

The Standard SQS message queue does not preserve message order nor guarantee messages are delivered only once - these are features of a FIFO message queue. Since Standard SQS message queues are designed to be massively scalable using a highly distributed architecture, receiving messages in the exact order they are sent is not guaranteed. Standard queues provide at-least-once delivery, and in some circumstances, duplicates can occur.

11. You have discovered duplicate messages being processed in your SQS queue. How do you resolve this?

**Increase the visibility timeout of your queue, so that messages do not become visible once obtained by a consumer**.

Duplicate messages occur when a consumer does not complete its message processing and the visibility timeout of the message expires, making it visible for another consumer to obtain. Increasing the visibility timeout to enable the consumer processing to complete, will prevent duplicate messages.

12. When you build an application layer on top of Amazon SWF, it restricts you to the use use of a specific programming language.

**False**

While there are a limited range of SDKs available for SWF, AWS provides an HTTP based API which allows you to interact using any language as long as you phrase the interactions in HTTP requests.

13. Amazon SWF is designed to help users _.

**Coordinate synchronous and asynchronous tasks**.

Similar to SQS, SWF manages queues of work, however unlike SQS it can have out-of-band parallel and sequential task to be completed by humans and non AWS services.

14. At a high level an Amazon API Gateway is a "front door" for applications to access data, business logic, or functionality from your backend services.

**True**

Amazon API Gateway is a fully managed service that makes it easy for developers to publish, maintain, monitor, and secure APIs at any scale. W/ a few clicks in the AWS Management Console, you can create an API that acts as a "front door" for applications to access data, business logic, or functionality from your AWS origin back-end services. Using API Gateway, you can create RESTful APIs and WebSocket APIs that enable real-time two-way communication applications. API Gateway supports containerized and serverless workloads, as well as web applications. [Amazon API Gateway](https://aws.amazon.com/api-gateway/).

15. What application allows you to decouple your infrastructure using message based queues?

**SQS**

In IT the term 'message' can be used in the common sense, or to describe a piece of data of Task in an asynchronous queueing system such as MQseries, RabbitMQ, or SQS.

16. What are the key components of Kinesis Data Firehose?

**Delivery streams, records of data and destinations**.

Key components of Kinesis Data Firehose are: delivery streams, records of data and destinations. Producers, shards, and consumers are components of Kinesis Data Streams.
