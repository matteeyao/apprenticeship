# CloudFormation Basics

You can use CloudFormation to treat your infrastructure as code. It gives you a way to model a collection of related AWS and third-party resources, provision them quickly and consistently, and manage them throughout their life-cycles.

You define your AWS resources in a structured text format, either YAML or JSON, called a **CloudFormation template**. Then you can create a **CloudFormation stack** in AWS, which contains the resources created. You can then manage these resources by updating the template.

CloudFormation tracks what changes need to be performed and makes all the changes while keeping your resources in a consistent state. CloudFormation can also create **Change Sets** for approval before making the changes, if you choose.

> By treating infrastructure as code, CloudFormation gives you a way to:
>
> * Model a collection of related AWS and third-party resources
>
> * Provision them quickly and consistently
>
> * Manage them throughout their life-cycles

## What problem does CloudFormation solve?

CloudFormation can help you manage your AWS resources, especially resources that depend on each other. You can use CloudFormation to group your resources into **stacks**, using declarative **templates**. CloudFormation can also help you manage creating, updating, and deleting the resources within a stack. You can create resources in parallel, if possible, or create them in specific orders, if they depend on each other.

## What are the benefits of CloudFormation?

CloudFormation benefits include the following:

> ### Automate best practices
>
> With CloudFormation, you can apply DevOps and GitOps best practices using widely adopted processes such as starting with a Git repository and deploying through a continuous integration and continuous delivery (CI/CD) pipeline. You can also simplify auditing changes and trigger automated deployments with pipeline integrations such as GitHub Actions and AWS CodePipeline.

> ### Scale your infrastructure worldwide
>
> Manage resource scaling by sharing CloudFormation templates for use across your organization to meet safety, compliance, and configuration standards across all AWS accounts and Regions. Templates and parameters help simplify scaling so you can share best practices and company policies. Additionally, you can use CloudFormation StackSets to create, update, or delete stacks across multiple AWS accounts and Regions with a single operation. 

> ### Integrate w/ other AWS services
>
> To further automate resource management across your organization, you can integrate CloudFormation with other AWS services, including AWS Identity and Access Management (IAM) for access control, AWS Config for compliance, and AWS Service Catalog for turnkey application distribution and additional governance controls. Integrations with AWS CodePipeline and other builder tools give you the ability to implement the latest DevOps best practices and improve automation, testing, and controls.

> ### Manage third-party and private resources
>
> Model, provision, and manage third-party application resources (such as monitoring, team productivity, incident management, CI/CD, and version control applications) alongside your AWS resources. Use the open source CloudFormation CLI to build your own CloudFormation resource providers (native AWS types published as open source).

> ### Extend CloudFormation w/ the community
>
> The CloudFormation GitHub organization offers open source projects that extend CloudFormation capabilities. You can use the CloudFormation registry and CloudFormation CLI to define and create resource providers to automate the creation of resources safely and systematically. Using CloudFormation GitHub projects, you can do things like check CloudFormation templates for policy compliance (using cfn-guard), or validate use of best practices (using cfn-lint).

## How can I architect a cloud solution using CloudFormation?

> Using CloudFormation, you can manage all your infrastructure with code.

![Fig. 1 Using CloudFormation in your Architecture](../../../../img/SAA-CO2/high-availability-architecture/cloudformation/diag01.png)

Since your infrastructure (your AWS resources) are created with code, you should manage it as code.

Different users can create CloudFormation templates and submit them to your code repository. After code reviews, the code is approved and merged into your main branch. This merging initiates a build process that will create your AWS resources.

## How can I use CloudFormation?

> ### Single Devs - Avoid costs
>
> CloudFormation can help you quickly create and destroy a group of related resources, which is useful when you are learning about a new AWS service. You can quickly spin down a stack with all its resources when you are not using it and re-create it later. As you transition into using the services in production, you can start from those templates and scale up as needed.

> ### Enterprise - Infrastructure as code
>
> Many companies use CloudFormation to manage all of their AWS resources, with CI/CD pipelines creating the stacks from code. Some companies even manage resources outside AWS using CloudFormation.

> ### Disaster Recovery
>
> If you create your infrastructure with CloudFormation, you can quickly re-create it in a different Region or account, enabling disaster recovery and business continuity.

## What else should I be aware of when using CloudFormation?

One important issue is how to create the CloudFormation stacks. You can create them manually on the console, but a better approach is to create an integration pipeline. You can create this so that merging changes to your templates into the main branch creates or modifies the stacks, then use your standard code review practices to manage the templates.

Another issue to keep in mind, when you get to specific resources, is how the lifecycle of each resource is managed. For example, if a change to a resource requires replacing the resource, you should be more careful when using it while it is changing. 

Finally, manually updating resources that belong to a CloudFormation stack is strongly discouraged. Do not make changes to stack resources outside of CloudFormation.

## How much does CloudFormation cost?

The cost structure of CloudFormation is simple. CloudFormation is free for managing AWS resources. You are only charged for the resources you create and the API calls that CloudFormation performs on your behalf.

## Additional CloudFormation Overview

![Fig. 2 CloudFormation Overview](../../../../img/SAA-CO2/high-availability-architecture/cloudformation/diagram.png)

> * Tool that allows us to **create**, **update**, and **delete** infrastructure as code
>
> * Is a way of completely scripting your cloud environment
>
> * Quick Start is a bunch of CloudFormation templates already built by AWS Solutions Architects allowing you to create complex environments very quickly.

## CloudFormation Template

> * All templates must contain at least **one resource** e.g. **EC2**, **S3**, **IAM**, **Lambda**

The **Parameters** section of the CloudFormation template allows us to add fields where we would have to add more information. For example, that could be what size of EC2 instance that we want to create, the number of availability zones to use, etc.

The **Mappings** section tells the CloudFormation template which region to create the resources. We also have **conditions**, which allow us to set decisions in our template, so we can create the condition, and then if that parameter is equal to the condition, then we can create the resource. **Conditions** are used w/ the resources that are used inside of the **CloudFormation** template.

The **Outputs** section give us some information back, such as a `Subnet ID`, `Instance ID`, etc.

CloudFormation takes the template w/ the resources, the parameters, the mappings, the outputs, etc. and creates a stack containing all of the logical resources that are outlined in the CloudFormation template, and then CloudFormation takes those logical resources from that stack and creates physical resources in an AWS account. So if our template called for an EC2 instance and an RDS instance, then the stack w/ the logical resources would create the physical instances inside our AWS account.

You can also use CloudFormation to update templates. CloudFormation would update our physical environment to match the logical environment for that updated stack, and then when you delete a stack, the logical resources are deleted, and then the physical environment is also deleted.

> * CloudFormation allows us to design and automate our infrastructure as code, so we can script our whole cloud environment.

## Design Resilient Architectures ▶︎ CloudFormation

* Declarative programming language for deploying AWS resources.

* Uses templates and stacks to provision resources.

* Create, update, and delete a set of resources as a single unit (stack).

* Template

  * Basic definition of resources to create

  * JSON text file

* Stack

  * Collection of AWS resources

## Learning Summary

**AWS CloudFormation** provides a common language for you to describe and provision all the infrastructure resources in your cloud environment. CloudFormation allows you to use a simple text file to model and provision, in an automated and secure manner, all the resources needed for your applications across all regions and accounts. This file serves as the single source of truth for your cloud environment. AWS CloudFormation is available at no additional charge, and you pay only for the AWS resources needed to run your applications.

* Although the use of CloudFormation service is free, you have to pay the AWS resources that you created.
