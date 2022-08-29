# VPC Peering Connections

1. A media company has two VPCs: VPC-1 and VPC-2 with peering connection between each other. VPC-1 only contains private subnets while VPC-2 only contains public subnets. The company uses a single AWS Direct Connect connection and a virtual interface to connect their on-premises network with VPC-1.

Which of the following options increase the fault tolerance of the connection to VPC-1? (Select TWO.)

[ ] Establish a new AWS Direct Connect connection and private virtual interface in the same region as VPC-2.

[x] Establish a hardware VPN over the Internet between VPC-1 and the on-premises network.

[ ] Establish a hardware VPN over the Internet between VPC-2 and the on-premises network.

[x] Establish another AWS Direct Connect connection and private virtual interface in the same AWS region as VPC-1.

[ ] Use the AWS VPN CloudHub to create a new AWS Direct Connect connection and private virtual interface in the same region as VPC-2.

**Explanation**: In this scenario, you have two VPCs which have peering connections with each other. Note that a VPC peering connection does not support edge to edge routing. This means that if either VPC in a peering relationship has one of the following connections, you cannot extend the peering relationship to that connection:

  * A VPN connection or an AWS Direct Connect connection to a corporate network

  * An Internet connection through an Internet gateway

  * An Internet connection in a private subnet through a NAT device

  * A gateway VPC endpoint to an AWS service; for example, an endpoint to Amazon S3.

  * (IPv6) A ClassicLink connection. You can enable IPv4 communication between a linked EC2-Classic instance and instances in a VPC on the other side of a VPC peering connection. However, IPv6 is not supported in EC2-Classic, so you cannot extend this connection for IPv6 communication.

![Fig. 3 Peering Connection](../../../../img/SAA-CO2/virtual-private-cloud/peering/fig01.png)

For example, if VPC A and VPC B are peered, and VPC A has any of these connections, then instances in VPC B cannot use the connection to access resources on the other side of the connection. Similarly, resources on the other side of a connection cannot use the connection to access VPC B.

Hence, this means that you cannot use VPC-2 to extend the peering relationship that exists between VPC-1 and the on-premises network. For example, traffic from the corporate network can't directly access VPC-1 by using the VPN connection or the AWS Direct Connect connection to VPC-2, which is why the incorrect options are incorrect.

You can do the following to provide a highly available, fault-tolerant network connection:

  * Establish a hardware VPN over the Internet between the VPC and the on-premises network.

  * Establish another AWS Direct Connect connection and private virtual interface in the same AWS region.
