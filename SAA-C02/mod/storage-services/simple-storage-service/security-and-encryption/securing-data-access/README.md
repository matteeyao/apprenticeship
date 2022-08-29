# Securing Data Access

As w/ all of the AWS services, security exists in all layers of Amazon S3. By default, all Amazon S3 resources are private and accessible by the resource owner or account administrator. By implementing a strict security stance, Amazon S3 allows you to work backwards, configure, and finely-tune your access policies. This helps to align your organizational, governance, security, and compliance requirements. One of AWS Security best practices is to design your environment based on the principle of least privilege.

## Principle of least privilege

Least privilege is a security design strategy where granted permissions allow only the minimum necessary rights required to accomplish the task. W/ least privilege, you start w/ a strict set of minimum permissions and grant additional permissions only when necessary. Starting w/ tight restrictions and adding new ones when required is more secure. Starting w/ open permissions too lenient and then trying to tighten them later is less secure.

When working a/ Amazon S3, identify what each user, role, and application needs to accomplish within your buckets and then create policies that allow them to perform only those specific tasks. When granting permissions, you decide who gets permissions and into which Amazon S3 resources. You enable specific actions that you want to allow on those resources. Therefore, you should grant only the permissions that are required to perform a task. Implementing least privilege access is fundamental in reducing security vulnerabilities.

## Security mechanisms

A newly created bucket can only be accessed by the user who created it or by the account owner. You must grant access to other users by using one or a combination of the following access management features.

> ### AWS Identity and Access Management
>
> **IAM** is used to create users and manage their respective access to resources, including buckets and objects.

> ### Bucket policies
>
> **Bucket policies** are used to configure permissions for all or a subnet of objects using tags and prefixes.

> ### Pre-Signed URLs
>
> **Pre-Signed URLs** are used to grant time-limited access to others w/ temporary URLs.

> ### Access control lists
>
> **Access Control List (ACLs)** permit individual objects to be accessible to authorized users.

> [!NOTE]
> Amazon S3 ACLs are a legacy access control mechanism that predates IAM. AWS recommends using Amazon S3 bucket policies or IAM policies for access control.

## Block public access

AWS introduced the **S3 Block Public Access** feature to help you avoid inadvertent data exposure. W/ **Block Public Access**, you can manage public access of your Amazon S3 resources at both the AWS account level and the bucket level, which helps ensure that your data is not publicly available. Any new bucket created created has block all public access enabled by default.

What does this mean for you? If you want to grant public access on any resources managed by **Block Public Access**, you will have to adjust your **Block Public Access** configuration.

## Amazon S3 Block Public Access Settings

Review each of the available **Block Public Access (BPA)** options, which are either enabled at the account level or the bucket level. If enabled at the account level, the BPA settings override any settings on the individual buckets.

> ### Block all public access
>
> Sometimes, you want to make sure that a bucket will never allow public access. By enabling this one click option, you can prevent public access to your bucket. This overrides any configured ACLs and bucket policies that would normally grant public access. Choosing to enable this option equates to enabling all of the other options listed here.
>
> Any new bucket you create will have this option enabled by default. You need to disable this option if you want to allow public access to your bucket or objects.

> ### Block public access granted through new ACLs
>
> This option prevents you from creating any new ACL, either for a bucket or object, which grants public access permissions. This option only affects the creation of new public ACLs; it does not alter any existing ACLs or policies. Any existing ACLs or policies granting public access will not affect permissions and public access to those resources will remain intact.
>
> After you enable this option, you should then review your ACLs to evaluate any existing public access permissions and assess whether or not those permissions should stay the same.
>
> Remember, w/ this option enabled, if you have any bucket policies or existing ACLs granting public access to buckets and objects, those resources will remain publicly accessible. If you wish to block all public access to buckets and objects, choose the block all public access option.

