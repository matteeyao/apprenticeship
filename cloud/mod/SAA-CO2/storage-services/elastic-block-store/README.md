# Elastic Block Store (EBS)

> Amazon Elastic Block Store (EBS) provides persistent block storage volumes for use w/ Amazon EC2 instances in the AWS Cloud. Each Amazon EBS volume is automatically replicated within its Availability Zone to protect you from component failure, offering high availability and durability

Amazon Elastic Block Store (Amazon EBS) is an easy-to-use, high-performance, block storage service designed for use w/ Amazon Elastic Compute CLoud (Amazon EC2) for both throughput and transaction intensive workloads at any scale. A broad range of workloads is widely deployed on Amazon EBS, such as relational and nonrelational databases, enterprise applications, containerized applications, big data analytics engines, file systems, and media workflows.

You can choose from different volume types to balance optimal price and performance. You can change volume types, tune performance, or increase volume size w/o disrupting your critical applications, so that you have cost-effective storage when you need it.

Designed for mission-critical systems, Amazon EBS volumes are replicated within an Availability Zone and can easily scale to petabytes of data. Also, you can use Amazon EBS Snapshots w/ automated lifecycle policies to back up your volumes in Amazon Simple Storage Service (Amazon S3) while ensuring geographic protection of your data and business continuity.

AWS recommends Amazon EBS for data that must be quickly accessible and requires long-term persistence. EBS volumes are particularly well suited for use as the primary storage for file systems, databases, or any applications that require fine granular updates and access to raw, unformatted, block-level storage. Amazon EBS is well suited to both database-style applications that rely on random reads and writes, and to throughput-intensive applications that perform long, sequential reads and writes.

EBS volumes behave like raw, unformatted block devices. You can mount these block devices as EBS volumes on your EC2 instances. EBS volumes that are attached to an EC2 instance are exposed as raw block storage volumes that persist independently from the life of the instance. You can create a file system on top of these volumes or use them in any way you would use a block device (such as a hard drive). You can dynamically change the configuration of a volume attached to an EC2 instance, unlike traditional disk drives that come in fixed sizes.

You can choose from six different EBS volume types to balance optimal price and performance. You can achieve single-digit millisecond latency for high-performance database workloads such as SAP HANA or gigabyte-per-second throughput for large, sequential workloads such as Apache Hadoop. You can change EBS volume types, tune performance, or increase volume size w/o disrupting your critical applications. Amazon EBS provides you cost-effective block storage when you need it.

Designed for mission-critical systems, EBS volumes are replicated within an AWS Availability Zones and can easily scale to petabytes of data. Also, you can use EBS snapshots w/ automated lifecycle policies to back up your volumes in Amazon Simple Storage Service (Amazon S3) while ensuring geographic protection of your data and business continuity.

W/ Amazon EBS, you pay for only the storage and resources that you provision.

## AWS block storage portfolio

The AWS block storage portfolio consists of two types of block storage services-instance storage and Amazon EBS-and an integrated snapshot service.

> ### Instance storage
>
> Instance storage is temporary block-level storage attached to host hardware that is ideal for storing information that frequently changes or is replicated across multiple instances.
>
> * Instance storage units are referred to as instance stores and are directly associated w/ an Amazon EC2 instance. Instance stores are also referred to as EC2 instance stores.
>
> * An instance store is nonpersistent and is terminated when the associated EC2 instance is terminated.
>
> * Instance stores resemble Amazon EBS storage in initial configuration options. However, they functionally most closely resemble a direct attached disk drive. The available storage type is directly tied to the EC2 instance type.

> ### Amazon EBS storage
>
> Amazon EBS is a block storage service designed for use w/ Amazon EC2. Amazon EBS scales to support virtually any workload and any volume size.
>
> * EBS volumes attach to your EC2 instances and can be moved from one EC2 instance to a different EC2 instance.
>
> * EBS volumes are persistent, which means they persist independently from the life of your EC2 instance. If an EC2 instance goes down, your volume and, most importantly, your data on that volume remain available to attach to a different EC2 instance.

