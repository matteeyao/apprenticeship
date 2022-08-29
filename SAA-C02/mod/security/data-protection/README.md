# Security (Data Tier)

* Data in Transit

  * In and out of AWS

  * Within AWS

* Data at rest

  * Amazon S3

  * Amazon EBS

## Data in Transit

Transferring data in and out of your AWS infrastructure

  * SSL over web

  * VPN for IPsec (for data moving between customer data centers and AWS)

  * IPsec over AWS Direct Connect

  * Import/Export/Snowball

Data sent to the AWS API

  * AWS API calls use HTTPS/SSL by default

## Data at Rest

Data stored in Amazon S3 is private by default, requires AWS credentials for access

* Access over HTTP or HTTPS

* Audit of access to all objects

* Support ACL and policies

  * Buckets

  * Prefixes (directory/folder)

  * Objects

### Data at Rest Encryption Options

* Server-side encryption options

  * Amazon S3-Managed Keys (SSE-S3)

  * KMS-Managed Keys (SSE-KMS)

  * Customer-Provided Keys (SSE-C)

* Client-side encryption options

  * KMS managed master encryption keys (CSE-KMS)

  * Customer managed master encryption keys (CSE-C)

## Managing Your Keys

**Key Management Service (KMS)**

* Customer software-based key management

* Integrated w/ *many* AWS services

* Use directly from application

**AWS CloudHSM**

  * Hardware-based key management

  * Use directly from application

  * FIPS 140-2 compliance
