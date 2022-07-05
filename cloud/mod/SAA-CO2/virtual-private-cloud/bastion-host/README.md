# Bastion Host

If our instance in a private subnet needs to connect out to the internet, it's going to do that using a NAT instance or NAT Gateway. If, however, we want to SSH in or RDP into our instances in our private subnet, we do that via a Bastion host.

A Bastion host is simply an EC2 instance in a public subnet inside a VPC. They are a great security feature that can be used to allow incoming management connections. Once connected, you can jump into private resources and subnets. So Bastion hosts are an inbound management point and you can really tighten up what access is allowed w/ your Route Tables.

> **A Bastion Host**:
>
> A bastion host is a special purpose computer on a network specifically designed and configured to withstand attacks. The computer generally hosts a single application, for example a proxy server, and all other services are removed or limited to reduce the threat to the computer. It is hardened in this manner primarily due to its location and purpose, which is either on the outside of a firewall or in a demilitarized zone (DMZ) and usually involves access from untrusted networks or computers.

Bastion is a way of `SSH`ing or `RDP`ing into our private instances in our private subnets.

## Learning summary

> **Remember the following**;
>
> * A NAT Gateway or NAT Instance is used to provide internet traffic to EC2 instances in a private subnets.
>
> * A Bastion is used to securely administer EC2 instances (Using SSH or RDP).
>
> * You cannot use a NAT Gateway as a Bastion host.
