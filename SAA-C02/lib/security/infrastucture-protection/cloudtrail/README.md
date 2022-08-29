# CloudTrail

1. A company troubleshoots the operational issues of their cloud architecture by logging the AWS API call history of all AWS resources. The Solutions Architect must implement a solution to quickly identify the most recent changes made to resources in their environment, including creation, modification, and deletion of AWS resources. One of the requirements is that the generated log files should be encrypted to avoid any security issues.

Which of the following is the most suitable approach to implement the encryption?

[ ] Use CloudTrail and configuring the destination Amazon Glacier archive to use Server-Side Encryption (SSE)

[ ] Use CloudTrail and configure the destination S3 bucket to use Server-Side Encryption (SSE).

[ ] Use CloudTrail w/ its default settings.

[ ] Use CloudTrail and configure the destination S3 bucket to use Server Side Encryption (SSE) w/ AES-128 encryption algorithm.

**Explanation**: By default, CloudTrail event log files are encrypted using Amazon S3 server-side encryption (SSE). You can also choose to encrypt your log files with an AWS Key Management Service (AWS KMS) key. You can store your log files in your bucket for as long as you want. You can also define Amazon S3 lifecycle rules to archive or delete log files automatically. If you want notifications about log file delivery and validation, you can set up Amazon SNS notifications.

> **Using CloudTrail and configuring the destination Amazon Glacier archive to use Server-Side Encryption (SSE)** is incorrect because CloudTrail stores the log files to S3 and not in Glacier. Take note that by default, CloudTrail event log files are already encrypted using Amazon S3 server-side encryption (SSE).

> **Using CloudTrail and configuring the destination S3 bucket to use Server-Side Encryption (SSE)** is incorrect because CloudTrail event log files are already encrypted using the Amazon S3 server-side encryption (SSE) which is why you do not have to do this anymore.

> **Use CloudTrail and configure the destination S3 bucket to use Server Side Encryption (SSE) with AES-128 encryption algorithm** is incorrect because Cloudtrail event log files are already encrypted using the Amazon S3 server-side encryption (SSE) by default. Additionally, SSE-S3 only uses the AES-256 encryption algorithm and not the AES-128.

<br />
