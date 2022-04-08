# AWS Single Sign-On

So we talked about using a multi-account strategy in a previous lesson, and we know this is best practice, but managing user permissions for all these individual accounts can be a real headache.

AWS Single Sign-on simplifies this.

> Single sign-on (SSO) service helps **centrally manage** access to AWS accounts and business applications.
>
> * Centrally manage accounts
>
> * Use existing corporate identities
>
> * SSO access to business applications

* In addition to being able to centrally manage your accounts, you can sign in to AWS accounts and third party accounts in one place using the AWS SSO portal w/ your existing corporate identities

* W/ SSO, you can manage user permissions for all your AWS resources across all your accounts using AWS Organizations

## Granular Account-Level Permissions

Let's say you want to grant your security team administrative access to only the AWS accounts running your security tools, but then you only want to grant them auditor level permissions to other AWS accounts. We would use SSO to set this up.

## Active Directory and SAML Integration

So we mentioned SSO allows you to log into business applications like G suite and Office 365

SSO also integrates w/ Active Directory (AD) or any SAML 2.0 identity provider, for example, Azure AD

This allows users to log in to the AWS SSO portal w/ their Active Directory (AD) credentials.

This can also be used to grant access to AWS Organizations, so structures like OUs for development and production environments.

It can also grant access to any SAML 2.0 enabled applications.

> **S**ecurity **A**ssertion **M**arkup **L**anguage (SAML) is a standard for logging users into applications based on their sessions in another context.

* So, for example, one context being your Microsoft AD environment and another context being your Microsoft AD environment, and another context being your business application like G suite

* SAML allows you to log into the G suite application, for example, using your AD context and all sign on activities are recorded in AWS CloudTrail. This helps you meet your audit and compliance requirements

> [!EXAM-TIP]
> If you see SAML 2.0 in an exam question, look for SSO in one of the answers.

## Learning summary

> * Centrally manage access

* The SSO service helps us centrally manage access to AWS accounts and business applications

* So, on the exam, you might be presented w/ a scenario where it's asking about using existing corporate identities to sign into AWS services or third party applications

> * Examples: G Suite, Office 365, Salesforce
>
> * Use existing identities

* Anything that asks about using existing identities to log into other contexts. This is where Single Sign-on works.

> * Account-level permissions
>
> * SAML

* **S**ecurity **A**ssertion **M**arkup **L**anguage (SAML)
