# Simple Storage Service

1. A company must store around 500 GB of files, and expects the data size to increase to 80 TB over the next couple of months. The company needs access to this data at all times. Which of the following would be an ideal storage option for these requirements?

[ ] Amazon DynamoDB

[x] Amazon Simple Storage Service (Amazon S3)

[ ] Amazon S3 Glacier

[ ] Amazon Redshift

**Explanation**: Redshift is a data warehouse service used for structured data not files. S3 Glacier is not online at all times, so retrieving from Glacier requires some wait time as S3 Glacier is designed for archiving, not frequently accessing data.

<br />

2. An application provides a feature that allows users to securely download private and personal files. The web server is currently overwhelmed w/ serving files for download. A solutions architect must find a more effective solution to reduce the web server load and cost, and must allow users to download only their own files.

Which solution meets all requirements?

[x] Store the files securely on Amazon S3 and have the application generate an Amazon S3 presigned URL for the user to download.

[ ] Store the files in an encrypted Elastic Block Store (Amazon EBS) volume, and use a separate set of servers to serve the downloads.

[ ] Have the application encrypt the files and store them in the local Amazon EC2 instance prior to serving them up for download.

[ ] Create an Amazon CloudFront to distribute and cache the files.

**Explanation:** CloudFront is a delivery network that would work w/ signed URLs, but signed URLs are not mentioned. Additionally, CloudFront carries a high cost. EBS is high-performance block storage for EC2s that are applicable for high throughput transactions. The addition of an EC2 instance would transfer costs and problems to a new EC2 and encryption would mean that the user can still access all of the storage, except that storage would be encrypted. Instance store is great if we have a load balancer, but is still not a great option for storing user-sensitive data.

<br />

3. An analytics company is planning to offer a site analytics service to its users. The service will require that the users’ webpages include a JavaScript script that makes authenticated GET requests to the company’s Amazon S3 bucket.

What must a solutions architect do to ensure that the script will successfully execute?

[x] Enable cross-origin resource sharing (CORS) on the S3 bucket.

[ ] Enable S3 versioning on the S3 bucket.

[ ] Provide the users with a signed URL for the script.

[ ] Configure a bucket policy to allow public execute privileges.

**Explanation**: Web browsers will block the execution of a script that originates from a server with a different domain name than the webpage. Amazon S3 can be configured with CORS to send HTTP headers that allow the script execution.

<br />

4. A company needs to maintain access logs for a minimum of 5 years due to regulatory requirements. The data is rarely accessed once stored, but must be accessible with one day’s notice if it is needed.

What is the MOST cost-effective data storage solution that meets these requirements?

[x] Store the data in Amazon S3 Glacier Deep Archive storage and delete the objects after 5 years using a
lifecycle rule.

[ ] Store the data in Amazon S3 Standard storage and transition to Amazon S3 Glacier after 30 days using a
lifecycle rule.

[ ] Store the data in logs using Amazon CloudWatch Logs and set the retention period to 5 years.

[ ] Store the data in Amazon S3 Standard-Infrequent Access (S3 Standard-IA) storage and delete the
objects after 5 years using a lifecycle rule.

**Explanation**: Data can be stored directly in Amazon S3 Glacier Deep Archive. This is the cheapest S3 storage class.

<br />

5. There was an incident in your production environment where the user data stored in the S3 bucket has been accidentally deleted by one of the Junior DevOps Engineers. The issue was escalated to your manager and after a few days, you were instructed to improve the security and protection of your AWS resources.

What combination of the following options will protect the S3 objects in your bucket from both accidental deletion and overwriting? (Select TWO.)

[x] Enable Multi-Factor Authentication Delete

[ ] Enable Amazon S3 Intelligent-Tiering

[ ] Disallow S3 Delete using an IAM bucket policy

[ ] Provide access to S3 data strictly through pre-signed URL only

[x] Enable Versioning

**Explanation**: By using Versioning and enabling MFA (Multi-Factor Authentication) Delete, you can secure and recover your S3 objects from accidental deletion or overwrite.

Versioning is a means of keeping multiple variants of an object in the same bucket. Versioning-enabled buckets enable you to recover objects from accidental deletion or overwrite. You can use versioning to preserve, retrieve, and restore every version of every object stored in your Amazon S3 bucket. With versioning, you can easily recover from both unintended user actions and application failures.

You can also optionally add another layer of security by configuring a bucket to enable MFA (Multi-Factor Authentication) Delete, which requires additional authentication for either of the following operations:

* Change the versioning state of your bucket

* Permanently delete an object version

MFA Delete requires two forms of authentication together:

* Your security credentials

* The concatenation of a valid serial number, a space, and the six-digit code displayed on an approved authentication device

> **Providing access to S3 data strictly through pre-signed URL only** is incorrect since a pre-signed URL gives access to the object identified in the URL. Pre-signed URLs are useful when customers perform an object upload to your S3 bucket, but does not help in preventing accidental deletes.

> **Disallowing S3 Delete using an IAM bucket policy** is incorrect since you still want users to be able to delete objects in the bucket, and you just want to prevent accidental deletions. Disallowing S3 Delete using an IAM bucket policy will restrict all delete operations to your bucket.

> **Enabling Amazon S3 Intelligent-Tiering** is incorrect since S3 intelligent tiering does not help in this situation.

<br />

6. A company collects atmospheric data such as temperature, air pressure, and humidity from different countries. Each site location is equipped with various weather instruments and a high-speed Internet connection. The average collected data in each location is around 500 GB and will be analyzed by a weather forecasting application hosted in Northern Virginia. As the Solutions Architect, you need to aggregate all the data in the fastest way.

