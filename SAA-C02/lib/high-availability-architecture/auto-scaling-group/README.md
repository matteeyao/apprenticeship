# Auto-Scaling Group

1. A radio station runs a contest where every day at noon they make an announcement that generates an immediate spike in traffic that requires 8 EC2 instances to process. All other times the web site requires 2 EC2 instances.

Which is the most cost effective way to meet these requirements?

[ ] Create an Auto Scaling group w/ a minimum capacity of 2 and scale up based upon CPU utilization `(delay in launching instances)`

[x] Create an Auto Scaling group w/ a minimum capacity of 2 and set a schedule to scale up at 11:40 am

[ ] Create an Auto Scaling group w/ a minimum capacity of 2 and scale up based upon memory utilization `(not a native CloudWatch event)`

<br />

2. An application runs on EC2 instances in an Auto Scaling group. The application runs optimally on 9 EC2 instances and must have at least 6 running instances to maintain minimally acceptable performance for a short period of time. Which is the most cost-effective Auto Scaling group configuration that meets the requirements?

[ ] A desired capacity of 9 instances across 2 Availability Zones `(less than 6 if an AZ is unavailable)`

[x] A desired capacity of 9 instances across 3 Availability Zones `(if an AZ becomes unavailable, we will still have 6 instances)`

[ ] A desired capacity of 12 instances across 2 Availability Zones `(Works, but extra compute)`

[ ] A desired capacity of 9 instances across 1 Availability Zone `(Not Highly Available)`

<br />

3. The web tier for an application is running on 6 EC2 instances spread across 2 availability zones behind an ELB Classic Load Balancer. The data tier is a MySQL database running on an EC2 instance. What changes will increase the availability of the application? (Select two)

[ ] Turn on cross-zone load-balancing on the Classic Load Balancer `(little or no effect)`

[ ] Turn on CloudTrail in the AWS account of the application `(Helps w/ monitoring, no effect on availability)`

[ ] Increase the instance size of the web tier EC2 instance `(no effect)`

[x] Launch the web tier EC2 instances in an Auto Scaling group

[x] Migrate the MySQL database to a Multi-AZ RDS MySQL database instance

<br />

4. A suite of web applications is hosted in an Auto Scaling group of EC2 instances across three Availability Zones and is configured with default settings. There is an Application Load Balancer that forwards the request to the respective target group on the URL path. The scale-in policy has been triggered due to the low number of incoming traffic to the application.

Which EC2 instance will be the first one to be terminated by your Auto Scaling group?

[x] The EC2 instance launched from the oldest launch configuration

[ ] The EC2 instance which has been running for the longest time

[ ] The instance will be randomly selected by the Auto Scaling Group

[ ] The EC2 instance which has the least number of user sessions

**Explanation**: The default termination policy is designed to help ensure that your network architecture spans Availability Zones evenly. With the default termination policy, the behavior of the Auto Scaling group is as follows:

  1. If there are instances in multiple Availability Zones, choose the Availability Zone with the most instances and at least one instance that is not protected from scale in. If there is more than one Availability Zone with this number of instances, choose the Availability Zone with the instances that use the oldest launch configuration.

  2. Determine which unprotected instances in the selected Availability Zone use the oldest launch configuration. If there is one such instance, terminate it.

  3. If there are multiple instances to terminate based on the above criteria, determine which unprotected instances are closest to the next billing hour. (This helps you maximize the use of your EC2 instances and manage your Amazon EC2 usage costs.) If there is one such instance, terminate it.

  4. If there is more than one unprotected instance closest to the next billing hour, choose one of these instances at random.

<br />

5. An online shopping platform is hosted on an Auto Scaling group of On-Demand EC2 instances with a default Auto Scaling termination policy and no instance protection configured. The system is deployed across three Availability Zones in the US West region (us-west-1) with an Application Load Balancer in front to provide high availability and fault tolerance for the shopping platform. The us-west-1a, us-west-1b, and us-west-1c Availability Zones have 10, 8 and 7 running instances respectively. Due to the low number of incoming traffic, the scale-in operation has been triggered.   

Which of the following will the Auto Scaling group do to determine which instance to terminate first in this scenario? (Select THREE.)

[x] Choose the Availability Zone w/ the most number of instances, which is the `us-west-1a` Availability Zone in this scenario.

[ ] Select the instance that is farthest to the next billing hour.

[x] Select the instance that is closest to the next billing hour.

[ ] Select the instances w/ the most recent launch configuration.

[ ] Select the instances w/ the oldest launch configuration.

[ ] Choose the Availability Zone w/ the least number of instances, which is the `us-west-1c` Availability Zone in this scenario.

**Explanation**: The default termination policy is designed to help ensure that your network architecture spans Availability Zones evenly. With the default termination policy, the behavior of the Auto Scaling group is as follows:

  1. If there are instances in multiple Availability Zones, choose the Availability Zone with the most instances and at least one instance that is not protected from scale in. If there is more than one Availability Zone with this number of instances, choose the Availability Zone with the instances that use the oldest launch configuration.

  2. Determine which unprotected instances in the selected Availability Zone use the oldest launch configuration. If there is one such instance, terminate it.

  3. If there are multiple instances to terminate based on the above criteria, determine which unprotected instances are closest to the next billing hour. (This helps you maximize the use of your EC2 instances and manage your Amazon EC2 usage costs.) If there is one such instance, terminate it.

  4. If there is more than one unprotected instance closest to the next billing hour, choose one of these instances at random.

<br />

6. A loan processing application is hosted in a single On-Demand EC2 instance in your VPC. To improve the scalability of your application, you have to use Auto Scaling to automatically add new EC2 instances to handle a surge of incoming requests.

Which of the following items should be done in order to add an existing EC2 instance to an Auto Scaling group? (Select TWO.)

[x] You have to ensure that the instance is launched in one of the Availability Zones defined in your Auto Scaling Group.

[ ] You must stop the instance first.

[ ] You have to ensure that the instance is in a different Availability Zone as the Auto Scaling group.

[x] You have to ensure that the AMI used to launch the instance still exists.

[ ] You have to ensure that the AMI used to launch the instance no longer exists.

**Explanation**: Amazon EC2 Auto Scaling provides you with an option to enable automatic scaling for one or more EC2 instances by attaching them to your existing Auto Scaling group. After the instances are attached, they become a part of the Auto Scaling group.

The instance that you want to attach must meet the following criteria:

* The instance is in the `running` state.

* The AMI used to launch the instance must still exist.

* The instance is not a member of another Auto Scaling group.

* The instance is launched into one of the Availability Zones defined in your Auto Scaling group.

* If the Auto Scaling group has an attached load balancer, the instance and the load balancer must both be in EC2-Classic or the same VPC. If the Auto Scaling group has an attached target group, the instance and the load balancer must both be in the same VPC.

Based on the above criteria, the following are the correct answers among the given options:

* You have to ensure that the AMI used to launch the instance still exists.

* You have to ensure that the instance is launched in one of the Availability Zones defined in your Auto Scaling group.

> The option that says: **You must stop the instance first** is incorrect because you can directly add a running EC2 instance to an Auto Scaling group without stopping it.

> The option that says: **You have to ensure that the AMI used to launch the instance no longer exists** is incorrect because it should be the other way around. The AMI used to launch the instance should still exist.

> The option that says: **You have to ensure that the instance is in a different Availability Zone as the Auto Scaling group** is incorrect because the instance should be launched in one of the Availability Zones defined in your Auto Scaling group.

<br />
