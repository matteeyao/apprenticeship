# Virtual Private Cloud

## What is an Amazon VPC?

A VPC is a virtual private cloud. In essence, a VPC is a virtual data center in the Cloud. Amazon VPCs are virtual networks, associated to a single AWS Region, and is a service that defines a boundary around the AWS services and resources that customers choose to deploy and how those services and resources communicate w/ each other and external networks such as the internet. AWS supports hybrid Cloud configurations that facilitate a connection between an Amazon VPC and an on-premises location such as a physical data center.

There are two types of Amazon VPCs in an AWS account: a default Amazon VPC and a custom Amazon VPC.

## Default Amazon VPC

When you create an AWS account, default Amazon VPCs are created in each supported AWS Region. Using the default Amazon VPC, you can immediately start deploying resources and not have to think about the underlying network.

* Each default Amazon VPC creates a public subnet within each Availability Zone within the supported Region.

* Each public subnet is configured w/ a default route for all inbound and outbound traffic that routes IP traffic to the general internet.

* AWS sets up the configuration that allows all traffic, so there is no privacy and isolation by default.

* Only one default Amazon VPC per Region is permitted.

* Each default comes w/ one Amazon VPC Classless Inter-Domain Routing (CIDR) range, which is a given range of IP addresses.

This default Amazon VPC CIDR defines the start and end range of the IP address that the default Amazon VPC can use. Everything inside an Amazon VPC uses this CIDR range. All communications to the Amazon VPC will need to use the Amazon VPC CIDR and outgoing communications will be from this Amazon VPC CIDR.

All default Amazon VPCs are configured in the same way. For added resiliency, the default Amazon VPC is automatically divided into subnets across Availability ZOnes.

* Each default Amazon VPC is configured to have one subnet located in each Availability Zone of that Region.

* Each subnet in the default Amazon VPC uses part of the IP address range of the Amazon VPC CIDR.

* Each subnet's IP address range must be unique to the other subnets' IP address range and cannot overlap.

If one Availability Zone fails in your default Amazon VPC, the associated subnet will also fail, but w/ the default Amazon VPC, the other subnets in other Availability Zones are still operating.

The Amazon VPC IPv6 CIDR for your subnet range is /64, and the Amazon VPC CIDR range is /56. The local routes is used to communicate between subnets inside your Amazon VPC. The default Amazon VPC IPv4 CIDR, 172.31.0.0/16, is always the same, and is designed and configured the same too.

## Custom Amazon VPC

A custom Amazon VPC is a logically isolated virtual network within a supported Region under a single account, making it a regional service.

* Unlike a default Amazon VPC, each component of a custom Amazon VPC must be explicitly defined when you create it; nothing is allowed in or out w/o explicit configuration.

* Some decisions, such as the IPv4 and IPv6 support and the CIDR block for the Amazon VPC, cannot be modified later.

* Other features of an Amazon VPC, such as subnets, routing, and VPC endpoints, can be modified as needed.

A custom Amazon VPC, similar to a default Amazon VPC, provides a logically isolated virtual network that supports the deployment of resources and services supported by the Region that the Amazon VPC is created in. Complete control is provided over the virtual network defined by the Amazon VPC.

This includes:

* Choosing the IP address ranges supported by defining each subnet

* Managing network internal traffic flow

* Managing how traffic enters and leaves the Amazon VPC through route tables and network gateways

In a later section, you learn how a public subnet can be configured to allow an application or resource to have access to the internet and how a private subnet, a subnet w/o external network access, can be used to help secure a database or backend system. You also learn about the multiple layers of security available through an Amazon VPC and how an Amazon VPC can use, for example, network ACLs and security groups to control who and what is allowed access to resources deployed within the Amazon VPC.

AWS supports extending your AWS environment by establishing a secure connection between an Amazon VPC and an on-premises network using:

* AWS Direct Connect

* AWS Site-to-Site Virtual Private Network (VPN)

