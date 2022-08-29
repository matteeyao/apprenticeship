# Event Processing Patterns

## Event-Driven Architecture

> **Publish/Subscribe (Pub/Sub) Messaging**
>
> Decoupled systems running in response to events

**SNS Topic** provides a mechanism to broadcast asynchronous event notifications and endpoints that allow other AWS services to connect to the topic in order to send and receive those messages. To broadcast a message, a component called a **Publisher** simply pushes a message to the topic. The publisher can be our own application or one of many AWS services that can publish messages to SNS topics. All services that subscribed to the topic will instantly receive every message that is broadcast. The **Subscribers** to the message topic often perform different functions and can each do something different w/ the message in parallel. The **Publisher** doesn't need to know who's using the information that is broadcasting and the **Subscribers** don't need to know who the message comes from.

## Event processing patterns

> **Dead-Letter Queue (DLQ)**
>
> * **SNS**
>
>   * Messages published to a topic that fail to deliver are sent to an **SNS queue**; held for further analysis or reprocessing.
>
> * **SQS**
>
>   * Messages sent to SQS that exceed the queue's **maxReceiveCount** are sent to a DLQ (another SQS queue)
>
> * **Lambda**
>
>   * Result from failed **asynchronous** invocations; will retry twice and send to **either** an SQS queue or SNS topic

Messages in the Dead-Letter Queue mean your normal error handling and retries have all failed. The Dead-Letter Queue becomes a list of issues that need your attention. It could indicate bugs or it could indicate exceptional situations that your code isn't designed to handle.

> **Fanout Pattern**
>
> ![Fig. 1 Fanout Pattern](../../../../img/aws/applications/event-processing-patterns/fanout-pattern.png)

> **S3 Event Notifications**
>
> ![Fig. 2 S3 Event Notification](../../../../img/aws/applications/event-processing-patterns/s3-event-notification.png)
>
> ![Fig. 3 S3 Event Notifications](../../../../img/aws/applications/event-processing-patterns/s3-event-notifications.png)

## Learning summary

> * Understand the pub/sub pattern - facilitated by SNS
>
> * DLQ - SNS, SQS, Lambda
>
> * Fanout pattern - SNS
>
> * S3 event notifications - which events trigger; which services consume
