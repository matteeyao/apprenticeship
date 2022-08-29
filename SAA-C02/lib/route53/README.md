# Route 53

1. A company launched an EC2 instance in the newly created VPC. They noticed that the generated instance does not have an associated DNS hostname.

Which of the following options could be a valid reason for this issue?

[ ] The security group of the EC2 instance needs to be modified.

[x] The DNS resolution and DNS hostname of the VPC configuration should be enabled.

[ ] Amazon Route53 is not enabled.

[ ] The newly created VPC has an invalid CIDR block.

**Explanation**: When you launch an EC2 instance into a default VPC, AWS provides it with public and private DNS hostnames that correspond to the public IPv4 and private IPv4 addresses for the instance.

However, when you launch an instance into a non-default VPC, AWS provides the instance with a private DNS hostname only. New instances will only be provided with public DNS hostname depending on these two DNS attributes: the **DNS resolution** and **DNS hostnames**, that you have specified for your VPC, and if your instance has a public IPv4 address.

In this case, the new EC2 instance does not automatically get a DNS hostname because the **DNS resolution** and **DNS hostnames** attributes are disabled in the newly created VPC.

Hence, the correct answer is: **The DNS resolution and DNS hostname of the VPC configuration should be enabled.**

> The option that says: **The newly created VPC has an invalid CIDR block** is incorrect since it's very unlikely that a VPC has an invalid CIDR block because of AWS validation schemes.

> The option that says: **Amazon Route 53 is not enabled** is incorrect since Route 53 does not need to be enabled. Route 53 is the DNS service of AWS, but the VPC is the one that enables assigning of instance hostnames.

> The option that says: **The security group of the EC2 instance needs to be modified** is incorrect since security groups are just firewalls for your instances. They filter traffic based on a set of security group rules.

<br />
