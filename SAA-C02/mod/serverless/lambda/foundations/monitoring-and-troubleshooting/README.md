# Monitoring and Troubleshooting

AWS Lambda integrates with other AWS services to help you monitor and troubleshoot your Lambda functions. This lesson describes how to use these AWS services to monitor, trace, debug, and troubleshoot your Lambda functions and applications.

## Types of monitoring graphs

AWS Lambda automatically monitors Lambda functions on your behalf and reports metrics through Amazon CloudWatch. To help you monitor your code when it runs, Lambda automatically tracks the following:

* Number of requests

* Invocation duration per request

* Number of requests that result in an error

Amazon CloudWatch provides built-in metrics to help monitor your Lambda functions.

> ### Invocations
>
> The number of times your function code is run, including successful runs and runs that result in a function error. If the invocation request is throttled or otherwise resulted in an invocation error, invocations aren't recorded.

> ### Duration
>
> The amount of time that your function code spends processing an event. The billed duration for an invocation is the value of Duration rounded up to the nearest millisecond.

> ### Errors
>
> The number of invocations that result in a function error. Function errors include exceptions thrown by your code and exceptions thrown by the Lambda runtime. The runtime returns errors for issues such as timeouts and configuration errors.

> ### Throttles
>
> The number of times that a process failed because of concurrency limits. When all function instances are processing requests and no concurrency is available to scale up, Lambda rejects additional requests.

> ### IteratorAge
>
> Pertains to event source mappings that read from streams. The age of the last record in the event. The age is the amount of time between when the stream receives the record and when the event source mapping sends the event to the function.

> ### DeadLetterErrors
>
> For asynchronous invocation, this is the number of times Lambda attempts to send an event to a dead-letter queue but fails. 

> ### ConcurrentExecutions
>
> The number of function instances that are processing events.
>
> You can also view metrics for the following:
>
> * **UnreservedConncurrentExections** – The number of events that are being processed by functions that don't have reserved concurrency.
>
> * **ProvisionedConcurrentExecutions** – The number of function instances that are processing events on provisioned concurrency. For each invocation of an alias or version with provisioned concurrency, Lambda emits the current count.

## Amazon CloudWatch Lambda Insights

Amazon CloudWatch Lambda Insights is a monitoring and troubleshooting solution for serverless applications running on Lambda. Lambda Insights collects, aggregates, and summarizes system-level metrics. It also summarizes diagnostic information such as cold starts and Lambda worker shutdowns to help you isolate issues with your Lambda functions and resolve them quickly.

Lambda Insights uses a new CloudWatch Lambda extension, which is provided as a Lambda layer. When you enable this extension on a Lambda function, it collects system-level metrics and emits a single performance log event for every invocation of that Lambda function. CloudWatch uses embedded metric formatting (EMF) to extract metrics from the log events.

### Lambda Insights dashboard

The Lambda Insights dashboard has two views in the CloudWatch console: the multi-function overview and the single-function view. The multi-function overview aggregates the runtime metrics for the Lambda functions in the current AWS account and Region. The single-function view shows the available runtime metrics for a single Lambda function.

You can use the Lambda Insights dashboard multi-function overview in the CloudWatch console to identify over- and under-utilized Lambda functions. You can use the Lambda Insights dashboard single-function view in the CloudWatch console to troubleshoot individual requests.

## Monitoring Lambda functions using AWS X-Ray

You can use AWS X-Ray to visualize the components of your application, identify performance bottlenecks, and troubleshoot requests that resulted in an error. Your Lambda functions send trace data to X-Ray, and X-Ray processes the data to generate a service map and searchable trace summaries.

> ![Fig. 1 AWS X-Ray](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig01.jpeg)
>
> AWS X-Ray processes the data to generate a service map and searchable trace summaries.

AWS X-Ray records how the Lambda functions are running.  

You can use X-Ray for:

* Tuning performance

* Identifying the call flow of Lambda functions and API calls

* Tracing path and timing of an invocation to locate bottlenecks and failures

## Example: Analyzing a cold start using AWS X-Ray

In the following example, an object is added to a designated S3 bucket, which triggers the Lambda function invocation. 

> ![Fig. 2 AWS X-Ray Trace](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig02.jpeg)
>
> The trace starts when Lambda is invoked. B/c Amazon S3 is an asynchronous event source, the invocation is queued and Amazon S3 doesn't wait for a response.

