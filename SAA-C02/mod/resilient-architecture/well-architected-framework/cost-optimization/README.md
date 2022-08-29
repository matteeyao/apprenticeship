# V. Cost Optimization Pillar

> *Ability to run systems to deliver business value at the lowest price point*

The Cost Optimization pillar includes the ability to run systems to deliver business value at the lowest price point.

## Fundamental Pricing Characteristics

Three fundamental characteristics you pay fro w/ AWS:

* Compute

* Storage

* Data transfer

## Cost-Effective Services

> * AWS Budgets
>
> * AWS Cost and Usage Report
>
> * AWS Cost Explorer
>
> * Reserved Instance Reporting

> We can optimize over time using **Trusted Advisor**, **Cost and Usage Report**, along w/ **Cost Explorer**

## Cost-Effective Resources

> * Spot Instance / Reserved Instance
>
> * AMZN Simple Storage Service
>
> * AMZN S3 / S3 Glacier
>
> * Lambda
>
> * Auto Scaling

## Design Principles

There are five design principles for cost optimization in the cloud:

* Practice cloud financial mgmt

  * Use AWS-managed services to reduce your overall cost of ownership b/c AWS can offer us a lower cost due to economies of scale

* Adopt a consumption model

  * Pay for what you consume

* Measure overall efficiency

  * Define and track your metrics and goals

* Stop spending money on undifferentiated heavy lifting

  * Stop spending money on data centers and data center operations

  * Transition to serverless services

* Analyze and attribute expenditure

  * Track your costs and usage w/ the use of Tags, Budgets, Cost and Usage Reports, Cost Explorer, Reserved Instances

> **AWS Cost Optimization Designs**
>
> For the exam, **be aware** of the AWS Well-Architected Framework and cost-optimized design best practices.

> 1. **Right-sizing instances**
>
>   * Right-sizing means picking the **correct instances for our current resources** as well as resources we plan to use. So maybe we are using a larger EC2 instance size when all we need to cover our demand is a small instance size. We can save money by right-sizing and choosing the correct instance type, but also the cheapest instance type that meets our performance requirements.
>
>   * Right-sizing is the process of reviewing deployed resources and seeking opportunities to downsize when possible. For example, if an application instance is consistently under-utilizing its RAM and CPU, switching that to a smaller instance can offer significant savings while maintaining the same performance.

> 2. **Increasing application elasticity**
>
>   * Increasing elasticity means using Auto Scaling to only use resources when those resources are **needed**, and not using resources when they are not needed. This gives us a **pay-for-what-we-use model**.
>
>   * **Auto Scaling** + **Horizontal Scaling**
>
>     * Sometimes we can use smaller instances vs. fewer larger instances for our workload. This can reduce your cost. We can use Auto-Scaling to scale up our instances when that demand scales, and then scale back down when the demand lessens. Auto Scaling can also schedule instances for periods of demand. Usually your production instances will need to be up 24/7, but what about your test or dev environment? We can use Auto Scaling to scale these instances down when they're not being used or during non-business hours. AWS provides a tool called the AWS Instance Scheduler, and this can create custom start and stop schedules for your instances. AWS provides us w/ a CloudFormation template managed by tags - another reason to add a tagging strategy to your environment.

> 3. **Choosing the right pricing model**
>
>   * AWS offers several different pricing models: reserved instances, on-demand instances, and spot instances.
>
>     * **Reserved Instances** come w/ a one or three-year commitment. But w/ that commitment, you receive a lower price
>
>     * **Spot Instances** tailor to flexible start and end times
>
>     * AWS also offers a service called **Trusted Advisor**, which monitors our infrastructure and recommends solutions to make out infrastructure more optimized
>
>     * **Cost Explorer** monitors those costs
>
>   * Comes into play after we have right-sized our instances and set up Auto Scaling so that we can ensure to meet our demand, but also scale back down when that demand is no longer needed.
>
>   * Another example is using **Reserved Instances** for workloads that need to run most or all the time, such as production environments. This can have a significant impact on savings compared to on-demand; in some cases, up to 75 percent.

> 4. **Matching storage to usage / Optimizing storage**
>
>   * Reducing our storage can save money b/c we can match our storage usage to a certain storage class. AWS offers **multiple storage classes**, and we need to make sure we pick the correct one.
>
>     * AMZN Simple Storage Service (S3)
>
>     * Amazon Elastic Block Store
>
>     * AWS Storage Gateway
>
>     * AMZN EFS
>
>     * Data Transfer Options: AWS Snowball Edge, AWS Snowball, AWS Snowmobile
>
>   * Another example is the S3 Intelligent-Tiering storage class, which is designed to optimize costs by automatically moving data to the most cost-effective storage tier.

> 5. **Measure, Monitor, and Improve**
>
>   * The infrastructure will most likely change, so we need measures in place to monitor and track our usage and costs.
>
>   * We can monitor our utilization of CPU, RAM, storage, etc. to identify instances that could be downsized or actually might need to be increased in size. AWS provides *CloudWatch* to track our metrics. *CloudWatch* allows us to set alarms so that we can immediately take actions to anything that happens in our environment. We also have *Trusted Advisor* along w/ the *AWS Well-Architected Framework* tool and *Cost Explorer*, but it's crucial to define your metric, set target goals, define and enforce your tagging strategy, use *Cost Allocation Tags*, and make sure that you are regularly reviewing any infrastructure changes.

## Best Practices

As w/ the other pillars, there are trade-offs to consider. For example, do you want to optimize for speed to market or for cost? In some cases, it's best to optimize for speed-going to market quickly, shipping new features, or simply meeting a deadline-rather than investing in up-front cost optimization.

Design decisions are sometimes directed by haste rather than data, and as the temptation always exists to overcompensate rather than spend time benchmarking for the most cost-optimal deployment. This might lead to over-provisioned and under-optimized deployments.

Using the appropriate services, resources, and configurations for your workloads is key to cost savings.

## Learning Summary

* If you know it's going to be on, reserve it.

* Any unused CPU time is a waste of money.

* Use the most cost-effective data storage service and class.

* Determine the most cost-effective EC2 pricing model and instance type for each workload.
