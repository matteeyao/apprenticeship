# AWS Proton

1. A technology company has a suite of container-based web applications and serverless solutions that are hosted in AWS. The Solutions Architect must define a standard infrastructure that will be used across development teams and applications. There are application-specific resources too that change frequently, especially during the early stages of application development. Developers must be able to add supplemental resources to their applications, which are beyond what the architects predefined in the system environments and service templates.

Which of the following should be implemented to satisfy this requirement?

[ ] Use the Amazon Elastic Container Service (ECS) Anywhere service for deploying container applications and serverless solutions. Configure Prometheus metrics collection on the ECS cluster and use Amazon Managed Service for Prometheus for monitoring frequently-changing resources

[ ] Use the Amazon EKS Anywhere service for deploying container applications and serverless solutions. Create a service instance for each application-specific resource.

[ ] Set up AWS Proton for deploying container applications and serverless solutions. Create components from the AWS Proton console and attach them to their respective service instance.

[ ] Set up AWS Control Tower to automate container-based application deployments. Use AWS Config for application-specific resources that change frequently.

**Explanation**: AWS Proton allows you to deploy any serverless or container-based application with increased efficiency, consistency, and control. You can define infrastructure standards and effective continuous delivery pipelines for your organization. Proton breaks down the infrastructure into environment and service (“infrastructure as code” templates).

As a developer, you select a standardized service template that AWS Proton uses to create a service that deploys and manages your application in a service instance. An AWS Proton service is an instantiation of a service template, which normally includes several service instances and a pipeline.

![Fig. 1 AWS Proton](../../../../../img/SAA-CO2/serverless/ecs/proton/fig01.jpeg)

The diagram above displays the high-level overview of a simple AWS Proton workflow.

In AWS Proton administrators define standard infrastructure that is used across development teams and applications. However, development teams might need to include additional resources for their specific use cases, like Amazon Simple Queue Service (Amazon SQS) queues or Amazon DynamoDB tables. These application-specific resources might change frequently, particularly during early application development. Maintaining these frequent changes in administrator-authored templates might be hard to manage and scale—administrators would need to maintain many more templates without real administrator added value. The alternative—letting application developers author templates for their applications—isn't ideal either, because it takes away administrators' ability to standardize the main architecture components, like AWS Fargate tasks. This is where components come in.

With a component, a developer can add supplemental resources to their application, above and beyond what administrators defined in environment and service templates. The developer then attaches the component to a service instance. AWS Proton provisions infrastructure resources defined by the component just like it provisions resources for environments and service instances.

Hence, the correct answer is: **Set up AWS Proton for deploying container applications and serverless solutions. Create components from the AWS Proton console and attach them to their respective service instance.**

The option that says: **Use the Amazon EKS Anywhere service for deploying container applications and serverless solutions. Create a service instance for each application-specific resource** is incorrect. Amazon EKS Anywhere just allows you to manage a Kubernetes cluster on external environments that are supported by AWS. It is better to use AWS Proton with custom Components that can be attached to the different service instances of the company's application suite.

The option that says: **Set up AWS Control Tower to automate container-based application deployments. Use AWS Config for application-specific resources that change frequently** is incorrect. AWS Control Tower is used to simplify the creation of new accounts with preconfigured constraints. It isn't used to automate application deployments. Moreover, AWS Config is commonly used for monitoring the changes of AWS resources and not the custom resources for serverless or container-based applications in AWS. A combination of AWS Proton and Components is the most suitable solution for this scenario.

The option that says: **Use the Amazon Elastic Container Service (ECS) Anywhere service for deploying container applications and serverless solutions. Configure Prometheus metrics collection on the ECS cluster and use Amazon Managed Service for Prometheus for monitoring frequently-changing resources** is incorrect. The Amazon Managed Service for Prometheus is only a Prometheus-compatible monitoring and alerting service that makes it easy to monitor containerized applications and infrastructure at scale. It is not capable of tracking or maintaining your application-specific resources that change frequently.
