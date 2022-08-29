# AWS Security Token Service (AWS STS)

1. An Intelligence Agency developed a missile tracking application that is hosted on both development and production AWS accounts. The Intelligence agency’s junior developer only has access to the development account. She has received security clearance to access the agency’s production account but the access is only temporary and only write access to EC2 and S3 is allowed.

Which of the following allows you to issue short-lived access tokens that act as temporary security credentials to allow access to your AWS resources?

[ ] Use AWS STS

[ ] All of the given options are correct

[ ] Use AWS Cognito to issue JSON Web Tokens (JWT)

[ ] Use AWS SSO

**Explanation**: **AWS Security Token Service (AWS STS)** is the service that you can use to create and provide trusted users with temporary security credentials that can control access to your AWS resources. Temporary security credentials work almost identically to the long-term access key credentials that your IAM users can use.

In this diagram, IAM user Alice in the Dev account (the role-assuming account) needs to access the Prod account (the role-owning account). Here’s how it works:

  1. Alice in the Dev account assumes an IAM role (WriteAccess) in the Prod account by calling AssumeRole.

  2. STS returns a set of temporary security credentials.

  3. Alice uses the temporary security credentials to access services and resources in the Prod account. Alice could, for example, make calls to Amazon S3 and Amazon EC2, which are granted by the WriteAccess role.

![Fig. 1 Security Token Service Application](../../../../../img/SAA-CO2/security/iam/sts/fig01.png)

> **Using AWS Cognito to issue JSON Web Tokens (JWT)** is incorrect because the Amazon Cognito service is primarily used for user authentication and not for providing access to your AWS resources. A JSON Web Token (JWT) is meant to be used for user authentication and session management.

> **Using AWS SSO** is incorrect. Although the AWS SSO service uses STS, it does not issue short-lived credentials by itself. AWS Single Sign-On (SSO) is a cloud SSO service that makes it easy to centrally manage SSO access to multiple AWS accounts and business applications.

> The option that says **All of the above** is incorrect as only STS has the ability to provide temporary security credentials.

<br />
