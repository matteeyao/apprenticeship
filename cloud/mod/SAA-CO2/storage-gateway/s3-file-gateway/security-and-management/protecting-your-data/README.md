# Protecting Your Data

## Data encryption

Data that is in the cache is not encrypted. Your data is encrypted in transit and in the AWS Cloud. 

* All data transferred between any type of gateway appliance and AWS storage is encrypted with Secure Sockets Layer/Transport Layer Security (SSL/TLS).

* All data stored by Storage Gateway in Amazon S3 is encrypted, using server-side encryption, with 256-bit Advanced Encryption Standard (AES-256). The objects are encrypted using server-side encryption with either:

  * Amazon S3 server-side encryption (SSE-S3) managed inside Amazon S3 (default)

  * AWS Key Management Service (AWS KMS) keys managed with AWS KMS

## Access to the file share

To restrict file access for your NFS and SMB clients, you can edit the file share access setting within the Storage Gateway console.

### NFS file shares: 

By default, any client on your network can mount to your file share. You can configure your NFS file share with administrative controls such as the following:

* Limit access to specific NFS clients or networks by IP address.

* Permit read-only or read-write access.

* Activate user permission squashing.

### SMB file shares: 

You can set the security level for your gateway by doing the following:

* Limiting access for Active Directory users only or providing authenticated guest access to users

* Setting file share visibility for your file share to one of the following:

  * Read-only

  * Read-write

* Controlling file or directory access by Portable Operating System Interface (POSIX) or, for fine grained permissions, using Windows access control lists (ACLs)

  * If you configure guest access authentication, POSIX is used for permissions.

## Access to Amazon S3

The gateway uses an S3 bucket. The content of this bucket can also be accessed by other workflows. You need a way to control access to data in Amazon S3. Here are some ways to control who can access your S3 bucket and your S3 objects:

* Write IAM user policies that specify the users that can access specific buckets and objects.

* Write bucket policies that define access to specific buckets and objects. You can use a bucket policy to grant access across AWS accounts, grant public or anonymous permissions, and allow or block access based on conditions. 

* Use Amazon S3 Block Public Access as a centralized way to limit public access. Block Public Access settings override bucket policies and object permissions. Be sure to activate Block Public Access for all accounts and buckets that you don't want publicly accessible. 

* Restrict access to specific actions by activating S3 Object Lock and setting Guess MIME type and requester pays.
