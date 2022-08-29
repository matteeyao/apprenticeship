# Use AWS SAM and AWS Cloud9 to Create, Edit, and Deploy a Lambda Function

In this demonstration, you will learn how to create, edit, and deploy a Lambda function by using AWS SAM and AWS Cloud9.

## Step I

In this demonstration, you will learn how to create, edit, and deploy a Lambda function by using AWS SAM and AWS Cloud9. In this demo, you will watch an Amazon S3 event trigger a Lambda function, which then writes the event information to a DynamoDB table. An AWS SAM template is used to create this S3 bucket and the DynamoDB table used in the function. 

To begin, sign in to the AWS Management Console using a login with administrator rights. In the top-right corner, select the region in which to create the AWS Cloud9 instance. In the **Services** field, enter **Cloud9**. Select the **AWS Cloud9** service, and choose **Create Environment** from the AWS Cloud9 page.

Name the environment **LambdaC9Env**, and choose **Next step**. For the environment type, select **Create a new EC2 instance**. For the instance type in this demo, choose **t2.micro**. If you plan to use AWS Cloud9 for a large production workload, choose an instance size and type that matches your anticipated workload. Keep the default 30-minute cost-saving setting, and make a note of the IAM role name that will be created. This IAM role will allow AWS Cloud9 to call other AWS services. 

Choose **Next step**. On the **Review** page, read the AWS Cloud9 best practice recommendations and then choose **Create environment**.

It takes a few minutes for AWS to create the environment, but once completed, the **Welcome** page appears, with a terminal window near the bottom of the screen. Before you start, it’s a good idea to verify the version of AWS SAM installed in your environment. In the terminal window at the bottom, enter:

```zsh
sam --version
```

## Step II

The second section of the demo focuses on creating and editing a Lambda function. In the AWS Cloud9 console, in the left navigation pane, choose the AWS explorer icon. Select the region to see the list of available resources and then select **Lambda**. This lists any available Lambda functions within the same AWS account and region as your AWS Cloud9 environment. If you have a function here, you could import it and work with it. For this demo, you create a new one. To create a new function, right-click Lambda and choose **Create Lambda SAM application**. Enter the following:

* In the **Select a SAM Application Runtime**, choose **nodejs14.x**.

* In the **Select a SAM Application Template**, choose **AWS SAM Hello World A basic SAM app**.

* In the **Select a workspace folder for your new project**, select **LambdaC9Env**.

* In the **Enter a name for your new application**, enter **LambdaSAMApp** in the text field and choose ENTER.

The Information: AWS Toolkit window appears. Choose **Open launch.json** button. This JSON file contains the launch configurations of the **LambdaSAMApp** application. In the left navigation pane, choose the project folder icon for `LambdaSAMApp`, expand **hello-word**. The function is listed as `app.js`. To test the Lambda function, you can either right-click on `app.js` and choose Run or go to the top menu bar, choose the **Auto** drop-down menu, select the **LambdaSAMApp:HelloWorldFunction (nodejs14.x)** and then choose the **Run** button.

In the bottom pane of the AWS Cloud9 instance, an AWS toolkit tab appears. This window displays the logs of the function run, and you can verify that the function ran successfully, when the “hello-world” message appears. Now, you’ll edit the test payload to use the planned Amazon S3 trigger.

Select the bash terminal window, and enter:

```zsh
sam local generate-event s3 put
```

A sample payload is generated. Select the payload and copy it. Now, in the `Launch.json` window, under the **Lambda** section, locate the “payload” variable. In this demo, the variable is on line 33. Between the curly braces, enter `"json"`. 

This indicates the payload format, and now paste the payload from the previous step. To format the payload, choose **Edit, Code Formatting and Apply Code Formatting**, and then save the file. To test this, select the `app.js` tab and add the line `console.log(event)` to the function. This line allows you to see the output in the console window. Select **File**, **Save**, and choose **Run** to test the function locally. Now, in the **AWS Toolkit** window you can see the Amazon S3 output sample payload listed as a result of entering the `console.log(event)` command.

## Step III

