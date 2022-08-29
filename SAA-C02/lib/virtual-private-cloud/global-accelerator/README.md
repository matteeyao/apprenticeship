# AWS Global Accelerator

1. A software development company has hundreds of Amazon EC2 instances with multiple Application Load Balancers (ALBs) across multiple AWS Regions. The public applications hosted in their EC2 instances are accessed on their on-premises network. The company needs to reduce the number of IP addresses that it needs to regularly whitelist on the corporate firewall device.

Which of the following approach can be used to fulfill this requirement?

[ ] Use AWS Global Accelerator and create an endpoint group for each AWS Region. Associate the Application Load Balancer from each region to the corresponding endpoint group.

[ ] Use AWS Global Accelerator and create multiple endpoints for all the available AWS Regions. Associate all the private IP addresses of the EC2 instances to the corresponding endpoints.

[ ] Launch a Network Load Balancer w/ an associated Elastic IP address. Set the ALBs in multiple Regions as targets.

[ ] Create a new Lambda function that tracks the changes in the IP addresses of all ALBs across multiple AWS Regions. Schedule the function to run and update the corporate firewall every hour using Amazon CloudWatch Events.

**Explanation**: **AWS Global Accelerator** is a service that improves the availability and performance of your applications with local or global users. It provides static IP addresses that act as a fixed entry point to your application endpoints in a single or multiple AWS Regions, such as your Application Load Balancers, Network Load Balancers, or Amazon EC2 instances.

When the application usage grows, the number of IP addresses and endpoints that you need to manage also increase. AWS Global Accelerator allows you to scale your network up or down. AWS Global Accelerator lets you associate regional resources, such as load balancers and EC2 instances, to two static IP addresses. You only whitelist these addresses once in your client applications, firewalls, and DNS records.

With AWS Global Accelerator, you can add or remove endpoints in the AWS Regions, run blue/green deployment, and A/B test without needing to update the IP addresses in your client applications. This is particularly useful for IoT, retail, media, automotive, and healthcare use cases in which client applications cannot be updated frequently.

If you have multiple resources in multiple regions, you can use AWS Global Accelerator to reduce the number of IP addresses. By creating an endpoint group, you can add all of your EC2 instances from a single region in that group. You can add additional endpoint groups for instances in other regions. After it, you can then associate the appropriate ALB endpoints to each of your endpoint groups. The created accelerator would have two static IP addresses that you can use to create a security rule in your firewall device. Instead of regularly adding the Amazon EC2 IP addresses in your firewall, you can use the static IP addresses of AWS Global Accelerator to automate the process and eliminate this repetitive task.

Hence, the correct answer is: **Use AWS Global Accelerator and create an endpoint group for each AWS Region. Associate the Application Load Balancer from each region to the corresponding endpoint group.**

> The option that says: **Use AWS Global Accelerator and create multiple endpoints for all the available AWS Regions. Associate all the private IP addresses of the EC2 instances to the corresponding endpoints** is incorrect. It is better to create one endpoint group instead of multiple endpoints. Moreover, you have to associate the ALBs in AWS Global Accelerator and not the underlying EC2 instances.

> The option that says: **Create a new Lambda function that tracks the changes in the IP addresses of all ALBs across multiple AWS Regions. Schedule the function to run and update the corporate firewall every hour using Amazon CloudWatch Events** is incorrect because this approach entails a lot of administrative overhead and takes a significant amount of time to implement. Using a custom Lambda function is actually not necessary since you can simply use AWS Global Accelerator to achieve this requirement.

> The option that says: **Launch a Network Load Balancer with an associated Elastic IP address. Set the ALBs in multiple Regions as targets** is incorrect. Although you can allocate an Elastic IP address to your ELB, it is not suitable to route traffic to your ALBs across multiple Regions. You have to use AWS Global Accelerator instead.

<br />
