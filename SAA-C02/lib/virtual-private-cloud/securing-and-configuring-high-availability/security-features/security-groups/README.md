# VPC Security Groups

1. A tech company is having an issue whenever they try to connect to the newly created EC2 instance using a Remote Desktop connection from a computer. Upon checking, the Solutions Architect has verified that the instance has a public IP and the Internet gateway and route tables are in place.

What else should he do to resolve this issue?

[ ] You should restart the EC2 instance since there might be some issue w/ the instance

[ ] Adjust the security group to allow inbound traffic on port 3389 from the company's IP address.

[ ] Adjust the security group to allow inbound traffic on port 22 from the company's IP address.

[ ] You should create a new instance since there might be some issue w/ the instance

**Explanation**: Since you are using a Remote Desktop connection to access your EC2 instance, you have to ensure that the Remote Desktop Protocol is allowed in the security group. By default, the server listens on TCP port 3389 and UDP port 3389.

Hence, the correct answer is: **Adjust the security group to allow inbound traffic on port 3389 from the company’s IP address.**

> The option that says: **Adjust the security group to allow inbound traffic on port 22 from the company’s IP address** is incorrect as port 22 is used for SSH connections and not for RDP.

> The options that say: **You should restart the EC2 instance since there might be some issue with the instance** and **You should create a new instance since there might be some issue with the instance** are incorrect as the EC2 instance is newly created and hence, unlikely to cause the issue. You have to check the security group first if it allows the Remote Desktop Protocol (3389) before investigating if there is indeed an issue on the specific instance.

<br />
