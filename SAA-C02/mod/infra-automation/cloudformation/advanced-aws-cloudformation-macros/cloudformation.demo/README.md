# Simple AWS CloudFormation Demo

In this section, you will use the AWS Management Console to create a stack. You will work with an example template from the AWS CloudFormation Sample Template Library.

The following three videos will take you through these steps:

1. Navigating the AWS CloudFormation console and designer view.

2. Creating an Amazon EC2 key pair.

3. Launching a simple WordPress blog site using an AWS CloudFormation template.

> [!NOTE]
>
> While AWS CloudFormation is free, the AWS resources that AWS CloudFormation creates are live. You will incur standard usage fees for these resources until you terminate them in the last task in this tutorial.

## Creating an Amazon EC2 key pair

The use of some AWS CloudFormation resources and templates will require you to specify an Amazon EC2 key pair for authentication. One example is when you are configuring SSH access to your instances.

Amazon EC2 key pairs can be created with the AWS Management Console.

> [!NOTE]
>
> **AWS CloudFormation can perform only actions that you have permission to do**. You will need similar permissions to terminate instances when you delete stacks with instances. Use AWS Identity and Access Management (IAM) to manage permissions for AWS resources.

## Deleting a stack

When you delete a stack, you specify the stack to delete, and AWS CloudFormation deletes the stack and all the resources in that stack. You can delete stacks by using the AWS CloudFormation console, API, or AWS Command Line Interface (AWS CLI).

## Conclusion

AWS CloudFormation makes deploying a set of AWS resources as simple as submitting a template. Follow the steps below to launch your next applications using AWS CloudFormation.

1. **Prepare**: Determine what permissions you need for the resources, or what endpoints to use.

2. **Design template**: Design an AWS CloudFormation template (a JSON or YAML formatted document) in AWS CloudFormation Designer, or write one in a text editor.

3. **Save template**: Store the template file locally or in an Amazon S3 bucket. If you created a template, save it w/ any file extension, such as .json, .yaml, or .txt.

4. **Create AWS CloudFormation stack**: Create the stack by specifying the location of your template file, such as a path on your local computer or an Amazon S3 URL. You can create stacks by using the AWS CloudFormation console, API, or the AWS CLI.
