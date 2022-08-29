# S3 Lifecycle Configuration

1. A company has both on-premises data center as well as AWS cloud infrastructure. They store their graphics, audios, videos, and other multimedia assets primarily in their on-premises storage server and use an S3 Standard storage class bucket as a backup. Their data is heavily used for only a week (7 days) but after that period, it will only be infrequently used by their customers. The Solutions Architect is instructed to save storage costs in AWS yet maintain the ability to fetch a subset of their media assets in a matter of minutes for a surprise annual data audit, which will be conducted on their cloud storage.

Which of the following are valid options that the Solutions Architect can implement to meet the above requirement? (Select TWO.)

[x] Set a lifecycle policy in the bucket to transition to S3 - Standard IA after 30 days

[ ] Set a lifecycle policy in the bucket to transition the data to S3 Glacier Deep Archive storage class after one week (7 days)

[x] Set a lifecycle policy in the bucket to transition the data to Glacier after one week (7 days).

[ ] Set a lifecycle policy in the bucket to transition the data to S3-One Zone-Infrequent Access storage class after one week (7 days).

[ ] Set a lifecycle policy in the bucket to transition the data to S3 - Standard IA storage class after one week (7 days).

**Explanation**: You can add rules in a lifecycle configuration to tell Amazon S3 to transition objects to another Amazon S3 storage class. For example: When you know that objects are infrequently accessed, you might transition them to the `STANDARD_IA` storage class. Or transition your data to the `GLACIER` storage class in case you want to archive objects that you don't need to access in real time.

In a lifecycle configuration, you can define rules to transition objects from one storage class to another to save on storage costs. When you don't know the access patterns of your objects or your access patterns are changing over time, you can transition the objects to the `INTELLIGENT_TIERING` storage class for automatic cost savings.

The lifecycle storage class transitions have a constraint when you want to transition from the STANDARD storage classes to either `STANDARD_IA` or `ONEZONE_IA`. The following constraints apply:

* For larger objects, there is a cost benefit for transitioning to `STANDARD_IA` or `ONEZONE_IA`. Amazon S3 does not transition objects that are smaller than 128 KB to the `STANDARD_IA` or `ONEZONE_IA` storage classes because it's not cost effective.

* Objects must be stored **at least 30 days** in the current storage class before you can transition them to `STANDARD_IA` or `ONEZONE_IA`. For example, you cannot create a lifecycle rule to transition objects to the `STANDARD_IA` storage class one day after you create them. Amazon S3 doesn't transition objects within the first 30 days because newer objects are often accessed more frequently or deleted sooner than is suitable for `STANDARD_IA` or `ONEZONE_IA` storage.

* If you are transitioning noncurrent objects (in versioned buckets), you can transition only objects that are at least 30 days noncurrent to `STANDARD_IA` or `ONEZONE_IA` storage.

Since there is a time constraint in transitioning objects in S3, you can only change the storage class of your objects from S3 Standard storage class to `STANDARD_IA` or `ONEZONE_IA` storage after 30 days. This limitation does not apply on `INTELLIGENT_TIERING`, `GLACIER`, and `DEEP_ARCHIVE` storage class.

In addition, the requirement says that the media assets should be fetched in a matter of minutes for a surprise annual data audit. This means that the retrieval will only happen once a year. You can use expedited retrievals in Glacier which will allow you to quickly access your data (within 1–5 minutes) when occasional urgent requests for a subset of archives are required.

Hence, the following are the correct answers:

* Set a lifecycle policy in the bucket to transition the data from Standard storage class to Glacier after one week (7 days).

* Set a lifecycle policy in the bucket to transition to S3 - Standard IA after 30 days.

> **Setting a lifecycle policy in the bucket to transition the data to S3 - Standard IA storage class after one week (7 days)** and **setting a lifecycle policy in the bucket to transition the data to S3 - One Zone-Infrequent Access storage class after one week (7 days)** are both incorrect because there is a constraint in S3 that objects must be stored at least 30 days in the current storage class before you can transition them to STANDARD_IA or ONEZONE_IA. You cannot create a lifecycle rule to transition objects to either STANDARD_IA or ONEZONE_IA storage class 7 days after you create them because you can only do this after the 30-day period has elapsed. Hence, these options are incorrect.

> **Setting a lifecycle policy in the bucket to transition the data to S3 Glacier Deep Archive storage class after one week (7 days)** is incorrect because although DEEP_ARCHIVE storage class provides the most cost-effective storage option, it does not have the ability to do expedited retrievals, unlike Glacier. In the event that the surprise annual data audit happens, it may take several hours before you can retrieve your data.

<br />
