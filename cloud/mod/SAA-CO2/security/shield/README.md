# AWS Shield 

This service can help protect against Denial of Service attacks. This is a malicious attempt to affect the availability of a targeted system, such as a website or an application to legitimate end-users. Shield is another security product that sits at the edge of AWS's Perimeter Network, which can help mitigate these attacks.

So in the case of a distributed Denial of Service or DDoS attack, the attacker uses multiple compromised or controlled sources to generate the attack.

> **What is AWS Shield?**
>
> * Protects against distributed denial-of-service (DDoS) attacks

| **AWS Shield Standard**                                          | **AWS Shield Advanced**                                                                     |
|------------------------------------------------------------------|---------------------------------------------------------------------------------------------|
| * Automatically enabled for all customers at **no cost**         | * $3,000 per month, per org                                                                 |
| * Protects against common layer 3 and 4 attacks                  | * Enhanced protection for EC2, ELB, CloudFront, Global Accelerator, Route 53                |
|   * SYN/UDP floods                                               | * Business and Enterprise support customers get 24x7 access to the DDoS Response Team (DRT) |
|   * Reflection attacks                                           | * DDoS cost protection                                                                      |
| * Stopped a 2.3 Tbps DDoS attack for three days in February 2020 |                                                                                             |

* **AWS Shield Standard**

  * When you use WAF w/ CloudFront or an application load balancer, you get Shield standard at no additional cost

  * This will help protect you against common layer three and layer four attacks

    * SYN/UDP floods

      * The most common attack method is when you have UDP packets that flood your servers. Every packet sent to the targeted server needs to. Those are the rules for UDP. This eats through the servers connectivity and sometimes other resources. And it makes regular connections to the server impossible. SYN floods work similarly to UDP floods, but they work on TCP rather than UDP. Basically, they make your servers wait for an answer, but no one's coming. It does this by basically leaving a huge number of connections half open, waiting for handshakes that never complete.

    * Reflection attacks
      
      * Shield can also protect against reflection attacks. These are another kind of UDP attack where the source IP address of packets is spoofed, where the victim will end up receiving a large volume of response packets it never requested and Shield has absolutely proven effective.

* **AWS Shield Advanced**

  * Provides an enhanced level of protection, but it's $3,000 per month per AWS organization and this provides enhanced protection for EC2, Elastic Load Balancer, CloudFront Global Accelerator, and Route 53 resources.

  * DDoS cost protection - a form of insurance against any attacks that would otherwise result in impact to your AWS bill.
