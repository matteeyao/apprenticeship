# Storage Gateway

You can use the **AWS Storage Gateway**, in File Gateway mode, to store your on premises data in an existing Amazon S3 bucket. You can deploy **AWS Storage Gateway** as a virtual appliance or purchase a hardware appliance version.

**AWS Storage Gateway** configured as a File Gateway enables you to connect your Amazon S3 bucket using either the Network File System (NFS) or Server Message Block (SMB) protocol w/ local caching. You can transfer your data using an **AWS File Storage Gateway** over the internet or over an **AWS Direct Connect** connection.

## What is Storage Gateway?

> *Integrate cloud storage w/ on-site workloads*

AWS Storage Gateway is a service that connects an on-premises software appliance w/ cloud-based storage to provide seamless and secure integration between an organization's on-premises IT environment and AWS's storage infrastructure. The service enables you to securely store data to the AWS cloud for scalable and cost-effective storage.

Storage gateway is a virtual or physical device that replicates your data into AWS.

AWS Storage Gateway's software appliance is available for download as a virtual machine (VM) image that you install on a host in your datacenter. Storage Gateway supports either VMware ESXi or Microsoft Hyper-V. Once you've installed your gateway and associated it w/ your AWS account through the activation process, you can use the AWS Management Console to create the storage gateway option that is right for you.

## Storage gateway types

**The three different types of Storage Gateway** are as follows:

* File Gateway (NFS & SMB)

    * A way of storing files in S3

* Volume Gateway (iSCSI) → way of storing copies of your hard disk drives or virtual hard disk drives in S3

    * Stored Volumes

    * Cached Volumes

* Tape Gateway (VTL)

    * Virtual tape library

## File Gateway

Files are stored as objects in your S3 buckets, accessed through a Network File System (NFS) mount point. Ownership, permissions, and timestamps are durably stored in S3 in the user-metadata of the object associated w/ the file. Once objects are transferred to S3, they can be managed as native S3 objects, and bucket policies such as versioning, lifecycle management, and cross-region replication apply directly to objects stored in your bucket.

Essentially a way of connecting your on-premise infrastructure to S3. Stores your application data into S3.

![File Gateway](../../../../img/aws/simple-storage-service/storage-gateway/file-gateway.png)

## Volume Gateway

The volume interface presents your applications w/ disk volumes using the iSCSI block protocol.

Data written to these volumes can be asynchronously backed up as point-in-time snapshots on your volumes, and stored in the cloud as Amazon EBS snapshots.

Snapshots are incremental backups that capture only changed blocks. All snapshot storage is also compressed to minimize your storage charges.

Allows you to store your virtual hard disk drives in S3 and looks like EBS snapshots.

Comes in two types:

* **Stored volumes**: Stored Volumes let you store your primary data locally, while asynchronously backing up that data to AWS. Stored volumes provide your on-premises applications w/ low-latency access to their entire datasets, while providing durable, off-site backups. (entire dataset) You can create storage volumes and mount them as iSCSI devices from your on-premises application servers. Data written to your stored volumes is stored on your on-premises storage hardware. This data is asynchronously backed up to Amazon Simple Storage Service (Amazon S3) in the form of Amazon Elastic Block Store (Amazon EBS) snapshots. 1 GB - 16TB in size for Stored Volumes.

![Stored Volumes](../../../../img/aws/simple-storage-service/storage-gateway/stored-volume.png)

* **Cached volumes**: Cached Volumes let you use Amazon Simple Storage Service (Amazon S3) as your primary data storage while retaining frequently accessed data locally in your storage gateway. Cached volumes minimize the need to scale your on-premises storage infrastructure while still providing your applications w/ low-latency access to their frequently accessed data. You can create storage volumes up to 32 TiB in size and attach to them as iSCSI devices from your on-premises application servers. Your gateway stores data that you write to these volumes in Amazon S3 and retains recently read data in your on-premises storage gateway's cache and upload buffer storage. 1 GB - 32 TB in size for Cached Volumes.

    *  **Cached volumes** is a way of caching the most actively used data whereas w/ **Stored Volumes**, it's the entire dataset

![Cached Volumes](../../../../img/aws/simple-storage-service/storage-gateway/cached-volume.png)

## Tape Gateway

Tape Gateway offers a durable, cost-effective solution to archive your data in the AWS Cloud. The VTL interface it provides lets you leverage your existing tape-based backup application infrastructure to store data on virtual tape cartridges that you create on your tape gateway. Each tape gateway is pre-configured w/ a media changer and tape drives, which are available to your existing client backup applications as iSCSI devices. You add tape cartridges as you need to archive your data. Supported by NetBackup, Backup Exec, Veeam etc.

Great solution of transferring your backups to cloud - replicating your tapes to S3. And of course, you can archive them off to Glacier as well to keep your costs low.

![Tape Gateway](../../../../img/aws/simple-storage-service/storage-gateway/tape-gateway.png)


## Learning summary

* **File Gateway**: 

    * NFS
    
    * For flat files, stored directly on S3

* **Volume Gateway**: use iSCSI and consists of two different volumes:

    * **Stored Volumes**: entire dataset stored on site and is asynchronously backed up to S3.

    * **Cached Volumes**: entire dataset stored on S3 and the most frequently accessed data is cached on site.

* **Gateway Virtual Tape Library**

    * Used for backup and uses popular backup applications like NetBackup, Backup Exec, Veeam etc.

    * Great way of getting rid of your physical tapes
