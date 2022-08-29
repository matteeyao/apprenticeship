# Template Structure

What is an AWS CloudFormation template?

A template is a declaration of the AWS resources that make up a stack. The template is stored as a text file in a format that complies w/ the JSON or YAML standard. As text files, you can create and edit them in any text editor. You can also manage the files in your source control management (SCM) system w/ the rest of your source code.

In the template, you can declare the AWS resources that you want to create and configure. You declare an object as a name-value pair or a pairing of a name w/ a set of child objects enclosed. The syntax depends on the format you use. The only required top-level object is the resources object, which must declare at least one resource.

To provision and configure your stack resources, you must understand AWS CloudFormation templates. These formatted text files in JSON or YAML describe the resources to provision in your AWS CloudFormation stacks. Create and save templates using AWS CloudFormation Designer or any text editor.

If you are unfamiliar w/ JSON or YAML, you can use AWS CloudFormation Designer to help you get started w/ AWS CloudFormation templates. AWS CloudFormation Designer is a tool for visually creating and modifying templates.

## Template fragments

```yaml
---
AWSTemplateFormatVersion: "<VERSION DATE>"
Description: "<STRING>"
Metadata:
  <TEMPLATE METADATA>
Parameters:
  <SET OF PARAMETERS>
Mappings:
  <SET OF MAPPINGS>
Conditions:
  <SET OF CONDITIONS>
Transform:
  <SET OF TRANSFORMS>
Resources:
  <SET OF RESOURCES>
Outputs:
  <SET OF OUTPUTS>
```

### Template Version

The AWS CloudFormation template version is the version that the template conforms to.

**Template format version example**:

`YAML:AWSTemplateFormatVersion: "2010-09-09"`

`JSON:"AWSTemplateFormatVersion": "2010-09-09"`

### Description

The description is a text sting that describes the template. This section must always follow the template format version section. The description input field appears in the Specify Parameters page of the Create Stack wizard.

**Description example**:

```yaml
Description:
  Here are some
  details about the template
```

```json
"Description": "Here are some details about the template".
```

### Metadata

The optional metadata section provides objects w/ additional information about the template. One helpful metadata key is "AWS::CloudFormation::Interface." When using parameters in the AWS CloudFormation console to create or update a stack, the console alphabetically lists input parameters by their logical ID.

To override this default ordering, you can use the interface metadata key. For example, you could group all parameters related to virtual private cloud (VPC) so they aren't scattered throughout an alphabetical list.

**Metadata example**:

```yaml
Metadata:
  Instances:
    Description: "Information about the instances"
  Databases:
    Description: "Information about the databases"
```

```json
"Metadata": {
  "Instances": {
    "Description": "Information about the instances"
  },
  "Databases": {
    "Description": "Information about the databases"
  }
}
```

### Parameters

Parameters are values to pass to your template at runtime (when you create or update a stack). They enable you to input custom values to your template each time you create or update a stack.

You can refer to parameters from the Resources and Outputs sections of the template.

**Parameters example**:

The example below declares a parameter named `InstanceTypeParameter`. This parameter lets you specify the Amazon EC2 instance type for the stack to use when you create or update the stack.

Note that `InstanceTypeParameter` has a default value of t2.micro. This is the value that AWS CloudFormation uses to provision the stack unless another value is provided.

```yaml
Parameters:
  InstanceTypeParameter:
    Type: String
    Default: t2.micro
    AllowedValues:
      - t2.micro
      - m1.small
      - m1.large
    Description: Enter t2.micro, m1.small, or m1.large. Default is t2.micro.
```

```json
"Parameters": {  
  "InstanceTypeParameter": {
    "Type" : "String",
    "Default" : "t2.micro",
    "AllowedValues" : ["t2.micro", "m1.small", "m1.large"],
    "Description" : "Enter t2.micro, m1.small, or m1.large. Default is t2.micro.",
  }
}
```

### Mappings

Mappings are collections of key value pairs that specify conditional parameter values, similar to a lookup table.

For example, say you want to set values based on a Region. You can create a mapping that uses the Region name as a key and contains values for each specific Region. You use the `Fn::FindInMap` intrinsic function to retrieve values in a map.

