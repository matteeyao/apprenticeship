# Reserved Instance

1. A company conducted a surprise IT audit on all of the AWS resources being used in the production environment. During the audit activities, it was noted that you are using a combination of Standard and Convertible Reserved EC2 instances in your applications.

Which of the following are the characteristics and benefits of using these two types of Reserved EC2 instances? (Select TWO.)

[ ] Unused Convertible Reserved Instances can later be sold at the Reserved Instance Marketplace.

[ ] It runs in a VPC on hardware that's dedicated to a single customer.

[ ] It can enable you to reserve capacity for your Amazon EC2 instances in multiple Availability Zones and multiple AWS Regions for any duration.

[x] Convertible Reserved Instances allow you to exchange for another convertible reserved instance of a different instance family.

[x] Unused Standard Reserved Instances can later be sold at the Reserved Instance Marketplace.

**Explanation**: **Reserved Instances (RIs)** provide you with a significant discount (up to 75%) compared to On-Demand instance pricing. You have the flexibility to change families, OS types, and tenancies while benefiting from RI pricing when you use Convertible RIs. One important thing to remember here is that Reserved Instances are not physical instances, but rather a billing discount applied to the use of On-Demand Instances in your account.

The offering class of a Reserved Instance is either Standard or Convertible. A **Standard Reserved Instance** provides a more significant discount than a **Convertible Reserved Instance**, but you can't exchange a Standard Reserved Instance unlike Convertible Reserved Instances. You can modify Standard and Convertible Reserved Instances. Take note that in Convertible Reserved Instances, you are allowed to exchange another Convertible Reserved instance with a different instance type and tenancy.

The configuration of a Reserved Instance comprises a single instance type, platform, scope, and tenancy over a term. If your computing needs change, you might be able to modify or exchange your Reserved Instance.

When your computing needs change, you can modify your Standard or Convertible Reserved Instances and continue to take advantage of the billing benefit. You can modify the Availability Zone, scope, network platform, or instance size (within the same instance type) of your Reserved Instance. You can also sell your unused instance for Standard RIs but not Convertible RIs on the Reserved Instance Marketplace.

> The option that says: **Unused Convertible Reserved Instances can later be sold at the Reserved Instance Marketplace** is incorrect. This is not possible. Only Standard RIs can be sold at the Reserved Instance Marketplace.

> The option that says: **It can enable you to reserve capacity for your Amazon EC2 instances in multiple Availability Zones and multiple AWS Regions for any duration** is incorrect because you can reserve capacity to a specific AWS Region (regional Reserved Instance) or specific Availability Zone (zonal Reserved Instance) only. You cannot reserve capacity to multiple AWS Regions in a single RI purchase.

> The option that says: **It runs in a VPC on hardware that's dedicated to a single customer** is incorrect because that is the description of a Dedicated instance and not a Reserved Instance. A Dedicated instance runs in a VPC on hardware that's dedicated to a single customer.

<br />

2. A large financial firm in the country has an AWS environment that contains several Reserved EC2 instances hosting a web application that has been decommissioned last week. To save costs, you need to stop incurring charges for the Reserved instances as soon as possible.

What cost-effective steps will you take in this circumstance? (Select TWO.)

[x] Go to the AWS Reserved Instance Marketplace and sell the Reserved instances.

[ ] Stop the Reserved instances as soon as possible.

[x] Terminate the Reserved instances as soon as possible to avoid getting billed at the on-demand price when it expires.

**Explanation**: The **Reserved Instance Marketplace** is a platform that supports the sale of third-party and AWS customers' unused Standard Reserved Instances, which vary in terms of lengths and pricing options. For example, you may want to sell Reserved Instances after moving instances to a new AWS region, changing to a new instance type, ending projects before the term expiration, when your business needs change, or if you have unneeded capacity.

Hence, the correct answers are:

* Go to the AWS Reserved Instance Marketplace and sell the Reserved instances.

* Terminate the Reserved instances as soon as possible to avoid getting billed at the on-demand price when it expires.

> **Stopping the Reserved instances as soon as possible** is incorrect because a stopped instance can still be restarted. Take note that when a Reserved Instance expires, any instances that were covered by the Reserved Instance are billed at the on-demand price which costs significantly higher. Since the application is already decommissioned, there is no point of keeping the unused instances. It is also possible that there are associated Elastic IP addresses, which will incur charges if they are associated with stopped instances

<br />
