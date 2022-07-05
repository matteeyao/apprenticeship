# Bucket

## Bucket Overview

Buckets are permanent containers that hold objects. You can create between 1 and 100 buckets in each AWS account. You can increase the bucket limit to a maximum of 1,000 buckets by submitting a service limit increase. Bucket sizes are virtually unlimited so you don't have to allocate a predetermined bucket size the way you would when creating a storage volume or partition.

An Amazon S3 bucket is a versatile storage option w/ the ability to: host a static web site, retain version information on objects, and employ life-cycle management policies to balance version retention w/ bucket size and cost.

## Bucket Limitations

> **Bucket owner**
>
> * Amazon S3 buckets are owned by the account that creates them and cannot be transferred to other accounts

> **Bucket names**
>
> * Bucket names are globally unique. There can be no duplicate names within the entire S3 infrastructure.
>
> * Use a dot `.` in the name only if the bucket's intended purpose is to host an Amazon S3 static website; otherwise do not use a dot `.` in the bucket name

> **Bucket renaming**
>
> * Once created, you cannot change a bucket name.

> **Permanent entities**
>
> * Buckets are permanent storage entities and only removable when they are empty. After deleting a bucket, the name becomes available for reuse by any account after 24 hours if not taken by another account.

> **Object storage limits**
>
> * There's no limit to the number of objects you can store in a bucket. You can store all of your objects in a single bucket, or organize them across several buckets. However, you can't create a bucket from another bucket, also known as nesting buckets.

> **Bucket creation limits**
>
> * By default, you can create up to 100 buckets in each of your AWS accounts. If you need additional buckets, you can increase your account bucket limit to a maximum of 1,000 buckets by submitting a service limit increase.
