# Hybrid Cloud Storage with AWS Storage Gateway

The **Storage Gateway** service facilitates hybrid cloud storage use cases. It integrates w/ other AWS services for storage, backup, management, and security, while still integrating w/ on-premises environments.

Storage Gateway helps you store your on-premises data in the cloud, where you can take advantage of additional AWS Cloud services to help monitor and manage it.

Using **Storage Gateway**, on-premises applications leverage the following AWS Cloud features:

* **Storage** ▶︎ Connect to storage services such as Amazon Simple Storage Service (Amazon S3), Amazon S3 Glacier Flexible Retrieval , Amazon S3 Glacier Deep Archive, Amazon Elastic Block Store (Amazon EBS), Amazon FSx for Windows File Server, and AWS Backup.

* **Management and monitoring** ▶︎ Use the Storage Gateway management console to manage and monitor your Storage Gateway and its associated resources. Also use other AWS services such as the following:

  * **AWS Identity and Access Management (IAM)** to secure access to the service and resources

  * **AWS Key Management Service (AWS KMS)** for encrypting data

  * **AWS CloudTrail** for logging account activity

  * **Amazon CloudWatch** for monitoring

  * **Amazon EventBridge** for monitoring alarms

## Key features and benefits

With **Storage Gateway**, you can bring your data into AWS for processing in the cloud. You can also back up, archive, and tier your storage or add it to your on-premises environment to help you meet your business and regulatory compliance requirements.

### Architecture and end-to-end data flow

The Storage Gateway service consists of in-cloud and on-premises components. The component that deploys on premises is referred to as a *Storage Gateway appliance*.

This image depicts the Storage Gateway architecture. It describes the end-to-end flow of data from the on-premises location to the gateway appliance, over the internet, and up to the AWS Cloud.

![Fig. 1 Storage Gateway architecture](../../../../../img/SAA-CO2/storage-gateway/hybrid-cloud-solutions/hybrid-cloud-storage-with-aws-storage-gateway/diag01.png)

> ### On-premises environments that benefit from cloud storage
>
> * Windows or Linux file server or user workstation
>
> * Backup server
>
> * Storage area network (SAN) system or network-attached storage (NAS) device that must be expanded

> ### Standard protocols
>
> W/ standard storage protocols, your on-premises environment can access cloud storage much like local storage is accessed. These include the following:
>
> * Network file system (NFS)
>
> * Server message block (SMB)
>
> * Internet Small Computer Systems Interface (iSCSI)
>
> * iSCSI virtual tape library (VTL)

> ### Storage Gateway appliance
>
> * The appliance connects to the Storage Gateway service securely over the internet.
>
> * It deploys on premises, connected w/ a local cache, providing low-latency access to frequently accessed data.
>
> * Deployment options include VMware, Kernel-based Virtual Machine (KVM), a hardware appliance, and Amazon Elastic Compute Cloud (Amazon EC2).

> ### Secure and optimized uploads
>
> Connect to AWS over the internet, using HTTPS.
>
> You can also use AWS Direct Connect for an even more secure and private connection.
>
> You can use AWS Virtual Private Network (AWS VPN) services for a private and dedicated network connection from your on premises to the AWS Cloud.

> ### Storage Gateway service
>
> Storage Gateway is an AWS managed service that provides hybrid cloud solutions.
>
> * It integrates your on-premises environments w/ AWS storage.
>
> * Gateway types include Amazon S3 File Gateway, Amazon FSx File Gateway, Tape Gateway, and Volume Gateway.

> ### Storage
>
> W/ Storage Gateway, you can connect to and use key cloud storage services such as Amazon S3, S3 Glacier Flexible Retrieval, S3 Glacier Deep Archive, Amazon FSx for Windows File Server, Amazon EBS, and AWS Backup.

> ### Security, management, and monitoring
>
> Storage Gateway integrates w/ other AWS services for security, management, and monitoring.
>
> This includes services such as **AWS KMS**, **IAM**, **CloudTrail**, and **CloudWatch**.

## Storage Gateway Features

