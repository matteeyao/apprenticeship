# Security and Management

1. How can you use AWS Identity and Access Management (IAM) to control access to administrative actions on the Amazon S3 File Gateway? (Select two)

[x] Create an IAM policy and attach it to an IAM role.

[x] Create an IAM policy and attach it to an IAM user or IAM groups.

[ ] Attach an IAM policy to the AWS Storage Gateway console.

[ ] Attach an IAM policy to the AWS Command Line Interface (AWS CLI).

**Explanation**: IAM is used to control who can sign in as an authorized user of an account (authenticated) and what they have permission to do (authorized). You manage access in AWS by creating policies with specific permissions and attaching them to IAM identities or resources. AWS deﬁnes several authentication identities. These include AWS account root user, IAM user, IAM group, and IAM role. 

So, if you want to control access to administrative actions on the S3 File Gateway, you create an IAM policy and attach it to an IAM role, or, you create an IAM policy and attach it to an IAM user or IAM group.

2. Which statements are true about the refresh cache operation? (Select three) 

[x] It finds objects in the S3 bucket that were added, removed, or replaced.

[ ] It *can only* be performed using the AWS Storage Gateway application programming interface (API)

[x] It *can be* performed from the AWS Storage Gateway console.

[x] It *can be* scheduled.

**Explanation**: The refresh cache operation finds objects in the S3 bucket that have been added, removed, or replaced since the gateway last listed the bucket contents and cached results.  

To refresh the cached inventory of objects for the specified ﬁle share, you can use the Storage Gateway console or the refresh cache operation in the Storage Gateway API. You can also modify the ﬁle share settings to automatically cache refresh from Amazon S3 at specific intervals.

3. What can you use to limit file permissions if you configure guest access for your Amazon S3 File Gateway?

**Portable Operating System Interface (POSIX) permissions**

**Explanation**: Not **AWS Identity and Access Management (IAM) policy**. Control ﬁle or directory access using POSIX or, for ﬁne grained permissions, use Windows ACLs. If you configure guest access authentication, POSIX is used for permissions.

4. What are some best practices when reading or writing to an Amazon S3 bucket through an Amazon S3 File Gateway? (Select two)

[x] When using two or more S3 File Gateways to write to the same S3 bucket, use unique object prefixes for each file share.

[ ] Use only one S3 File Gateway and file share to write and read to the S3 bucket.

[ ] Permit multiple Network File System (NFS) clients to write to the same S3 bucket through a single S3 File Gateway.

[x] Use one S3 File Gateway to write to an S3 bucket and another File Gateway to read from the same S3 bucket.

**Explanation**: When multiple gateways or file shares write to the same S3 bucket, unpredictable results might occur. You can prevent this in two ways:

- Configure your S3 bucket so that only one file share can write to it.

- Conﬁgure a separate, unique object preﬁx for each ﬁle share.

Multiple clients can mount to the same file share as long as they are using the same file protocol.

5. Which methods does AWS Storage Gateway use to secure your data in transit or data at rest? (Select TWO.)

[ ] Challenge-Handshake Authentication Protocol (CHAP) between the Amazon S3 File Gateway and AWS for data in transit

[x] Server-side encryption with 256-bit Advanced Encryption Standard (AES-256) for data at rest

[x] Secure Socket Layers/Transport Layer Security (SSL/TLS) for data in transit

[ ] AWS Identity and Access Management (IAM) between on-premises application and the Storage Gateway for data in transit

**Explanation**: Data that is in the cache is not encrypted. Your data is encrypted in transit and in the AWS Cloud. All  data transferred between any type of gateway appliance and AWS storage is encrypted with SSL/TLS. All data stored by Storage Gateway in Amazon S3 is encrypted at the server side with 256-bit AES-256.
