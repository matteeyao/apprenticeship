# IAM

1. Your AWS account administrator left your company today. The administrator had access to the root user and a personal IAM administrator account. W/ these accounts, he generated IAM users and keys.

Which of the following should you do today to protect your AWS infrastructure? (Select three)

[x] Change the password and add MFA to the root user

[ ] Put an IP restriction on root user logins `(Impossible)`

[x] Rotate keys and change passwords for IAM users

[ ] Delete all IAM users `(Not needed)`

[x] Delete the administrator's IAM user

[ ] Relaunch all EC2 instances w/ new roles `(Roles are temporary credentials, not accounts)`

<br />

2. Which of the following actions can be controlled w/ IAM policies? Select three.

[ ] Creating tables in a MySQL RDS database `(DB Administrator task)`

[x] Configuring a VPC security group

[ ] Logging into a .NET application `(Application account)`

[x] Creating an Oracle RDS database

[x] Creating an Amazon S3 bucket

<br />

3. You have written an application that needs access to a particular bucket in S3. This application will run on an EC2 instance. What should you do to give the application access to the bucket securely?

[ ] Store your access key and secret access key on the EC2 instance in a file called 'secrets' `(Storing access and secret access key on the EC2 instance compromises their security and is anti-pattern)`

[ ] Attach an IAM role to the EC2 instance w/ a policy that grants it access to the bucket in S3

[ ] Store your access key and secret key on the EC2 instance in '$HOME/.aws/credentials' `(Storing access key and secret access key on the EC2 instance compromises their security and is anti-pattern)`

[ ] Use S3 bucket policies to make the bucket public `(Making the bucket public will give it access that is too broad)`

<br />

4. A Solutions Architect is building a cloud infrastructure where EC2 instances require access to various AWS services such as S3 and Redshift. The Architect will also need to provide access to system administrators so they can deploy and test their changes.

Which configuration should be used to ensure that the access to the resources is secured and not compromised? (Select TWO.)

[ ] Assign an IAM user for each Amazon EC2 instance.

[ ] Store the AWS Access Keys in the EC2 instance.

[ ] Store the AWS Access Keys in ACM.

[x] Enable Multi-Factor Authentication.

[x] Assign an IAM role to the Amazon EC2 instance.

**Explanation**: Always remember that you should associate IAM roles to EC2 instances and not an IAM user, for the purpose of accessing other AWS services. IAM roles are designed so that your applications can securely make API requests from your instances, without requiring you to manage the security credentials that the applications use. Instead of creating and distributing your AWS credentials, you can delegate permission to make API requests using IAM roles.

**AWS Multi-Factor Authentication (MFA)** is a simple best practice that adds an extra layer of protection on top of your user name and password. With MFA enabled, when a user signs in to an AWS website, they will be prompted for their user name and password (the first factor—what they know), as well as for an authentication code from their AWS MFA device (the second factor—what they have). Taken together, these multiple factors provide increased security for your AWS account settings and resources. You can enable MFA for your AWS account and for individual IAM users you have created under your account. MFA can also be used to control access to AWS service APIs.

> **Storing the AWS Access Keys in the EC2 instance** is incorrect. This is not recommended by AWS as it can be compromised. Instead of storing access keys on an EC2 instance for use by applications that run on the instance and make AWS API requests, you can use an IAM role to provide temporary access keys for these applications.

> **Assigning an IAM user for each Amazon EC2 Instance** is incorrect because there is no need to create an IAM user for this scenario since IAM roles already provide greater flexibility and easier management.

> **Storing the AWS Access Keys in ACM** (AWS Certificate Manager) is incorrect because ACM is just a service that lets you easily provision, manage, and deploy public and private SSL/TLS certificates for use with AWS services and your internal connected resources. It is not used as a secure storage for your access keys.

<br />

5. A company uses one AWS account to run production workloads. The company has a separate AWS account for its security team. During periodic audits, the security team needs to view specific account settings and resource configurations in the AWS account that runs production workloads. A solutions architect must provide the required access to the security team by designing a solution that follows AWS security best practices.

Which solution will meet these requirements?

[ ] Create an IAM user for each security team member in the production account. Attach a permissions policy that provides the permissions required by the security team to each user. → `Incorrect. This solution does not follow the security best practice of using roles to delegate permissions.`

[ ] Create an IAM role in the production account. Attach a permissions policy that provides the permissions required by the security team. Add the security team account to the trust policy. → `Correct. This solution follows security best practices by using a role to delegate permissions that consist of least privilege access.`

[ ] Create a new IAM user in the production account. Assign administrative privileges to the user. Allow the security team to use this account to log in to the systems that need to be accessed. → `Incorrect. The assignment of administrative privileges to a user violates security best practices and the principle of least privilege.`

[ ] Create an IAM user for each security team member in the production account. Attach a permissions policy that provides the permissions required by the security team to a new IAM group. Assign the security team members to the group. → `Incorrect. This solution does not follow the security best practice of using roles to delegate permissions.`

<br />
