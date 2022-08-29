# Serverless

1. In which direction(s) does Lambda scale automatically?

**Out**

Lambda scales out automatically - each time your function is triggered, a new, separate instance of that function is started. There are limits, but these can be adjusted on request.

2. Lambda pricing is based on which of these measurements after the free tier?

[x] The amount of memory assigned.

[ ] The amount of CPU you choose.

[x] The number of requests for each time the lambda executes in response to an event notification, or invoke call.

[x] Duration of each request (in ms).

Lambda billing is based on both The MB of RAM reserved and the execution duration in 100ms units. You don't choose the amount of CPU when setting up a Lambda Function. You can however choose between x86 or Arm/Graviton2 processors which does impact the price. (Note: the CPU choice came into general feature availability on 29 SEP 2021. AWS generally waits 6 months before new feature information is potentially introduced to exams).

The number of requests for each time the lambda executes in response to an event notification, or invoke call is one of the factors involved in Lambda pricing. As of December 2020, the Lambda duration is billed in 1ms increments instead of being rounded up to the nearest 100 ms increment per invoke. For example, a function that runs in 30ms on average used to be billed for 100ms. Now, it will be billed for 30ms resulting in a 70% drop in its duration speed. Reference: [AWS Lambda changes duration billing granularity from 100ms to 1ms](https://aws.amazon.com/about-aws/whats-new/2020/12/aws-lambda-changes-duration-billing-granularity-from-100ms-to-1ms/).

3. On Friday morning your marketing manager calls an urgent meeting to celebrate that they have secured a deal to run a coordinated national promotion on TV, radio, and social media over the next 10 days. They anticipate a 500x increase on site visits and trial registrations. After the meeting you throw some ideas around w/ your team about how to ensure that your current 1 server web site will survive. Which of these best embody the AWS design strategy for this situation.

[ ] Recreate your 5 most popular new customer web pages and sign up web pages on Lightsail and take advantage of AWS auto scaling to pick up the load.

[ ] Upgrade your existing server from a 1xlarge to a 32xlarge for the duration of the campaign.

[ ] Work w/ your web design team to create some web pages in PHP to run on a 32xlarge EC2 instance to emulate your 5 most popular information web pages and sign up web pages.

[ ] Create a stand by sign up server to use in case the primary fails due to load.

[x] Work w/ your web design team to refactor some web pages w/ embedded JavaScript for your 5 most popular information web pages and sign up web pages. Host those pages on S3 w/ static web hosting.

[x] Create a duplicate sign up page that stores registration details in DynamoDB for asynchronous processing using SQS & Lambda.

An AWS solution for this situation might include S3 static web pages w/ client side scripting (JavaScript) to meet high demand of information pages. Use NoSQL database to collect customer registration for asynchronous processing, and SQS backed by scalable compute to keep up w/ the requests.

A standby server is a good idea, but will not help w/ the anticipated 500x load increase.

4. You have created a simple serverless website using S3, Lambda, API Gateway and DynamoDB. Your website will process the contact details of your customers, predict and expected delivery date of their order and store their order in DynamoDB. You test the website before deploying it into production and you notice that although the page executes, and the lambda function is triggered, it is unable to write to DynamoDB. What could be the cause of this issue?

**Your lambda function does not have sufficient Identity Access Management (IAM) permissions to write to DynamoDB.**

Like any services in AWS, Lambda needs to have a Role associated w/ it that provide credentials w/ rights to other services. This is exactly the same as needing a Role on an EC2 instance to access S3 or DynamoDB.

5. Which of the following services can invoke a Lambda function synchronously (w/ functionality built-in w/ the invoking service)?

[x] Amazon Lex

[ ] S3

[ ] IAM

[x] Kinesis Data Firehose

[x] API Gateway

[ ] EC2

ALB, Cognito, Lex, Alexa, API Gateway, CloudFront, and Kinesis Data Firehose are all valid direct (synchronous) triggers for Lambda functions. S3 is one of the valid asynchronous triggers.

S3 is one of the valid **asynchronous** triggers.

6. What AWS service can help you to understand how your Lambda functions are performing?

**AWS X-Ray**

AWS X-Ray helps developers analyze and debug production, distributed applications, such as those built using a microservices & serverless architectures. W/ X-Ray, you can understand how your application and its underlying services are performing to identify and troubleshoot the root cause of performance issues and errors.

7. What does the common term 'Serverless' mean according to AWS

[x] The ability to run applications and services w/o thinking about servers or capacity provisioning.

[x] A native Cloud Architecture that allows customers to shift more operational responsibility to AWS.

[ ] A pricing model based on high level commodity measures such as on compute duration and storage capacity.

'Serverless' computing is not about eliminating servers, but shifting most of the responsibility for infrastructure and operation of the infrastructure to a vendor so that you can focus more on the business services, not how to manage infrastructure that they run on. Billing does tend to be based on simple units, but the choice of services, intended using pattern (RIs), and amount of capacity needed also influences the pricing.

8. As a DevOps engineer you are told to prepare a complete solution to run a piece of code that requires multi-threaded processing. The code has been running on an old custom server using a 4 core Intel Xeon processor. Which of these options best describes the AWS compute services that could be used for multi-threaded processing?

**EC2, ECS, and Lambda**

The exact ratio of cores to memory has varied over time for Lambda instances, however Lambda like EC2 and ECS supports hyper-threading on one or more virtual CPUs (if your code supports hyper-threading).

9. You have created a serverless application to add metadata to images that are uploaded to a specific S3 bucket. To do this, your lambda function is configured to trigger whenever a new image is created in the bucket. What will happen when multiple users upload multiple different images at the same time?

**Multiple instances of the Lambda function will be triggered, one for each image**

Each time a Lambda function is triggered, an isolated instance of that function is invoked. Multiple triggers result in multiple concurrent invocations, one for each time it is triggered
