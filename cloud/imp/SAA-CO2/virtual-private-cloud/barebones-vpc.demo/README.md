# Deploying a Basic Amazon VPC

1. What is the maximum number of AWS Regions in which an Amazon Virtual Private Cloud (Amazon VPC) can be deployed?

**1**

**Explanation**: An Amazon VPC is launched into one AWS Region and cannot be spread across multiple regions.

2. Is the following statement correct: To ensure high availability, you should make your subnets span across more than one Availability Zone (make them Multi-AZ).

**No, it is not possible to make a subnet Multi-AZ**

**Explanation**: Subnets cannot span more than one Availability Zone.

3. In order for resources in your public subnet to reach the internet, they need to be provided with what kind of target? 

**Internet gateway**

**Explanation**: An internet gateway serves two purposes: 

* To provide a target in your VPC route tables for internet-routable traffic

* To perform network address translation (NAT) for instances that have been assigned public IPv4 addresses

4. What does this route table do? (Select TWO)

| **Destination** | **Target** |
|-----------------|------------|
| 10.0.0.0/16     | local      |
| 0.0.0.0/0       | igw-id     |

[ ] Row 1 overrides row 2, which means all traffic is routed within the Amazon Virtual Private Cloud (Amazon VPC), and none of it will leave for the internet. This route table needs to be corrected so that private traffic and internet traffic are routed separately.

[ ] Row 2 overrides row 1, which means it routes all traffic to the internet, including traffic destined for the Amazon VPC. This route table needs to be corrected so that private traffic is routed to the internet.

[ ] Nothing. This route table would not work if it were associated w/ a subnet.

[x] All IPv4 traffic within the local Classless Inter-Domain Routing (CIDR) range will route internally to the Amazon VPC.

[x] Any IPv4 traffic that does not match the 10.0.0.0/16 CIDR range will route to the internet gateway.

**Explanation**: Each route in a route table specifies a destination and a target. If a route table has multiple routes, the most specific route that matches the traffic is used to determine how to route the traffic.

In the example provided, IPv4 addresses within the 10.0.0.0/16 range will route to the destination **local** and all traffic destined for IPv4 addressed destinations will be routed to the internet gateway attached to the Amazon VPC.
