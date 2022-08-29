# Server Access Logs

1. A large electronics company is using Amazon Simple Storage Service to store important documents. For reporting purposes, they want to track and log every request access to their S3 buckets including the requester, bucket name, request time, request action, referrer, turnaround time, and error code information. The solution should also provide more visibility into the object-level operations of the bucket.

Which is the best solution among the following options that can satisfy the requirement?

[ ] Enable the Requester Pays option to track access via AWS Billing.

[ ] Enable AWS CloudTrail to audit all Amazon S3 bucket access.

[ ] Enable server access logging for all required Amazon S3 buckets.

[ ] Enable Amazon S3 Event Notifications for PUT and POST.

**Explanation**: **Amazon S3** is integrated with AWS CloudTrail, a service that provides a record of actions taken by a user, role, or an AWS service in Amazon S3. CloudTrail captures a subset of API calls for Amazon S3 as events, including calls from the Amazon S3 console and code calls to the Amazon S3 APIs.

**AWS CloudTrail** logs provide a record of actions taken by a user, role, or an AWS service in Amazon S3, while **Amazon S3 server access logs** provide detailed records for the requests that are made to an S3 bucket.

For this scenario, you can use CloudTrail and the Server Access Logging feature of Amazon S3. However, it is mentioned in the scenario that they need detailed information about every access request sent to the S3 bucket including the referrer and turn-around time information. These two records are not available in CloudTrail.

Hence, the correct answer is: **Enable server access logging for all required Amazon S3 buckets.**

> The option that says: **Enable AWS CloudTrail to audit all Amazon S3 bucket access** is incorrect because enabling AWS CloudTrail alone won't give detailed logging information for object-level access.

> The option that says: **Enabling the Requester Pays option to track access via AWS Billing** is incorrect because this action refers to AWS billing and not for logging.

> The option that says: **Enabling Amazon S3 Event Notifications for PUT and POST** is incorrect because we are looking for a logging solution and not an event notification.

<br />
