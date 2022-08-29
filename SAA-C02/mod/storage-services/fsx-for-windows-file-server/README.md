# Amazon FSx for Windows and Amazon FSx for Lustre

> "Amazon FSx for Windows File Server provides a fully managed native Microsoft Windows file system so you can easily move your Windows-based applications that require file storage to AWS. Amazon FSx is built on Windows Server."

FSx for Windows File Server is an AWS service that simplifies the setup, provisioning, and maintenance of your Windows workloads. As a fully managed service, FSx for Windows File Server eliminates the administrative overhead of managing file servers and storage volumes. FSx for Windows File Server automatically keeps your Windows software up to date, detects and addresses hardware failures, and performs backups.

Designed for use w/ Microsoft applications such as SQL Server, Active Directory, IIS, SharePoint, etc

## How is Windows FSx different to EFS?

### Windows FSx

* A managed Windows Server that runs Windows Server Message Block (SMB)-based file services

* Designed for Windows and Windows applications

* Supports AD users, access control lists, groups and security policies, along w/ Distributed File System (DFS) namespaces and replication

### EFS

* A managed NAS filer for EC2 instances based on Network File System (NFS) version 4

    * One of the first network file sharing protocols native to Unix and Linux. Amazon does not supports EC2 instance that are running Windows to connect to EFS, but only support EFS connections from Linux EC2 instances

* One of the first network file sharing protocols native to Unix and Linux

## Amazon FSx for Lustre

> "Amazon FSx for Lustre is a fully managed file system that is optimized for compute-intensive workloads, such as high-performance computing, machine learning, media data processing workflows, and electronic design automation (EDA).
>
> With Amazon FSx, you can launch and run a Lustre file system that can process massive data sets at up to hundreds of gigabytes per second of throughput, millions of IOPS, and sub-millisecond latencies."

* Applicable for cases where you're processing large data sets w/ up to hundreds of gigabits per second and you need that kind of throughput or millions of IOPS or sub millisecond latencies. So think big data, machine learning, high performance computing

* Amazon FSx for Windows is applicable to situations when we're using Windows applications

* If you just need shared storage for Linux, you would just use EFS

## How is Lustre FSx different to EFS?

### Lustre FSx

* Designed specifically for fast processing of workloads such as machine learning, high performance computing (HPC), video processing, financial modeling, and electronic design automation (EDA).

* Lets you launch and run a file system that provides sub-millisecond access to your data and allows you to read and write data at speeds of up to hundreds of gigabytes per second of throughput and millions of IOPS

### EFS

* A managed NAS filer for EC2 instances based on Network File System (NFS) version 4

* One of the first network file sharing protocols native to Unix and Linux

## Key components of FSx for Windows File Server

> ### File system
>
> The primary resource in FSx for Windows File Server is a file system. A file system is an entity that enables your users to store and access files and folders. Your file system is accessible by its Domain Name System (DNS) name. A file system consists of a Windows file server and storage volumes. The Windows file server serves data over the network to your users accessing the file system. The storage volumes (or disks) host your file system data. You can choose hard disk drives (HDD) or solid state drives (SSD) storage volumes for your file system. HDD storage is appropriate for a broad spectrum of workloads, including home directories and content management systems. SSD storage is ideal for high-performance and latency-sensitive workloads, including databases, media processing workloads, and data analytics applications. 

> ### File share
>
> A file share is a specific folder and subfolders within your file system. File shares are accessible to compute instances by supporting the Server Message Block (SMB) protocol, versions 2.0 to 3.1.1. Every file system comes with a default file share named share. You can create as many file shares as you need.

> ### Elastic network interface
>
> An elastic network interface is a resource that allows client compute instances, whether they reside in AWS or on premises, to connect to your file systems. The network interface resides in the Amazon Virtual Private Cloud (Amazon VPC) that you associate with your file system. The DNS name of your file system maps to the private IP address of the file system's elastic network interface in your Amazon VPC. 

![Fig. 1 Elastic network interface](../../../../img/SAA-CO2/storage-services/fsx-for-windows-file-server/diag01.png)

## Network connectivity options for FSx for Windows File Server

You can access your file system from compute instances that reside in the same virtual private cloud (VPC) as your file system. FSx for Windows File Server supports access from instances outside the VPC associated with your file system only if those instances have an IP address in the following private IPv4 address ranges:

* 10.0.0.0–10.255.255.255 (10/8 prefix)

* 172.16.0.0–172.31.255.255 (172.16/12 prefix)

