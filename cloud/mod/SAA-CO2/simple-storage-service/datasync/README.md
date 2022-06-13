# AWS DataSync

## What is AWS DataSync?

![AWS DataSync overview](../../../aws/simple-storage-service/datasync/datasync.png)

DataSync allows you to move large amounts of data into AWS, and is used on a on-premise data center. You'd install the DataSync agent as an agent on a server that connects to your NAS or file system that will then copy data to AWS and write data from AWS. It's a way of synchronizing your data and it automatically encrypts your data and accelerates transfer over the wide area network (WAN). It performs automatic data integrity checks in transit and ar rest as well. Seamlessly connects to Amazon S3, Amazon EFS, or Amazon FSx for Windows File Server to copy data and metadata to and from AWS.

A way of sinking your data to AWS.

## Learning summary

* Used to move **large amounts** of data from on-premises to AWS.

* Used w/ **NFS**- and **SMB**-compatible file systems.

* **Replication** can be done hourly, daily, or weekly.

* Install the **DataSync agent** to start the replication.

* Can be used to replicate **EFS** to **EFS**.

    * Through this method, you would install the DataSync agent on an EC2 instance that was connected to a EFS and then you can use that DataSync agent to replicate your EFS to another copy or another EFS in the cloud.
