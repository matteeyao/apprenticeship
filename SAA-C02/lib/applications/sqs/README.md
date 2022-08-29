# Standard Queue Service

1. A company is experiencing problems w/ its message-processing application. During periods of high demand, the application becomes overloaded. The application is based on a monolithic design and is hosted in an on-premises data center. The company wants to move the application to the AWS Cloud and decouple the monolithic architecture. A solutions architect must design a solution that allows worker components of the application to access the messages and handle the peak volume.

Which solution meets these requirements w/ the HIGHEST throughput?

[ ] Use a Network Load Balancer w/ target groups that are configured to perform the path-based routing to Amazon EC2 instances

[ ] Use Amazon Simple Queue Service (Amazon SQS) FIFO queues in combination w/ Amazon EC2 instances that are scaled by an Auto Scaling group

[ ] Use an Application Load Balancer w/ target groups that are configured to perform path-based routing to Amazon EC2 instances.

[x] Use Amazon Simple Queue Service (Amazon SQS) standard queues in combination w/ Amazon EC2 instances that are scaled by an Auto Scaling group.

**Explanation**: There is no indication that path-based routing will distribute traffic evenly. The use of a queueing tool is a better way to decouple this monolithic architecture. "Overload" and "decouple" are key words that signal SQS. SQS is an in-transit message service that prevents messages from being lost. If the order of messages processed need to be processed in the order that they are received or are "time-sensitive", FIFO SQS queues would be the better choice.

<br />

2. A company needs to perform asynchronous processing, and has Amazon SQS as part of a decoupled architecture. The company wants to ensure that the number of empty responses from polling requests are kept to a minimum.

What should a solutions architect do to ensure that empty responses are reduced?

[ ] Increase the maximum message retention period for the queue.

[ ] Increase the maximum receives for the redrive policy for the queue.

[ ] Increase the default visibility timeout for the queue.

[x] Increase the receive message wait time for the queue.

**Explanation**: When the `ReceiveMessageWaitTimeSeconds` property of a queue is set to a value greater than zero, long polling is in effect. Long polling reduces the number of empty responses by allowing Amazon SQS to wait until a message is available before sending a response to a `ReceiveMessage` request.

<br />

3. An application hosted in EC2 consumes messages from an SQS queue and is integrated with SNS to send out an email to you once the process is complete. The Operations team received 5 orders but after a few hours, they saw 20 email notifications in their inbox.

Which of the following could be the possible culprit for this issue?

[x] The web application is not deleting the messages in the SQS queue after it has processed them.

[ ] The web application is set to short polling on some messages are not being picked up

[ ] The web application is set for long polling so the messages are being sent twice.

[ ] The web application does not have permission to consume messages in the SQS queue.

**Explanation**: Always remember that the messages in the SQS queue will continue to exist even after the EC2 instance has processed it, until you delete that message. You have to ensure that you delete the message after processing to prevent the message from being received and processed again once the visibility timeout expires.

There are three main parts in a distributed messaging system:

  1. The components of your distributed system (EC2 instances)

  2. Your queue (distributed on Amazon SQS servers)

  3. Messages in the queue.

You can set up a system which has several components that send messages to the queue and receive messages from the queue. The queue redundantly stores the messages across multiple Amazon SQS servers.

Component 1 sends Message A to a queue, and the message is distributed across the Amazon SQS servers redundantly.

When Component 2 is ready to process a message, it consumes messages from the queue, and Message A is returned. While Message A is being processed, it remains in the queue and isn't returned to subsequent receive requests for the duration of the visibility timeout.

Component 2 **deletes** Message A from the queue to prevent the message from being received and processed again once the visibility timeout expires.

