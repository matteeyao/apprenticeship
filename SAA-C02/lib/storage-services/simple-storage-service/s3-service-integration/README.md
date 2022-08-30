# Amazon S3 Service Integration

## Amazon FSx for Lustre and Amazon S3 data lakes

1. An AI-powered Forex trading application consumes thousands of data sets to train its machine learning model. The application’s workload requires a high-performance, parallel hot storage to process the training datasets concurrently. It also needs cost-effective cold storage to archive those datasets that yield low profit.

Which of the following Amazon storage services should the developer use?

[ ] Use Amazon FSx For Lustre and Amazon EBS Provisioned IOPS SSD (io1) volumes for hot and cold storage respectively.

[ ] Use Amazon Elastic File System and Amazon S3 for hot and cold storage respectively.

[ ] Use Amazon FSx For Windows File Server and Amazon S3 for hot and cold storage respectively.

[ ] Use Amazon FSx For Lustre and Amazon S3 for hot and cold storage respectively.

**Explanation**: **Hot storage** refers to the storage that keeps frequently accessed data (hot data). **Warm storage** refers to the storage that keeps less frequently accessed data (warm data). **Cold storage** refers to the storage that keeps rarely accessed data (cold data). In terms of pricing, the colder the data, the cheaper it is to store, and the costlier it is to access when needed.

**Amazon FSx For Lustre** is a high-performance file system for fast processing of workloads. Lustre is a popular open-source **parallel file system** which stores data across multiple network file servers to maximize performance and reduce bottlenecks.

**Amazon FSx for Windows File Server** is a fully managed Microsoft Windows file system with full support for the SMB protocol, Windows NTFS, Microsoft Active Directory (AD) Integration.

**Amazon Elastic File System** is a fully-managed file storage service that makes it easy to set up and scale file storage in the Amazon Cloud.

The question has two requirements:

* High-performance, parallel hot storage to process the training datasets concurrently.

* Cost-effective cold storage to keep the archived datasets that are accessed infrequently

In this case, we can use Amazon FSx For Lustre for the first requirement, as it provides a high-performance, parallel file system for hot data. On the second requirement, we can use Amazon S3 for storing cold data. Amazon S3 supports a cold storage system via Amazon S3 Glacier / Glacier Deep Archive.

> **Using Amazon FSx For Lustre and Amazon EBS Provisioned IOPS SSD (io1) volumes for hot and cold storage respectively** is incorrect because the Provisioned IOPS SSD (io1) volumes are designed for storing hot data (data that are frequently accessed) used in I/O-intensive workloads. EBS has a storage option called "Cold HDD," but due to its price, it is not ideal for data archiving. EBS Cold HDD is much more expensive than Amazon S3 Glacier / Glacier Deep Archive and is often utilized in applications where sequential cold data is read less frequently.

> **Using Amazon Elastic File System and Amazon S3 for hot and cold storage respectively** is incorrect. Although EFS supports concurrent access to data, it does not have the high-performance ability that is required for machine learning workloads.

> **Using Amazon FSx For Windows File Server and Amazon S3 for hot and cold storage respectively** is incorrect because Amazon FSx For Windows File Server does not have a parallel file system, unlike Lustre.

<br />

2. A company wishes to query data that resides in multiple AWS accounts from a central data lake. Each account has its own Amazon S3 bucket that stores data unique to its business function. Users from different accounts must be granted access to the data lake based on their roles.

Which solution will minimize overhead and costs while meeting the required access patterns?

[ ] Use AWS Kinesis Firehose to consolidate data from multiple accounts into a single account.

[ ] Create a scheduled Lambda function for transferring data from multiple accounts to the S3 buckets of a central account.

[x] Use AWS Lake Formation to consolidate data from multiple accounts into a single account.

[ ] Use AWS Central Tower to centrally manage each account's S3 buckets.

**Explanation**: **AWS Lake Formation** is a service that makes it easy to set up a secure data lake in days. A data lake is a centralized, curated, and secured repository that stores all your data, both in its original form and prepared for analysis. A data lake enables you to break down data silos and combine different types of analytics to gain insights and guide better business decisions.

Amazon S3 forms the storage layer for Lake Formation. If you already use S3, you typically begin by registering existing S3 buckets that contain your data. Lake Formation creates new buckets for the data lake and import data into them. AWS always stores this data in your account, and only you have direct access to it.

AWS Lake Formation is integrated with AWS Glue which you can use to create a data catalog that describes available datasets and their appropriate business applications. Lake Formation lets you define policies and control data access with simple “grant and revoke permissions to data” sets at granular levels. You can assign permissions to IAM users, roles, groups, and Active Directory users using federation. You specify permissions on catalog objects (like tables and columns) rather than on buckets and objects.

Thus, the correct answer is: **Use AWS Lake Formation to consolidate data from multiple accounts into a single account**.

> The option that says: **Use AWS Kinesis Firehose to consolidate data from multiple accounts into a single account** is incorrect. Setting up a Kinesis Firehose in each and every account to move data into a single location is costly and impractical. A better approach is to set up cross-account sharing which is free with AWS Lake Formation.

> The option that says: **Create a scheduled Lambda function for transferring data from multiple accounts to the S3 buckets of a central account** is incorrect. This could be done by utilizing the AWS SDK, but implementation would be difficult and quite challenging to manage. Remember that the scenario explicitly mentioned that the solution must minimize management overhead.

> The option that says: **Use AWS Central Tower to centrally manage each account's S3 buckets** is incorrect because the AWS Central Tower service is primarily used to manage and govern multiple AWS accounts and not just S3 buckets. Using the AWS Lake Formation service is a more suitable choice.

<br />

3. A company needs to assess and audit all the configurations in their AWS account. It must enforce strict compliance by tracking all configuration changes made to any of its Amazon S3 buckets. Publicly accessible S3 buckets should also be identified automatically to avoid data breaches.

Which of the following options will meet this requirement?

[ ] Use AWS CloudTrail and review the event history of your AWS account.

[ ] Use AWS IAM to generate a credential report.

[ ] Use AWS Config to set up a rule in your AWS account.

[ ] Use AWS Trusted Advisor to analyze your AWS environment.

**Explanation**: **AWS Config** is a service that enables you to assess, audit, and evaluate the configurations of your AWS resources. Config continuously monitors and records your AWS resource configurations and allows you to automate the evaluation of recorded configurations against desired configurations. With Config, you can review changes in configurations and relationships between AWS resources, dive into detailed resource configuration histories, and determine your overall compliance against the configurations specified in your internal guidelines. This enables you to simplify compliance auditing, security analysis, change management, and operational troubleshooting.

You can use AWS Config to evaluate the configuration settings of your AWS resources. By creating an AWS Config rule, you can enforce your ideal configuration in your AWS account. It also checks if the applied configuration in your resources violates any of the conditions in your rules. The AWS Config dashboard shows the compliance status of your rules and resources. You can verify if your resources comply with your desired configurations and learn which specific resources are noncompliant.

Hence, the correct answer is: **Use AWS Config to set up a rule in your AWS account**.

> The option that says: **Use AWS Trusted Advisor to analyze your AWS environment** is incorrect because AWS Trusted Advisor only provides best practice recommendations. It cannot define rules for your AWS resources.

> The option that says: **Use AWS IAM to generate a credential report** is incorrect because this report will not help you evaluate resources. The IAM credential report is just a list of all IAM users in your AWS account.

> The option that says: **Use AWS CloudTrail and review the event history of your AWS account** is incorrect. Although it can track changes and store a history of what happened to your resources, this service still cannot enforce rules to comply with your organization's policies.

<br />
