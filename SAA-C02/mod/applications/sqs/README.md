# SQS

> Amazon SQS is a web service that gives you access to a message queue that can be used to store messages while waiting for a computer to process them.
>
> It's a distributed queue system that enables web service applications to quickly and reliably queue messages that one component in the application generates to be consumed by another component.
>
> A queue is a temporary repository for messages that are awaiting processing.

Essentially enables us to store messages independently from our EC2 instances.

> Using Amazon SQS, you can decouple the components of an application so they run independently, easing message management between components.
>
> Any component of a distributed application can store messages in a fail-safe queue.
>
> Messages can contain up to 256 KB of text in any format. Any component can later retrieve the messages programmatically using the Amazon SQS API.

> The queue acts as a buffer between the component producing and saving data, and the component receiving the data for processing.
>
> This means the queue resolves issues that arise if the producer is producing work faster than the consumer can process it, or if the producer or consumer are intermittently connected to the network.

## Queue Types

> There are two types of queue:
>
> * Standard queues (default)
>
> * FIFO queues

## Standard Queues

> Amazon SQS offers standard as the default queue type. A standard queue lets you have a **nearly-unlimited number of transactions per second**. Standard queues guarantee that a message is delivered at least once.
>
> Occasionally (b/c of the highly-distributed architecture that allows high throughput), more than one copy of a message might be delivered out of order.
>
> However, standard queues provide best-effort ordering which ensures that messages are generally delivered in the same order as they are sent.

## FIFO Queues

> The FIFO queue complements the standard queue.
>
> The most important features of this queue type are **FIFO (first-in-first-out) delivery** and **exactly-once processing**: the order in which messages are sent and received is strictly preserved and a message is delivered once and remains available until a consumer processes and deletes it; duplicates are not introduced into that queue.

> FIFO queues also support message groups that allow multiple ordered message groups within a single queue.
>
> FIFO queues are limited to 300 transactions per second (TPS), but have all the capabilities of standard queues.

## Long Polling

* Long polling helps reduce your cost of using Amazon SQS by reducing the number of empty responses when there are no messages available to return in reply to a *ReceiveMessage* request sent to an Amazon SQS queue and eliminating false empty responses when messages are available in the queue but aren't included in the response.

* Long polling reduces the number of empty responses by allowing Amazon SQS to wait until a message is available in the queue before sending a response. Unless the connection times out, the response to the `ReceiveMessage` request contains at least one of the available messages, up to the maximum number of messages specified in the `ReceiveMessage` action.

* Long polling eliminates false empty responses by querying all (rather than a limited number) of the servers. Long polling returns messages as soon any message becomes available.

## Learning Summary

> * SQS is pull-based, not pushed-based.
>
> * Messages are 256 KB in size.
>
> * Messages can be kept in the queue from 1 minute to 14 days; the default retention period is 4 days.

> * Visibility timeout is the amount of time that the message is invisible in the SQS queue after a reader picks up that message. Provided the job is processed before the visibility timeout expires, the message will then be deleted from the queue. If the job is not processed within that time, the message will become visible again and another reader will process it. This could result in the same message being delivered twice.
>
> * Visibility timeout maximum is 12 hours.

> * SQS guarantees that your messages will be processed at least once.
>
> * Amazon SQS long polling is a way to retrieve messages from your Amazon SQS queues. While the regular short polling returns immediately (even if the message queue being polled is empty), long polling doesn't return a response until a message arrives in the message queue, or the long poll times out.
>
> * Any time you see a scenario based question about "decoupling" your infrastructure - think SQS.
