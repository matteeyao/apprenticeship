# Managing the Gateway

Managing your gateway includes tasks such as adding or deleting a file share, configuring cache storage, refreshing the file share, and doing general maintenance. 

## Adding a file share

When you create and activate the S3 File Gateway, it does not contain files shares. You can add one or more file shares. Each gateway is limited to 10 shares.

Each file share needs to be connected to an S3 bucket and given access to the bucket through an IAM role with a trust policy. A role’s trust policy describes who or which service is permitted to assume that role. This way the file share can be trusted and can assume a role with permissions to the bucket as defined by the policy assigned to the IAM role.

> The following screenshots show an IAM role with overly permissive permissions policies. The role can perform all Amazon S3 actions, all gateway actions, and some Amazon Elastic Compute Cloud (Amazon EC2) actions on all resources. In addition, a trust policy permits the file gateway to assume an IAM role.

```
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Principal": {
        "Service": [
          "ec2.amazonaws.com",
          "storagegateway.amazonaws.com"
        ]
      },
      "Action": "sts:AssumeRole"
    }
  ]
}
```

## File share health

Each file share has an associated status that reflects its health. A file share should have AVAILABLE status all or most of the time that it is in use. 

<table style="width:100%;"><thead><tr><th><span style="font-size:20px;">Status</span><br></th><th><span style="font-size:20px;">Meaning</span><br></th></tr></thead><tbody><tr><td style="width:19.6915%;"><span style="font-size:17px;">AVAILABLE</span><br></td><td style="width:80.1618%;"><span style="font-size:17px;">The file share is configured properly and is available to use. The AVAILABLE status is the normal running status for a file share.</span><br></td></tr><tr><td style="width:19.6915%;"><span style="font-size:17px;">CREATING</span><br></td><td style="width:80.1618%;"><span style="font-size:17px;">The file share is being created and is not ready for use.</span><br></td></tr><tr><td style="width:19.6915%;"><span style="font-size:17px;">UPDATING</span><br></td><td style="width:80.1618%;"><span style="font-size:17px;">The file share configuration is being updated.<br></span></td></tr><tr><td style="width:19.6915%;"><span style="font-size:17px;">DELETING</span><br></td><td style="width:80.1618%;"><span style="font-size:17px;">The file share is being deleted. The file share is not deleted until all data is uploaded to AWS.</span><br></td></tr><tr><td style="width:19.6915%;"><span style="font-size:17px;">FORCE_DELETING</span><br></td><td style="width:80.1618%;"><span style="font-size:17px;">The file share is being deleted forcibly. The file share is deleted immediately, and uploading to AWS ceases.</span><br></td></tr><tr><td style="width:19.6915%;"><span style="font-size:17px;">UNAVAILABLE</span><br></td><td style="width:80.1618%;"><span style="font-size:17px;">The file share is in an unhealthy state.</span><br></td></tr></tbody></table>

An UNAVAILABLE file share state can be caused by role policy errors or a problem reaching the Amazon S3 endpoint, a restriction on a specific IP address, or many other issues. You will need to troubleshoot why the file share is in an unhealthy state and take appropriate action.

## File share actions

After creating a file share, you can perform additional actions.

### Edit file share settings

When you create a new file share, there are some settings that you will not be able to change, such as the bucket or access point or the virtual private cloud (VPC) endpoint settings. You can configure those settings only when creating a new file share.

You can, however, change other settings of the file share configuration after the file share has been created. For example, you can edit the storage class for your S3 bucket, edit the file share name, export as read-write or read-only, automatically refresh cache settings, and more. 

What you can change is specific to SMB and NFS. To learn more about editing file share settings, see the following:

### Refresh cache

To refresh the cached inventory of objects for the specified file share, you can use the Storage Gateway console or the refresh cache operation in the Storage Gateway API. You can also modify the file share settings to automatically refresh cache from Amazon S3 at specific intervals.

The refresh cache operation finds objects in the S3 bucket that have been added, removed, or replaced since the gateway last listed the bucket contents and cached results. Amazon S3 request pricing applies.

### File upload notification

You can configure your gateway to notify you when a file has been fully uploaded to Amazon S3 by the file gateway. Storage Gateway can invoke Amazon EventBridge when your file operations are completed. This in turn, can send an event to a target such as Amazon Simple Notification Service (Amazon SNS).

The file upload notification is different than the Amazon S3 event notification. The file upload notification provides a notification for each individual file that is uploaded to Amazon S3 through S3 File Gateway. Amazon S3 event notifications provide notifications that include partial file uploads. Therefore, there is no way to tell from the Amazon S3 event notification that the file upload has been completed.

## Multi-writer best practices

When multiple gateways or file shares write to the same S3 bucket, unpredictable results might occur. You can prevent this in two ways:

1. Configure your S3 bucket so that only one file share can write to it. You can create an S3 bucket policy that denies all roles, except the role used for the specific file share, to put or delete objects in the bucket and attach this policy to the S3 bucket. The best practice for secondary gateways is to either use an IAM role that is prevented from writing to the bucket or permit the export of file shares as read-only. These measures can prevent accidental writes to the bucket from the secondary clients at the Amazon S3 level and at Storage Gateway level.

![Fig. 1 Multi-writer](img/SAA-CO2/storage-gateway/s3-file-gateway/security-and-management/managing-the-gateway/diag01.png)

> Configure your S3 bucket so that only one file share can write to it. You can have multiple readers of the bucket, regardless of whether it is an S3 directory or another gateway. 

2. If you want to write to the same Amazon S3 bucket from multiple file shares, you must prevent the file shares from trying to write to the same objects simultaneously. To do this, you configure a separate, unique object prefix for each file share. This means that each file share will only write to objects with its corresponding prefix. It will not write to objects associated with the other file shares in your deployment. You configure the object prefix in the S3 prefix name field when you create a new file share.
