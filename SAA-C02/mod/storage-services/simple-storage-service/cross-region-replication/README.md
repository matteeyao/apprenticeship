# Region location

Amazon S3 is a globally viewable service. This means that in the AWS Management Console you do not have to specify a region in order to view the buckets. Remember that when you initially create the bucket, you must choose a region to indicate where you want the bucket data to reside. The region you choose should be local to your users or consumers to optimize latency, minimize costs, or to address regulatory requirements.

For example, if you reside in Europe, you will want to create buckets in the Europe (Ireland) or Europe (Frankfurt) regions rather than creating your buckets in Asia Pacific (Sydney) or South America (Sao Paulo). This way the data is closer to your users and consumers, reducing latency and ensuring regulatory and legal requirements are met.

## Cross region replication

A way of replicating objects across regions (in the name), but you can also replicate them within the same region.

* Versioning must be enabled on both the source and destination buckets.

* Files in an existing bucket are not replicated automatically.

* All subsequent updated files will be replicated automatically.

* Delete markers are not replicated.

    * So if you delete an object in one bucket, it's not going to be deleted in the other.

* Deleting individual versions or delete markers will not be replicated.

* Understand what Cross Region Replication is at a high level.

> ### Cross-Region Replication (CRR)
>
> If you need data stored in multiple regions, you can replicate your bucket to other regions using cross-region replication. This enables you to automatically copy objects from a bucket in one region to different bucket in another, separate region. You can replicate the entire bucket or you can use tags to replicate only the objects w/ the tags you choose.

> ### Same-Region Replication (SRR)
>
> Amazon S3 supports automatic and asynchronous replication of newly uploaded S3 objects to a destination bucket in the same AWS Region.
>
> SRR makes another copy of S3 objects within the same AWS Region, w/ the same redundancy as the destination storage class. This allows you to automatically aggregate logs from different S3 buckets for in-region processing, or configure live replication between test and development environments. SRR helps you address data sovereignty and compliance requirements by keeping a copy of your objects in the same AWS Region as the original.
