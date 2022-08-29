# Applications summary

## SQS

> * SQS is a way to decouple your infrastructure
>
> * SQS is pull based, not pushed based.
>
> * Messages are 256 KB in size.
>
> * Messages can be kept in the queue from 1 minute to 14 days; the default retention period is 4 days.
>
> * Standard SQS and FIFO SQS
>
> * Standard order is not guaranteed and messages can be delivered more than once.
>
> * FIFO order is strictly maintained and messages are delivered only once.

> * Visibility Time Out is the amount of time that the message is invisible in the SQS queue after a reader picks up that message. Provided the job is processed before the visibility time out expires, the message will then be deleted from the queue. If the job is not processed within that time, the message will become visible again and another reader will process it. This could result in the same message being delivered twice.
>
> * Visibility timeout maximum is 12 hours.

So if you are getting the same message being delivered twice and this is the root cause, you may want to increase your visibility timeout, just to give your EC2 instances a little bit more time to process the message.

> * SQS guarantees that your messages will be processed at least once.
>
> * Amazon SQS long polling is a way to retrieve messages from your Amazon SQS queues. While the regular short polling returns immediately (even if the message queue being polled is empty), long polling doesn't return a response until a message arrives in the message queue, or the long poll times out.

## SWF vs SQS

> * SQS has a retention period of up to 14 days; w/ SWF, workflow executions can last up to 1 year.
>
> * AMZN Simple Workflow (SWF) presents a task-oriented API, whereas AMZN SQS offers a message-oriented API.
>
> * AMZN SWF ensures that a task is assigned only once and is never duplicated. W/ AMZN SQS, you need to handle duplicated messages and may also need to ensure that a message is processed only once.
>
> * AMZN SWF keeps track of all the tasks and events in an application. W/ AMZN SQS, you need to implement your own application-level tracking, especially if your application uses multiple queues.

## SWF Actors

> * **Workflow Starters** ▶︎ An application that can initiate (start) a workflow. Could be your e-commerce website following the placement of an order, or a mobile app searching for bus times.
>
> * **Deciders** ▶︎ Control the flow of activity tasks in a workflow execution. If something has finished (or failed) in a workflow, a Decider decides what to do next.
>
> * **Activity Workers** ▶︎ Carry out the activity tasks.

## SNS Benefits

> * Instantaneous, push-based delivery (no polling)
>
> * Simple APIs and easy integration w/ applications
>
> * Flexible message delivery over multiple transport protocols
>
> * Inexpensive, pay-as-you-go model w/ no up-front costs
>
> * Web-based AWS Management Console offers the simplicity of a point-and-click interface

## SNS vs. SQS?

> * Both Messaging Services in AWS
>
> * SNS - Push
>
> * SQS - Polls (Pulls)

## Elastic Transcoder

> Just remember that Elastic Transcoder is a media transcoder in the cloud. It converts media files from their original source format in to different formats that will play on smartphones, tablets, PCs, etc.

## API Gateway

> * Gateway to your AWS resources.
>
> * API Gateway has caching capabilities to increase performance.
>
> * API Gateway is low cost and scales automatically
>
> * You can throttle API Gateway to prevent attacks
>
> * You can log results to CloudWatch
>
> * If you are using JavaScript/AJAX that uses multiple domains w/ API Gateway, ensure that you have enabled CORS on API Gateway
>
> * CORS is enforced by the client

## Kinesis

> * Know the difference between Kinesis Streams and Kinesis Firehose. You will be given scenario questions and you must choose the most relevant service.
>
> * Kinesis Analytics helps you analyze your data in both Firehose and Kinesis Streams.

**Kinesis Streams** has data persistence, storing your data by default for 24 hours. EC2 instances will go in and retrieve data from the streams. **Kinesis Firehose** is where you need to analyze your data in real time and then find a place to store it b/c there is no persistence so you could have Lambda functions running in Firehose that then store that data in S3, or place that data in Elastic Search clusters. So if you see anything mentioning about shards or about data persistence, you want streams. If you want real time analytics, and a place to put it in instantly, w/ no data persistence, then you're going to be using **Firehose**.

## Cognito

> * Federation allows users to authenticate w/ a Web Identity Provider (Google, Facebook, Amazon)
>
> * The user authenticates first w/ the Web ID Provider and receives and authentication token, which is exchanged for temporary AWS credentials allowing them to assume an IAM role.
>
> * Cognito is an Identity Broker which handles interaction between your application and the Web ID provider (You don't need to write your own code to do this).

> * User pool is user based. It handles things like user registration, authentication, and account recovery.
>
> * Identity pools authorize access to AWS resources.