* AWS Client VPN

## Default Amazon VPC versus Custom Amazon VPC

> ### Default Amazon VPC
>
> Each AWS account comes w/ a default Amazon VPC, enabling a customer to immediately start building an environment within the AWS Cloud.
>
> The default Amazon VPC is initially configured w/ a /16 IPv4 CIDR block, a /20 default subnet for the Regions' Availability Zones, an internet gateway, a default route to the internet gateway, a default security group, a default network ACL, and a default Dynamic Host Configuration Protocol (DHCP) option set.
>
> The default routing to the internet gateway ensures that all initial subnets within the default Amazon VPC have an outbound route to the internet and are configured to assign any Amazon Elastic Compute Cloud (Amazon EC2) instance launched within the default Amazon VPC w/ both public and private IP addresses.
>
> The following image illustrates all of the features of a default Amazon VPC. For example, note that the CIDR block for a default Amazon VPC is always `172.31.0.0/16`.

> ### Custom Amazon VPC
>
> In each AWS account, you can also build a custom Amazon VPC, which is an isolated network, unlike the default Amazon VPC. W/ a custom Amazon VPC, nothing is allowed in or out w/o explicit configuration.
>
> Custom Amazon VPCs are completely configurable, unlike the default Amazon VPC which AWS configures when the AWS account is created.
>
> Another difference between custom and default Amazon VPCs is that w/ a custom Amazon VPC, there is a choice for default or dedicated tenancy. Default tenancy is used for resources provisioned inside the Amazon VPC that are provisioned on shared hardware that is shared w/ others. Dedicated tenancy is hardware that is dedicated to you alone. It is not shared w/ others, but if dedicated tenancy is chosen, it cannot be modified at a later date. The Amazon VPC will need to be deleted and then reconfigured to choose default tenancy. Dedicated tenancy does cost more, so for cost optimization, choose default tenancy unless there is a specific reason to choose dedicated tenancy.
>
> Custom Amazon VPCs can also use IPv4 CIDR blocks and Public IPs. AWS assigns a private IPv4 CIDR block when the custom Amazon VPC is provisioned. There is a choice between the largest CIDR range w/ a `/16` that has 65,536 Ip addresses or the smallest `/28` that gives 16 IP addresses. Also w/ a custom Amazon VPC, there is an optional secondary block of IPv4 addresses. Custom Amazon VPCs can also be configured w/ a single assigned IPv6 `/56` CIDR block; the IPv6 CIDR is automatically assigned by AWS, unlike w/ IPv4.
>
> Custom Amazon VPCs also have fully provisioned Domain Name System (DNS) that is the Network +2 IP address.

Explore the features of a **default Amazon VPC**:

![Fig. 1 Default Amazon VPC](../../../../img/SAA-CO2/virtual-private-cloud/vpc/diag01.png)

> ### Availability Zone
>
> AWS creates a size `/20` default subnet in each Availability Zone. This provides up to 4,096 addresses per subnet, along w/ a few reserved for AWS to use.

> ### Amazon EC2 instance
>
> AWS assigns a public and private IP address for Amazon EC2 instances.

> ### Amazon VPC
>
> AWS creates an Amazon VPC w/ a size `/16` IPv4 CIDR block (`172.31.0.0/16`). This provides up to 65,536 private IPv4 addresses.

> ### DHCP options sets
>
> AWS associates the default Dynamic Host Configuration Protocol (DHCP) options set for your AWS account w/ your default Amazon VPC.

> ### Network ACL
>
> AWS creates a default network ACL and associates it w/ your default Amazon VPC.

> ### Security group
>
> AWS creates a default security group and associates it w/ your default Amazon VPC.

> ### Internet gateway
>
> AWS creates an internet gateway and connects it to your default Amazon VPC.

> ### Route table
>
> AWS adds a route to the main route table that points all traffic (0.0.0.0/0) to the internet gateway.
