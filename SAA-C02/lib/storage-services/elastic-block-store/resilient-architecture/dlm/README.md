# Data Lifecycle Manager

1. As part of the Business Continuity Plan of your company, your IT Director instructed you to set up an automated backup of all of the EBS Volumes for your EC2 instances as soon as possible. 

What is the fastest and most cost-effective solution to automatically back up all of your EBS Volumes?

[ ] For an automated solution, create a scheduled job that calls the "create-snapshot" command via the AWS CLI to take a snapshot of production EBS volumes periodically.

[ ] Set your Amazon Storage Gateway w/ EBS volumes as the data source and store the backups in your on-premises servers through the storage gateway.

[ ] Use Amazon Data Lifecycle Manager (Amazon DLM) to automate the creation of EBS snapshots.

[ ] Use EBS-cycle policy in Amazon S3 to automatically back up the EBS volumes.

**Explanation**: You can use Amazon Data Lifecycle Manager (Amazon DLM) to automate the creation, retention, and deletion of snapshots taken to back up your Amazon EBS volumes. Automating snapshot management helps you to:

  * Protect valuable data by enforcing a regular backup schedule.

  * Retain backups as required by auditors or internal compliance.

  * Reduce storage costs by deleting outdated backups.

Combined with the monitoring features of Amazon CloudWatch Events and AWS CloudTrail, Amazon DLM provides a complete backup solution for EBS volumes at no additional cost.

Hence, **using Amazon Data Lifecycle Manager (Amazon DLM) to automate the creation of EBS snapshots** is the correct answer as it is the fastest and most cost-effective solution that provides an automated way of backing up your EBS volumes.

> The option that says: **For an automated solution, create a scheduled job that calls the "create-snapshot" command via the AWS CLI to take a snapshot of production EBS volumes periodically** is incorrect because even though this is a valid solution, you would still need additional time to create a scheduled job that calls the "create-snapshot" command. It would be better to use Amazon Data Lifecycle Manager (Amazon DLM) instead as this provides you the fastest solution which enables you to automate the creation, retention, and deletion of the EBS snapshots without having to write custom shell scripts or creating scheduled jobs.

> **Setting your Amazon Storage Gateway with EBS volumes as the data source and storing the backups in your on-premises servers through the storage gateway** is incorrect as the Amazon Storage Gateway is used only for creating a backup of data from your on-premises server and not from the Amazon Virtual Private Cloud.

> **Using an EBS-cycle policy in Amazon S3 to automatically back up the EBS volumes** is incorrect as there is no such thing as EBS-cycle policy in Amazon S3.

<br />
