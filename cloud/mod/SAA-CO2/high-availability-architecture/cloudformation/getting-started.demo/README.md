# Getting Started w/ CloudFormation

## Learning Objectives

> ✓ Create a cloudformation stack
>
> ✓ Update the cloudformation stack
>
> ✓ Add cloudformation stacks
>
> ✓ Delete cloudformation stacks

## Introduction

> **About this lab**
>
> CloudFormation is a powerful automation service within AWS. It can be used to create simple or complex sets of infrastructure any number of times. This hands-on lab provides a gentle introduction to CloudFormation, using it to create and update a number of S3 buckets. By the end of this hands-on lab, you will be comfortable using CloudFormation and can begin experimenting w/ your own templates.

![Fig. 1 Getting Started w/ CloudFormation](../../../../img/aws/ha-arch/cloudformation-getting-started.demo/getting-started-with-cloudformation.png)

## Solution

Login to the live AWS environment using the credentials provided, and make sure you are in the `us-east-1` (N. Virginia) region.

The CloudFormation templates and other hands-on lab files can be found on [Github](https://github.com/ACloudGuru-Resources/Course-Certified-Solutions-Architect-Associate/tree/master/labs/getting_started_with_cfn). Navigate to the needed file and choose the `RAW` view. From there, use the **Save As...** functionality in your browser.

A list of AWS resources and what happens when updates occur can be found [online](https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-template-resource-type-ref.html).

## Create a CloudFormation Stack

1. Download the [createstack.json](https://raw.githubusercontent.com/ACloudGuru-Resources/Course-Certified-Solutions-Architect-Associate/master/labs/getting_started_with_cfn/createstack.json) file by right-clicking and selecting **Save As** functionality.

2. In the AWS console, navigate to CloudFormation.

3. Click **Create stack** ▶︎ **With new resources (standard)**.

4. Select **Template is ready**.

5. Select **Upload a template file**.

6. Click **Choose file**.

7. Browse to the `createstack.json` file you downloaded and saved.

8. Select and upload it, and click **Next**.

9. Name the stack "cfnlab".

10. Click **Next**.

11. Scroll through the available stack options, leaving them all at the defaults, and click **Next**.

12. Review your selections and click **Create stack**.

13. Refresh the page to watch the progress.

14. Navigate to S3. We didn't specify a name in the `json` file for this bucket, so AWS names it w/ the **<STACK_NAME>-<LOGICAL_VOLUME_NAME>-<RANDOM_STRING> format. Yours will be: **cfnlab-catpics-<RANDOM_STRING>**.

## Update the CloudFormation Stack

### Compare the Original and Updates Templates

1. Download and save the [updatestack1.json](https://raw.githubusercontent.com/ACloudGuru-Resources/Course-Certified-Solutions-Architect-Associate/master/labs/getting_started_with_cfn/updatestack1.json) and [update2.json](https://raw.githubusercontent.com/ACloudGuru-Resources/Course-Certified-Solutions-Architect-Associate/master/labs/getting_started_with_cfn/updatestack2.json) files like you did for `createstack.json`.

2. Open the `createstack.json`, `updatestack1.json`, and `updatestack2.json` files in a text editor.

3. Compare the contents of the `createstack.json` and `updatestack1.json` files, focusing on the differences in the `Resources` section. You should see that the `updatestack1.json` file contains an additional logical resource named "dogpics".

4. Compare the contents of the `createstack.json` and `updatestack2.json` files, once again focusing on the differences in the `Resources` section. You should see that the `updatestack2.json` file contains an additional logical resource named "dogpics" and includes a bucket name of "catsareawesome123" for the "catpics" resource.

### Update #1

1. Navigate to CloudFormation.

2. Select the `cfnlab` stack, and click **Update**.

3. Select **Replace current template**.

4. Select **Upload a template file**.

5. Click **Choose a file**, and select and upload `updatestack1.json`.

6. Click **Next** ▶︎ **Next** ▶︎ **Next**.

7. In the **Change set preview** section, review the changes that will be made based on the `updatestack1.json` template. You should be adding the "dogpics" resource.

8. Click **Update stack**.

9. Refresh the page to watch the progress.

10. Once it's finished updating, navigate to S3. You should see the new `dogpics` bucket.

### Remove the Update

1. Navigate back to CloudFormation.

2. Select the `cfnlab` stack, and click **Update**.

3. Select **Replace current template**.

4. Select **Upload a template file**.

5. Click **Choose file**, and select and upload `createstack.json` again.

6. Click **Next** ▶︎ **Next** ▶︎ **Next**

7. In the **Change set preview** section, review the changes that will be made based on the `createstack.json` template. You should be removing the "dogpics" resource.

8. Click **Update stack**.

9. Refresh the page to watch the progress.

10. Once it's finished updating, navigate to S3. You should see the `dogpics` bucket is now gone.

### Update #2

1. Navigate to the `updatestack2.json` file that is open in the text editor.

2. Change the `123` characters in `catsareawesome123` to something unique (e.g., your birthday and today's date).

3. Save the file.

4. In the CloudFormation console, select the `cfnlab` stack, and click **Update**.

5. Select **Replace a current template**.

6. Select **Upload a template file**.

7. Click **Choose file**, and select and upload `updatestack2.json`.

8. Click **Next** ▶︎ **Next** ▶︎ **Next**.

9. In the **Change set preview** section, review the changes that will be made based on the `updatestack2.json` template. You should be modifying the "catpics" resource and adding the "dogpics" resource.

10. Click **Update stack**.

11. Refresh the page to watch the progress.

12. Once it's finished updating, navigate to S3. You should see 2 changes: The `dogpics` bucket is back and the `catpics` bucket has been replaced w/ the `catsareawesome` bucket.

## Add CloudFormation Stacks

### Create a Stack w/ `updatestack2.json`

1. Navigate to CloudFormation.

2. CLick **Create stack** ▶︎ **With new resources (standard)**.

3. Select **Template is ready**.

4. Choose to **Upload a template file** and **Choose file**.

5. Select and upload `updatestack2.json`.

6. Click **Next**.

7. Name the stack "cfnlab2".

8. Click **Next** ▶︎ **Next**.

9. Accept the remaining defaults, and click **Create stack**.

10. Refresh the page to watch the progress.

11. Note it eventually fails, b/c you can't have another S3 bucket w/ the same name (in this case, a bucket w/ the `catsareawesome` name already exists).

### Create a Stack w/ `updatestack1.json`

1. Click **Create stack** ▶︎ **With new resources (standard)**.

2. Select **Template is ready**.

3. Choose to **Upload a template file** and **Choose file**.

4. Select and upload `updatestack1.json`.

5. Click **Next**.

6. Name the stack "cfnlab3".

7. Click **Next** ▶︎ **Next**.

8. Accept the remaining defaults, and click **Create stack**.

9. Refresh the page to watch the progress.

10. Once it's complete, navigate to S3, where you should see 2 new buckets: `cfnlab3-catpics-<RANDOM_STRING>` and `cfnlab3-dogpics-<RANDOM_STRING>`.

## Delete CloudFormation Stacks

1. Navigate to CloudFormation.

2. Select `cfnlab3`.

3. Click **Delete**.

4. In the dialog box, click **Delete stack**.

5. Click the `cfnlab3`, and then click the **Events** tab to see the resources being deleted.

6. Click **Stacks** in the breadcrumb link trail at the top.

7. Select `cfnlab2`.

8. Click **Delete**.

9. In the dialog box, click **Delete stack**.

10. Click the `cfnlab2`, and then click the **Events** tab to see the resources being deleted.

11. Click **Stacks** in the breadcrumb link trail at the top.

12. Select `cfnlab`.

13. Click **Delete**.

14. In the dialog box, click **Delete stack**.

15. Click the `cfnlab`, and then click the **Events** tab to see the resources being deleted.

16. Once it's all done, navigate to S3. You should see all the `cfnlab` buckets are gone, as well as the `catsareawesome` bucket.

## Conclusion

We've managed to use templates to create stacks and related resources, and we were able to get things cleaned up when we were done with them in pretty short order.
