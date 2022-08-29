# Organizing data using tags

A tag is a label that you assign to an AWS resource. Each tag consists of a key and an optional value, both of which you define to suit your company's requirements. Tags enable you to categorize your AWS resources or data in different ways.

For example, you could define a set of tags for your objects that help you track project data or owner. Amazon S3 tags are key-value pairs and apply to a whole bucket or to individual objects to help w/ identification, searches, and data classification. Using tags for your objects allows you to effectively manage your storage and provide valuable insight on how your data is used. Newly created tags assigned to a bucket, are not retroactively applied to existing child objects.

You can use two types of tags: Bucket tags and Object tags.

> ### Bucket tags
>
> Bucket tags allow you to track storage costs, or other criteria, by labeling your Amazon S3 buckets using cost allocation tags. A cost allocation tag is a key-value pair that you associate w/ an S3 bucket. After you activate cost allocation tags, AWS uses the tags to organize your resource costs on your cost allocation report. You can only use cost allocation tags on buckets and not on individual objects.
>
> AWS provides two types of cost allocation tags, an AWS-generated tag and user-defined tag. AWS defines, creates, and applies the AWS-generated tag, `createdBy`, for you after an S3 `CreateBucket` event. You define, create, and apply user-defined tags to your S3 bucket. Once you have created and applied the user-defined tags, you can activate them by using the Billing and Cost Management console for cost allocation tracking. Cost Allocation Tags appear on the console after enabling AWS Cost Explorer, AWS Budgets, AWS Cost and Usage reports, or legacy reports.
>
> After you activate the AWS services, they appear on your cost allocation report. You can then use the tags on your cost allocation report to track your AWS costs.

> ### Bucket tag set
>
> Each S3 bucket has a tag set. A tag set contains all of the tags that are assigned to that bucket and can contain as many as 50 tags, or it can be empty.
>
> Keys must be unique within a tag set but values don't. However, when attempting to add a second Key called "items" an error occurs b/c the key must be unique within the tag set.

> ### Object tags
>
> Object tagging gives you a way to categorize and query your storage. You can add tags to an Amazon S3 object during the upload or after the upload. Each tag is a key-value pair that adheres to the following rules:
>
> * You can associate up to 10 tags w/ an object; they must have unique tag keys.
>
> * Tag keys can be up to 128 characters in length
>
> * Tag values can be up to 255 characters in length
>
> * Key and tag values are case sensitive

> ### Additional benefits
>
> Adding tags to your objects offer benefits such as the following:
>
> * Object tags enable fine-grained access control of permissions. For example, you could grant an IAM user permission to read-only objects w/ specific tags.
>
> * Object tags enable fine-grained object lifecycle management in which you can specify a tag-based filter, in addition to a key name prefix, in a lifecycle rule.
>
> * When using Amazon S3 analytics, you can configure filters to group objects together for analysis by object tags, key name prefix, or both prefix and tags.
>
> * You can also customize Amazon CloudWatch metrics to display information by specific tag filters.

> ### Object API operations for tagging
>
> W/ Amazon S3 tagging, if you want to add or replace a tag in a tag set (all the tags associated w/ an object or bucket), you mist downloads all the tags, modify the tags, and then replace all the tags at once.