**Mappings example**:

```yaml
Mappings:
  RegionMap:
    us-east-1:
      "HVM64": "ami-Off8a91507f77f867"
    us-west-1:
      "HVM64": "ami-0bdb828fd58c52235"
    eu-west-1:
      "HVM64": "ami-047bb4163c506cd98"
    ap-southeast-1:
      "HVM64": "ami-08569b978cc4dfa10"
    ap-northeast-1:
      "HVM64": "ami-06cd52961ce9f0d85"
```

```json
"Mappings": {
  "RegionMap": {
    "us-east-1": {
      "HVM64": "ami-Off8a91507f77f867"
    },
    "us-west-1": {
      "HVM64": "ami-0bdb828fd58c52235"
    },
    "eu-west-1": {
      "HVM64": "ami-047bb4163c506cd98"
    },
    "ap-southeast-1": {
      "HVM64": "ami-08569b978cc4dfa10"
    },
    "ap-northeast-1": {
      "HVM64": "ami-06cd52961ce9f0d85"
    },
  }
}
```

### Conditions

Conditions are statements that define the circumstances under which entities are created or configured. For example, you could conditionally create a resource that depends on whether the stack is for a production or test environment.

During a stack update, you cannot update conditions by themselves. You can update conditions only when you include changes that add, modify, or delete resources.

**Conditions example**:

```yaml
Conditions:
  CreateProdResources: !Equals [!Ref EnvType, prod]
```

```json
"Conditions": {
  "CreateProdResources": {
    "Fn::Equals": [{"Ref": "EnvType"}, "prod"]
  }
}
```

### Transform

The transform section specified one or more macros that AWS CloudFormation uses to process your template.

AWS CloudFormation also supports the AWS::Serverless and AWS::Include transforms, which are macros hosted by AWS CloudFormation.

The AWS::Serverless transform specifies the version of the AWS Serverless Application Model (AWS SAM) to use.

The AWS::Include transform works w/ template snippets that are stored separately from the main AWS CloudFormation template.

**Transform example**:

```yaml
Transform: "AWs::Serverless-2016-10-31"

Transform:
  Name: 'AWS::Include'
  Parameters:
    Location: 's3://MyAmazonS3BucketName/MyFileName.yaml'
```

```json
"Transform" : "AWS::Serverless-2016-10-31",
{
  "Transform": {
    "Name" : "AWS::Include",
    "Parameters" : {
      "Location": "s3://MyAmazonS3BucketName/MyFileName.json"
    }
  }
}
```

### Resources

The resources section is the only mandatory section in an AWS CloudFormation template. It specifies the stack resources and their properties, such as Amazon Elastic Compute Cloud (Amazon EC2) instance or an Amazon Simple Storage Service (Amazon S3) bucket.

```yaml
Resources:
  MyEC2Instance:
    Type: "AWS::EC2::Instance"
    Properties:
      ImageId: "ami-Off8a91507f77f867"
```

```json
"Resources" :
  "MyEC2Instance": {
    "Type" : "AWS::EC2::Instance",
    "Properties" : {
      "ImageId": "ami-0ff8a91507f77f867"
    }
  }
```

### Outputs

The optional outputs section declares output values that you can import into other stacks. You can create cross-stock references, return in response to describe stack calls, or view on the AWS CloudFormation console.

For example, you can output the Amazon S3 bucket name for a stack to make the bucket easier to find.

**Outputs example**:

```yaml
Outputs:
  BackupLoadBalancerDNSName:
    Description: The DNSName of the backup load balancer
    Value: !GetAtT BackupLoadBalancer.DNSName
    Condition: CreateProdResources
    InstanceID:
      Description: The Instance ID
      Value: !Ref EC2Instance
```

```json
"Outputs": {
  "BackupLoadBalancerDNSName": {
    "Description": "The DNSName of the backup load balancer",
    "Value": { "Fn::GetAtt" : ["BackupLoadBalancer", "DNSName"]},
    "Condition" : "CreateProdResources"
  },
  "InstanceID": {
    "Description": "The Instance ID",
    "Value": { "Ref": "EC2Instance" }
  }
}
```

