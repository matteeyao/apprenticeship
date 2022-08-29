# AWS Lambda Function Permissions

This lesson explores permissions and security in your Lambda functions.

With Lambda functions, there are two sides that define the necessary scope of permissions – permission to invoke the function, and permission of the Lambda function itself to act upon other services. Because Lambda is fully integrated with AWS Identity and Access Management (IAM), you can control the exact actions of each side of the Lambda function.

![Fig. 01 AWS Lambda Policies](../../../../../../img/SAA-CO2/serverless/lambda/foundations/function-permissions/fig01.jpeg)

> The Lambda function (center) requires two different IAM policies (one on each side) to invoke and run the function.

Permissions to invoke the function are controlled using an IAM resource-based policy. An IAM execution role defines the permissions that control what the function is allowed to do when interacting with other AWS services. Look at the full interaction of these two permission types and then explore each one in further detail.

> ![Fig. 02 IAM Permission types](img/SAA-CO2/serverless/lambda/foundations/function-permissions/fig02.jpeg)
>
> Resource policies grant permissions to invoke the function, whereas the execution role strictly controls what the function can to do within the other AWS service.

## Execution role

The execution role gives your function permissions to interact with other services. You provide this role when you create a function, and Lambda assumes the role when your function is invoked. The policy for this role defines the actions the role is allowed to take — for example, writing to a DynamoDB table. The role must include a trust policy that allows Lambda to “AssumeRole” so that it can take that action for another service. You can write the role or use the managed roles (with predefined permissions) provided by Lambda to simplify the process of creating an execution role. You can add or remove permissions from a function's execution role at any time, or configure your function to use a different role. 

Remember to use the principle of least privilege when creating IAM policies and roles. Always start with the most restrictive set of permissions and only grant further permissions as required for the function to run. Using the principle of least privilege ensures security in depth and eliminates the need to remember to 'go back and fix it' once the function is in production.

> ![Fig. 03 IAM policy](../../../../../../img/SAA-CO2/serverless/lambda/foundations/function-permissions/fig03.jpeg)
>
> This IAM policy grants Lambda permissions to other services.

You can also use IAM Access Analyzer to help identify the required permissions for the IAM execution role. IAM Access Analyzer reviews your AWS CloudTrail logs over the date range that you specify and generates a policy template with only the permissions that the function used during that time.

## Example: Execution role definitions

> ### IAM policy
>
> This IAM policy allows the function to perform the `"Action": "dynamodb:PutItem"` action against a DynamoDB table called "test" in the us-west-2 region.

```json
"Version": "2012-10-17",
  "Statement": [
    {
      "Sid": "Allow PutItem in table/test",
      "Effect": "Allow",
      "Action": "dynamodb:PutItem",
      "Resource": "arn:aws:dynamodb:us-west-2:###:table/test"
    }
  ]
```

> ### Trust policy
>
> A trust policy defines what actions your role can assume. The trust policy allows Lambda to use the role's permissions by giving the service principal lambda.amazonaws.com permission to call the AWS Security Token Service (AWS STS) AssumeRole action.
>
> This example illustrates that the principal "Service":"lambda.amazonaws.com" can take the "Action":"sts:AssumeRole" allowing Lambda to assume the role and invoke the function on your behalf.

```json
  {
    "Effect": "Allow",
    "Action": "sts:AssumeRole",
    "Principal": {
      "Service": "lambda.amazonaws.com"
    }
  }
```

## Resource-based policy

A resource policy (also called a function policy) tells the Lambda service which principals have permission to invoke the Lambda function. An AWS principal may be a user, role, another AWS service, or another AWS account.

> ![Fig. 4 IAM Resource Policy](img/SAA-CO2/serverless/lambda/foundations/function-permissions/fig04.jpeg)
>
> This IAM policy grants permissions to invoke the Lambda function.

Resource policies make it easy to grant access to the Lambda function across separate AWS accounts. For example, if you need an S3 bucket in the production account to invoke your Lambda function in the Prod-2 account, you can create a new IAM role in Prod-2 and allow production to assume that role. Alternatively, you can include a resource-based policy that allows production to invoke the function in Prod-2.

The resource-based policy is an easier option and you can see and modify it via the Lambda console. A consideration with cross-account permissions is that a resource policy does have a size limit. If you have many different accounts that need to invoke the function and you have to add permissions for each account via the resource policy, you might reach the policy size limit. In that case, you would need to use IAM roles instead of resource policies. 

## Policy comparison

> ### Resource-based policy
>
> Lambda resource-based (function) policy
>
>   * Associated with a "push" event source such as Amazon API Gateway
>
>   * Created when you add a trigger to a Lambda function
>
>   * Allows the event source to take the *lambda:InvokeFunction* action

> ### Execution role
>
> IAM execution role
>
>   * Role selected or created when you create a Lambda function
>
>   * IAM policy includes actions you can take with the resource
>
>   * Trust policy that allows Lambda to *AssumeRole*
>
>   * Creator must have permission for *iam:PassRole*

### Distinct permissions for distinct purposes

> ![Fig. 5 IAM Resource Policy](../../../../../../img/SAA-CO2/serverless/lambda/foundations/function-permissions/fig05.jpeg)
>
> To illustrate how Lambda uses these two policies for permissions, imaging Lambda as a large warehouse. The resource policy is a set of rules defining who can deliver packages to the warehouse. Here, you have defined a rule that allows both the red and blue truck to deliver packages to the warehouse. This rule represents a resource policy providing warehouse access only to the red and blue trucks.

