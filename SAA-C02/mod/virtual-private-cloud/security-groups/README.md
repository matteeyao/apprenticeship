# Security Groups

> **Security Groups** control traffic at the `instance` level. There can be up to five security groups assigned to an instance. Each instance in a subnet in a VPC could be assigned to a different set of **security groups**. VPCs automatically come w/ a default **security group**. If a customer does not specify a different security group when an instance is launched, AWS associates the default security group within the instance.

If you make a rule change to a security group, that change takes effect immediately. Unlike NACLs that are attached to the subnet, Security Groups are attached to the ENI of the AWS resources in the subnet. They work differently from NACLs. Security Groups are assigned to the ENI of the AWS resource, so it sits at the boundary of the instances instead of the subnet.

![Fig. 1 Creating Security Groups](../../../../img/SAA-CO2/virtual-private-cloud/security-groups/diagram-i.png)

> 1. Security groups are **stateful**. Security groups have inbound and outbound rules, but security groups are **stateful**. If traffic is allowed in, then traffic is automatically allowed back out.
>
> 2. See traffic as **one stream**. See both the inbound and outbound traffic and consider that traffic 1 stream. One big difference between Security Groups and NACLs is that Security Groups recognize AWS resources and you can add rules for these. So for an EC2 instance, you could add the instance ID for that instance to allow traffic from that specific instance. You could also allow rules for other security groups or add a rule for security groups themselves.
>
> 3. Security group rules are processed **all at once**-not in order.
>
> 4. Have an **explicit deny**. Anything that is not explicitly allowed is denied. Security groups cannot explicitly deny an IP address like NACLs, so if you need to explicitly deny, then you need to use NACLs.

Default security group allows all traffic inbound and outbound. Custom security groups require us to go in and add rules to allow traffic, but again, that traffic is automatically allowed back out.

Security groups process traffic rules all at once. There is no order like there is w/ NACLs. Default security groups allow all inbound and outbound rules.

Security groups are stateful → when you create an Inbound rule, an Outbound rule is created automatically. For instance, if you allow HTTP in, it's automatically allowed out as well. This extends to RDP, SSH, MySQL

Whereas **Network Access Controllers** (NACLs) are stateless → when you create an Inbound rule, you also have to create an Outbound rule as well

When creating an inbound rule, you are unable to block a specific port or specific IP address using Security Groups

However, when you create an individual security group, everything is blocked by default → you have to go in and allow something. But, when you go in and allow HTTP or MySQL, then traffic is allowed through.

## Learning summary

> All Inbound traffic is blocked by default

* You have to go into a security group and enable individual ports

> All Outbound traffic is allowed

* If we went in and deleted that rule, there is not effect on the security group

> The reason is that security groups are stateful
>
> Changes to Security Groups take effect immediately

* As soon as we deleted Port 80, we could no longer access our website

> You can have any number of EC2 instances within a security group
>
> You can have multiple security groups attached to EC2 instances

* In our EFS lab, we attached our default security group, and our WebDMZ security group as well

> Security Groups are **stateful**

* Meaning we don't have to change Inbound and Outbound ports. If you enable a port in the Inbound ports, Outbound is enabled automatically for that port. Any rule that is added to allow inbound traffic will automatically be allowed outbound So, you don't have to add specific outbound rules for any traffic that's allowed in b/c that traffic will automatically be allowed back out.

* Network ACLs are stateless. When you go and do a network ACL, you're going to have to open up port 80, both Inbound and Outbound

* If you create an inbound rule allowing traffic in, that traffic is automatically allowed back out again

> You cannot deny or block specific IP addresses using **Security Groups**, instead use **Network Access Control Lists** (NACLs)

* You can only block or deny specific IP addresses w/ NACLs.

> You can specifically allow rules, but you cannot deny rules in a security group

* By default, they deny everything, but then you go in and allow. You can't put a specific deny rule on a security group

* You can do deny rules w/ a Network Access Control Lists

![Fig. 2 NACLs vs. Security Groups](../../../../img/SAA-CO2/virtual-private-cloud/security-groups/diagram-ii.png)
