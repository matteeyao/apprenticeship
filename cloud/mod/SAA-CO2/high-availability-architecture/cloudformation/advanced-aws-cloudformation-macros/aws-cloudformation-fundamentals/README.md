# AWS CloudFormation Fundamentals

AWS CloudFormation is a service that helps you model and provision your Amazon Web Services (AWS) resources. You spend less time managing those resources and more time focusing on your applications that run in AWS. You can also use it to programmatically develop your infrastructure using a software development lifecycle such as Git, tracking, or auditable. AWS CloudFormation enables you to define your *infrastructure as code (IaC)*.

## Three components of AWS CloudFormation

> ### Resources
>
> **Resources** are any of the objects you create, such as Amazon Simple Storage Service (Amazon S3) buckets, Amazon DynamoDB, Amazon Simple Queue Service (Amazon SQS) queues, or Amazon Elastic Compute Cloud (Amazon EC2) instances.

> ### Templates
>
> **Templates** are JSON or YAML files that define the characteristics of stack parameters, mappings, resource properties, and output values.

> ### Stack
>
> **Stacks** are a grouping of related AWS resources, which are created by AWS CloudFormation templates. Whenever you create a stack, AWS CloudFormation provisions the resources described in your template, such as mappings or resource properties.

AWS CloudFormation helps you provision your **resources**, which are grouped together into a **stack**, by means of a **template**.

## Benefits

* Allows you to version control your infrastructure.

* Frees you to focus on application logic, as opposed to time spent managing the infrastructure.

* Makes it possible to experiment quickly.

## Common use cases

* Quickly replicate your infrastructure when the application needs more availability.

* Simplify the creation of a set of base resources for experimenting or deploying an application or solution.

* Implement a disaster recovery plan.

* Control and track changes to your infrastructure.
