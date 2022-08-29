# Elastic Container Service (ECS)

## What are containers and Docker?

> * A **container** is a package that contains an application, libraries, runtime, and tools required to run that application. In other words, it's a bundle that contains an application and all of its dependencies.
>
> * Run on a container engine like **Docker**
>
> * Provides the **isolation** benefits of virtualization w/ less overhead and faster starts than VMs
>
> * Containerized applications are **portable** and offer a consistent environment

Your containers are managed by your container engine and they'll allocate memory and CPU on a per container basis. This will help you constrain the resources consumed by your applications.

> * Package code, configurations, and dependencies into a single object
>
> * Containers share an operating system installed on the server
>
> * Run as resource-isolated processes, ensuring quick, reliable, and consistent deployments, regardless of environment.
>
> * AWS offers infrastructure resources optimized for running containers as well as a set of orchestration services that make it easy for you to build and run containerized applications in production.

> ### Use cases
>
> * Provide process isolation that makes it easy to break apart and run applications as independent components called microservices
>
> * Package batch processing and ETL jobs into containers to start jobs quickly and scale them dynamically in response to demand
>
> * Use containers to quickly scale machine learning models for training and inference and run them close to data sources on any platform.
>
> * Containers let your customers standardize how code is deployed, making it easy to build workflows for applications that run between on-premises and cloud environments.
>
> * Containers make it easy to package entire applications and move them to the cloud w/o needing to make any code changes.
>
> * Containers can be used to build platforms that remove the need for developers to manage infrastructure and standardize how applications are deployed and managed.

## What is ECS?

![Fig. 1 Elastic Container Service diagram](../../../../img/SAA-CO2/elastic-container-service/fig01.png)

> * Managed container **orchestration service**
>
> * Create **clusters** to manage fleets of container deployments
>
> * ECS manages EC2 or Fargate instances
>
> * Schedules containers for optimal placement
>
> * Monitors resource utilization
>
> * Deploy, update, roll back
>
> * FREE ... for real!
>
> * VPC, security groups, EBS volumes
>
> * CloudTrail and CloudWatch

Managed container **orchestration service** that lets you run and scale containerized applications. It eliminates the need for your own orchestration, tooling, or clusters.

ECS will manage your cluster for you and take care of the state of the cluster as well as scheduling, running, and monitoring containers across your cluster.

When we talk about scheduling, what we mean is placing containers optimally within your cluster. You could define rules for CPU and memory requirements for your tasks and the cluster will monitor your resource utilization for you.

ECS will schedule your containers to help find the optimal placement based on rules that you define. W/ ECS, you can run a mix of different types of applications or tasks across your cluster. Now, this is ideal if you have a web application that runs all the time and maybe something like an image processing job that might run intermittently. ECS can schedule and run these together in the same cluster. ECS also monitors your cluster and shows you each application's resource utilization and how much headroom is left for new tasks. ECS allows you to quickly deploy, update, and roll back containers, regardless of whether you're dealing w/ tens or thousands of them, making it easy to run all the different kinds of workloads that make up your application.

The scheduling and orchestration components of ECS are free as well as the clusters, but any EC2 instances or Fargate tests that you spin up, you'll pay for.

ECS also integrates w/ your VPCs, Security groups, and EBS volumes, as well as your Elastic Load Balancers, and, like most other AWS services, there's CloudTrail integration w/ ECS so you can see any of the changes to your cluster while logged into CloudTrail. There's also native support for CloudWatch so that you can get alarmed on state changes in your cluster and you can respond however you like.

## ECS Components

> * **Cluster**
>
>   * Logical collection of ECS resources - either ECS EC2 instances or Fargate instances
>
> * **Task Definition**
>
>   * Defines your application. Similar to a Dockerfile but for running containers in ECS. Can contain multiple containers. So for example, if you have 2 containers that always need to run together, you would place both inside the same Task Definition. Now, within our Task Definition, we have a container definition.
>
> * **Container Definition**
>
>   * Inside a task definition, it defines the individual containers a task uses. Controls CPU and memory allocation and port mappings for our containers.
>
> * **Task**
>
>   * Single running copy of any containers defined by a task definition. Each task is one working copy of an application (e.g., DB and web containers).
>
> * **Service**
>
>   * Allows task definitions to be scaled by adding tasks. Defines minimum and maximum values. So for example, you can define a minimum of one task and a maximum of say 10. Depending upon our auto scaling rules, the number of running tasks will vary between those minimum and maximum values within our service.
>
> * **Registry**
>
>   * Storage for container images (e.g., Elastic Container Registry (ECR) or Docker Hub). Used to download images to create containers.

## Fargate

> * **Serverless** container engine
>
> * Eliminates need to provision and manage servers
>
> * Specify and pay for resources per application
>
> * Works w/ both **ECS** and **EKS**
>
> * Each workload runs in its own kernel
>
> * Isolation and security
>
> * Choose EC2 instead if:
>
>   * Compliance requirements
>
>   * Require broader customization
>
>   * Require GPUs


