# VPC

**Objective**: Build out a VPC from memory.

> **What is a VPC?**
>
> Think of a VPC as a virtual data centre in the cloud.
>
> Amazon Virtual Private Cloud (Amazon VPC) allows you to provision a logically isolated section of the Amazon Web Services (AWS) Cloud where you can launch AWS resources in a virtual network that you define. You have complete control over your virtual working environment, including selection of your own IP address range, creation of subnets, and configuration of route tables and network gateways.
>
> You can easily customize the network configuration for your Amazon VPC. For example, you can create a public-facing subnet for your web-servers that has access to the Internet, and place your backend systems such as databases or application servers in a private-facing subnet w/ no Internet access. You can leverage multiple layers of security, including security groups and network access control lists, to help control access to Amazon EC2 instances in each subnet.
>
> Additionally, you can create a Hardware Virtual Private Network (VPN) connection between your corporate data-center and your VPC and leverage the AWS cloud as an extension of your corporate data-center.

> A virtual private cloud, is your very own virtual data center in the cloud. And AWS allows us to provision our own VPC in a logically isolated section of AWS, and we can launch different resources and create our own virtual private network. We have complete control over this virtual networking environment, and that even includes choosing our own IP address ranges, creating our own subnet, and configuring access w/ our route tables and network gateways.

When you create a VPC, it is in one region in one AWS account, which makes VPCs a regional service and VPCs operate resiliently by operating in multiple availability zones inside a specific AWS region. By default, the VPC is isolated and private. We have to explicitly grant public access or any access.

There are two types of VPCs in AWS, the default VPC, which you can have only one of (default VPC) per region, and custom VPCs. These are isolated and private. Default VPCs are created by AWS when you create your AWS account and AWS sets up the configuration for us. So Default VPCs are configured and created by AWS when we create our AWS account, and custom VPCs are created and configured by us as we need them. Each default VPC comes w/ one VPC CIDR range, which is a given range of IP addresses and this VPC CIDR defines the start and the end range of the IP addresses that this default VPC can use. Everything inside our VPC uses the CIDR range. All communications to the VPC will need to use the VPC CIDR, and outgoing communications will also need to be from this VPC CIDR.

All default VPCs in AWS are configured the same and w/ our VPC, we can define our network across the Availability Zones for resiliency. So we can subdivide our VPC into subnets and subnets is short for sub-network. Each default VPC is configured to have one subnet located in each Availability Zone for that region and each subnet in our default VPC uses part of the IP address range of the VPC CIDR. Each subnet's IP address range must be unique to the other subnet's IP address range. These cannot overlap.

So if one of our Availability Zone fails, then our subnet in that failed Availability Zone will also fail. But w/ the default VPC, we have other subnets in other Availability Zones that are still operating.

## AWS VPC and Infrastructure Overview

![Fig. 1 AWS VPS and Infrastructure Diagram](../../../../img/SAA-CO2/virtual-private-cloud/diagram-i.png)

We have complete control over this virtual networking environment and includes choosing our own IP ranges, creating our own subnet, and configuring access w/ our **Route Tables** and our **Network Gateways**. We can create a public facing subnet for applications to have access to the internet, but we can also create private subnets w/ no internet access to secure our databases or back-end systems. AWS VPCs also offer multiple different layers of security-**NACLs** and security groups that we can configure to control who and what is allowed to access resources inside our VPC. We can create our own hardware virtual private network VPN connection between our on-premise data center and our AWS VPC, so we can leverage AWS as an extension of our current data center or environment. Recall that this is referred to as a **Hybrid environment**.

Private subnets are great if you don't want any traffic accessing your AWS resources, but you still can connect into your private subnet using your EC2 instance in this public subnet. When you do this, this EC2 instance acts as a **Bastion Host** or a **Jump Box** and it allows us access to the private EC2 instance.

A bastion host is an EC2 instance in a public subnet that you then use to connect to an EC2 instance in a private subnet.

The Internet Assigned Numbers Authority reserves three sets of IP addresses that are reserved for private IP address ranges:

* `10.0.0.0 - 10.255.255.255` (10/8 prefix)

* `172.16.0.0 - 172.31.255.255` (172.16/12 prefix)

* `192.168.0.0 - 192.168.255.255` (192.168/16 prefix)

When we create a VPC, we're creating by default, a **Route Table**, a **Network ACL**, and a **Security Group**.

