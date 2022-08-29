# Amazon S3 Data Management

Amazon S3 gives you robust capabilities to manage access to your data. In this module, the focus is on how to get data into and out of Amazon S3 from both the AWS Management Console and the CLI. It is important to become familiar w/ using both tools due to file upload limitations in the AWS Management Console.

The first topic to explore is the new Amazon S3 data consistency model used when adding or modifying data to an S3 bucket.

## S3 Strong data consistency model

Amazon S3 now delivers strong read-after-write consistency for any storage request, w/o changes to performance or availability, w/o sacrificing regional isolation for applications, and at no additional cost. Any request for S3 storage is now strongly consistent.

After a successful write of a new object or overwrite of an existing object, any subsequent read request immediately receives the latest version of the object. Amazon S3 also provides strong consistency for list operations, so after a write, you can immediately perform a listing of the objects in a bucket w/ any changes reflected.

Why is this important? Previously, the Amazon S3 consistency model was strongly consistent for new objects and eventually consistent for modified or recently queried objects. However, w/ the increased usage of high process analytics engines, applications and users need to have access to update data immediately after written. The strong read-after-write consistency model addresses the needs of large data lakes and applications that require immediate access to changed data.

There is no charge for this feature and it is available for all GET, PUT, LIST, HEAD requests, as well as Access Control Lists, object tags, and other metadata. For bucket operations such as reading a bucket policy or metadata, the consistency model remains eventually consistent.

## Versioning

Versioning is a means of keeping multiple variants of an object in the same bucket. You can use versioning to preserve, retrieve, and restore every version of every object stored in your Amazon S3 bucket. W/ versioning, you can easily recover from both unintended user actions and application failures. If Amazon S3 receives multiple write requests for the same object simultaneously, and you enable versioning, it will store all of the object write requests.

If you enable versioning for a bucket, Amazon S3 automatically generates a unique version ID for the object being stored. In one bucket, for example, you can have two objects w/ the same key, but different version IDs, such as `dolphins.jpg` (version 111111) and `dolphins.jpg` (version 2222222).

Versioning-enabled buckets enable you to recover objects from accidental deletion or overwrite. For example:

* If you delete an object, instead of removing it permanently, Amazon S3 inserts a delete marker, which becomes the current object version. You can always restore the previous version.

* If you overwrite an object, it results in a new object version in the bucket. You can always restore the previous version.

## Examples of High-level commands

The following example uses the aws s3 command to make a bucket called `demo-oceanlife`, then the example shows how to copy a file into the bucket, then list the Amazon S3 buckets available, and finally list the individual objects within the buckets.

> ### Make a bucket
>
> Below, we use the `aws s3 mb` or make bucket command to make a bucket called `demo-oceanlife`.
>
> The output, listed on the second line, `make_bucket: demo-oceanlife` indicates a successful bucket creation.

```zsh
> aws s3 mb s3://demo-oceanlife
make_bucket: demo-oceanlife
```

> ### Copy files to a bucket
>
> In the command below, we use the `aws s3 cp` command to copy the `whale.jpg` file from the client local machine in `c:\sourcefiles` to the `demo-onceanlife` bucket.
>
> The output on the second line, `upload:..\..\source-files\whale.jpg to s3://demo-oceanlife/whale.jpg` appears while the file copies.

```zsh
> aws s3 cp c:\sourcefiles\whale.jpg s3://demo-oceanlife

upload: ..\..\source-files\whale.jpg to s3://demo-oceanlife/whale.jpg
```

> ### Viewing the list of Amazon S3 buckets
>
> In order to list all of the buckets in your account, you use the `aws s3 ls` command.
>
> The output on the second line lists the one bucket you created in the first step called `demo-oceanlife`. If you had more buckets in your account, you would see all of the buckets listed here.

```zsh
> aws s3 ls

2020-08-19 17:34:31 demo-oceanlife
```

> ### Viewing objects in the bucket
>
> Additionally, to view all objects in a specific bucket, in this case the `demo-oceanlife` bucket, you can use the `aws s3 ls <BUCKET NAME>` to see all of the files in the bucket.
>
> The output on the second line shows the one file, `whale.jpg`, that was uploaded.

