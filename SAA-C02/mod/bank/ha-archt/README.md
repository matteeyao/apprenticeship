# HA Architecture

1. In discussions about Cloud services the words 'Availability', 'Durability', 'Reliability', and 'Resiliency' are often used. Which term is used to refer to the likelihood that a resource is able to recover from damage or disruption?

**Resiliency**

Resiliency is the ability of a workload to recover from infrastructure or service disruptions, dynamically acquire computing resources to meet demand, and mitigate disruptions such as misconfigurations or transient network issues. Reference: [Resiliency](https://wa.aws.amazon.com/wat.concept.resiliency.en.html).

Availability refers to how much time the service provider guarantees that your data and services are available.

2. You have a web site w/ three distinct services (mysite.co/accounts, mysite.co/sales, mysite.co/support); each hosted by different web server Auto Scaling groups. You need to use advanced routing to send requests to specific web servers, based on configured rules. Which of the following AWS services should you use?

**Application Load Balancers (ALB)**

The ALB has functionality to distinguish traffic for different targets (mysite.co/accounts vs. mysite.co/sales vs. mysite.co/support) and distribute traffic based on rules for: target group, condition, and priority.

3. In discussions about Cloud services, the words 'Availability', 'Durability', 'Reliability', and 'Resiliency' are often used. Which term is used to refer to the likelihood that a resource will work as designed?

**Reliability**

Each word has a specific meaning and your ability to select a correct answer may depend on understanding the difference. Reliability is closely related to Availability, however a system can be 'Available' but now be working properly. Reliability is the probability that a system will work as designed. This term is not used much in AWS, but it is still worth understanding.

4. When an EC2 instance is being modified to have more RAM, is this considered Scaling Up or Scaling Out?

**Scaling Up**

Scaling out is where you have more of the same resource separately working in parallel (visualize services sitting side by side). "Scaling Up" is where you make it bigger and bigger like an ugly tower w/ more floors being added after the initial design was finished.

5. Regarding the S3 Standard, S3 Intelligent-Tiering, S3 Standard-IA, S3 One Zone-IA, S3 Glacier, and S3 Glacier Deep Archive Amazon S3 storage classes, objects are designed for _ durability.

**99.999999999 percent**

S3 Standard, S3 Intelligent-Tiering, S3 Standard-IA, S3 One Zone-IA, S3 Glacier, and S3 Glacier Deep Archive Amazon S3 storage classes, objects are designed for 99.999999999% (11 9s) durability. Reference: [Performance across the S3 Storage Classes](https://aws.amazon.com/s3/storage-classes/#Performance_across_the_S3_Storage_Classes).

6. You work for a manufacturing company that operates a hybrid infrastructure w/ systems located both in local data center and in AWS, connected via AWS Direct Connect. Currently, all on-premise servers are bached up to a local NAS, but your CTO wants you to decide on the best way to store copies of these backups in AWS. He has asked you to propose a solution which will provide access to the files within milliseconds should they be needed, but at the same time minimizes cost. As these files will be copies of backups stored on-premise, availability is not as critical as durability, but both are important. Choose the best option from the following which meets the brief.

**Copy the files from the NAS to an S3 bucket w/ the Standard-IA class.**

S3 Standard-IA provides rapid access to files and is resilient against events that impact an entire Availability Zone, while offering the same 11 9s of durability as all other storage classes. The trade-offs is in the availability. It is designed for 99.9% availability over a given year, as opposed to 99.99% that S3 Standard offers. However, in this brief, as cost is more important than availability, S3 Standard-IA is the logical choice.

S3 Standard isn't the most cost-effective solution since S3 Standard-IA is also designed for durability of 99.999999999%.

7. Which service works in conjunction w/ EC2 Autoscaling in order to provide predictive scaling based on daily and weekly trends?

**AWS Autoscaling**

EC2 Autoscaling works in conjunction w/ the AWS Autoscaling service to provide a predictive ability to your autoscaling groups.

8. A product manager walks into your office and advises that the simple single node MySQL RDS instance that has been used for a pilot needs to be upgraded for production. She also advises that they may need to alter the size of the instance once they see how many people use the system during peak periods. The key concern is that there can not be any outages of more than a few seconds during the go-live period. Which of the following might you recommend?

[x] Consider replacing it w/ Aurora before going live.

[ ] Upgrade the RDS instance to a large size before go-live to avoid the 10-15 minute outage needed to change size later.

[x] Convert the RDS instance to a multi-AZ implementation.

[ ] Implement Read-Replicas now to allow the instance size to be altered on the fly w/o any user impact.

There are two issues to be addressed in this question. Minimizing outages, whether due to required maintenance or unplanned failures. Plus the possibility of needing to scale up or down. Read-replicas can help you w/ high read loads, but are not intended to be a solution to system outages. Multi-AZ implementations will increase availability b/c in the event of an instance outage one of the instances in another AZs will pick up the load w/ minimal delay. Aurora provided the same capability w/ potentially higher availability and faster response.

9. Following an unplanned outage, you have been called into a planning meeting. You are asked what can be done to reduce the risk of bad deployments and single point of failure for your AWS resources. Which solutions can be used to mitigate the problem? The options do not necessarily need to work together.

[ ] Use automation to ensure that all updates are always deployed to all autoscaling groups at the same time.

[x] Use multiple autoscaling groups and boundaries for a staged or 'canary' deployment process.

[ ] Use Route 53 to direct traffic to the multi-region compute services on a round-robin basis.

[x] Use Route 53 w/ health checks to distribute load across multiple ELBs.

[x] Use a Classic Load Balancer to spread the load over several availability zones.

[ ] Use an Application Load Balancer to spread the load over several regions.

[x] Use several Target groups or Auto Scaling groups under each Load Balancer.

The purpose of a canary deployment is to reduce the risk of deploying a new version that impacts the workload.

Using Route 53 in combination w/ ELBs is a good pattern to distribute regionally as well as across AZs.

Cross-zone load balancing reduces the need to maintain equivalent numbers of instances in each enabled Availability Zone, and improves your application's ability to handle the loss of one or more instances.

Although the methods vary, you can place multiple autoscaling or target groups behind ELBs.

Using Route 53 to distribute work direct to compute resources can work, but is hard to manage, and is not a recommended AWS pattern. Application Load Balancers can spread load across AZs not regions.

10. You manage a high-performance site that collects scientific data using a bespoke protocol over TCP port 1414. The data comes in at high speed and is distributed to an autoscaling group of EC2 compute services spread over three AZs. Which type of AWS Load Balancer would best meet this requirement?

**Network Load Balancers (NLB)**

The Network Load Balancer is specifically designed for high performance traffic that is not conventional Web traffic. The Classic LB might also do the job, but would not offer the same performance.

11. You are running an Amazon RDS Multi-AZ deployment. Can you use the secondary database as an independent read node?

**No**. You can't use the standby (secondary database) to offload reads from an application. The standby is only there for failover. If you want an independent read node, you need to create a special type of DB instance called a read replica, from a source DB instance. Reference: [Working w/ Read Replicas](https://docs.aws.amazon.com/AmazonRDS/latest/UserGuide/USER_ReadRepl.html)

12. Placement Groups can either be of the type 'Cluster', 'Spread', or 'Partition'. Choose options from below which are only specific to Spread Placement Groups.

**A spread placement group supports a maximum of seven running instances per Availability Zone**.

A spread placement group supports a maximum of seven running instances per Availability Zone. For example, in a Region w/ three Availability Zones, you can run a total of 21 instances in the group (seven per zone). If you try to start an eight instance in the same Availability Zone and in the same placement group, the instance will not launch. If you need to have more than seven instances in an Availability Zone, then the recommendation is to use multiple spread placement groups. Using multiple spread placement groups does not provide guarantees about the spread of instances between groups, but it does ensure the spread for each group, thus limiting impact from certain classes of failures.

13. In discussions about Cloud services the word 'Availability', 'Durability', 'Reliability', and 'Resiliency' are often used. Which term is used to refer to the likelihood that you can access a resource or service when you need it?

**Availability**

Availability can be described as the % of a time period when the service will be able to respond to your request in some fashion.

14. You work for a major news network in Europe. They have just released a new mobile app that allows users to post their photos on newsworthy events in real-time. Your organization expects this app to grow very quickly, essentially doubling its user base each month. The app uses S3 to store the images, and you are expecting sudden and sizable increases in traffic to S3 when a major news event takes place (as users will be uploading large amounts of content.) You need to keep your storage costs to a minimum, and you are happy to temporarily lose access to up to 0.1% of uploads per year. W/ these factors in mind, which storage media should you use to keep costs as low as possible?

[ ] S3 - OneZone-Infrequent Access

[ ] S3 - Reduced Redundancy Storage (RRS)

[ ] S3 - Provisioned IOPS

[ ] S3 Standard

[ ] Glacier

[x] S3 Standard-IA

The key drivers here are availability and cost, so an awareness of cost is necessary to answer this. Glacier cannot be considered as it not intended for direct access. S3 has an availability of 99.99%, S3 Standard-IA has an availability of 99.9% while S3-1Zone-IA only has 99.5%

15. Can I "force" a failover for any RDS instance that has Multi-AZ configured?

**Yes**.

In the event of a planned or unplanned outage of your DB instance, Amazon RDS automatically switches to a standby replica in another Availability Zone if you have enabled Multi-AZ. You can force a failover manually when you reboot a DB instance. Reference: [Failover Process for Amazon RDS](https://docs.aws.amazon.com/AmazonRDS/latest/UserGuide/Concepts.MultiAZ.html)

16. Which of the below are not components of Amazon EC2 Auto Scaling?

[x] cfn-init

[ ] Scaling Options

[ ] Groups

[ ] Configuration Templates

cfn-init is not a component of EC2 Autoscaling service. Instead it is a feature which allows commands to be run, and software installed/configured on EC2 instances when they launch.

Your group uses a launch template, or a launch configuration (not recommended, offers fewer features), as a configuration template for its EC2 instances. You can specify information such as the AMI ID, instance type, key pair, security groups, and block device mapping for your instances. Reference: [What is Amazon EC2 Auto Scaling?](https://docs.aws.amazon.com/autoscaling/ec2/userguide/what-is-amazon-ec2-auto-scaling.html)

17. Your company has built an internal scrum tool for running all your scrum ceremonies. Usage is predictably between 9 - 10AM Mon-Fri and also 1PM - 2PM Thu and Fri. Which feature of autoscaling will easily prepare your system to handle the load?

**Scheduled Scaling**

Target tracking could work but you need to invest time in determining the correct metric to track (e.g. CPU, Memory, Load Balancer Requests). Also Manual Scaling requires that someone changes configuration to scale up and scale down every day. Finally over provisioning in order to cope w/ peak demand defeats the purpose of elastic scaling of your compute. For situations where your traffic is very unpredictable, the easiest way to scale w/ demand is to create scheduled scaling actions.

18. You need to use object-based storage solution to store your critical, non-replaceable data in a cost-effective way. This data will be frequently updated and will need some form of version control enabled on it. Which S3 storage solution should you use?

From the question, we can identify that:

* the data is non-replaceable (All S3 classes are at 11 9s of Durability now except for RRS)

* the data is frequently updated (Classes outside of S3 Standard & S3 Intelligent-Tiering have extra charges for frequently accessed data).

* cost-effective (S3 is more cost effective than S3 Intelligent-Tiering if the data is updated frequently)

* version control must be an available feature (S3 has versioning as a feature)

All of these items combined make S3 the best option for the available information.

19. In discussions about Cloud services, the words 'Availability', 'Durability', 'Reliability',and 'Resiliency' are often used. Which term is used to refer to the likelihood that a resource will continue to exist until you decide to remove it?

**Durability**

Durability refers to the on-going existence of the object or resource. Note that it does not mean you can access it, only that it continues to exist.
