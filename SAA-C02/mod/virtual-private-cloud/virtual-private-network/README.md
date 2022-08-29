# How AWS Site-to-Site VPN works

A Site-to-Site VPN connection offers two VPN tunnels between a virtual private gateway or a transit gateway on the AWS side, and a customer gateway (which represents a VPN device) on the remote (on-premises) side.

A Site-to-Site VPN connection consists of the following components.

**Contents**

* Virtual private gateway

* Transit gateway

* Customer gateway device

* Customer gateway

## Virtual private gateway

A *virtual private gateway* is the VPN concentrator on the Amazon side of the Site-to-Site VPN connection. You create a virtual private gateway and attach it to the VPC from which you want to create the Site-to-Site VPN connection.

![Fig. 1 VPN w/ Virtual Private Gateway](https://docs.aws.amazon.com/vpn/latest/s2svpn/images/vpn-how-it-works-vgw.png)

When you create a virtual private gateway, you can specify the private Autonomous System Number (ASN) for the Amazon side of the gateway. If you don't specify an ASN, the virtual private gateway is created w/ the default ASN (64512). You cannot change the ASN after you've created the virtual private gateway. To check the ASN for your virtual private gateway, view its details in the **Virtual Private Gateways** screen in the Amazon VPC console, or use the `describe-vpn-gateways` AWS CLI command.

## Transit gateway

A transit gateway is a transit hub that you can use to interconnect your virtual private clouds (VPC) and on-premises networks. You can create a Site-to-Site VPN connection as an attachment on a transit gateway.

![Fig. 2 VPN w/ Transit gateway](https://docs.aws.amazon.com/vpn/latest/s2svpn/images/vpn-how-it-works-tgw.png)

You can modify the target gateway of a Site-to-Site VPN connection from a virtual private gateway to a transit gateway.

## Customer gateway device

A *customer gateway device* is a physical device or software application on your side of the Site-to-Site VPN connection. You configure the device to work w/ the Site-to-Site VPN connection.

By default, your customer gateway device must bring up the tunnels for your Site-to-Site VPN connection by generating traffic and initiating the Internet Key Exchange (IKE) negotiation process. You can configure your Site-to-Site VPN connection to specify tat AWS must initiate the IKE negotiation process instead.

## Customer gateway

A *customer gateway* is a resource that you create in AWS that represents the customer gateway device in your on-premises network. When you create a customer gateway, you provide information about your device to AWS.

To use Amazon VPC w/ a Site-to-Site VPN connection, you or your network administrator must also configure the customer gateway device or application in your remote network. When you create the Site-to-Site VPN connection, we provide you w/ the required configuration information and your network administrator typically performs this configuration.
