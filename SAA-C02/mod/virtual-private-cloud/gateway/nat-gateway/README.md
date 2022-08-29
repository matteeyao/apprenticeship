# NAT Instances and NAT Gateways

**Network Address Translation (NAT)** → enables our EC2 instances in our private subnets to still be able to go out and download software and, in order to do that, our EC2 instances require a way of communicating to our internet gateway. However, we don't want to make our subnets public. We still want a degree of control.

**Network Address Translation (NAT)** is a process of providing a private resource outgoing access to the internet. **Internet gateways** perform a type of NAT called static NAT. Recall that the internet gateway allocates a resource w/ a public IPv4 IP address, so when the data or the packets leave the resource, it passes through the internet gateway and the internet gateway switches the source IP address from the private IP address to the public IP address, and then sends the packets on its way. When that packet returns, it switches the destination address from the public IP address back to the private IP address.

NAT gives us a private CIDR range for outgoing internet access and also to the AWS public zone. When private resources initiate traffic w/ a NAT gateway, they can receive responses back in, but outside traffic from the internet cannot initiate traffic inbound. AWS provides us two ways to use NAT using EC2 instances or by using a NAT gateway.

Why would a private service need access to the internet? Software updates. But these private resources and services also need to stay isolated and private.

![Fig. 1 Creating a NAT Gateway and Egress-Only Gateways](../../../../../img/SAA-CO2/virtual-private-cloud/gateway/nat-gateway/fig01.png)

How do we provision a **NAT gateway** in a public **subnet** that has a public IP address, an associated **route table**, and a route to the **internet gateway**? The private instances and private subnet also have a route, different from our public **route table**. We can configure our private **route table** to have a route to the **NAT gateway** and configure our public **route table** to send traffic from our NAT gateway out. Like the **internet gateway**, the NAT gateway works in a similar manner. When the private instance sends data to the NAT gateway, it does so using a **route table** to the **NAT gateway** from the private **route table** and the **NAT gateway** then records and stores the source IP address of the instance sending the packet as well as the destination address that the packet is for. It can do this for multiple different instances too, in a translation table, and then it will adjust the packets and change the source address of the instance to be its own source address, or the **NAT gateway**'s source address. The private instance will then use the route to the **internet gateway** to send that packet. The **internet gateway** takes the packet from the NAT gateway and modifies the packet to the NAT gateways public address.

So the **NAT gateway** allows multiple private addresses to masquerade behind it by using the NAT source address, and then eventually, the NAT gateway's public IP address is given to the **internet gateway**.

To give private instances access to the internet, we need both the **NAT gateway** and **internet gateway**. NAT gateways have to sit inside a public subnet b/c it needs that public IP address. NAT gateways also require updated routes to allow routing. NAT gateways use elastic IPs, a special type of an IPv4 address that is static and do not change. NAT gateways are an availability resilient service, so for high availability, we need to add one NAT gateway into each availability zone that our VPC uses and, remember, we have to go in and update our **route tables** for each availability zone as well.

NAT is not required for IPv6 since all IPv6 addresses are already publicly route-able and the **internet gateway** can work directly w/ those IPv6 addresses. NAT gateways do not work w/ IPv6, so we are going to need 3 NAT gateways, one in each public subnet, and a private route table in each availability zone (availability zone A, B, and C) and a route pointing to the NAT gateway in the same availability zone. This will allow our architecture for high availability.

You can also use an EC2 instances as a NAT instance to provide NAT.

We must disable the source destination check on our EC2 instance in order to use it as a NAT instance. NAT gateway is the preferred NAT solution for our VPC.

![Fig. 2 NAT Gateways](../../../../../img/SAA-CO2/virtual-private-cloud/gateway/nat-gateway/fig02.png)

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