> ### Snapshots
>
> Snapshots are incremental, point-in-time copies of your data stored on your EBS volumes. You can use snapshots to restore new volumes, expand the size of a volume, or move columns across Availability Zones.
>
> Snapshots let you geographically protect your data and achieve business continuity. You can use Amazon Data Lifecycle Manager (Amazon DLM) to automate snapshot management w/o any additional overhead or cost.

> **Essentially, a virtual hard disk drive in the cloud**
>
> * Persist independently from instance
>
> * Used like a physical hard drive
>
> * Automatically replicated
>
> * Attached to any instance in the same AZ
>
> * One EBS volume to one EC2 instance
>
> * One instance to many EBS volumes
>
> * EBS volumes can retain data after EC2 instance termination
>
> * Allow point-in-time snapshots to S3 GiB increments

These snapshots can be used as the starting point for new EBS volumes and to protect data for long-term durability. The same snapshot can be used to instantiate as many volumes as desired. These snapshots can be copied across AWS Regions, making it easier to leverage multiple AWS Regions for geographical expansion, data center migration, and disaster recovery. Sizes for Amazon EBS volumes range from 1 gigabyte to 16 terabytes, depending on the volume type and are allocated in 1 gigabyte increments.

## EBS Simplified

> An Amazon EBS volume is a durable, block-level storage device that you can attach to a single EC2 instance. You can think of EBS as a cloud-based virtual hard disk. You can use EBS volumes as primary storage for data that requires frequent updates, such as the system drive for an instance or storage for a database application. You can also use them for throughput-intensive applications that perform continuous disk scans.

## EBS Key Details

* EBS volumes persist independently from the running life of an EC2 instance.

> ### Persistent
>
> Amazon EBS volumes are durable and persistent by default. Your EBS volume survives even if your EC2 instance is terminated. Your data is preserved for your future use and persists until you decide to delete it.
>
> EBS volumes are managed independently from the Amazon EC2 instances to which they are attached. You can detach an existing EBS volume from an EC2 instance and reattach it to a different EC2 instance. This provides you the ability to change EC2 instance types to meet your performance requirements and optimize your Amazon EC2 costs. It also means that spot instances can be stopped or terminated w/o losing your data.

* Each EBS volume is automatically replicated within its Availability Zone to protect from both component failure and disaster recovery (similar to Standard S3).

* EBS Volumes offer 99.999% SLA

> ### High availability and high durability
>
> EBS volumes are designed to be highly available, reliable, and durable at no additional charge to you. EBS volume data is replicated across multiple servers in an Availability Zone to prevent the loss of data from the failure of any single component.
>
> Amazon EBS offers a higher durability io2 volume that is designed to provide 99.999 percent durability w/ an AFR of 0.001 percent, where failure refers to a complete or partial loss of the volume. For example, if you have 100,000 EBS io2 volumes running for 1 year, you should expect only one io2 volume to experience a failure. This makes io2 ideal for business-critical applications such as SAP HANA, Oracle, Microsoft SQL Server, and IBM DB2 that will benefit from higher uptime. io2 volumes are 2,000 times more reliable than typical commodity disk drives, which fail w/ an annual failure rate (AFR) between 2 and 4 percent.
>
> All other Amazon EBS volumes are designed to provide 99.8 - 99.95 durability w/ an AFR of 0.1 to 0.2 percent. Amazon EBS also supports a snapshot feature, which is a good way to take point-in-time backups of your data.

* Wherever your EC2 instance is, your volume for it is going to be in the same availability zone

* An EBS volume can only be attached to one EC2 instance at a time.

* After you create a volume, you can attach it to any EC2 instance in the same availability zone.

