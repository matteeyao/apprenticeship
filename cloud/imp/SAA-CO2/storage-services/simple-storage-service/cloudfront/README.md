# CloudFront

1. You have created a CloudFront web distribution to give global users low latency access to videos and images. After a successful pilot test, you receive notification that you must be able to filter traffic requests based on a range of IP addresses. Which of the following will satisfy this requirement w/ the least effort?

[ ] Create a Lambda@Edge function

[x] Create a Web Access Control List

[ ] Enable geoproximity routing

[ ] Modify your CloudFront web distribution settings

**Explanation**: Creating a Lambda@Edge function requires more effort than creating a Web Access Control List. Geoproximity routing within Route 53 is not by IP address. You can only configure whitelisting and blacklisting by geolocation and country in CloudFront web distribution settings, not by IP ranges.

2. A company runs an online media site, hosted on-premises. An employee posted a product review that contained videos and pictures. The review went viral, and the company needs to handle the resulting spike in website traffic

What action would provide an immediate solution?

[ ] Redesign the website to use Amazon API Gateway and use AWS Lambda to deliver content

[ ] Add server instances using Amazon EC2 and use Amazon Route 53 w/ a failover routing policy.

[x] Serve the images and videos using an Amazon CloudFront distribution created using the news site as the origin.

[ ] Use Amazon ElastiCache for Redis for caching and reducing the load requests from the origin.

**Explanation**: The Amazon Route 53 w/ a failover routing policy is not helpful in this scenario. You'd typically use Amazon ElastiCache for Redis for caching data from databases to reduce latency.
