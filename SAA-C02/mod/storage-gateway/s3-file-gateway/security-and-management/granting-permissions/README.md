# Granting Permissions

Let's consider the security of the Storage Gateway solution. The primary resource of the solution is a gateway. Storage Gateway also supports additional resource types such as file shares. These are referred to as sub-resources, and they don't exist unless they are associated with a gateway.

The solution needs to be deployed, used, and managed. It will need to connect with other resources such as an Amazon Simple Storage Service (Amazon S3) bucket. It will also need to be accessed from your on-premises clients, which use the Network File System (NFS) or Server Message Block (SMB) protocols. As a user, your clients will be using the mounted drive, but the gateway appliance will authenticate to AWS and write to Amazon S3.  

![Fig. 1 AWS Storage Gateway](../../../../../../../img/SAA-CO2/storage-gateway/s3-file-gateway/security-and-management/granting-permissions/diag01.png)

## AWS Identity and Access Management

The AWS Identity and Access Management (IAM) service helps you securely control access to AWS resources. IAM provides the infrastructure necessary to control authentication and authorization to your services. 

![Fig. 2 IAM](../../../../../../../img/SAA-CO2/storage-gateway/s3-file-gateway/security-and-management/granting-permissions/diag02.png)

> IAM includes users, user groups, policies and permissions, and roles.

IAM is used to control who can sign in as an authorized user of an account (authenticated) and what they have permission to do (authorized). You can have valid credentials to authenticate your request, but you also need permissions to perform tasks.

An IAM identity represents a user (a person or an application) that can be authenticated and then authorized to perform actions in AWS. You manage access in AWS by creating policies with specific permissions and attaching them to IAM identities or resources.

### IAM Identities

AWS defines several authentication identities that can be used to provide access to an AWS account.

* **AWS account root user** ▶︎ This is the identity that created the AWS account and thus has unrestricted access to the account. We recommend that you do not use the AWS account root user for everyday tasks, not even administrative ones. Instead, adhere to the best practice of using the root user only to create your first IAM user.

* **IAM user** ▶︎ An IAM user can be a person or a service. An IAM user can sign into AWS using a name and a password. The IAM user can have up to two access keys. Access keys are long-term credentials that can be used to sign programmatic requests to AWS. An IAM user can have specific custom permissions through policies that determine what it can and cannot do within AWS. For example, it might have permissions to create a gateway in Storage Gateway.

* **IAM group** ▶︎ Users can belong to one or more groups. Using groups simplifies the management of permissions. You add permissions to the group, and then IAM users can be added or removed from the group, consequently gaining or losing permissions accordingly.

* **IAM role** ▶︎ An IAM role has specific permissions similar to an IAM user, but instead of being associated with one person, it can be assumed by anyone who needs it. When you assume a role, temporary security credentials are provided for the session. A role needs two things:

  * Permission policies that specify which resources the role can access and what actions are allowed

  * A trust policy that specifies the entities that can assume the role

![Fig. 3 IAM identities](../../../../../../../img/SAA-CO2/storage-gateway/s3-file-gateway/security-and-management/granting-permissions/diag03.png)

### IAM Policies (Permissions)

After the credentials are authenticated, requests need to be authorized. Policies define the permissions for actions.

During authorization, requests to AWS are checked against the relevant IAM policies to determine if the request is allowed or denied. Policies can be attached to a user, group, or a role.

Several policy types exist, such as **identity-based policies** for access to AWS resources within a specific account and **resource-based policies** for cross-account access. The most common examples of resource-based policies are S3 bucket policies and IAM role trust policies.

When granting permissions, always use the principle of least privilege and grant only the permissions required to perform a task.

> The following IAM policy allows all List* and Describe* actions on all resources. These actions are read-only actions. Thus, the policy doesn't allow the user to change the state of any resources—that is, the policy doesn't allow the user to perform actions such as DeleteGateway, ActivateGateway, and ShutdownGateway.

```
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Sid": "AllowReadOnlyAccessToAllGateways",
      "Action": [
        "storagegateway:List*",
        "storagegateway:Describe*"
      ],
      "Effect": "Allow",
      "Resource": "*"
    }
  ]
}
```

Remember when you set up your gateway? During activation of the S3 File Gateway, your gateway was associated with your AWS account. When you configured your Amazon S3 storage settings, you provided the IAM role that is needed to communicate with the S3 bucket.

