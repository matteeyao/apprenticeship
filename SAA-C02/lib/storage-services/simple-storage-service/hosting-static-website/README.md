# Hosting Static Content on S3

1. A company has a static corporate website hosted in a standard S3 bucket and a new web domain name that was registered using Route 53. You are instructed by your manager to integrate these two services in order to successfully launch their corporate website.

What are the prerequisites when routing traffic using Amazon Route 53 to a website that is hosted in an Amazon S3 Bucket? (Select TWO.)

[x] The S3 bucket name must be the same as the domain name

[ ] The S3 bucket must be in the same region as the hosted zone

[ ] The record set must be of type "MX"

[x] A registered domain name

[ ] The Cross-Origin Resource Sharing (CORS) option should be enabled in the S3 bucket

**Explanation**: Here are the prerequisites for routing traffic to a website that is hosted in an Amazon S3 Bucket:

  * An S3 bucket that is configured to host a static website. The bucket must have the same name as your domain or subdomain. For example, if you want to use the subdomain portal.tutorialsdojo.com, the name of the bucket must be portal.tutorialsdojo.com.

  * A registered domain name. You can use Route 53 as your domain registrar, or you can use a different registrar.

  * Route 53 as the DNS service for the domain. If you register your domain name by using Route 53, we automatically configure Route 53 as the DNS service for the domain.

> The option that says: **The record set must be of type "MX"** is incorrect since an MX record specifies the mail server responsible for accepting email messages on behalf of a domain name. This is not what is being asked by the question.

> The option that says: **The S3 bucket must be in the same region as the hosted zone** is incorrect. There is no constraint that the S3 bucket must be in the same region as the hosted zone in order for the Route 53 service to route traffic into it.

> The option that says: **The Cross-Origin Resource Sharing (CORS) option should be enabled in the S3 bucket** is incorrect because you only need to enable Cross-Origin Resource Sharing (CORS) when your client web application on one domain interacts with the resources in a different domain.

<br />

2. A media company needs to configure an Amazon S3 bucket to serve static assets for the public-facing web application. Which methods ensure that all of the objects uploaded to the S3 bucket can be read publicly all over the Internet? (Select TWO.)

[ ] Configure the cross-origin resource sharing (CORS) of the S3 bucket to allow objects to be publicly accessible from all domains.

[ ] Do nothing. Amazon S3 objects are already public by default.

[x] Configure the S3 bucket policy to set all objects to public read.

[x] Grant public read access to the object when uploading it using the S3 Console.

[ ] Create an IAM role to set the objects inside the S3 bucket to public read.

**Explanation**: By default, all Amazon S3 resources such as buckets, objects, and related subresources are private which means that only the AWS account holder (resource owner) that created it has access to the resource. The resource owner can optionally grant access permissions to others by writing an access policy. In S3, you also set the permissions of the object during upload to make it public.

Amazon S3 offers access policy options broadly categorized as resource-based policies and user policies. Access policies you attach to your resources (buckets and objects) are referred to as resource-based policies.

For example, bucket policies and access control lists (ACLs) are resource-based policies. You can also attach access policies to users in your account. These are called user policies. You may choose to use resource-based policies, user policies, or some combination of these to manage permissions to your Amazon S3 resources.

You can also manage the public permissions of your objects during upload. Under **Manage public permissions**, you can grant read access to your objects to the general public (everyone in the world), for all of the files that you're uploading. Granting public read access is applicable to a small subset of use cases such as when buckets are used for websites.

Hence, the correct answers are:

* Grant public read access to the object when uploading it using the S3 Console.

* Configure the S3 bucket policy to set all objects to public read.

> The option that says: **Configure the cross-origin resource sharing (CORS) of the S3 bucket to allow objects to be publicly accessible from all domains** is incorrect. CORS will only allow objects from one domain (travel.cebu.com) to be loaded and accessible to a different domain (palawan.com). It won't necessarily expose objects for public access all over the internet.

> The option that says: **Creating an IAM role to set the objects inside the S3 bucket to public read** is incorrect. You can create an IAM role and attach it to an EC2 instance in order to retrieve objects from the S3 bucket or add new ones. An IAM Role, in itself, cannot directly make the S3 objects public or change the permissions of each individual object.

> The option that says: **Do nothing. Amazon S3 objects are already public by default** is incorrect because, by default, all the S3 resources are private, so only the AWS account that created the resources can access them.

<br />