Fargate is a serverless compute engine for containers that works w/ both ECS and EKS (Elastic Kubernetes service). Fargate makes it easy for you to focus on building your applications.

Fargate removes the need to provision and manage servers and lets you specify and pay for resources on a per application basis.

Fargate will allocate the right amount of compute resources that you need, eliminating the need to choose instances and scale cluster capacity. You only pay for the resources required to run your containers so there's no risk of over-provisioning and paying for additional servers that you won't need.

Fargate runs each task or pod if you're running Kubernetes in its own kernel, providing the tasks and pods w/ their own isolated compute environment. This enables your application to have workload isolation and improves security by design.

So when do I choose EC2 instances over Fargate instances? You might want to choose EC2 instead if you have any kind of strict compliance requirements that necessitate EC2 instances. If your applications require any kind of broader customization than you're able to do on Fargate, since you're not able to access the actual Fargate instance itself, whereas you can, w/ am EC2 instance, or if your applications require access to GPUs.

## EKS

> * Elastic Kubernetes Service
>
> * K8s is **open-source** software that lets you deploy and manage containerized applications at scale
>
> * Same toolset on-premises and in-cloud
>
> * Containers are grouped in **pods**
>
> * Like ECS, supports both EC2 and Fargate
>
> * Why use EKS?
>
>   * Already using K8s
>
>   * Want to migrate to AWS

It serves a similar purpose to ECS, but unlike w/ ECS, EKS lets you use the same tool set on premises and in-cloud. And w/ Kubernetes, your containers are grouped in **pods**. This is roughly analogous to a task in ECS and like ECS, EKS supports both EC2 and Fargate deployment models.

If you're already using Kubernetes, this is the logical choice and if you're already using Kubernetes and have an investment in that ecosystem, but you want to migrate your application workloads to AWS, you'll consider EKS.

## ECR

We briefly discussed registries, both ECR, the Elastic Container Registry and Docker Hub.

> * Managed Docker container registry
>
> * Store, manage, and deploy images
>
> * Integrated w/ ECS and EKS
>
> * Works w/ on-premises deployments
>
> * Highly available
>
> * Integrated w/ **IAM**
>
> * Pay for storage and data transfer

Can work w/ on-premises deployments, just like Docker hub can.

It's designed to be highly available so under the hood it's deployed across multiple AZs within a region.

Integrated w/ **IAM**, so you can create policies to restrict access to your container images.

Now w/ ECR, you pay for storage and data transfer similar to S3.

## ECS + ELB

> * Distribute traffic evenly across tasks in your service
>
> * Supports ALB, NLB, CLB
>
> * Use ALB to route HTTP/HTTPS (layer 7) traffic
>
> * Use NLB or CLB to route TCP (layer 4) traffic
>
> * Supported by both EC2 and Fargate launch types
>
> * ALB allows:
>
>   * Dynamic host port mapping
>
>   * Path-based routing
>
>   * Priority rules
>
> * ALB is recommended over NLB or CLB

Your ECS service can optionally be configured to use ELB to distribute traffic evenly across the tasks in your service.

ECS services support the application load balancer, network load balancer, and classic load balancer load types.

ALBs are used to route HTTP or HTTPS, otherwise known as layer 7 traffic.

Network Load Balancer and Classic Load Balancers can be used to route TCP or Layer 4 traffic, and this is supported by both EC2 and Fargate launch types. Now Application Load Balancers offer several features that make them attractive for use w/ ECS services: ALBs allow your containers to use what's called Dynamic host port mapping so that multiple tasks from the same service are allowed per container instance. ALBs also support what's called path-based routing and priority rules. This allows multiple services to use the same listener port on a single ALB and ALB is recommended over the network or Classic Load Balancers. This lets you take advantage of the latest features unless your service requires a feature that's only available w/ NLB or CLV.

## ECS Security

![Fig. 2 ECS Security diagram](../../../../img/SAA-CO2/elastic-container-service/fig02.png)

The most important concept you'll need to understand for the exam are instance roles vs. task roles.

So first, on the left side of the diagram, you see an EC2 instance w/ a number of tasks running inside. We have a role assigned to that EC2 instance w/ a number of policies applied to that role. When we deploy containers in this fashion, the effective policies will be applied to all tasks running in that EC2 instance. So for example, if the role grants us access to an S3 bucket, for example, all tasks running on that instance will have access to that S3 bucket via the role.

Now from a security perspective, this is not ideal b/c we want to follow what's called the Least Privilege approach to security. We only want to grant tasks access to the resources that they need. Not every task in our workload is going to need access to S3. So this can prove to be a security concern. For this reason, we have task roles available (right side of the diagram). Task roles allow us to have much more granular control over the permissions that our tasks get.

Task roles will apply a policy on a per task basis. So you see each of these 3 tasks here are assigned a role. Our first task receives role A. Role A only has access to S3. Our second task gets role B. This role might allow access to both S3 and DynamoDB and our third task gets role C. In this case, it'll only have access to DynamoDB. This way, we respect the Least Privilege approach.

