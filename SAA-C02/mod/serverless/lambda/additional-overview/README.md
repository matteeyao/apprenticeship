# Lambda

Using this section, we'll create a serverless website using API Gateway and Lambda as well as create an Alexa skill.

Only pay for the execution time of your code.

> **What is Lambda?**
>
> Lambda is the ultimate abstraction layer;
>
> * Data Centers
>
> * Hardware
>
> * Assembly Code/Protocols
>
> * High Level Languages
>
> * Operating Systems
>
> * Application Layer/AWS APIs
>
> * AWS Lambda

> AWS Lambda is a compute service where you can upload your code and create a Lambda function. AWS Lambda takes care of provisioning and managing the servers that you use to run the code. You don't have to worry about operating systems, patching, scaling, etc.

> **You can use Lambda in the following ways**:
>
> * As an event-driven compute service where AWS Lambda runs your code in response to events. These events could be changes to data in an Amazon S3 bucket or an Amazon DynamoDB table.
>
> * As a compute service to run your code in response to HTTP requests using API Gateway or API calls made using AWS SDKs. This is what we use at A Cloud Guru.

Those event-driven compute services are called triggers.

Lambda is different from Auto-Scaling in that, when a user goes in, you might just spread it across two webservers. If you have a million users hitting your API Gateway at once, that will then go on, and trigger a million different Lambda functions. That's how well Lambda scales.

## Traditional vs. Serverless Architecture

W/ Traditional architecture, you're still relying on virtual machines, Operating Systems.

Lambda scales w/ the number of requests sent.

Remember that you'll always have API Gateway at the front end to serve the request. You'll have Lambda functions that scale out automatically, and then you're going to have a database that's serverless, either DynamoDB, or Aurora Serverless.

## Lambda the basics

> **What languages does Lambda support?**
>
> * Node.js
>
> * Java
>
> * Python
>
> * C#
>
> * Go
>
> * PowerShell

* Continuous scaling

## Lambda Pricing

> **How is Lambda Priced**
>
> 1. **Number of Requests**
>
>   First 1 million requests are free. $0.20 per 1 million requests thereafter.
>
> 2. **Duration**
>
>   Duration is calculated from the time your code begins executing until it returns or otherwise terminates, rounded up to the nearest 100ms. The price depends on the amount of memory you allocate in your function. You are charged $0.00001667 for every GB-second used.

## Learning summary

> * Lambda scales out (not up) automatically
>
> * Lambda functions are independent, 1 event = 1 function
>
> * Lambda is serverless
>
> * Know what services are serverless
>
> * Lambda functions can trigger other Lambda functions, 1 event can = x functions if functions trigger other functions
>
> * Architectures can get extremely complicated, AWS X-ray allows you to debug what is happening
>
> * Lambda can do things globally, you can use it to back up S3 buckets to other S3 buckets etc
>
> * Know your triggers

RDS is not serverless. There is still an Operating System that needs to be accessed and patched for RDS. Aurora Serverless is the only RDS service that is serverless. DynamoDB, S3, Lambda, API Gateway → all serverless technologies. EC2 is not serverless, it's a virtual machine.

AWS X-ray allows you to debug your serverless applications.