Storage Gateway is fast and straightforward to deploy. You can integrate it with your existing environments and access AWS storage in a frictionless manner. Let's take a look at five common features.

> ### Standard storage protocols
>
> Storage Gateway uses standard storage protocols—namely, NFS, SMB, iSCSI, or iSCSI VTL—to connect your local production or backup applications to AWS Cloud storage. With its protocol conversion and device emulation, you can do the following:
>
> * Access block data on volumes managed by Storage Gateway on top of Amazon S3.
>
> * Store files as native S3 objects or in fully managed cloud file shares with Amazon FSx for Windows File Server.
>
> * Keep virtual tape backups online in a VTL backed by Amazon S3 or move the backups to a tape archive tier on S3 Glacier Flexible Retrieval and S3 Glacier Deep Archive. 

> ### Low-latency access to your data
>
> The Storage Gateway appliance provides your applications with low-latency access to data by maintaining either a full-volume copy of your stored volumes or a volume cache for your cached volumes. The cache is Least Recently Used (LRU) managed.
>
> As your applications write data to the AWS storage, the gateway first stores the data in the on-premises disks that are used for cache storage. Then, the gateway uploads the data to AWS. The cache store acts as the on-premises durable store for data. If your application requests data, the gateway first checks the cache storage for the data, before checking AWS.

> ### Optimized data transfers
>
> Storage Gateway uses optimization such as multi-part management, automatic buffering, and delta transfers. Data compression is applied for block and virtual tape data. The data transfers are optimized to reduce cost and the amount of data that is transferred in and out of AWS.

> ### Security and compliance
>
> Storage Gateway supports security features, access control, and security compliance certifications. Data is encrypted at transit and at rest. Your data at rest is encrypted by default using **Amazon S3 server-side encryption (S3-SSE)**. Alternatively, Storage Gateway integrates with **AWS KMS**, so you can choose to encrypt your data using your own encryption keys. By integrating with IAM, you manage and secure access to your data.

> ### High availability on VMware
>
> Storage Gateway provides high availability on VMware through a set of health checks integrated with VMware vSphere High Availability (VMware HA). With this integration, Storage Gateway deployed in a VMware environment on premises or in VMware Cloud on AWS will automatically recover from most service interruptions in under 60 seconds. This protects storage workloads against hardware, hypervisor, or network failures, storage errors, or software errors, such as connection timeouts and file share or volume unavailability.

## Storage Gateway types

To support numerous hybrid cloud use cases, the Storage Gateway service offers four different types of gateways: **S3 File Gateway**, **FSx File Gateway**, **Tape Gateway**, and **Volume Gateway**. They seamlessly connect on-premises applications to cloud storage and cache data locally for low-latency access. 

> ### Amazon S3 File Gateway
>
> **S3 File Gateway** provides native file access to Amazon S3 for backups, archives, and ingest for data lakes.
>
> **S3 File Gateway** presents a file-based interface to Amazon S3, which appears as a network file share. With it, you can store files that support your latency-sensitive applications and workloads requiring local caching and file protocol access. S3 File Gateway moves your file data into an object format, which is highly durable and cost efficient.
>
> S3 File Gateway supports data lakes, backups, and machine learning (ML) workflows. You can store file data as objects in Amazon S3 cloud storage using file protocols such as NFS and SMB. Objects written through S3 File Gateway can be directly accessed in Amazon S3.

> ### FSx File Gateway
>
> FSx File Gateway provides native file access to Amazon FSx for on-premises group file shares and home directories.
>
> **FSx File Gateway** optimizes on-premises access to Windows file shares on Amazon FSx, helping you access FSx for Windows File Server data with low latency and conserving shared bandwidth. A local cache of frequently used data that you can access is stored, providing faster performance and reduced data transfer traffic. FSx File Gateway stores your data natively as files rather than as objects.
>
> FSx File Gateway is a solution for replacing on-premises NAS, such as end-user home directories and departmental or group servers, with cloud storage. It faciliatates user or team file shares and file-based application migration shares in Amazon FSx for Windows File Server, using the SMB protocol. Files written through FSx File Gateway can be directly accessed in FSx for Windows File Server.