> ![Fig. 3 AWS X-Ray Dwell Time](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig03.jpeg)
>
> The Dwell time in the trace represents the time the invocation is queued. For the asynchronous invocation model, Lambda will try up to three times to invoke the function. In this case, it was successful on the first attempt (Attempt #1).

> ![Fig. 4 AWS X-Ray Hold in queue](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig04.jpeg)
>
> Lambda fetches the request from the queue and invokes the Lambda function itself.

> ![Fig. 5 AWS X-Ray Environment Initialization](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig05.jpeg)
>
> B/c the function has not been invoked in a while, no warm environment is available to run in. So Lambda starts the environment and initializes the environment.

> ![Fig. 6 AWS X-Ray Function Initialization](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig06.jpeg)
>
> When the environment is ready, the function itself must be initialized. Anything that's part of your function but outside of the handler is initialized and loaded into memory.

> ![Fig. 7 AWS X-Ray Handler Method](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig07.jpeg)
>
> After the function is initialized, the Handler method is called.

> ![Fig. 8 AWS X-Ray Function Invocation](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig08.jpeg)
>
> And your function is invoked.

> ![Fig. 9 AWS X-Ray Duration](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig09.jpeg)
>
> The entire duration is 876 ms.

> ![Fig. 10 AWS X-Ray Cold Start](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig10.jpeg)
>
> You can see how the cold start extends the overall duration. The duration of this cold start was about 200 ms to start the environment and 261 ms to initialize the function.

> ![Fig. 11 AWS X-Ray Billed Duration](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig11.jpeg)
>
> Only the function initialization and runtime are counted as part of the billed duration for this invocation. In this example, that would be 558 ms.

## Example: Analyzing a warm start using AWS X-Ray

> ![Fig. 12 AWS X-Ray Triggering event](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig12.jpeg)
>
> The triggering event invokes Lambda.

> ![Fig. 13 AWS X-Ray Hold in queue](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig13.jpeg)
>
> The request is again held in queue.

> ![Fig. 14 AWS X-Ray Lambda Invocation](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig14.jpeg)
>
> And then Lambda takes the request out of the queue and calls the Lambda function.

> ![Fig. 15 AWS X-Ray Thaw environment](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig15.jpeg)
>
> Lambda thaws the available environment.

> ![Fig. 16 AWS X-Ray Handler method](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig16.jpeg)
>
> No additional setup is required before the Handler method is run.

> ![Fig. 17 AWS X-Ray Function code runs](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig17.jpeg)
>
> And your function code runs.

> ![Fig. 18 AWS X-Ray Invocation duration](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig18.jpeg)
>
> In this example, the duration for this invocation was 199 ms.

> ![Fig. 19 AWS X-Ray Warm start](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig19.jpeg)
>
> B/c there was an available environment, this invocation had a warm start. No overhead was incurred for bootstrapping the environment or initializing the function.

> ![Fig. 20 AWS X-Ray Billed duration](../../../../../../img/SAA-CO2/serverless/lambda/foundations/monitoring-and-troubleshooting/fig20.jpeg)
>
> In this case of a warm start, the billed duration is equal to the time to run the code. In this example, the time is 82 ms.

## Additional monitoring and troubleshooting tools

In addition to Amazon CloudWatch metrics and AWS X-Ray, you can use other tools to monitor Lambda functions. To learn more, select each of the following tabs.

> ### AWS CloudTrail
>
> AWS CloudTrail helps audit your application by recording all the API actions made against the application. These logs can be exported to the analysis tool of your choice for additional analysis. 
>
> CloudTrail logging provides the following options:
>
>   * The default Lambda CloudTrail logging is for control plane (management) events.
>
>   * Optional logging: CloudTrail also logs data events. You can turn on data event logging so that you log an event every time Lambda functions are invoked.
>
> CloudTrail can be an important tool for auditing serverless deployments and rolling back unplanned deployments.

> ### Dead-Letter Queues
>
> Dead-letter queues help you capture application errors that must receive a response, such as an ecommerce application that processes orders. If an order fails, you cannot ignore that order error. You move that error into the dead-letter queue and manually look at the queue and fix the problems.
>
>   * Use dead-letter queues to analyze failures for follow-up or code corrections.
>
>   * Dead-letter queues are available for asynchronous and non-stream polling events.
>
>   * A dead-letter queue can be an Amazon Simple Notification Service (Amazon SNS) topic or an Amazon Simple Queue Service (Amazon SQS) queue.
>
> You can configure your dead-letter queue from the Lambda console. 

## Learning Summary

* Amazon CloudWatch ▶︎ Review metrics on invocations, errors, and throttling for a Lambda function.

* Dead-Letter queue ▶︎ Manually review errors for Lambda invocations that failed and must be addressed.

* AWS X-Ray ▶︎ Review trace details about an invocation to identify potential bottlenecks.

* AWS CloudTrail ▶︎ Audit actions made against your applications.

**Explanation**: Amazon CloudWatch metrics for invocations, errors, and throttling are visible on the Lambda console dashboard. Use a dead-letter queue to manually review invocations that failed. Use AWS X-Ray to analyze details about an invocation to look for bottlenecks, and use AWS CloudTrail to audit API calls made to your application.
