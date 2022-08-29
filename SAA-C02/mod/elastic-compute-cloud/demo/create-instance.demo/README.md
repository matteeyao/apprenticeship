# Create and working w/ an EC2 instance

## Introduction

> Elastic Compute Cloud (EC2) is AWS's Infrastructure as a Service product. It provides a huge range of virtual machines suitable for general purpose and specialized on-demand compute tasks. In this hands-on lab, you will gain experience creating and interacting w/ an EC2 instance. The lab covers EC2 requirements, the choices available w/ creating EC2 instances, andtthe provisioning process itself. By the end of the lab, you will have gained the experience needed to be confident using EC2 in smaller deploymenets, such as blogs or lower-volume websites.

![Architecture (Simple, single EC2 instance running inside a Virtual Private Vloud (VPC) that has internet access)](https://s3.amazonaws.com/assessment_engine/production/labs/e43cb83b-29db-4387-ae52-16d148d8445b/lab_diagram_Lab_-_Creating_and_Working_with_an_EC2_Instance.001.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=ASIAVKPCGNLNQMNWVJM7%2F20220315%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20220315T035926Z&X-Amz-Expires=3600&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEPP%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FwEaCXVzLWVhc3QtMSJGMEQCICig%2FwfYMx9ICuHWWdGJaXs8NNw%2Bfk2ya9WlVhqXF2HcAiA0O9HR3dqls3xj76Gd0VkokG%2FSDzi7Kg%2FzKUiqCsjydCqDBAhsEAIaDDM2NjA4MzQ2Nzk5NSIM19WAKm%2BJjxDk2AXLKuAD8iE%2B0iJKhBmenSeRprV8IxnKkmKCtlZ58r%2FASVq%2BJDA5Pm7eXW6UhiS3juR%2B9cfv2R10TMZm7DPdaJ3lVtLDr%2FtsQjLj6Is%2FNjjGv3Y9dJTF1V3yGzBT7adtfAAS7KSVmC%2BD4t5%2BCCXkvRIkTUiMP6BJaoPIbg7cYLSCQgNeJIT9g8MIxtiaReRkWPyBzqJwb2XbOMhMTUCuXdasSMRjZPqRAMCA1yEOmFjK7%2BN5zv%2BFdFrWrpqwgmwTJ9abGpSmgo2eF2GL27myeVFea5XMxU5%2BQfeIv85YkGVuZ6p6%2BgSLF3DODg%2BUdIwc4tx8K%2F5N67RJ7at43VMXTGwvRFrs53xab4Ud3bUQPceuVtmwcFQm%2BGnoHAg4od5HXZ9IJdvzVXLlgNxb8kG0DD0vBprgxA7pF3R5RNTD6RxPkeATRlkI%2F26pDzZfAo0maA47cW9RMPwkyQjzVD0HkGDVUXY%2BjUF%2FPtVvlnzChevDIFSKuZiTiucwfM08VFXAFlo6iA4jX6kws%2BeFzAvnhBeislCPB1%2Fg5HFCl%2B1KU7zp%2F00ejvgR0NxFDGsWfteY8umZeIO0dQMt6UNlSE3w92wajc3yoFWXnvz6T3n%2FL8%2FKg%2Fr7Zxb9GlVeSYhMxFr8DHKwORQuMOT1v5EGOqYBRQcFJYYp4RM1EcqQJ9ZmZo6l7CguJSaT8GRzcpYlPigChtgAY0hd4uOsWtnZdxY8tcyzS4AAw2FhDjXJDTKK5xiq9f6esFmkuJhihivdEK7IP8osHrAS1QTlRWqXrxW5fgNH8t36wGhvmfr3VfTZBGmEJUmL6cszd1J1notUFMOvrUUHsRjCCVb7D3ELuu%2FWcZ4%2BE%2BozousQcBMRv%2FCsxsWEVwJeMQ%3D%3D&X-Amz-SignedHeaders=host&X-Amz-Signature=da6bf687e95c23c875ce199cb06daa42ce2c126596cc4eb551fed0cb68a3c966)

EC2 benefits from a super powerful set of supporting services, including Elastic Block Store (EBS), which provides high-performance storage, and Virtual Private Cloud (VPC), which provides software-defined networking.

EC2 instances operate within a networking product called Virtual Private Cloud (VPC). This is essentially the private, public, and hybrid networking product by AWS, where you can provision and configure networks and software. It's essentailly software-defined networking, where instead of needing to take physical actions, such as installing a network switch or firewall, you can create virtual versions of these things inside AWS.

Now, if you get more involved w/ AWS, you'll be using VPC extensively, especially in larger projects.

When you initially create an AWS account, each region comes w/ what's known as a default VPC. This default VPC allows you to provision services into it w/o worrying about the networking.

## Solution

Log in to the live AWS environment using the credentials provided. Make sure you're in the N. Virgina (`us-east-1`) region throughout the lab.

Here are the instructions on how to connect to EC2 using PuTTY on Windows.

Windows users can also use WSL2 on how to connect to EC2 using WSL2 on Windows (Recommended)

## Create a Default VPC

1. Naviage to **VPC** ▶︎ **Your VPCs**.

2. Select **Actions** ▶︎ **Create default VPC**.

3. Click **Create default VPC**.


## Create an EC2 Instance

1. Naviage to **EC2** ▶︎ **Instances**.

2. Click **Launch instance**.

3. On the AMI page, select the **Amazon Linux 2 AMI**.

4. Leave *t2.micro* selected, and click **Next: Configure Instance Details**.

5. On the *Configure Instance Details* page:

    * *Network*: **default**

    * *Subnet*: **No preference**

    * *Auto-assign Public IP*: **Enable**

6. Expand *Advanced details*, and paste the following into the user data box:

```script
#!/bin/bash
yum update -y
yum install -y httpd
yum install -y curl
chkconfig httpd on
service httpd start
```

7. Click **Next: Add Storage**, and then click **Next: Add Tags**.

8. On the *Add Tags* page, select **Add Tag** then add the following:

    * *Key*: **Name**

    * *Value*: **Webserver**

9. Click **Next: Configure Security Group**.

10. On the *Configure Security Group* page, click **Create a new security group**, and set the following values:

    * *Security group name*: **LabSG**

    * *Description*: **LabSG**

11. Click **Add Rule**, and set the following values (leave the default SSH rule):

    * *Type*: **HTTP**

    * *Source*: **My IP**

12. Click **Review and Launch**, and then **Launch**.

13. In the key pair dialog, select **Create a new key pair**.

14. Give it a *Key pair name* of "Lab".

15. Click **Download Key Pair**, and then **Launch Instances**.

16. Click **View Instances**.

## Manage the EC2 instance

1. Wait for the instance to enter a *running* state and show 2/2 status checks complete.

2. Ensure the instance is selected.

3. In the *Description* section, copy its public DNS (IPv4).

4. Paste it into a new browser tab to preview it, which should result in the Apache test page.

### Connect to the EC2 instance

1. Open a terminal session and change to your downloads directory where the key pair file should be saved (e.g., `cd Downloads`).

2. In the terminal, change the permissions on the key pair:

```scipt
chmod 400 Lab.pem
```

3. On the EC2 instances page, w/ the EC2 instance still selected, click **Connect**.

4. Copy the `ssh` connection string, listed under *Example*, and paste it into the terminal window to connect to the instance.

5. Enter `yes` at the prompt.

6. In the AWS console, note the IPv4 public IP of the instance.

7. Click **Actions** ▶︎ **Instance State** ▶︎ **Reboot**.

8. In the dialog, click **Yes, Reboot**.

9. Note if the IPv4 public IP changes.

    * (It should stay the same.)

10. Click **Actions** ▶︎ **Instance State** ▶︎ **Stop**.

11. In the dialog, click **Yes, Stop**. Give it a minute to fully stop.

12. Note the IP.

    * (Once it's fully stopped, the IP will dissapear.)

13. Click **Actions** ▶︎ **Instance State** ▶︎ **Start**.

14. In the dialog, click **Yes, Start**. Give it a minute to enter a *running* status.

15. Note the IP.

    * (A new IP should appear.)

16. Click **Actions** ▶︎ **Instance State** ▶︎ **Stop**.

17. In the dialog, click **Yes, Stop**. Give it a minute to fully stop.

18. Ckick **Actions** ▶︎ **Instance Settings ▶︎ **Change Instance Type**.

19. Change the instance type to **t3.small**, and click **Apply**.

## Additional resources

You have been asked to create a basic web server for your group on a default VPC. To do this, you will need to create an EC2 instance.

Once you have created this instance, you will need to log in to it to make sure it can be accesseed.

Later, you may be asked to change the machine type of the EC2 instance b/c it needs more resources. You need to do this w/o losing the EC2 instance (but you may stop it).

Log in to the live AWS environment using the credentials provided, and make sure you are in `us-east-1` throughout the lab.

When entering the instance user data, use the following:

```script
#!/bin/bash
yum update -y
yum install -y httpd
yum install -y curl
chkconfig httpd on
service httpd start
```

Here are the instructions on how to connect to EC2 using PuTTY on Windows.

Windows users can also use WSL2 on how to connect to EC2 using WSL2 on Windows (Recommended)
