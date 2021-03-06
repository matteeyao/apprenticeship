# VPC Flow Logs

> **What are VPC Flow Logs?**
>
> VPC Flow Logs is a feature that enables you to capture information about the IP traffic going to and from network interfaces in your VPC. Flow log data is stored using Amazon CloudWatch Logs. After you've created a flow log, you can view and retrieve its data in Amazon CloudWatch Logs.

Stores all the network traffic that's going on within our VPC

## VPC Flow Logs Levels

> **Flow logs can be created at 3 levels**;
>
> * VPC
>
> * Subnet
>
> * Network Interface Level

## Learning summary

> **Remember the following**;
>
> * You cannot enable flow logs for VPCs that are peered w/ your VPC unless the peer VPC is in your account.
>
> * You can tag flow logs.
>
> * After you've created a flow log, you cannot change its configuration; for example, you can't associate a different IAM role w/ the flow log.

So you can't have flow logs across multiple AWS accounts. You can have them across multiple VPCs, but they have to be within the same account.

> **Not all IP Traffic is monitored**;
>
> * Traffic generated by instances when they contact the Amazon DNS server. If you use your own DNS server, then all traffic to that DNS server is logged.
>
> * Traffic generated by a Windows instance for Amazon Windows license activation.
>
> * Traffic to and from 169.254.169.254 for instance metadata.
>
> * **DHCP** traffic.
>
> * Traffic to the reserved IP address for the default VPC router.
