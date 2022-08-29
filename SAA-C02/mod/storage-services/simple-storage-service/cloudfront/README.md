# CloudFront

## What is CloudFront?

A content delivery network (CDN) is a system of distributed servers (network) that deliver webpages and other web content to a user based on the geographic locations of the user, the origin of the webpage, and a content delivery server.

## What is a CDN and why use one?

For caching, a CDN will reduce the load on an application origin and improve the experience of the requestor by delivering a local copy of the content from a nearby cache edge, or Point of Presence (PoP). The application origin is off the hook for opening the connection and delivering the content directly as the CDN takes care of the heavy lifting. The end result is that the application origins don’t need to scale to meet demands for static content.

![Fig. 1 CloudFront CDN](../../../../../img/SAA-CO2/storage-services/simple-storage-service/cloudfront/fig01.png)

Amazon CloudFront is a fast content delivery network (CDN) service that securely delivers data, videos, applications, and APIs to customers globally with low latency, high transfer speeds, all within a developer-friendly environment. CloudFront is integrated with AWS – both physical locations that are directly connected to the AWS global infrastructure, as well as other AWS services.

* *CDN* stands for Content Delivery Network

* Large distribution of caching servers

* Routes viewers to the best location using geolocation

* Caches appropriate content on the edge

* Accelerates dynamic content

* Provides scalability and performance of applications

## Application: Acceleration

* CloudFront latency-based routing

* Collapse multiple requests for the same object back to the origin

* TCP window scaling

* Persistent TCP connections to origin

* AWS backbone network

* SSL/TLS optimizations

## CloudFront Service Components

![Fig. 1 CloudFront Service Components](../../../../../img/SAA-CO2/storage-services/simple-storage-service/cloudfront/fig02.png)

## Key terminology

**Edge Location**: this is the location where content will be cached. This is separate to an AWS Region/Availability Zone (AZ).

**Origin**: this is the origin of all the files that the CDN will distribute. This can be an S3 Bucket, an EC2 Instance, an Elastic Load Balancer, or Route53.

**Distribution**: this is the name given the CDN which consists of a collection of Edge Locations.

Amazon CloudFront can be used to deliver your entire website, including dynamic, static, streaming, and interactive content using a network of edge locations. Requests for your content are automatically routed to the nearest edge location, so content is delivered w/ the best possible performance.

Two different types of distribution:

* **Web Distribution**: typically used for websites (HTTP, HTTPS data)

* **RTMP**: Used for Media Streaming

> ### Use cases
>
> * Static Asset Caching
>
> * Live and On-Demand Video Streaming
>
> * Security and DDoS Protection
>
> * Dynamic and Customized Content
>
> * API Acceleration
>
> * Software Distribution

## CloudFront Components: Behaviors

### 1. Path pattern matching

Vary behavior based on path pattern matching

* Route requests to specific origins

* Set HTTP Protocol

* Set HTTP Methods

* Set header options

* Set caching options

* Set cookie and query string forwarding

* Restrict access

* Set compression

### 2. Origin selection

Set up one-to-many origins, AWS or Custom Resource as origin

### 3. Headers

GET, HEAD, OPTIONS, PUT, POST, PATCH, DELETE

* Forward request headers to the origin

* Cache based on header values

* Set object caching TTLs

* Device detection

* None: Optimized

* Whitelist: Specify headers to forward

* All: Dynamic content, no caching

![Fig. 2 Cloudfront Behaviors, Headers](../../../../../img/SAA-CO2/storage-services/simple-storage-service/cloudfront/fig03.png)

### 4. Query strings/cookies

* Customize web content

* Appears after the `?` character in the web request

* Cache based on cookies

* Forward cookies to your origin

* Forward a whitelist of cookies

### 5. Signed URL

* Restrict access to content

* Subscription content, digital rights, etc.

* Canned and custom policies

* Application creates signed URL

* CloudFront caches based on Signed URL or Signed Cookie

