# Single Sign-On

1. A company needs to integrate the Lightweight Directory Access Protocol (LDAP) directory service from the on-premises data center to the AWS VPC using IAM. The identity store which is currently being used is not compatible with SAML.

Which of the following provides the most valid approach to implement the integration?

[ ] Use AWS Single Sign-On (SSO) service to enable single sign-on between AWS and your LDAP.

[x] Develop an on-premises custom identity broker application and use STS to issue short-lived AWS credentials.

[ ] Use IAM roles to rotate the IAM credentials whenever LDAP credentials are updated.

[ ] Use an IAM policy that references the LDAP identifiers and AWS credentials.

**Explanation**: If your identity store is not compatible with SAML 2.0 then you can build a custom identity broker application to perform a similar function. The broker application authenticates users, requests temporary credentials for users from AWS, and then provides them to the user to access AWS resources.

The application verifies that employees are signed into the existing corporate network's identity and authentication system, which might use LDAP, Active Directory, or another system. The identity broker application then obtains temporary security credentials for the employees.

To get temporary security credentials, the identity broker application calls either `AssumeRole` or `GetFederationToken` to obtain temporary security credentials, depending on how you want to manage the policies for users and when the temporary credentials should expire. The call returns temporary security credentials consisting of an AWS access key ID, a secret access key, and a session token. The identity broker application makes these temporary security credentials available to the internal company application. The app can then use the temporary credentials to make calls to AWS directly. The app caches the credentials until they expire, and then requests a new set of temporary credentials.

![Fig. 1 Custom Identity Broker Architecture](../../../../img/security/iam/identity-broker/fig01.png)

> **Using an IAM policy that references the LDAP identifiers and AWS credentials** is incorrect because using an IAM policy is not enough to integrate your LDAP service to IAM. You need to use SAML, STS, or a custom identity broker.

> **Using AWS Single Sign-On (SSO) service to enable single sign-on between AWS and your LDAP** is incorrect because the scenario did not require SSO and in addition, the identity store that you are using is not SAML-compatible.

> **Using IAM roles to rotate the IAM credentials whenever LDAP credentials are updated** is incorrect because manually rotating the IAM credentials is not an optimal solution to integrate your on-premises and VPC network. You need to use SAML, STS, or a custom identity broker.

<br />