> ### Block public access granted through any ACLs
>
> This option only affects how you evaluate ACL public permissions. When you enable this option, it ignores any existing ACLs that grant public permission on buckets and objects. This does not alter the existing ACLs themselves, but any resources configured w/ existing public ACLs will no longer be publicly accessible.
>
> It can be confusing b/c it does not prevent you from creating new ACLs that would normally grant public access. You can still create them, but those ACLs will not become effective, resulting in the bucket or object not being publicly accessible.
>
> You should take the time to review your ACLs once enabled and remove any public ACLs to prevent any possible future mistakes. Any existing public ACLs will no longer be ignored if the block public access granted through any ACLs option is later disabled
>
> Remember, w/ this option enabled, if you have any bucket policies granting public access to buckets and objects, those buckets or objects will remain publicly accessible. If you wish to block all public access to buckets and objects, choose the block all public access option.

> ### Block public access granted through new public bucket policies
>
> This option only prevents the creation of new bucket policies that grant public access. Any existing bucket policies are not affected. If you currently have any bucket policies configured that grant public access, those buckets or objects will remain publicly accessible.
>
> To use this setting effectively, you should apply it at the AWS account level. A bucket policy can allow users to alter a bucket's **Block Public Access** settings. Therefore, users who have permission to change a bucket policy could insert a policy that allows them to disable the **Block Public Access** settings for the bucket. If enabling this setting for the entire account, rather than for a specific bucket, Amazon S3 blocks public policies even if a user alters the bucket policy to disable this setting.
>
> Remember, w/ this option enabled, if you have any existing bucket policies or ACLs granting public access to buckets and objects, those buckets or objects will remain publicly accessible. If you wish to block all public access to buckets and objects, choose the block all public access option.

> ### Block public and cross-account access granted through any public bucket policies
>
> This option only affects how you evaluate bucket policy permissions. When you enable this option, it ignores any buckets or objects that have public permissions granted through bucket policies. This option, when enabled, restricts access to a bucket w/ a public policy to only AWS services and authorized users within the bucket owner's account. This setting blocks all cross-account access to the bucket w/ a public policy (except by AWS services), while still allowing users within the account to manage the bucket.
>
> This does not alter existing bucket policies, but ignores any existing bucket policies that grant public access, blocking public access and and any cross-account access configurations. Remember, w/ this option enabled, if you have any ACLs granting public access to buckets and objects, these buckets and objects will remain publicly accessible. If you wish to block all public access to buckets and objects, choose the block all public access option.

Unless you need to make your bucket or objects publicly available, we strongly recommend enabling the block all public access option.

## Access policies

**Access policy** describes who has access to what resources. They attach to your resources, such as buckets and objects, and are also called resource policies. For example, **bucket policies** and **access control lists** are resource-based policies b/c you attach them directly to buckets and objects.

**User policies** or **IAM policies** are access policies attached to users in your account. You may choose to use one type of policy or a combination of both, to manage permissions w/ your Amazon S3 resources.

Examine the examples of access policies in the image below. Both the bucket and user policies are written in JSON format. Just by looking at the code, it may not be immediately apparent which policy is for a user and which is for a bucket. However, by looking at what the policy is attached to, you can quickly determine which type of policy it is.

![Fig. 1 Example of a bucket policy (resource-based policy) and an IAM policy (user-based policy)](img/SAA-CO2/storage-services/simple-storage-service/securing-data-access/diagram01.png)

## Bucket policies

In order to grant other AWS accounts or IAM users access to the bucket and the objects in it, you need to attach a bucket policy. B/c you are granting access to a user or account, a bucket must define a PRINCIPAL (which is an account, user, role, or service) entity within the policy. You will notice that the "Principal" statement is listed in the policy. Consult the image below as an example.

![Fig. 2 The red arrow points to the Principal called demo-user who is being granted access to the getting-started-s3-bucket.](img/SAA-CO2/storage-services/simple-storage-service/securing-data-access/diagram01.png)

> B/c bucket policies grant access to another AWS account or IAM user, you must specify the principal, or the user to whom you are granting access, as a "Principal" in the bucket policy.

When using bucket policies, Amazon S3 is managing the security. Bucket policies supplement, and in many cases, replace legacy ACL-based access policies. Amazon S3 supports a bucket policy size limit of up to 20 kb.

