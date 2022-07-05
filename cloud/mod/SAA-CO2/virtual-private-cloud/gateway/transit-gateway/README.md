# AWS Transit Gateway

A single point in which all of our different network connections can connect into. So whether it be our VPN connections, our AWS BPCs, or even Direct Connect, it all hits Transit Gateway and then operates on a hub and spoke model.

**Transit Gateway** allows for transitive peering so all of our VPCs can all communicate w/ each other.

> **Simplify your network topology (architecture)**

## Learning summary

> **Transit Gateway**
>
> * Allows you to have transitive peering between thousands of VPCs and on-premises data centers.
>
> * Works on a hub-and-spoke model.
>
> * Works on a regional basis, but you can have it across multiple regions.
>
> * You can use it across multiple AWS accounts using RAM (Resource Access Manager).
>
> * You can use route tables to limit how VPCs talk to one another.
>
> * Works w/ Direct Connect as well as VPN connections.
>
> * Supports **IP multicast** (not supported by any other AWS service).
