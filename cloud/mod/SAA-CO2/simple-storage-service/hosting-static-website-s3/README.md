# Create a static website using Amazon S3

## About this lab

In this AWS hands-on lab, we will create and configure a simple static website. We will go through configuring that static website w/ a custom error page. This will demonstrate how to create a cost-efficient website hosting for sites that consist of files like HTML, CSS, JavaScript, fonts, and images.

![Creating a Static Website Using Amazon S3](https://s3.amazonaws.com/assessment_engine/production/labs/832301d1-2af5-42c6-9114-d2e171297f3d/lab_diagram_Lab_-_Creating_a_Static_Website_Using_Amazon_S3.001.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=ASIAVKPCGNLN6Q4JYFA5%2F20220308%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20220308T182348Z&X-Amz-Expires=3600&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEFkaCXVzLWVhc3QtMSJIMEYCIQDGLsEGGFqRQ%2B%2FQlcSUB4rf%2Bq%2Fw7YrxOhBXybPjII97dwIhAIg3SGC5hQJC0UEmhQqnxx8oASNqwroi7GikTL%2BD78DWKowECML%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FwEQAhoMMzY2MDgzNDY3OTk1IgykSliITy9K5NQWgEkq4AMEwjOTkwvveM%2FHMqj2bA%2BK0xMLKavawwj87qk7KO03UvlA53PMjzWoPDRf0GkYOHrvjdlxSeHnw%2FukdU0elc5qltdlGO2SZnbjA4dizY8bvPuR5MJHwjvffpIunC%2F%2BWwEwnHBZaMpP6jWPBYtZ5kk8lcQqhRJX481cj0l4lYdDR5FBVGoVtREJO6y5GEQLw7Pe9t6bLgxL%2FZU4KgbskX97nGLGPamrP%2BxRrNsy6FdAyh73YtTdCVORpBabFv9WNb3z%2BPgTCNGdaWuTbVLKwqiNFRWMpMmrz6oGlC2EPgIX5EAqXEfQ%2FyeoDRcSrP8ZjOR7mz1FjQAs032GA2LLC6xYM4mxbk0FyIN3LdQerv47XDIf%2F7%2FPjfH6Giif8dL2B2dxoLvxnSkknyka%2BCXZQr8mP%2B%2B%2BytbZOKko1ZhaUYUfsVEqxjzfVYHl%2BcSgJvSzjrzpYlm5LMTOZxKn%2FS310NK3U8ZYaACBt9JB0WrF8yKw2KREPNvRngCcNOct4xxspClIz%2BKVXgTXczgKtizx82lpqmp4Hm8PwPutY9g7euRXG37hcO2901fYxYCR2Kf3SBuuCAEN%2FZfqfc47%2FKd6pG6HJIOVYTajuCuRKq9bVJkEIgh%2BcaNPusHwFRrOQhHFf4MwqI%2BekQY6pAFNt8LVnpCiZWX%2FlAqdbJEvJ84RaC35vvyanOxxYl9VItGuTaSSYszVWGlc%2FPiNEFXRi2WEZqfPPlngXB1pqxX59ypenA7ReDpm0U4VawJKWKChJ28oJMC0%2BdoGesvK5wwoOHYNWIoCEyNNby1OKPEzB67sWYfzBLBRPQjzLnElQFe%2FM6fFKIW1n3TbBKVS9i2iN29trNRt1kTbjKpXuuBKEsueOw%3D%3D&X-Amz-SignedHeaders=host&X-Amz-Signature=d15ee753fe85d876d1d43cbc80cc9aeb40f9b3a091ded9e215c0b62d25e0debb)

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
