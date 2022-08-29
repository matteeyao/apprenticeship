# AWS Systems Manager: Distributor

* Securely store and distribute software packages

  * Software agents

  * Applications

  * Drivers

* Simplify and scale distribution

  * Central repository w/ version control

  * Share w/ other AWS accounts

  * Control access to packages using IAM

* Install on demand or on a schedule

* Install automatically on new instances

## How does Distributor work?

* Managed Instance prerequisites:

  * SSM agent installed

  * Granted permissions via IAM

  * Connectivity w/ Systems Manager endpoints

* Create a package

  * Specify Amazon S3 bucket to store package

  * Provide software files (rpm, MSI, or deb)

  * Specify platform, version, and architecture

  * Validate install/uninstall/update scripts

* Install package

  * One time using Run Command

  * Scheduled install w/ State Manager associations
