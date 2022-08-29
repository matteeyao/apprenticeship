# Network Load Balancer

1. A company has developed public APIs hosted in Amazon EC2 instances behind an Elastic Load Balancer. The APIs will be used by various clients from their respective on-premises data centers. A Solutions Architect received a report that the web service clients can only access trusted IP addresses whitelisted on their firewalls.

What should you do to accomplish the above requirement?

[ ] Associate an Elastic IP address to a Network Load Balancer.

[ ] Create a CloudFront distribution whose origin points to the private IP addresses of your web servers.

[ ] Associate an Elastic IP address to an Application Load Balancer.

[ ] Create an Alias Record in Route 53 which maps to the DNS name of the load balancer.

**Explanation**: A **Network Load Balancer** functions at the fourth layer of the Open Systems Interconnection (OSI) model. It can handle millions of requests per second. After the load balancer receives a connection request, it selects a target from the default rule's target group. It attempts to open a TCP connection to the selected target on the port specified in the listener configuration.

![Fig. 1 Network Load Balancer](../../../../img/SAA-CO2/security/network-load-balancer/fig01.jpeg)

Based on the given scenario, web service clients can only access trusted IP addresses. To resolve this requirement, you can use the Bring Your Own IP (BYOIP) feature to use the trusted IPs as Elastic IP addresses (EIP) to a Network Load Balancer (NLB). This way, there's no need to re-establish the whitelists with new IP addresses.

Hence, the correct answer is: **Associate an Elastic IP address to a Network Load Balancer**.

The option that says: **Associate an Elastic IP address to an Application Load Balancer** is incorrect because you can't assign an Elastic IP address to an Application Load Balancer. The alternative method you can do is assign an Elastic IP address to a Network Load Balancer in front of the Application Load Balancer.

The option that says: **Create a CloudFront distribution whose origin points to the private IP addresses of your web servers** is incorrect because web service clients can only access trusted IP addresses. The fastest way to resolve this requirement is to attach an Elastic IP address to a Network Load Balancer.

The option that says: **Create an Alias Record in Route 53 which maps to the DNS name of the load balancer** is incorrect. This approach won't still allow them to access the application because of trusted IP addresses on their firewalls.
