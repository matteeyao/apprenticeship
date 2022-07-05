# Reducing Security Threats

> **Bad Actors**
>
> * Typically automated processes
>
> * Content scrapers
>
> * Bad bots
>
> * Fake user agent
>
> * Denial of service (DoS)

> **Benefits of Preventing Bad Actors**
>
> * Reduce security threats
>
> * Lower overall costs

You can lower overall costs b/c you no longer have to serve traffic to unintended audiences.

## AWS Shared Responsibility Model

* AWS is responsible for the security of the Cloud (global infrastructure, which comprises hardware, software, networking, and facilities that run AWS services)

* AWS is also responsible for the security configurations of its products that are considered to be managed services such as Amazon RDS.

* Customers are responsible for security in the Cloud, which refers to security measures the customer implements and operates related to the security of customer content and applications that use AWS services.

* Customers retain control of the security they implement to protect their own content, platform, applications, systems, and networks, just as they would for applications in an onsite data center.

## Network Access Control List (NACL)

You can use Network Access Control Lists to allow or deny certain IPs or ranges of IPs into your subnet.

In addition to a NACL, you can also employ what's called a host-based firewall.

This runs directly on your EC2 instance and can serve as yet another layer of defense against bad actors.

> **Hose-based** firewall e.g., firewalld, iptables, ufw, Windows Firewall

## Application Load Balancer

W/ an Application Load Balancer, the incoming connection from your bad actor will terminate at the ALB itself, so your EC2 instance itself will be completely unaware of that origin IP.

A host-based firewall would be ineffective in this case.

One additional measure that you could take is to allow only the ALB security group access to the EC2 security group. However, this won't completely block traffic to the ALB originating from that bad actor `1.2.3.4`. We will still have to use a NACL in this case.

Keep in mind, when using an ALB, the connection from that bad actor is going to terminate at the ALB, not at the EC2 instance.

![Fig. 1 Application Load Balancer](../../../../img/aws/security/reducing-security-threats/application-load-balancer.png)

## Network Load Balancer

Unlike w/ an ALB, w/ a Network Load Balancer, the traffic doesn't terminate at the NLB. It passes directly through it directly to your EC2 instance. The client IP, the IP address of that bad actor is visible from end-to-end. Since the Client IP is visible end to end, a firewall block on the EC2 instance would be possible, but it is better to block at the NACL. A WAF rule could be used as well.

![Fig. 2 Network Load Balancer](../../../../img/aws/security/reducing-security-threats/network-load-balancer.png)

## Web Application Firewall

![Fig. 3 Web Application Firewall](../../../../img/aws/security/reducing-security-threats/web-application-firewall.png)

So, when might you want to use WAF and when might you want to use a NACL? If you want to block common exploits such as SQL injections or cross-site scripting attacks, then use WAF. WAF operates on layer seven and can inspect that level of traffic for these types of exploits. If you want to block an IP or range of IPs, then you'd want to use a NACL which operates on layer four. Keep in mind that hackers will often use multiple IPs in different ranges to attack you. If you rely solely on NACL rules, you'll have a hard time keeping up. So if you're operating a public web application, you really want to prefer WAF in these instances and speaking of public web applications, you might have a configuration that involves CloudFront.

## WAF + CloudFront

Just w/ the ALB, you can also attach wire to your CloudFront distribution. W/ CloudFront, similar to ALB, the client's connection terminates at CloudFront. That client IP is not visible to your NACL, only the CloudFront IP is passed along to the NACL. So blocking your bad actor's IP in a NACL when sitting behind a CloudFront distribution will be ineffective. In these cases, you want to attach a WAF to your CloudFront distribution and use the IP blocking and filtering options. Additionally, if you find that you're getting abuse from a particular country, you can use CloudFront Geo Match feature to block that country's traffic altogether.

![Fig. 4 WAF + CloudFront](../../../../img/aws/security/reducing-security-threats/cloudfront.png)
