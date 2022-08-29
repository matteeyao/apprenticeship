# Bastion Host

1. A company is running a multi-tier web application farm in a virtual private cloud (VPC) that is not connected to their corporate network. They are connecting to the VPC over the Internet to manage the fleet of Amazon EC2 instances running in both the public and private subnets. The Solutions Architect has added a bastion host with Microsoft Remote Desktop Protocol (RDP) access to the application instance security groups, but the company wants to further limit administrative access to all of the instances in the VPC.

Which of the following bastion host deployment options will meet this requirement?

[ ] Deploy a Windows Bastion host w/ an Elastic IP address in the public subnet and allow SSH access to the bastion from anywhere.

[ ] Deploy a Windows Bastion host on the corporate network that has RDP access to all EC2 instances in the VPC.

[x] Deploy a Windows Bastion host w/ an Elastic IP address in the public subnet and allow RDP access to bastion only from the corporate IP addresses.

[ ] Deploy a Windows Bastion host w/ an Elastic IP address in the private subnet, and restrict RDP access to the bastion from only the corporate public IP addresses.

**Explanation**: The correct answer is to deploy a Windows Bastion host with an Elastic IP address in the public subnet and allow RDP access to bastion only from the corporate IP addresses.

A bastion host is a special purpose computer on a network specifically designed and configured to withstand attacks. If you have a bastion host in AWS, it is basically just an EC2 instance. It should be in a public subnet with either a public or Elastic IP address with sufficient RDP or SSH access defined in the security group. Users log on to the bastion host via SSH or RDP and then use that session to manage other hosts in the private subnets.

> **Deploying a Windows Bastion host on the corporate network that has RDP access to all EC2 instances in the VPC** is incorrect since you do not deploy the Bastion host to your corporate network. It should be in the public subnet of a VPC.

> **Deploying a Windows Bastion host with an Elastic IP address in the private subnet and restricting RDP access to the bastion from only the corporate public IP addresses** is incorrect since it should be deployed in a public subnet, not a private subnet.

> **Deploying a Windows Bastion host with an Elastic IP address in the public subnet and allowing SSH access to the bastion from anywhere** is incorrect. Since it is a Windows bastion, you should allow RDP access and not SSH as this is mainly used for Linux-based systems.

<br />
