# Identity Access Management

## Part I

1. You have created a new AWS account for your company, and you have also configured multi-factor authentication on the root account. You are about to create your new users. What strategy should you consider in order to ensure that there is good security on this account?

Enact a strong password policy: user passwords must be changed every 45 days, w/ each password containing a combination of capital letters, lower case letters, numbers, and special symbols.

A password policy to set a minimum standard is good practice and is generally a top requirement for any industry compliance endorsement.

2. What is an additional way to secure the AWS accounts of both the root account and new users alike?

Implement Multi-Factor Authentication for all accounts.

MFA provides an additional requirement for the person signing on to prove that they are who they claim to be. Username and password are things you 'know' the MFA is something that you 'have'. e.g. you have the only device that can generate the token.

3. When you create a new user, that user _.

Will be able to interact w/ AWS using their access key ID and secret access key using the API, CLI, or the AWS SDKs assuming programmatic access was enabled.

To access the console you use an account and password combination. To access AWS programmatically you use a Key and Secret Key combination.

4. You are a solutions architect working for a large engineering company that are moving from a legacy infrastructure to AWS. You have configured the company's first AWS account and you have set up IAM. Your company is based in Andorra, but there will be a small subsidiary operating out of South Korea, so that office will need its own AWS environment. Which of the following statements are true?

You will need to configure Users and Policy Documents only once, as these are applied globally.

Users are not Regional in IAM. You can have regional conditions in policies, however by default policies are Global.

IAM is a Global service.

You can have regional conditions in policies, however by default users and policies are Global.

5. You are a security administrator working for a hotel chain. You have a new member of staff who has started as a systems administrator, and she will need full access to the AWS console. You have created the user account and generated the access key id and the secret access key. You have moved this user into the group where the other administrators are, and you have provided the new user w/ their secret access key and their access key it. However, when she tries to log in to the AWS console, she cannot. Why might that be?

You cannot log in to the AWS console using the Access Key ID / Secret Access Key pair. Instead, you must generate a password for the user, and supply the user w/ this password and your organization's unique AWS console login URL.

6. You have a client who is considering a move to AWS. In establishing a new account, what is the first thing the company should do?

Set up an account using their company email address.

This email address is a key part of linking the AWS account to your company. Using a private email address may make it harder to establish ownership if you need help from AWS.

7. Every user you create in the IAM systems starts w/ _.

No Permissions.

AWS systems are designed to be secure first. The system administrator needs to add permissions to allow accounts to take actions.

8. Which statement best describes IAM?

IAM allows you to manage users, groups, roles, and their corresponding level of access to the AWS Platform.

Key concepts: Users, Groups, Roles, and the level of access. And "...to the AWS Platform."

9. Which of the following is not a component of IAM?

