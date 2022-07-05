# Secrets Manager

This is a service that helps you rotate, manage, and retrieve all kinds of secrets, like database credentials, API keys. Using Secrets Manager, you can secure audit and manage secrets to access resources in AWS on third party services and on premises.

> **What is Secrets Manager?**
>
> * Similar to Systems Manager Parameter Store
>
> * **Charge** per secret stored and per 10,000 API calls
>
> * Automatically **rotate** secrets
>
> * Apply the new key/password in RDS for you
>
> * Generate **random secrets**

Out of the box, Secrets Manager provides full key rotation integration w/ RDS. Secrets Manager can rotate the keys and automatically apply the new credentials in RDS on your behalf. So what about key rotation for services other than RDS? You can use Lambda to write a function to rotate your keys and this is integrated directly in the Secrets Manager console. Another huge difference, and again a win for Secrets Manager over Parameter Store is the ability to generate random secrets. You can randomly generate passwords in CloudFormation and store the passwords in Secrets Manager. This is not just a functionality for CloudFormation. The AWS SDKs can be used to do this in your own application code. Secrets Manager can also be shared across accounts. So there's certainly still a place for Parameter Store. W/ Parameter Store, you can store secrets and encrypt them, but you can also store un-encrypted data and it's all free.
