# Elastic Network Interface (ENI)

1. A company plans to run a monitoring application on an Amazon EC2 instance in a VPC. Connections are made to the instance using its private IPv4 address. A solutions architect needs to design a solution that will allow traffic to be quickly directed to a standby instance if the application fails and becomes unreachable.

Which approach will meet these requirements?

[ ] Deploy an Application Load Balancer configured with a listener for the private IP address and register the primary instance with the load balancer. Upon failure, de-register the instance and register the secondary instance

[ ] Configure a custom DHCP option set. Configure DHCP to assign the same private IP address to the secondary instance when the primary instance fails.

[x] Attach a secondary elastic network interface (ENI) to the instance configured with the private IP address. Move the ENI to the standby instance if the primary instance becomes unreachable. 

[ ] Associate an Elastic IP address with the network interface of the primary instance. Disassociate the Elastic IP from the primary instance upon failure and associate it with a secondary instance.

**Explanation**: A secondary ENI can be added to an instance. While primary ENIs cannot be detached from an instance, secondary ENIs can be detached and attached to a different instance.

<br />

2. A company is running a dashboard application on a Spot EC2 instance inside a private subnet. The dashboard is reachable via a domain name that maps to the private IPv4 address of the instance’s network interface. A solutions architect needs to increase network availability by allowing the traffic flow to resume in another instance if the primary instance is terminated.

Which solution accomplishes these requirements?

[x] Create a secondary elastic network interface and point its private IPv4 address to the application's domain name. Attach the new network interface to the primary instance. If the instance goes down, move the secondary network interface to another instance.

[ ] Set up AWS Transfer for FTPS service in Implicit FTPS mode to automatically disable the `source/destination` checks on the instance's primary elastic network interface and re-associate it to another instance.

[ ] Attach an elastic IP address to the instance's primary network interface and point its IP address to the application's domain name. Automatically move the EIP to a secondary instance if the primary instance becomes unavailable using the AWS Transit Gateway.

[ ] Use the AWS Network Firewall to detach the instance's primary elastic network interface and move it to a new instance upon failure.

**Explanation**: If one of your instances serving a particular function fails, its network interface can be attached to a replacement or hot standby instance pre-configured for the same role in order to rapidly recover the service. For example, you can use a network interface as your primary or secondary network interface to a critical service such as a database instance or a NAT instance. If the instance fails, you (or more likely, the code running on your behalf) can attach the network interface to a hot standby instance.

![Fig. 1 Elastic Network Interface](../../../../img/SAA-CO2/elastic-compute-cloud/elastic-network-interface/fig01.jpeg)

Because the interface maintains its private IP addresses, Elastic IP addresses, and MAC address, network traffic begins flowing to the standby instance as soon as you attach the network interface to the replacement instance. Users experience a brief loss of connectivity between the time the instance fails and the time that the network interface is attached to the standby instance, but no changes to the route table or your DNS server are required.

Hence, the correct answer is **Create a secondary elastic network interface and point its private IPv4 address to the application’s domain name. Attach the new network interface to the primary instance. If the instance goes down, move the secondary network interface to another instance.**

> The option that says: **Attach an elastic IP address to the instance’s primary network interface and point its IP address to the application’s domain name. Automatically move the EIP to a secondary instance if the primary instance becomes unavailable using the AWS Transit Gateway** is incorrect. Elastic IPs are not needed in the solution since the application is private. Furthermore, an AWS Transit Gateway is primarily used to connect your Amazon Virtual Private Clouds (VPCs) and on-premises networks through a central hub. This particular networking service cannot be used to automatically move an Elastic IP address to another EC2 instance.

> The option that says: **Set up AWS Transfer for FTPS service in Implicit FTPS mode to automatically disable the `source/destination` checks on the instance’s primary elastic network interface and re-associate it to another instance** is incorrect. First of all, the AWS Transfer for FTPS service is not capable of automatically disabling the source/destination checks and it only supports Explicit FTPS mode. Disabling the source/destination check only allows the instance to which the ENI is connected to act as a gateway (both a sender and a receiver). It is not possible to make the primary ENI of any EC2 instance detachable. A more appropriate solution would be to use an Elastic IP address which can be re-associated with your secondary instance.

> The option that says: **Use the AWS Network Firewall to detach the instance’s primary elastic network interface and move it to a new instance upon failure** is incorrect. It's not possible to detach the primary network interface of an EC2 instance. In addition, the AWS Network Firewall is only used for filtering traffic at the perimeter of your VPC and not for detaching ENIs.

<br />
