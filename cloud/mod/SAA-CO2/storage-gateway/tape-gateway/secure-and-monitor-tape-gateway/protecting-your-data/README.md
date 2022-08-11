# Protecting Your Data

> AWS works closely to understand your data protection needs and offers the most comprehensive set of services, tooling, and expertise to help protect your data.

Tape Gateway extends data protection to your virtual tapes using data encryption in transit and at rest, the option for enabling Federal Information Processing Standards (FIPS), numerous protocol options to meet your networking needs, and secure durable storage in Amazon S3.

## Data encryption

Your data is encrypted in transit and in the AWS Cloud. Data that is in the gateway cache is not encrypted.

* All data transfers between any type of gateway appliance and AWS storage is encrypted with Secure Sockets Layer/Transport Layer Security (SSL/TLS).

* All data stored by Storage Gateway in Amazon S3 is encrypted server-side with either of the following: 

  * Amazon S3 server-side encryption (S3-SSE) (default)

  * AWS Key Management Service (AWS KMS)

* You can also encrypt virtual tapes using your own keys if you want.

  * This option is available through the application programming interface (API) and command line interface (CLI) only.

## Access to tape data

The Tape Gateway uses an Amazon S3 service bucket rather than an Amazon S3 customer bucket. The content of the service bucket is not accessible from the Amazon S3 console or Amazon S3 APIs.

## Additional tape security

For added security, Tape Gateway provides write once, read many (WORM) enabled virtual tapes and tape retention lock features.

> ### WORM-enabled virtual tapes
>
> WORM-enabled virtual tapes help ensure that the data on active tapes in your virtual tape library (VTL) cannot be overwritten or erased. WORM configuration can only be set when tapes are created and that configuration cannot be changed after the tapes are created.

> ### Tape retention lock
>
> You can set up tape retention lock by creating a custom tape pool. Then apply the new custom pool to a tape by assigning the tape to a pool.
>
> With tape retention lock, you can specify the retention mode and period on archived virtual tapes, preventing them from being deleted for a fixed amount of time up to 100 years. It includes permission controls on who can delete tapes or modify the retention settings.
>
> You can configure tape retention lock in one of two modes:
>
> * **Governance mode** ▶︎ Only IAM users with the permissions to perform `storagegateway:BypassGovernanceRetention` can remove tapes from the pool.
>
> * **Compliance mode** ▶︎ In compliance mode, the protection cannot be removed by any user, including the root AWS account.
>
> When a tape is locked in compliance mode, its retention lock type can't be changed and its retention period can't be shortened. The compliance mode lock type helps ensure that a tape can't be overwritten or deleted for the duration of the retention period.

## Data protection best practices

Additional recommendations to secure your data include the following:

* Use multi-factor authentication (MFA) with each account.

* Use Secure Sockets Layer/Transport Layer Security (SSL/TLS) to communicate with AWS resources. TLS 1.2 or later is recommended.

* Set up API and user activity logging with AWS CloudTrail.

* Use AWS encryption solutions, along with all default security controls within AWS services.

* If you require Federal Information Processing Standards (FIPS) 140-2 validated cryptographic modules when accessing AWS through a command line interface or an API, use a FIPS endpoint.