Organizational units. 'OU's are a feature of [AWS Organizations](https://aws.amazon.com/organizations/features/)

10. Power User Access allows _.

Access to all AWS services except the management of groups and users within IAM.

11. What level of access does the "root" account have?

Administrator Access

The *root* account in an AWS account represents the Owner of the account and can do anything including changing billing details and even close the account. The details for this account should be locked away and only used when absolutely necessary.

[Link](https://docs.aws.amazon.com/IAM/latest/UserGuide/id_root-user.html)

12. You are a developer at a fast-growing startup. Until now, you have used the root account to log in to the AWS console. However, as you have taken on more staff, you will need to stop sharing the root account to prevent accidental damage to your AWS infrastructure. What should you do so that everyone can access the AWS resources they need to do their jobs?

Create a customized sign-in link such as "yourcompany.signin.aws.amazon.com/console" for your new users to use to sign in with

and

Create individual user accounts w/ minimum necessary rights and tell the staff to log in to the console using the credentials provided.

Read the AWS Security Best Practice white paper. Also note that the IAM account sign-in URL is different from the Root account sign-in URL.

13. A _ is an object in AWS stored as JSON document that provides a form statement of one or more permissions.

Policy

A policy is an object in AWS that, when associated w/ an identity or resource, defines their permissions. Most policies are stored in AWS as JSON documents.

14. Which of the following are not a feature of IAM?

✓ IAM offers centralized control of your AWS account.

✓ IAM integrates w/ existing active directory account allowing single sign-on.

✓ IAM offers fine-grained access control to AWS resources.

✕ IAM allows you to set up biometric authentication, so that no passwords are required.

AWS makes use of Accounts and Passwords, or Keys and Secret keys, and MFA, to prove identity. You may have a 3rd party device that uses BioMetric to initiate and exchange the password or secret key w/ AWS, but that is not an AWS / IAM service.

[Link](https://aws.amazon.com/iam/features/)

15. A new employee has just started work, and it is your job to give her administrator access to the AWS console. You have given her a user name, an access key ID, a secret access key, and you have generated a password for her. She is now able to log in to the AWS console, but she is unable to interact w/ any AWS services. What should you do next?

**Grant her Administrator access by adding her to an Administrators group.**

By default new user accounts come w/ no permission to interact w/ services. These must be explicitly assigned by adding a Policy or adding them to a Group. The admin user should have also been configured w/ MFA as best practice, but MFA would not be related to the permission issue itself.

16. In what language/format are policy documents written?

JSON

JavaScript Object Notation is a human-readable and easily parsed structured data format used to pass blocks of data into and between systems.

18. What is the default level of access a newly created IAM User is granted?

**No access to any AWS services.**

By default new IAM Users have no permissions to AWS services. They must be explicitly granted.

## Part II

1. IAM roles permanent, long-term credentials.

**False**

IAM roles are usually temporary, and roles are assumed, so you assume the permissions for that level of access inside the AWS account and then become that role. And then you stop using those permissions you no longer assume that role but **IAM users** have long-term credentials. A role does not represent you like an **IAM user** does, a role is a level of access or a thing you can use to get the needed permissions. So w/ a role, you are using permissions for a short period of time; you become that identity for a short period of time.

2. How can you secure your AWS account?

**Add multi-factor authentication**

We can secure our AWS account by adding multi-factor authentication

3. What is a Service Control Policy?

**SCPs are permission boundaries and limit what the accounts can do, including the account root user of that account.**

A SCP policy is a json policy doc and an SCP can be attached to an AWS Org as a whole by attaching the SCP to the root container, or attached to one or more OUs, or they can be attached to one or more AWS accounts directly. So an SCP inherits down the org structure, so if an SCP is attached to the org they affect all accounts in that org, if they are attached to an OU then they affect all accounts in that OU and the accounts below that OU, and if attached to one or more accounts then they just affect those accounts.

4. What permissions does a new user start w/ in an AWS account?

**No permissions**

IAM identities start w/ no permissions on an AWS Account, but can be granted permissions needed.

5. What is an IAM role?

**An IAM role is an identity in AWS, but an IAM role is used by multiple principals**

An IAM role is an identity in AWS, but an IAM role is used by multiple principals, so this could be multiple AWS users, applications, services, inside your AWS account but these could also be users, applications, etc outside of your AWS account too.

6. Which of the following are considered identities in AWS?

[x] IAM Roles

[x] IAM Groups (in a limited way)

[x] IAM Users

[ ] IAM Policies

**IAM users**, **IAM roles**, and **IAM Groups** are considered identities. Groups are limited in that they cannot be identified as a Principal in a resource-based policy. **IAM Policies** are not identities.

**IAM Policies** are not identities. IAM users, IAM roles, and IAM Groups are considered identities. Groups are limited in that they cannot be identified as a Principal in a resource-based policy.

7. What is a benefit of an AWS Organization?

**Consolidated account billing**

So AWS Org adds simplicity to managing all of your AWS accounts by basically grouping up different accounts to match your business organization AND it also gives you the ability to consolidate all of your AWS accounts into one bill called consolidated billing, so instead of receiving 5 to 10 individual AWS account bills, AWS org allows you to consolidate all of your AWS accounts into a single bill, so the AWS member accounts pass their billing info to the master account which is the payer account. And this single consolidated bill covers the usage for the master account along w/ the usage for the AWS member accounts.

Another awesome benefit of using AWS Organization is that some AWS services get cheaper the more users that you have, and other AWS services are cheaper if you reserve that service and pay in advance for your usage, and w/ AWS Org all of these services and usage is pooled for all AWS accounts in the AWS Org, so that all accounts can benefit from the combined cost savings.

8. What BEST describes an AWS account?

**An AWS account is a container for your AWS resources.**

An AWS account is a container for your AWS resources. You create and manage your AWS resources in an AWS account, and the AWS account provides administrative capabilities for access and billing. [AWS Organizations FAQs ▶︎ Core concepts](https://aws.amazon.com/organizations/faqs/#Core_concepts).

9. What is the maximum number of users allowed to be created in an AWS account?

You can only have **5000** users per AWS account

10. What is an IAM policy?

**IAM policies are policies that get attached to identities inside of AWS**

IAM policies are a set of security statements that grant or deny access to AWS resources, products, features, etc.
