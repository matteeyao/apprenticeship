# Building a Custom VPC Structure

A custom VPC is also a regional service, just as w/ a default VPC. It is an isolated network and nothing is allowed in or out w/o explicit configurations. Custom VPCs are completely configurable by us. We can create a hybrid network to connect our AWS VPC w/ our on-premise network. In a custom VPC, there is a choice for default or dedicated tenancy.

Default tenancy is resources provisioned inside our VPC that are provisioned on shared hardware w/ others. Dedicated tenancy is a hardware that is dedicated to you and is much more expensive. Make sure you choose default tenancy unless you really need that dedicated tenancy.

Our custom VPCs can use IPv4 CIDR block and public IP addresses. We are given a private IPv4 CIDR block when we provision. We can choose the largest CIDR range which is a `/16` that gives us 65,536 IP addresses, or the smallest `/28` which only gives us 16 IP addresses. W/ a custom VPC, you can also have an optional secondary block of IPv4 addresses. We can configure our custom VPC w/ a single assigned IPv6 /56 CIDR block. We don't get a choice w/ this IPv6 like we do w/ IPv4. IPv6 are all public IP addresses by default.

Custom VPCs also have fully provisioned DNS. That is the `network + 2` IP address. Remember to choose to `enableDnsHostnames` and your instances in your VPC will be given a public DNS name. Enable DNS support as well to enable DNS resolution in your VPC.

![Fig. 1 VPC with Public & Private Subnet(s)](https://saurabh15blog.files.wordpress.com/2017/11/screenshot-2017-11-20-nats-vs-bastions-certified-solutions-architect-associate-2017.png)

So far, we've created a VPC. We've got a public and private **subnet** w/ instances in both. As of right now, however, all we can do is SSH into our public instance within our public subnet. We have no way of communicating to our private instance.

Within AWS Console, **EC2** ▶︎ **Instances**, we can see that we have two running instances, one in the public subnet `WebServer01` and one in our private subnet, `MyDBServer`, which doesn't have a public IP address. It does have, however, a private IP address.

We won't be able to SSH into `MyDBServer` from `WebServer01` b/c they're both in two separate security groups, and, by default, the security groups do not allows access to each other.

Instead, we will create a new **Security Group**. For `MyDBServer`, we should probably have a database security group.

In **Inbound rules**, we will need to determine what we want to be able to communicate to EC2 instances inside the security group. We'll want ICMP, being able to ping EC2 instances inside the **Security Group** from our `WebDMZ` security group. How do we allow our `WebDMZ` security group to ping? We'll have to enter in our sou1233rce, which will be our `WebDMZ` Security Group or an IP address range.

In **Inbound rules**, next we'll add **HTTP** to enable us to talk to our database server using HTTP. Maybe we're installing management software to manage MySQL, for example. We'll also want to perform the same task using **HTTPS**, so add **HTTPS**.
Add **SSH**. To communicate to our database servers using MySQL or Aurora, enable **MYSQL/AURORA**.

Now, we'll move `MyDBServer` from the default security group and add the instance to `MyDBSG` security group.
