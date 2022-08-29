# Create a VPC Endpoint and S3 Bucket in AWS

## Learning Objectives

[ ] Create an S3 Bucket

[ ] Create a VPC Endpoint

[ ] Verify VPC Endpoint access to S3

> **About this lab**
>
> In this hands-on lab, we will create a VPC endpoint and an S3 bucket to illustrate the benefits available for our cloud implementations. VPC endpoints can be used instead of NAT gateways to provide access to AWS resources. Many customers have legitimate privacy and security concerns about sending and receiving data across the public internet. VPC endpoints for S3 can alleviate these challenges by using the private IP address of an instance to access S3 w/ no exposure to the public internet.

![Fig. 1 Create an S3 Bucket and VPC Endpoint](../../../../../img/SAA-CO2/virtual-private-cloud/virtual-private-cloud.demo/module03/diagram.png)

* What is the advantage of using a VPC endpoint over a NAT Gateway to connect to an S3 bucket?

* In our lab, at some point both our private and public ec2 instances will need to get out to S3 to get updates or packages. The options that we have for our 2 EC2 instances to get out and receive those updates are first in our public subnet-our public EC2 instance can use the Internet Gateway to reach the internet or S3 for updates, but what about our private EC2 instance in our private subnet?

* We could use a NAT Gateway, or we could use a VPC endpoint. So why would we choose to use a VPC endpoint over a NAT Gateway? The VPC endpoint connects the private EC2 instance to our S3 bucket w/o using the public internet, the way a NAT Gateway would. W/ a VPC endpoint, we are going to get better performance and a more secure connection b/c we're not using the public internet like a NAT Gateway does, but a NAT Gateway is a great option b/c it will scale it is highly available, but it does use the public internet and it's not going to have the same performance as a VPC endpoint.

## Guide

### Create an S3 Bucket

> 1. Begin by navigating to EC2.
>
> 2. In *Resources*, click **Instances (running)**.
>
> 3. Select the checkbox next to one of the instances.
>
> 4. In *Details* below, notice that the *Public IPv4 address* field is either blank or has an address value. The instance that has the IPv4 address is the **public** instance, and the instance where this field is blank is the **private** instance 
>
> 5. In the *Name* column at the top, click the blank field, and rename the instances to *public* and *public* accordingly.
>
> 6. Navigate to S3.
>
> 7. Click **Create bucket**.
>
> 8. In *Bucket name*, create an S3 bucket beginning w/ the name `vpcendpointbucket` followed by random characters to ensure the bucket is globally unique.
>
> 9. At the bottom, click **Create bucket**.

### Create a VPC Endpoint

> 1. Navigate to VPC.
>
> 2. From the left navigation, select **Endpoints**.
>
> 3. Click **Create Endpoint**.
>
> 4. Leave *Service category* as **AWS services**.
>
> 5. For *Service Name*, search for and select **com.amazonaws.us-east-1.s3** (w/ a *Type* of **Gateway**).
>
> 6. For *VPC*, select the provided VPC from the dropdown.
>
> 7. You have been provided w/ both a public and private route table for this lab. Before proceeding, you'll need to identify the private route table in VPC. Keep this tab open, but open another instance of VPC in a new tab.
>
> 8. From the left navigation, select **Route Tables**.
>
> 9. Select the route w/ no name (the other will be called **Endpointrb**).
>
> 10. Select the *Routes* tab below. Note the *Target* shows only a local IP. This is the private table.
>
> 11. Go back up to *Name*, click the blank field, and enter *private*.
>
> 12. W/ the private route still selected, select the *Subnet Associations* tab below.
>
> [!NOTE]
> If you do not see an associated subnet, click **Edit subnet associations**. Select the one w/ **private** in the *Route table ID*, and click **Save associations**.
>
> 13. Keep note of the subnet name for the following steps.
>
> 14. Go back to your first browser tab w/ the VPC endpoint console.
>
> 15. For *Configure route tables*, select the private subnet you just copied (the name you just noted will match the name in the **Associated with** column).
>
> 16. Leave the rest of the fields as their defaults, and click **Create endpoint**.
>
> 17. Click **Close**.

### Verify VPC Endpoint Access to S3

> 1. From the VPC navigation menu, select **Route Tables**.
>
> 2. Select the **private** route table.
>
> 3. Select the **Routes** tab. Note that AWS has automatically updated the private route table w/ a route to the VPC endpoint.
>
> [!NOTE]
> If you do not see this right away, you may have to wait a minute and refresh to see the update.
>
> 4. Open a terminal, and SSH into the public instance, replacing `PUBLIC_IP_ADDRESS` w/ the public IP address found in the **Cloud Server of Public Instance** section from your lab credentials (use the password from here as well):

```zsh
ssh cloud_user@<PUBLIC_IP_ADDRESS>
```

> 5. From the public instance, SSH into the private instance using the private IP address and password found in the **Cloud Server of Private Instance** section from your lab credentials:

```zsh
ssh cloud_user@<PRIVATE_IP_ADDRESS>
```

> 6. View the S3 bucket:

```zsh
aws s3 ls
```

>   * You should see the 2 S3 buckets — the one provided for the lab and the one you created.
