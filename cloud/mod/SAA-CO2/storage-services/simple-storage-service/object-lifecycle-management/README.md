# Object Lifecycle Management

When using multipart uploads, Amazon S3 retains all the parts on the server until you complete or discontinue the upload. To avoid unnecessary storage costs related to incomplete uploads, make sure to complete or discontinue an upload. Use lifecycle rules to clean up incomplete multipart uploads automatically.

An Amazon S3 Lifecycle configuration is an XML file that consists of a set of rules w/ predefined actions that you want Amazon S3 to perform on objects during their lifetime. As a best practice, we recommend you configure a lifecycle rule using the `AbortIncompleteMultipartUpload` action to minimize your storage costs.

It is recommended that you enable the ability to clean incomplete multipart uploads in the lifecycle settings even if you are not sure whether you are going to perform multipart uploads. Some applications default to multipart uploads when uploading files over a particular application-dependent size and failed or incomplete uploads will result in increased storage costs.

> **Use lifecycle rules to manage your objects**
>
> You can manage an object's li

> **Lifecycle Policies**
>
> * Automates moving your objects between the different storage tiers.
>
> * Can be used in conjunction w/ versioning.
>
> * Can be applied to current versions and previous versions.
