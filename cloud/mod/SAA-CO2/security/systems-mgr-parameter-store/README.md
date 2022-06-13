# Systems Manager Parameter Store

Feature that addresses securely managing configuration and secrets within AWS. Parameter Store is an essential tool for caching and distributing secrets securely to AWS resources.

> **What is Parameter Store?**
>
> * Component of AWS Systems Manager (SSM)
>
> * Secure **serverless** storage for configuration and secrets:
>
>   * Passwords
>
>   * Database connection strings
>
>   * License codes
>
>   * API keys
>
> * Values can be stored encrypted (KMS) or plaintext
>
> * Separate data from source control
>
> * Store parameters in **hierarchies**
>
> * Track versions of values in the Parameter Store
>
> * Set TTL to expire values such as passwords

Imagine that you manage security for a large scale-application which uses thousands of EC2 instances all set to automatically build and be replaced if they fail. They're stateless and they're designed to be swapped out, terminated, and replaced constantly to maintain the performance of the platform. So where do you store configurations? What about passwords? What about connection strings, host names, and anything else that your application needs to function? Traditionally, what we've done is we've stored secrets along w/ application code or hard-coded into applications and even stored within S3 buckets that are designed to host centralized configuration or centralizes secrets and that's not ideal. Storing passwords or secrets anywhere can result in some pretty high profile leakage. One fairly common scenario is that access keys or passwords are stored along w/ code inside a public GitHub repository. Once it's committed, these keys can be leaked, and then we get an application exploit or other information security issues.

Parameter Store addresses this. It's a serverless, scalable, and high-performance system for storing data and secrets, things like passwords, database connection strings, license codes, API keys.

Now by using Parameter Store, you can separate data from source control to avoid the scenario where we would potentially leak access keys into GitHub repositories for example.

You can track and audit how values change over time or roll back if necessary.

Setting TTL to expire values such as passwords enables you to enforce password rotation.

## Organizing Parameters into Hierarchies

![Fig. 1 Hierarchies of Parameters](../../../../img/aws/security/systems-mgr-parameter-store/hierarchies-of-parameters.png)

> * `GetParametersByPath` to retrieve all parameters in a hierarchy:
>
>   * /dev
>
>   * /dev/db
>
>   * /prod/app

W/ Parameter Store, your data can be stored hierarchically, which means that you can build tree-like structures. In this example, we have to hierarchies: `dev` and `prod`. These can represent your development or production environments. At each level of this hierarchical structure, we can retrieve data based on that level, so you can retrieve all the data for the entire `prod` tree, if you wanted to, or jut the data for `dev`, `DB`, `mySQL`. You could represent a position in the hierarchy using a path like you see here.

B/c it's hierarchical, we're able to grant permissions at any point in this tree structure so you're able to give administrators access to a single piece of information, maybe a single configuration item, or you can grant them access to an entire area for their application, or we can grant teams access to whole environments, or maybe just `prod`, `dev`, or `test` and these hierarchies can be up to 15 levels deep. To retrieve all parameters in the hierarchy, you can use the `get` parameters by path API call.

## Launching latest Amazon Linux AMI in CloudFormation

One really cool application of parameter store is its integration w/ CloudFormation. So here's an example of how we can use Parameter Store inside our CloudFormation templates:

```
Parameters:
  LatestAmiId:
    Type: 'AWS::SSM::Parameter::Value<AWS::EC2::Image::Id>'
    Default: '/aws/service/ami-amazon-linux-latest/amzn2-ami-hvm-x86_64-gp2'

Resources:
  Instance:
    Type: 'AWS::EC2::Instance'
    Properties:
      ImageId: !Ref LatestAmiId
```

AWS make available the latest AMI IDs in any given region via path and Parameter Store that they manage. You could see that path here on line four. So you can see here, in this example, we can get the latest AMI ID for Amazon Linux Two in our region and reference that in our EC2 instance resource here, in our example, that's line `ImageId: !Ref LatestAmiId`. So there's no need to create and manage any complex mappings like we used to have to do prior to the introduction of Parameter Store.

The first thing we need to do before we create our Lambda function is to create an execution role in IAM. This execution role for the Lambda function is going to define what permissions that Lambda function has. So let's go to IAM and see how this works. We're going to create a new policy that we'll attach to a new role.

```json
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": [
      "logs:CreateLogGroup",
      "logs:CreateLogStream",
      "logs:PutLogEvents",
      "ssm:GetParameter*",
      "ssm:GetParametersByPath"
    ],
      "Resource": "*"
    }
  ]
}
```

What this policy allows is access to CloudWatch logs, all of the Parameter Store API calls that start w/ `get` parameter, and `get` parameter by path.

Now that we have created that policy, we're going to create a new role and we're going to attach that policy.

Now we need to go to Lambda and create our function.

```py
import json
import os
import boto3

client = boto3.client("ssm")
env = os.environ["ENV"]
app_config_path = os.environ["APP_CONFIG_PATH"]
full_config_path = "/" + env + "/" + app_config_path


def lambda_handler(event, context):

    print("Config Path: " + full_config_path)

    param_details = client.get_parameters_by_path(
        Path=full_config_path, Recursive=True, WithDecryption=True
    )

    print(json.dumps(param_details, default=str))
```

First, we import a couple of dependencies. We need JSON to format some output, OS so that we can have access to our environment variables, and package called `Boto3`. `Boto3` is the AWS SDK for Python. Next, we're going to create a client object. This is the interface to the SSM service, and we're going to create two environment variables, one called `ENV` and another called `APP_CONFIG_PATH`. To set these, we need to scroll down here and under environment variables, click `Edit`. So we get that variable of our `ENV` environment variable set to `prod` and `APP_CONFIG_PATH` set to `ACG`, and now we're going to concatenate all these w/ some slashes, so our full config path is going to be `/prod/acg`. We're going to be creating some parameters in Parameter Store under this hierarchy, and we're going to use the following code to retrieve those values. We're going to use the `get` parameters by path API call in Parameter Store to retrieve all of the values under this tree structure that we're going to create in Parameter Store.

Before we switch over to Parameter Store to create those parameter values, we need to go to KMS to create a customer master key that we can use to encrypt those values. So we'll go here to services, go to KMS, and create a new key.

As you can see, Parameter Store is incredibly effective at storing secrets and other values that you might want to secure.
