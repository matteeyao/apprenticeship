# RDS

1. An online cryptocurrency exchange platform is hosted in AWS which uses ECS Cluster and RDS in Multi-AZ Deployments configuration. The application is heavily using the RDS instance to process complex read and write database operations. To maintain the reliability, availability, and performance of your systems, you have to closely monitor how the different processes or threads on a DB instance use the CPU, including the percentage of the CPU bandwidth and total memory consumed by each process.

Which of the following is the most suitable solution to properly monitor your database?

[ ] Enable Enhanced Monitoring in RDS.

[ ] Check the `CPU%` and `MEM%` metrics which are readily available in the Amazon RDS console that shows the percentage of the CPU bandwidth and total memory consumed by each database process of your RDS instance.

[ ] Create a script that collects and publishes custom metrics to CloudWatch, which tracks the real-time CPU utilization of the RDS instance, and then set up a custom CloudWatch dashboard to view the metrics.

[ ] Use Amazon CloudWatch to monitor the CPU Utilization of your database.

**Explanation**: Amazon RDS provides metrics in real time for the operating system (OS) that your DB instance runs on. You can view the metrics for your DB instance using the console, or consume the Enhanced Monitoring JSON output from CloudWatch Logs in a monitoring system of your choice. By default, Enhanced Monitoring metrics are stored in the CloudWatch Logs for 30 days. To modify the amount of time the metrics are stored in the CloudWatch Logs, change the retention for the `RDSOSMetrics` log group in the CloudWatch console.

Take note that there are certain differences between CloudWatch and Enhanced Monitoring Metrics. CloudWatch gathers metrics about CPU utilization from the hypervisor for a DB instance, and Enhanced Monitoring gathers its metrics from an agent on the instance. As a result, you might find differences between the measurements, because the hypervisor layer performs a small amount of work. Hence, **enabling Enhanced Monitoring in RDS** is the correct answer in this specific scenario.

The differences can be greater if your DB instances use smaller instance classes, because then there are likely more virtual machines (VMs) that are managed by the hypervisor layer on a single physical instance. Enhanced Monitoring metrics are useful when you want to see how different processes or threads on a DB instance use the CPU.

> **Using Amazon CloudWatch to monitor the CPU Utilization of your database** is incorrect. Although you can use this to monitor the CPU Utilization of your database instance, it does not provide the percentage of the CPU bandwidth and total memory consumed by each database process in your RDS instance. Take note that CloudWatch gathers metrics about CPU utilization from the hypervisor for a DB instance while RDS Enhanced Monitoring gathers its metrics from an agent on the instance.

> **Create a script that collects and publishes custom metrics to CloudWatch, which tracks the real-time CPU Utilization of the RDS instance and then set up a custom CloudWatch dashboard to view the metrics** is incorrect. Although you can use Amazon CloudWatch Logs and CloudWatch dashboard to monitor the CPU Utilization of the database instance, using CloudWatch alone is still not enough to get the specific percentage of the CPU bandwidth and total memory consumed by each database processes. The data provided by CloudWatch is not as detailed as compared with the Enhanced Monitoring feature in RDS. Take note as well that you do not have direct access to the instances/servers of your RDS database instance, unlike with your EC2 instances where you can install a CloudWatch agent or a custom script to get CPU and memory utilization of your instance.

> **Check the `CPU%` and `MEM%` metrics which are readily available in the Amazon RDS console that shows the percentage of the CPU bandwidth and total memory consumed by each database process of your RDS instance** is incorrect because the CPU% and MEM% metrics are not readily available in the Amazon RDS console

<br />

2. Which of the following statements are true of security in Amazon RDS? (Select three)

[ ] Amazon Cognito is used to control who can access the database instance.

[x] Amazon VPC is used to isolate your database from internet traffic.

[x] Connections to the database are secured using SSL.

[ ] Data is encrypted at rest using 256-bit RC6 algorithms.

[x] Security groups are used to control access to the database instance.

<br />

3. Which of the following security groups can control access to an Amazon RDS database instance? (Select three)

[ ] Amazon Cognito security groups

[x] Amazon EC2 security groups

[x] Amazon RDS security groups

[x] Amazon VPC security groups

[ ] Database security groups

**Explanation**: Amazon RDS and Amazon Cognito do not have their own security groups. Amazon RDS uses the other mentioned security groups.

<br />

4. Which of the following instance types are available to pay for Amazon RDS? (Select two)

[x] On-Demand

[ ] Provisioned

[x] Reserved

[ ] Spot

**Explanation**: You pay for the instance hosting the databases. There are two instance types to choose from: On-Demand and Reserved. On-Demand Instance pricing lets you pay for the compute capacity by the hour. This is great when your database runs intermittently or is a little unpredictable. Reserved Instance pricing is great when you have a good understanding of the resource consumption of your database. With this type of instance, you can secure a one- or three-year term and receive a significant discount over On-Demand pricing.

<br />

6. Which of the following can you use to create and modify an Amazon RDS instance?

[x] Amazon RDS API

[x] AWS Command Line Interface (CLI)

[x] AWS Management Console

[ ] Connect using the hostname and endpoint

[ ] Remote Desktop Protocol (RDP)

**Explanation**: You can create and modify database instances by using the AWS Management Console, AWS Command Line Interface (CLI), or the Amazon RDS Application Programming Interface (API). The AWS CLI and RDS API enable you to automate many tasks and integrations with your AWS environment.

<br />
