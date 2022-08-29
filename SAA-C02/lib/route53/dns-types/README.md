# DNS Types

1. An advertising company is currently working on a proof of concept project that automatically provides SEO analytics for its clients. Your company has a VPC in AWS that operates in a dual-stack mode in which IPv4 and IPv6 communication is allowed. You deployed the application to an Auto Scaling group of EC2 instances with an Application Load Balancer in front that evenly distributes the incoming traffic. You are ready to go live but you need to point your domain name (tutorialsdojo.com) to the Application Load Balancer.

In Route 53, which record types will you use to point the DNS name of the Application Load Balancer? (Select TWO.)

[x] Alias w/ a type "AAAA" record set

[ ] Alias w/ a type "CNAME" record set

[ ] Alias w/ a type of "MX" record set

[x] Alias w/ a type "A" record set

[ ] Non-Alias w/ a type "A" record set

**Explanation**: The correct answers are: **Alias with a type "AAAA"** record set and **Alias with a type "A" record set**.

To route domain traffic to an ELB load balancer, use Amazon Route 53 to create an alias record that points to your load balancer. An alias record is a Route 53 extension to DNS. It's similar to a CNAME record, but you can create an alias record both for the root domain, such as tutorialsdojo.com and for subdomains, such as portal.tutorialsdojo.com. (You can create CNAME records only for subdomains.) To enable IPv6 resolution, you would need to create a second resource record, tutorialsdojo.com ALIAS AAAA -> myelb.us-west-2.elb.amazonnaws.com, this is assuming your Elastic Load Balancer has IPv6 support.

> **Non-Alias with a type "A" record set** is incorrect because you only use Non-Alias with a type “A” record set for IP addresses.

> **Alias with a type "CNAME" record set** is incorrect because you can't create a CNAME record at the zone apex. For example, if you register the DNS name tutorialsdojo.com, the zone apex is tutorialsdojo.com.

> **Alias with a type of “MX” record set** is incorrect because an MX record is primarily used for mail servers. It includes a priority number and a domain name, for example: 10 mailserver.tutorialsdojo.com.

<br />