In order to be able to use a VPC, we'll have to create **Subnets** for that VPC. At least one **Subnet** will have to be publicly accessible. One **subnet** will equal one AZ.

You can only have one **Internet Gateway** per VPC.

After creating an **Internet Gateway**, configure main routes in **Route Tables**, we a main route is provided to the Internet.

Keep main route table as private, and then configure a separate **Route Table** for our public subnets.

First thing we're going to do w/ our public route is create a route out to the Internet.

W/ our public and private subnets, we can start provisioning EC2 instances. We're going to create an EC2 instance in a public subnet, as well as one in our private subnet.

## VPC features

> **What can we do with a VPC?**
>
> * Launch instances into a subnet of your choosing
>
> * Assign custom IP address ranges in each subnet
>
> * Configure route tables between subnets
>
> * Create internet gateways and attach it to our VPC
>
> * Much better security control over our AWS resources
>
> * Instance security groups
>
> * Subnet network access control lists (ACLs)

> **Default VPC vs Custom VPC**
>
> * Default VPC is user friendly, allowing you to immediately deploy instances.
>
> * All Subnets in default VPC have a route out to the internet.
>
> * Each EC2 instance has a public and private IP address.

> **VPC Peering**
>
> * Allows you to connect one VPC w/ another via a direct network route using private IP addresses.
>
> * Instances behave as if they were on the same private network
>
> * You can peer VPC's w/ other AWS accounts as well as w/ other VPCs in the same account.
>
> * Peering is in a star configuration: i.e. 1 central VPC peers w/ 4 others. NO TRANSITIVE PEERING!
>
> * You can peer between regions.

Transitive peering refers to peering through one VPC to another, but this is not allowed. You must set up a new peering relationship.

## Designing Subnets

Subnets are what services run from inside our VPC, and they are how we add structure and functionality to our VPCs. Subnets are an availability zone resilient feature of AWS. A subnet is a sub-network of our VPC CIDR range, created in one availability zone since it runs in one availability zone.

![Fig. 2 Designing Subnets](../../../../img/SAA-CO2/virtual-private-cloud/diagram-ii.png)

If that availability zone fails, so will our subnet and the services running within those subnets. To design our infrastructure for High Availability, we can put our infrastructure in different subnets in a different availability zone. **One subnet is in one availability zone** and can never be in more than one availability zone. An availability zone can have zero to multiple subnets.

By default, the subnet uses IPv4 and this subnet is allocated from the IPv4 VPC CIDR. Subnet CIDRs cannot overlap. We can, however, add an IPv6 CIDR as long as the VPC is enabled for IPv6. What is the IPv6 CIDR subnet range? **`IPv6 Subnet Range = /64`**. **`IPv6 CIDR range for VPC = /56`**.

Subnets within our VPC can communicate between each other using the local route.

Some IP addresses inside each VPC are reserved.

> **Subnetting**
>
> There are **five IP addresses** in each subnet that we **cannot use**!

![Fig. 3 Reserved IP addresses](../../../../img/SAA-CO2/virtual-private-cloud/table.png)

## Routing and adding an Internet Gateway

![Fig. 4 Internet Gateway and Routing](../../../../img/SAA-CO2/virtual-private-cloud/diagram-iii.png)

Every VPC has a VPC router that is highly available and simply moves traffic. It runs in every availability zone that your VPC uses and the router has a network interface in each subnet in your VPC and uses the Network + 1 address (see above table). The router routes traffic between subnets in your VPC. We can control this router by creating a Route Table and associate the route table w/ the subnet and then add rules to allow traffic in and out of our subnet.

Each VPC has a main route table associated w/ our subnet, so if we do not explicitly associate our route table w/ our subnet, then the VPC will use the main route table. A subnet can have only 1 route table associated w/ itself, but you can use one Route Table for many different subnets inside our VPC.

Route Tables can have rules for one specific IP address, or it could be the default to allow all traffic. Route Tables are created at the VPC level, but they are associated w/ the subnet, which is in 1 availability zone.

For our scenario, we're going to need to create 3 Route Tables and associate them w/ our 3 subnets in different availability zones. Only 1 Route Table is associated per subnet at a time.

Similarly, your VPC can have only one Internet Gateway. The Internet Gateway is a regionally resilient service, so it's highly available and sits right on the edge of our VPC, the AWS public zone, and the internet. It manages traffic between our VPC, the AWS public zone, and the internet.