* Amazon EBS provides the ability to create snapshots (backups) of any EBS volume and write a copy of the data in the volume to S3, where it is stored redundantly in multiple Availability Zones.

> ### Backups
>
> AWS Backup supports backing up your EBS volumes. AWS Backup allows you to centralize and automate data protection across AWS services. AWS Backup offers a cost-effective, fully managed, policy-based service that further simplifies data protection at scale.
>
> AWS Backup also helps you support your regulatory compliance obligations and meets your business continuity goals. Together w/ AWS Organizations, AWS Backup enables you to centrally deploy data protection (backup) policies to configure, manage, and govern your backup activity across your organization's AWS accounts and resources.
>
> AWS Backup supports many AWS services, including EC2 instances, EBS volumes, Amazon Relational Database Service (Amazon RDS) databases (including Amazon Aurora clusters), Amazon DynamoDB tables, Amazon EFS, FSx for Lustre, FSx for Windows File Server, and AWS Storage Gateway volumes.

* An EBS snapshot reflects the contents of the volume during a concrete instant in time.

> ### Snapshots
>
> Amazon EBS provide the ability to save point-in-time snapshots of your volumes to Amazon S3. EBS snapshots are stored incrementally. Only the blocks that have changed after your last snapshot are saved, and you are billed only for the changed blocks.
>
> When you delete a snapshot, you remove only the data not needed by any other snapshot. All active snapshots contain all the information needed to restore the volume to the instant at which that snapshot was taken. The time to restore changed data to the working volume is the same for all snapshots.
>
> Snapshots can be used to instantiate multiple new volumes, expand the size of a volume, or move volumes across Availability Zones. When a new volume is created, you can choose to create it based on an existing EBS snapshot. In that scenario, the new volume begins as an exact replica of the snapshot.
>
> **Key snapshot feature capabilities**
>
> * Direct read access of EBS snapshots
>
> * Ability to create EBS snapshots from any block storage
>
> * Immediate access to EBS volume data
>
> * Instant full performance on EBS volumes restored from snapshots using Fast Snapshot Restore (FSR)
>
>   * Additional hourly charge for FSR
>
> * Ability to resize EBS volumes
>
> * Ability to share EBS snapshots
>
> * Ability to copy EBS snapshots across AWS Regions

* An image (AMI) is the same thing, but includes an operating system and a boot loader so it can be used to boot an instance.

* AMIs can be thought of as pre-baked, launch-able servers. AMIs are always used when launching an instance.

* When you provision an EC2 instance, an AMI is actually the first thing you are asked to specify. You can choose a pre-made AMI or choose your own made from an EBS snapshot.

* You can also use the following criteria to help pick your AMI:

    * Operating System

    * Architecture (32-bit or 64-bit)

    * Region

    * Launch permissions

    * Root Device Storage

* You can copy AMIs into entirely new regions.

* When copying AMIs to new regions, Amazon won't copy launch permissions, user-defined tags, or Amazon S3 bucket permissions from the source AMI to the new AMI. You must ensure those details are properly set for the instances in the new region.

* You can change EBS volumes on the fly, including the size and storage type

> ### Multiple volume type options
>
> Amazon EBS provides multiple types that allow you to optimize storage performance and cost for a broad range of applications. These volume types are divided into two major categories: SSD-backed storage for transactional workloads, such as databases, virtual desktops, and boot volumes, and HDD-backed storage for throughput-intensive workloads, such as `MapReduce` and log processing.
>
> * SSD-based volumes include two levels to meet your application requirements: General Purpose SSD volumes and Provisioned IOPS SSD volumes.
>
>   * General Purpose SSD volumes (gp3 and gp2) balance price and performance for transactional applications, including virtual desktops, test and development, and interactive gaming applications.
>
>   * Provisioned IOPS SSD volumes are the highest performance EBS volumes (io2 and io1) for your most demanding transactional applications, including SAP HANA, Microsoft SQL Server, and IBM DB2.
>
> * HDD-based volumes include Throughput Optimized HDD (st1) for frequently accessed, throughput-intensive workloads and the lowest cost Cold HDD (sc1) for less frequently accessed data.
>
> You can chose the volume type that best meets your application and use case requirements. You can change from one volume type to another.

