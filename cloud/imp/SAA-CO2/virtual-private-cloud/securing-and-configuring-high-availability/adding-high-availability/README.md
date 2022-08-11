# Adding High Availability

1. True or False: Since Amazon VPC is highly available by default, there's no need to provide a second set of resources in a second subnet. If the resources in one subnet fail, it automatically fails over to a new subnet.

**False**

**Explanation**: Amazon VPC high availability is due to the fact that there are multiple Availability Zones (AZs) per AWS Region. But effective implementation of high availability depends on customer design and infrastructure implementation. 

2. What would you need to configure a second subnet with to maximize the availability of resources?

**Availability Zone**

**Explanation**: For high availability, we need to make sure we add resiliency across multiple Availability Zones.

3. True or False: Each subnet needs its own separate route table.

**False**

**Explanation**: Each subnet must be associated with a route table, which specifies the allowed routes for outbound traffic leaving the subnet. Every subnet created is automatically associated with the main route table for the VPC. This association can be changed and the contents of the main route table can be modified.

4. You can use Elastic Load Balancing to provide which of these for your infrastructure:

[x] Traffic management between resources in different subnets

[x] Automatic failover from unhealthy connections to healthy ones

[x] Automatic registration of new healthy instances to your load balancer
