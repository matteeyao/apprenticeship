# Design Resilient Architectures

1. How does AWS provide such amazing cloud computing?

**AWS offers a global infrastructure which is a collection of smaller groupings of infrastructure that is connected by a global high speed network.**

This global infrastructure allows us to design systems that are resilient and highly available. AWS offers globally resilient services, regional resilient services, and also zone resilient services.

2. What is fault tolerance?

**The actual ability of a system to keep operating in the event of a failure.**

Fault tolerance is the actual ability of a system to keep operating in the event of a failure. If one or more of a system's components fail, the system is able to continue operating.

3. What is cost optimization?

**It is the ability to build and operate cost-aware workloads that achieve business outcomes while minimizing costs and allowing your organization to maximize its return on investment.**

Cost optimization is a continual process of refinement and improvement over the span of a workload's lifecycle. These practices help you to build and operate cost-aware workloads that achieve business outcomes while minimizing costs and allowing your organization to maximize its return on investment. Reference: [Cost Optimization](https://docs.aws.amazon.com/wellarchitected/latest/cost-optimization-pillar/cost-optimization.html).

4. What is the Shared Responsibility Model?

**The Shared Responsibility Model is how AWS provides clarity around which areas of systems security are theirs, and which are owned by us, the customers.**

AWS provides us this Shared Responsibility Model so we are clear which elements of the infrastructure it manages and what elements we are responsible for managing.

5. Besides regions and their included Availability Zones, which of the following is another "regional" data center location used for content distribution?

**Edge Location**

An edge location is an AWS data center that is primarily used for content delivery. There are edge locations located in multiple countries around the world.

6. What is a hybrid cloud computing deployment model?

**Using cloud-based resources and on-premises infrastructure together**

A hybrid cloud computing deployment model connects infrastructure and applications between cloud-based resources and existing resources that are not located in the cloud. The most common method of hybrid deployment is between the cloud and existing on-premises infrastructure to extend, and grow, an organization's infrastructure into the cloud while connecting cloud resources to internal system. See [Types of Cloud Computing](https://aws.amazon.com/types-of-cloud-computing/).

7. What best describes the concept of elasticity?

**The ability of a system to increase and decrease in size.**

Elasticity is defined as the ability to both increase and decrease. In architecting applications, this usually refers to the ability of an application to increase and decrease server capacity on demand.

8. What is high availability?

**The ability to keep your systems up and running, providing service as much as possible.**

High Availability (HA) is a way to design your systems to have the ability to keep your systems up and running as much as possible. High availability does not guarantee that the solution will operate through the failure of a component w/o any downtime. Fault-tolerant (FT) architecture allows solutions to operate even if a component is lost. Disaster Recovery (DR) enables the recovery of infrastructure (usually in a different location) following a disaster.

9. What are the main benefits of AWS regions?

[ ] All regions offer the same service at the same prices.

[x] Regions allow you to design applications to conform to specific laws and regulations for specific parts of the world.

[ ] Regions allow you to choose a location in any country in the world.

[x] Regions allow you to place AWS resources in the area of the world closest to your customers that access those resources.

Regions are groupings of AWS resources that are located all around the world. This allows for you, as the architect, to provision AWS resources in areas of the world that make sense for your application needs and where you are serving traffic. In addition, certain companies may be subject to government rules and regulations that require data to be stored in a certain location.

10. What is horizontal scaling?

**A way to achieve the increased need for more capacity by adding more instances.**

Horizontal scaling is designed to keep up w/ the change in your load and the way this works is when your load increases, you add additional instances that brings additional capacity to handle that increase in demand.

11. What are the benefits of an Availability Zone?

[ ] Availability Zones span across multiple regions.

[x] Each Availability Zone is isolated from each other to ensure fault tolerance.

[ ] Availability Zones work independently from a region.

[x] Availability Zones have direct, low latency connections to each other.

Availability Zones are isolated from each other within each specific region to endure a natural disaster or some other outage and work together via low latency connections.

12. What best describes the concept of high availability?

**The ability to keep your systems up and running, providing service as much as possible**

High availability (HA) is a way to design your systems to have the ability to keep your systems up and running as much as possible. High availability does not guarantee that the solution will operate through the failure of a component w/o any downtime. Fault-tolerant (FT) architecture allows solutions to operate even if a component is lost. Disaster recovery (DR) enables the recovery of infrastructure (usually in a different location) following a disaster.

13. What is vertical scaling?

**A way to achieve the increased need for more capacity by re-sizing**

Vertical scaling is a way to achieve the increased for more capacity. So lets say we have an EC2 instance that is a t2.micro and we have noticed that the t2.micro cannot handle the incoming load from our users, and if this load keeps increasing then our users are going to experience delays, crashes, etc. So w/ vertical scaling we can resize this t2.micro to a larger instance size.

14. Which of the following AWS services facilitate the implementation of loosely coupled architectures? (Select two)

[ ] AWS CloudFormation (stands up stacks of components)

[x] Amazon Simple Queue Service

[ ] AWS CloudTrail (logging service)

[x] Elastic Load Balancing

[ ] Amazon Elastic MapReduce (managed Hadoop)
