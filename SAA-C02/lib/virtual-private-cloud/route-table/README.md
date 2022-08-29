# Route Table

1. For which of the following is a default route table provided automatically? Select two.

[x] Each Amazon VPC you create

[x] Each subnet you create

[ ] No resources; all route tables must be created by you

[ ] Each account

[ ] Each Amazon Elastic Compute Cloud (Amazon EC2 instance)

**Explanation**: Default route tables are created for each Amazon VPC and subnet you create.

Whereas every AWS Region in an account has its own default Amazon VPC with its own default route table, those route tables are per Amazon VPC, not per account.

While route tables can direct traffic to and from Amazon EC2 instances, they are not provided for instances by default.

<br />

2. What is the standard default address range used to route all outbound traffic to an Amazon Virtual Private Cloud (Amazon VPC) internet gateway?

**Explanation** If you are having trouble connecting resources in your Amazon VPC to the internet, make sure you have the correct entry for your route table:

Destination: 0.0.0.0/0 (or whatever the acceptable IP address ranges are for outbound traffic)

Target: [name of your internet gateway]

<br />

3. A company created a VPC with a single subnet then launched an On-Demand EC2 instance in that subnet. You have attached an Internet gateway (IGW) to the VPC and verified that the EC2 instance has a public IP. The main route table of the VPC is as shown below:

| **Destination** | **Target** | **Status** | **Propagated** |
|-----------------|------------|------------|----------------|
| 10.0.0.0/27     | local      | Active     | No             |

However, the instance still cannot be reached from the Internet when you tried to connect to it from your computer. Which of the following should be made to the route table to fix this issue?

[ ] Add the following entry to the route table: 10.0.0.0/27 → Your Internet Gateway

[ ] Add the new entry to the route table: 0.0.0.0/0 → Your Internet Gateway

[ ] Modify the above route table: 10.0.0.0/27 → Your Internet Gateway

[ ] Add this new entry to the route table: 0.0.0.0/27 → Your Internet Gateway

**Explanation**: Apparently, the route table does not have an entry for the Internet Gateway. This is why you cannot connect to the EC2 instance. To fix this, you have to add a route with a destination of `0.0.0.0/0` for IPv4 traffic or `::/0` for IPv6 traffic, and then a target of the Internet gateway ID (`igw-xxxxxxxx`).

<br />
