# Create a static website using Amazon S3

## About this lab

In this AWS hands-on lab, we will create and configure a simple static website. We will go through configuring that static website w/ a custom error page. This will demonstrate how to create a cost-efficient website hosting for sites that consist of files like HTML, CSS, JavaScript, fonts, and images.

> ### Amazon S3 static websites
>
> You can use Amazon S3 to host a static website. W/ a static website, the individual web pages include only static content that does not change frequently. This is different from using a dynamc website, where the content is constantly changing and constantly updated. From the AWS Management Console, you can easily configure your bucket for static website hosting w/o needing to write any code.
>
> To host a static website on Amazon S3, you configure a bucket for website hosting and then upload your content. When you configure a bucket as a static website, you must enabled website hosting, set public read permissions, and create and add an index document.
>
> Depending on your website requirements, you can also configure redirects, web traffic logging, and a custom error document. If you prefer not to use the AWS Management Console, you can create, update, and delete the website configuration programmatically by using the AWS SDKs.
>
> For the best compatibility, we recommend that you avoid using dots (.) in bucket names, except when using buckets for static website hosting. If you include dots in a bucket name that is not a static website, you can't use virtual-host-style addressing over HTTPS, unless you perform your own certificate validation. This is b/c the security certificates used for virtual hosting of the buckets don't work for buckets w/ dots in their names. Virtual hosting is the practice of serving multiple websites from a single web server.

## Solution

Log in to the live AWS environment using the credentials provided. Make sure you're in the N. Virginia (`us-east-1`) region throughout the lab.

## Create S3 bucket

1. Begin by navigating to the [GitHub repository](https://github.com/ACloudGuru-Resources/Course-Certified-Solutions-Architect-Associate/tree/master/labs/creating-a-static-website-using-amazon-s3) for the code.

2. Select the `error.html` file.

3. Above the code area, click **Raw**.

4. Right-click and select **Save Page As**, and save the file as `error.html`.

> [!NOTE]
> If you are using Safari as your web browser, ensure that you remove the **.txt** from the end of the filename. Also, ensure that the *Format* is **Page Source**. When asked whether you want to save the file as plain text, click **Don't Append**.

5. Repeat this for the `index.html` file.

6. From the AWS Management Console, navigate to S3.

7. Click **Create bucket**.

8. Set the following values:

    * *Bucket name*: **my-bucket-** with the AWS account number or another series of numbers at the end to make it globally unique

    * *Region*: **US East (N. Virginia) us-east-1**

9. In the *Block Public Access settings for this bucket* section, un-check *Block all public access*.

    * Ensure all 4 permissions restrictions beneath it are also un-checked.

10. Check the box to acknowledge that turning off all public access might result in the bucket and its objects becoming public.

11. Leave the rest of the settings as their defaults.

12. Click **Create bucket**.

13. Click the radio button next to the bucket name to select it.

14. Click **Copy ARN**.

15. Select the bucket name.

16. Click **Upload**.

17. Click **Add files**, and upload the `error.html` and `index.html` files you previously saved from GitHub.

18. Leave the rest of the settings as their defaults.

19. Click **Upload**.

20. Click **Close** in the upper right.

## Enable static website hosting

1. Click the **Properties** tab.

2. Scroll to the bottom of the screen to find the Static *website hosting* section.

3. On the right in the Static website hosting section, click **Edit**.

4. On the *Edit static website hosting* page, set the following values:

    * *Static website hosting*: **Enable**

    * *Hosting type*: **Host a static website**

    * *Index document*: **index.html**

    * *Error document*: **error.html**

5. Scroll down, and click **Save changes**.

6. Scroll back down to the *Static website hosting* section.

7. Open the listed endpoint URL in a new browser tab. You'll see a `403 Forbidden` error message.

## Apply Bucket Policy

1. Back in S3, click the **Permissions** tab.

2. In the *Bucket policy* section, click **Edit**.

3. In the *Policy* box, enter the following JSON statement (replacing `<BUCKET_ARN>` with the bucket ARN provided right above the Policy box):

```json
{
    "Version": "2012-10-17",
    "Id": "Policy1645724938586",
    "Statement": [
        {
            "Sid": "Stmt1645724933619",
            "Effect": "Allow",
            "Principal": "*",
            "Action": "s3:GetObject",
            "Resource": "arn:aws:s3:::<BUCKET_ARN>/*"
        }
    ]
}
```

> [!NOTE]
> Ensure the trailing `/*` is present so the policy applies to all objects within the bucket. It is best to make the Id and Sid unique values.

1. Click **Save changes**.

2. Refresh the browser tab with the static website (the endpoint URL you opened a minute ago). This time, it should load the site correctly.

3. Add a `/` at the end of the URL and some random letters (anything that's knowingly an error). This will display your `error.html` page.