> ### Elastic Volumes
>
> Elastic Volumes is a feature that allows you to easily adapt your volumes as the needs of your applications change. The Elastic Volumes feature allows you to dynamically increase capacity, tune performance, and change the type of any new or existing current generation volume w/ no downtime or performance impact. You can easily right-size your deployment and adapt to performance changes.
>
> You can create a volume w/ the capacity and performance needed today, knowing you have the ability to modify your volume configuration in the future. Elastic Volumes can save you hours of planning cycles.
>
> By using Amazon CloudWatch w/ AWS Lambda, you can automate volume changes to meet the changing needs of your applications.
>
> The Elastic Volumes feature makes it easier to adapt your resources to changing application demands, giving you confidence that you can make modifications in the future as your business needs change.

> ### Built-in encryption
>
> Amazon EBS encryption offers seamless encryption of EBS data volumes, boot volumes, and snapshots, eliminating the need to build and manage a secure key management infrastructure.
>
> Amazon EBS encryption enables data at-rest security by encrypting your data volumes, boot volumes, and snapshots using AWS managed keys or keys that you create and manage using the AWS Key Management Service (AWS KMS). You can also configure your profile so that encryption is enabled by default for al newly created EBS volumes.
>
> The encryption occurs on the servers that host EC2 instances, providing data in-transit encryption of your data as it moves between EC2 instances and EBS data and boot volumes.
>
> W/ your data encrypted in-transit and at-rest, you are protected from unauthorized access to your data.

> ### Multi-attach
>
> Customers can enable Multi-attach on an EBS Provisioned IOPS io2 or io1 volume. Multi-attach allows a single EBS volume to be concurrently attached to up to 16 Nitro-based EC2 instances within the same Availability Zone.
>
> Multi-attach makes it easier to achieve higher application availability for applications that manage storage consistency from multiple writers. Each attached instance has full read and write permission to the shared volume. Applications using Multi-attach need to provide I/O fencing for storage consistency. There is no additional fee to enable Multi-attach.

> ### Volume monitoring
>
> Performance metrics, such as bandwidth, throughput, latency, and average queue length, are available through the AWS Management Console. Using these metrics, provided by CloudWatch, you can monitor the performance of your volumes to make sure that you are providing enough performance for your applications w/o paying for resources you don't need.

## Five different types of EBS storage:

* General Purpose (SSD)

* Provisioned IOPS (SSD, built for speed)

* Throughput Optimized Hard Disk Drive (magnetic, built for larger data loads)

* Cold Hard Disk Drive (magnetic, built for less frequently accessed workloads)

* Magnetic

## Comparison of EBS types

<table>
    <thead>
        <tr>
            <th colspan=3>Solid-State Drives (SSD)</th>
            <th colspan=3>Hard disk Drives (HDD)</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Volume Type</td>
            <td><b>General Purpose SSD</b></td>
            <td><b>Provisioned IOPS SSD</b></td>
            <td><b>Throughput Optimized HDD</b></td>
            <td><b>Cold HDD</b></td>
            <td><b>EBS Magnetic</b></td>
        </tr>
        <tr>
            <td>Description</td>
            <td>General purpose SSD volume that balances price and performance for a wide variety of transactional workloads</td>
            <td>Highest-performance SSD volume designed for mission-critical applications</td>
            <td>Low cost HDD volume designed for frequently accessed, throughput-intensive workloads</td>
            <td>Lowest cost HDD volume designed for less frequently accessed workloads</td>
            <td>Previous generation HDD</td>
        </tr>
        <tr>
            <td>Use Cases</td>
            <td><b>Most Work Loads</b></td>
            <td><b>Databases</b></td>
            <td><b>Big Data & Dara Warehouses</b></td>
            <td><b>File Servers</b></td>
            <td><b>Workloads where data is infrequently accessed</b></td>
        </tr>
        <tr>
            <td>API Name</td>
            <td>gp2</td>
            <td>io1</td>
            <td>st1</td>
            <td>sc1</td>
            <td>Standard</td>
        </tr>
        <tr>
            <td>Volume Size</td>
            <td>1 GiB - 16 TiB</td>
            <td>4 GiB - 16 TiB</td>
            <td>500 GiB - 16 TiB</td>
            <td>500 GiB - 16 TiB</td>
            <td>1 GiB - 1 TiB</td>
        </tr>
        <tr>
            <td>Max. IOPS**/Volume</td>
            <td>16,000</td>
            <td>64,000</td>
            <td>500</td>
            <td>250</td>
            <td>40-200</td>
        </tr>
    </tbody>
