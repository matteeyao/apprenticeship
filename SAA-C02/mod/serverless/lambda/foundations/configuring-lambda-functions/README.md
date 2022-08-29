# Configuring Your Lambda Functions

When building and testing a function, you must specify three primary configuration settings: memory, timeout, and concurrency. These settings are important in defining how each function performs. Deciding how to configure memory, timeout, and concurrency comes down to testing your function in real-world scenarios and against peak volume. As you monitor your functions, you must adjust the settings to optimize costs and ensure the desired customer experience with your application.

> ![Fig. 1 Memory, Timeout, Concurrency](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig01.png)
>
> Memory, timeout, and concurrency are the three main settings that determine how your function performs.

First, you'll review the importance of configuring your memory and timeout values. Then you'll review the billing considerations for memory and timeout values before you examine concurrency and how to optimize for it.

## Memory

> ![Fig. 2 Memory](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig01.png)
>
> Lambda allocates CPU and other resources linearly in proportion to the amount of memory configured.

You can allocate up to 10 GB of memory to a Lambda function. Lambda allocates CPU and other resources linearly in proportion to the amount of memory configured. Any increase in memory size triggers an equivalent increase in CPU available to your function. To find the right memory configuration for your functions, use the **AWS Lambda Power Tuning tool**.

Because Lambda charges are proportional to the memory configured and function duration (GB-seconds), the additional costs for using more memory may be offset by lower duration.

## Timeout

> ![Fig. 3 Timeout](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig03.png)
>
> The maximum timeout for a Lambda function is 900 seconds.

The AWS Lambda timeout value dictates how long a function can run before Lambda terminates the Lambda function. At the time of this publication, the maximum timeout for a Lambda function is 900 seconds. This limit means that a single invocation of a Lambda function cannot run longer than 900 seconds (which is 15 minutes). 

Set the timeout for a Lambda function to the maximum only after you test your function. There are many cases when an application should fail fast and not wait for the full timeout value. 

It is important to analyze how long your function runs. When you analyze the duration, you can better determine any problems that might increase the invocation of the function beyond your expected length. Load testing your Lambda function is the best way to determine the optimum timeout value.

Your Lambda function is billed based on runtime in 1-ms increments. Avoiding lengthy timeouts for functions can prevent you from being billed while a function is simply waiting to time out.

## Lambda billing costs

With AWS Lambda, you pay only for what you use. You are charged based on the number of requests for your functions and the duration, the time it takes for your code to run. Lambda counts a request each time it starts running in response to an event notification or an invoke call, including test invokes from the console.

Duration is calculated from the time your code begins running until it returns or otherwise terminates, rounded up to the nearest 1 ms. Price depends on the amount of memory you *allocate* to your function, not the amount of memory your function uses. If you allocate 10 GB to a function and the function only uses 2 GB, you are charged for the 10 GB. This is another reason to test your functions using different memory allocations to determine which is the most beneficial for the function and your budget.

In the AWS Lambda resource model, you can choose the amount of memory you want for your function and are allocated proportional CPU power and other resources. An increase in memory triggers an equivalent increase in CPU available to your function. The AWS Lambda Free Tier includes 1 million free requests per month and 400,000 GB-seconds of compute time per month.

## The balance between power and duration

Depending on the function, you might find that the higher memory level might actually cost less because the function can complete much more quickly than at a lower memory configuration.

You can use an open-source tool called Lambda Power Tuning to find the best configuration for a function. The tool helps you to visualize and fine-tune the memory and power configurations of Lambda functions. The tool runs in your own AWS account—powered by AWS Step Functions—and supports three optimization strategies: cost, speed, and balanced. It's language-agnostic so that you can optimize any Lambda functions in any of your languages. 

