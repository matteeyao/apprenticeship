# VPC Mod II

1. Can an existing DHCP option set be edited?

**No**

Subnets have a DHCP option set. DHCP stands for dynamic host configuration protocol. This is how devices automatically receive their IP addresses. Each VPC comes w/ one DHCP option set and can only have one applied at a time. It can be changed, but you must create a new one to do so. You cannot edit your DHCP option set.

2. What best describes the difference between a bastion host and a NAT gateway?

**A bastion host is used as a "gateway" for traffic that is destined for instances located in a private subnet; whereas, a NAT gateway provides instances in a private subnet w/ a route to the internet.**

3. Which VPC features can you use to control inbound and outbound traffic at the instance and subnet levels?

[ ] Route tables

[x] Network access control lists (ACLs)

[x] Security Groups

[ ] IAM Roles

Network ACLs act as a firewall for associated subnets, controlling both inbound and outbound traffic at the subnet level.

Security Groups act as a firewall for associated Amazon EC2 instances, controlling both inbound and outbound traffic at the instance level. When you launch an instance, you can associate it w/ one or more security groups that you've created. Each instance in your VPC could belong to a different set of security groups. If you don't specify a security group when you launch an instance, the instance is automatically associated w/ the default security group for the VPC.

See [Internetwork traffic privacy in Amazon VPC](https://docs.aws.amazon.com/vpc/latest/userguide/VPC_Security.html).

4. What does ::/0 mean?

**All IPv6 addresses**

5. What is an egress-only gateway?

**Outbound gateway for IPv6**

IPv6 addresses are publicly accessible, and they can communicate w/ an IGW in a bidirectional manner. The IPv6 instance can communicate w/ the public internet, and the public internet can communicate w/ that instance. We can use egress-inly gateways instead of NAT gateways for IPv6 instances where we do not want the public internet to be able to access these private instances directly.

6. What is a bastion host?

**An EC2 instance in a public subnet inside a VPC**

A bastion host is an EC2 instance in a public subnet inside of a VPC. They are a great security feature that can be used to allow an incoming management connection, and then once connected, you can jump/connect into private resources in subnets. It is an inbound management point, and you can really tighten up what access is allowed w/ your route tables.

7. What is a Gateway VPC endpoint?

**A gateway endpoint is used for AWS public services**

A gateway endpoint is used for AWS public services. Remember that some AWS services are public services, and they sit inside the AWS public zone. Sometimes we want to connect to these public services like S3 or DynamoDB from a private instance or subnet that does not have access to the internet and hoes not have a NAT gateway set up.

8. What are the 2 primary requirements of a NAT Gateway (or NAT instance)?

**A NAT gateway must be provisioned into a public subnet, and it must be part of the private subnet's route table.**

A NAT gateway must be provisioned into a public subnet (so that it has a route to the internet), and it must be part of the private subnet's route table (so that the private instances have a route to the NAT gateway).

A NAT gateway does not require a bastion host to work (but can be used in combination).

9. What traffic is affect by NACLs?

**Traffic crossing your subnet**

NACLs only affect traffic crossing the subnet border.

10. You work for a company that has been experiencing attacks on its network. Management has asked that you design a solution that will provide increased security for EC2 instances containing sensitive data, while still allowing employees to access the data when needed. Which of the following suggestions is best?

**Place the EC2 instances into private subnets, and set up a bastion host so employees can access them.**

Placing EC2 instances into private subnets is a great way to increase their security since they will no longer be directly accessible from any host outside of the VPC. Adding a bastion host to the architecture will allow unauthorized users to gain access to the internal resources (instances in private subnets) while providing an additional "hardened" layer of security.

NAT gateways give instances in a private subnet a route OUT to the internet, but do not address the issue of allowing outside access to these instances.

11. What is a subnet?

[ ] Default for IPv4 addresses

[ ] Default for IPv6 address

[x] A subnet is a logical subdivision of an IP network

[x] Subnets are how we add structure and functionality to our VPCs

A subnet is a logical subdivision of an IP network. Subnets are an AZ-resilient feature of AWS that adds structure and functionality to our VPCs.

12. What are the 2 types of VPCs?

Custom VPCs are configured by us as we need them, and we are responsible for all the configurations. By default, custom VPCs are isolated and private. Default VPCs are created by AWS when you create your AWS account, and AWS sets up the configuration for you. You can have only 1 default VPC per region.

13. What is an Internet Gateway?

**It is a network component that manages traffic between our VPC, the AWS public zone, and the internet.**

An internet gateway sits at the edge of the VPC and the AWS public zone, and the internet and manages traffic between them. There is only one internet gateway per VPC, so one internet gateway works across all of your availability zones.
