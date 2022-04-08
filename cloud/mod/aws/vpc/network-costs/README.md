# AWS Network Costs

Cost optimization for our VPC architecture. Need to know, at a high level, how AWS network costs work.

## Example Network Cost diagram

So let's say we've got our user and they want to connect into a VPC. Every time they speak to a web server inside our VPC, the traffic coming in is going to be free. So they're doing a get request or something. That initial request coming into the VPC is going to be framed. Your web server is then going to go ahead and connect up to a MySQL database server. Now, if it's using a private IP address and it's in the same availability zone, then that data is going to be free. If, however, you're connecting in across another availability zone, you will be charged one cent per gig, and again, these are just placeholder costs. It depends on the region, depends on the different regions in the world. So the cost can vary, but let's just use an example here of 1 cent per gig. So you're going to pay an amount of money for communication across private IP addresses from one availability zone to the other and, if you're going outside your availability zones, if you're using a public IP address, that's then going to traverse the internet and, for example, it's going to be twice as expensive than doing it through an internal IP address across your availability zones and for the reasons for that is you're using the AWS backbone network.

When you're connecting from availability zone A to zone B using the private IP address but if you're using the public IP address, you're coming out of the VPC. You're going across the internet and you're coming back in. So you're going to incur a larger cost doing it that way and let's say we've got VPC B, which is in another region as well and we need to communicate from VPC A in one region to VPC B in another region. You are going to pay into region costs, so it is going to be more expensive than communicating from one availability zone to the other, within a single region. It's essentially going to cost you more to do region-to-region into communications between your VPCs.

## Learning summary

> **AWS Network Costs**
>
> * Use private IP addresses over public IP addresses to save on costs. This then utilizes the AWS backbone network.
>
> * If you want to cut all network costs, group your EC2 instances in the same Availability Zone and use private IP addresses. This will be cost-free, but make sure to keep in mind single point of failure issues.

So if you're availability zone goes down, you are going to lose your entire application.
