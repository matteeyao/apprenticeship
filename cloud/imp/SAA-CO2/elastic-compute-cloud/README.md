# EC2

1. A company uses Reserved Instances to run its data-processing workload. The nightly job typically takes 7 hours to run and must finish within a 10-hour time window. The company anticipates temporary increases in demand at the end of each month that will cause the job to run over the time limit w/ the capacity of the current resources. Once started, the processing job canno be interrupted before completion. The company wants to implement a solution that would allow it to provide increased capacity as cost-effectively as possible. What should a solutions architect do to accomplish this?

**A) Deploy On-Demand Instances during periods of high demand.**

B) Create a second Amazon EC2 reservation for additional instances.

C) Deploy Spot Instances during periods of high demand.

D) Increase the instance size of the instances in the Amazon EC2 reservation to support the increased workload.

**Explanation**: Spot Instances are not suitable for jobs that cannot be interrupted or disrupted.

2. A company has many applications on Amazon EC2 instances running in Auto Scaling groups. Company policy requires that the data on the attached Amazon Elastic Block Store (Amazon EBS) volumes will be retained.

Which action will meet these requirements w/o impacting performance?

**Disable `DeleteOnTermination` attribute for the Amazon EBS volumes**

**Explanation**: Health check will not affect whether the EBS volume will be retained.

3. You wish to deploy a microservices-based application w/o the operational overhead of managing infrastructure. This solution needs to accommodate rapid changes in the volume of requests. What do you do?

[ ] Run the microservices in containers using AWS Elastic Beanstalk

[x] Run the microservices in AWS Lambda behind an API Gateway

[ ] Run the microservices on Amazon EC2 instances in an Auto Scaling group

[ ] Run the microservices in containers using Amazon Elastic Container Service (Amazon ECS)

**Explanation**: "w/o... managing infrastructure" means staying away from EC2. AWS Elastic Beanstalk, EC2 instances in an Auto Scaling group, and AMZN Elastic Container Service all require dealing w/ EC2 services. AWS Lambda behind an API Gateway is serverless. AWS Elastic Beanstalk spins up a CloudFormation template which ultimately spins up a set of EC2 instances, which will require some maintenance, more than AWS Lambda behind an API Gateway.
