# Store and Access Data with Volume Gateway

## About Volume Gateway

Volume Gateway is hybrid cloud block storage with local caching. Volume Gateway presents your on-premises applications with block storage by using the iSCSI protocol.

Depending on your on-premises latency and caching needs, Volume Gateway operates in either cached mode or stored mode. Your block storage data is stored in AWS in an Amazon S3 service bucket instead of a customer bucket. You can manage your data that is stored in Amazon S3 through the creation of Amazon EBS snapshots, by using the **Storage Gateway** service's native snapshot scheduler or **AWS Backup**.

Snapshots are incremental backups that capture only blocks that have changes. All snapshot storage is compressed to minimize your storage charges.

## Primary use cases

> ### Hybrid cloud file services
>
> Use Volume Gateway in conjunction with Windows and Linux file servers to provide scalable storage for on-premises file applications with cloud recovery options. With a cached volume architecture, you get the benefit of scalable cloud storage and data protection for growing file stores that demand low latency local access for frequently used data. 
>
> **Features**:
>
> * Industry standard iSCSI connectivity
>
> * Local cache for frequently accessed data
>
> * Integrates seamlessly with on-premises applications
>
> * Data stored durably in Amazon S3 as Amazon EBS snapshots
>
> **Benefits**:
>
> * Reduce on-premises storage for backups
>
> * Flexible data protection and recovery
>
> * Petabyte scalability

> ### Backup and disaster recovery
>
> Back up local applications and implement disaster recovery strategies based on EBS snapshots or cached volume clones.
>
> Volume Gateway integrates with AWS Backup to extend on-premises application protection. Using AWS Backup with Volume Gateway helps you to centralize backup management and meet compliance requirements with customizable scheduled backup policies, including retention and expiration rules.
>
> **Features**:
>
> * Quick restore from point-in-time snapshots
>
> * Incremental backups that capture only changed blocks to reduce storage costs
>
> * Customize backup lifecycle policies
>
> **Benefits**:
>
> * Reduce on-premises capacity and operational burden
>
> * Manage and monitor backups across multiple Volume Gateways from a central view
>
> * Schedule snapshots
>
> * Space efficient versioned copies

> ### Migration of application data
>
> Migrate on-premises data to Amazon EBS to use with Amazon EC2 based applications. An initial snapshot captures the full volume, which can be used for migration testing. Then, incremental snapshots can be used to migrate changed blocks and you can complete the final copy when you are ready to pause the application on premises and cut over.
>
> **Features**:
>
> * Point-in-time snapshots
>
> * Volume data stored durably in Amazon S3 as EBS snapshots
>
> * Bandwidth optimized, only changes are transferred
>
> **Benefits**:
>
> * On-demand compute capacity
>
> * Cost optimization
>
> * No rewrites to your on-premises applications needed to use cloud storage
>
> * Create volumes from your Storage Gateway snapshots to mount with Amazon EC2 instances
>
> * Data stored cost effectively and centrally in the cloud

## How Volume Gateway works

The Volume Gateway cached mode is deployed into your on-premises or AWS Cloud environment as a VM running on VMware ESXi, KVM, Microsoft Hyper-V hypervisor, Amazon EC2, or a physical gateway hardware appliance. Stored mode is only available w/ on-premises host platform options.

When activated, you can create gateway storage volumes. These volumes are then mapped to on-premises direct-attached storage (DAS) or SAN disks. Start w/ either new disks or disks already holding data and mount these storage volumes to your on-premises application servers as iSCSI devices. Using the iSCSI protocol, clients (called *initiators*) send Small Computer System Interface (iSCSI) commands to storage devices (called *targets*) on remote servers. As your on-premises applications write data to and read data from a gateway's storage volume, this data is stored and retrieved fom the volume's assigned disk.

![Fig. 1 Volume Gateway architecture](../../../../../../img/SAA-CO2/storage-gateway/hybrid-cloud-solutions/store-and-access-data/volume-gateway/diag01.jpeg)

The cloud storage solution includes an on-premises gateway appliance; in this case, it is Volume Gateway, which interacts with the Storage Gateway fully managed service. It provides durable storage for your volume data stored in Amazon S3 as EBS snapshots using the iSCSI protocol.

## Volume Gateway modes

When connecting with the Volume Gateway, you can run the gateway in two different modes: cached and stored.

> ### Cached Volume
>
> In cached mode, primary data is stored in Amazon S3 and you retain your frequently accessed data locally in the cache. This results in a substantial cost savings for primary storage because it reduces the need to scale your storage on premises while retaining low-latency access to your frequently accessed data. Any data that your applications modify is moved to the upload buffer, where it is prepared for uploading asynchronously to Amazon S3.

