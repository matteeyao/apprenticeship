# EC2 Pricing

1. To save costs, your manager instructed you to analyze and review the setup of your AWS cloud infrastructure. You should also provide an estimate of how much your company will pay for all of the AWS resources that they are using. In this scenario, which of the following will incur costs? (Select TWO.)

[x] EBS Volumes attached to stopped EC2 Instances

[ ] A stopped On-Demand EC2 Instance

[ ] Public Data Set

[x] A running EC2 Instance

[ ] Using an Amazon VPC

**Explanation**: Billing commences when Amazon EC2 initiates the boot sequence of an AMI instance. Billing ends when the instance terminates, which could occur through a web services command, by running "shutdown -h", or through instance failure. When you stop an instance, AWS shuts it down but doesn't charge hourly usage for a stopped instance or data transfer fees. However, AWS does charge for the storage of any Amazon EBS volumes.

Hence, **a running EC2 Instance** and **EBS Volumes attached to stopped EC2 Instances** are the right answers and conversely, a stopped On-Demand EC2 Instance is incorrect as there is no charge for a stopped EC2 instance that you have shut down.

> **Using Amazon VPC** is incorrect because there are no additional charges for creating and using the VPC itself. Usage charges for other Amazon Web Services, including Amazon EC2, still apply at published rates for those resources, including data transfer charges.

> **Public Data Set** is incorrect due to the fact that Amazon stores the data sets at no charge to the community and, as with all AWS services, you pay only for the compute and storage you use for your own applications.

<br /> 
