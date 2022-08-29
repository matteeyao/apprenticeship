# AWS Control Tower

1. A company wants to streamline the process of creating multiple AWS accounts within an AWS Organization. Each organization unit (OU) must be able to launch new accounts with pre-approved configurations from the security team which will standardize the baselines and network configurations for all accounts in the organization.

Which solution entails the least amount of effort to implement?

[x] Set up an AWS Control Tower Landing Zone. Enable pre-packaged guardrails to enforce policies or detect violations.

[ ] Set up an AWS Config aggregator on the root account of the organization to enable multi-account, multi-region data aggregation. Deploy conformance packs to standardize the baselines and network configurations for all accounts.

[ ] Configure AWS Resource Access Manager (AWS RAM) to launch new AWS accounts as well as standardize the baselines and network configurations for each organization unit.

[ ] Centralized the creation of AWS accounts using AWS Systems Manager OpsCenter. Enforce policies and detect violations to AWS accounts using AWS Security Hub.

**Explanation**: **AWS Control Tower** provides a single location to easily set up your new well-architected multi-account environment and govern your AWS workloads with rules for security, operations, and internal compliance. You can automate the setup of your AWS environment with best-practices blueprints for multi-account structure, identity, access management, and account provisioning workflow. For ongoing governance, you can select and apply pre-packaged policies enterprise-wide or to specific groups of accounts.

AWS Control Tower provides three methods for creating member accounts:

  * Through the Account Factory console that is part of AWS Service Catalog.

  * Through the Enroll account feature within AWS Control Tower.

  * From your AWS Control Tower landing zone's management account, using Lambda code and appropriate IAM roles.

AWS Control Tower offers "guardrails" for ongoing governance of your AWS environment. Guardrails provide governance controls by preventing the deployment of resources that don’t conform to selected policies or detecting non-conformance of provisioned resources. AWS Control Tower automatically implements guardrails using multiple building blocks such as AWS CloudFormation to establish a baseline, AWS Organizations service control policies (SCPs) to prevent configuration changes, and AWS Config rules to continuously detect non-conformance.

In this scenario, the requirement is to simplify the creation of AWS accounts that have governance guardrails and a defined baseline in place. To save time and resources, you can use AWS Control Tower to automate account creation. With the appropriate user group permissions, you can specify standardized baselines and network configurations for all accounts in the organization.

Hence, the correct answer is: **Set up an AWS Control Tower Landing Zone. Enable pre-packaged guardrails to enforce policies or detect violations.**

> The option that says: **Configure AWS Resource Access Manager (AWS RAM) to launch new AWS accounts as well as standardize the baselines and network configurations for each organization unit** is incorrect. The AWS Resource Access Manager (RAM) service simply helps you to securely share your resources across AWS accounts or within your organization or organizational units (OUs) in AWS Organizations. It is not capable of launching new AWS accounts with preapproved configurations.

> The option that says: **Set up an AWS Config aggregator on the root account of the organization to enable multi-account, multi-region data aggregation. Deploy conformance packs to standardize the baselines and network configurations for all accounts** is incorrect. AWS Config cannot provision accounts. A conformance pack is only a collection of AWS Config rules and remediation actions that can be easily deployed as a single entity in an account and a Region or across an organization in AWS Organizations.

> The option that says: **Centralize the creation of AWS accounts using AWS Systems Manager OpsCenter. Enforce policies and detect violations to all AWS accounts using AWS Security Hub** is incorrect. AWS Systems Manager is just a collection of services used to manage applications and infrastructure running in AWS that is usually in a single AWS account. The AWS Systems Manager OpsCenter service is just one of the capabilities of AWS Systems Manager, provides a central location where operations engineers and IT professionals can view, investigate, and resolve operational work items (OpsItems) related to AWS resources.

<br />
