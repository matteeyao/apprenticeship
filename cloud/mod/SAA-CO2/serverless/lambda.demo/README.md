# Programmatically Utilizing Data from S3

> **Learning Objectives**
>
> * Investigate the Lab Environment
>
> * Collect Object Keys from S3
>
> * Collect and Combine Data from S3
>
> * Observe the Results on the Web Interface

> **About this lab**
>
> In this lab, we're tasked w/ completing a Lambda function that collects data from S3, performs some basic formatting, and returns it to API Gateway to be loaded into a simple web interface.

## Introduction

![Fig. 1 Lab diagram](../../../../img/aws/serverless/lambda.demo/lambda-lab-diagram.png)

### Scenario

You've been brought into a team to complete a proof of concept for a low cost employee directory for your company. The team has created a simple web interface, organized some placeholder employee information, and placed it in S3. They're having trouble getting the data from 3 into Lambda and need your assistance. You will need to collect all 1500 placeholder records from JSON files stored in an S3 bucket and return them in a single return from the Lambda function provided in the lab environment.

### Additional resources

* [Boto3 S3 List Objects V2](https://boto3.amazonaws.com/v1/documentation/api/latest/reference/services/s3.html#S3.Client.list_objects_v2)

* [Boto3 Get Object](https://boto3.amazonaws.com/v1/documentation/api/latest/reference/services/s3.html#S3.Client.get_object)

* [Lambda Function](https://github.com/linuxacademy/Content-AWS-Certified-Data-Analytics---Speciality/tree/master/Lab_Assets/programmatically_utilizing_data_from_s3/lambda)

### Solution

Log in to the AWS Management Console using the credentials provided for the lab. In another browser tab, open the **random-users** website provided for the lab. The site won't load yet b/c you haven't assigned the Lambda function an action.

## Investigate the lab environment

1. From the AWS Management Console, navigate to *S3* using the *Services* menu or the unified search bar. You should see two buckets in your account:

  * `random-users-<ACCOUNT_NUMBER>`, which is your static website.

  * `random-users-data-<ACCOUNT_NUMBER>`, which is the data that will populate the website.

2. Select the `random-users-data-<ACCOUNT_NUMBER>` bucket, then select **users_1.json**.

3. Use the *Object actions* dropdown in the top right corner to select **Download**.

4. Open the file and review the user data. You will collect the data from all 3 objects and organize it into a single object that can be returned in the web interface.

5. Close the file and navigate back to the AWS Management Console.

## Create the Employee Directory using Objects Keys and data from S3

1. Navigate to *Lambda* using the *Services* menu or the unified search bar. You should see 2 Lambda functions in your account.

2. Select the **Users_primary** function.

3. In the *Code Source* section, select **function.py** and review the code.

4. Replace the existing code w/ the *function_solved.py* code provided in the lab resources.

5. In a new browser tab, navigate to S3 and copy the `random-users-data-<ACCOUNT_NUMBER>` bucket name.

6. Navigate back to Lambda and paste the bucket name on the `s3_bucket = ` line.

7. Click **Deploy**. The Lambda function executes a number of tasks:

  * Combines data from multiple S3 buckets into a single JSON object.

  * Initializes a list to hold this data before returning it.

  * Initializes a boto3 S3 client to interact w/ S3.

  * Lists the objects in the bucket and collects the object keys that begin w/ `users-`.

  * Combines the returned data w/ the existing data list. What makes this programmatic is that you can add or remove users files from the S3 bucket, and that changes how many users are loaded in the web interface.

  * Retrieves the data from the objects in the bucket and concatenates it so you see just one list of data in the web interface.

## Observe the Results on the Web Interface

1. After the changes are successfully deployed, navigate to the `random-users` website. You may need to refresh the page and wait a few moments for the data to load. All 1500 users should load correctly.

2. Navigate back to the AWS Management Console, then navigate to *S3*.

3. Check the checkbox to left of the `random-users-data-<ACCOUNT_NUMBER>` bucket, then select the **users_1.json** object.

4. Click **Delete**, then type `permanently delete` into the text field and click **Delete objects**.

5. After the file is successfully deleted, click **Close** to return to your S3 bucket.

6. Navigate back to the `random-users` website and refresh the page. You should now have 1000 employee records instead of 1500.
