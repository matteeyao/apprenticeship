# CloudWatch

> Amazon CloudWatch is a monitoring service to monitor your AWS resources + the applications run on AWS. Three parts:
>
> * Metrics
>
> * Logs
>
> * Event (Event-based)

**CloudWatch** collects and manages our operational data inside our AWS account and operational data, data related to an AWS account. **CloudWatch** is responsible for detailing how an account performs, but also collects log data for our account.

![Fig. 1 CloudWatch overview](../../../../img/SAA-CO2/elastic-compute-cloud/cloudwatch/diagram-i.png)

**CloudWatch Logs** allow us to collect, monitor, and create actions based on log data.

> ### CloudWatch Logs
>
> * Monitor and troubleshoot systems and applications using existing log files
>
>   * Monitor logs for specific phrases, values, or patterns
>
> * Retrieve the associated log data from CloudWatch Logs
>
> * Agent for Ubuntu, Amazon Linux, and Windows

**CloudWatch Events** allow us to create an event to then perform an action when the event occurs.

## What can CloudWatch monitor?

CloudWatch monitors performance

> CloudWatch can monitor things like:
>
> * Compute
>
>    * EC2 Instances
>
>    * Autoscaling Groups
>
>    * Elastic Load Balancers
>
>    * Route53 Health Checks
>
> * Storage and Content Delivery
>
>    * EBS Volumes
>
>    * Storage Gateways
>
>    * CloudFront

Examples of metrics are CPU utilization, disk space usage, network-in and network-out, etc. CloudWatch is a public service that gathers metrics inside AWS, inside our on-premise environment, and monitors custom metrics as well, but we do have to distinguish between CloudWatch collecting metrics on AWS products and CloudWatch collecting metrics from our on-premise environment.

**Cloudwatch** collects data inside AWS automatically by default, but some metrics may require extra software, the **CloudWatch agent**, to be installed to collect metrics for EC2 instances or to collect metrics outside of AWS from our on-premise site or other cloud environments.

Some **CloudWatch** metrics are not collected by default, so CloudWatch metrics need a CloudWatch agent to be installed in order to collect those metrics.

We can use the data that **CloudWatch** collects and **Alarms** to take actions like sending notifications, scaling up the compute environment when the load increases, etc.

![Fig. 2 CloudWatch dimension](../../../../img/SAA-CO2/elastic-compute-cloud/cloudwatch/diagram-ii.png)

## CloudWatch and EC2

Host Level Metrics consist of:

* CPU

* Network

* Disk

* Status Check

    * Able to monitor the hypervisor and underlying EC2 instance

## What is AWS CloudTrail

AWS **CloudTrail** increases visibility into your user and resource activity by recording AWS Management Console actions and API calls. You can identify which users and accounts called AWS, the source IP address from which the calls were made, and when the calls occurred.

![Fig. 3 CloudTrail](../../../../img/SAA-CO2/elastic-compute-cloud/cloudwatch/diagram-iii.png)

**CloudTrail** logs all API actions in an AWS account. All activity within an AWS account is logged by **CloudTrail** as a **CloudTrail** event. A **CloudTrail** event is an AWS activity taken inside our AWS account and the actions could be taken by any user, role, or service. By default, **CloudTrail** stores the last 90 days of events in the Event History.

> There are **two types** of events:
>
> * Management events
>
> * Data events

By default, CloudTrail only logs **Management Events** (e.g. creating an EC2 instance, creating a VPC). **Management Events** provide information about management operations.

**Data Events** are more about the resource operations in a resource. Examples of this are **Lambda** functions, objects uploaded to **S3**. **CloudWatch** only logs **Management Events** by default since **Data Events** occur frequently.

CloudTrail **Trails** only logs events for the AWS region that the **Trail** was created in. Thus **CloudTrail** is a regional service, but we can choose to allow **CloudTrail** to log for all regions in our AWS account.

## CloudTrail vs CloudWatch

> * CloudWatch monitors performance
>
> * CloudTrail monitors API calls in the AWS platform

* So **CloudTrail** will inform you of who provisioned an EC2 instance or who setup an S3 bucket

## Learning summary

> * CloudWatch is used for monitoring performance
>
> * CloudWatch can monitor most of AWS as well as your applications that run on AWS
>
> * CloudWatch w/ EC2 will monitor events every 5 minutes by default
>
> * You can have 1 minute intervals by turning on detailed monitoring
>
> * You can create CloudWatch alarms which trigger notifications
>
> * CloudWatch is all about performance. CloudTrail is all about auditing
>
>   * CloudTrail will tell you who setup an S3 bucket or provisioned an EC2 instance, not CloudWatch
>
>   * CloudWatch will deal w/ monitoring performance (network throughput, disk IO, CPU utilization) of your EC2 instance

* Standard Monitoring = 5 minutes

* Detailed Monitoring = 1 minute

> * What can I do w/ CloudWatch?
>
>   * Dashboards → Creates awesome dashboards to see what is happening w/ your AWS environment
>
>   * Alarms → Allows you to set Alarms that notify you when particular thresholds are hit
>
>   * Events → CloudWatch Events help you to respond to state changes in your AWS resources
>
>   * Logs → CloudWatch logs help you to aggregate, monitor, and store logs
