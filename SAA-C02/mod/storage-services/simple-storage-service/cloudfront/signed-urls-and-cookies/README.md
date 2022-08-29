# CloudFront Signed URLs and Cookies

Decide whether or not you're using a CloudFront signed URL or cookie, or if you're using an S3 signed URL.

## What we've learned so far

* **Edge Location**: this is the location where content will be cached. This is different than an **AWS Region/AZ**. We have a lot more edge locations than we do regions or availability zones.

* **Distribution**: this is the name given the **CDN**, which consists of a collection of edge locations. A way of caching large files at locations that's close to your end-users. The distribution will be connected up to an origin and this is the origin of all the files that the CDN is going to distribute.

    * **Web Distribution**: typically used for websites

    * **RTMP**: used for media streaming

* **Origin**: this is the origin of all the files the CDN will distribute. This can be either an S3 bucket, an EC3 instance, an Elastic Load Balancer (w/ a whole bunch of EC2 instances), or Route 53.

## Restricting content access

### Use CloudFront signed URLs or Signed Cookies

1. A signed URL is for individual files. **1 file = 1 URL**.

2. A signed cookie is for multiple files. **1 cookie = multiple files**.

## Signed URL policies

When we create a signed URL or signed cookie, we attach a policy

The policy can include:

* URL expiration

* IP ranges

* Trusted signers (which AWS accounts can create **signed URLs**)

## How signed URLs work

Origin Access Identity (OAI). Your users will only be able to access CloudFront, which then uses OAI to access the origin.

Client uses authentication and authorization to access the application, which then uses a SDK to generate a **signed URL** or a **signed cookie**. CloudFront along w/ its edge locations, returns that back to the client. The client can then use CloudFront to access the files in S3, EC2, or elastic load balancer, etc.

## CloudFront signed URL features

* Can have different origins. Does not have to be **EC2**

* Can utilize **caching** features

* Key-pair is account wide and managed by the root user

* Can filter by date, path, IP address, expiration, etc.

## S3 signed URL features

* Issues a request as the **IAM user** who creates the presigned URL

    * So you have all the same permissions as the IAM user who has created that signed URL

* Limited **lifetime**

So for the exam, if CloudFront signed URL versus an S3 signed URL is discussed, just think about whether or not your users can access S3. If they are using OAI through CloudFront, then they can't. So you'd be using a CloudFront signed URL, but if they can access the S3 bucket directly, and it's just an individual object, then you probably want an S3 signed URL.

## Learning summary

* Use signed **URLs/cookies** when you want to secure content so that only the people you authorize are able to access it.

* A signed URL is for individual files. **1 file = 1 URL**

* A signed cookie is for multiple files. **1 cookie = multiple files**

* If your origin is EC2, then use CloudFront

    * If your origin is going to be S3, and you've only got a single file in there, then use a S3 signed URL instead of a CloudFront signed URL.
