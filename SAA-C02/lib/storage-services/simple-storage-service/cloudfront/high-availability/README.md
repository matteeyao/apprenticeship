# S3 High Availability enabled through Origin Group

1. A Solutions Architect needs to launch a web application that will be served globally using Amazon CloudFront. The application is hosted in an Amazon EC2 instance which will be configured as the origin server to process and serve dynamic content to its customers.

Which of the following options provides high availability for the application?

[ ] Provision two EC2 instances deployed in different Availability Zones and configure them to be part of an origin group.

[ ] Launch an Auto Scaling group of EC2 instances and configure it to be part of an origin group.

**Explanation**: An origin is a location where content is stored, and from which CloudFront gets content to serve to viewers. **Amazon CloudFront** is a service that speeds up the distribution of your static and dynamic web content, such as .html, .css, .js, and image files, to your users. CloudFront delivers your content through a worldwide network of data centers called edge locations. When a user requests content that you're serving with CloudFront, the user is routed to the edge location that provides the lowest latency (time delay), so that content is delivered with the best possible performance.

You can also set up CloudFront with origin failover for scenarios that require high availability. An origin group may contain two origins: a primary and a secondary. If the primary origin is unavailable or returns specific HTTP response status codes that indicate a failure, CloudFront automatically switches to the secondary origin. To set up origin failover, you must have a distribution with at least two origins.

The scenario uses an EC2 instance as an origin. Take note that we can also use an EC2 instance or a custom origin in configuring CloudFront. To achieve high availability in an EC2 instance, we need to deploy the instances in two or more Availability Zones. You also need to configure the instances to be part of the origin group to ensure that the application is highly available.

Hence, the correct answer is: **Provision two EC2 instances deployed in different Availability Zones and configure them to be part of an origin group.**

> The option that says: **Launch an Auto Scaling group of EC2 instances and configure it to be part of an origin group** is incorrect because you must have at least two origins to set up an origin failover in CloudFront. In addition, you can't directly use a single Auto Scaling group as an origin.

<br />