> ### Stored Volume
>
> If you need low-latency access to your entire dataset, first configure your on-premises gateway to store all your data locally. Then asynchronously back up point-in-time snapshots of this data to Amazon S3. This configuration provides durable and inexpensive offsite backups that you can recover to your local data center or Amazon EC2. For example, if you need replacement capacity for disaster recovery, you can recover the backups to Amazon EC2.
>
> Stored mode is only available with on-premises host platform options.

To prepare data for upload to Amazon S3, your gateway also stores incoming data in a staging area, referred to as an *upload buffer*. You can use on-premises DAS or SAN disks for working storage. Your gateway uploads data from the upload buffer over an encrypted Secure Sockets Layer (SSL) connection to the Storage Gateway running in the AWS Cloud. Storage Gateway then stores the data encrypted in Amazon S3 as Amazon EBS snapshots.

Depending on the storage solution you deploy, the gateway requires the following additional storage.

> ### Cache storage
>
> The cache store acts as the on-premises durable store for data that is pending upload to Amazon S3 from the upload buffer and is required for a cached Volume Gateway.
>
> When your application performs I/O on a volume, the gateway saves the data to the cache storage for low-latency access. As your application requests data from a volume, the gateway first checks the cache storage for the data before downloading the data from AWS.

> ### Upload buffer
>
> The upload buffer provides a staging area for the data before the gateway uploads the data to Amazon S3. Your gateway uploads this buffer data over an encrypted SSL connection to AWS. 
>
> Both cached volume and storage volume gateway deployments require upload buffer storage.

## Volumes as EBS snapshots

You can take incremental backups, called *snapshots*, of your storage volumes. When you take a new snapshot, only the data that has changed since your last snapshot is stored. You can initiate snapshots on a schedule or one-time basis.

To recover a backup of your data, you can restore an Amazon EBS snapshot to an on-premises gateway storage volume. You can also use the snapshot as a starting point for a new Amazon EBS volume, which you can then attach to an Amazon EC2 instance for processing in the cloud.

## Choosing Volume Gateway for your workloads

Review several key questions to help determine if Volume Gateway will work for your workloads.

> 1. **Do you want to centrally manage and automate your volume snapshots?**
> 
> Using **AWS Backup**, you can set backup retention and expiration rules so that you no longer need to develop custom scripts or manually manage the point-in-time backups of your Volume Gateway volumes. You can manage and monitor backups across multiple Volume Gateways and other AWS resources such as EBS volumes and Amazon Relational Database Service (Amazon RDS) databases from a central view.
> 
> 2. **Are your volume sizes steady and predictable?**
> 
> If yes, Volume Gateway is beneficial for your block storage needs. Volume resizing for the gateway is not supported. To decrease the storage capacity, you will need to create a new gateway and migrate your data to the new gateway. To increase storage capacity, you add new disks to the gateway instead of expanding disks previously allocated.
> 
> 3. **Do you often use processes that require reading all of the data on the entire volume such as virus scans?**
> 
> If so, you will want to consider *stored mode*. This use case would not benefit from *cached mode*, as all data would have to be downloaded from Amazon S3.
> 
> 4. **Do you want to access snapshot data using the Amazon S3 console or APIs?**
> 
> EBS snapshots are stored in an S3 service bucket instead of a customer bucket. They are accessible from the Storage Gateway and Amazon EBS management console and cannot be directly accessed using the Amazon S3 management console or application programming interfaces (APIs).

By using Storage Gateway, you can help protect your on-premises business applications that use Storage Gateway volumes for cloud-backed storage. Back up your on-premises Storage Gateway volumes using the **native snapshot scheduler in Storage Gateway** or **AWS Backup**. In both cases, Storage Gateway volume backups are stored as Amazon EBS snapshots in AWS.

## Centralize storage backups

**AWS Backup** is a centralized backup service that makes it straightforward and cost effective for you to back up your application data across AWS services in both the AWS Cloud and on premises. Doing this helps you meet your business and regulatory backup compliance requirements. **AWS Backup** is directly integrated into the Storage Gateway console and makes protecting your AWS storage volumes, databases, and file systems simple by providing a central place where you can perform the following:

> AWS Backup is directly integrated within the Storage Gateway console.

* Configure and audit the AWS resources that you want to back up.

* Automate backup scheduling.

* Set retention policies.

* Monitor all recent backup and restore activity.