For deployment information and specifications on Lambda Power Tuning, see [aws-lambda-power-tuning](https://serverlessrepo.aws.amazon.com/applications/arn:aws:serverlessrepo:us-east-1:451282441545:applications~aws-lambda-power-tuning). This resource provides detailed instructions and explanations on running the tool.

> ![Fig. 4 Lambda Power Tuning](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig04.jpeg)
>
> Lambda Power Tuning provides a comparison of cost vs. speed of your Lambda function.

## Concurrency and scaling

> ![Fig. 5 Concurrency](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig05.jpeg)
>
> The number of AWS Lambda function invocations running at a single time

Concurrency is the third major configuration that affects your function's performance and its ability to scale on demand. Concurrency is the number of invocations your function runs at any given moment. When your function is invoked, Lambda launches an instance of the function to process the event. When the function code finishes running, it can handle another request. If the function is invoked again while the first request is still being processed, another instance is allocated. Having more than one invocation running at the same time is the function's concurrency.

### Concurrent invocations

As an analogy, you can think of concurrency as the total capacity of a restaurant for serving a certain number of diners at one time. If you have seats in the restaurant for 100 diners, only 100 people can sit at the same time. Anyone who comes while the restaurant is full must wait for a current diner to leave before a seat is available. If you use a reservation system, and a dinner party has called to reserve 20 seats, only 80 of those 100 seats are available for people without a reservation. Lambda functions also have a concurrency limit and a reservation system that can be used to set aside runtime for specific instances.

> ![Fig. 6 Concurrency Analogy](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig06.jpeg)
>
> Concurrency can be compared to the seating limit in a restaurant. If there are 100 total seats and 40 are in use and 20 are reserved, only 40 seats are available (40+20+40=100 seats) for people arriving without a reservation.

## Concurrency types

> ### Unreserved concurrency
>
> The amount of concurrency that is not allocated to any specific set of functions. The minimum is 100 unreserved concurrency. This allows functions that do not have any provisioned concurrency to still be able to run. If you provision all your concurrency to one or two functions, no concurrency is left for any other function. Having at least 100 available allows all your functions to run when they are invoked.

> ### Reserved concurrency
>
> Guarantees the maximum number of concurrent instances for the function. When a function has reserved concurrency, no other function can use that concurrency. No charge is incurred for configuring reserved concurrency for a function.

> ### Provisioned concurrency
>
> Initializes a requested number of runtime environments so that they are prepared to respond immediately to your function's invocations. This option is used when you need high performance and low latency. 
>
> You pay for the amount of provisioned concurrency that you configure and for the period of time that you have it configured. 
>
> For example, you might want to increase provisioned concurrency when you are expecting a significant increase in traffic. To avoid paying for unnecessary warm environments, you scale back down when the event is over.

## Concurrency examples and limits

> ![Fig. 7 Total concurrent functions](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig07.jpeg)
>
> Concurrency is subject to a Regional quota of 1000 but can be increased to tens of thousands through a support ticket.

> ![Fig. 8 Concurrency Reserved Pools](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig08.jpeg)
>
> You can reserve pools of concurrency for specific functions.

> ![Fig. 9 Concurrency Unreserved Pool](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig09.jpeg)
>
> The unresolved pool is shared by all functions. The unreserved pool is the total limit (1000) minus any reservations (150+300=450). Unreserved concurrency is 550 (1000-450=550). In the absence of any reserved pools, the entire default pool is unreserved.

> ![Fig. 10 Concurrency Unreserved Pool Cap](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig10.jpeg)
>
> AWS Lambda keeps the unreserved concurrency pool at a minimum of 100 concurrent runs so that functions that do not have a reservation can continue to process requests.

### Reasons for setting concurrency limits

> ### Limit Concurrency
>
> Limit a function’s concurrency to achieve the following:
>
> * Limit costs
>
> * Regulate how long it takes you to process a batch of events
>
> * Match it with a downstream resource that cannot scale as quickly as Lambda

> ### Reserve Concurrency
>
> Reserve function concurrency to achieve the following: 
>
> * Ensure that you can handle peak expected volume for a critical function 
>
> * Address invocation errors

## How concurrency bursts are managed

A burst is when there is a sudden increase in the number of instances needed to fulfill the requested number of running functions. An example is an increase in orders on a website during a limited time sale. The burst concurrency quota is not per function. It applies to all of your functions in the Region. 

The burst quotas differ by region:

* 3000 – US West (Oregon), US East (N. Virginia), Europe (Ireland)

* 1000 – Asia Pacific (Tokyo), Europe (Frankfurt), US East (Ohio)

* 500 – Other Regions

After the initial burst, your functions' concurrency can scale by an additional 500 instances each minute. This continues until there are enough instances to serve all requests, or until a concurrency limit is reached.

The following set of images explains how bursting works with Lambda functions.

> ![Fig. 11 Concurrency Limits](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig11.jpeg)
>
> Bursts are handled based on a combination of your limits and a predetermined **Immediate Concurrency Increase** amount that is dependent on the Region where your Lambda function is running. In this example, the account limit is set for 5,000 concurrent invocations in a Region w/ an immediate concurrency increase of 3,000.

> ![Fig. 12 Concurrency Burst](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig12.jpeg)
>
> In this example, if the concurrent invocations burst from 100 to 4,000, Lambda will respond by adding the Immediate Concurrency Increase of 3,000 to the 100 concurrent invocations that were running before the burst.

> ![Fig. 13 More Concurrent Invocations added from Concurrency Burst](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig13.jpeg)
>
> After one minute, Lambda evaluates whether more are needed. If the account limit or the burst level has not been reached, Lambda adds 500 additional concurrent invocations. In this example, 500 more concurrent invocations are added.

> ![Fig. 14 More Concurrent Invocations added from Concurrency Burst](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig14.jpeg)
>
> After a second minute, another 500 concurrent invocations are added. After another minute, Lambda evaluates that there are enough concurrent invocations available to handle the burst, and the evaluate/add cycle ends.

> ![Fig. 15 Concurrency Invocation Throttling due to Limit](../../../../../../img/SAA-CO2/serverless/lambda/foundations/configuring-lambda-functions/fig15.jpeg)
>
> If you consider the same burst for a function that has concurrency limit set for 1,000, Lambda would immediately increase the concurrent invocations by 1,000. By having reached the function limit, Lambda would start throttling invocations to keep the concurrent invocations no higher than 1,000.

## CloudWatch metrics for concurrency

When your function finishes processing an event, Lambda sends metrics about the invocation to Amazon CloudWatch. You can build graphs and dashboards with these metrics in the CloudWatch console. You can also set alarms to respond to changes in use, performance, or error rates.

CloudWatch includes two built-in metrics that help determine concurrency: `ConcurrentExecutions` and `UnreservedConcurrentExecutions`.

> ### ConcurrentExecutions
>
> Shows the sum of concurrent invocations for a given function at a given point in time. Provides historical data on how functions are performing. 
>
> You can view all functions in the account or only the functions that have a custom concurrency limit specified.

> ### UnreservedConcurrentExecutions
>
> Shows the sum of the concurrency for the functions that do not have a custom concurrency limit specified.

## Testing concurrency

The most important factor for your concurrency, memory, and timeout settings is to verify application testing against real-world conditions. To do this, follow these suggestions:

* Run performance tests that simulate peak levels of invocations.

  * View the metrics for the amount of throttling that occurs during performance peaks.

* Determine whether the existing backend can handle the speed of requests sent to it.

  * Don't test in isolation. If you’re connecting to Amazon Relational Database Service (Amazon RDS), ensure that you test that the concurrency levels for your function can be processed by the database.

* Does your error handling work as expected? 

  * Tests should include pushing the application beyond the concurrency settings to verify correct error handling.

## Learning summary

1. A developer has been asked to troubleshoot a Lambda function that is in production. They've been told that it runs for 5 minutes and has been asked to reduce its duration to save on billable costs. Which actions should the developer take? (Select THREE.)

[ ] Test the function from the console once to confirm that it is taking 5 minutes.

[x] Confirm whether 5 minutes is the typical duration through production monitoring.

[ ] Decrease the timeout to 4 minutes.

[x] Test at higher memory configurations and compare the duration and cost at each configuration.

[x] Check whether any unnecessary SDK components are in the deployment package.

**Explanation**: A developer must make sure that a 5-minute duration isn't reflective of a single invocation. Instead of running it once from the console, they need to examine how it actually runs in production. 

Decreasing the timeout would save costs, but it would probably mean that the function would frequently fail to complete. A best practice is to experiment with different memory configurations and estimate whether a higher memory configuration would actually be less expensive. They can also determine whether there are unnecessary components in the function itself that could be removed to speed up its initialization.

2. What are some reasons a developer would set a concurrency limit (or reserve) on a function? (Select THREE.)

[x] Manage costs

[x] Regulate how long it takes to process a batch of events

[x] Match the limit w/ a downstream resource

[ ] help CloudWatch track logging events

[ ] Ensure that Amazon Simple Queue Service (Amazon SQS) queues are cleared efficiently

[ ] Limit the memory that is used
