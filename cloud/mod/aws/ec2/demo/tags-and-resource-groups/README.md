# Using AWS Tags and Resource Groups

## Introduction

> To simplify the management of AWS resources such as EC2 instances, you can assign metadata using tags. Resource groups can then use these tags to automate tasks on large numbers of resources at one time. They serve as a unique identifier for custom annotation, to break out cost reporting by department and much more. In this hands-on lab, we will explore tag restrictions and best practices for tagging strategies. We will also get experience w/ Tag Editor, AWS resource group basics, and leveraging automation through the use of tags.

AWS allows customers to assign metadata to their AWS resources in the form of tags. Each tag is a simple label consisting of a customer-defined key and an optional value that can make it easier to manage, search for, and filter resources by purpose, owner, environment, or other criteria. AWS tags can be used for many purposes, but w/o the use of tags, it can become difficult to manage your resources effectively.

As your utilization of AWS services grows, however, it's not always evident how to determine which tags to use and for which types of resources. The goal of this hands-on lab is to equip you w/ the basics so we can start to manage our AWS resources more efficiently.

![Architecture](https://s3.amazonaws.com/assessment_engine/production/labs/df7ec6e9-8407-4961-b4bd-2c85d30fd3b4/lab_diagram_Lab_-_Using_AWS_Tags_and_Resource_Groups.001.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=ASIAVKPCGNLNUF5JUB7X%2F20220315%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20220315T023324Z&X-Amz-Expires=3600&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEPH%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FwEaCXVzLWVhc3QtMSJHMEUCIE%2FrI878qi43uRIz1KYy1SgJtF%2Bm4WbK36Y8ux%2FCjYX6AiEAj0L6BFhqrDaqs2eqNmVKmra3a%2FoleXuX1MI6MmcL6CcqgwQIahACGgwzNjYwODM0Njc5OTUiDG5YcZzzjlX34p%2B6WCrgA%2BjRNXYo1csNLphsxMshgDWAIcKC5yyJwMh%2BpAkpMpIDtFXpIxCsp1tEVQGKOe3%2B2XPkWXFJ%2B0c9wEbzFxDj8CJrZ4Ox4u0FiUH5JoRDuFtD04XFTNBPBTZKT43uXuRrw8xvzkl5TWqsy6orLZ42k6CevYfmUNopKnDNHehcSQ3DFewUs9cesPoNSfy%2BkeG5ksNyJy7mgwTLyMkxZDAz59Q4p1K0ncWXs9G9aYxec%2FvsF7yRJtFQmSTqz6Hxo0zWh%2FDbUT86Sl21%2FOkOMoD3IjhPjVUI7o7D2BNVDLo24Qu4Jq4eT8mjtCFEnODYlaYiqL3xw3V1%2FiGDoL6L2X69%2Fs3Y1KZS8vSzu%2BSSM0WwqhkyZUXcjyyD5YCV9eSV3IsfwHsheUZNeOZ80UyyN95BV4XF8F2OF4EJGpeCY41mOQwtBDbIgSGtuYL7ZhdKgPjUaiQJpO%2FdOoSNLH87M7sm5DJntnbx951sgn2tQp5P%2F2DCfqGFsBajZ5ROaxmkErmmnxPgnlEDm%2FS5IeE2z61btM2NkB3Mbv2IcPE2a1Vxs0f8pSWiSzZe2U%2F9ZDJfIxtBAlBVNusxwmQuK1XQaJO5YcQjeEM8rKToa7HacRC0HpKlT%2F2ACT%2FDgzr3ztziiBjEDTDOv7%2BRBjqlAWKufF8MQ2Tbjn5UDzZQ7kMvr1itvW585AKZm3CezmvpC8cet%2BOfsBYYGCk6axB643Id8GrjJgPTWrLBXw5MVzY3jxgFpBEzY%2F%2FM6qad2WWWL%2B3SSg%2BZyqWCpjrxcpNH68LqyI09j4wp8LtEsvTpnHvH7ELavGtCDC9i%2BiSEziDy0m3fl%2BNuavHblpBZfbmDUdVK6hK%2BWM7K%2FdwdPL5mJp%2Bxd0AIqQ%3D%3D&X-Amz-SignedHeaders=host&X-Amz-Signature=99469a0c28c8f812b721fc67e212d0b05859cb680515241470a45cb431e041a0)

We start w/ the virtual private cloud, and have a couple of subnets. We have some EC2 instances: `Mod. 1`, which represents a component of an application running on `Server A` in `Subnet A`. We also have `Server B` in `Subnet B` and we have `Mod. 2` in `Subnet A`.

We'll be creating an Amazon Machine Image (AMI) and launching a test web server. We'll be using this server w/ AWS Config later in this lab. AWS Config is a service that enables you to view the configurations of your resources overtime.

Additionally, we have two S3 buckets: `moduleone S3 Bucket` and `moduletwo S3 Bucket`. Now these are deliberately labeled differently than the applications, which can simulate the growth of an organization and different teams working on resources following different naming conventions. We're going to use tags to organize all of these resources and look at them together. So, we'll be able to look at `moduleone` resources, which include the two module EC2 instances and the S3 bucket, and then we'll also look at the `moduletwo` resources by the tags.

## Solution

Log in to the live AWS environment using the credentials provided. Make sure you're in the N. Virginia (`us-east-1`) Region throughout the lab.

## Setup AWS Config

1. Navigate to *Config* using the *Services* menu or the unified search bar.

2. In the *Set up AWS Config* window, click **1-click setup**.

3. Leave the settings as their defaults.

4. Click **Confirm**.

## Tag an EMI and EC2 Instance

1. In a new browser tab, navigate to **EC2** ▶︎ **Instances(running)**.

2. Select any one of the instances listed.

3. Right-click on the selected instance and select **Image and templates** ▶︎ **Create image**.

4. For the *Image name*, enter "Base".

5. Click **Create image**

6. Click **AMIs** in the left-hand menu.

7. Once the AMI you just created has a status of *available*, select it. (It could take up to 5 minutes.)

8. Click **Launch**.

9. Select *t3.micro*, and click **Next: Configure Instance Details**.

10. Leave the defaults on the *Configure Instance Details* page.

11. Click **Next: Add Storage**, and then click **Next: Add Tags**.

12. On the *Add Tags* page, click **Add tag** and set the following values:

    * *Key*: **Name**

    * *Value*: **Test Web Server**

13. Click **Next: Configure Security Group**.

14. Click **Select an existing security group**.

15. Select the one w/ **SecurityGroupWeb** in the name (*not* the default security group).

16. Click **Review and Launch**.

17. Click **Continue** in the warning dialog.

18. If the *Boot from General Purpose (SSD)* dialog pops up, click **Next**.

19. Click **Launch**.

20. In the key pair dialog, select **Proceed w/o a key pair** and check the acknowledgement box.

21. Click **Launch Instances**

22. Click **View Instances**, and give it a few minutes to enter the *running* state.

## Create Resource Groups and Use AWS Config Rules for Compliance

### Create the `Starship-Monitor` Resource Group

1. In the left-hand menu, select **Create Resource Group**.

2. Select **Tag based**.

3. In the *Grouping criteria* section, click **All supported resource types**.

4. In the *Tags* field, add the following:

    * *Tag key*: **Module**

    * *Optional tag value*: **Starship Monitor**

5. Click **Preview group resources**.

6. In the *Group Details* section, enter a *Group name* of "Starship-Monitor".

7. Click **Create group**.

### Create the `Warp-Drive` Resource Group

1. In the left-hand menu, click **Create Resource Group**.

2. Select **Tag based**.

3. In the *Grouping criteria* section, click **All supported resource types*.

4. In the *Tags* field, add the following:

    * *Tag key*: **Module**

    * *Optional tag value*: **Warp Drive**

5. Click **Preview group resources**.

6. In the *Group Details* section, enter a *Group name* of "Warp-Drive".

7. Click **Create group**.

### View the Saved Resource Groups

1. Click **Saved Resource Groups** in the left-hand menu.

2. Click **Starship-Monitor**. Here, we should see all the resources in our Starship-Monitor group.

### Use AWS Config Rules for Compliance

1. Navigate to **EC2** ▶︎ **Instances**.

2. Select the **Test Web Server** instance.

3. In the *Details* section, copy its AMI ID.

4. Navigate to your AWS Config Console tab.

5. In the left-hand menu, click **Rules**.

6. Click **Add rule**.

7. Select **Add AWS managed rule** for the rule type.

8. Search for "approved-amis-by-id" in the search box, and select that rule.

9. Click **Next**.

10. In the *Trigger* section, set the following values:

    * *Scope of changes*: **Tags**

    * *Resources by tag*:

        * *Tag key*: **Module**

        * *Tag value*: **Starship Monitor**

11. In the *Parameters* section, paste the AMI ID you copied earlier into the *Value* field.

12. Click **Next** ▶︎ **Add rule**.

13. Back in the EC2 instances console, select all the instances.

14. Click **Instance state** ▶︎ **Reboot instance**.

15. In the *Reboot instances* dialog, click **Reboot**.

16. Back in the AWS Config Console, after a few minutes, we should see there are now noncompliant resources.

17. Click the **approved-amis-by-id** link.

18. Click the link for one of the noncompliant resources to see more information.

## Additional resources

Your company runs many applications in a shared AWS account w/ hundreds of instances. The application and security teams want an easy way to find resources associated w/ a particular application. AWS tags and resource groups demonstrated in this lab make it easy to identify application components.

Log in to the live AWS environment using the credentials provided.  Make sure you're in the N. Virgina (`us-east-1`) Region throughout the lab.

## Lab prerequisites

* Understand how to log in to and use the AWS Management Console.

* Understand EC2 basics, including how to launch an instance.

* Understand AWS Identity & Access Management (IAM) basics, including users, policies, and roles.

* Understand how to use the AWS Command Line Interface (CLI)

## Helpful documentation

* Tag Editor

* Tagging Strategies

* Tagging and Cost Allocation

* AWS Resource Groups

* AWS Systems Manager

* AWS Config
