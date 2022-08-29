# AWS PrivateLink

**Privatelink** is a VPC endpoint service. Solves the problem of needing to expose an application to other VPCs and other AWS accounts. You could make the application public, but then you're using the internet and exposing your VPC.

## VPC Peering

VPC peering is a way to link multiple VPCs together and it allows direct communications between 2 isolated AWS accounts using their private IP addresses. VPC peers can span AWS accounts, and can also span AWS regions. That data shared is encrypted using the AWS global infrastructure. We can use a Virtual Private Network (VPN) and Direct Connect to link our VPCs to our on-premise network w/ a virtual or physical connection.

**Why use VPC peering?**

![Fig. 1 VPC Peering](../../../../img/SAA-CO2/virtual-private-cloud/privatelink/diagram-i.png)

* VPC peering is great for shared services running in a single VPC, and you want those shared services to be accessed from other VPCs or to other VPCs

* We seek to connect our VPC to a vendor or a partner system to access an application, or the reverse

* We seek to gives access to our VPC during a security audit

* We have certain requirements to split up an application into multiple isolated VPCs to limit our blast radius

**How does VPC peering work?**

VPC peering uses a peering connection, which is a network gateway very similar to our **internet gateway** or our **NAT gateway**. A VPC peering is simply a link between 2 VPCs. Remember that VPC peering can only peer 2 VPCs together, no more.

> **VPC Peering Exam Tips**
>
> ✓ **VPC peering is great for:**
>
>   * Shared services
>
>   * Connecting our VPC to a vendor
>
>   * Adding access for an audit
>
>   * Requirements for isolated VPCs
>
>   * Peering two VPCs - no more
>
> ✓ Use NACLs and security groups to **control access to our VPC peering connection**
>
> ✓ VPC peering **does not allow** transitive peering. **Transitive routing** is using a peering connection to connect through one VPC to get to another VPC. VPC peering can only occur between two VPCs, so if you need to connect to another VPC, you must set up that other peering connection. AWS does have a service called **Transit Gateway** that allows us to set up transitive peering.

## Opening your services in a VPC to another VPC

Let's say we had an application that sits both within our public subnet and private subnet, and we want to make this application available to other services in another VPC.

The first option would be to open up the VPC where the application is hosted to the internet itself.

Another way is to use VPC peering, but if we're using hundreds of different VPCs, we'd have to open up peering connection to each and every one of those other VPCs. That is a lot of management overhead and VPC peers must be set up to each VPC and that will also expose other applications, etc in the VPC to other VPCs. We could also set up a **Privatelink** instead and this is the most secure and scalable way to expose a service to tens or hundreds of other VPCs.

![Fig. 1 Endpoint service](../../../../img/SAA-CO2/virtual-private-cloud/privatelink/diagram-ii.png)

This does not require VPC peering, internet gateway, NAT gateway, etc.

To create this, we're going to need a network load balancer to associate w/ the application service and then we create a new ENI in our target VPC. We then link the 2 VPCs privately and this links our network load balancer to the elastic network interface.

**Classic Link** allows us to link EC2 classic instances to a VPC in our account in the same region. To create a link between our EC2 classic instances w/ our VPC, we have to associate the VPC security groups w/ an EC2 classic instance and this enabled communication between our EC2 Classic Instance and instances in our VPC uses private IPv4 addresses. So **Classic Link** removes the need to make use of public IPv4 addresses or elastic IP addresses to enable communications between instances in the platforms.

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
