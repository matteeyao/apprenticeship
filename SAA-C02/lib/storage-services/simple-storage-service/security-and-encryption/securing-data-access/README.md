# Securing Data Access

1. An organization needs to control the access for several S3 buckets. They plan to use a gateway endpoint to allow access to trusted buckets.

Which of the following could help you achieve this requirement?

[ ] Generate a bucket policy for trusted S3 buckets.

[ ] Generate an endpoint policy for trusted S3 buckets.

[ ] Generate an endpoint policy for trusted VPCs.

[ ] Generate a bucket policy for trusted VPCs.

**Explanation**: A **VPC endpoint** enables you to privately connect your VPC to supported AWS services and VPC endpoint services powered by AWS PrivateLink without requiring an internet gateway, NAT device, VPN connection, or AWS Direct Connect connection. Instances in your VPC do not require public IP addresses to communicate with resources in the service. Traffic between your VPC and the other service does not leave the Amazon network.

When you create a VPC endpoint, you can attach an endpoint policy that controls access to the service to which you are connecting. You can modify the endpoint policy attached to your endpoint and add or remove the route tables used by the endpoint. An endpoint policy does not override or replace IAM user policies or service-specific policies (such as S3 bucket policies). It is a separate policy for controlling access from the endpoint to the specified service.

![Fig. 1 Endpoint Policy in Action](../../../../../../../img/SAA-CO2/storage-services/simple-storage-service/security-and-encryption/securing-data-access/fig01.png)

We can use a bucket policy or an endpoint policy to allow the traffic to trusted S3 buckets. The options that have 'trusted S3 buckets' key phrases will be the possible answer in this scenario. It would take you a lot of time to configure a bucket policy for each S3 bucket instead of using a single endpoint policy. Therefore, you should use an endpoint policy to control the traffic to the trusted Amazon S3 buckets.

Hence, the correct answer is: **Generate an endpoint policy for trusted S3 buckets**.

> The option that says: **Generate a bucket policy for trusted S3 buckets** is incorrect. Although this is a valid solution, it takes a lot of time to set up a bucket policy for each and every S3 bucket. This can simply be accomplished by creating an S3 endpoint policy.

> The option that says: **Generate a bucket policy for trusted VPCs** is incorrect because you are generating a policy for trusted VPCs. Remember that the scenario only requires you to allow the traffic for trusted S3 buckets, and not to the VPCs.

> The option that says: **Generate an endpoint policy for trusted VPCs** is incorrect because it only allows access to trusted VPCs, and not to trusted Amazon S3 buckets.

<br />