```zsh
> aws s3 ls s3://demo-oceanlife

2020-08-19 17:35:58 31228 whale.jpg
```

## Low level commands

Low-level commands use the s3api command-set and provide direct access to the Amazon S3 APIs, enabling operations not exposed in the high-level s3 commands. Most of the s3api commands are generated from JSON models that directly imitate the APIs of other AWS services that provide API-level access.

The **s3api list-objects** and **s3api make-bucket** commands share a similar operation name, input, and output as the corresponding operation in the Amazon S3 API. As a result, these commands allow a significantly more granular amount of control over your buckets when using the CLI.

## PUT operations

Use the `PUT` request operation to add an object to a bucket. You must have WRITE permissions on a bucket in order to add an object. Amazon S3 never adds partial objects; if you receive a success response, you can be confident that the entire object was stored durably.

If the object already exists in the bucket, the new object overwrites the existing object. Amazon S3 orders all of the requests that it receives but it is possible that if you send two requests nearly simultaneously, the received requests will be in a different order than sent.

The last request received is the one which is stored. This means that if multiple parties are simultaneously writing to the same object, they may all get a success response even though only the last write wins. This is b/c Amazon S3 is a distributed system and it may take a few moments for one part of the system to communicate that another part has received an object update.

> ### Multipart Upload API
>
> Multipart uploads break a large object into smaller parts to improve throughput.
>
> You can upload or copy objects of up to 5 GB in a single PUT operation. For objects over 5 TB, you must use the multipart upload API. The multipart upload API allows you to upload a single object as a set of parts. Each part is a contiguous portion of the object's data. You can upload these object parts independently and in any order.

If transmission of any part fails, you can re-transmit just that part w/o having to re-transmit all the parts. After all parts of your object upload, Amazon S3 assembles these pieces and creates the object. In general, when your object size reaches 100 MB, you should consider using multipart uploads instead of uploading the object in a single operation.

Using multipart upload provides the following advantages:

* Improved throughput - You can upload parts in parallel to improve throughput.

* Quick recovery from any network issues - Smaller part size minimizes the impact of restarting a failed upload due to a network error.

* Pause and resume object uploads - You can upload object parts over time. Once you initiate a multipart upload there is no expiry; you must explicitly complete or abort the multipart upload.

* Begin and upload before you know the final object size - You can upload an object as you are creating it.

## GET operations

> GET operations allow retrieval of whole or individual parts of an object.

You can use the GET operation to retrieve a whole object or parts of an object directly from Amazon S3.

If you need to retrieve the object in parts, use the Range HTTP header in a GET request. Doing this allows you to retrieve a specific range of bytes from an object stored in Amazon S3. You can then resume fetching other parts of the object whenever you or your application is ready. This resumable download is useful if you only need portions of your object data, in cases where network connectivity is poor, or if your application must process only subsets of object data.

## DELETE operations

W/ a delete operation, you can delete either a single object or multiple objects in a single delete request. Multiple outcomes are possible when you issue a DELETE request, depending on whether you enable versioning or disable it on your bucket.

> ### Versioning not enabled
>
> When versioning is disabled objects are deleted and not recoverable.
>
> If a bucket is not versioning enabled, you can permanently delete an object by specifying the key name of an object. The DELETE request will permanently remove the object from the bucket, making it unrecoverable.

> ### Permanent deletes
>
> If a bucket is versioning-enabled, you can either permanently delete an object or have Amazon S3 create a delete marker for the object, which allows the object to be recoverable.
>
> You can permanently delete individual versions of an object by invoking a DELETE request w/ the object's key and version ID. To completely remove the object from your bucket, you must delete each individual version.

> ### Recoverable deletes
>
> If your DELETE request specifies only the object's key name, Amazon S3 inserts a delete marker that becomes the current version of the object. If you try to retrieve an object that has a delete marker, Amazon S3 returns a 404 NOT FOUND error. You can recover the object by removing the delete marker from the current version of the object, making the object available for retrieval.
