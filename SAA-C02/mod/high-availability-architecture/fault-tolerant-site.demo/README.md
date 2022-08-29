# Building a Fault-Tolerant WordPress Site

## Getting started

1. Setup S3 buckets

2. Create CloudFront distribution

3. **Networking & Content Delivery** ▶︎ **VPC** ▶ Create security groups

4. Provision RDS instances

5. Create a role allowing us to communicate w/ S3

Bootstrap script:

```script
#!/bin/bash
yum install amazon-linux-extras httpd -y 
amazon-linux-extras install php7.2 -y
yum install httpd -y
systemctl start httpd
systemctl enable httpd
cd /var/www/html
wget https://wordpress.org/latest.tar.gz
tar -xzf latest.tar.gz
cp -r wordpress/* /var/www/html/
rm -rf wordpress
rm -rf latest.tar.gz
chmod -R 755 /var/www/html/*
chown -R apache:apache /var/www/html/*
```

## Setting up EC2

We're going to be placing our EC2 instance behind an Application Load Balancer and connect our Application Load Balancer to Route53.

SSH into the EC2 server:

```zsh
$ ssh ec2-user@<PUBLIC_IPV4_ADDRESS> -i <KEY_NAME_KP>.pem 
```

Elevate privileges to root:

```zsh
$ sudo su
```

Navigate to the `html` directory:

```zsh
$ cd /var/www/html
```

Perform a URL rewrite on our CloudFront distribution and sync http access file to S3.

Set `AllowOverride` in Apache (httpd) from `None` to `All`.

Move our EC2 instance behind our Application Load Balancer.

## Adding Resilience and Autoscaling

```zsh
$ cd /etc
$ nano crontab
```

> [!NOTE]
> Crontab is similar to Scheduled Tasks for Windows. Runs commands on a set time interval.

```zsh
*/1 * * * * root aws s3 sync --delete s3://<S3_BUCKET_NAME>
```

```zsh
$ service crond restart
```

```zsh
$ cd /var/www/html
$ cat hello.txt
```

Create an Amazon Machine Image (AMI) to be used in our Autoscaling Group, which will sit behind our Application Load Balancer. When clients visit our website, Route53 will send them to our Application Load Balancer and move onto our EC2 instances, which will be polling S3 every minute, attempting to download a local copy of the files on the S3 bucket.

```zsh
*/1 * * * * root aws s3 sync --delete /var/www/html s3://<S3_CODE_BUCKET_NAME>
*/1 * * * * root aws s3 sync --delete /var/www/html/wp-content/uploads s3://<S3_MEDIA_BUCKET_NAME>
```

```zsh
$ nano crontab
$ cd /var/www/html
$ echo "This is a TEST" > test.txt
$ service crond restart
$ service httpd status
```

**Create Auto Scaling Group** ▶︎ **Launch Configuration**

Set **IAM role** to `S3ForWP`

```script
#!/bin/bash
yum update -y
aws s3 sync --delete s3://<YOUR_S3_BUCKET_NAME> /var/www/html
```

## Cleaning up

Test failover from one Availability Zone to another using RDS.

Simulate a failover in RDS by rebooting an instance.

## CloudFormation

**Services** ▶︎ **Management & Governance** ▶︎ **CloudFormation**

Select "Use a sample template" ▶︎ "WordPress blog"

> **CloudFormation**
>
> * Is a way of completely scripting your cloud environment
>
> * Quick Start is a bunch of CloudFormation templates already built by AWS Solutions Architects allowing you to create complex environments very quickly.
