# CloudFront

1. You have created a CloudFront web distribution to give global users low latency access to videos and images. After a successful pilot test, you receive notification that you must be able to filter traffic requests based on a range of IP addresses. Which of the following will satisfy this requirement w/ the least effort?

[ ] Create a Lambda@Edge function

[x] Create a Web Access Control List

[ ] Enable geoproximity routing

[ ] Modify your CloudFront web distribution settings

**Explanation**: Creating a Lambda@Edge function requires more effort than creating a Web Access Control List. Geoproximity routing within Route 53 is not by IP address. You can only configure whitelisting and blacklisting by geolocation and country in CloudFront web distribution settings, not by IP ranges.

<br />

2. A company runs an online media site, hosted on-premises. An employee posted a product review that contained videos and pictures. The review went viral, and the company needs to handle the resulting spike in website traffic

What action would provide an immediate solution?

[ ] Redesign the website to use Amazon API Gateway and use AWS Lambda to deliver content

[ ] Add server instances using Amazon EC2 and use Amazon Route 53 w/ a failover routing policy.

[x] Serve the images and videos using an Amazon CloudFront distribution created using the news site as the origin.

[ ] Use Amazon ElastiCache for Redis for caching and reducing the load requests from the origin.

**Explanation**: The Amazon Route 53 w/ a failover routing policy is not helpful in this scenario. You'd typically use Amazon ElastiCache for Redis for caching data from databases to reduce latency.

<br />

3. A popular social media website uses a CloudFront web distribution to serve their static contents to their millions of users around the globe. They are receiving a number of complaints recently that their users take a lot of time to log into their website. There are also occasions when their users are getting HTTP 504 errors. You are instructed by your manager to significantly reduce the user's login time to further optimize the system.

Which of the following options should you use together to set up a cost-effective solution that can improve your application's performance? (Select TWO.)

[x] Customize the content that the CloudFront web distribution delivers to your users using Lambda@Edge, which allows your Lambda functions to execute the authentication process in AWS locations closer to the users.

[ ] Use multiple and geographically disperse VPCs to various AWS regions then create a transit VPC to connect all of your resources. In order to handle the requests faster, set up Lambda functions in each region using the AWS Serverless Application Model (SAM) service.

[ ] Deploy your application to multiple AWS regions to accommodate your users around the world. Set up a Route 53 record w/ latency routing policy to route incoming traffic to the region that provides the best latency to the user.

[ ] Configure your origin to add a `Cache-Control max-age` directive to your objects, and specify the longest practical value for `max-age` to increase hit ratio of your CloudFront distribution.

[x] Set up an origin failover by creating an origin group w/ two origins. Specify one as the primary origin and the other as the second origin which CloudFront automatically switches to when the primary origin returns specific HTTP status code failure responses.

**Explanation**: Lambda@Edge lets you run Lambda functions to customize the content that CloudFront delivers, executing the functions in AWS locations closer to the viewer. The functions run in response to CloudFront events, without provisioning or managing servers. You can use Lambda functions to change CloudFront requests and responses at the following points:

* After CloudFront receives a request from a viewer (viewer request)

* Before CloudFront forwards the request to the origin (origin request)

* After CloudFront receives the response from the origin (origin response)

* Before CloudFront forwards the response to the viewer (viewer response)

In the given scenario, you can use Lambda@Edge to allow your Lambda functions to customize the content that CloudFront delivers and to execute the authentication process in AWS locations closer to the users. In addition, you can set up an origin failover by creating an origin group with two origins with one as the primary origin and the other as the second origin which CloudFront automatically switches to when the primary origin fails. This will alleviate the occasional HTTP 504 errors that users are experiencing.

> **Use multiple and geographically disperse VPCs to various AWS regions then create a transit VPC to connect all of your resources. In order to handle the requests faster, set up Lambda functions in each region using the AWS Serverless Application Model (SAM) service** is incorrect because of the same reason provided above. Although setting up multiple VPCs across various regions which are connected with a transit VPC is valid, this solution still entails higher setup and maintenance costs. A more cost-effective option would be to use Lambda@Edge instead.

> The option that says: **Configure your origin to add a `Cache-Control max-age` directive to your objects, and specify the longest practical value for `max-age` to increase the cache hit ratio of your CloudFront distribution** is incorrect because improving the cache hit ratio for the CloudFront distribution is irrelevant in this scenario. You can improve your cache performance by increasing the proportion of your viewer requests that are served from CloudFront edge caches instead of going to your origin servers for content. However, take note that the problem in the scenario is the sluggish authentication process of your global users and not just the caching of the static objects.

> The option that says: **Deploy your application to multiple AWS regions to accommodate your users around the world. Set up a Route 53 record with latency routing policy to route incoming traffic to the region that provides the best latency to the user** is incorrect. Although this may resolve the performance issue, this solution entails a significant implementation cost since you have to deploy your application to multiple AWS regions. Remember that the scenario asks for a solution that will improve the performance of the application with **minimal cost**.

<br />

4. A web application is using CloudFront to distribute their images, videos, and other static contents stored in their S3 bucket to its users around the world. The company has recently introduced a new member-only access to some of its high quality media files. There is a requirement to provide access to multiple private media files only to their paying subscribers without having to change their current URLs.

Which of the following is the most suitable solution that you should implement to satisfy this requirement?

[x] Use Signed Cookies to control who can access the private files in your CloudFront distribution by modifying your application to determine whether a user should have access to your content. For members, send the required `Set-Cookie` headers to the viewer which will unlock the content only to them.