When you create a file share, Storage Gateway, or more specifically S3 File Gateway, requires access. It requires access to upload or retrieve files from an S3 bucket. 

To grant this access, the S3 File Gateway assumes a role. The role is associated with an IAM policy. The IAM policy grants access and determines which actions the role can perform.

The S3 bucket must also have a policy that allows the IAM role to access the S3 bucket.

For examples of a trust policy and a permissions policy for the IAM role, choose the appropriate tab.

### Example: IAM Role Trust Policy

The following example is an IAM role trust policy that helps your file gateway to assume an IAM role.

```
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Sid": "",
      "Effect": "Allow",
      "Principal": {
        "Service": "storagegateway.amazonaws.com"
      },
      "Action": "sts:AssumeRole"
    }
  ]
}
```

### Example: Permissions Policy

The following is an example of an IAM role permissions policy that allows the role to perform all the Amazon S3 actions listed in the policy. The first part of the statement allows all the actions listed to be performed on the S3 bucket named TestBucket. The second part allows the listed actions on all objects in TestBucket.

```
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Action": [
        "s3:GetAccelerateConfiguration",
        "s3:GetBucketLocation",
        "s3:GetBucketVersioning",
        "s3:ListBucket",
        "s3:ListBucketVersions",
        "s3:ListBucketMultipartUploads",
      ],
      "Resource": "arn:aws:s3:::TestBucket",
      "Effect": "Allow"
    },
    {
      "Action": [
        "s3:AbortMultipartUpload",
        "s3:DeleteObject",
        "s3:DeleteObjectVersion",
        "s3:GetObject",
        "s3:GetObjectAc1",
        "s3:GetObjectVersion",
        "s3:ListMultipartUploadParts",
        "s3:PutObject",
        "s3:PutObjectAc1",
      ],
      "Resource": "arn:aws:s3:::TestBucket/*",
      "Effect": "Allow"
    },
  ]
}
```

## Actions

The Storage Gateway defines a set of actions that can be used to grant permission for specific API operations. The table shows a sample of Storage Gateway application programming interface (API) operations, required permissions, and resources in the action element of an IAM policy statement. For example, if you create an IAM user in your AWS account, you can grant them permissions to activate a gateway by granting them the ActivateGateway action. 

<table style="width:100%;"><thead><tr><th style="width:25.0565%;"><span style="font-size:17px;">API Operation<br></span></th><th style="width:43.0969%;"><span style="font-size:17px;">Required Permissions<br>(API Actions)<br></span></th><th style="width:31.7483%;"><span style="font-size:17px;">Resources<br></span></th></tr></thead><tbody><tr><td style="width:25.0565%;"><span style="font-size:17px;">ActivateGateway<br></span></td><td style="width:43.0969%;"><span style="font-size:17px;">storagegateway:ActivateGateway<br></span></td><td style="width:31.7483%;"><span style="font-size:17px;">For any gateway previously deployed on your host.<br></span></td></tr><tr><td style="width:25.0565%;"><span style="font-size:17px;">AddCache<br></span></td><td style="width:43.0969%;"><span style="font-size:17px;">storagegateway:AddCache<br></span></td><td style="width:31.7483%;"><span style="font-size:17px;">Gateway<br></span></td></tr><tr><td style="width:25.0565%;"><span style="font-size:17px;">CreateNFSFileShare<br></span></td><td style="width:43.0969%;"><span style="font-size:17px;">storagegateway:CreateNFSFileShare<br></span></td><td style="width:31.7483%;"><span style="font-size:17px;">Gateway<br></span></td></tr><tr><td style="width:25.0565%;"><span style="font-size:17px;">ListFileShares<br></span></td><td style="width:43.0969%;"><span style="font-size:17px;">storagegateway:ListFileShares<br></span></td><td style="width:31.7483%;"><span style="font-size:17px;">Gateway<br></span></td></tr><tr><td style="width:25.0565%;"><span style="font-size:17px;">RefreshCache<br></span></td><td style="width:43.0969%;"><span style="font-size:17px;">storagegateway:RefreshCache<br></span></td><td style="width:31.7483%;"><span style="font-size:17px;">File share</span><br></td></tr></tbody></table>
