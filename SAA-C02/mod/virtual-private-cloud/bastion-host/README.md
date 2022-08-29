# Bastion Host

If our instance in a private subnet needs to connect out to the internet, it's going to do that using a NAT instance or NAT Gateway. If, however, we want to SSH in or RDP into our instances in our private subnet, we do that via a Bastion host.

A Bastion host is simply an EC2 instance in a public subnet inside a VPC. They are a great security feature that can be used to allow incoming management connections. Once connected, you can jump into private resources and subnets. So Bastion hosts are an inbound management point and you can really tighten up what access is allowed w/ your Route Tables.

> **A Bastion Host**:
>
> A bastion host is a special purpose computer on a network specifically designed and configured to withstand attacks. The computer generally hosts a single application, for example a proxy server, and all other services are removed or limited to reduce the threat to the computer. It is hardened in this manner primarily due to its location and purpose, which is either on the outside of a firewall or in a demilitarized zone (DMZ) and usually involves access from untrusted networks or computers.

Bastion is a way of `SSH`ing or `RDP`ing into our private instances in our private subnets.

## Implementing a Bastion Host

The best way to implement a bastion host is to create a small EC2 instance which should only have a security group from a particular IP address for maximum security. This will block any SSH Brute Force attacks on your bastion host. It is also recommended to use a small instance rather than a large one because this host will only act as a jump server to connect to other instances in your VPC and nothing else.

![Fig. 1 Bastion Host Infrastructure](../../../../img/SAA-CO2/virtual-private-cloud/bastion-host/fig01.png)

Therefore, there is no point of allocating a large instance simply because it doesn't need that much computing power to process SSH (port 22) or RDP (port 3389) connections. It is possible to use SSH with an ordinary user ID and a pre-configured password as credentials but it is more secure to use public key pairs for SSH authentication for better security.

Create a small EC2 instance with a security group which only allows access on port 22 via the IP address of the corporate data center. Use a private key (.pem) file to connect to the bastion host.

## Learning summary

> **Remember the following**;
>
> * A NAT Gateway or NAT Instance is used to provide internet traffic to EC2 instances in a private subnets.
>
> * A Bastion is used to securely administer EC2 instances (Using SSH or RDP).
>
> * You cannot use a NAT Gateway as a Bastion host.
