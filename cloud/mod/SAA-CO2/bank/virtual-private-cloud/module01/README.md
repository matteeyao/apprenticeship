# VPC Mod I

1. VPC stands for:

**Virtual Private Cloud**

It is logically isolated (private) section where you can place AWS resources within a virtual network.

2. A VPN connection consists of which of the following components?

[x] Customer Gateway

[ ] Cross Connect

[ ] Direct Connect Gateway

[x] Virtual Private Gateway

A customer gateway is a resource that is installed on the customer side and provides a customer gateway inside a VPC.
A Virtual Private Gateway sits at the edge of your VPC and is a key component when using a VPN. It's responsible for site-to-site connection from on-premises to a VPC.

3. How many internet gateways can be attached to a custom VPC?

**1**

An internet gateway is a highly available VPC component; only one is attachable to a custom VPC.

4. A subnet can span multiple Availability Zones.

**False**

Each subnet must reside entirely within one Availability Zone and cannot span across zones.

5. What is the name of the AWS Global Accelerator component that services the static IP addresses for your accelerator from a unique IP subnet?

**Network Zone**

A network zone services the static IP addresses for your accelerator from a unique IP subnet. Similar to an AWS Availability Zone, a network zone is an isolated unit w/ its own set of physical infrastructure. When you configure an accelerator, by default, Global Accelerator allocates two IPv4 addresses for it. If one IP address from a network zone becomes unavailable due to IP address blocking by certain client networks, or network disruptions, then client applications can retry on the healthy static IP address from the other isolated network zone.

