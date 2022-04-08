# Creating a Basic VPC and Associated Components in AWS

## Learning Objectives

[ ] Create a vpc

[ ] Create an internet gateway, and connect it to the vpc

[ ] Create a public and private subnet in different availability zones

[ ] Create two route tables, and associate them with the correct subnet

[ ] Create two network access control lists (nacls), and associate each with the proper subnet

> **About this lab**
>
> AWS networking consists of multiple components, and understanding the relationship between the networking components is a key part of understanding the overall functionality and capabilities of AWS. In this hands-on lab, we will create a VPC w/ an internet gateway, as well as create subnets across multiple Availability Zones.

![Fig. 1 VPC and Associated Components](https://s3.amazonaws.com/assessment_engine/production/labs/2cc3cf9e-61ce-475d-a00e-03306e9ba285/lab_diagram_Lab_-_Creating_a_Basic_VPC_and_Associated_Components.001.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=ASIAVKPCGNLNRX37LT67%2F20220406%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20220406T171737Z&X-Amz-Expires=3600&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEA8aCXVzLWVhc3QtMSJHMEUCIQCm%2F7i2ohjCElqjGGE1qDN0x7%2BbIUnXfRt%2BrdX%2B7Q6QewIgekJeujRWXEKEANuOGNcyhqmgu43Onf3npKhG5rOgd%2BsqjAQIqP%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FARACGgwzNjYwODM0Njc5OTUiDMbhpeaiQvgPzKbpVCrgA3PhcClLaZ6xr0dSXZfu%2B2uth%2FbIm4IfWx4ND6dfBRKPq73AzQwZi4prUD2D3mYLhJ7iHuQ%2B01dkDu81yZK4UDEF6CjfNmxyBmnsOF1H5NQuCU1pjXd40VBrM23693hReOZalGrSP471J%2BCgfBZx7AovxqW99gNY7Pyays2H%2BHcQJvz4wr%2Bkq5zCa6eBZFcQC45zcErWJU2Za0HV7flIGECbRZJWG4PK8Fzf8cEq0IWrJSE6hX9fr8NqFK3THgntQGvzfZ%2BEmqUdEHp5qy08gxro4zvqexe6kdC%2FAiaMNyeyBdzVJnzTZTpHSow4pSt6XWdVJnxJr%2BCMZpIWKgJkYFiYshRMX1U2r4khVDioQcGFxzbitgopwt7HvIH9euUoq1rO7C%2FejPGfMWDTqphBFLpz6p1RjehppczJd9p5UlehR7904LFp%2BVvWWK6J50WtODiHL0rdV4XYTpXy5fSwXfudXdNcRqObeymj7u%2FRM2LNcZgml1SwclxrSV5lCqE%2BTLd3koPF6VwxGatfUV8BBZ0X%2FoEMqVv4XUqzkOt%2FooaPlYakKc3Va338gkS9KWfVKluhgTGYbLXBOIIihoG0HqsKTc95nNtAzxYIJD1q0PfOKw5mVUvHrPCnuODpHTWZTzDHyLaSBjqlAS5cMdz6vazlRJyK9w5ZPJMfpGrWE78hZ%2FJiRpRVp9%2BLlscQFR7hz98O5PkN2v7R8vJEPHDCG9skZwdAcObxl2z%2BsQdhCLV6Ri4e5IkbquUFaTIJI%2FtA6CEeVCkrORMDNQE%2B%2FKI5StR5ngVmHU64f%2FZx5xGvq83Vsoft30AvQQ4AU5uxHt9mDtojf4usH6Zfy0GakfK%2FY1xKd31gGWoSZP5GX1j%2F%2BQ%3D%3D&X-Amz-SignedHeaders=host&X-Amz-Signature=f69c9ce90ca9ef4151fd7c60bf87175e8804f5bb1597922dcb02dbce51cc4975)

Our goal for this hands-on lab is to set up our VPC so that the resources that reside in the public subnet will be accessible from the internet. We'll be creating a VPC in the `us-east-1` or Northern Virginia region. Once we've created that VPC, we're going to attach an **Internet Gateway** to the VPC.

We're going to create 2 different subnets in 2 different availability zones. One will be a public subnet and one will be a **Private Subnet**. We'll then attach a **Route Table** to the public subnet and ensure that there's a route to the **Internet Gateway**.

This will allow traffic to flow from our clients over the public internet and through the **Internet Gateway** over to the **Public Subnet**. But before the traffic can get into the **Public Subnet**, we need to update the network access control list, or **NACL**, to ensure the proper traffic is allowed into the **Public Subnet**.

Now this **Route Table** will also have an entry that will allow the **Public Subnet** to be able to communicate w/ the **Private Subnet**. In order to set up access for our **Private Subnet**, we're going to set up a second **Route Table**, and this **Route Table** will not have a route to the **Internet Gateway**, but will allow the **Private Subnet** to be able to communicate w/ resources in our **Public Subnet**. 

## Introduction

In this hands-on lab, we will create a VPC w/ an internet gateway, as well as create subnets across multiple Availability Zones.

## Solution

Log in to the live AWS environment using the credentials provided. Make sure you're in the N. Virginia (`us-east-1`) region throughout the lab.

## Create a VPC

> Let's create a virtual private cloud, a **VPC**, which serves as our network within AWS.

1. Navigate to the VPC dashboard.

2. Click **Your VPCs** in the left-hand menu.

3. Click **Create VPC**, and set the following values:

    * *Name tag*: **VPC1**

    * *IPv4 CIDR block*: **172.16.0.0/16**

4. Leave the *IPv6 CIDR block* and *Tenancy* fields as their default values.

5. Click **Create**.

> This VPC does not have an Internet Gateway. It has no Subnets and has one default Route Tables, but our own custom Route Table is best practice. It doesn't have any of the components we need to make the VPC functional.

## Create an Internet Gateway, and Connect it to the VPC

1. Click **Internet Gateways** in the left-hand menu.

2. Click **Create internet gateway**.

3. Give it a **Name tag** of "IGW".

4. Click **Create internet gateway**.

5. Once it's created, click **Actions** ▶︎ **Attach to VPC**

6. In the *Available VPCs* dropdown, select our **VPC1**.

7. Click **Attach internet gateway**

## Create a Public and Private Subnet in different Availability Zones

### Create Public Subnet

1. Click **Subnets** in the left-hand menu.

2. Click **Create subnet**, and set the following values:

    * *Name tag*: **Public1**

    * *VPC*: **VPC1**

    * *Availability Zone*: **us-east-1a**

    * *IPv4 CIDR block*: **172.16.1.0/24**

3. Click **Create**.

> Notice how this IPV4 CIDR block starts w/ the same 2 octets as the CIDR block for our VPC: `172.16`

### Create Private Subnet

1. Click **Create subnet**, and set the following values:

    * *Name tag*: **Private1**

    * *VPC*: **VPC1**

    * *Availability Zone*: **us-east-1b**

    * *IPv4 CIDR block*: **172.16.2.0/24**

2. Click **Create**.

> Recall that the public subnet was `1.0`

## Create two Route Tables, and associate them w/ the correct Subnet

### Create and Configure Public Route Table

> [!NOTE]
> The VPC has a default route table, but we will be creating custom route tables.

1. Click **Route Tables** in the left-hand menu.

2. Click **Create route table**, and set the following values:

    * *Name tag*: **PublicRT**

    * *VPC*: **VPC1**

3. Click **Create**.

4. W/ *PublicRT* selected, click the **Routes** tab on the page.

5. Click **Edit routes**.

6. Click **Add route**, and set the following values:

    * *Destination*: **0.0.0.0/0**

    * *Target*: **Internet Gateway** ▶︎ **IGW**

7. Click **Save routes**.

> Now, any traffic that's not destined for our local network is routed out to the Internet Gateway.

> What we've done so far is we've set up our VPC so that any traffic that comes through the Internet Gateway will go this public Route Table and this Route Table will then direct the traffic off to whichever subnet that'll be attached to it, which in this case, is going to be our Public Subnet.

8. Select **PublicRT**, then click the **Subnet Associations** tab.

9. Click **Edit subnet associations**.

10. Select our **Public1** subnet.

11. Click **Save**.

> The association is made so traffic into and out of the Public Subnet will look at this Route Table and see that it has the Internet Gateway attached, and it'll allow the traffic to route between the internet and our Public Subnet.

### Create and Configure Private Route Table

1. Click **Create route table**, and set the following values:

    * *Name tag*: **PrivateRT**

    * *VPC*: **VPC1**

2. Click **Create**.

3. Select **PrivateRT**, then click the **Subnet Associations** tab.

4. Click **Edit subnet associations**.

5. Select our **Private1** subnet.

6. Click **Save**.

> This associates the Route Table w/ this Private Subnet and b/c it only has a local route, meaning that it can only send traffic between the public and private subnet, it's not able to access the internet. Because the Public Subnet has the local Route Table entry, the Public Subnet is also able to communicate w/ the Private Subnet.

## Create two Network Access Control Lists (NACLs), and associate each w/ the proper Subnet

> [!NOTE]
> There's already a default NACL and we don't want to use the default NACL b/c, under **Inbound Rules**, all traffic is allowed. We don't want to necessarily allow all traffic to both subnets b/c if you take a look, both subnets are associated w/ this particular NACL. So we're going to create a new NACL.

### Create and configure public NACL

1. Click **Network ACLs** in the left-hand menu.

2. Click **Create network ACL**, and set the following values:

    * *Name tag*: **Public_NACL**

    * *VPC*: **VPC1**

3. Click **Create**.

> Anytime you use the default NACL, all traffic in and out of the associated subnets will automatically be allowed.

4. W/ *Public_NACL* selected, click the **Inbound Rules** tab below.

5. Click **Edit inbound rules**.

6. Click **Add Rule**, and set the following values:

    * *Rule #*: **100**

    * *Type*: **HTTP (80)**

    * *Port Range*: **80**

    * *Source*: **0.0.0.0/0**

    * *Allow / Deny*: **ALLOW**

7. Click **Add Rule**, and set the following values:

    * *Rule #*: **110**

    * *Type*: **SSH (22)**

    * *Port Range*: **22**

    * *Source*: **0.0.0.0/0**

    * *Allow / Deny*: **ALLOW**

> SSH is not typically a rule that you would want to create and allow all traffic to have access to. We're doing that in this lab so that you don't have to worry about determining your own personal IP address, and then adding that IP address into the configuration. But when you think about this in terms of setting up your own environment, you'd want to make sure that you restrict this to either your own personal address or to the addresses of the admins on your team.

8. Click **Save**.

9. Click the **Outbound Rules** tab.

10. Click **Edit outbound rules**.

> Recall NACLs are stateless. So unless we create those Outbound rules, the traffic won't be allowed back out of the Subnet.

11. Click **Add Rule**, and set the following values:

    * *Rule #*: **100**

    * *Type*: **Custom TCP Rule**

    * *Port Range*: **1024-65535**

    * *Source*: **0.0.0.0/0**

    * *Allow / Deny*: **ALLOW**

> Port Range `1024-65535` allows for what's called the ephemeral port range. The ephemeral port range says "whenever traffic is sent into your subnet, that traffic comes from a random port on the source computer. As that traffic comes into the Subnet, we have to send it back to the source computer on the same port. So that port is going to be one of the ports in this range. Make sure to have the entire range allowed or else the traffic can't return back to its original source reliably.

12. Click **Save**.

13. Click the **Subnet associations** tab.

14. Click **Edit subnet associations**.

15. Select the **Public1** subnet, and click **Save changes**.

> So now our public subnet is finally accessible to the internet. It's accessible b/c we have the Internet Gateway attached to the VPC. We have a Route Table that has a route to the Internet Gateway. We have our Subnet created, and our Subnet has the NACL and the Route Table associated w/ it. So this makes our complete path for internet accessible traffic.

### Create and configure private NACL

1. Click **Create network ACL**, and set the following values:

    * *Name tag*: **Private_NACL**

    * *VPC*: **VPC1**

2. Click **Create**.

3. W/ *Private_NACL* selected, click the **Inbound Rules** tab below.

4. Click **Edit inbound rules**.

5. Click **Add Rule**, and set the following values:

    * *Rule #*: **100**

    * *Type*: **SSH (22)**

    * *Port Range*: **22**

    * *Source*: **172.16.1.0/24**

    * *Allow / Deny*: **ALLOW**

6. Click **Save**.

7. Click the **Outbound Rules** tab.

8. Click **Edit outbound rules**.

9. Click **Add Rule**, and set the following values:

    * *Rule #*: **100**

    * *Type*: **Custom TCP Rule**

    * *Port Range*: **1024-65535**

    * *Source*: **0.0.0.0/0**

    * *Allow / Deny*: **ALLOW**

10. Click **Save**.

11. Click the **Subnet associations** tab.

12. Click **Edit subnet associations**.

13. Select the **Private1** subnet, and click **Save changes**.

## Additional resources

Log in to the live AWS environment using the credentials provided. Make sure you are using `us-east-1` (N. Virginia) as the selected region.

> [!NOTE]
> Please use the original, older user interface of Athena for this hands on lab activity. We have notified the lab creator, who will be scheduling an update to the lab.

### CloudWatch Log Metric Filter Pattern

```script
[version, account, eni, source, destination, srcport, destport="22", protocol="6", packets, bytes, windowstart, windowend, action="REJECT", flowlogstatus]
```

### Custome Log Data to Test

```script
2 086112738802 eni-0d5d75b41f9befe9e 61.177.172.128 172.31.83.158 39611 22 6 1 40 1563108188 1563108227 REJECT OK
2 086112738802 eni-0d5d75b41f9befe9e 182.68.238.8 172.31.83.158 42227 22 6 1 44 1563109030 1563109067 REJECT OK
2 086112738802 eni-0d5d75b41f9befe9e 42.171.23.181 172.31.83.158 52417 22 6 24 4065 1563191069 1563191121 ACCEPT OK
2 086112738802 eni-0d5d75b41f9befe9e 61.177.172.128 172.31.83.158 39611 80 6 1 40 1563108188 1563108227 REJECT OK
```

### Create Athena Table

```script
CREATE EXTERNAL TABLE IF NOT EXISTS default.vpc_flow_logs (
  version int,
  account string,
  interfaceid string,
  sourceaddress string,
  destinationaddress string,
  sourceport int,
  destinationport int,
  protocol int,
  numpackets int,
  numbytes bigint,
  starttime int,
  endtime int,
  action string,
  logstatus string
) PARTITIONED BY (
  dt string
) ROW FORMAT DELIMITED FIELDS TERMINATED BY ' ' LOCATION 's3://{your_log_bucket}/AWSLogs/{account_id}/vpcflowlogs/us-east-1/' TBLPROPERTIES ("skip.header.line.count"="1");
```

### Create Partitions

```script
ALTER TABLE default.vpc_flow_logs
ADD PARTITION (dt='{Year}-{Month}-{Day}') location 's3://{your_log_bucket}/AWSLogs/{account_id}/vpcflowlogs/us-east-1/{Year}/{Month}/{Day}';
```

### Analyze Data

```script
SELECT day_of_week(from_iso8601_timestamp(dt)) AS
  day,
  dt,
  interfaceid,
  sourceaddress,
  destinationport,
  action,
  protocol
FROM vpc_flow_logs
WHERE action = 'REJECT' AND protocol = 6
order by sourceaddress
LIMIT 100;
```
