# VPC Peering Connection

1. An operations team has an application running on EC2 instances inside two custom VPCs. The VPCs are located in the Ohio and N.Virginia Region respectively. The team wants to transfer data between the instances without traversing the public internet.

Which combination of steps will achieve this? (Select TWO.)

[ ] Deploy a VPC endpoint on each region to enable a private connection.

[ ] Create an Egress-only Internet Gateway.

[ ] Launch a NAT Gateway in the public subnet of each VPC.

[ ] Re-configure the route table's target and destination of the instances' subnet.

[ ] Set up a VPC peering connection between the VPCs.

**Explanation**: A **VPC peering connection** is a networking connection between two VPCs that enables you to route traffic between them using private IPv4 addresses or IPv6 addresses. Instances in either VPC can communicate with each other as if they are within the same network. You can create a VPC peering connection between your own VPCs, or with a VPC in another AWS account. The VPCs can be in different regions (also known as an inter-region VPC peering connection).

**Inter-Region VPC Peering** provides a simple and cost-effective way to share resources between regions or replicate data for geographic redundancy. Built on the same horizontally scaled, redundant, and highly available technology that powers VPC today, Inter-Region VPC Peering encrypts inter-region traffic with no single point of failure or bandwidth bottleneck. Traffic using Inter-Region VPC Peering always stays on the global AWS backbone and never traverses the public internet, thereby reducing threat vectors, such as common exploits and DDoS attacks.

Hence, the correct answers are:

* Set up a VPC peering connection between the VPCs.

* Re-configure the route table’s target and destination of the instances’ subnet.

> The option that says: **Create an Egress only Internet Gateway** is incorrect because this will just enable outbound IPv6 communication from instances in a VPC to the internet. Take note that the scenario requires private communication to be enabled between VPCs from two different regions.

> The option that says: **Launch a NAT Gateway in the public subnet of each VPC** is incorrect because NAT Gateways are used to allow instances in private subnets to access the public internet. Note that the requirement is to make sure that communication between instances will not traverse the internet.

> The option that says: **Deploy a VPC endpoint on each region to enable private connection** is incorrect. VPC endpoints are region-specific only and do not support inter-region communication.

<br />
