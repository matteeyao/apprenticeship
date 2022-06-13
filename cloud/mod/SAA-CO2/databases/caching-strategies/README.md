# Caching Strategies on AWS

## Caching services

> The following services have caching capabilities:
>
> * CloudFront

* Caches media files, videos, pictures, etc. at edge locations near your end user

> * API Gateway
>
> * ElastiCache - **Memcached** and **Redis**
>
> * DynamoDB Accelerator (DAX)

### Typical architecture

```txt
                                             ElastiCache
                                            /
User → CloudFront → API Gateway → Lambda/EC2 → RDS/DynamoDB
    \
     CloudFront → Origin (S3, EC2, etc.)
```

* The deeper you go into the flow, the more latency you're going to have.

* So if you can cache at the edge location, *CloudFront*, we will end up w/ less latency.

* If we can cache at *API Gateway*, we will still have less latency than going all the way down to the RDS or DynamoDB layer

* So the more caching you put up front, the lower your latency is for your end user.

## Learning summary

> Caching is a balancing act between **up-to-date**, **accurate information** and **latency**. We can use the following services to **cache on AWS**:
>
> 1. CloudFront
>
> 2. API Gateway
>
> 3. ElastiCache - **Memcached** and **Redis**
>
> 4. DynamoDB Accelerator (DAX)
