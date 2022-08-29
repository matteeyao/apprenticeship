# CloudTrail

1. A healthcare company stores sensitive patient health records in their on-premises storage systems. These records must be kept indefinitely and protected from any type of modifications once they are stored. Compliance regulations mandate that the records must have granular access control and each data access must be audited at all levels. Currently, there are millions of obsolete records that are not accessed by their web application, and their on-premises storage is quickly running out of space. The Solutions Architect must design a solution to immediately move existing records to AWS and support the ever-growing number of new health records.

Which of the following is the most suitable solution that the Solutions Architect should implement to meet the above requirements?

[ ] Set up AWS DataSync to move the existing health records from the on-premises network to the AWS Cloud. Launch a new Amazon S3 bucket to store existing and new records. Enable AWS CloudTrail w/ Data Events and Amazon S3 Object Lock in the bucket.

[ ] Set up AWS DataSync to move the existing health records from the on-premises network to the AWS Cloud. Launch a new Amazon S3 bucket to store existing and new records. Enable AWS CloudTrail w/ Management Events and Amazon S3 Object Lock in the bucket.

**Explanation**: AWS CloudTrail is an AWS service that helps you enable governance, compliance, and operational and risk auditing of your AWS account. Actions taken by a user, role, or an AWS service are recorded as events in CloudTrail. Events include actions taken in the AWS Management Console, AWS Command Line Interface, and AWS SDKs and APIs.

There are two types of events that you configure your CloudTrail for:

* **Management Events** ▶︎ Management Events provide visibility into management operations that are performed on resources in your AWS account. These are also known as control plane operations. Management events can also include non-API events that occur in your account.

* **Data Events** ▶︎ Data Events, on the other hand, provide visibility into the resource operations performed on or within a resource. These are also known as data plane operations. It allows granular control of data event logging with advanced event selectors. You can currently log data events on different resource types such as Amazon S3 object-level API activity (e.g. GetObject, DeleteObject, and PutObject API operations), AWS Lambda function execution activity (the Invoke API), DynamoDB Item actions, and many more.

You can record the actions that are taken by users, roles, or AWS services on Amazon S3 resources and maintain log records for auditing and compliance purposes. To do this, you can use server access logging, AWS CloudTrail logging, or a combination of both. AWS recommends that you use AWS CloudTrail for logging bucket and object-level actions for your Amazon S3 resources.

Hence, the correct answer is: **Set up AWS DataSync to move the existing health records from the on-premises network to the AWS Cloud. Launch a new Amazon S3 bucket to store existing and new records. Enable AWS CloudTrail with Data Events and Amazon S3 Object Lock in the bucket.**

> The option that says: **Set up AWS DataSync to move the existing health records from the on-premises network to the AWS Cloud. Launch a new Amazon S3 bucket to store existing and new records. Enable AWS CloudTrail with Management Events and Amazon S3 Object Lock in the bucket** is incorrect. Although it is right to use AWS DataSync to move the health records, you still have to configure Data Events in AWS CloudTrail and not Management Events. This type of event only provides visibility into management operations that are performed on resources in your AWS account and not the data events that are happening in the individual objects in Amazon S3.

<br />