![Fig. 3 Signed URL](../../../../../img/SAA-CO2/storage-services/simple-storage-service/cloudfront/fig04.png)

### 6. SSL certificates

* Custom SSL options

* SNI custom CCL

* Dedicated IP custom SSL

### 7. Protocol enforcement

* HTTP/S requests to origin server

* Customize content based on protocol

* Match viewer

### 8. Time to Live (TTL)

* Short TTL = dynamic content

* Long TTL = static content

* Reduce load on origin

* If-Modified-Since

* Min, Max, Default TTL's

### 9. GZIP compression

* Faster downloads

* Compress files up to 10,000,000 bytes in size

* Custom origin is already configured to compress files

### 10. Restrictions, Errors, Tags

![Fig. 4 Restrictions, Errors, Tags](../../../../../img/SAA-CO2/storage-services/simple-storage-service/cloudfront/fig05.png)

## Pricing Components

* Gigabytes transferred

* Request Rates (HTTP, HTTPS)

    * GET, HEAD

    * PUT, POST, PATCH, OPTIONS, DELETE

* Custom SSL Certificate

## Example Architecture

CloudFront is integrated with AWS – both physical locations that are directly connected to the AWS global infrastructure, as well as other AWS services. CloudFront works seamlessly with services, including AWS Shield for DDoS mitigation, Amazon S3, Elastic Load Balancing or Amazon EC2 as origins for your applications, and Lambda@Edge to run custom code closer to customers’ users and to customize the user experience. Lastly, if you use AWS origins such as Amazon S3, Amazon EC2 or Elastic Load Balancing, you don’t pay for any data transferred between these services and CloudFront.

![Fig. 5 Example Architecture](../../../../../img/SAA-CO2/storage-services/simple-storage-service/cloudfront/fig06.png)

## Lambda@Edge

* Lambda@Edge is an extension of AWS Lambda that allows you to run Node.js code at global AWS locations

* Bring your own code to the Edge and customize your content very close to your users, improving end-user experience.

> * No servers to manage
>
> * Continuous scaling
>
> * Globally distributed
>
> * Never pay for idle - no cold servers

### CloudFront Triggers for Lambda@Edge Functions

![Fig. 6 CloudFront CDN Architecture](../../../../../img/SAA-CO2/storage-services/simple-storage-service/cloudfront/fig07.png)

![Fig. 7 CloudFront Triggers for Lambda@Edge Functions](../../../../../img/SAA-CO2/storage-services/simple-storage-service/cloudfront/fig08.png)

* All Lambda@Edge invocations are synchronous

* Request events

    * URI and header modifications can change the object being requested

    * Viewer request can change the object being requested from the CloudFront cache and the origin

    * Origin request can change the object or path pattern being requested from the origin

* Response events

    * Origin response can modify what is cached and generate cacheable responses to be returned to the viewer

    * Viewer response can change what is returned to the viewer

### Lambda@Edge Functionality

* Read and write access to headers, URIs, and cookies across all triggers

* Ability to generate custom responses from scratch

* Access to make network calls to external resources

### Authorization at the Edge

* Inspect cookies or custom headers to authenticate clients right at the Edge

* Enforce paywalls at the Edge to gate access to premium content to only authenticated viewers

### Limited Access to Content

Enforce timed access to content at the Edge

* Make a call to an external authentication server to confirm if a user's session is still valid

* Forward valid requests to the origin, and serve redirects to new users to login pages

### Recap: Using Lambda@Edge

* Modify response header

* CloudFront response generation

* CloudFront HTTP redirect

* A/B testing

* Simple remote call at origin-facing hooks

* Cacheable static content generation

* Content generation w/ remote calls

### Response Generation at the Edge

* Generate an HTTP response to end-user requests arriving at AWS locations

* Generate customized error pages and static websites directly from Edge locations

* Combine content drawn from multiple external resources to dynamically build websites at the Edge

### Write once, run everywhere

* Automatically deployed to the AWS network of 105 Edge locations

* Requests are routed to the locations closest to your end users across the world

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