## Learning Summary

1. A media company is setting up an ECS batch architecture for its image processing application. It will be hosted in an Amazon ECS Cluster with two ECS tasks that will handle image uploads from the users and image processing. The first ECS task will process the user requests, store the image in an S3 input bucket, and push a message to a queue. The second task reads from the queue, parses the message containing the object name, and then downloads the object. Once the image is processed and transformed, it will upload the objects to the S3 output bucket. To complete the architecture, the Solutions Architect must create a queue and the necessary IAM permissions for the ECS tasks.

Which of the following should the Architect do next?

[ ] Launch a new Amazon AppStream 2.0 queue and configure the second ECS task to read from it. Create an IAM role that the ECS tasks can assume in order to get access to the S3 buckets and AppStream 2.0 queue. Declare the IAM Role (`taskRoleArn`) in the task definition.

[ ] Launch a new Amazon Kinesis Data Firehose and configure the second ECS task to read from it. Create an IAM role that the ECS tasks can assume in order to get access to the S3 buckets and Kinesis Data Firehose. Specify the ARN of the IAM Role in the (`taskDefinitionArn`) field of the task definition.

[x] Launch a new Amazon SQS queue and configure the second ECS task to read from it. Create an IAM role that the ECS tasks can assume in order to get access to the S3 buckets and SQS queue. Declare the IAM Role (`taskRoleArn`) in the ask definition.

[ ] Launch a new Amazon MQ queue and configure the second ECS task to read from it. Create an IAM role that the ECS tasks can assume in order to get access to the S3 buckets and Amazon MQ queue. Set the (`EnableTaskIAMRole`) option to true in the task definition.

**Explanation**: Docker containers are particularly suited for batch job workloads. Batch jobs are often short-lived and parallel. You can package your batch processing application into a Docker image so that you can deploy it anywhere, such as in an Amazon ECS task.

Amazon ECS supports batch jobs. You can use Amazon ECS *Run Task* action to run one or more tasks once. The Run Task action starts the ECS task on an instance that meets the task’s requirements including CPU, memory, and ports.

![Fig. 3 ECS Batch Architecture](../../../img/SAA-CO2/elastic-container-service/fig03.png)

For example, you can set up an ECS Batch architecture for an image processing application. You can set up an AWS CloudFormation template that creates an Amazon S3 bucket, an Amazon SQS queue, an Amazon CloudWatch alarm, an ECS cluster, and an ECS task definition. Objects uploaded to the input S3 bucket trigger an event that sends object details to the SQS queue. The ECS task deploys a Docker container that reads from that queue, parses the message containing the object name and then downloads the object. Once transformed it will upload the objects to the S3 output bucket.

By using the SQS queue as the location for all object details, you can take advantage of its scalability and reliability as the queue will automatically scale based on the incoming messages and message retention can be configured. The ECS Cluster will then be able to scale services up or down based on the number of messages in the queue.

You have to create an IAM Role that the ECS task assumes in order to get access to the S3 buckets and SQS queue. Note that the permissions of the IAM role don't specify the S3 bucket ARN for the incoming bucket. This is to avoid a circular dependency issue in the CloudFormation template. You should always make sure to assign the least amount of privileges needed to an IAM role.

Hence, the correct answer is: **Launch a new Amazon SQS queue and configure the second ECS task to read from it. Create an IAM role that the ECS tasks can assume in order to get access to the S3 buckets and SQS queue. Declare the IAM Role (`taskRoleArn`) in the task definition**.

The option that says: **Launch a new Amazon AppStream 2.0 queue and configure the second ECS task to read from it. Create an IAM role that the ECS tasks can assume in order to get access to the S3 buckets and AppStream 2.0 queue. Declare the IAM Role (`taskRoleArn`) in the task definition** is incorrect because Amazon AppStream 2.0 is a fully managed application streaming service and can't be used as a queue. You have to use Amazon SQS instead.

The option that says: **Launch a new Amazon Kinesis Data Firehose and configure the second ECS task to read from it. Create an IAM role that the ECS tasks can assume in order to get access to the S3 buckets and Kinesis Data Firehose. Specify the ARN of the IAM Role in the (`taskDefinitionArn`) field of the task definition** is incorrect because Amazon Kinesis Data Firehose is a fully managed service for delivering real-time streaming data. Although it can stream data to an S3 bucket, it is not suitable to be used as a queue for a batch application in this scenario. In addition, the ARN of the IAM Role should be declared in the `taskRoleArn` and not in the `taskDefinitionArn` field.

The option that says: **Launch a new Amazon MQ queue and configure the second ECS task to read from it. Create an IAM role that the ECS tasks can assume in order to get access to the S3 buckets and Amazon MQ queue. Set the (`EnableTaskIAMRole`) option to true in the task definition** is incorrect because Amazon MQ is primarily used as a managed message broker service and not a queue. The `EnableTaskIAMRole` option is only applicable for Windows-based ECS Tasks that require extra configuration.
