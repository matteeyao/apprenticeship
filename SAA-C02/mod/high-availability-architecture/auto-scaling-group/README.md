# Auto Scaling

![Fig. 1 AWS Auto Scaling](../../../../img/SAA-CO2/high-availability-architecture/auto-scaling-group/diag01.png)

> * Automatically adjusts resource capacity.
>
> * Define where Amazon EC2 Auto Scaling deploys resources.
>
> * Specify the Amazon BPC and subnets.

> **Auto Scaling has 3 components**
>
> 1. **Groups**
>
> Logical Component. Webserver group or Application group or Database group etc.
>
> 2. **Configuration Templates**
>
> Groups uses a launch template or a launch configuration as a configuration template for its EC2 instances. You can specify information such as the AMI ID, instance type, key pair, security groups, and block device mapping for your instances.
>
> 3. **Scaling Options**
>
> Scaling Options provide several ways for you to scale your Auto Scaling groups. For example, you can configure a group to scale based on the occurrence of specified conditions (dynamic scaling) or on a schedule.

> **What are my scaling options?**
>
> * Maintain current instance levels at all times
>
> * Scale manually
>
> * Scale based on a schedule
>
> * Scale based on demand
>
> * Use predictive scaling

## Unified Scaling

![Fig. 2 Unified Scaling](../../../../img/SAA-CO2/high-availability-architecture/auto-scaling-group/diag02.png)

## Automatic Resource Discovery

* AWS Auto Scaling automatically scans all resources used by your application.

* Discovers and lists which resources are scalable.

* No need to manually identify resources through individual interfaces.

## Different Scaling Options

> **Maintain current instance levels at all times**
>
> You can configure your Auto Scaling group to maintain a specified number of running instances at all times.
>
> To maintain the current instance levels, Amazon EC2 Auto Scaling performs a periodic health check on running instances within an Auto Scaling group.
>
> When Amazon EC2 Auto Scaling finds an unhealthy instance, it terminates that instance and launches a new one.

> **Scale manually**
>
> Manual scaling is the most basic way to scale your resources, where you specify only the change in the maximum, minimum, or desired capacity of your Auto Scaling group.
>
> Amazon EC2 Auto Scaling manages the process of creating or terminating instances to maintain the updated capacity.

> **Scale based on a schedule**
>
> Scaling by schedule means that scaling actions are performed automatically as a function of time and date.
>
> This is useful when you know exactly when to increase or decrease the number of instances in your group, simply b/c the need arises on a predictable schedule.

> **Scale based on demand**
>
> A more advanced way to scale your resources - using scaling policies - lets you define parameters that control the scaling process.
>
> For example, let's say that you have a web application that currently runs on two instances and you want the CPU utilization of the Auto Scaling group to stay at around 50 percent when the load on the application changes. This method is useful for scaling in response to changing conditions, when you don't know when those conditions will change. You can set up Amazon EC2 Auto Scaling to respond for you. We will do this in the next lab.

> **Use predictive scaling**
>
> You can also use Amazon EC2 Auto Scaling in combination w/ AWS Auto Scaling to scale resources across multiple services.
>
> AWS Auto Scaling can help you maintain optimal availability and performance by combining predictive scaling and dynamic scaling (proactive and reactive approaches, respectively) to scale your Amazon EC2 capacity faster.

## Build-in Scaling Strategies

✓ Optimize for Availability ▶︎ Low resource utilization target

✓ Balance Availability and Cost ▶︎ Moderate resource utilization target

✓ Optimize for Cost ▶︎ High resource utilization target

## Considerations

When should you consider using AWS Auto Scaling?

* If you have applications that use one or more scalable resources, and have a variable load

* If you want more guidance on defining your application scaling plan

* If you want to maintain the health of your EC2 fleet, scale individual resources separately, create scheduled scaling actions, or setup step-scaling policies, you can use Amazon EC2 auto-scaling or application auto-scaling

## Learning summary

> **What are my scaling options?**
>
> * Maintain current instance levels at all times
>
> * Scale manually
>
> * Scale based on a schedule
>
> * Scale based on demand
>
> * Use predictive scaling

### Key Takeaways

✓ Monitor your applications continually and automatically adjust capacity.

✓ Set up scaling efficiently through automatic resource discovery via a single unified interface.

✓ Make smart decisions regarding application high availability w/ build-in scaling strategies and predictable scaling.

✓ Maintain application performance automatically w/ smart scaling policies.

### Recap Question

1. Which of the following are characteristics of the Auto Scaling service on AWS? (Select three)

[ ] Sends traffic to healthy instances `(Elastic Load Balancer)`

[x] Responds to changing conditions by adding or terminating Amazon EC2 instances

[ ] Collects and tracks metrics and sets alarms `(CloudWatch)`

[ ] Delivers push notifications `(Amazon SNS)`

[x] Launches instances from a specified Amazon Machine Image (AMI)

[x] Enforces a minimum number of running Amazon EC2 instances
