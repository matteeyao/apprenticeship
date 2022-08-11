# Planning and Designing a Volume Gateway Deployment

1. Which setting is required to activate the AWS Storage Gateway before you can deploy it?

**AWS Region**

**Explanation**: The Storage Gateway is deployed to a selected AWS Region. This setting is required to deploy the service.

2. Which statement, if true, would make stored volume the appropriate choice for your gateway?

**You need all of your data stored locally and only require Amazon Elastic Block Store (Amazon EBS) snapshots for backup.**

**Explanation**: Stored volume is best suited for when you need all your data stored locally and only require Amazon EBS snapshots for backup and recovery purposes.

3. Which products or services are supported as a host platform for your Volume Gateway appliance? (Select three)

[ ] Amazon Lightsail instance

[x] Amazon EC2 instance

[ ] Amazon DynamoDB

[x] VMware ESXi VM

[ ] AWS Snowball Edge

[x] Hardware appliance

**Explanation**: Amazon EC2, VMware ESXi VM and a physical hardware appliance are all supported as a host platform for your Volume Gateway appliance.