> ### When to use a bucket policy
>
> Use a bucket policy if:
>
> * You need to grant cross-account permissions to other AWS accounts or users in another account, w/o using IAM roles
>
> * Your IAM policies reach the size limits for users, groups, roles.
>
> * You prefer to keep access control policies in the Amazon S3 environment.
>
> * Although both bucket and user policies support granting permission for all Amazon S3 operations, the user policies are for managing permissions for users in your account.

## IAM policies

You can use IAM to manage access to your Amazon S3 resources. You can create IAM users, groups, and roles in your account and attach access policies to them granting them access to AWS resources, including Amazon S3.

There are maximum size limitations for IAM policies for users, groups, and roles. IAM policies have a 2 kb size for users, 5 kb for groups, and a 10 kb for roles. Also note that there is no principal stanza listed in the User policy as the principal is whichever user the policy is applied to.

![Fig. 3 IAM policy](../../../../../img/SAA-CO2/storage-services/simple-storage-service/securing-data-access/diagram03.png)

> In this example, you grant your IAM user access to your buckets, ocean-life-bucket, and allow the user to Put*, Get*, Delete* objects. This policy also grants `s3:ListAllMyBuckets`, `s3:GetBucketLocation`, `s3:ListBucket`, `s3:PutObjectAcl`, and `s3:GetObjectAcl` permissions which are additional permissions required by the AWS console. This example also shows each permission applied to different resources. In the top permission, the resource is all buckets (*). In the middle the permission is applied to one specific bucket, called `ocean-life-bucket`. The bottom permission applies to only objects within the `ocean-life-bucket`.

> ### When to use IAM user policies
>
> Use IAM policies if:
>
> * You need to control access to AWS services other than Amazon S3. IAM policies allow for easier centralized management of all your permissions.
>
> * You have numerous Amazon S3 buckets each w/ different permission requirements. IAM policies will be easier to manage than having to define a large number of Amazon S3 bucket policies. This way you can focus on having fewer, more detailed IAM policies.
>
> * You prefer to keep access control policies in the IAM environment.

## Query string authentication

You can use a query string to express a request entirely in a URL. To do this, you use query parameters to provide request information, including the authentication information. B/c the request signature is part of the URL, this is referred to as a presigned URL.

### Presigned URLs

All objects and buckets by default are private and only the object owner has permission to access these objects. However, the object owner can share their objects w/ others who do not have AWS credentials. They can create a presigned URL to grant time-limited permission to download objects.

The main purpose of a pre-signed URL is to grant temporary access to the required object. When you create a presigned URL, you must provide your security credentials and then specify a bucket name, an object key, an HTTP method (`PUT` for uploading objects), and an expiration date and time. Anyone who receives the presigned URL can then access the object. For example, if you have a video in your bucket and both the bucket and the object are private, you can share the video w/ others by generating a presigned URL. A presigned URL remains valid for a limited period of time, which is specified when the URL generates. You can use presigned URLs to embed clickable links, which can be valid for up to seven days, in HTML.

A use case scenario for presigned URLS is that you can grant temporary access to your Amazon S3 resources. For example, you can embed a presigned URL on your website or alternatively use it in command line client (such as Curl) to download objects. You could also programmatically generate a presigned URL to allow a user to upload an object to a bucket.

