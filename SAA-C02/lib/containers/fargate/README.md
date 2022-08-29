# Fargate

1. A new online banking platform has been re-designed to have a microservices architecture in which complex applications are decomposed into smaller, independent services. The new platform is using Docker considering that application containers are optimal for running small, decoupled services. The new solution should remove the need to provision and manage servers, let you specify and pay for resources per application, and improve security through application isolation by design.

Which of the following is the MOST suitable service to use to migrate this new platform to AWS?

[ ] AWS Fargate

[ ] Amazon EKS

**Explanation**: **AWS Fargate** is a serverless compute engine for containers that works with both Amazon Elastic Container Service (ECS) and Amazon Elastic Kubernetes Service (EKS). Fargate makes it easy for you to focus on building your applications. Fargate removes the need to provision and manage servers, lets you specify and pay for resources per application, and improves security through application isolation by design.

Fargate allocates the right amount of compute, eliminating the need to choose instances and scale cluster capacity. You only pay for the resources required to run your containers, so there is no over-provisioning and paying for additional servers. Fargate runs each task or pod in its own kernel providing the tasks and pods their own isolated compute environment. This enables your application to have workload isolation and improved security by design. This is why customers such as Vanguard, Accenture, Foursquare, and Ancestry have chosen to run their mission-critical applications on Fargate.

![Fig. 1 Fargate Overview](../../../../img/SAA-CO2/serverless/fargate/fig01.png)

Hence, the correct answer is: **AWS Fargate**.

> **Amazon EKS** is incorrect because this is more suitable to run the Kubernetes management infrastructure and not Docker. It does not remove the need to provision and manage servers nor let you specify and pay for resources per application, unlike AWS Fargate.

<br />

2. A company is deploying a new application that will consist of an application layer and an online transaction processing (OLTP) relational database. The application must be available at all times. However, the application will have periods of inactivity. The company wants to pay the minimum for compute costs during these idle periods.

Which solution meets these requirements MOST cost-effectively?

[ ] Deploy the application on Amazon EC2 instances in an Auto Scaling group behind an Application Load Balancer. Use Amazon RDS for MySQL for the database. → `Incorrect. With this solution, at least one instance and a database will run during the periods of inactivity. This solution does not minimize cost during the periods of inactivity. This solution is not the most cost-effective option.`

[ ] Run the application in containers w/ Amazon ECS on AWS Fargate. Use Amazon Aurora Serverless for the database. → `Correct. When Amazon ECS uses Fargate for compute, it incurs no costs when the application is idle. Aurora Serverless also incurs no compute costs when it is idle.`

<br />
