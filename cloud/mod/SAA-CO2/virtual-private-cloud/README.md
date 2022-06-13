# VPC

**Objective**: Build out a VPC from memory.

> **What is a VPC?**
>
> Think of a VPC as a virtual data centre in the cloud.
>
> Amazon Virtual Private Cloud (Amazon VPC) allows you to provision a logically isolated section of the Amazon Web Services (AWS) Cloud where you can launch AWS resources in a virtual network that you define. You have complete control over your virtual working environment, including selection of your own IP address range, creation of subnets, and configuration of route tables and network gateways.
>
> You can easily customize the network configuration for your Amazon Virtual Private Cloud. For example, you can create a public-facing subnet for your web-servers that has access to the Internet, and place your backend systems such as databases or application servers in a private-facing subnet w/ no Internet access. You can leverage multiple layers of security, including security groups and network access control lists, to help control access to Amazon EC2 instances in each subnet.
>
> Additionally, you can create a Hardware Virtual Private Network (VPN) connection between your corporate data-center and your VPC and leverage the AWS cloud as an extension of your corporate data-center.

> A virtual private cloud, is your very own virtual data center in the cloud. And AWS allows us to provision our own VPC in a logically isolated section of AWS, and we can launch different resources and create our own virtual private network. We have complete control over this virtual networking env, and that even includes choosing our own IP address ranges, creating our own subnet, and configuring access w/ our route tables and network gateways.

## AWS VPC and Infrastructure Overview

![Fig. 1 AWS VPS and Infrastructure Diagram](../../../../img/SAA-CO2/virtual-private-cloud/diagram.png)

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
