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

## What is ECS?

![Fig. 1 Elastic COntainer Service diagram](../../../../img/aws/serverless/ecs/elastic-container-service.png)

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

![Fig. 2 ECS Security diagram](../../../../img/aws/serverless/ecs/ecs-security.png)

The most important concept you'll need to understand for the exam are instance roles vs. task roles.

So first, on the left side of the diagram, you see an EC2 instance w/ a number of tasks running inside. We have a role assigned to that EC2 instance w/ a number of policies applied to that role. When we deploy containers in this fashion, the effective policies will be applied to all tasks running in that EC2 instance. So for example, if the role grants us access to an S3 bucket, for example, all tasks running on that instance will have access to that S3 bucket via the role.

Now from a security perspective, this is not ideal b/c we want to follow what's called the Least Privilege approach to security. We only want to grant tasks access to the resources that they need. Not every task in our workload is going to need access to S3. So this can prove to be a security concern. For this reason, we have task roles available (right side of the diagram). Task roles allow us to have much more granular control over the permissions that our tasks get.

Task roles will apply a policy on a per task basis. So you see each of these 3 tasks here are assigned a role. Our first task receives role A. Role A only has access to S3. Our second task gets role B. This role might allow access to both S3 and DynamoDB and our third task gets role C. In this case, it'll only have access to DynamoDB. This way, we respect the Least Privilege approach.