Which of the following options can satisfy the given requirement?

[ ] Set up a Site-to-Site VPN connection.

[x] Enable Transfer Acceleration in the destination bucket and upload the collected data using Multipart Upload.

[ ] Upload the data to the closest S3 bucket. Set up a cross-region replication and copy the objects to the destination bucket.

**Explanation**: Since the weather forecasting application is located in N.Virginia, you need to transfer all the data in the same AWS Region. With Amazon S3 Transfer Acceleration, you can speed up content transfers to and from Amazon S3 by as much as 50-500% for long-distance transfer of larger objects. Multipart upload allows you to upload a single object as a set of parts. After all the parts of your object are uploaded, Amazon S3 then presents the data as a single object. This approach is the fastest way to aggregate all the data.

> **Upload the data to the closest S3 bucket. Set up a cross-region replication and copy the objects to the destination bucket** is incorrect because replicating the objects to the destination bucket takes about 15 minutes. Take note that the requirement in the scenario is to aggregate the data in the fastest way.

> **Set up a Site-to-Site VPN connection** is incorrect because setting up a VPN connection is not needed in this scenario. Site-to-Site VPN is just used for establishing secure connections between an on-premises network and Amazon VPC. Also, this approach is not the fastest way to transfer your data. You must use Amazon S3 Transfer Acceleration.

<br />

7. A government agency plans to store confidential tax documents on AWS. Due to the sensitive information in the files, the Solutions Architect must restrict the data access requests made to the storage solution to a specific Amazon VPC only. The solution should also prevent the files from being deleted or overwritten to meet the regulatory requirement of having a write-once-read-many (WORM) storage model.

Which combination of the following options should the Architect implement? (Select TWO.)

[x] Create a new Amazon S3 bucket w/ the S3 Object Lock feature enabled. Store the documents in the bucket and set the Legal Hold option for object retention.

[ ] Enable Object Lock but disable Object Versioning on the new Amazon S3 bucket to comply w/ the write-once-read-many (WORM) storage model requirement.

[x] Configure an Amazon S3 Access Point for the S3 bucket to restrict data access to a particular Amazon VPC only.

[ ] Store the tax documents in the Amazon S3 Glacier Instant Retrieval storage class to restrict fast data retrieval to a particular Amazon VPC of your choice.

[ ] Set up a new Amazon S3 bucket to store the tax documents and integrate it w/ AWS Network Firewall. Configure the Network Firewall to only accept data access requests from a specific Amazon VPC.

**Explanation**: Amazon S3 access points simplify data access for any AWS service or customer application that stores data in S3. Access points are named network endpoints that are attached to buckets that you can use to perform S3 object operations, such as `GetObject` and `PutObject`.

Each access point has distinct permissions and network controls that S3 applies for any request that is made through that access point. Each access point enforces a customized access point policy that works in conjunction with the bucket policy that is attached to the underlying bucket. You can configure any access point to accept requests only from a virtual private cloud (VPC) to restrict Amazon S3 data access to a private network. You can also configure custom block public access settings for each access point.

You can also use Amazon S3 Multi-Region Access Points to provide a global endpoint that applications can use to fulfill requests from S3 buckets located in multiple AWS Regions. You can use Multi-Region Access Points to build multi-Region applications with the same simple architecture used in a single Region, and then run those applications anywhere in the world. Instead of sending requests over the congested public internet, Multi-Region Access Points provide built-in network resilience with acceleration of internet-based requests to Amazon S3. Application requests made to a Multi-Region Access Point global endpoint use AWS Global Accelerator to automatically route over the AWS global network to the S3 bucket with the lowest network latency.

With S3 Object Lock, you can store objects using a write-once-read-many (WORM) model. Object Lock can help prevent objects from being deleted or overwritten for a fixed amount of time or indefinitely. You can use Object Lock to help meet regulatory requirements that require WORM storage, or to simply add another layer of protection against object changes and deletion.

Before you lock any objects, you have to enable a bucket to use S3 Object Lock. You enable Object Lock when you create a bucket. After you enable Object Lock on a bucket, you can lock objects in that bucket. When you create a bucket with Object Lock enabled, you can't disable Object Lock or suspend versioning for that bucket.

> The option that says: **Set up a new Amazon S3 bucket to store the tax documents and integrate it with AWS Network Firewall. Configure the Network Firewall to only accept data access requests from a specific Amazon VPC** is incorrect because you cannot directly use an AWS Network Firewall to restrict S3 bucket data access requests to a specific Amazon VPC only. You have to use an Amazon S3 Access Point instead for this particular use case. An AWS Network Firewall is commonly integrated to your Amazon VPC and not to an S3 bucket.

> The option that says: **Store the tax documents in the Amazon S3 Glacier Instant Retrieval storage class to restrict fast data retrieval to a particular Amazon VPC of your choice** is incorrect because Amazon S3 Glacier Instant Retrieval is just an archive storage class that delivers the lowest-cost storage for long-lived data that is rarely accessed and requires retrieval in milliseconds. It neither provides write-once-read-many (WORM) storage nor a fine-grained network control that restricts S3 bucket access to a specific Amazon VPC.

> The option that says: **Enable Object Lock but disable Object Versioning on the new Amazon S3 bucket to comply with the write-once-read-many (WORM) storage model requirement** is incorrect. Although the Object Lock feature does provide write-once-read-many (WORM) storage, the Object Versioning feature must also be enabled too in order for this to work. In fact, you cannot manually disable the Object Versioning feature if you have already selected the Object Lock option.

<br />
