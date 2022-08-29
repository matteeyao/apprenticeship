# Scenario Introduction

> The organization you are working for, Scuba Syndrome, has decided to start moving to the cloud for all the benefits cloud computing offers. Scuba Syndrome particularly likes that AWS features a pay-as-you-go mode, as they are tired of the wasted expense of over-provisioned resources and are interested in a more resilient and elastic architectural design.
>
> As a solutions architect, we have been tasked to build out a new AWS environment for our organization w/ detailed requirements:
>
> 1. Create **two AWS accounts** to add into an AWS Organization.
>
> 2. **Three** AWS accounts will be needed (development, production, security).
>
> 3. Build a multi-tiered VPC w/ a **scalable, resilient design**. Includes EC2 instances, database instances, etc.
>
> 4. Create a **VPC peering** connection btwn 2 of our AWS accounts.
>
> 5. Create a **WordPress blog**.
>
> 6. Review possible **connectivity** options btwn our new AWS organization and our Scuba Syndrome on-premise data center.
>
> 7. Review **storage** options for the AWS environment along w/ data migration options that we could use to potentially move from our Scuba Syndrome on-premise data center into our new AWS environment.
>
> 8. Add detailed **logging** and **monitoring** to our environment so that costs and usage can be tracked.
>
> 9. Add security based on the **principle of least privilege**.
>
> 10. Use **cost-effective** options to ensure the AWS environment being designed and built is done w/ cost optimization.










Edge Locations are endpoints for AWS which are used for caching content. Typically this consists of CloudFront, Amazon's Content Delivery Network (CDN). There are many more Edge Locations than Regions. Currently there are over 150 Edge Locations.

AWS Global Infrastructure: **Compute**, **Storage**, **Databases**, **Network & Content Delivery**, **Migration & Transfer**, **Machine Learning**, **Management & Governance**, **Analytics**, **Security, Identity & Compliance**, **Desktop & App Streaming** are high-level services relevant to the solutions architect exam

However, **Compute**, **Storage**, **Databases**, **Network & Content Delivery**, **Security, Identity & Compliance** are the core services to pass the solutions architect exam

## Understand the difference between a region, an Availability Zone (AZ) and an Edge Location

* A region is a physical location in the world which consists of two or more Availability Zones (AZ's).

* An AZ is one or more discrete data centers, each with redundant power, networking and connectivity, housed in separate facilities.

* Edge Locations are endpoints for AWS which are used for caching content. Typically this consists of CloudFront, Amazon's Content Delivery Network (CDN)

Which statement best describes Availability Zones?

* Distinct locations from within an AWS region that are engineered to be isolated from failures. An Availability Zone (AZ) is a distinct location within an AWS Region. Each Region comprises at least two AZ.

The VPC service is a member of which group of AWS services in the "All services" view of the AWS Portal?

* Networking & Content Delivery. A Virtual Private Cloud (VPC) is a virtual network dedicated to a single AWS account. It is logically isolated from other virtual networks in the AWS cloud. VPC is found in the "Networking & Content Delivery" section of the AWS Portal.

AWS compute services: Amazon Elastic Compute Cloud (Amazon EC2) is a web service that provides resizable compute capacity in the cloud. It is designed to make web-scale computing easier for developers. AWS Lambda lets you run code w/o provisioning or managing servers. You pay only for the compute time you consume.

What does an AWS Region consist of?

* Each AWS Region consists of multiple, isolated, and physically separate Availability Zones within a geographic area. AWS has the concept of a Region, which is a physical location around the world where data centers are clustered. Each group of logical data centers is called an Availability Zone. Each AWS Region consists of multiple, isolated, and physically separate AZ's within a geographic area.

Which of the below are database services from AWS?

* Amazon RDS, DynamoDB.

* Amazon Relational Database Service (Amazon RDS) is a managed service that makes it easy to set up, operate, and scale a relational database in the cloud. Amazon RDS gives you access to the capabilities of a familiar MySQL, MariaDB, Oracle, SQL Server, or PostgreSQL database.

* DynamoDB enables customers to offload the administrative burdens of operating and scaling distributed databases to AWS so that they don't have to worry about hardware provisioning, setup and configuration, throughput capacity planning, replication, software patching, or cluster scaling.

Which of the below are storage services in AWS?

* EFS, S3

* S3 and EFS both provide the ability to store files in the cloud. EC2 provides compute, and is often augmented w/ other storage services. VPC is a networking service.

What is an AWS region?

* A region is a geographical area divided into Availability Zones. Each region contains at least two Availability Zones.

What is an Amazon VPC?

* VPC stands for Virtual Private Cloud

Which of the following are a part of AWS' Networking & Content Delivery services?

* VPC, CloudFront

* VPC is part of the "Networking & Content Delivery" services

* CloudFront is part of the "Networking & Content Delivery" services

## Cloud Architecture Best Practices

1. Design for failure and nothing fails

  * Avoid single points of failure

  * Multiple instances

  * Multiple Availability Zones

  * Separate single server into multiple tiered application where each server performs a single function, such as a separate web host and database server

  * For Amazon RDS, use Multi-AZ feature 

2. Build security in every layer

  * Encrypt Data at rest and in transit

  * Enforce principle of least privilege in IAM

  * Create a robust firewall by implementing both Security Groups and Network Access Control Lists (NACL)

  * Consider advanced security features and services (Amazon Inspector, Guard Duty, and AWS Shield)

3. Leverage different storage options

  * Move static web assets to Amazon S3

  * Use Amazon CloudFront to serve globally

  * Store session state in DynamoDB

  * Use ElastiCache between hosts and databases

4. Implement elasticity

  * Implement Auto Scaling policies

  * Architect resiliency to reboot and relaunch

  * Leverage managed services like Amazon S3 and Amazon DynamoDB

5. Think parallel

  * Scale horizontally, not vertically by adding more resources to a compute application rather than adding more power to compute resources

  * Decouple compute from session/state

  * Use Elastic Load Balancing

  * Right-size your infrastructure

6. Loose coupling sets you free

  * Instead of a single, ordered workflow, use multiple queues

  * Use Amazon Simple Queue Service and Simple Notification Service (SQS and SNS)

  * Leverage existing services

7. Don't fear constraints

  * Rethink traditional constraints

  * Need more RAM? Distribute load across a number of commodity instances

  * Better IOPS for databases?

    * Consider creating a Read Replica for your database and modifying your application to separate database reads from writes.

  * Response to failure?
