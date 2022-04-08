# VPC Endpoints

> **A VPC Endpoint**:
>
> A VPC endpoint enables you to privately connect your VPC to supported AWS services and CPV endpoint services powered by PrivateLink w/o requiring an internet gateway, NAT device, VPN connection, or AWS Direct Connect connection. Instances in your VPC do not require public IP addresses to communicate w/ resources in the service. Traffic between your VPC and the other service does not leave the Amazon network.
>
> Endpoints are virtual devices. They are horizontally scaled, redundant, and highly available VPC components that allow communication between instances in your VPC and services without imposing availability risks or bandwidth constraints on your network traffic.

Traverses our traffic w/o having to leave the Amazon network. We won't need to go over the internet.

> **There are two types of VPC endpoints**:
>
> * Interface Endpoints
>
> * Gateway Endpoints

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
