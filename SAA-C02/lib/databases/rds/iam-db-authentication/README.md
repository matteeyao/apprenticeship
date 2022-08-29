# IAM DB Authentication

1. A financial application is composed of an Auto Scaling group of EC2 instances, an Application Load Balancer, and a MySQL RDS instance in a Multi-AZ Deployments configuration. To protect the confidential data of your customers, you have to ensure that your RDS database can only be accessed using the profile credentials specific to your EC2 instances via an authentication token.

As the Solutions Architect of the company, which of the following should you do to meet the above requirement?

[ ] Configure SSL in your application to encrypt the database connection to RDS.

[ ] Create an IAM Role and assign it to your EC2 instances which will grant exclusive access to your RDS instance.

[x] Enable the IAM DB Authentication.

[ ] Use a combination of IAM and STS to restrict access to your RDS instance via a temporary token.

**Explanation**: You can authenticate to your DB instance using AWS Identity and Access Management (IAM) database authentication. IAM database authentication works with MySQL and PostgreSQL. With this authentication method, you don't need to use a password when you connect to a DB instance. Instead, you use an authentication token.

An ***authentication token*** is a unique string of characters that Amazon RDS generates on request. Authentication tokens are generated using AWS Signature Version 4. Each token has a lifetime of 15 minutes. You don't need to store user credentials in the database, because authentication is managed externally using IAM. You can also still use standard database authentication.

IAM database authentication provides the following benefits:

  1. Network traffic to and from the database is encrypted using Secure Sockets Layer (SSL).

  2. You can use IAM to centrally manage access to your database resources, instead of managing access individually on each DB instance.

  3. For applications running on Amazon EC2, you can use profile credentials specific to your EC2 instance to access your database instead of a password, for greater security

> **Configuring SSL in your application to encrypt the database connection to RDS** is incorrect because an SSL connection is not using an authentication token from IAM. Although configuring SSL to your application can improve the security of your data in flight, it is still not a suitable option to use in this scenario.

> **Creating an IAM Role and assigning it to your EC2 instances which will grant exclusive access to your RDS instance** is incorrect because although you can create and assign an IAM Role to your EC2 instances, you still need to configure your RDS to use IAM DB Authentication.

> **Using a combination of IAM and STS to restrict access to your RDS instance via a temporary token** is incorrect because you have to use IAM DB Authentication for this scenario, and not a combination of an IAM and STS. Although STS is used to send temporary tokens for authentication, this is not a compatible use case for RDS.

<br />

2. A company needs secure access to its Amazon RDS for MySQL database that is used by multiple applications. Each IAM user must use a short-lived authentication token to connect to the database.

Which of the following is the most suitable solution in this scenario?

[ ] Use AWS SSO to access the RDS database.

[ ] Use AWS Secrets Manager to generate and store short-lived authentication tokens.

[ ] Use an MFA token to access and connect to a database.

[ ] Use IAM DB Authentication and create database accounts using the AWS-provided `AWSAuthenticationPlugin` plugin in MySQL.

**Explanation**: You can authenticate to your DB instance using AWS Identity and Access Management (IAM) database authentication. IAM database authentication works with MySQL and PostgreSQL. With this authentication method, you don't need to use a password when you connect to a DB instance.

An **authentication token** is a string of characters that you use instead of a password. After you generate an authentication token, it's valid for 15 minutes before it expires. If you try to connect using an expired token, the connection request is denied.

Since the scenario asks you to create a short-lived authentication token to access an Amazon RDS database, you can use an IAM database authentication when connecting to a database instance. Authentication is handled by `AWSAuthenticationPlugin`—an AWS-provided plugin that works seamlessly with IAM to authenticate your IAM users.

IAM database authentication provides the following benefits:

* Network traffic to and from the database is encrypted using Secure Sockets Layer (SSL).

* You can use IAM to centrally manage access to your database resources, instead of managing access individually on each DB instance.

* For applications running on Amazon EC2, you can use profile credentials specific to your EC2 instance to access your database instead of a password, for greater security

Hence, the correct answer is the option that says: **Use IAM DB Authentication and create database accounts using the AWS-provided `AWSAuthenticationPlugin` plugin in MySQL.**

> The options that say: **Use AWS SSO to access the RDS database is incorrect because AWS SSO just enables you to centrally manage SSO access and user permissions for all of your AWS accounts managed through AWS Organizations.**

> The option that says: **Use AWS Secrets Manager to generate and store short-lived authentication tokens** is incorrect because AWS Secrets Manager is not a suitable service to create an authentication token to access an Amazon RDS database. It's primarily used to store passwords, secrets, and other sensitive credentials. It can't generate a short-lived token either. You have to use IAM DB Authentication instead.

> The option that says: **Use an MFA token to access and connect to a database** is incorrect because you can't use an MFA token to connect to your database. You have to set up IAM DB Authentication instead.

<br />

3. A web application, which is hosted in your on-premises data center and uses a MySQL database, must be migrated to AWS Cloud. You need to ensure that the network traffic to and from your RDS database instance is encrypted using SSL. For improved security, you have to use the profile credentials specific to your EC2 instance to access your database, instead of a password.   

Which of the following should you do to meet the above requirement?

[ ] Configure your RDS database to enable encryption.

[ ] Set up an RDS database and enable the IAM DB Authentication.

[ ] Launch the mysql client using the `--ssl-ca` parameter when connecting to the database.

[ ] Launch a new RDS database instance w/ the Backtrack feature enabled.

**Explanation**: You can authenticate to your DB instance using AWS Identity and Access Management (IAM) database authentication. IAM database authentication works with MySQL and PostgreSQL. With this authentication method, you don't need to use a password when you connect to a DB instance. Instead, you use an authentication token.

An *authentication token* is a unique string of characters that Amazon RDS generates on request. Authentication tokens are generated using AWS Signature Version 4. Each token has a lifetime of 15 minutes. You don't need to store user credentials in the database, because authentication is managed externally using IAM. You can also still use standard database authentication.

IAM database authentication provides the following benefits:

* Network traffic to and from the database is encrypted using Secure Sockets Layer (SSL).

* You can use IAM to centrally manage access to your database resources, instead of managing access individually on each DB instance.

* For applications running on Amazon EC2, you can use profile credentials specific to your EC2 instance to access your database instead of a password, for greater security.

Hence, **setting up an RDS database and enable the IAM DB Authentication** is the correct answer based on the above reference.

> Launching a new RDS database instance with the Backtrack feature enabled is incorrect because the Backtrack feature simply "rewinds" the DB cluster to the time you specify. Backtracking is not a replacement for backing up your DB cluster so that you can restore it to a point in time. However, you can easily undo mistakes using the backtrack feature if you mistakenly perform a destructive action, such as a `DELETE` without a `WHERE` clause.

> **Configuring your RDS database to enable encryption** is incorrect because this encryption feature in RDS is mainly for securing your Amazon RDS DB instances and snapshots at rest. The data that is encrypted at rest includes the underlying storage for DB instances, its automated backups, Read Replicas, and snapshots.

> **Launching the mysql client using the `--ssl-ca` parameter when connecting to the database** is incorrect because even though using the `--ssl-ca` parameter can provide SSL connection to your database, you still need to use IAM database connection to use the profile credentials specific to your EC2 instance to access your database instead of a password.

<br />
