# AWS Lambda Foundations

1. Which monitoring tool provides the ability to visualize the components of an application and the flow of API calls?

**AWS X-Ray**

**Explanation**: You can use AWS X-Ray to visualize the components of your application, identify performance bottlenecks, and troubleshoot requests that resulted in an error. Your Lambda functions send trace data to X-Ray, and X-Ray processes the data to generate a service map and searchable trace summaries. AWS X-Ray records how the Lambda functions are running. Use it to identify the call flow of your Lambda function and the performance of every API call within your application.

2. Match the terms on the left with the appropriate definition.

**Producer** ▶︎ Create events w/ all required information

**Router** ▶︎ Ingests and filters events using rules

**Consumer** ▶︎ Subscribe and are notified when events occur

**Explanation**:

  * Producers are users who create the events. Events contain all the necessary information required for the consumers to take action on the event.

  * The router ingests, filters, and pushes the events to the appropriate consumers. It does this by using a set of rules or another service, such as Amazon Simple Notification Service (Amazon SNS), to send the messages.

  * Consumers subscribe to be notified about the events, or they can monitor an event stream and act on events that pertain only to them.

3. What does an AWS Identity and Access Management (IAM) resource-based policy control?

[x] Permissions to invoke the function

[ ] What the other AWS services can do when processing the events

[ ] Permissions to create the function

[ ] What the function can do within the other AWS services

**Explanation**: A resource policy (also called a function policy) is used to tell the Lambda service which principals have permission to invoke the Lambda function.

4. Which of these statements describe a resource policy? (Select THREE.) 

[x] Can grant access to the Lambda function across AWS accounts

[ ] Determines what Lambda is allowed to do

[x] Determines who has access to invoke the function

[x] Can give Amazon S3 permission to initiate a Lambda function

[ ] Can give Lambda permission to write data to a DynamoDB table

[ ] Must be chosen or created when you create a Lambda function

**Explanation**: A resource policy determines who is allowed in (who can initiate your function, such as Amazon S3), and it can be used to grant access across accounts. 

An execution role must be created or selected when creating your function, and it controls what Lambda is allowed to do (such as writing to a DynamoDB table). It includes a trust policy with AssumeRole. 

5. What is the importance of the IAM execution role?

[ ] Allows individual users control over creating and authoring the function

[ ] Gives your function permissions to run within an account

[x] Gives your function permissions to interact with other services

[ ] Allows groups of users to invoke the execution role to test the function.

**Explanation**: The IAM execution role grant your function permissions to interact with other services. You specify this execution role when you create a function. AWS Lambda assumes the execution role when your function is invoked. The policy for this execution role defines the actions the execution role is allowed to take—for example, writing to a DynamoDB table.

6. Which capabilities are features of Lambda? (Select THREE.)

[ ] Updates the operating system without scheduling

[x] Triggers Lambda functions on your behalf in response to events

[ ] Requires no configuration of memory or CPU

[x] Runs code without you provisioning or managing servers

[ ] Scales automatically

**Explanation**: You can write code for Lambda in a programming language that you already know. Development in Lambda is not tightly coupled to AWS so you can easily port code in and out of AWS. Instead of scaling by adding servers, Lambda scales in response to events. You configure memory settings, and AWS handles details such as CPU, network, and I/O throughput. 

7. What are the reasons for setting a concurrency limit (or reserve) on a function? (Select THREE.)

[ ] Facilitate CloudWatch Lambda Insight metrics

[ ] Restrict memory usage

[x] Match the limit with a downstream resource

[x] Manage costs

[x] Regulate how long it takes to process a batch of events

[ ] Verify that Amazon Simple Queue Service (Amazon SQS) queues clear

**Explanation**: Reasons for setting a concurrency reserve for a function can include the following:

* Managing cost

* Matching speed with a downstream resource

* Regulating how long it takes to process events

You might try to limit the number of concurrent runs. You might also want to make sure that your function can scale up to a reserved level regardless of other functions that are running.

8. Which patterns are Lambda invocation models? (Select THREE.)

[ ] Event source mapping

[ ] First In-First Out (FIFO)

[x] Synchronous

[x] Asynchronous

[ ] Trigger

[x] Polling

**Explanation**: Event sources can invoke a Lambda function in three general design patterns. These patterns are called invocation models: synchronous, asynchronous, and polling.

9. Which feature can a developer enable to create a copy of a function for testing?

[ ] Aliases

[ ] Replica

[x] Versioning

[ ] Copy

**Explanation**: You can use versions to manage the deployment of your functions. For example, you can publish a new version of a function for beta testing without affecting users of the stable production version. AWS Lambda creates a new version of your function each time that you publish the function. The new version is a copy of the unpublished version of the function.