> The option that says: **The web application is set for long polling so the messages are being sent twice** is incorrect because long polling helps reduce the cost of using SQS by eliminating the number of empty responses (when there are no messages available for a ReceiveMessage request) and false empty responses (when messages are available but aren't included in a response). Messages being sent twice in an SQS queue configured with long polling is quite unlikely.

> The option that says: **The web application is set to short polling so some messages are not being picked up** is incorrect since you are receiving emails from SNS where messages are certainly being processed. Following the scenario, messages not being picked up won't result into 20 messages being sent to your inbox.

> The option that says: **The web application does not have permission to consume messages in the SQS queue** is incorrect because not having the correct permissions would have resulted in a different response. The scenario says that messages were properly processed but there were over 20 messages that were sent, hence, there is no problem with the accessing the queue.

<br />

4. A car dealership website hosted in Amazon EC2 stores car listings in an Amazon Aurora database managed by Amazon RDS. Once a vehicle has been sold, its data must be removed from the current listings and forwarded to a distributed processing system.

Which of the following options can satisfy the given requirement?

[ ] Create an RDS event subscription and send the notifications to Amazon SNS. Configure the SNS topic to fan out the event notifications to multiple Amazon SQS queues. Process the data using Lambda functions.

[ ] Create an RDS event subscription and send the notifications to Amazon SQS. Configure the SQS queues to fan out the event notifications to multiple Amazon SNS topics. Process the data using Lambda functions.

[ ] Create an RDS event subscription and send the notifications to AWS Lambda. Configure the Lambda function to fan out the event notifications to multiple SQS queues to update the processing system.

[ ] Create a native function or stored procedure that invokes a Lambda function. Configure the Lambda function to send notifications to an Amazon SQS queue for the processing system to consume.

**Explanation**: You can invoke an AWS Lambda function from an Amazon Aurora MySQL-Compatible Edition DB cluster with a native function or a stored procedure. This approach can be useful when you want to integrate your database running on Aurora MySQL with other AWS services. For example, you might want to capture data changes whenever a row in a table is modified in your database.

In the scenario, you can trigger a Lambda function whenever a listing is deleted from the database. You can then write the logic of the function to send the listing data to an SQS queue and have different processes consume it.

Hence, the correct answer is: **Create a native function or a stored procedure that invokes a Lambda function. Configure the Lambda function to send event notifications to an Amazon SQS queue for the processing system to consume**.

RDS events only provide operational events such as DB instance events, DB parameter group events, DB security group events, and DB snapshot events. What we need in the scenario is to capture data-modifying events (`INSERT`, `DELETE`, `UPDATE`) which can be achieved thru native functions or stored procedures. Hence, the other options are incorrect.

<br />

5. A company developed a web application and deployed it on a fleet of EC2 instances that uses Amazon SQS. The requests are saved as messages in the SQS queue, which is configured with the maximum message retention period. However, after thirteen days of operation, the web application suddenly crashed and there are 10,000 unprocessed messages that are still waiting in the queue. Since they developed the application, they can easily resolve the issue but they need to send a communication to the users on the issue.

What information should they provide and what will happen to the unprocessed messages?

[x] Tell the users that the application will be operational shortly and all received requests will be processed after the web application is restarted.

[ ] Tell the users that unfortunately, they have to resubmit all of the requests since the queue would not be able to process the 10,000 messages together.

[ ] Tell the users that the application will be operational shortly however, requests sent over three days ago will need to be resubmitted.

[ ] Tell the users that unfortunately, they have to resubmit all the requests again.

**Explanation**: In **Amazon SQS**, you can configure the message retention period to a value from 1 minute to 14 days. The default is 4 days. Once the message retention limit is reached, your messages are automatically deleted.

A single Amazon SQS message queue can contain an unlimited number of messages. However, there is a 120,000 limit for the number of inflight messages for a standard queue and 20,000 for a FIFO queue. Messages are inflight after they have been received from the queue by a consuming component, but have not yet been deleted from the queue.

In this scenario, it is stated that the SQS queue is configured with the maximum message retention period. The maximum message retention in SQS is 14 days that is why the option that says: **Tell the users that the application will be operational shortly and all received requests will be processed after the web application is restarted** is the correct answer i.e. there will be no missing messages.

> The options that say: **Tell the users that unfortunately, they have to resubmit all the requests again** and **Tell the users that the application will be operational shortly, however, requests sent over three days ago will need to be resubmitted** are incorrect as there are no missing messages in the queue thus, there is no need to resubmit any previous requests.

> The option that says: **Tell the users that unfortunately, they have to resubmit all of the requests since the queue would not be able to process the 10,000 messages together** is incorrect as the queue can contain an unlimited number of messages, not just 10,000 messages.

<br />

6. A company has several microservices that send messages to an Amazon SQS queue and a backend application that poll the queue to process the messages. The company also has a Service Level Agreement (SLA) which defines the acceptable amount of time that can elapse from the point when the messages are received until a response is sent. The backend operations are I/O-intensive as the number of messages is constantly growing, causing the company to miss its SLA. The Solutions Architect must implement a new architecture that improves the application's processing time and load management.

Which of the following is the MOST effective solution that can satisfy the given requirement?

[ ] Create an AMI of the backend application's EC2 instance. Use the image to set up an Auto Scaling group and configure a target tracking scaling policy based on the `CPUUtilization` metric w/ a target value of 80%.

[ ] Create an AMI of the backend application's EC2 instance. Use the image to set up an Auto Scaling group and configure a target tracking scaling policy based on the `ApproximateAgeOfOldestMessage` metric.

**Explanation**: **Amazon Simple Queue Service (SQS)** is a fully managed message queuing service that enables you to decouple and scale microservices, distributed systems, and serverless applications. SQS eliminates the complexity and overhead associated with managing and operating message-oriented middleware and empowers developers to focus on differentiating work. Using SQS, you can send, store, and receive messages between software components at any volume, without losing messages or requiring other services to be available.

The `ApproximateAgeOfOldestMessage` metric is useful when applications have time-sensitive messages and you need to ensure that messages are processed within a specific time period. You can use this metric to set Amazon CloudWatch alarms that issue alerts when messages remain in the queue for extended periods of time. You can also use alerts to take action, such as increasing the number of consumers to process messages more quickly.

With a target tracking scaling policy, you can scale (increase or decrease capacity) a resource based on a target value for a specific CloudWatch metric. To create a custom metric for this policy, you need to use AWS CLI or AWS SDKs. Take note that you need to create an AMI from the instance first before you can create an Auto Scaling group to scale the instances based on the `ApproximateAgeOfOldestMessage` metric.

Hence, the correct answer is: **Create an AMI of the backend application's EC2 instance. Use the image to set up an Auto Scaling Group and configure a target tracking scaling policy based on the `ApproximateAgeOfOldestMessage` metric.**

> The option that says: **Create an AMI of the backend application's EC2 instance. Use the image to set up an Auto Scaling Group and configure a target tracking scaling policy based on the `CPUUtilization` metric with a target value of 80%** is incorrect. Although this will improve the backend processing, the scaling policy based on the `CPUUtilization` metric is not meant for time-sensitive messages where you need to ensure that the messages are processed within a specific time period. It will only trigger the scale-out activities based on the CPU Utilization of the current instances, and not based on the age of the message, which is a crucial factor in meeting the SLA. To satisfy the requirement in the scenario, you should use the ApproximateAgeOfOldestMessage metric.
