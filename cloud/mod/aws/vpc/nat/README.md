# NAT Instances and NAT Gateways

Network Address Translation (NAT) → enables our EC2 instances in our private subnets to still be able to go out and download software and, in order to do that, our EC2 instances require a way of communicating to our internet gateway. However, we don't want to make our subnets public. We still want a degree of control.

## NAT Instance

NAT instances are individual EC2 instances that do this, whereas NAT gateways are highly available, they are not just one EC2 instance. They are spread across multiple availability zones. They are not dependent on a single instance.

**EC2** ▶︎ **Launch Instance** ▶︎ **Community AMIs** ▶︎ Search `NAT`

**Actions** ▶︎ **Networking** ▶︎ Disable Source/Destination check

In order to get `MyDBServer` to talk to `NAT_Instance`, need to create a route in our default *Route Table*, so that our EC2 instances can talk to our NAT instance. **VPC** ▶︎ **Edit Routes** ▶︎ Add `0.0.0.0/0` ▶︎ Use NAT instance

SSH into `WebServer01`: `ssh ec2-user@<Public IP> -i MyNewKP.pem` ▶︎ Elevate privileges to root `sudo su` ▶︎ `ssh ec2-user@<Private IP> -i MyPvKey.pem` ▶︎ Elevate privileges to root `sudo su` ▶︎ `yum update -y`

## NAT Gateway explained

A NAT gateway is a highly available gateway that allows you to have your private subnets communicate out to the Internet w/o becoming public.

**VPC** ▶︎ **NAT Gateways** ▶︎ **Create NAT Gateway** ▶︎ `Subnet*: subnet-<Public IP>` ▶︎ **Create New EIP** ▶︎ **Edit Route Table** ▶︎ Edit Main Route Table ▶︎ Destination: `0.0.0.0/0` and Target: `nat-<NAT Gateway>`

## Learning summary

> **NAT Instances**
>
> * When creating a NAT instance, Disable Source/Destination Check on the Instance.
>
> * NAT instances must be in a public subnet.
>
> * There must be a route out of the private subnet to the NAT instance, in order for this to work.
>
> * The amount of traffic that NAT instances can support depends on the instance size. If you are bottlenecking, increase the instance size.
>
> * You can create high availability using Autoscaling Groups, multiple subnets in different AZs, and a script to automate failover.
>
> * Behind a Security Group.

In the demo, we placed our NAT instance in our `WebDMZ` security group.

For implementation, always prefer **NAT Gateways** to **NAT Instances**

> **NAT Gateways**
>
> * Redundant inside the Availability Zone
>
> * Preferred by the enterprise
>
> * Starts at 5Gbps and scales currently to 45Gbps
>
> * No need to patch
>
> * Not associated w/ security groups
>
> * Automatically assigned a public IP address
>
> * Remember to update your **Route Tables**
>
> * No need to disable Source/Destination Checks
>
> * If you have resources in multiple Availability Zones and they share one NAT gateway, in the event that the NAT gateway's Availability Zone is down, resources in the other Availability Zones lose internet access. To create an Availability Zone-independent architecture, create a NAT gateway in each Availability Zone and configure your routing to ensure that resources use the NAT gateway in the same Availability Zone.

"Redundant inside the Availability Zone." They are not a single EC2 instance. They can survive failure of the EC2 instances that power NAT Gateways.

You can only have one NAT Gateway inside one AZ. NAT Gateways cannot span AZs.