In this section, you edit the Lambda function to add both the S3 bucket and the event source to trigger the function. In the left navigation pane, open the template file by selecting the `template.yaml` file. This template file indicates which resources were created as part of the AWS SAM application. You need to define the S3 bucket in this template. Locate the **Resources** section of the template, go to the bottom, and add a new line that aligns with the `HelloWorldFunction` resource. 

Enter the bucketname **HelloWorldFunctionBucket**. In the next line, add a resource type of **AWS:: S3:: Bucket**. Ensure that the resource type is indented correctly, and save the file. To verify that the indentation is correct, go to the terminal window and enter:

```zsh
cd LambdaSAMApp
```

To change to the `LambdaSAMApp` directory, enter:

```zsh
sam validate
```

The message displays that **This is a valid SAM Template**. Next, open the *AWS Serverless Application Model Developer Guide* and locate the S3-Event example. This example can be used to update the Amazon S3 event source in your function. Select the S3Event and copy it. In your template, locate the **Events** section in your function, and paste the example S3Event you just copied from the AWS documentation. For this demo, you want the function to trigger whenever any object is created in this S3 bucket. This example does that by using the `s3:ObjectCreated*` event. Align the code properly, and then replace the “ImagesBucket” name in the **Bucket** property with the name of the bucket: `HelloWorldFunctionBucket`.

Save this file, and then run:

```
sam validate
```

This time, you receive an invalid SAM Template error, because the event type and properties are not properly aligned under the `S3Event`. After the alignment fix, save the file and rerun:

```
sam validate
```

The results indicate that the template is now valid. Next, deploy the HelloWorldFunction with this template. For this, you will need to create a bucket to upload the artifacts that AWS SAM creates for the deployment. 

In the left navigation pane, select the **AWS Explorer** icon, expand the region, right-click S3, and select **Create bucket**. Enter **lambdasamtestbucket** and press ENTER. Next, right-click Lambda and select **Deploy SAM Application**.

  * In the **Which SAM Template would you like to deploy to AWS**, select the template.yaml file that is displayed.

  * In the **Select an AWS S3 bucket to deploy code to**, select the **lambdasamtestbucket** from the list.

  * In the **Enter the name to use for the deployed stack**, enter **LambdaTestDeploy** and press ENTER.

Choose the AWS Toolkit window in the bottom pane of the AWS Cloud9 console to view the deployment logs. 

The deploy process creates all the necessary IAM roles and permissions, along with the resources defined in the `template.yaml` file. The deployment process takes a few minutes to complete. 

When the **Successfully deployed SAM application** message appears, open the Lambda console in a new browser tab. Now, under **Functions**, you have the **LambdaTestDeploy-HelloWorldFunction**. Open the function, in the **Code Source** section, expand `app.js` and verify that the “hello world” updates you made locally to the function now appear here. 

Next, you want to verify that the S3 bucket was created.

Open the Amazon S3 console in a new tab, and locate the **LambdaTestDeploy-helloworldfunctionbucket**. Open the bucket. Next, choose **Upload** to upload a file to this bucket. Choose **Add files**, and select a file. Then choose **Upload**.

Adding this file to the bucket triggers the invocation of the **HelloWorldFunction**. 

Switch back to the Lambda console, choose the **Monitor** tab, select **View logs in CloudWatch**. On the **Log Streams** tab, select the log stream for this function. Expand the message between START and END. In the details of the message, you can verify a successful Amazon S3 PUT event. This indicates that the file you uploaded to your S3 bucket successfully triggered your Lambda function to run. 

Navigate to the Lambda console, select the **HelloWorld** function and choose the **Monitor** tab. In the CloudWatch metrics panel, you can view the runtime metrics of your Lambda function in the individual panels. If the data does not appear, change the duration by selecting the **Custom** dropdown button and setting the time to 15-minute intervals. Then choose the **Refresh** button. You must refresh the window a few times before the data appears. Once you view the data, scroll over the data to view more details.

## Step IV