> ![Fig. 6 Lambda function](../../../../../../img/SAA-CO2/serverless/lambda/foundations/function-permissions/fig06.jpeg)
>
> The arrival of the blue trick prompts your workers to perform actions, such as moving boxes from the blue truck to the warehouse. This would be like the arrival of the blue truck invoking the code in your Lambda function.

> ![Fig. 7 Execution role](../../../../../../img/SAA-CO2/serverless/lambda/foundations/function-permissions/fig07.jpeg)
>
> Now, if you require your workers to unpack any box w/ a blue label, you would need an execution role of "unpack boxes w/ blue labels" to grant your worker (the Lambda function) permission to perform this action. The execution role defines what actions your function can perform. If your role only allows the blue labeled boxes to be unpacked, then that is the only action it can take. It cannot unpack the red labeled boxes b/c that permission wasn't granted.

## Ease of management

For ease of policy management, you can use authoring tools such as the AWS Serverless Application Model (AWS SAM) to help manage your policies. For a Lambda function, AWS SAM scopes the permissions of your Lambda functions to the resources used by your application. You can add IAM policies as part of the AWS SAM template. The policies property can be the name of AWS managed policies, inline IAM policy documents, or AWS SAM policy templates.

## Example resource policy

The following is a basic resource policy example.

* The policy has an Effect of "Allow". The Effect can be Deny or Allow.

* The Principal is the Amazon S3 "s3.amazonaws.com" service. This policy is allowing the Amazon S3 service to perform an Action.

* The Action that S3 is allowed to perform is the ability to invoke a Lambda function "lambda:InvokeFunction" called "my-s3-function".

```json
{
  "Version": "2012-10-17",
  "Id": "default",
  "Statement": [
    {
      "Sid": "lambda-56cad012-...",
      "Effect": "Allow",
      "Principal": {
        "Service": "s3.amazonaws.com"
      },
      "Action": "lambda:InvokeFunction",
      "Resource": "arn:aws:lambda:us-west-2:...:function:my-s3-function",
      "Condition": {
        "StringEquals": {
          "AWS:SourceAccount": "..."
        },
        "ArnLike": {
          "AWS:SourceArn": "arn:aws:s3:::lambda-lambda-2"
        }
      }
    }
  ]
}
```

> The resource policy gives Amazon S3 permission to invoke a Lambda function called "my-s3-function".

## Resource policies and execution roles on the AWS Lambda console

> ### Lambda permissions on the console
>
> From the Lambda console, choose the function you are working with, then choose the **Configuration** tab and **Permissions** to reveal the execution role and resource policy associated w/ the function.

> ### Execution role details
>
> To show the details of the IAM policy for the execution role, choose **Edit**.

> ### Execution role
>
> The resource summary lists each resource that is part of the function's execution role. Select the **By action** or the **By resource** tab to see the actions the function is permitted to take w/ that resource.

> ### Resource-based policy
>
> The resource-based policy (function policy) for a Lambda function shows the permissions to invoke the function. Adding a trigger to your Lambda function from the console automatically generates the function policy.

## Accessing resources in a VPC

Enabling your Lambda function to access resources inside your virtual private cloud (VPC) requires additional VPC-specific configuration information, such as **VPC subnet IDs** and **security group IDs**. This functionality allows Lambda to access resources in the VPC. It does not change how the function is secured. You also need an execution role with permissions to create, describe, and delete elastic network interfaces. Lambda provides a permissions policy for this purpose named "AWSLambdaVPCAccessExecutionRole".

### Lambda and AWS PrivateLink

To establish a private connection between your VPC and Lambda, create an interface VPC endpoint. Interface endpoints are powered by AWS PrivateLink, which enables you to privately access Lambda APIs without an internet gateway, NAT device, VPN connection, or AWS Direct Connect connection. 

Instances in your VPC don't need public IP addresses to communicate with Lambda APIs. Traffic between your VPC and Lambda does not leave the AWS network. 

## Learning Summary

1. What IAM entities must be included in an execution role for a Lambda function to interact with other services, such as DynamoDB? (Select TWO.)

[x] IAM policy that defines the actions that can be taken within DynamoDB

[x] Trust policy that grants "AssumeRole" permission to Lambda to act on DynamoDB

[ ] IAM group defining users of the Lambda function

[ ] IAM user w/ admin permissions to Lambda and DynamoDB

**Explanation**: You need both the IAM policy that defines the actions Lambda can take and a trust policy that grants Lambda the "AssumeRole" permission. You do not have to create any IAM users or groups to allow Lambda to take action.

2. Which of these statements describe a resource policy? (Select THREE.)

[ ] Must be chosen or created when you create a Lambda function

[x] Can give Amazon S3 permission to initiate a Lambda function

[ ] Can give Lambda permission to write data to a DynamoDB table

[x] Can grant access to the Lambda function across AWS accounts

[x] Determines who has access to invoke the function

[ ] Determines what Lambda is allowed to do

**Explanation**: A resource policy determines who is allowed in (who can initiate your function, such as Amazon S3), and it can be used to grant access across accounts.

An execution role must be created or selected when creating your function, and it controls what Lambda is allowed to do (such as writing to a DynamoDB table). It includes a trust policy w/ AssumeRole.
