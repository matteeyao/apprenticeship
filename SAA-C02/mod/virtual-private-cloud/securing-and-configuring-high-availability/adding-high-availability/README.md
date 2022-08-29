# Adding High Availability

How are interruptions to the availability of your application's resources handled? Remember the custom Amazon VPC that was built in the first module? How can you make it highly available to lessen interruptions and add high availability and scalability to the custom Amazon VPC? 

AWS provides load balancers to achieve high availability, fault-tolerance, and scaling, and also custom Amazon VPCs where two subnets can be configured, each in a separate Availability Zone which creates a Multi-AZ design.  

## Elastic load balancers

A load balancer is a resource used to distribute incoming connections across a group of servers or services. Incoming connections are made to the load balancer, which then distributes the connections to the servers or services. Load balancers are great to pair with an AWS Auto Scaling Group to enhance the high availability, fault-tolerance, and scalability of an application.

Elastic Load Balancing (ELB) automatically distributes incoming application traffic across multiple targets, such as Amazon EC2 instances, containers, IP addresses, AWS Lambda functions, and virtual appliances. It can handle the varying load of your application traffic in a single Availability Zone or across multiple Availability Zones. 

All elastic load balancers offers high availability, automatic scaling, and robust security necessary to make your applications fault-tolerant. AWS provides four types of load balancers; each offers advantages for specific configurations.

> ### Classic load balancer
>
> AWS started off with one type of load balancer which was an elastic load balancer.  The elastic load balancer did not provide a lot of features, so AWS added more features and created the Application Load Balancer.  AWS then added even more features and released the Network Load Balancer and so on.
>
> The legacy load balancer for AWS, the elastic load balancer, is actually the Classic Load Balancer. The Classic Load Balancer, the Application Load Balancer, the Network Load Balancer, and the Gateway Load Balancer are services that make up the family of products known as Elastic Load Balancing (ELB).
>
> Classic Load Balancers are not recommended for use unless you have legacy services or applications that need the Classic Load Balancer.  It is recommended to choose the Application Load Balancer over the Classic Load Balancer whenever possible. 

> ### Application load balancer
>
> The Application Load Balancer is known as a layer 7 load balancer from the Open Systems Interconnection (OSI) model. Layer 7 means that the Application Load Balancer can inspect data that is passed through it and can understand the application layer, namely HTTP and HTTPs. The Application Load Balancer can then take actions based on things in that protocol such as paths, headers, and hosts. 
>
> All AWS load balancers are scalable and highly available. The Application Load Balancer has individual nodes running in each Availability Zone that are configured with the Application Load Balancer. Application Load Balancers can be internet-facing or internal; the difference is that internet facing Application Load Balancers will have public IP addresses and internal Application Load Balancers will have private IP addresses. 
>
> An internet-facing Application Load Balancer is designed to connect from the internet and those load balancer connections connect against the target instances. Internal load balancers are not accessible from the internet and are used to balance loads inside the Amazon VPC or between the layers of a multi-tier application.
>
> Again external Application Load Balancers listen from the outside and send traffic to targets or target groups within an Amazon VPC. Application Load Balancers are billed at an hourly rate and an additional rate based on the load placed on your load balancer.

> ### Network load balancer
>
> Network Load Balancers have advantages over Application Load Balancers because a Network Load Balancer does not need to worry about the upper layer protocol and it is much faster. Network Load Balancers are able to handle high-end workloads and scale to millions of requests per second.
>
> Network Load Balancers can allocate static IP addresses, they are easier to integrate with security and firewall products. Network Load Balancers also support routing requests on multiple applications on a single Amazon EC2 instance and supports the use of containerized applications.  
>
> Application Load Balancers are great for high end layer 7 protocol support, and Network Load Balancers support all other protocols and can handle millions of requests. 
>
> ![Fig. 1 Network load balancer](../../../../../img/SAA-CO2/virtual-private-cloud/securing-and-configuring-high-availability/adding-high-availability/diag01.png)

