# AWS Backup

1. A company needs to use Amazon Aurora as the Amazon RDS database engine of their web application. The Solutions Architect has been instructed to implement a 90-day backup retention policy.

Which of the following options can satisfy the given requirement?

[ ] Configure RDS to export the automated snapshot automatically to Amazon S3 and create a lifecycle policy to delete the object after 90 days.

[ ] Create a daily scheduled event using CloudWatch Events and AWS Lambda to directly download the RDS automated snapshot to an S3 bucket. Archive snapshots older than 90 days to Glacier.

[ ] Configure an automated backup and set the backup retention period to 90 days.

[ ] Create an AWS Backup plan to take daily snapshots w/ a retention period of 90 days.

**Explanation**: **AWS Backup** is a centralized backup service that makes it easy and cost-effective for you to backup your application data across AWS services in the AWS Cloud, helping you meet your business and regulatory backup compliance requirements. AWS Backup makes protecting your AWS storage volumes, databases, and file systems simple by providing a central place where you can configure and audit the AWS resources you want to backup, automate backup scheduling, set retention policies, and monitor all recent backup and restore activity.

![Fig. 1 AWS Backup Overview](../../../../../img/SAA-CO2/storage-services/elastic-block-storage/aws-backup/fig01.png)

In this scenario, you can use AWS Backup to create a backup plan with a retention period of 90 days. A backup plan is a policy expression that defines when and how you want to back up your AWS resources. You assign resources to backup plans, and AWS Backup then automatically backs up and retains backups for those resources according to the backup plan.

Hence, the correct answer is: **Create an AWS Backup plan to take daily snapshots with a retention period of 90 days**.

> The option that says: **Configure an automated backup and set the backup retention period to 90 days** is incorrect because the maximum backup retention period for automated backup is only 35 days.

> The option that says: **Configure RDS to export the automated snapshot automatically to Amazon S3 and create a lifecycle policy to delete the object after 90 days** is incorrect because you can't export an automated snapshot automatically to Amazon S3. You must export the snapshot manually.

> The option that says: **Create a daily scheduled event using CloudWatch Events and AWS Lambda to directly download the RDS automated snapshot to an S3 bucket. Archive snapshots older than 90 days to Glacier** is incorrect because you cannot directly download or export an automated snapshot in RDS to Amazon S3. You have to copy the automated snapshot first for it to become a manual snapshot, which you can move to an Amazon S3 bucket. A better solution for this scenario is to simply use AWS Backup.

<br />
