# Security Groups

If you make a rule change to a security group, that change takes effect immediately

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
