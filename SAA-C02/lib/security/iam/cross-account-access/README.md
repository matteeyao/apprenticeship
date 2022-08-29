# IAM Cross-Account Access

1. A company requires corporate IT governance and cost oversight of all of its AWS resources across its divisions around the world. Their corporate divisions want to maintain administrative control of the discrete AWS resources they consume and ensure that those resources are separate from other divisions.

Which of the following options will support the autonomy of each corporate division while enabling the corporate IT to maintain governance and cost oversight? (Select TWO.)

[ ] Create separate VPCs for each division within the corporate IT AWS account. Launch an AWS Transit Gateway w/ equal-cost multipath routing (ECMP) and VPN tunnels for intra-VPC communication.

[ ] Create separate Availability Zones for each division within the corporate IT AWS account. Improve communication between the two AZs using the AWS Global Accelerator

[ ] Use AWS Consolidated Billing by creating AWS Organizations to link the divisions' accounts to a parent corporate account.

[ ] Enable IAM cross-account access for all corporate IT administrators in each child account.

[ ] Use AWS Trusted Advisor and AWS Resource Groups Tag Editor

**Explanation**: You can use an IAM role to delegate access to resources that are in different AWS accounts that you own. You share resources in one account with users in a different account. By setting up cross-account access in this way, you don't need to create individual IAM users in each account. In addition, users don't have to sign out of one account and sign into another in order to access resources that are in different AWS accounts.

You can use the consolidated billing feature in AWS Organizations to consolidate payment for multiple AWS accounts or multiple AISPL accounts. With consolidated billing, you can see a combined view of AWS charges incurred by all of your accounts. You can also get a cost report for each member account that is associated with your master account. Consolidated billing is offered at no additional charge. AWS and AISPL accounts can't be consolidated together.

The combined use of IAM and Consolidated Billing will support the autonomy of each corporate division while enabling corporate IT to maintain governance and cost oversight. Hence, the correct choices are:

* **Enable IAM cross-account access for all corporate IT administrators in each child account**

* **Use AWS Consolidated Billing by creating AWS Organizations to link the divisions’ accounts to a parent corporate account**

> **Using AWS Trusted Advisor and AWS Resource Groups Tag Editor** is incorrect. Trusted Advisor is an online tool that provides you real-time guidance to help you provision your resources following AWS best practices. It only provides you alerts on areas where you do not adhere to best practices and tells you how to improve them. It does not assist in maintaining governance over your AWS accounts. Additionally, the AWS Resource Groups Tag Editor simply allows you to add, edit, and delete tags to multiple AWS resources at once for easier identification and monitoring.

> **Creating separate VPCs for each division within the corporate IT AWS account. Launch an AWS Transit Gateway with equal-cost multipath routing (ECMP) and VPN tunnels for intra-VPC communication** is incorrect because creating separate VPCs would not separate the divisions from each other since they will still be operating under the same account and therefore contribute to the same billing each month. AWS Transit Gateway connects VPCs and on-premises networks through a central hub and acts as a cloud router where each new connection is only made once. For this particular scenario, it is suitable to use AWS Organizations instead of setting up an AWS Transit Gateway since the objective is for maintaining administrative control of the AWS resources and not for network connectivity.

> **Creating separate Availability Zones for each division within the corporate IT AWS account. Improve communication between the two AZs using the AWS Global Accelerator** is incorrect because you do not need to create Availability Zones. They are already provided for you by AWS right from the start, and not all services support multiple AZ deployments. In addition, having separate Availability Zones in your VPC does not meet the requirement of supporting the autonomy of each corporate division. The AWS Global Accelerator is a service that uses the AWS global network to optimize the network path from your users to your applications and not between your Availability Zones.

<br />