> ### Tape Gateway
>
> **Tape Gateway** replaces physical tape infrastructure using Amazon S3 archive tiers for long-term retention.
>
> **Tape Gateway** is a cloud-based virtual tape library (VTL). It presents your backup application with a VTL interface consisting of a media changer and tape drives. You can create virtual tapes in your VTL using the Storage Gateway console. Your backup application can read data from or write data to virtual tapes by mounting them to virtual tape drives using the virtual media changer.
>
> Virtual tapes are discovered by your backup application using its standard media inventory procedure. Virtual tapes are available for immediate access and are backed by **Amazon S3**. You can also archive tapes. Archived tapes are stored in **Amazon S3 Glacier Flexible Retrieval** or **Amazon S3 Glacier Deep Archive**.

> ### Volume Gateway
>
> Volume Gateway provides block storage volumes with snapshots, AWS Backup integration, and cloud recovery.
>
> **Volume Gateway** provides an iSCSI target, with which you can create block storage volumes and mount them as iSCSI devices from your on-premises or EC2 application servers. The Volume Gateway runs in either a cached or stored mode.
>
> * In the cached mode, your primary data is written to Amazon S3, while retaining your frequently accessed data locally in a cache for low-latency access.
>
> * In the stored mode, your primary data is stored locally and your entire dataset is available for low-latency access while asynchronously backed up to AWS.
>
> In either mode, you can take point-in-time copies of your volumes, which are stored as Amazon EBS snapshots in AWS. With this feature, you can make space-efficient versioned copies of your volumes for data protection, recovery, migration, and various other copy data needs.

### Choosing the right file gateway

Storage Gateway provides two gateway types for storing files that support your latency-sensitive applications and workloads that require local caching and file protocol access. If you want to move your file data into an object format that is highly durable and cost efficient, use S3 File Gateway. If you want to keep it stored natively as file data, use FSx File Gateway.

* **S3 File Gateway** supports data lakes, backups, and machine learning (ML) workflows. You can store file data as objects in Amazon S3 cloud storage using file protocols such as NFS and SMB. Objects written through **S3 File Gateway** can be directly accessed in Amazon S3.

* **FSx File Gateway** is a solution for replacing on-premises NAS—such as end-user home directories and departmental or group servers—with cloud storage. It facilitates user or team file shares and file-based application migration shares in FSx for Windows File Server, using the SMB protocol. Files written through **FSx File Gateway** can be directly accessed in FSx for Windows File Server.

## Solution deploys as a stateless gateway appliance

The gateway is straightforward to deploy and can use your existing virtual infrastructure and hypervisor investments. It can also be installed in your data center or remote offices as a hardware appliance. The gateway software running as a virtual machine (VM) or on the hardware appliance is stateless, so you can easily create and manage new instances of your gateway as your storage needs evolve. **Tape Gateway** can also be deployed using AWS Snowball Edge, providing offline cloud capabilities.

![Fig. 1 Deployment options for the Tape Gateway appliance include on premises (as a VM appliance or a hardware appliance), offline transfer using Snowball Edge, or in AWS Cloud (as an EC2 instance).](../../../../../../img/SAA-CO2/storage-gateway/tape-gateway/hybrid-cloud-solutions/hybrid-cloud-storage-with-aws-storage-gateway/diag02.png)

The Storage Gateway deployment consists of two components: a cloud component and an on-premises component referred to as the *gateway appliance*.

* The gateway appliance you are using depends on the storage solution you require and where you will be deploying the gateway appliance.

* The gateway appliance will need to be hosted, use a local cache, and securely connect to the AWS Storage Gateway service in the cloud for native data storage in AWS.

### Host platform options

Deployment options include the following:

* Download, deploy, and activate the Storage Gateway VM image on any of the supported host platforms.

* Create an Amazon EC2 instance to deploy your gateway in the AWS Cloud.

* Order, deploy, and activate a Storage Gateway hardware appliance.

* Deploy using AWS Snowball Edge for offline cloud capabilities.

