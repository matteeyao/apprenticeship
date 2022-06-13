# AWS Core Services

1. What are the two types of events that CloudTrail logs?

[x] Managed Events

[ ] Logs

[ ] Alerts

[x] Data Events

2. What happens when you reboot your instance using the Amazon EC2 console, a command line tool, and the Amazon EC2 API?

**The instance remains on the same host computer and maintains its public DNS name, private IP address, and any data on its instance store volumes**

Rebooting an instance is equivalent to rebooting an operating system. The instance remains on the same host computer and maintains its public DNS name, private IP address, and any data in its instance store volumes. It typically takes a few minutes for the reboot to complete, but the time it takes to reboot depends on the instance configuration - [Instance lifecycle](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/ec2-instance-lifecycle.html).

3. What is AWS' NoSQL database?

**DynamoDB**

Redshift is used for petabyte scale and is a column based database. AWS's database service for Non-Relational databases, DynamoDB and are considered NoSQL.

4. What is required to have in your CloudFormation template?

**Resource**

All templates have at least one resource, and this part of your CF template is mandatory

5. What three parts of CloudWatch do we need to know for our exam?

[ ] Collections

[x] Logs

[x] Metrics

[x] Events

6. What is the difference between CNAME and Alias records?

**CNAME records redirect DNS queries to any DNS record and Alias records map to AWS resources**

CNAME can be used to resolve one domain name to another so lets say you have www.mobile.amazon.com and also www.amazon.com, so both will resolve to the same IP address, so we just map the domain name to the other. Alias records are used to map resources to record sets in your hosted zone to ELB, CF, S3 static websites, etc.

7. Can s3 store virtually an unlimited amount of objects?

**Yes**

Your s3 bucket can store virtually unlimited objects

8. What is Route 53?

**Route 53 is AWS' managed DNS (Domain Name System) product and it essentially helps us w/ two things: first we can register domain and second Route 53 can host zones on managed nameservers.**

Route 53 is AWS' managed DNS (Domain Name System) product and it essentially helps us w/ two things: first we can register domain and second Route 53 can host zones on managed nameservers.
