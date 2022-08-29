# Protecting Your Data

> AWS works closely to understand your data protection needs and offers the most comprehensive set of services, tooling, and expertise to help protect your data.

Volume Gateway extends data protection to your volume data using data encryption in transit and at rest, Challenge-Handshake Authentication Protocol (CHAP) authentication, the option for enabling Federal Information Processing Standards (FIPS), numerous protocol options to meet your networking needs, and secure durable storage in Amazon S3.

## Data encryption

Your data is encrypted in transit and in the AWS Cloud. Data that is in the gateway cache is not encrypted.

* All data transfers between any type of gateway appliance and AWS storage is encrypted with Secure Sockets Layer/Transport Layer Security (SSL/TLS).

* All data stored by Storage Gateway in Amazon S3 is encrypted server-side with either of the following: 

  * Amazon S3 server-side encryption (S3-SSE) (default)

  * AWS Key Management Service (AWS KMS)

* EBS snapshots are encrypted at rest using Advanced Encryption Standard (AES-256), a secure symmetric-key encryption standard using 256-bit encryption keys.

## CHAP authentication

AWS Storage Gateway supports authentication between your gateway and iSCSI initiators by using CHAP. CHAP provides protection against man-in-the-middle and playback attacks by periodically verifying the identity of an iSCSI initiator as authenticated to access a storage volume target. In the **Configure CHAP authentication** dialog box, you provide information to configure CHAP for your volumes.

To set up CHAP, you must configure it both on the Storage Gateway console and in the iSCSI initiator software that you use to connect to the target. Storage Gateway uses mutual CHAP, which is when the initiator authenticates the target and the target authenticates the initiator.

Configuring CHAP authentication is recommended to secure the data exchange between host initiator and target.

> [!IMPORTANT]
>
> CHAP configuration is optional but AWS recommends using CHAP for secure exchange between host initiator and target.

## Access to volume data

The Volume Gateway uses an Amazon S3 service bucket rather than an Amazon S3 customer bucket. The content of the service bucket is not accessible from the Amazon S3 console or Amazon S3 application programming interfaces (APIs).

## Data protection best practices

Additional recommendations to secure your data include the following:

* Use multi-factor authentication (MFA) with each account.

* Use Secure Sockets Layer/Transport Layer Security (SSL/TLS) to communicate with AWS resources. TLS 1.2 or later is recommended.

* Set up API and user activity logging with AWS CloudTrail.

* Use AWS encryption solutions, along with all default security controls within AWS services.

* If you require Federal Information Processing Standards (FIPS) 140-2 validated cryptographic modules when accessing AWS through a command line interface or an API, use a FIPS endpoint.