</table>

* If you need to optimize throughput, choose **Throughput Optimized HDD**. If you just want the lowest cost storage available, use **Cold Hard Disk Drive**.

## SSD versus HDD

* SSD-backed volumes are built for transactional workloads involving frequent read/write operations, where the dominant performance attribute is IOPS. 

    * **Rule of thumb**: Will your workload be IOPS heavy? Plan for SSD.

* HDD-backed volumes are built for large streaming workloads where throughput (measured in MiB/s) is a better performance measure than IOPS.

    * **Rule of thumb**: Will your workload be throughput heavy? Plan for HDD.

## EBS Volumes and Snapshots

EBS volumes will always be in the same availability zone as the EC2 instance.

When you terminate an EC2 instance, by default, the root device volume will also be terminated. However, additional volumes that are attached to that EC2 instance will continue to persist.

## Learning summary

> * Volumes exist on EBS. Think of EBS as a virtual hard disk.
>
> * Snapshots exist on S3. Think of snapshots as a photograph of the disk.
>
> * Snapshots are point in time copies of Volumes.
>
> * *Snapshots are incremental* - this means that only the blocks that have changed since your last snapshot are moved to S3.
>
> * If this is your first snapshot, it may take some time to create.
>
> * If it is a second snapshot, it will only replicate the Delta, or the changes.

> * To create a snapshot for Amazon EBS volumes that serve as root devices, you should stop the instance before taking the snapshot
>
>   * However, you can take a snap while the instance is running

> * You can create AMI's from Snapshots
>
> * You can change EBS volume sizes on the fly, including changing the size and storage type
>
> * Volumes will **ALWAYS** be in the same availability zone as the EC2 instance
>
> * So you cannot have an EX2 instance in one availability zone and an EBS volume in another availability zone
>
> * To move an EC2 volume from one AZ to another, take a snapshot of it, create an AMI from the snapshot and then use the AMI to launch the EC2 instance in a new AZ
>
> * To move an EC2 volume from one region to another, take a snapshot of it, create an AMI from the snapshot and then copy the AMI from one region to the other. Then use the copied AMI to launch the new EC2 instance in the new region.

> * Termination Protection is **turned off** by default, you must turn it on.

* If you want to protect your EC2 instances from being accidentally deleted by your developers or system administrators, ensure Termination Protection is turned on

> * On an EBS-backed instance, the **default action is for the root EBS volume to be deleted** when the instance is terminated

* So if you do go in and terminate your EC2 instances, you are going to delete that root device volume automatically but if you add additional attached volumes to that EC2 instance, those additionally attached volumes won't be deleted automatically, unless you go in and ensure Termination Protection is checked

> * EBS Root Volumes of your DEFAULT AMI's **CAN** be encrypted. You can also use a third party tool (such as bit locker etc) to encrypt the root volume, or this can be done when creating AMI's (remember the lab) in the AWS console or using the API.

> * Additional volumes can be encrypted
