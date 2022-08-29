# Lambda

AWS Lambda supports the synchronous and asynchronous invocation of a Lambda function. You can control the invocation type only when you invoke a Lambda function. When you use an AWS service as a trigger, the invocation type is predetermined for each service. You have no control over the invocation type that these event sources use when they invoke your Lambda function. Since processing only takes 5 minutes, Lambda is also a cost-effective choice.

You can use an AWS Lambda function to process messages in an Amazon Simple Queue Service (Amazon SQS) queue. Lambda event source mappings support standard queues and first-in, first-out (FIFO) queues. With Amazon SQS, you can offload tasks from one component of your application by sending them to a queue and processing them asynchronously.

> * Fully managed compute service that runs stateless code (Node.js, Java, C#, Go and Python) in response to an event or a time-based interval.
>
> * Allows you to run code w/o managing infrastructure like Amazon EC2 instances and Auto Scaling groups.

## Lambda Key Details

* Lambda is a compute service where you upload your code as a function and AWS provisions the necessary details underneath the function so that the function executes successfully.

* AWS Lambda is the ultimate abstraction layer. You only worry about code, AWS does everything else.

* Lambda supports Go, Python, C#, PowerShell, Node.js, and Java

* Each Lambda function maps to one request. Lambda scales horizontally automatically.

* Lambda is priced on the number of requests and the first one million are free. Each million afterwards is $0.20.

* Lambda is also priced on the runtime of your code, rounded up to the nearest 100mb, and the amount of memory your code allocates.

* Lambda works globally.

* Lambda functions can trigger other Lambda functions.

* You can use Lambda as an event-driven service that executes based on changes in your AWS ecosystem.

* You can also use Lambda as a handler in response to HTTP events via API calls over the AWS SDK or API Gateway.

![Fig. 01 Services that can invoke Lambda](../../../../img/SAA-CO2/serverless/lambda/diag01.png)

* When you create or update Lambda functions that use environment variables, AWS Lambda encrypts them using the AWS Key Management Service. When your Lambda function is invoked, those values are decrypted and made available to the Lambda code.

* The first time you create or update Lambda functions that use environment variables in a region, a default service key is created for you automatically within AWS KMS. This key is used to encrypt environment variables. However, if you wish to use encryption helpers and use KMS to encrypt environment variables after your Lambda function is created, you must create your own AWS KMS key and choose it instead of the default key.

* To enable your Lambda function to access resources inside a private VPC, you must provide additional VPC-specific configuration information that includes VPC subnet IDs and security group IDs. AWS Lambda uses this information to set up elastic network interfaces (ENIs) that enable your function to connect securely to other resources within a private VPC.

* AWS X-Ray allows you to debug your Lambda function in case of unexpected behavior.

## Learning Recap

1. You wish to deploy a microservices-based application w/o the operational overhead of managing infrastructure. This solution needs to accommodate rapid changes in the volume of requests. What do you do?

[ ] Run the microservices in containers using AWS Elastic Beanstalk

[x] Run the microservices in AWS Lambda behind an API Gateway

[ ] Run the microservices on Amazon EC2 instances in an Auto Scaling group

[ ] Run the microservices in containers using Amazon Elastic Container Service (Amazon ECS)

**Explanation**: "w/o... managing infrastructure" means staying away from EC2. AWS Elastic Beanstalk, EC2 instances in an Auto Scaling group, and AMZN Elastic Container Service all require dealing w/ EC2 services. AWS Lambda behind an API Gateway is serverless. AWS Elastic Beanstalk spins up a CloudFormation template which ultimately spins up a set of EC2 instances, which will require some maintenance, more than AWS Lambda behind an API Gateway.