AWS Storage Gateway provides public, Amazon Virtual Private Cloud (Amazon VPC), and Federal Information Processing Standards (FIPS) service endpoints. This provides you with options to deploy and connect your gateway to Storage Gateway in a framework that best suits your networking and security needs. You can connect a gateway to the service either using public internet or through AWS Direct Connect.

## Benefits, use cases, and gateway types of AWS Storage Gateway

Some of your applications may need to remain on-premises for performance or compliance reasons, or they may simply be too complex to move completely into the cloud.

While the cloud offers a large variety of services that can help modernize your IT infrastructure, you may also be considering a gradual transition to the cloud while still wanting to benefit from cloud capabilities in your data center.

**AWS Storage Gateway** is a fast, simple way to get started with using the cloud from your data centers, remote offices, and edge locations. Storage Gateway is a hybrid cloud storage service that provides low-latency, on-premises access to virtually unlimited cloud storage. Your file, database, and backup applications can continue to run without changes and, once your data is safely and securely in AWS, it's available for all your current and future cloud initiatives since it can be easily accessed and processed by many other AWS services. 

In just minutes, you can be up and running in the cloud using **AWS Storage Gateway**. Using **Storage Gateway**, your on-premises applications can access data stored in the cloud via standard storage protocols so there's no need to change application code. **Storage Gateway** works as a file share, as a virtual tape library, or as a block storage volume. Applications write data to the **Amazon S3 File Gateway** as files, which are stored in Amazon S3 as objects. 

Applications can also write data as files to the **Amazon FSx File Gateway**, which are stored in fully managed file shares in **Amazon FSx for Windows File Server**. The **Tape Gateway** presents a virtual tape library on your local network and is compatible with all major backup software. And the **Volume Gateway** attaches to your application servers as iSCSI block storage. No matter which type of gateway you're using, data is cached locally and moved to the cloud with optimized data transfers. 

Many storage gateway customers begin to use AWS by moving backups to the cloud, shifting on-premises storage to cloud-backed file shares, and ensuring low-latency access for on-premises applications to access and process cloud data.

## Tape Gateway 

In this module, you will dive deep into the **Tape Gateway**. With Tape Gateway, you will no longer need to store media at offsite facilities and migrate tape media from one generation to the next manually. Use Tape Gateway to do the following:

1. Replace physical tape libraries with VTLs.

2. Implement long-term storage retention for disaster recovery and compliance needs.

3. Simplify management and lower total cost of ownership of your backup library.

Tape Gateway offers a durable, cost-effective solution to archive your data into AWS. With its VTL interface, you use your existing tape-based backup infrastructure to store data on virtual tape cartridges that you create on your Tape Gateway. Each Tape Gateway is preconfigured with a media changer and tape drives. These are available to your existing client backup applications as iSCSI devices. You add virtual tapes manually or automatically as you need to archive your data.

![Fig. 3 Tape Gateway](../../../../../../img/SAA-CO2/storage-gateway/tape-gateway/hybrid-cloud-solutions/hybrid-cloud-storage-with-aws-storage-gateway/diag03.png)

## Volume Gateway

In this module, you will dive deep into the **Volume Gateway**. **Volume Gateway** offers block storage for on-premises compute that facilitates the following:

1. Backups for your local applications in the cloud

2. Disaster and recovery solution plans based on EBS snapshots

3. Cached volume clones

Volume Gateway provides block storage durably stored by AWS in Amazon S3 buckets. This gateway type provides an iSCSI mount target to your applications. Data is cached locally, compressed in transit and at rest, and securely transferred to AWS. Volume Gateway can be deployed in two different modes, cached and stored, based on your data access needs.

![Fig. 4 Volume Gateway](../../../../../../img/SAA-CO2/storage-gateway/hybrid-cloud-solutions/hybrid-cloud-storage-with-aws-storage-gateway/diag04.jpeg)

Back up your on-premises Volume Gateway volumes using the service’s native snapshot scheduler or by using the AWS Backup service. With either service, volume backups are stored as Amazon EBS snapshots in AWS.

With a basic understanding of Storage Gateway, its architecture, and end-to-end data flow, let's now take a closer look at **Volume Gateway**.
