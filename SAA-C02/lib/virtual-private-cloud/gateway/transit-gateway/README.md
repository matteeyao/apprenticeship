# CloudHub

1. A company has established a dedicated network connection from its on-premises data center to AWS Cloud using AWS Direct Connect (DX). The core network services, such as the Domain Name System (DNS) service and Active Directory services, are all hosted on-premises. The company has new AWS accounts that will also require consistent and dedicated access to these network services.

Which of the following can satisfy this requirement with the LEAST amount of operational overhead and in a cost-effective manner?

[ ] Create a new AWS VPN CloudHub. Set up a Virtual Private Network (VPN) connection for additional AWS accounts.

[ ] Create a new Direct Connect gateway and integrate it w/ the existing Direct Connect connection. Set up a Transit Gateway between AWS accounts and associate it w/ the Direct Connect gateway.

[ ] Set up another Direct Connect connection for each and every new AWS account that will be added.

[ ] Set up a new Direct Connect gateway and integrate it w/ the existing Direct Connect connection. Configure a VPC peering connection between AWS accounts and associate it w/ Direct Connect gateway.

**Explanation**: **AWS Transit Gateway** provides a hub and spoke design for connecting VPCs and on-premises networks. You can attach all your hybrid connectivity (VPN and Direct Connect connections) to a single Transit Gateway consolidating and controlling your organization's entire AWS routing configuration in one place. It also controls how traffic is routed among all the connected spoke networks using route tables. This hub and spoke model simplifies management and reduces operational costs because VPCs only connect to the Transit Gateway to gain access to the connected networks.

![Fig. 1 AWS Transit Gateway](../../../../../img/SAA-CO2/virtual-private-cloud/gateway/transit-gateway/fig01.png)

By attaching a transit gateway to a Direct Connect gateway using a transit virtual interface, you can manage a single connection for multiple VPCs or VPNs that are in the same AWS Region. You can also advertise prefixes from on-premises to AWS and from AWS to on-premises.

The AWS Transit Gateway and AWS Direct Connect solution simplify the management of connections between an Amazon VPC and your networks over a private connection. It can also minimize network costs, improve bandwidth throughput, and provide a more reliable network experience than Internet-based connections.

Hence, the correct answer is: **Create a new Direct Connect gateway and integrate it with the existing Direct Connect connection. Set up a Transit Gateway between AWS accounts and associate it with the Direct Connect gateway.**

> The option that says: **Set up another Direct Connect connection for each and every new AWS account that will be added** is incorrect because this solution entails a significant amount of additional cost. Setting up a single DX connection requires a substantial budget and takes a lot of time to establish. It also has high management overhead since you will need to manage all of the Direct Connect connections for all AWS accounts.

> The option that says: **Create a new AWS VPN CloudHub. Set up a Virtual Private Network (VPN) connection for additional AWS accounts** is incorrect because a VPN connection is not capable of providing consistent and dedicated access to the on-premises network services. Take note that a VPN connection traverses the public Internet and doesn't use a dedicated connection.

> The option that says: **Set up a new Direct Connect gateway and integrate it with the existing Direct Connect connection. Configure a VPC peering connection between AWS accounts and associate it with Direct Connect gateway** is incorrect because VPC peering is not supported in a Direct Connect connection. VPC peering does not support transitive peering relationships.

<br />

2. A company has a VPC for its Human Resource department and another VPC located in different AWS regions for its Finance department. The Solutions Architect must redesign the architecture to allow the finance department to access all resources that are in the human resource department, and vice versa. An Intrusion Prevention System (IPS) must also be integrated for active traffic flow inspection and to block any vulnerability exploits.

Which network architecture design in AWS should the Solutions Architect set up to satisfy the above requirement?

[ ] Launch an AWS Transit Gateway and add VPC attachments to connect all departments. Set up AWS Network Firewall to secure the application traffic traveling between the VPCs.

[ ] Create a Traffic Policy in Amazon Route 53 to connect the two VPCs. Configure the Route 53 Resolver DNS Firewall to do active traffic flow inspection and block any vulnerability exploits.

[ ] Establish a secure connection between the two VPCs using a NAT Gateway. Manage user sessions via the AWS the AWS Systems Manager Session Manager service.

