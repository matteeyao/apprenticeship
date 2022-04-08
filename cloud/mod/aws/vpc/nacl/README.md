# Network Access Control Lists vs Security Groups

Whenever you create a new Network ACL, the default is to deny everything on Inbound and Outbound Rules.

**exit** ▶︎ **exit** will take us back to our `WebServer01`.

In our public web server, execute `status httpd.service` to view Apache which shouldn't be installed. Type `yum install httpd -y` ▶︎ `chkconfig httpd on` ▶︎ run `service httpd start` ▶︎ `cd /var/www/html`

When we create our VPC, a Network ACL is created by default, our default Network ACL. Every time we add subnet to our VPC, it's going to be associated w/ our default Network ACL. You can then associate the subnet w/ a new Network ACL, but a Subnet itself can only be associated w/ one network ACL at any given time.

That being said, Network ACLs can have multiple subnets with each one.

Rules are evaluated in numerical order. If you have a DENY first, that's always going to deny, even if you ALLOW it later. So, if you're going to deny a specific IP address, you want that rule to be evaluated first.

When you're using Sub-Network ACLs, they're always going to be evaluated before Security Groups. So if you deny a specific port on your Network ACL, it's never even going to reach your Security Group. So, Network ACLs always act first before Security Groups.

To use `yum update` on the public server, enable ephemeral ports on Inbound and Outbound rules.

## Learning summary

> **Remember the following for your exam**
>
> * Your VPC automatically comes w/ a default network ACL, and by default it allows all outbound and inbound traffic.
>
> * You can create custom network ACLs. By default, each custom network ACL denies all inbound and outbound traffic until you add rules.
>
> * Each subnet in your VPC must be associated w/ a Network ACL. If you don't explicitly associate a subnet w/ a Network ACL, the subnet is automatically associated w/ the default network ACL.
>
> * Block IP Addresses using Network ACLs, not Security Groups
>
> * You can associate a network ACl w/ multiple subnets; however, a subnet can be associated w/ only one network ACL at a time. When you associate a network ACL w/ a subnet, the previous association is removed.
>
> * Network ACLs contain a numbered list of rules that is evaluated in order, starting w/ the lowest numbered rule.
>
> * Network ACLs have separate inbound and outbound rules, and each rule can either allow or deny traffic.
>
> * Network ACLs are stateless; responses to allowed inbound traffic are subject to the rules for outbound traffic (and vice versa).

Recall, if you're going to deny a IP address, place the deny rule in front of our allow rules.