In this section, you add logic to your function, allowing you to write information about the Amazon S3 objects to a DynamoDB table. To do this, you must define the DynamoDB table as a resource in the SAM Template just like you did with the event source. To begin, select the **Template.yaml** tab. Locate the **Resources, HellowWorldFunctionBucket**, and add a line below it called, *HelloWorldFunctionTable* with a resource type of *AWS:Serverless::SimpleTable*. Save the file.

In the Bash window, at the bottom, enter:

```
sam validate
```

If you’ve aligned the columns correctly, you get a confirmation that this is a valid template. Next, you need to grant Lambda permission to write to the DynamoDB table. You’ll use a SAM Policy Template to give only the required permissions to your function. In the **Template.yaml** tab, locate line 31, which reads **Events: S3:ObjectCreated**, add a new line for Policies. You’ll copy the Policy Template from Github. Locate and copy the **DynamoDBCrudPolicy** and paste it into the template under the **Policies: section**. Update the table name reference to match your HelloWorldFunctionTable. Save the file. In the Bash window at the bottom, enter:

```
sam validate
```

You should get a confirmation that this is a valid template. Next, on the **AWS Explorer** tab, right-click on Lambda, and choose **Deploy SAM Application**. Choose the same template file and  bucket name that you’ve been using, and enter the same stack name **LambdaTestDeploy**. The logs of deployment can be viewed in the AWS Toolkit window. Once you see the message indicating you’ve “Successfully deployed SAM application to Cloudformation stack”, open a new browser and go to the DynamoDB console. 

In the left navigation pane, select **Tables**, and verify that the **LambdaTestDeploy** table is listed. You can select the table and review the table’s properties. 

## Step 5

In this last part of the demonstration, you update the function to capture and write values from the Amazon S3 event payload to the DynamoDB table. 

In AWS Cloud9, select the `launch.json` file. Locate and view the message structure of the **eventSource** and **Objectkey**. These are the variables used to store the Amazon S3 object values and need to be added to your `app.js` file. Add the variable for **S3ObjectKey** and **S3TimeStamp** in order for this information from the payload to write to the Lambda function.

Next, you add code to write these values to the DynamoDB table. In a new browser tab, open the *AWS SDK for JavaScript Developer Guide*, and copy the line that loads the SDK. Paste this line at the top of the function. Return to the Developer’s Guide and copy the rest of the code. Paste this code into the function below your Amazon S3 variable. Go to **Edit, CodeFormatting, Apply Code formatting**.

The next step is to replace the Table name of `CUSTOMER_LIST` with your Lambda table name. Open the DynamoDB console, copy the table name that was created by the SAM template, and paste it into the function for the **Table Name** value.

Replace `year` with `ID` to match the primary key in the table, and make that equal to the **s3ObjectKey** variable. Replace `CUSTOMER_NAME` timestamp, and make that value equal to the `s3ObjectTime` variable, in this case enter **S3TimeStamp**.

The last step is to add the **await** operator to the putItem command to ensure the item is written to the table in a timely fashion.

Select **File**, and save the function. Go to **AWS: Explorer**, expand **Region**, **Lambda**, and **Deploy SAM Application**. Use the same variables that you’ve been using, and wait for the function to complete. Watch the **AWS ToolKit** tab for the message “Successfully deployed SAM Application to CloudFormation stack: LambdaTestDeploy”.

Open the Amazon S3 console and upload another file to the event source s3 bucket to trigger the Lambda function. The `lavender.jpg` file is now uploaded to Amazon S3. Navigate back to the **CloudWatch** tab, select **Log Groups**, choose **HelloWorldFunction**, and select the **Log streams** tab. Open the event and make note of the eventTime stamp. Navigate to the DynamoDB console, choose the **Items** tab, and you see the `lavender.jpeg` with the eventTIme Id that matches the eventTime in the CloudWatch logs entry. 

In this demonstration, you successfully created, edited, and deployed a Lambda function using AWA SAM and AWS Cloud9. You accomplished this by having an Amazon S3 event trigger a Lambda function that writes event information to a DynamoDB table. You used an AWS SAM template to create the S3 bucket and the DynamoDB table used in the demonstration. Thank you for watching this demonstration. 