[ ] Create a Direct Connect Gateway and add VPC attachments to connect all departments. Configure AWS Security Hub to secure the application traffic traveling between VPCs.

**Explanation**: A *transit gateway* is a network transit hub that you can use to interconnect your virtual private clouds (VPCs) and on-premises networks. As your cloud infrastructure expands globally, inter-Region peering connects transit gateways together using the AWS Global Infrastructure. Your data is automatically encrypted and never travels over the public internet.

A transit gateway attachment is both a source and a destination of packets. You can attach the following resources to your transit gateway:

* One or more VPCs

* One or more VPN connections

* One or more AWS Direct Connect gateways

* One or more Transit Gateway Connect attachments

* One or more transit gateway peering connections

AWS Transit Gateway deploys an elastic network interface within VPC subnets, which is then used by the transit gateway to route traffic to and from the chosen subnets. You must have at least one subnet for each Availability Zone, which then enables traffic to reach resources in every subnet of that zone. During attachment creation, resources within a particular Availability Zone can reach a transit gateway only if a subnet is enabled within the same zone. If a subnet route table includes a route to the transit gateway, traffic is only forwarded to the transit gateway if the transit gateway has an attachment in the subnet of the same Availability Zone.

Intra-region peering connections are supported. You can have different transit gateways in different Regions.

AWS Network Firewall is a managed service that makes it easy to deploy essential network protections for all of your Amazon Virtual Private Clouds (VPCs). The service can be setup with just a few clicks and scales automatically with your network traffic, so you don't have to worry about deploying and managing any infrastructure. AWS Network Firewall’s flexible rules engine lets you define firewall rules that give you fine-grained control over network traffic, such as blocking outbound Server Message Block (SMB) requests to prevent the spread of malicious activity.

AWS Network Firewall includes features that provide protections from common network threats. AWS Network Firewall’s stateful firewall can incorporate context from traffic flows, like tracking connections and protocol identification, to enforce policies such as preventing your VPCs from accessing domains using an unauthorized protocol. AWS Network Firewall’s intrusion prevention system (IPS) provides active traffic flow inspection so you can identify and block vulnerability exploits using signature-based detection. AWS Network Firewall also offers web filtering that can stop traffic to known bad URLs and monitor fully qualified domain names.

Hence, the correct answer is: **Launch a Transit Gateway and add VPC attachments to connect all departments. Set up AWS Network Firewall to secure the application traffic traveling between the VPCs.**

> The option that says: **Create a Traffic Policy in Amazon Route 53 to connect the two VPCs. Configure the Route 53 Resolver DNS Firewall to do active traffic flow inspection and block any vulnerability exploits** is incorrect because the Traffic Policy feature is commonly used in tandem with the geoproximity routing policy for creating and maintaining records in large and complex configurations. Moreover, the Route 53 Resolver DNS Firewall can only filter and regulate outbound DNS traffic for your virtual private cloud (VPC). It can neither do active traffic flow inspection nor block any vulnerability exploits.

> The option that says: **Establish a secure connection between the two VPCs using a NAT Gateway. Manage user sessions via the AWS Systems Manager Session Manager service** is incorrect because a NAT Gateway is simply a Network Address Translation (NAT) service and can't be used to connect two VPCs in different AWS regions. This service allows your instances in a private subnet to connect to services outside your VPC but external services cannot initiate a connection with those instances. Furthermore, the AWS Systems Manager Session Manager service is meant for managing EC2 instances via remote SSH or PowerShell access. This is not used for managing user sessions.

> The option that says: **Create a Direct Connect Gateway and add VPC attachments to connect all departments. Configure AWS Security Hub to secure the application traffic travelling between the VPCs** is incorrect. An AWS Direct Connect gateway is meant to be used in conjuction with an AWS Direct Connect connection to your on-premises network to connect with a Transit Gateway or a Virtual Private Gateway. You still need a Transit Gateway to connect the two VPCs that are in different AWS Regions. The AWS Security Hub is simply a cloud security posture management service that automates best practice checks, aggregates alerts, and supports automated remediation. It's important to note that it doesn't secure application traffic just by itself.

<br />
