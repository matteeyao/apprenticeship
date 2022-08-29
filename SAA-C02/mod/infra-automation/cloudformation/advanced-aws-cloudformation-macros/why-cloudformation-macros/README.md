# Why Use AWS CloudFormation Macros

## AWS CloudFormation Macros

AWS CloudFormation Macros perform custom processing on AWS CloudFormation templates from simple actions such as find and replace to the transformation of entire templates. AWS CloudFormation helps simplify template authoring by condensing the expression of AWS infrastructure as code and enabling reuse of template components.

Since AWS CloudFormation Macros can arbitrarily transform your template, their applicability is only limited by your imagination (and practicality). You can use macros to define your own domain-specific language (DSL) within AWS CloudFormation. As a result, you can better match it to your organization, although new users will need to learn AWS CloudFormation plus your DSL or macros too.

## Use cases

> ### Enforce standards
>
> The macro can verify that your resources satisfy one or more standards that your organization applies. You could even write one macro to enforce all of them and include it in all templates.
>
> If the resources do not satisfy the standard, the macro would log the specific error and signal a failure. Examples include:
>
> * Verifying that hard drives for all Amazon EC2 instances are encrypted.
>
> * Verifying that all resources are tagged w/ a cost center (from a list of approved cost centers).

> ### Ensure consistency
>
> Macros can also generate resources or properties to all resources to ensure that an organization is following standards. Examples include:
>
> * Adding tags to all resources (see example).
>
> * Adding Amazon CloudWatch alarms for specific resources, depending on the resource type.
>
> * Adding dead-letter queues to SQS queues.
>
> * Encrypting Amazon Elastic Block Store (Amazon EBS) volumes for Amazon EC2 instances.

> ### Simplify templates
>
> You can write macros that enable you to write simpler code. This can generate other code that would be harder or more cumbersome to write in AWS CloudFormation. Examples include:
>
> * Adding tags to all resources (see example).
>
> * Stringing manipulation functions (to make them uppercase or lowercase, for example).
>
> * Generating a list of environment variables in ECS/EKS, where AWS CloudFormation does not yet have a list type for tem.

> ### Communicate w/ external entities
>
> Sometimes you want to use your AWS CloudFormation stack to manage external resources. You can do this w/ macros, although AWS CloudFormation custom resources provide better management. Applications include:
>
> * Verifying that external resources exist.
>
> * Reading values for external resources.
>
> * Creating resources external to AWS CloudFormation (although custom resources would be better here).

> [!NOTE]
>
> AWS CloudFormation custom resources manage the lifecycle of resources outside of what is defined in the AWS CloudFormation template.
>
> AWS CloudFormation Macros transform statements in your template.
