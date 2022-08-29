# Querying an S3 Bucket

1. An application needs to retrieve a subset of data from a large CSV file stored in an Amazon S3 bucket by using simple SQL expressions. The queries are made within Amazon S3 and must only return the needed data. 

Which of the following actions should be taken?

[ ] Perform an S3 Select operation based on the bucket's name.

[x] Perform an S3 Select operation based on the bucket's name and object's key.

[ ] Perform an S3 Select operation based on the bucket's name and object's metadata.

[ ] Perform an S3 Select operation based on the bucket's name and object tags.

**Explanation**: **S3 Select** enables applications to retrieve only a subset of data from an object by using simple SQL expressions. By using S3 Select to retrieve only the data needed by your application, you can achieve drastic performance increases.

Amazon S3 is composed of buckets, object keys, object metadata, object tags, and many other components as shown below:

* An Amazon S3 bucket name is globally unique, and the namespace is shared by all AWS accounts.

* An Amazon S3 object key refers to the key name, which uniquely identifies the object in the bucket.

* An Amazon S3 object metadata is a name-value pair that provides information about the object.

* An Amazon S3 object tag is a key-pair value used for object tagging to categorize storage.

You can perform S3 Select to query only the necessary data inside the CSV files based on the bucket's name and the object's key.

The following snippet below shows how it is done using boto3 ( AWS SDK for Python ):

```py
client = boto3.client('s3')
resp = client.select_object_content(
Bucket='tdojo-bucket', # Bucket Name.
Key='s3-select/tutorialsdojofile.csv', # Object Key.
ExpressionType= 'SQL',
Expression = "select \"Sample\" from s3object s where s.\"tutorialsdojofile\" in ['A', 'B']"
```

Hence, the correct answer is the option that says: **Perform an S3 Select operation based on the bucket's name and object's key**.

> The option that says: **Perform an S3 Select operation based on the bucket's name and object's metadata** is incorrect because metadata is not needed when querying subsets of data in an object using S3 Select.

> The option that says: **Perform an S3 Select operation based on the bucket's name and object tags** is incorrect because object tags just provide additional information to your object. This is not needed when querying with S3 Select although this can be useful for S3 Batch Operations. You can categorize objects based on tag values to provide S3 Batch Operations with a list of objects to operate on.

> The option that says: **Perform an S3 Select operation based on the bucket's name** is incorrect because you need both the bucket’s name and the object key to successfully perform an S3 Select operation.

<br />
