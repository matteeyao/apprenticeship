# Network Load Balancer

A Network Load Balancer is used for applications that need extreme network performance and static IP.

* Operates on Layer 4 (TCP protocol)

* Designed to handle millions of request per second

* Maintain ultra-low latencies during queries

* Static and Elastic IP support

* Source IP preserved

* Integrated w/ Auto Scaling and Amazon ECS

* Health checks at TCP/application level

## Network Load Balancer Static IP

* Automatically gets assigned a single IP per Availability Zone

* Assign an Elastic IP address per Availability Zone to get Static IP

* Help w/ white-listing for firewalls

## Assign Elastic IP Addresses

![Fig. 1 Assign Elastic IP Addresses](../../../../../img/SAA-CO2/virtual-private-cloud/elastic-load-balancer/application-load-balancer/diag02.png)

## Availability Zone Fail-over

![Fig. 2 Availability Zone Fail-over](../../../../../img/SAA-CO2/virtual-private-cloud/elastic-load-balancer/network-load-balancer/diag02.png)

## IP Address Target Type

Use any IPv4 address from the load balancer's VPC CIDR for targets within load balancer's VPC

Use any IP address from the RFC 6598 range (100.64.0.0/10) and in RFC 1918 ranges (10.0.0.0/8, 172.16.0.0/12, and 192.168.0.0/16) for targets located outside the load balancer's VPC (on-premises targets reachable over Direct Connect)
