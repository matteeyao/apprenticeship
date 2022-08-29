# Sharing S3 Buckets across accounts

## S3 cross-account access

### 3 different ways to share S3 buckets across accounts

* Using Bucket Policies + IAM (applies across the entire bucket). Programmatic Access Only.

    * Applies across the entire bucket and you can't lock down individual objects

* Using Bucket ACLs (Access Control Lists) + IAM (individual objects). Programmatic Access Only.

    * ACLs go down into the individual objects

* Cross-account IAM Roles. Programmatic AND Console access.

> [!NOTE]
> A roles is a temporary method of granting access to an AWS resource either from another AWS service such as EC2 or by other AWS accounts.

We are going to create an IAM role that will allow us to access an S3 bucket in another AWS account.
