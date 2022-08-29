# Public, Private, Multi, and Hybrid Environments

## Understanding Public and Private AWS Services

A **Public Cloud** environment is a cloud environment that is available to the public that meets the criteria of cloud computing.

**Private Cloud** has dedicated services for your environment and those services are actually on premise. For AWS, one of the Private Cloud services is **AWS Outposts**. The **Private Cloud** still has to meet Cloud computing criteria and has to be dedicated directly to your business.

A **Hybrid Cloud** environment is the use of public and private services meeting the cloud computing criteria. Differs from **Hybrid cloud computing deployment model** where you use your on-premise data center and a cloud service provider.

AWS determines whether a service is private or public by its network.

An AWS public service is a service that can be connected to from anywhere there is an internet connection. Inside of AWS, we have the AWS **Public Zone** which can communicate openly w/ the public internet. And AWS public services sit inside this **Public Zone**. This is not the public internet. Recall that AWS has its own global network and the AWS **Public Zone** sits inside AWS but it sits beside or adjacent to the public internet.

Inside of AWS and inside of the AWS **Public Zone** is a network that is attached directly to the public internet. So any AWS service that is considered a public service sits and runs from this **Public Zone** w/ the network attached to the open internet.

An example of an AWS public service is **S3**. So when users are connecting to an S3 bucket, they're connecting through the public internet.

An AWS private service resides inside the AWS **Private Zone**. It has no direct connection to the open internet or to the AWS **Public Zone**. Inside the AWS **Private Zone**, we can create isolated environments such as a **VPC**, or **EC2** instances. By default, these are completely isolated, but we can add permissions for our EC2 instance or for our VPC to access the **Public Zone** or the public internet. We simply have to configure the routing or make the instance public.

Recall that, by default, the only services that can access the services inside that private zone are the services inside that private zone, and by default, there are no permissions for the **Private Zone** except for your local routes.
