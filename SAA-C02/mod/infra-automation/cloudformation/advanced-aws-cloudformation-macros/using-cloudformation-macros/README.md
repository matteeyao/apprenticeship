# Using AWS CloudFormation Macros

## Types of Macros

We can use macros in two different ways:

* To process the whole template, we include the macro in the Transform section. While you cannot directly pass any parameters to the macro, you can include them in the Metadata section of the template.

* To process only a piece of the template, we reference the macro in an Fn::Transform. The macro applies to elements on the same level at the Fn::Transform. This allows us to pass parameters to the macro.

## Global macros

Macros that apply to the whole document go in the Transform section of the document, where we specify a list of transforms. We reference the macro by name, as demonstrated below. 

```
Transform: [IdentityMacro]
```

## Local macros

We can also apply macros to only a portion of the document. This also allows us to pass parameters to our macro. For example, the AddTags macro described in the previous section expects a Tags parameter. It also expects to be applied on the Resources section of the document, so we would call it as follows.

```
Resources:
  Fn::Transform:
    Name: 'AddTags'
    Parameters:
      Tags: [Tag1=Val1, Tag2=Val2]
... define other resources here ...
```

Notice that the fragment passed to the macro includes all of the sibling nodes of the macro. In the preceding example, it would include all the other elements in the Resources section.

## Running the template

Calling both macros:

```yml
AWSTemplateFormatVersion: '2010-09-09'
Description: Examples of using macros
Transform: 
  - IdentityMacro
Resources:
  Fn::Transform:
    Name: 'AddTags'
    Parameters:
      Tags: [Author=me, Team=Accounting, Tag3=Val3]
  FirstQueue:
    Type: "AWS::SQS::Queue"
    Properties:
      DelaySeconds: 0               # How long to delay messages
      MaximumMessageSize: 1024      # in bytes
      MessageRetentionPeriod: 3600  # in seconds; let's use an hour, just for fun 
      QueueName: FirstQueue
      ReceiveMessageWaitTimeSeconds: 0 # don't wait to see if more messages appear
      VisibilityTimeout: 30
```