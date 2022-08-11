# Simple Storage Service

1. A company must store around 500 GB of files, and expects the data size to increase to 80 TB over the next couple of months. The company needs access to this data at all times. Which of the following would be an ideal storage option for these requirements?

[ ] Amazon DynamoDB

[x] Amazon Simple Storage Service (Amazon S3)

[ ] Amazon S3 Glacier

[ ] Amazon Redshift

**Explanation**: Redshift is a data warehouse service used for structured data not files. S3 Glacier is not online at all times, so retrieving from Glacier requires some wait time as S3 Glacier is designed for archiving, not frequently accessing data.

2. An application provides a feature that allows users to securely download private and personal files. The web server is currently overwhelmed w/ serving files for download. A solutions architect must find a more effective solution to reduce the web server load and cost, and must allow users to download only their own files.

Which solution meets all requirements?

[x] Store the files securely on Amazon S3 and have the application generate an Amazon S3 presigned URL for the user to download.

[ ] Store the files in an encrypted Elastic Block Store (Amazon EBS) volume, and use a separate set of servers to serve the downloads.

[ ] Have the application encrypt the files and store them in the local Amazon EC2 instance prior to serving them up for download.

[ ] Create an Amazon CloudFront to distribute and cache the files.

**Explanation:** CloudFront is a delivery network that would work w/ signed URLs, but signed URLs are not mentioned. Additionally, CloudFront carries a high cost. EBS is high-performance block storage for EC2s that are applicable for high throughput transactions. The addition of an EC2 instance would transfer costs and problems to a new EC2 and encryption would mean that the user can still access all of the storage, except that storage would be encrypted. Instance store is great if we have a load balancer, but is still not a great option for storing user-sensitive data.