> ### How to create a presigned URL
>
> This demonstration shows you how to create a presigned URl for a non-public object called `whale.jpg`
>
> Imagine that you need to share this image w/ an external vendor who is creating an informational brochure on ocean life. The external vendor does not have an AWS account and has no access to your private bucket to view objects.
>
> 1. First, you must locate the bucket w/ the correct image. In this step, you identify the necessary bucket as `getting-started-s3-demo-bucket` and the object, `whale.jpg`, as the object you need to share w/ the external vendor.
>
> 2. Using the AWS CLI, you view the command to help to determine which options are required to generate the presigned url.
>
> 3. Amazon S3 supports only AWS Signature Version 3 in most AWS Regions. You must set the default Signature Version for your AWS CLI connection. This step shows how to configure Signature Version 4 as default ▶︎ `aws configure set default.s3.signature_version s3v4`
>
> 4. In this step you run the command to generate the presigned URL indicating the correct bucket, `getting-started-s3-demo-bucket` and the object to share as `whale.jpg` ▶︎ `aws s3 presign s3://getting-started-s3-demo-bucket/whale.jpg --expires 3600 --region us-east-1`
>
> 5. You can now copy and send the presigned URL to your external vendor so that they may access the `whale.jpg` image. This URL contains a short time limit, 'Amz-Expires=3600' which means this particular URL will expire in 5 minutes.
>
> 6. Finally, once you have sent the presigned URL to your external vendor, they will be able to paste the URL into a browser to view the image within the allotted time. Once that time has expired, they will lose access to view the image.

> ### Permissions to the object
>
> Anyone w/ valid security credentials can create a presigned URL. However, to successfully access an object, someone who has permission to perform the operation must create the presigned URL.

> ### Credentials
>
> The credentials that you can use to create a presigned URL include:
>
> * IAM instance profile: Valid up to 6 hours
>
> * AWS Security Token Service: Valid up to 36 hours when signed w/ permanent credentials, such as the credentials of the AWS account root user or an IAM user
>
> * IAM user: Valid up to 7 days when using AWS Signature Version 4
>
> To create a presigned URL that's valid for up to 7 days, first designate IAM user credentials (the access key and secret access key) to the SDK that you're using. Then, generate a presigned URL using AWS Signature Version 4.

> ### Token expiration
>
> If you created a presigned URL using a temporary token, then the URL expires when the token expires, even if you created the URL w/ a later expiration time.

## Amazon S3 Object Ownership

Prior to the addition of Amazon S3 Object Ownership, an S3 object was owned by the AWS account that uploaded the object. If a bucket owner uploads an object, the bucket owner remains the owner of that object. If another AWS account uploads objects to your bucket, the objects remain owned by the other AWS account that uploaded the object.

AWS has released a new feature that allows all objects written to a bucket to be owned by the bucket owner. W/ Amazon S3 Object Ownership, the bucket owner now has full control of the objects, and may own any new objects written by other accounts automatically.

Amazon S3 Object Ownership has two modes:

1. Object writer - The account that is writing the object owns the object.

2. Bucket owner preferred - The bucket owner will own the object if uploaded w/ the **bucket-owner-full-control** canned ACL. W/o this setting and canned ACL, the object is uploaded to the bucket, but remains owned by the uploading account.

## Enforcing S3 Object Ownership

After setting S3 Object Ownership to bucket owner preferred, you can add a bucket policy to require all Amazon S3 PUT operations to include the bucket-owner-full-control canned ACL. This ACL grants the bucket owner full control of new objects, and w/ the S3 Object Ownership setting, it transfers object ownership to the bucket owner. If the uploader fails to meet the ACL requirement in their upload, the request will fail. This enables bucket owners to enforce uniform object ownership across all newly uploaded objects in their buckets.

## Access Analyzer

On the Amazon S3 console, you can use **Access Analyzer** for Amazon S3 to review all buckets that have bucket access control lists (ACLs), bucket policies, or access point policies that grant public or shared access.

**Access Analyzer** for Amazon S3 alerts you to buckets configured to allow access to anyone on the internet or other AWS accounts, including AWS accounts outside of your organization. For each public or shared bucket, you receive findings that report the source and level of public or shared access.

## Ways to prevent accidental public access

There are a number of ways to prevent accidental public access to your data.

* Do use **Amazon S3 Block Public Access** at the account level to prevent public access to your buckets.

* Do audit your existing bucket ACLs and bucket policies.

* Do configure the appropriate roles and permissions to limit the ability to change block public access settings.

* Don't allow public access unless you have an explicit reason that something must be public, such as static website hosting in Amazon S3.

* Don't allow public access as a way to troubleshoot. Take your time and review permission/access policies incrementally to determine access issues.