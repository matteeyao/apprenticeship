# AWS PrivateLink

## Opening your services in a VPC to another VPC

Let's say we had an application that sits both within our public subnet and private subnet, and we want to make this application available to other services in another VPC.

The first option would be to open up the VPC where the application is hosted to the internet itself.

Another way is to use VPC peering, but if we're using hundreds of different VPCs, we'd have to open up peering connection to each and every one of those other VPCs.

## Sharing applications across VPCs

> To open our applications up to other VPCs, we can either:
>
> **Open the VPC up to the internet**:
>
> * Security considerations; everything in the public subnet is public.
>
> * A lot more to manage.
>
> **Use VPC peering**:
>
> * You will have to create and manage many different peering relationships.
>
> * The whole network will be accessible. This isn't good if you have multiple applications within your VPC.

## Opening your services in a VPC to another VPC using PrivateLink

> [x] The best way to expose a service VPC to tens, hundreds, or thousands of customer VPCs
>
> [x] Doesn't require VPC peering; no route tables, NAT, IGWs, etc.
>
> [x] Requires a Network Load Balancer on the service VPC and an ENI on the customer VPC

![AWS PrivateLink](https://www.qubole.com/wp-content/uploads/2020/09/image1-3.png)

Best approach to expose a service VPC to hundreds of customer VPCs and it doesn't require VPC peering. There's no Route Tables, NAT gateways, internet gateways, etc.

All that is required is a Network Load Balancer on the service VPC and an elastic network interface on the customer's VPC.

Take the static IP address of the Network Load Balancer and open it up to our Elastic Network Interface by creating an AWS PrivateLink. Essentially, AWS PrivateLink is a way of sharing your applications within your own VPC to multiple VPCs at once.

## Learning summary

> **AWS PrivateLink**
>
> * If you see a question asking about peering VPCs to tens, hundreds, or thousands of customer VPCs, think of AWS PrivateLink.
>
> * Doesn't require VPC peering; no Route Tables, NAT, IGWs, etc.
>
> * Requires a Network Load Balancer on the service VPC and an ENI on the customer VPC
