# CloudWatch

Amazon CloudWatch is a monitoring service to monitor your AWS resources, as well as the applications that you run on AWS

## What can CloudWatch monitor?

CloudWatch monitors performance

CloudWatch can monitor things like:

* Compute

    * EC2 Instances

    * Autoscaling Groups

    * Elastic Load Balancers

    * Route53 Health Checks

* Storage and Content Delivery

    * EBS Volumes

    * Storage Gateways

    * CloudFront

## CloudWatch and EC2

Host Level Metrics consist of:

* CPU

* Network

* Disk

* Status Check

    * Able to monitor the hypervisor and underlying EC2 instance

## What is AWS CloudTrail

AWS CloudTrail increases visibility into your user and resource activity by recording AWS Management Console actions and API calls. You can identify which users and accounts called AWS, the source IP address from which the calls were made, and when the calls occurred.

## CloudTrail vs CloudWatch

> * CloudWatch monitors performance
>
> * CloudTrail monitors API calls in the AWS platform

* So CloudTrail will inform you of who provisioned an EC2 instance or who setup an S3 bucket

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