* 192.168.0.0–192.168.255.255 (192.168/16 prefix)

> ### Access file systems from on-premises environments
>
> You can access file systems from your on-premises environment using an AWS Direct Connect or AWS Client VPN connections.
>
> * **AWS Direct Connect** is an AWS service that enables you to access your file system over a dedicated network connection from your on-premises environment.
>
> * **AWS Client VPN** is an AWS service that enables you to access your file system from your on-premises environment over a secure and private tunnel.
>
> After you connect your on-premises environment to the VPC associated with your file system, you can access your file system using its DNS name.

![Fig. 2 Using an AWS Direct Connect or AWS Client VPN](../../../../img/SAA-CO2/storage-services/fsx-for-windows-file-server/diag02.png)

> ### Accessing file systems from another VPC, account, or AWS Region
>
> You can access your file systems from compute instances in different VPCs, AWS accounts, and AWS Regions from those associated with your file system. To do so, you can use VPC peering connections or AWS Transit Gateway.
>
> * You can use **VPC peering** to connect VPCs within the same AWS Region or between AWS Regions. A VPC peering connection is a network connection between two VPCs that enables you to route traffic between them privately.
>
> * A **transit gateway** is a network transit hub that you can use to interconnect your VPCs and on-premises networks.
>
> After you configure a VPC peering or transit gateway connection, you can access your file system using its DNS name. 

## Deployment types for FSx for Windows File Server

FSx for Windows File Server offers file systems with two levels of availability and durability: Single-AZ and Multi-AZ. 

> ### Single-AZ deployments
>
> In Single-AZ deployments, FSx for Windows File Server automatically replicates the data in your file system within the Availability Zone your file system resides in. In this deployment type, FSx for Windows File Server continuously monitors, detects, and automatically recovers from the most common infrastructure failure scenarios. To ensure durability, FSx for Windows File Server automatically takes highly durable backups of your file systems daily. The backups are then stored in Amazon Simple Storage Service (Amazon S3). 
>
> When you create a Single-AZ file system, you specify a single subnet for the file system. The subnet you choose defines the Availability Zone in which FSx for Windows File Server creates the file system.

![Fig. 3 Single-AZ deployments](../../../../img/SAA-CO2/storage-services/fsx-for-windows-file-server/diag03.gif)

> ### Multi-AZ deployments
>
> Multi-AZ deployments support all the availability and durability features of Single-AZ deployments. Multi-AZ deployments also provide availability to data even when an Availability Zone is unavailable. In a Multi-AZ deployment, FSx for Windows File Server creates active and standby file servers in separate Availability Zones. FSx for Windows File Server synchronously replicates any changes written to your active file server across Availability Zones to the standby. During planned maintenance, or in the event of unavailability of the active file server or its Availability Zone, FSx for Windows File Server fails over to the standby file server. When failover occurs, the new active file server processes all file system read and write requests. This enables your file server operations to continue without loss of availability to your data.
>
> When you create a Multi-AZ file system, you specify two subnets: one for the preferred file server and one for the standby file server. The two subnets you choose must be in different Availability Zones. For in-AWS access, we recommend that you launch your clients in the same Availability Zone as your preferred file server to reduce cross-Availability Zone data transfer costs and minimize latency.
>
> Failovers typically complete within 30 seconds. When the file server in the preferred subnet becomes available again, FSx for Windows File Server fails back to the original Multi-AZ configuration.

![Fig. 4 Multi-AZ deployments](../../../../img/SAA-CO2/storage-services/fsx-for-windows-file-server/diag04.gif)

## Choosing Single-AZ or Multi-AZ deployments

Multi-AZ file systems are ideal for mission-critical workloads that require their data to be highly available. Examples of these workloads include business applications and web serving environments. Single-AZ file systems offer a lower-cost option for workloads that do not require the high availability of a Multi-AZ solution.

## Learning summary

> In the exam you'll be given different scenarios and asked to choose whether you should use an EFS, FSx for Windows, or FSx for Lustre
>
> * **EFS**: When you need distributed, highly resilient storage for Linux instances and Linux-based applications
>
> * **Amazon FSx for Windows**: When you need centralized storage for Windows-based applications such as Sharepoint, Microsoft SQL Server, Workspaces, IIS Web Server, or any other native Microsoft Application
>
> * **Amazon FSx for Lustre**: When you need high-speed, high-capacity distributed storage. This will be for applications that do High Performance Compute (HPC), financial modeling, etc. Remember that FSx for Lustre can store data directly on S3
