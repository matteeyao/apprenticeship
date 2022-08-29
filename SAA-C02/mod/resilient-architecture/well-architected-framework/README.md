# The 6 Pillars of the AWS Well-Architected Framework

> * A framework for ensuring infrastructures are:
>
>   ✓ Operational excellence
>
>   ✓ Security
>
>   ✓ Reliability
>
>   ✓ Performance efficiency
>
>   ✓ Cost optimization

> The AWS Well-Architected Framework is AWS's best practices and core strategies for architecting systems in the cloud, helping you design, build, and operate reliable, secure, efficient, and cost-effective systems.

Creating a software system is a lot like constructing a building. If the foundation is not solid, structural problems can undermine the integrity and function of the building.

When building technology solutions on Amazon Web Services (AWS), if you neglect the six pillars of operational excellence, security, reliability, performance efficiency, cost optimization, and sustainability, it can become challenging to build a system that delivers on your expectations and requirements.

Incorporating these pillars into your architecture helps produce stable and efficient systems. This allows you to focus on the other aspects of design, such as functional requirements.

The **AWS Well-Architected Framework** helps cloud architects build the most secure, high-performing, resilient, and efficient infrastructure possible for their applications. The framework provides a consistent approach for customers and AWS partners to evaluate architectures, and provides guidance to implement designs that scale w/ your application needs over time.

In this post, we provide an overview of the Well-Architected Framework's six pillars and explore design principles and best practices. You can find more details-including definitions, FAQs, and resources-in each pillar's whitepaper we link to below.

## AWS Well-Architected Framework Tool

> Review workloads and compare the state of workload to architectural best practices.

## General Design Principles

> 1. Stop guessing capacity
>
>   * Use auto scaling and have your supply actually meet demand
>
> 2. Test systems at production scale
>
> 3. Automate your architecture
>
> 4. Allow for evolutionary changes
>
> 5. Improve through game days
>
>   * This allows us to spin up test environments using CloudFormation to see how our systems would run at production scale.

## 1. Operational Excellence

> The ability to run and monitor systems to deliver business value and continually improve supporting processes and procedures.
>
> * Prepare
>
> * Operate
>
> * Evolve

The Operational Excellence pillar includes the ability to support development and run workloads effectively, gain insight into their operation, and continuously improve supporting processes and procedures to delivery business value.

### Design Principles

There are five design principles for operational excellence in the cloud:

* Perform operations with code

* Annotate documentation

* Make frequent, small, reversible changes

* Refine operations procedures frequently

* Anticipate failure

* Learn from all operational failures

### AWS Services Supporting Operational Excellence

* AWS Config ▶︎ Tracks resources such as EBS volumes and EC2 instances. Verifies that new resources conform to your resource rules.

* AWS CloudFormation ▶︎ Converts JSON and YAML templates into infrastructure and resources.

* AWS Cloud Trail ▶︎ Logs API calls

* VPC Flow Logs ▶︎ Log network traffic

* AWS Inspector ▶︎ Checks EC2 instances for security vulnerabilities

* AWS Trusted Advisor ▶︎ Checks accounts for best practices on security, reliability, performance, cost, and service limits.

* CloudWatch logs ▶︎ stores log files from EC2 instances, from CloudTrail, and from Lambda

### Best Practices

> * IAM roles are easier and safer than keys and passwords
>
> * Monitor metrics across the system
>
> * Automate responses to metrics where appropriate
>
> * Provide alerts for anomalous conditions

Operations teams need to understand their business and customer needs so they can support business outcomes. Ops creates and uses procedures to respond to operational events, and validates their effectiveness to support business needs. Ops also collects metrics that are used to measure the achievement of desired business outcomes.

Everything continues to change-your business context, business priorities, and customer needs. It's important to design operations to support evolution over time in response to change, and to incorporate lessons learned through performance.

## 2. Security

> Ability to protect your information, systems, and assets through risk assessment and mitigation.

The Security pillar includes the ability to protect data, systems, and assets to take advantage of cloud technologies to improve your security.

### Design Principles

There are seven design principles for security in the cloud:

* Implement a strong identity foundation

* Enable traceability

* Apply security at all layers

* Automate security best practices

* Protect data in transit and at rest

* Keep people away from data

* Prepare for security events

### Best Practices

Before you architect any workload, you need to put in place practices that influence security. You'll want to control who can do what. In addition, you want to be able to identify security incidents, protect your systems and services, and maintain the confidentiality and integrity of data through data protection.

You should have a well-defined and practiced process for responding to security incidents. These tools and techniques are important b/c they support objectives such as preventing financial loss or complying w/ regulatory obligations.

The *AWS Shared Responsibility Model* enables organizations that adopt the cloud to achieve their security and compliance goals. B/c AWS physically secures the infrastructure that supports our cloud services, as an AWS customer, you can focus on using services to accomplish your goals. The AWS Cloud also provides greater access to security data and an automated approach to responding to security events.

## 6. Sustainability

The discipline of sustainability addresses the long-term environmental, economic, and societal impact of your business activities.

### Design Principles

There are six design principles for sustainability in the cloud:

* Understand your impact

* Establish sustainability goals

* Maximize utilization

* Anticipate and adopt new, more efficient hardware and software offerings

* Use managed services

* Reduce the downstream impact of your cloud workloads

### Best Practice

![Fig. 1 Cost Optimization Best Practices](img/SAA-CO2/Design Resilient Architectures/Well-Architected Framework/cost-optimization-best-practices.png)

Choose AWS Regions where you will implement workloads based on your business requirements and sustainability goals.

User behavior patterns can help you identify improvements to meet sustainability goals. For example, scale infrastructure down when not needed, position resources to limit the network required for users to consume them, and remove unused assets.

Implement software and architecture patterns to perform load smoothing and maintain consistent high utilization of deployed resources. Understand the performance of your workload components, and optimize the components that consume the most resources.

Analyze data patterns to implement data mgmt practices that reduce the provisioned storage required to support your workload. Use lifecycle capabilities to move data to more efficient, less performant storage when requirements decrease, and delete data that's not longer required.

Analyze hardware patterns to identify opportunities that reduce workload sustainability impact by making changes, such as updating systems to gain performance efficiencies and manage sustainability impacts. Use automation to manage the lifecycle of your development and test environments, and use managed device farms for testing.
