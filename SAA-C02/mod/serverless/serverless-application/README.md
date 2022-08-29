# Serverless Application Model (SAM)

SAM is an open source framework that allows you to build serverless applications easily.

> **What is SAM?**
>
> * CloudFormation extension optimized for serverless applications
>
> * New types: functions, APIs, tables
>
> * Supports anything CloudFormation supports
>
> * Run serverless applications locally
>
> * Package and deploy using CodeDeploy

On top of CloudFormation, SAM defines a number of new resource types.

W/ just a few lines of configuration, you can define the application you want and model it using SAM.

SAM is simply a superset of CloudFormation

You can also run your serverless applications locally using Docker. This not only makes it ideal for unit testing. You can also save on your bill b/c you're not consuming any resources inside AWS when you're testing.

You can also use SAM to package and deploy your applications using CodeDeploy.

## Anatomy of a SAM Template

```
AWSTemplateFormatVersion: '2010-09-09'
Transform: AWS::Serverless-2016-10-31
Description: Hello World SAM Template

Globals:
  Function:
    Timeout: 3

Resources:
  HelloWorldFunction:
    Type: AWS::Serverless::Function
    Properties:
      CodeUri: hello_world/
      Handler: app.lambda_handler
      Runtime: python3.8
      Events:
        HelloWorld:
          Type: Api
          Properties:
            Path: /hello
            Method: get

Outputs:
  HelloWorldApi:
    Description: "API Gateway endpoint URL for Prod stage for Hello World function"
    Value: !Sub "https://${ServerlessRestApi}.execute-api.${AWS::Region}.awazonaws.com/Prod/hello/"
  HelloWorldFunction:
    Description: "Hello World Lambda Function ARN"
    Value: !GetAtt HelloWorldFunction.Arn
  HelloWorldFunctionIamRole:
    Description: "Implicit IAM Role created for Hello World function"
    Value: !GetAtt HelloWorldFunctionRole.Arn
```

At the top, we tell CloudFormation that this is a SAM template. Notice on line two, we have `Transform`. When we deploy the SAM template, CloudFormation will recognize that this is a SAM template and transform it internally into something a little more low level.

The next sections is a `Globals` section which applies the same properties to all functions. If we have a number of Lambda functions that we define in our SAM template, and they all have certain attributes in common, for example, the function timeout, we don't need to repeat ourselves in every single resource. We just set it here in `Globals`. This basically just applies the same properties to all our functions.

Like w/ CloudFormation, we have our resources section, which creates a Lambda function from local code. Also creates an API Gateway endpoint, mappings, and permissions. `AWS::Serverless::Function` not only creates a Lambda function from our local code, but it also creates an API gateway endpoint, mappings, and permissions for you automatically. It's much simpler than trying to do this directly in CloudFormation. So you can see here that this is a Lambda function written in Python and that's exposed via an API gateway. The path `/hello` and we're using an HTTP `get` method.

Not the `Outputs` section's going to give some relevant information for you. It's going to output the API gateway endpoint URL that it automatically generates for you along w/ the Lambda functions ARN and the IAM role that it also creates for you. Now, aside from API gateway, SAM lets you integrate your Lambda function w/ a number of events sources.

## Lambda function event sources

![Fig. 1 Lambda Function Event Sources](../../../../img/aws/serverless/serverless-application/lambda-function-event-sources.png)

Event sources, all of which can trigger a Lambda function. We have Alexa skills, IOT rules, SNS topics, SQS queues, DynamoDB tables, CloudWatch, API Gateway.

```
sam init

Choice: 1

Runtime: 8

cd sam-app

ll

vim template.yaml

sam deploy -guided
```

SAM - the best way to define and deploy your serverless applications.