> ### Gateway load balancer
>
> Gateway Load Balancers let you deploy, scale, and manage virtual appliances, such as firewalls, intrusion detection and prevention systems, and deep packet inspection systems. It combines a transparent network gateway (that is, a single entry and exit point for all traffic) and distributes traffic while scaling your virtual appliances with the demand. 
>
> * A Gateway Load Balancer operates at the third layer of the Open Systems Interconnection (OSI) model, the network layer.
>
> * It listens for all IP packets across all ports and forwards traffic to the target group that's specified in the listener rule.
>
> * It maintains stickiness of flows to a specific target appliance using 5-tuple (for TCP/UDP flows) or 3-tuple (for non-TCP/UDP flows). 
>
> * The Gateway Load Balancer and its registered virtual appliance instances exchange application traffic using the GENEVE protocol on port 6081. It supports a maximum transmission unit (MTU) size of 8,500 bytes.
>
> Gateway Load Balancers use Gateway Load Balancer endpoints to securely exchange traffic across VPC boundaries. A Gateway Load Balancer endpoint is a VPC endpoint that provides private connectivity between virtual appliances in the service provider VPC and application servers in the service consumer VPC. You deploy the Gateway Load Balancer in the same VPC as the virtual appliances. You register the virtual appliances with a target group for the Gateway Load Balancer.
>
> * Traffic to and from a Gateway Load Balancer endpoint is configured using route tables. 
>
> * Traffic flows from the service consumer VPC over the Gateway Load Balancer endpoint to the Gateway Load Balancer in the service provider VPC, and then returns to the service consumer VPC. 
>
> * You must create the Gateway Load Balancer endpoint and the application servers in different subnets.
>
> * This lets you to configure the Gateway Load Balancer endpoint as the next hop in the route table for the application subnet.
>
> ![Fig. 2 Gateway load balancer](../../../../../img/SAA-CO2/virtual-private-cloud/securing-and-configuring-high-availability/adding-high-availability/diag02.jpeg)

## Making Amazon VPCs highly available, scalable, and fault-tolerant 

![Fig. 3 Expanding your Amazon VPC](../../../../../img/SAA-CO2/virtual-private-cloud/securing-and-configuring-high-availability/adding-high-availability/diag03.png)

> Expanding your Amazon VPC

### Step 1 - You'll need a second subnet

![Fig. 4 Multiple subnets](../../../../../img/SAA-CO2/virtual-private-cloud/securing-and-configuring-high-availability/adding-high-availability/diag04.png)

The first step is to create a second subnet where you make available another set of resources similar to those in the first. These resources can be available all the time, taking on a portion of the traffic to ensure that your application is still up if the resources in one become unavailable for some reason. 

The second subnet can also be used for a cold failover option. If your application becomes unavailable, with AWS Auto Scaling, you can designate that a new set of resources be launched automatically into your second subnet. Your users can automatically be rerouted to the new resources when they're available, using a traffic management option such as Elastic Load Balancing or Amazon Route 53.

This option would have some downtime, but your costs would probably be lower than with the first option.

### Step 2 - Use a load balancer to manage traffic

![Fig. 5 VPC w/ an Application Load Balancer](../../../../../img/SAA-CO2/virtual-private-cloud/securing-and-configuring-high-availability/adding-high-availability/diag05.png)

Now that you have two subnets, how can you manage traffic between them? An Application Load Balancer (ALB) from the Elastic Load Balancing service can distribute load between endpoints in your subnets. You can split the traffic between those resources based on business needs or use your ALB to perform A/B testing and blue/green deployments.

## Step 3 - Use a Multi-AZ approach

![Fig. 6 Multi-AZ approach](../../../../../img/SAA-CO2/virtual-private-cloud/securing-and-configuring-high-availability/adding-high-availability/diag06.png)

AWS recommends keeping your second subnet in a separate Availability Zone for redundancy and fault-tolerance. This spreads out your risk, so if resources in one Availability Zone become unavailable, resources in the second Availability Zone are not affected.

### Step 4 - You can associate route tables w/ multiple subnets

![Fig. 7 Route tables](../../../../../img/SAA-CO2/virtual-private-cloud/securing-and-configuring-high-availability/adding-high-availability/diag07.png)

That handy route table you created in the last lesson? You don't need to create a replica to use it with your new subnet. Associate the same route table with both subnets because they'll be using the same routes.

Note that while you can associate one route table with multiple subnets, a subnet cannot be associated with more than one route table.

### Step 5 - Fail over from unhealthy resources to healthy ones

![Fig. 8 Fail over](../../../../../img/SAA-CO2/virtual-private-cloud/securing-and-configuring-high-availability/adding-high-availability/diag08.png)

Elastic Load Balancing also provides health checks of associated resources, letting your infrastructure to automatically fail over connections from unhealthy resources to healthy ones.