Reference: [AWS Global Accelerator components](https://docs.aws.amazon.com/global-accelerator/latest/dg/introduction-components.html)

6. You have created a new VPC and launched an EC2 instance into a public subnet. However, you did not assign a public IP to the instance during its creation. What is the easiest way to make your instance reachable from the internet?

[ ] Create an Elastic IP and new Network Interface. Associate the Elastic IP to the new Network Interface, and the new Network Interface to your instance.

[ ] Associate the Private IP of your instance to the Public IP of the Internet Gateway.

[x] Create an Elastic IP address and associate it with your EC2 instance.

[ ] Nothing - by default all instances deployed into any Public Subnet will automatically receive a Public IP.

Creating an Elastic IP address and associating it w/ your instance would be the simplest way to make your instance reachable from the outside world. A public subnet doesn't necessarily mean that a public IP is auto-assigned and set for your VPC.

An Elastic IP address is a public IPv4 address, which is reachable from the internet. If your instance does not have a public IPv4 address, you can associate an Elastic IP address w/ your instance to enable communication w/ the internet. For example, this allows you to connect to your instance from your local computer. [Elastic IP addresses](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/elastic-ip-addresses-eip.html).

7. When peering VPCs, you may peer your VPC only w/ another VPC in your same AWS account.

**False**. You may peer a VPC to another VPC that's in your same account, or to any VPC in any other account.

8. Which of the following offers the largest range of internal IP addresses?

**/16**

The higher the number is the fewer addresses it covers.

The `/16` offers 65,536 possible addresses.

9. In a default VPC, when you launch an EC2 instance and don't specify a subnet, the EC2 instances are assigned 2 IP addresses at launch. What are they?

**A Private IP Address & Public IP Address**

In a default VPC, when you launch an EC2 instance and don't specify a subnet, it's automatically launched into a default subnet in your default VPC. The default subnet may `MapPublicIPOnLaunch` set to the value of `true`. So when it is launched, a public and private IP is available for the instance.

10. You have five VPCs in a 'hub-and-spoke' configuration, w/ VPC 'A' in the center and individually peered w/ VPCs 'B', 'C', 'D', and 'E', which makes up the spokes. There are no other VPC connections. Which of the following VPCs can VPC 'B' communicate w/ directly?

**VPC 'A'**

As transitive peering is not allowed, VPC 'B' can communicate directly only w/ VPC 'A'. (A good alternative to many peer connections is AWS Transit Gateway. AWS Transit Gateway can connect VPCs and on-premises networks via a central hub and get around the transitive problem).

11. What is the purpose of an egress-only internet gateway?

[x] Prevents IPv6 based internet resources to initiate a connection into a VPC

[ ] Prevents IPv6 traffic from accessing the internet by utilizing security groups

[x] Allows VPC based IPv6 traffic to communicate to the internet

[ ] Allow instances communicating over IPv4 or IPv6 to access the internet

The purpose of an egress-only internet gateway is to allow IPv6 based traffic within a VPC to access the internet, whilst denying any internet based resources to connection back into the VPC.

12. By default, EC2 instances in new subnets in a custom VPC can communicate w/ each other across Availability Zones.

**True**

In a custom VPC with new subnets in each AZ, there is a route within the route table that supports communication across all subnets/AZs. Additionally, it has a Default SG with an "allow" rule: all traffic, all protocols, all ports, from resource using this default security group.

13. Are you permitted to conduct your own security assessments or penetration tests on your own VPC without alerting AWS first?

**Depends on the type of security assessment or penetration test and the service being assessed. Some assessments can be performed without alerting AWS, some require you to alert**.

AWS customers are welcome to carry out security assessments or penetration tests against their AWS infrastructure w/o prior approval for 8 services only. You should request authorization for other simulated events. Reference: [Penetration Testing](https://aws.amazon.com/security/penetration-testing/).

14. At which of the following levels can VPC Flow Logs be created?

[ ] Network Access Control List Level

[ ] Security Group Level

[x] VPC Level

[ ] Account Level

[x] Subnet Level

[x] Network Interface Level

VPC Flow Logs can be created at the VPC, subnet, and network interface levels. VPC Flow Logs is a feature that enables you to capture information about the IP traffic going to and from network interfaces in your VPC.

15. Which of the following are true for security groups?

[ ] Security groups operate at the subnet level.

[ ] Security groups support both "allow" and "deny" rules.

[x] Security groups evaluate all rules before deciding whether to allow traffic.

[x] Security groups operate at the instance level and are associated w/ network interfaces.

[x] Security groups support "allow" rules only.

[ ] Security groups process rules in number order when deciding whether to allow traffic.

*Security groups operate at the subnet level*. This is a characteristic of NACLs. W/ security groups their rules are applied based on the connection state of the traffic to determine if the traffic is allowed or denied. This approach allows security groups to be stateful.

Security groups control access at the instance-level (as they are associated w/ network interfaces), they support "allow" rules only, and they evaluate all rules before deciding whether to allow traffic into the instance(s).

Security groups operate at the instance level (as they are associated w/ network interfaces), they support "allow" rules only, and they evaluate all rules before deciding whether to allow traffic.

16. Which of the following is a chief advantage of using VPC gateway endpoints to connect your VPC to services such as S3 and DynamoDB?

**VPC gateway endpoints ensure traffic between your VPC and the other service does not leave the Amazon network**.

VPC gateway endpoints do not require public IP addresses, instead, they use AWS prefix list ID. Also, they are not used for rapid connectivity from the public internet.

In contrast to a NAT gateway, traffic between your VPC and the other services does not leave the Amazon network when using VPC gateway endpoints. A gateway endpoint is a gateway that you specify as a target for a route in your route table for traffic destined to a supported AWS service (S3 and DynamoDB).

17. Which of the following is true?

**Security groups are stateful and Network Access Control Lists (NACLs) are stateless**.

Security groups are stateful and Network Access Control Lists are stateless. Stateful means if you send a request from your instance, the response traffic for that request is allowed to flow in regardless of inbound security group rules.

18. When you create a custom VPC, which of the following are created automatically?

[ ] Internet Gateway

[ ] Subnets

[x] Security Group

[x] Route Table

[x] Network Access Control List (NACL)

[ ] NAT Gateway

When you create a custom VPC, a default Security Group, network access control list (NACL), and route table are created automatically. You must create your own subnets, internet gateway, and NAT gateway (if you need one).

19. To save administration headaches, a consultant advises that you leave all security groups in web-facing subnets open on port 22 to 0.0.0.0/0 CIDR. That way, you can connect wherever you are in the world. Is this a good security design?

**No**

0.0.0.0/0 would allow ANYONE from ANYWHERE to connect to your instances. This is generally a bad plan. The phrase 'web-facing subnets' does not mean just web servers. It would include any instances in that subnet, some of which you may not want strangers attacking. You would only allow 0.0.0.0/0 on port 80 or 443 to connect to your public-facing Web Servers, or preferably only to an ELB. Good security starts by limiting public access to only what the customer needs. Please see the AWS Security white paper for complete details.

20. Which of the following options allows you to securely administer an EC2 instance located in a private subnet?

**Bastion Host**

NAT gateway makes it easy to connect to the internet from instances within a private subnet.

A Bastion host allows you to securely administer (via SSH or RDP) an EC2 instance located in a private subnet. Don't confuse Bastions and NATs, which allow outside traffic to reach an instance in a private subnet.

21. An Application Load Balancer must be deployed into at least two Availability Zone subnets.

**True**

When setting up your ALB, for availability zone subnets, you must select at least two Availability Zone subnets from different Availability Zones. Each Availability Zone subnet for your load balancer should have a CIDR block w/ at least a /27 bitmask (Example, 10.0.0.0/27) and at least 8 free IP addresses per subnet.

22. Security groups act like a firewall at the instance level, whereas _ are an additional layer os security that act at the subnet level.

**Network ACLs**

NACLs act on the subnet level, while security groups act on the instance level.

23. When I create a new security group, all outbound traffic is allowed by default.

**True**

By default, a security group includes an outbound rule that allows all outbound traffic. You can remove the rule and add outbound rules that allow specific outbound traffic only. If your security group has no outbound rules, no outbound traffic originating from your instance is allowed.

24. What is the advantage of running your AWS VPN connection through your Direct Connect connection over using the ordinary internet?

[x] Faster performance

[ ] Can use Transit Gateway to service multiple accounts/VPCs

[x] Improved security

[ ] No data transfer charges when using Direct Connect

It is likely that if you choose to run your VPN through a Direct Connect from your datacenter to the AWS network that your VPN connection will be both faster, and more secure. However, data charges are still incurred whilst using Direct Connect. Additionally Transit Gateway attachments may be made to VPN regardless of if it is through DX or not.

25. Which of these is NOT a component of the AWS Global Accelerator service?

[x] CloudFront

[ ] Listeners

[ ] Endpoint Groups

[ ] Static IP Address

AWS Global Accelerator and Amazon CloudFront are separate services that use the AWS global network and its edge locations around the world. CloudFront improves performance for both cacheable content (such as images and videos) and dynamic content (such as API acceleration and dynamic site delivery). Global Accelerator improves performance for a wide range of applications over TCP or UDP by proxying packets at the edge to applications running in one or more AWS Regions.

26. You can accelerate your application by adding a second Internet Gateway to your VPC.

You can only have one Internet Gateway per VPC.

27. How many Amazon VPCs are allowed per AWS account per  Region? (Before any support requests to increase the number).

**5**

You can have up to five Amazon VPCs per AWS account per AWS Region, but you can place a support request to increase the number.

28. Which of the following statements are **NOT** true of EC2 instances in a VPC?

[ ] It is possible to have private subnets in a VPC.

[ ] In Amazon VPC, an EBS backed instance retains its private IP when stopped and started.

[x] In Amazon VPC, an instance retains its public IP when stopped and started.

[ ]  You may have only 1 active internet gateway for your instances per VPC.

AWS releases your instance's public IP address when it is stopped, hibernated, or terminated. Your stopped or hibernated instance receives a new public IP address when it is started.
