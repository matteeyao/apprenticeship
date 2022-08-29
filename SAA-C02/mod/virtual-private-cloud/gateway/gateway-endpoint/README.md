# VPC Endpoints

> **A VPC Endpoint**:
>
> A VPC endpoint enables you to privately connect your VPC to supported AWS services and CPV endpoint services powered by PrivateLink w/o requiring an internet gateway, NAT device, VPN connection, or AWS Direct Connect connection. Instances in your VPC do not require public IP addresses to communicate w/ resources in the service. Traffic between your VPC and the other service does not leave the Amazon network.
>
> Endpoints are virtual devices. They are horizontally scaled, redundant, and highly available VPC components that allow communication between instances in your VPC and services without imposing availability risks or bandwidth constraints on your network traffic.

Traverses our traffic w/o having to leave the Amazon network. We won't need to go over the internet.

![Fig. 1 VPC Endpoints](../../../../img/SAA-CO2/virtual-private-cloud/endpoints/diagram-i.png)

**VPC endpoints** are gateway objects that we can create inside our VPC, similar to our **internet gateways** and **NAT gateways**. They are used to allow instances inside a VPC to connect w/ AWS public services w/o the need for a gateway. So no **internet gateway** or **NAT gateway** is needed.

> **There are two types of VPC endpoints**:
>
> * Interface Endpoints
>
> * Gateway Endpoints

![Fig. 2 Two types of VPC endpoints](../../../../img/SAA-CO2/virtual-private-cloud/endpoints/diagram-ii.png)

**Gateway endpoints**: some AWS services are public services and sit inside the AWS public zone. Sometimes we will want to connect to these public services-S3, DynamoDB-from a private instance or a private subnet that does not have access to the internet and doesn't have a NAT gateway setup.

**Interface endpoints** are used for everything else and you do have to pick the correct endpoint depending on the AWS service that you want to use.

Remember that gateway endpoints are highly available by default and are used for DynamoDB and S3. All other services use Interface endpoints. VPC endpoints can be restricted using policies. Gateway endpoints use routing, so they have an entry in our route tables, but interface endpoints use DNS.

Interface endpoints do not use policies, but use security groups. We can choose our default security group as it allows all access. For interface endpoints and DNS, you get DNS names for each availability zone. You also get a DNS name that is not AZ-specific. So if we do choose to enable private DNS names, then it will override the default public endpoint DNS name and it'll give it a private DNS endpoint name, so we can access this endpoint using private IPs. This is great for applications and no code change is needed by using the private DNS.

For the exam, remember that interface endpoints use DNS, not routing w/ the prefix lists like gateway endpoints use. Interface endpoints are for all other services besides S3 and DynamoDB. Again, there is no code change for applications if you enable private DNS.

Endpoints allow us to connect to public services using the private IP addresses that we have.

> **An interface endpoint is an elastic network interface w/ a private IP address that serves as an entry point for traffic destined to a supported service. The following services are supported**:
>
> * Amazon API Gateway
>
> * AWS CloudFormation
>
> * Amazon CloudWatch
>
> * Amazon CloudWatch Events
>
> * Amazon CloudWatch Logs
>
> * AWS CodeBuild
>
> * AWS Config
>
> * Amazon EC2 API
>
> * Elastic Load Balancing API
>
> * AWS Key Management Service
>
> * Amazon Kinesis Data Streams
>
> * Amazon SageMaker and Amazon SageMaker Runtime
>
> * Amazon SageMaker Notebook Instance
>
> * AWS Secrets Manager
>
> * AWS Security Token Service
>
> * AWS Service Catalog
>
> * Amazon SNS
>
> * Amazon SQS
>
> * AWS Systems Manager
>
> * Endpoint services hosted by other AWS accounts
>
> * Supported AWS Marketplace partner services

Essentially, we attach an elastic network interface (ENI) to an EC2 instance, and that will essentially allow you to communicate to these services using the Amazon internal network. There won't be any need to traverse the internet.

## Gateway Endpoints

> **Currently Gateway Endpoints Support**
>
> * Amazon S3
>
> * DynamoDB

## VPC EndPoints in Action

**Services** ▶︎ **IAM** ▶︎ **Roles** ▶︎ `Create role`

`Attach permissions policies`: `AmazonS3FullAccess`

**EC2** ▶︎ **Instance Settings** ▶︎ **Attach/Replace IAM Role**

`IAM role`: `S3AdminAccess`

**Networking & Content Deliver** ▶︎ **VPC** ▶︎ **Network ACLs** ▶︎ **Edit subnet associations**

SSH into the EC2 instance inside our private subnet.

```zsh
# ssh into public instance
ssh ec2-user@18.216.240.182 -i MyNewKP.pem

# Elevate privileges to root
sudo su

# Should still have MyPvKey.pem in here
ls

# ssh into private instance
ssh ec2-user@10.0.2.235 -i MyPvKey.pem

# Elevate privileges to root
sudo su

# List all available buckets
aws s3 ls
```

Delete the route out to our NAT Gateway in our main Route Table.

To create VPC endpoint: **VPC** ▶︎ **Endpoints** ▶︎ **Create Endpoint**

```zsh
# List s3 buckets
aws s3 ls --region us-east-2
```

## Learning summary

> **A VPC Endpoint**:
>
> A VPC endpoint enables you to privately connect your VPC to supported AWS services and VPC endpoint services powered by PrivateLink without requiring an internet gateway, NAT device, VPN connection, or AWS Direct Connect connection. Instances in your VPC do not require public IP addresses to communicate w/ resources in the services. Traffic between your VPC and the other service does not leave the Amazon network.
>
> Endpoints are virtual devices. They are horizontally scaled, redundant, and highly available VPC components that allow communication between instances in your VPC and services without imposing availability risks or bandwidth constraints on your network traffic.

> **There are two types of VPC endpoints**:
>
> * Interface Endpoints
>
> * Gateway Endpoints

> **Currently Gateway Endpoints Support**:
>
> * Amazon S3
>
> * DynamoDB
