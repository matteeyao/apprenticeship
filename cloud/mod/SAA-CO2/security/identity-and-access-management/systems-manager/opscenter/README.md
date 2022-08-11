# AWS Systems Manager: OpsCenter

* Remediation hub for AWS and hybrid cloud

  * Enable faster issue resolution

  * View, investigate, and resolve operational issues

  * Presents contextual data for diagnosis

  * Associate Automation documents for remediation

* Integrations w/ SNS and AWS SDKs

* Aggregate information from:

  * AWS Config

  * AWS CloudTrail logs

  * Amazon CloudWatch Events

  * Resource descriptions

## OpsCenter benefits

* No need to navigate multiple pages

* OpsItems are aggregated, across services, in a central location

* OpsItems are service specific and have contextually relevant data

* Allows association of related resources

* Helps eliminate noise

* View resolution details of similar opsItems

* Execute Systems Manager Automation documents (runbooks) to resolve issues

## How does OpsCenter work?

* Create operational items (OpsItems)

  * Automatically created via CloudWatch Events

  * Manual creation cia console or API

* OpsItems contain:

  * Title, ID, priority, source, date/time

  * Overall status

  * Related resources or OpsItems

  * Searchable and private operational data

  * Deduplication

* Associate Automation playbooks

  * Automatically remediate common issues

* Reporting using SNS notifications
