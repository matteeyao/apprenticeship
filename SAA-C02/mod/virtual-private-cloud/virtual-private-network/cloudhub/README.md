# AWS VPN CloudHub

## AWS VPN CloudHub diagram

Connect into our VPC using VPN connections. AWS VPN CloudHub enables for the most efficient architecture, allowing our users all to connect into a single virtual private gateway. Our customers in New York would then be able to talk to our VPC over an encrypted VPN and they would also be able to communicate from New York through our VPN CloudHub to customers in Miami. So, it's a hub-and-spoke architecture. So they have direct VPN connections via our VPC from New York to Miami, or Los Angeles.

Essentially, a single point of contact to connect our VPN infrastructure into.

## Learning summary

As its name implies, the AWS VPN CloudHub is only for VPNs and not for VPCs.

> **AWS VPN CloudHub**
>
> * If you have multiple sites, each w/ its own VPN connection, you can use AWS VPN CloudHub to connect those together.
>
> * Hub-and-spoke model.
>
> * Low cost; easy to manage.
>
> * It operates over the public internet, but all traffic between the customer gateway and the AWS VPN CloudHub is encrypted.

If you come across a scenario talking about managing our multiple sites w/ VPNs, consider VPN CloudHub.
