# ENI

Elastic Network Interface, essentially a virtual network card

An ENI is simply a virtual network card for your EC2 instances. It allows:

* A primary private IPv4 address from the IPv4 address range of your VPC

* One or more secondary private IPv4 addresses from the IPv4 address range of your VPC

* One Elastic IP address (IPv4) per private IPv4 address

* One public IPv4 address

* One or more IPv6 addresses

* One or more security groups

* A MAC address

* A source/destination check flag

* A description

### Use cases

Scenarios for Network Interfaces:

* Create a management network

  * Normal use: web servers, DB servers, etc

* Use network and security appliances in your VPC

* Create dual-homed instances w/ workloads/roles on distinct subnets

* Create a low-budget, high-availability solution

  * Basic adapter type for when you don't have any high-performance requirements

## Amazon EC2 Networking

* You can attach more than one ENI to an EC2 instance.

* Can only be attached to subnets within the same AZ as the EC2 instance.

* Used to communicate from one EC2 instance to another EC2 instance and communicate w/ EBS.

## Learning summary

> In the exam you will be given different scenarios and you will be asked to choose whether you should use an ENI, EN, or EFA.

> * **ENI**
>
>   * For basic networking. Perhaps you need a separate management network to your production network or a separate logging network and you need to do this at low cost. In this scenario use multiple ENIs for each network.

> * **Enhanced Network**
>
>   * For when you need speeds between 10Gbps and 100Gbps. Anywhere you need reliable, high throughput
