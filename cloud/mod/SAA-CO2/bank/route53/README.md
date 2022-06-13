# Route 53

1. Which of the following Route 53 policies allow you to rout data to a second resource if the first is unhealthy, and route data to resources that have better performance?

**Failover Routing and Latency-based Routing**. Failover Routing and Latency-based Routing are the only two correct options, as they consider routing data based on whether the resource is healthy or whether one set of resources is more performant than another. Any answer containing location based routing (Geoproximity and Geolocation) cannot be correct in this case, as these types only consider where the client and resources are located before routing the data. They do not take into account whether a resource is online or slow. Simple Routing can also be discounted as it does not take into account the state of the resources.

2. True or False: There is a limit to the number of domain names that you can manage using Route 53.

**True and False**. W/ Route 53, there is a default limit of 20 domain names. However, this limit can be increased by contacting AWS support. The limit is 20 for new customers as of March 2021. If you have an existing account and your default limit is 50 now, it will remain at 50.

3. In AWS Route 53, which of the following are true?

[x] Alias Records provide a Route 53-specific extension to DNS functionality

[ ] Alias Records can point at any resources in AWS, but only within the same account

[ ] Alias Records can point at any resource w/ a Canonical Name.

[ ] Route 53 allows you to create a CNAME record that has the same name as the hosted zone (the zone apex)

[x] Route 53 allows you to create an Alias record at the top node of a DNS namespace (zone apex)

[ ] A CNAME record assigns an Alias name to an IP address.

Alias Records have special functions that are not present in other DNS servers. Their main function is to provide special functionality and integration into AWS services. Unlike CNAME records, they can also be sued at the Zone Apex, where CNAME records cannot. Alias Records can also point to AWS Resources that are hosted in other accounts by manually entering the ARN.

4. You have created a new subdomain for your popular website, and you need this subdomain to point to an Elastic Load Balancer using Route53. Which DNS record set type (or DNS extension type) could you create? (Choose 2).

[x] CNAME

[ ] A

[x] Alias

[ ] MX

An A record routes traffic using an IPv4 address

CNAME maps to the host name

An alias could be created for the ELB. Alias records provide a Route 53-specific extension to DNS functionality

5. Route 53 is named so b/c _.

**The `route` part of the Route 53 name came from a reference to Route 66. The `53` part came from the fact that the port for DNS is 53.**

6. Your company hosts 8 web servers all serving the same web content in AWS. They want Route 53 to serve traffic to random web servers. Which routing policy will meet this requirement, and provide the best resiliency?

**Multivalue Routing**. R53 Multivalue lets you respond to DNS queries w/ up to eight IP addresses of 'healthy' targets. Plus it will give a different set of 8 to different DNS resolvers. The R53 Simple policy will provide a list of multiple instances in random order, but Multivalue is the AWS preferred option for this type of service - [ref](https://docs.aws.amazon.com/Route53/latest/DeveloperGuide/routing-policy.html).

7. You are hosting websites in the eu-west-2 and ap-southeast-2 regions, and would like visitors from United Kingdom to see a different site than those in Australia. Which Routing Policy would help you to accomplish this?

**Geoproximity routing policy** and **Geolocation routing policy**. Geoproximity routing lets Amazon Route 53 route traffic to your resources based on the geographic location of your users and your resources. Geolocation routing lets you choose the resources that serve your traffic based on the geographic location of your users, meaning the location that DNS queries originate from. For example, you might want all queries from Europe to be routed to an ELB load balancer in the Frankfurt region.

8. You have an enterprise solution that operates Active-Active with facilities in Regions US-West and India. Due to growth in the Asian market you have been directed by the CTO to ensure that only traffic in Asia (between Turkey and Japan) is directed to the India Region. Which of these will deliver that result?

[ ] Route 53 - Weighted routing policy, calculate the proportion of customers in each and weight the policy to ensure that each location gets a fair load.

[x] Route 53 - Geolocation routing policy

[x] Route 53 - Geoproximity routing policy

[ ] Latency routing policy. This will ensure only customers that are close will go to the India installation.

[ ] CloudFront - a combination of blacklisting and whitelisting to control which countries go to which site

The instruction from the CTO is clear that that the division is based on geography. Latency based routing will approximate geographic balance only when all routes and traffic evenly supported which is rarely the case due to infrastructure and day night variations. You cannot combine blacklisting and whitelisting in CloudFront. Weighted routing is randomized and will not respect Geo boundaries. Geolocation is based on national boundaries and will meet the needs well. Geoproximity is based on Latitude & Longitude and will also provide a good approximation with potentially less configuration.

9. Route 53 is Amazon's DNS Service.

**True**. Route 53 is Amazon's highly available and scalable cloud DNS web service.