[ ] Create a Signed URL w/ a custom policy which only allows the members to see the private files.

[ ] Configure your CloudFront distribution to use Field-Level Encryption to protect your private data and only allow access to members.

[ ] Configure your CloudFront distribution to use Match Viewer as its Origin Protocol Policy which will automatically match the user request. This will allow access to the private content if the request is a paying member and deny it if it is not a member.

**Explanation**: CloudFront signed URLs and signed cookies provide the same basic functionality: they allow you to control who can access your content. If you want to serve private content through CloudFront and you're trying to decide whether to use signed URLs or signed cookies, consider the following:

Use **signed URLs** for the following cases:

* You want to use an Real-Time Messaging Protocol (RTMP) distribution. Signed cookies aren't supported for RTMP distributions.

* You want to restrict access to individual files, for example, an installation download for your application.

* Your users are using a client (for example, a custom HTTP client) that doesn't support cookies.

Use **signed cookies** for the following cases:

* You want to provide access to multiple restricted files, for example, all of the files for a video in HLS format or all of the files in the subscribers' area of a website.

* You don't want to change your current URLs.

> The option that says: **Configure your CloudFront distribution to use Match Viewer as its Origin Protocol Policy which will automatically match the user request. This will allow access to the private content if the request is a paying member and deny it if it is not a member** is incorrect because a Match Viewer is an Origin Protocol Policy which configures CloudFront to communicate with your origin using HTTP or HTTPS, depending on the protocol of the viewer request. CloudFront caches the object only once even if viewers make requests using both HTTP and HTTPS protocols.

> The option that says: **Create a Signed URL with a custom policy which only allows the members to see the private files** is incorrect because Signed URLs are primarily used for providing access to individual files, as shown on the above explanation. In addition, the scenario explicitly says that they don't want to change their current URLs which is why implementing Signed Cookies is more suitable than Signed URL.

> The option that says: **Configure your CloudFront distribution to use Field-Level Encryption to protect your private data and only allow access to members** is incorrect because Field-Level Encryption only allows you to securely upload user-submitted sensitive information to your web servers. It does not provide access to download multiple private files.

<br />

5. A company has clients all across the globe that access product files stored in several S3 buckets, which are behind each of their own CloudFront web distributions. They currently want to deliver their content to a specific client, and they need to make sure that only that client can access the data. Currently, all of their clients can access their S3 buckets directly using an S3 URL or through their CloudFront distribution. The Solutions Architect must serve the private content via CloudFront only, to secure the distribution of files.

Which combination of actions should the Architect implement to meet the above requirements? (Select TWO.)

[x] Restrict access to files in the origin by creating an origin access identity (OAI) and give it permission to read the files in the bucket.

[ ] Use S3 pre-signed URLs to ensure that only their client can access the files. Remove permission to use Amazon S3 URLs to read the files for anyone else.

[x] Require the users to access the private content by using special CloudFront signed URLs or signed cookies.

[ ] Enable the Origin Shield feature of the Amazon CloudFront distribution to protect the files from unauthorized access.

[ ] Create a custom CloudFront function to check and ensure that only their clients can access the files.

**Explanation**: Many companies that distribute content over the Internet want to restrict access to documents, business data, media streams, or content that is intended for selected users, for example, users who have paid a fee. To securely serve this private content by using CloudFront, you can do the following:

* Require that your users access your private content by using special CloudFront signed URLs or signed cookies.

* Require that your users access your Amazon S3 content by using CloudFront URLs, not Amazon S3 URLs. Requiring CloudFront URLs isn't necessary, but it is recommended to prevent users from bypassing the restrictions that you specify in signed URLs or signed cookies. You can do this by setting up an origin access identity (OAI) for your Amazon S3 bucket. You can also configure the custom headers for a private HTTP server or an Amazon S3 bucket configured as a website endpoint.

All objects and buckets by default are private. The pre-signed URLs are useful if you want your user/customer to be able to upload a specific object to your bucket, but you don't require them to have AWS security credentials or permissions.

You can generate a pre-signed URL programmatically using the AWS SDK for Java or the AWS SDK for .NET. If you are using Microsoft Visual Studio, you can also use AWS Explorer to generate a pre-signed object URL without writing any code. Anyone who receives a valid pre-signed URL can then programmatically upload an object.

Hence, the correct answers are:

* Restrict access to files in the origin by creating an origin access identity (OAI) and give it permission to read the files in the bucket.

* Require the users to access the private content by using special CloudFront signed URLs or signed cookies.

> The option that says: **Create a custom CloudFront function to check and ensure that only their clients can access the files** is incorrect. CloudFront Functions are just lightweight functions in JavaScript for high-scale, latency-sensitive CDN customizations and not for enforcing security. A CloudFront Function runtime environment offers submillisecond startup times which allows your application to scale immediately to handle millions of requests per second. But again, this can't be used to restrict access to your files.

> The option that says: **Enable the Origin Shield feature of the Amazon CloudFront distribution to protect the files from unauthorized access** is incorrect because this feature is not primarily used for security but for improving your origin's load times, improving origin availability, and reducing your overall operating costs in CloudFront.

> The option that says: **Use S3 pre-signed URLs to ensure that only their client can access the files. Remove permission to use Amazon S3 URLs to read the files for anyone else** is incorrect. Although this could be a valid solution, it doesn't satisfy the requirement to serve the private content via CloudFront only to secure the distribution of files. A better solution is to set up an origin access identity (OAI) then use Signed URL or Signed Cookies in your CloudFront web distribution.

<br />
