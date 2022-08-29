# Pilot Light

1. A company asks a solutions architect to implement a pilot light disaster recovery (DR) strategy for an existing on-premises application. The application is self contained and does not need to access any databases.

Which solution will implement a pilot light DR strategy?

[ ] Back up the on-premises application, configuration, and data to an Amazon S3 bucket. When the on-premises application fails, build a new hosting environment on AWS and restore the application from the information that is stored in the S3 bucket. → `Incorrect. This is a backup and restore DR strategy. Backup and restore DR strategies typically have the lowest cost but highest recovery time. A solution that manually rebuilds the hosting infrastructure on AWS could take hours.`

[ ] Recreate the application hosting environment on AWS by using Amazon EC2 instances and stop the EC2 instances. When the on-premises application fails, start the stopped EC2 instances and direct 100% of application traffic to the EC2 instances that are running in the AWS Cloud. 

[ ] Recreate the application hosting environment on AWS by using Amazon EC2 instances. Direct 10% of application traffic to the EC2 instances that are running in the AWS Cloud. When the on-premises application fails, direct 100% of application traffic to the EC2 instances that are running in the AWS Cloud. → `Incorrect. This is a warm standby DR strategy. This solution recreates an existing application hosting environment in an AWS Region. This solution serves a portion of live traffic. With this DR strategy, RPO and RTO are usually a few minutes. However, costs are higher because this solutions runs resources continuously.`

[ ] Back up the on-premises application, configuration, and data to an Amazon S3 bucket. When the on-premises application fails, rebuild the on-premises hosting environment on AWS and restore the application from the information that is stored in the S3 bucket. → `Incorrect. This is a backup and restore DR strategy. Backup and restore DR strategies typically have the lowest cost but highest recovery time. A solution that manually rebuilds the hosting infrastructure on premises and downloads the data that a company has backed up could take hours or days.`

**Explanation**: **Recreate the application hosting environment on AWS by using Amazon EC2 instances and stop the EC2 instances. When the on-premises application fails, start the stopped EC2 instances and direct 100% of application traffic to the EC2 instances that are running in the AWS Cloud.** This is a pilot light DR strategy. This solution recreates an existing application hosting environment in an AWS Region. This solution turns off most (or all) resources and uses the resources only during tests or when DR failover is necessary. RPO and RTO are usually 10s of minutes.

A pilot light strategy simplifies recovery at the time of a disaster because the core infrastructure requirements are all in place. A pilot light strategy also minimizes the ongoing cost of DR by minimizing the active resources.

<br />
