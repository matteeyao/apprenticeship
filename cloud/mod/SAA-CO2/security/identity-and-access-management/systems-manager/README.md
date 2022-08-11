# Systems Manager

* A set of fully managed AWS services and capabilities

* Automated configuration and ongoing management of systems at scale

## What problems does it solve?

1. Traditional IT tool sets are not designed or built for cloud scale.

2. Deploying and maintaining multiple management products is a significant operational overhead.

3. Licensing adds cost and complexity.

## Systems Manager capabilities

* **Run Command**

  * Remotely and securely run configuration actions at scale

    * Hybrid

    * No inbound network ports

    * Access control = authorization

    * Auditable (who performed which action and when)

  * Accessible via:
  
    * AWS Management Console

    * AWS Command Line Interface (AWS CLI)

    * Any of the AWS software development kits (SDKs)

* **State Manager**

* **Inventory**

* **Patch Manager**

  * Simplify operating system and application patching process

  * Select patches to deploy

    * Whitelist or blacklist specific patches

  * Specify timing to roll out

  * Control instance reboots

  * Report patching compliance

  * Schedule automatic rollout w/ Maintenance Windows

* **Maintenance Window**

* **Automation**

* **Parameter Store**

  * Provides secure and hierarchical storage for:

    * Configuration data

    * Secrets data

  * You can store:

    * Passwords, license codes, database strings,  etc.

  * Can be stored encrypted or in plaintext; can be accessed programmatically, from the AWS Console, or AWS CLI

  * Highly scalable, available, and durable

  * Auditable

  * Native integration w/ IAM

* **Session Manager**

* **OpsCenter**

* **Explorer**

* **Change Calendar**

* **Distributor**

## Systems Manager building blocks

* Simple Systems Manager (SSM) agent on Amazon Elastic Compute Cloud (Amazon EC2)

* Documents

## AWS Systems Manager Quick Setup

* AWS Identity and Access Management (IAM) instance profile roles for Systems Manager

* A scheduled, biweekly update of SSM agent

* A scheduled collection of inventory metadata every 30 minutes

* A daily scan of your instances to identify missing patches

* A one-time installation and configuration of the Amazon CloudWatch agent

* A scheduled, monthly update of the CloudWatch agent
