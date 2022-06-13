# High Availability (HA) Architecture

## HA sample exam question

> **Scenario: You have a website that requires a minimum of 6 instances and it must be highly available. You must also be able to tolerate the failure of 1 Availability Zone. What is the ideal architecture for this environment while also being the most cost effective?
>
> * 2 Availability Zones w/ 2 instances in each AZ.
>
> * 3 Availability Zones w/ 3 instances in each AZ.
>
> * 1 Availability Zone w/ 6 instances in each AZ.
>
> * 3 Availability Zones w/ 2 instances in each AZ.

**3 Availability Zones w/ 3 instances in each AZ**. If any of the Availability Zones go down, then we will not have sufficient instances to cover the minimum requirement of 6 instances.

## HA Architecture exam tips

> * Use Multiple AZ's and Multiple Regions where ever you can.
>
> * Know the difference between Multi-AZ and Read Replicas for RDS.
>
> * Know the difference between scaling out and scaling up.
>
> * Know the different S3 storage classes.

Multi-AZ is for disaster recovery. Read Replicas is for performance.

Scaling out is where we use Auto Scaling groups and we add additional EC2 instances. Scaling up is where we increase the resources inside our EC2 instances, so we might go from a `t2.micro` to a `6X extra large` - occasions where we'd increase the amount of RAM or CPU

Standard S3 and Standard S3 Infrequently Access are still highly available. Reduced Redundancy Storage or S3 Single AZ are not highly available.

> **3 Different Types of Load Balancers**:
>
> * Application Load Balancers
>
> * Network Load Balancers
>
> * Classic Load Balancers

> * 504 Error means the gateway has timed out. This means that the application is not responding within the idle timeout period.
>
> * Troubleshoot the application. Is it the Web Server or Database Server?

> * If you need the IPv4 address of your end user, look for the **X-Forwarded-For** header.

> * Instances monitored by ELB are reported as **InService** or **OutofService**
>
> * Health Checks check the instance health by talking to it.
>
> * Load Balancers have their own DNS name. You are never given an IP address.
>
> * Read the ELB FAQ for all the Load Balancers, inc. Classic Load Balancers.
