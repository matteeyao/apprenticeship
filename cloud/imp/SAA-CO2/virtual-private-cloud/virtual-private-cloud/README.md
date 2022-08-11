# Configuring and Deploying Amazon VPC w/ Multiple Subnets

1. Stateful is allowing traffic in and the response out automatically. Stateless is allowing traffic in and the response blocked from leaving unless explicitly permitted by a rule. (Select TWO.)

[x] Security groups are stateful.

[ ] Security groups are stateless.

[ ] Network Access Control Lists (network ACLs) are stateful.

[x] Network Access Control Lists (network ACLs) are stateless.

**Explanation**: Security groups are stateful. Network ACLs are stateless. Remember that when setting up traffic rules for each.

2. How many Availability Zones can you use in one Amazon VPC?

**Up to as many Availability Zones as are in that AWS Region.**

**Explanation**: VPCs are limited to a single Region, but you can place subnets in as many AZs as you want within a VPC's Region.

3. For which of the following is a default route table provided automatically? (Select two):

[x] Each Amazon VPC you create

[x] Each subnet you create

[ ] No resources; all route tables must be created by you

[ ] Each account

[ ] Each Amazon Elastic Compute Cloud (Amazon EC2 instance)

**Explanation**: Default route tables are created for each Amazon VPC and subnet you create.

Whereas every AWS Region in an account has its own default Amazon VPC with its own default route table, those route tables are per Amazon VPC, not per account.

While route tables can direct traffic to and from Amazon EC2 instances, they are not provided for instances by default.

4. What is the standard default address range used to route all outbound traffic to an Amazon Virtual Private Cloud (Amazon VPC) internet gateway?

**Explanation** If you are having trouble connecting resources in your Amazon VPC to the internet, make sure you have the correct entry for your route table:

Destination: 0.0.0.0/0 (or whatever the acceptable IP address ranges are for outbound traffic)

Target: [name of your internet gateway]

5. Classless Inter-Domain Routing (CIDR) blocks specified in route tables must be unique and cannot overlap.

**False**

**Explanation**: False. Although you should not have identical entries in route tables that apply to the same resource, the CIDR blocks can overlap. When the CIDR blocks for route table entries overlap, the more specific (smaller range) CIDR block takes priority.

6. Select all true statements. Elastic Load Balancing (ELB) ... (Select two):

[x] Is a managed service and is highly available by default.

[x] Load balancers can be internet facing or internal facing.

[ ] Can be used to route between resources in different subnets, Availability Zones, and AWS Regions.

[ ] Is a managed service, and therefore load balancers don't need to be launched into an Amazon VPC.

[ ] Requires its own unique subnet, separate from other resources.

**Explanation**: ELB will scale to handle changes in traffic and detect and replace unavailable load balancing nodes by default and is therefore highly available.

ELB load balancers can be internet facing or internal facing.

While you can use ELB load balancers to route between resources in different subnets and Availability Zones, they cannot route traffic between resources in different Regions.

ELB is a managed service; however, its load balancers do need to be **launched** into your Amazon VPC. If your Amazon VPC becomes unavailable, so will any load balancers within it.

ELB load balancers do not require their own subnets. They can share subnets with other internet-facing resources, such as network address translation (NAT) solutions.

7. Assuming the choices on the left are your chosen Classless Inter-Domain Routing (CIDR) block sizes, match them to the appropriate subnet on the right based on the multi-tier Amazon VPC architecture from this course.

`/20` → Private (Applications)

`/21` → Private (Data)

`/22` → Public

**Explanation**: In order of being likely to need the most IP addresses to the least, the three-tier VPC architecture is as follows:

  1. Private (Applications)

  2. Private (Data)

  3. Public

Why? Your public subnets should have a minimal attack surface. They should also be minimally resource-intensive. Where possible, they should use managed services that do not require as many IP addresses as Amazon EC2 Auto Scaling groups or your data layer.

Depending on your solution, your data-tier subnets are likely to need more IP addresses than your public subnets, but fewer than your application-tier subnets.

8. Which of these traffic patterns is correct for the multi-tier design pattern?

**Internet gateway → Application Load Balancer → Applications → Data → Applications → network address translation (NAT) solution → internet gateway**

**Explanation**: In the 3-tier architecture example, the internet-facing Application Load Balancer was deployed to the public subnets and routed directly to the private (application) subnets.

9. You have existing infrastructure in AWS running in a single Amazon VPC. You sized the VPC CIDR at time of creation to 192.168.1.0/24 and have now run out of IP addresses to launch new servers. How should you enable launch of new servers w/ least operational overhead and avoiding additional costs?

[ ] Move to IPv6 addressing

[ ] Re-size existing VPC CIDR from /24 to /16

[ ] Create a new VPC w/ CIDR 172.16.1.0/24 and peer the two VPC's together

[x] Add a secondary CIDR 192.168.2.0/24 to the VPC

**Explanation**: The additional CIDR needs to be smaller or equal in size to the existing CIDR at time of creation, in this case `/24`. IPv6 addresses are bound to the number of IPv4 addresses in the VPC. You are not able to re-size a CIDR. Peering two VPCs does not have the least operational overhead and does not avoid additional costs.

10. An application consists of Amazon Elastic Compute Cloud (Amazon EC2) instances placed in different Availability Zones. The Amazon EC2 instances sit behind an Application Load Balancer. The Amazon EC2 instances are managed via an Auto Scaling group. A network address translation (NAT) instance is used for the Amazon EC2 instances to download updates from the internet. Which option is the bottleneck in the architecture?

[ ] Amazon EC2 instances

[ ] Elastic Load balancing

[x] NAT instance

[ ] Auto Scaling group

**Explanation**: Auto Scaling Groups are highly available since they are a global service.
