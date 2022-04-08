# Create custom VPC

![Fig. 1 VPC with Public & Private Subnet(s)](https://saurabh15blog.files.wordpress.com/2017/11/screenshot-2017-11-20-nats-vs-bastions-certified-solutions-architect-associate-2017.png)

So far, we've created a VPC. We've got a public and private **subnet** w/ instances in both. As of right now, however, all we can do is SSH into our public instance within our public subnet. We have no way of communicating to our private instance.

Within AWS Console, **EC2** ▶︎ **Instances**, we can see that we have two running instances, one in the public subnet `WebServer01` and one in our private subnet, `MyDBServer`, which doesn't have a public IP address. It does have, however, a private IP address.

We won't be able to SSH into `MyDBServer` from `WebServer01` b/c they're both in two separate security groups, and, by default, the security groups do not allows access to each other.

Instead, we will create a new **Security Group**. For `MyDBServer`, we should probably have a database security group.

In **Inbound rules**, we will need to determine what we want to be able to communicate to EC2 instances inside the security group. We'll want ICMP, being able to ping EC2 instances inside the **Security Group** from our `WebDMZ` security group. How do we allow our `WebDMZ` security group to ping? We'll have to enter in our sou1233rce, which will be our `WebDMZ` Security Group or an IP address range.

In **Inbound rules**, next we'll add **HTTP** to enable us to talk to our database server using HTTP. Maybe we're installing management software to manage MySQL, for example. We'll also want to perform the same task using **HTTPS**, so add **HTTPS**.
Add **SSH**. To communicate to our database servers using MySQL or Aurora, enable **MYSQL/AURORA**.

Now, we'll move `MyDBServer` from the default security group and add the instance to `MyDBSG` security group.
