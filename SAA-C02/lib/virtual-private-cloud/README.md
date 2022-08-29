# Configuring and Deploying Amazon VPC w/ Multiple Subnets

1. How many Availability Zones can you use in one Amazon VPC?

[ ] Up to as many Availability Zones as are in that AWS Region.

**Explanation**: VPCs are limited to a single Region, but you can place subnets in as many AZs as you want within a VPC's Region.

<br />

2. Classless Inter-Domain Routing (CIDR) blocks specified in route tables must be unique and cannot overlap.

**False**

**Explanation**: False. Although you should not have identical entries in route tables that apply to the same resource, the CIDR blocks can overlap. When the CIDR blocks for route table entries overlap, the more specific (smaller range) CIDR block takes priority.

<br />

3. Assuming the choices on the left are your chosen Classless Inter-Domain Routing (CIDR) block sizes, match them to the appropriate subnet on the right based on the multi-tier Amazon VPC architecture from this course.

`/20` → Private (Applications)

`/21` → Private (Data)

`/22` → Public

**Explanation**: In order of being likely to need the most IP addresses to the least, the three-tier VPC architecture is as follows:

  1. Private (Applications)

  2. Private (Data)

  3. Public

Why? Your public subnets should have a minimal attack surface. They should also be minimally resource-intensive. Where possible, they should use managed services that do not require as many IP addresses as Amazon EC2 Auto Scaling groups or your data layer.

Depending on your solution, your data-tier subnets are likely to need more IP addresses than your public subnets, but fewer than your application-tier subnets.

<br />

4. Which of these traffic patterns is correct for the multi-tier design pattern?

**Internet gateway → Application Load Balancer → Applications → Data → Applications → network address translation (NAT) solution → internet gateway**

**Explanation**: In the 3-tier architecture example, the internet-facing Application Load Balancer was deployed to the public subnets and routed directly to the private (application) subnets.

<br />

5. You have existing infrastructure in AWS running in a single Amazon VPC. You sized the VPC CIDR at time of creation to 192.168.1.0/24 and have now run out of IP addresses to launch new servers. How should you enable launch of new servers w/ least operational overhead and avoiding additional costs?

[ ] Move to IPv6 addressing

[ ] Re-size existing VPC CIDR from /24 to /16

[ ] Create a new VPC w/ CIDR 172.16.1.0/24 and peer the two VPC's together

[x] Add a secondary CIDR 192.168.2.0/24 to the VPC

**Explanation**: The additional CIDR needs to be smaller or equal in size to the existing CIDR at time of creation, in this case `/24`. IPv6 addresses are bound to the number of IPv4 addresses in the VPC. You are not able to re-size a CIDR. Peering two VPCs does not have the least operational overhead and does not avoid additional costs.

<br />

6. An application consists of Amazon Elastic Compute Cloud (Amazon EC2) instances placed in different Availability Zones. The Amazon EC2 instances sit behind an Application Load Balancer. The Amazon EC2 instances are managed via an Auto Scaling group. A network address translation (NAT) instance is used for the Amazon EC2 instances to download updates from the internet. Which option is the bottleneck in the architecture?

[ ] Amazon EC2 instances

[ ] Elastic Load balancing

[x] NAT instance

[ ] Auto Scaling group

**Explanation**: Auto Scaling Groups are highly available since they are a global service.

<br />

7. You have deployed an instance running a web server in a subnet in your VPC. When you try to connect to it through a browser using HTTP over the Internet, the connection times out. Which of these steps could fix the problem? Select three.

[x] Check that the VPC contains an Internet Gateway and the subnet's route table is routing 0.0.0.0/0 to the Internet Gateway.

[ ] Check that the VPC contains a Virtual Private Gateway and that the subnet's route table is routing 0.0.0.0/0 to the Virtual Private Gateway `(VPG are not involved in public internet access, but connecting VPC to customer data centers)`

[x] Check that the security group allows inbound access on port 80.

[ ] Check that the security group allows outbound access on port 80. `(Not required since security groups are stateful)`

[x] Check that the network ACL allows inbound access on port 80.

<br />

8. An aerospace engineering company recently adopted a hybrid cloud infrastructure with AWS. One of the Solutions Architect’s tasks is to launch a VPC with both public and private subnets for their EC2 instances as well as their database instances.

Which of the following statements are true regarding Amazon VPC subnets? (Select TWO.)

[ ] The allowed block size in VPC is between a /16 netmask (65,536 IP addresses) and /27 netmask (32 IP addresses).

[ ] Each subnet spans to 2 Availability Zones.

[x] Every subnet that you create is automatically associated w/ the main route table for the VPC.

[ ] EC2 instances in a private subnet can communicate w/ the Internet only if they have an Elastic IP.

[x] Each subnet maps to a single Availability Zone.

**Explanation**: A VPC spans all the Availability Zones in the region. After creating a VPC, you can add one or more subnets in each Availability Zone. When you create a subnet, you specify the CIDR block for the subnet, which is a subset of the VPC CIDR block. Each subnet must reside entirely within one Availability Zone and cannot span zones. Availability Zones are distinct locations that are engineered to be isolated from failures in other Availability Zones. By launching instances in separate Availability Zones, you can protect your applications from the failure of a single location.

![Fig. 1 VPC Route Tables](../../img/virtual-private-cloud/fig05.png)

Below are the important points you have to remember about subnets:

* Each subnet maps to a single Availability Zone.

* Every subnet that you create is automatically associated with the main route table for the VPC.

* If a subnet's traffic is routed to an Internet gateway, the subnet is known as a public subnet.

> The option that says: **EC2 instances in a private subnet can communicate with the Internet only if they have an Elastic IP** is incorrect. EC2 instances in a private subnet can communicate with the Internet not just by having an Elastic IP, but also with a public IP address via a NAT Instance or a NAT Gateway. Take note that there is a distinction between private and public IP addresses. To enable communication with the Internet, a public IPv4 address is mapped to the primary private IPv4 address through network address translation (NAT).

> The option that says: **The allowed block size in VPC is between a /16 netmask (65,536 IP addresses) and /27 netmask (32 IP addresses)** is incorrect because the allowed block size in VPC is between a /16 netmask (65,536 IP addresses) and /28 netmask (16 IP addresses) and not /27 netmask.

> The option that says: **Each subnet spans to 2 Availability Zones** is incorrect because each subnet must reside entirely within one Availability Zone and cannot span zones.

<br />