The reason that we have only one public Route Table is that the Internet Gateway is per VPC, so one Internet Gateway works across all of your availability zones.

If we were to create a new Internet Gateway for our AWS account, then we would have to attach it b/c it will not be attached automatically. We would also have to add a route to our Route Table to allow traffic to and from the Internet Gateway. If we add an Internet Gateway route to a subnet, then that subnet will become a public subnet b/c it now has access to the AWS public zone and the open internet.

The Internet Gateway works by performing a type of NAT translation - static NAT. The Internet Gateway allocates a resource w/ a public IPv4 IP address, so when that data or the packets leave that resource, and passes through the Internet Gateway, the Internet Gateway will actually switch the source IP address from the private IP address to the public IP address and then sends the packets on its way. When those packets return, it'll switch that destination address from the public IP address back to the private IP address, so that it can communicate inside our VPC.

## VPC cost optimization

> **Network and VPC Cost Optimization**
>
> **No. 1**
>
>   * Define your account structure
>
>   * Define **goals and metrics** to track w/ the business design to achieve the optimal outcome your organization needs in order to be successful
>
>   * Use the cost optimization design principles to design your infrastructure
>
>   * Enforce tagging
>
> **No. 2**
>
>   * Enable Cost Explorer
>
>   * Use the **AWS Billing and Cost Management** dashboard to track your expenses and usage and don't forget to enable tagging and enforce that tagging strategy
>
> **No. 3**
>
>   * Attach EIPs as you are charged for EIPs not attached in your AWS account
>
>   * Monitor **data transfer**. Most data transferred inside of VPC is free, but that data transferred out is sometimes charged
>
> **No. 4**
>
>   * AWS **Well-Architected Framework**
>
>   * Cost Optimization Pillar. We can monitor the utilization of CPU, RAM, storage, etc. to identify instances that could be downsized or may need to be increased in size
>
> **No. 5**
>
>   * Trust Advisor → AWS Trust Advisor Cost Explorer
>
>   * AWS **Well-Architected Framework** Tool

## Learning summary

> **Remember the following**:
>
> * Think of a VPC as a logical datacenter in AWS.
>
> * Consists of IGWs (or Virtual Private Gateways), Route Tables, Network Access Control Lists, Subnets, and Security Groups
>
> * 1 Subnet = 1 Availability Zone
>
> * Security Groups are Stateful; Network Access Control Lists are Stateless
>
> * NO TRANSITIVE PEERING

You cannot have a subnet stretched over multiple availability zones. However, you can have multiple subnets in the same availability zone. So, when we say one subnet equals one availability zone, all we mean there is that you cannot have one subnet spread across multiple availability zones, but you can definitely have multiple subnets in one availability zone.

> [!Note]
> We cannot have a subnet span availability zones. So it's always one subnet equals one availability zone.

*Security Groups are Stateful; Network Access Control Lists are Stateless* → w/ Network Access Control Lists, you can add "deny" rules as well as "allow" rules. And when you open up a port on inbound, it doesn't automatically open up a port on outbound. You would have go in and add that as well.

> **Remember the following**:
>
> * When you create a VPC a default **Route Table**, **Network Access Control List** (NACL) and a default **Security Group**.
>
> * It won't create **subnets**, nor will it create a default internet gateway.
>
> * `US-East-1A` in your AWS account can be a completely different availability zone to `US-East-1A` in another AWS account. The AZ's are randomized.
>
> * Amazon will always reserve 5 IP addresses within our subnets.
>
> * You can only have 1 Internet Gateway per VPC.
>
> * Security Groups can't span VPCs.

> **VPC Exam Tips**
>
> 1. VPC CIDRs cannot overlap. We can have only one default VPC per region and it can be deleted and then recreated.
>
> 2. Can add IPv6 CIDR, but VPC must be enabled for IPv6.
>
> 3. IPv6 CIDR subnet range: **/64**
>
> 4. IPv6 CIDR range: **/56**
>
> 5. Local route is used for communications between subnets.

Our default VPC CIDR is always the same and is designed and configured the same too. That CIDR range is `172.31.0.0/16`. This is the default VPC CIDR. We also have `/20` for subnets in each Availability Zone in our region and subnets are assigned public IPv4 addresses, so they are all public subnets and each default VPC will also be provided an Internet Gateway, Security Groups, NACLs.
