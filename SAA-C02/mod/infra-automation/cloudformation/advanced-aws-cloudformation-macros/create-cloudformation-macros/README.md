# Creating AWS CloudFormation Macros

## AWS CloudFormation Macros

From a developer's perspective, AWS CloudFormation Macros are AWS Lambda functions that transform a fragment of your AWS CloudFormation template. For example, the macro inputs a JSON representation of your template (or a piece of it plus metadata) and returns another JSON fragment (plus metadata). AWS then inserts this new JSON fragment instead of the original one.

Notice that you can define AWS Cloudformation templates as YAML, but they will ultimately be processed into JSON. 
