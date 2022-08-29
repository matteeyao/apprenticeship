# Automation

* Platform to orchestrate operational playbooks

* Manage any AWS resource across accounts/Regions

* Orchestrate dynamic playbooks

* Standardize and share playbooks across organization

* Safe at-scale operations

* Integrates w/ AWS Config, AWS Service Catalog, and others

## Automation benefits

* Auditable service

* Native IAM integration

* Enhanced operations security

* Ability to share best practices via automation playbooks

* Enhanced integration

  * Ability to call and run AWS API actions, such as creating an AWS CloudFormation stack

  * Ability to run a script (PowerShell, Python)

  * AWS Service Catalog self-service actions like reboot RDS

* Automation at scale

## Common use cases

* Automating the creation of golden AMIs

* Handling one-click configuration tasks, such as configuring Amazon S3 buckets

* Performing routine maintenance tasks, such as patching AutoScaling groups

* Automatically remediating resources through AWS Config

* Stopping Amazon EC2 instances w/ approvals

* Taking backups of resources, such as DynamoDB or RDS

## How does Automation work?

* Assumes current user context by default

* Option to specify service role

* Leverage AWS playbooks

* Create custom Automation documents

  * Defines actions to perform

  * Provide dynamic parameters

  * Conditionally branch based on step results

  * Configure approvals as part of workflow

* Run the Automation playbook

  * Multi-account and multi-Region

  * Register as a Maintenance Window task

  * Automatic remediation w/ AWS Config

## Automation use cases

* Manage Patching for your Amazon Machine Images

* Run Automations Across Multiple AWS Regions and Accounts

* Remediate Non-Compliance Using AWS Config Rules and a Custom SSM Document
