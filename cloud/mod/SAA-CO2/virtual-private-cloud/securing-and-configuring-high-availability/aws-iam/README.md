# AWS Identity and Access Management (IAM)

When building Amazon VPCs, it's critical to build security into every layer. In this section and the next, you review three access controls: AWS Identity and Access Management (IAM), network access control lists (network ACLs), and security groups.

## Configure and manage your Amazon VPC

IAM lets you control who can configure and manage your Amazon VPCs. It lets you create and assign permission policies based on group, user, and role. These permissions then specify what each entity can and cannot do to resources you specify. 

## IAM overview

When an AWS account is created, AWS creates an account root user that has full permissions to the AWS account, and the permissions for the account root user cannot be adjusted. However, in most cases other people in your organization need to be granted permissions inside of the AWS account. 

Organizations have different users, perhaps a storage team, engineers, sysadmins, and those people need access to the AWS account, and also need access to the services in the AWS account. It is AWS best practice to follow the principle of least privilege, which means only giving access to users in the AWS account to the specific services that they need access to. Users in an AWS account only need permissions to do their job; extraneous permissions increase the risk of actions that are out of scope of their individual role and can have unforeseen impacts on your AWS environment.  

By using the principle of least privilege, you limit potential risk and ensure that users only have the permissions needed, nothing more. Each AWS account has a root user account. The account root user always has full access to the account and the AWS services allowing it to override any overly restrictive security controls enacted by non-root IAM accounts.

* **IAM users** represent people and also applications that need access to an AWS account. 

* **IAM groups** are groups of related users, for example, your development team, sysadmins, storage engineers, and the finance team.

* **IAM roles** are used by AWS services. IAM roles can also be used to grant external access to your AWS account along with access to resources and services in the AWS account. For example, an Amazon Elastic Compute Cloud (Amazon EC2) instance inside your AWS account requires programmable access to Amazon CloudWatch, Amazon Simple Storage Service (Amazon S3), and the like.

* **IAM policies** are used to allow or deny access to AWS services. IAM policies must be attached to an IAM user, an IAM group, or an IAM role. AWS provides preconfigured policies, AWS Managed Policies, that can be assigned as necessary. Customers can also create custom inline policies that allow or deny unique combinations of permissions that best suit the customer's AWS environment. On their own, policies just sit there, to take action they must be attached to a user, a group, or a role.

* On a high-level overview, **IAM acts as an identity provider (IdP)**, and manages identities inside an AWS account. IAM authenticates these identities facilitating AWS account login activities to be allowed to log into the AWS account, and then authorizes those identities to access resources or deny access to resources based on the policies attached.
