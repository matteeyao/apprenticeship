# AWS DataSync

## What is AWS DataSync?

![AWS DataSync Overview](../../../../img/SAA-CO2/data-migration-services/datasync/diagram.png)

DataSync allows you to move large amounts of data into AWS, and is used on a on-premise data center. You'd install the DataSync agent as an agent on a server that connects to your NAS or file system that will then copy data to AWS and write data from AWS. It's a way of synchronizing your data and it automatically encrypts your data and accelerates transfer over the wide area network (WAN). It performs automatic data integrity checks in transit and at rest as well. Seamlessly connects to Amazon S3, Amazon EFS, or Amazon FSx for Windows File Server to copy data and metadata to and from AWS.

A way of sinking your data to AWS.

AWS DataSync makes it simple and fast to move large amounts of data online between on-premises storage and Amazon S3. DataSync manages many of the tasks related to data transfers that can slow down migrations. You can also reduce the burden of your IT operations, including running your own instances, handling encryption, managing scripts, network optimization, and data integrity validation.

DataSync can transfer hundreds of terabytes and millions of files at speeds up to 10 times faster than open-source tools. You can use it to migrate active data sets or archives to AWS, transfer data to the cloud for timely analysis and processing, or replicate data to AWS for business continuity.

## Learning summary

* Used to move **large amounts** of data from on-premises to AWS.

* Used w/ **NFS**- and **SMB**-compatible file systems.

* **Replication** can be done hourly, daily, or weekly.

* Install the **DataSync agent** to start the replication.

* Can be used to replicate **EFS** to **EFS**.

    * Through this method, you would install the DataSync agent on an EC2 instance that was connected to a EFS and then you can use that DataSync agent to replicate your EFS to another copy or another EFS in the cloud.
