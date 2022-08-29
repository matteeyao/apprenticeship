# Auto Scaling Group Launch Configuration

1. What is the template that Auto Scaling uses to launch a fully configured instance automatically?

[ ] AMI ID `(AMI ID does not specify instance type)`

[ ] Instance type `(instance type does not specify AMI ID)`

[ ] Key pair `(does not specify AMI ID or instance type)`

[x] Launch configuration `(Includes AMI ID, instance type, key pair, and user data)`

[ ] User data `(does not specify AMI ID or instance type)`

<br />

2. A tech company is currently using Auto Scaling for their web application. A new AMI now needs to be used for launching a fleet of EC2 instances.

Which of the following changes needs to be done?

[ ] Do nothing. You can start directly launching EC2 instances in Auto Scaling Group w/ the same launch configuration.

[ ] Create a new launch configuration.

[ ] Create a new target group and launch configuration.

[ ] Create a new target group.

**Explanation**: A launch configuration is a template that an Auto Scaling group uses to launch EC2 instances. When you create a launch configuration, you specify information for the instances, such as the ID of the Amazon Machine Image (AMI), the instance type, a key pair, one or more security groups, and a block device mapping. If you've launched an EC2 instance before, you specified the same information in order to launch the instance.

You can specify your launch configuration with multiple Auto Scaling groups. However, you can only specify one launch configuration for an Auto Scaling group at a time, and you can't modify a launch configuration after you've created it. Therefore, if you want to change the launch configuration for an Auto Scaling group, you must create a launch configuration and then update your Auto Scaling group with the new launch configuration.

For this scenario, you have to create a new launch configuration. Remember that you can't modify a launch configuration after you've created it.

Hence, the correct answer is: **Create a new launch configuration**.

> The option that says: **Do nothing. You can start directly launching EC2 instances in the Auto Scaling group with the same launch configuration** is incorrect because what you are trying to achieve is change the AMI being used by your fleet of EC2 instances. Therefore, you need to change the launch configuration to update what your instances are using.

> The option that says: **create a new target group** and **create a new target group and launch configuration** are both incorrect because you only want to change the AMI being used by your instances, and not the instances themselves. Target groups are primarily used in ELBs and not in Auto Scaling. The scenario didn't mention that the architecture has a load balancer. Therefore, you should be updating your launch configuration, not the target group.

<br />
