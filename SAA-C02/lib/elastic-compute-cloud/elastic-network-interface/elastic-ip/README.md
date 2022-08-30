# Private, Public, and Elastic IPs

1. Part of your design requirements for a new EC2-based application is the need to be able to move the IPv4 address from the new EC2 instance to another instance in the same region if needed. Which of the following configurations will allow you to accomplish this goal w/ the least effort?

[ ] Move the default ENI to another instance to move the address

[x] Add a secondary IP address to the existing EC2 instance

[ ] Add a second ENI to the EC2 instance

[ ] Create the replacement EC2 instance w/ the same IP address as your existing instance and then stop the replacement instance until needed.

**Explanation**: Default ENI cannot be moved to another instance. Adding a second ENI to the EC2 instance is difficult if the instances are in the same subnet and is not the least effort.

<br />

2. One member of your DevOps team consulted you about a connectivity problem in one of your Amazon EC2 instances. The application architecture is initially set up with four EC2 instances, each with an EIP address that all belong to a public non-default subnet. You launched another instance to handle the increasing workload of your application. The EC2 instances also belong to the same security group. Everything works well as expected except for one of the EC2 instances which is not able to send nor receive traffic over the Internet.

Which of the following is the MOST likely reason for this issue?

[ ] The EC2 instance does not have a public IP address associated w/ it.

[ ] The EC2 instance does not have a private IP address associated w/ it.

[x] The EC2 instance is running in an Availability Zone that is not connected to an Internet gateway.

[ ] The route table is not properly configured to allow traffic to and from the Internet through the Internet gateway.

**Explanation**: IP addresses enable resources in your VPC to communicate with each other and with resources over the Internet. Amazon EC2 and Amazon VPC support the IPv4 and IPv6 addressing protocols.

By default, Amazon EC2 and Amazon VPC use the IPv4 addressing protocol. When you create a VPC, you must assign it an IPv4 CIDR block (a range of private IPv4 addresses). Private IPv4 addresses are not reachable over the Internet. To connect to your instance over the Internet or to enable communication between your instances and other AWS services that have public endpoints, you can assign a globally-unique public IPv4 address to your instance.

You can optionally associate an IPv6 CIDR block with your VPC and subnets and assign IPv6 addresses from that block to the resources in your VPC. IPv6 addresses are public and reachable over the Internet.

All subnets have a modifiable attribute that determines whether a network interface created in that subnet is assigned a public IPv4 address and, if applicable, an IPv6 address. This includes the primary network interface (eth0) that's created for an instance when you launch an instance in that subnet. Regardless of the subnet attribute, you can still override this setting for a specific instance during launch.

By default, non-default subnets have the IPv4 public addressing attribute set to `false`, and default subnets have this attribute set to `true`. An exception is a non-default subnet created by the Amazon EC2 launch instance wizard — the wizard sets the attribute to `true`. You can modify this attribute using the Amazon VPC console.

In this scenario, there are 5 EC2 instances that belong to the same security group that should be able to connect to the Internet. The main route table is properly configured but there is a problem connecting to one instance. Since the other four instances are working fine, we can assume that the security group and the route table are correctly configured. One possible reason for this issue is that the problematic instance does not have a public or an EIP address.

Take note as well that the four EC2 instances all belong to a public **non-default** subnet. This means that a new EC2 instance will not have a public IP address by default since the since IPv4 public addressing attribute is initially set to `false`.

Hence, the correct answer is the option that says: **The EC2 instance does not have a public IP address associated with it**.

The option that says: **The route table is not properly configured to allow traffic to and from the Internet through the Internet gateway** is incorrect because the other three instances, which are associated with the same route table and security group, do not have any issues.

The option that says: **The EC2 instance is running in an Availability Zone that is not connected to an Internet gateway** is incorrect because there is no relationship between the Availability Zone and the Internet Gateway (IGW) that may have caused the issue.

<br />
