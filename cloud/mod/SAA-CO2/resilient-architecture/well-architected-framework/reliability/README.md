# III. Reliability Pillar

> Ability to recover from infrastructure or service disruptions, acquiring resources to meet demand and mitigating disruptions.

The Reliability pillar encompasses the ability of a workload to perform its intended function correctly and consistently when it's expected to. This includes the ability to operate and test the workload through its total lifecycle.

> **High Availability, Fault Tolerance, and Disaster Recovery**
>
> 1. High Availability
>
>   * Minimize outages
>
> 2. Fault Tolerance
>
>   * Operate through failures
>
> 3. Disaster Recovery
>
>   * Recover when high availability and fault tolerance do not work

## High Availability

*High Availability* is way of designing your systems to have the ability to keep your systems up and running and providing services often as possible. It is designed so that if a system component fails, then that failure can be replaced or fixed as quickly as possible. So it maximizes your systems online time, but *High Availability* does not mean that it will stop failures and it also doesn't mean that there will be no downtime or outages. What *High Availability* does is respond when there is a failure and fix that failure as soon as possible so that system can be brought back into service.

So what is an example of *High Availability*? Let's say that we have an application running on a single server inside AWS. This server is used by employees to complete their job. If this server goes down, then the employees cannot work b/c the server is now down and currently experiencing an outage. But if you design this architecture to be *Highly Available*, you could quickly spin up a new VM to fail over to or you could run two servers for this application-one in active and one in standby mode. So if one server goes down, you can fail over to the second server to server your employees.

W/ *High Availability*, some downtime can be expected depending on your design. So the goal of *High Availability* is to reduce outages and staying operational fast w/ automatic recovery.

## Fault Tolerance

Similar to *High Availability*, but *Fault Tolerance* is the actual ability of a system to keep operating in the event of a failure. So, one ore more of your system's components fail, but that system is able to continue on w/ those faults failing. So a fault tolerant design must continue to operate.

If we're designing for *Fault Tolerance*, we would have two active servers serving the one application. If your one server goes down, then that second server is already active and will continue to serve the employees. There's actually no downtime in this situation. So *Fault Tolerant* designs operate to minimize failures and continue to operate through those failures. And these system designs are usually going to be more expensive than your *High Availability* designs.

## Disaster Recovery

Slightly different from *Fault Tolerance* and *High Availability* since *Fault Tolerance* and *High Availability* focus on designing systems to operate through a disaster, *Disaster Recovery* is all about what we need to plan for and also what we need to do in the event of a disaster. Having a disaster recovery plan is crucial b/c the worst time to recover from a disaster is in the middle of that disaster. *Disaster Recovery* requires pre-planning and it also needs steps to complete the *Disaster Recovery* process so that when that Disaster occurs, you are already set w/ a plan to recover your systems as quickly as possible. Maybe you have an onsite backup to switch your environment to. Maybe use AWS as your backup site and have a CloudFormation template ready to go to provision your environment inside AWS after the disaster. It's essential to have backups of your environment stored offsite in the cloud, but definitely not stored in the same building as your systems, b/c if that building is damaged, then so are your backups. So you need to have a plan to protect your data and stor backups elsewhere.

It is best practice to run *Disaster Recovery* exercises to practice this process so that in a real disaster, the process goes smoothly. We have to protect our systems.

## Design Principles

There are five design principles for reliability in the cloud:

* Automatically recover from failure

* Test recovery procedures

* Scale horizontally to increase aggregate workload availability

* Stop guessing capacity

* Manage change in automation

## Best Practices

Before building any system, foundational requirements that influence reliability should be in place. For example, you must have sufficient network bandwidth to your data center. These requirements are sometimes neglected (b/c they are beyond a single project's scope). W/ AWS, however, most of the foundational requirements are already incorporated or can be addressed as needed.

The cloud is designed to be nearly limitless, so it's the responsibility of AWS to satisfy the requirement for sufficient networking and compute capacity, leaving you free to change resource size and allocations on demand.

A reliable workload starts w/ upfront design decision for both software and infrastructure. Your architecture choices will impact your workload behavior across all six AWS Well-Architected pillars. For reliability, there are specific patterns you must follow, such as loosely coupled dependencies, graceful degradation, and limiting retries

Changes to your workload or its environment must be anticipated and accommodated to achieve reliable operation of the workload. Changes include those imposed on your workload, like a spikes in demand, as well as those from within such as feature deployments and security patches.

Low-level hardware component failures are something to be dealt w/ every day in an on-premises data center. In the cloud, however, these are often abstracted away. Regardless of your cloud provider, there is the potential for failures to impact your workload. You must therefore take steps to implement resiliency in your workload, such as fault isolation, automated failover to healthy resources, and a disaster recovery strategy.
