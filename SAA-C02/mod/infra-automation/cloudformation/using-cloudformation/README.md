# Using CloudFormation

## What are the basic technical concepts of CloudFormation?

* **Resources** – Any of the things you can create within AWS, which includes things like Amazon Simple Storage Service (Amazon S3) buckets, Amazon Elastic Compute Cloud (Amazon EC2) instances, or Amazon Simple Queue Service (Amazon SQS) queues.

* **Templates** - Text-based (JSON or YAML) descriptions of CloudFormation stacks that you can use to define all of your resources, including which resources depend on each other.

* **Stack** - A collection of AWS resources that you can manage as a single unit.

* **StackSet** – A named set of stacks that use the same template, but applied across different accounts and Regions. You can create, update, or delete stacks across multiple accounts and Regions with a single operation.

### Sample template

```yaml
AWSTemplateFormatVersion: "2010-09-09"
Description: "Simple SQS example"
Parameters:
  QueueName:
    Type: String
    Default: TheQueue1
    Description: Please enter the name of the Queue.
Resources:
  TheQueue:
    Type: AWS::SQS::Queue
    Properties:
      QueueName: !Ref QueueName
  TheQueueUpdaterRole:
    Type: AWS::IAM::Role
    Properties
      AssumeRolePolicyDocument:
        Version: 2012-10-17
        Statement:
          -
            Effect: Allow
            Principal:
              Service:
              - ec2.amazonaws.com
            Action:
              - 'sts:AssumeRole'
      Policies:
        -
          PolicyName: QueueUpdater
          PolicyDocument:
            Version: 2012-10-17
            Statement:
              -
                Effect: Allow
                Action:
                  - sqs:Get*
                  - sqs:List*
                  - sqs:SendMessage*
                  - sqs:ReceiveMessage
                Resource: !GetAtt TheQueue.Arn
Outputs:
  QueueUrl:
    Value: !Ref TheQueue
  QueueARN:
    Value: !GetAtt TheQueue.Arn
```

The accompanying CloudFormation template creates an Amazon SQS queue and illustrates parameters and outputs.

It creates two resources: an SQS queue, starting in line 10, and an IAM role, starting in line 14. 

It defines one parameter in line 5, which would need to be provided when creating a stack with this template (and which you could use to create different stacks with the same template, by providing different parameters).

It defines two *outputs*, values that get attached to the stack, and can be viewed in the console or accessed with a program.

## How can I create CloudFormation stacks using the AWS Management Console?

```yaml
---
AWSTemplateFormatVersion: "2010-09-09"
Description: "Simple budget example"
Parameters:
  Email:
    Type: String
    Default: email@example.com
    Description: Please enter the email address to which budget notifications should be addressed.
Resources:
  BudgetExample:
    Type: "AWS::Budgets::Budget"
    Properties:
      Budget:
        BudgetLimit:
          Amount: 10
          Unit: USD
        TimeUnit: MONTHLY
        BudgetType: COST
      NotificationsWithSubscribers:
        - Notification:
            NotificationType: ACTUAL
            ComparisonOperator: GREATER_THAN
            Threshold: 99
          Subscribers:
            - SubscriptionType: EMAIL
              Address: !Ref Email
        - Notification:
            NotificationType: ACTUAL
            ComparisonOperator: GREATER_THAN
            Threshold: 80
          Subscribers:
          - SubscriptionType: EMAIL
            Address: !Ref Email
Outputs:
  BudgetId:
    Value: !Ref BudgetExample
```

### 1. Open the console and find CloudFormation

Open the console, search for CloudFormation on the services search bar, then select CloudFormation.

### 2. Choose Create stack

On the CloudFormation section, choose Create stack.

### 3. Specify the template

You can provide your own template, use a sample template, or use the template designer, which is a graphical interface for creating templates.

If you are providing the template, you can specify an Amazon S3 location or upload it from your computer.

### 4. Specify details – name and parameters

You need to provide a unique name for your stack (stacks in different accounts or different Regions can have the same name, but not in the same account and Region).

If your template has any parameters, you can specify them here.

### 5. Configure more options

On the next screen, you can configure more options.

You can add tags to your stack to make it easier to group and manage them.

You can specify whether you want a special IAM role for CloudFormation to use to operate on the stack.

You can specify other advanced options, like enhanced protection with a stack policy, rollback and notification options, and other creation options.

### 6. Review and confirm

The console will display all options that will be used for your stack, so you can review and verify they are all as intended. 

Choosing Create stack will initiate the stack creation.

### 7. Stack list and events

While the stack is creating, you can see a list of events updating as new resources are created. The other tabs have different information about your stack.

Eventually, your stack and all its resources should be in CREATE_COMPLETE state.

## How can I create CloudFormation stacks using the AWS Command Line Interface?

If you have the [AWS Command Line Interface (AWS CLI)](https://aws.amazon.com/cli/) installed and configured, you can create a stack using the CloudFormation [create-stack](https://awscli.amazonaws.com/v2/documentation/api/latest/reference/cloudformation/create-stack.html) command, You need to pass it the stack name and the template, which can be a local file or on S3. You can use the CloudFormation [describe-stacks](https://awscli.amazonaws.com/v2/documentation/api/latest/reference/cloudformation/describe-stacks.html) command to obtain basic information about your stack.

* Create stack command:

```zsh
$ aws cloudformation create-stack --stack-name SimpleBudget --template-body file://SimpleBudget.yaml
```

* Describe stack command:

```zsh
$ aws cloudformation describe-stacks --stack-name SimpleBudget
```

> This commands show the use of that command and the describe-stacks command used to obtain information about the stack, which you can use to monitor its progress.
