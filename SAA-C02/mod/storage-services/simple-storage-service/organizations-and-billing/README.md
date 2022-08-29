# AWS Organizations and Consolidated Billing

## What is AWS organizations?

> "AWS Organizations is an account management service that enables you to consolidate multiple AWS accounts into an organization that you create and centrally manage."

Root: used for billing, and no deployment of resources is performed at this level

Organizational units (OU): finance, developers, production developers, production account, test and dev developers

The way to apply permissions is by using policies. Similar to IAM, we apply a policy document and that policy will then be inherited

AWS Organizations allows you to have multiple AWS accounts and be able to centrally manage them


## Best practices w/ AWS organizations

* Always enable multi-factor authentication on root account

* Always use a strong and complex password on root account

* Paying account should be used for billing purposes only. Do not deploy resources into the paying account

* Enable/Disable AWS services using Service Control Policies (SCP) either on Organization Units (OU), basically groups, or on individual accounts.

    * Service Control Policies (SCP) are similar to policies in Identity Access Management (IAM), enabling or disabling services for AWS accounts. So maybe you don't want your finance team/group and all the AWS accounts associated w/ it being able to provision EC2 instances for example.


## Consolidated billing

The more that you use, the less that you pay.

When you use consolidated billing w/ AWS, it takes into account the aggregate of all of your accounts. So the more that you use S3 across the entire organization, the less that you pay.

### Advantages of consolidated billing

* One bill per AWS account

* Very easy to track charges and allocate costs

* Volume pricing discount

## Learning recap

* Always enable multi-factor authentication on root account

* Always use a strong and complex password on root account

* Paying account should be used for billing purposes only. Do not deploy resources into the paying account

* Enable/Disable AWS services using Service Control Policies (SCP) either on an organizational unit basis (OU-basis) or on individual accounts

    * For example, disable EC2 instances using service control policies for the Finance Department
