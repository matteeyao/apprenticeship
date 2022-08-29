# Securing your VPC w/ Security Group

1. You want to create a group of Amazon EC2 instances in an application tier subnet that accepts HTTP traffic only from instances in the web tier (a group of instances in a different subnet sharing a web-tier security group). Which of the following will achieve this?

[ ] Adding a load balancer in front of the web tier instances `(doesn't block traffic)`

[x] Associating each instance in the application tier w/ a security group that allows inbound HTTP traffic from the web-tier security group

[ ] Adding an ACL to the application tier subnet that allows inbound HTTP traffic from the IP range of the web tier subnet `(Not stateful; allows access from other app tier instances)`

[ ] Changing the routing table for the web tier subnet to direct traffic to the application tier instances based on IP address `(Routing tables don't block traffic)`

<br />

2. A Solutions Architect needs to make sure that the On-Demand EC2 instance can only be accessed from this IP address (110.238.98.71) via an SSH connection. Which configuration below will satisfy this requirement?

[ ] Security Group Inbound Rule: Protocol - UDP, Port Range - 22, Source 110.238.98.71/32

[ ] Security Group Inbound Rule: Protocol - TCP, Port Range - 22, Source 110.238.98.71/0

[ ] Security Group Inbound Rule: Protocol - UDP, Port Range - 22, Source 110.238.98.71/0

[x] Security Group Inbound Rule: Protocol - TCP, Port Range - 22, Source 110.238.98.71/32

**Explanation**: A **security group** acts as a virtual firewall for your instance to control inbound and outbound traffic. When you launch an instance in a VPC, you can assign up to five security groups to the instance. Security groups act at the instance level, not the subnet level. Therefore, each instance in a subnet in your VPC can be assigned to a different set of security groups.

The requirement is to only allow the individual IP of the client and not the entire network. Therefore, the proper CIDR notation should be used. The /32 denotes one IP address and the /0 refers to the entire network. Take note that the SSH protocol uses TCP and port 22.

> **Protocol – UDP, Port Range – 22, Source 110.238.98.71/32 and Protocol – UDP, Port Range – 22, Source 110.238.98.71/0** are incorrect as they are using UDP.

> **Protocol – TCP, Port Range – 22, Source 110.238.98.71/0** is incorrect because it uses a /0 CIDR notation, allowing the entire network instead of a single IP.

<br />
