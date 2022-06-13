# CloudFront

## What is CloudFront?

A content delivery network (CDN) is a system of distributed servers (network) that deliver webpages and other web content to a user based on the geographic locations of the user, the origin of the webpage, and a content delivery server.


## Key terminology

**Edge Location**: this is the location where content will be cached. This is separate to an AWS Region/Availability Zone (AZ).

**Origin**: this is the origin of all the files that the CDN will distribute. This can be an S3 Bucket, an EC2 Instance, an Elastic Load Balancer, or Route53.

**Distribution**: this is the name given the CDN which consists of a collection of Edge Locations.

Amazon CloudFront can be used to deliver your entire website, including dynamic, static, streaming, and interactive content using a network of edge locations. Requests for your content are automatically routed to the nearest edge location, so content is delivered w/ the best possible performance.

Two different types of distribution:

* **Web Distribution**: typically used for websites

* **RTMP**: Used for Media Streaming


## Learning summary

* **Edge location**: this is the location where content will be cached. This is separate to an AWS Region/Availability Zone (AZ)

    * Edge locations are not just READ only - you can write to them too. (i.e. put an object on to them. We did that in the last module when we were demoing Transfer Acceleration)

* **Origin**: this is the origin of all the files that the CDN will distribute. This can be either an S3 Bucket, an EC2 Instance, and Elastic Load Balancer, or Route53.

* **Distribution**: this is the name given the CDN which consists of a collection of Edge Locations.

    * **Web Distribution**: typically used for websites.

    * **RTMP**: used for media streaming

* Objects are cached for the life of the **TTL (Time to Live.)**

    * That value is always in seconds

* You can clear cached objects by invalidating them, but you will be charged.

    * You can invalidate cached content, so if you've uploaded something and your users are still getting some old data and you've added new data, for example, you can go in and clear those cached objects, but you are going to be chared.

    * Referred to as invalidating the cache.
