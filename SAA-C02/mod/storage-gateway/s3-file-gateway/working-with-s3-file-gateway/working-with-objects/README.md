# Working with Objects

An S3 File Gateway simplifies how on-premises clients can store files as durable objects in Amazon S3 cloud storage. Depending on your workflow needs, you can configure and adjust Amazon S3 to better support you. You might also need to carefully consider some file operations that affect S3 objects and cause storage class implications.

>[!IMPORTANT]
>
> Monitor your S3 buckets so that you can adjust them to fit your needs. You might consider adjusting versioning, adding lifecycle rules, and configuring other settings.

## S3 objects

Amazon S3 is an object storage service that offers scalability, data availability, security, and performance. Data is stored as objects within resources called **buckets**. An object is a file and any metadata that describes the file. You can store any amount of data, and a single object can be up to 5 TB in size.

Buckets can be version enabled, which helps you recover objects from accidental deletions or overwrites. In a version-enabled bucket, every time an object changes, a new variant of the object is created. And only the owner of the bucket can permanently delete a version of an object. When others delete an object, the object is marked with a delete marker, but in reality, all versions remain in the bucket.

Amazon S3 offers a range of purpose-built storage classes. Based on your data access, resilience, and cost requirements, objects can be moved to the most optimal storage class. You can, for example, set up object lifecycle rules to transition and expire objects based on a particular bucket, prefix or object tag, object size, and even object version. Ingest or transition costs might apply when moving objects into any storage class.

You can see how turning on versioning on an S3 bucket or setting up lifecycle rules will have an impact on your overall data control and costs. And there are more settings that you should consider, such as S3 Same-Region Replication (SRR) or S3 Cross-Region Replication (CRR). Understanding your workflows and knowing what you need from your data can help you more effectively use cloud storage.

## File operations and S3 objects

When using **S3 File Gateway**, operations that modify a shared file will affect the S3 object that corresponds to that file. Because of this, you need to carefully consider how you configure your S3 storage.

As you know, when a new file is written to S3 File Gateway, the file is converted to an S3 object and uploaded to Amazon S3. When a file is written to the file gateway by an NFS or SMB client, the file gateway uploads the file's data to Amazon S3 followed by its metadata (ownerships, timestamps, and so on). Uploading the file data creates an S3 object, and uploading the metadata for the file updates the metadata for the S3 object. This process creates another version of the object, resulting in two versions. **If versioning is enabled for the S3 bucket, both versions of the object will be stored**.

Similarly, if a file is modified after it has been uploaded, the modification will also result in multiple versions of the S3 object. S3 File Gateway uploads the new or modified data instead of uploading the whole file and also uploads its metadata.

Some common file operations will change the file metadata and result in deleting the S3 object (or marking it deleted in version-enabled buckets) and creating a new object. For example, renaming a file will replace the existing object and create a new S3 object. Depending on the S3 storage class being used, early deletion fees and retrieval fees might apply.

## Learning summary

You learned that you need to configure and adjust Amazon S3 cloud storage to effectively support your workflows. Next, you will learn about mechanisms for monitoring and alerting on the health and performance of Storage Gateway and its file shares.
