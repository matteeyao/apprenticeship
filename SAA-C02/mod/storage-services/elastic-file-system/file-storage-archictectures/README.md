# File storage architectures

## File storage on premises

File storage is essentially hierarchical storage that can provide shared access to file data to a set of networked clients. Data is stored in files, the files are stored in directories, and the directories are stored in volumes, then mounted by clients. To access the files, the client compute systems must have the correct permissions to mount the file system and move between directories.

In on-premises architectures, shared file storage systems are typically called network attached storage (NAS). In an on-premises architecture, one or more NAS servers provide a network access point in between the storage subsystems while providing security, data management, fault tolerance, and disaster recovery capabilities.

## File storage in the cloud

With file storage hosting in the cloud, multiple users can store and access a common set of data files while taking advantage of the durability and resiliency of cloud infrastructure. Instead of storing data files locally and managing access and the underlying block storage on a NAS device, you store files in cloud storage resources. You control access through the setup and maintenance of your own cloud file servers, or by taking advantage of a managed file service such as Amazon EFS.

## Closer look at how managed file services work.

1. The customer manages the access controls for who can access the file system via network controls (security groups/NACLs), file system policies, access points, and IAM policies. The service manages all the file storage infrastructure for you, meaning that you can avoid the complexity of deploying, patching, and maintaining complex file system configurations.

2. Communications between the clients and storage is handled through a specialized protocol. For Amazon EFS, this would be the NFS protocol (currently NFSv4.0 and v4.1 are both supported).

3. The cloud service provides a common Domain Name System (DNS) namespace for the clients to connect to the shared file systems. Clients with appropriate permissions access the shared file systems by connecting to their attached mount points, and file systems appear as local volumes to the individual clients. Each client can access the same data on the shared volumes.

## AWS file storage services

AWS currently offers several different options for providing shared file storage services to meet your application, workflow, and use-case requirements.

![Fig. 1 File storage services](../../../../../img/SAA-CO2/storage-services/elastic-file-system/file-storage-architectures/diag.jpeg)

For running file system workflows on AWS, you can select from Amazon EFS, Amazon FSx for Lustre, Amazon FSx for NetApp ONTAP, Amazon FSx for Windows File Server, or Amazon FSx for OpenZFS. 

* Amazon EFS was the original managed file sharing service in AWS. It is scalable, elastic, cloud native, and fully managed. Amazon EFS supports the NFS protocol. Unlike the other AWS-managed file storage service offerings, you do not need to select a specific storage capacity when you create an Amazon EFS file system. With Amazon EFS, capacity is always dynamically added or removed for you as you add or remove files. Amazon EFS file systems can scale to Petabytes in size depending on your needs.

* FSx for Lustre is an AWS fully managed parallel file system built on Lustre for high performance computing (HPC) workloads. When you create an FSx for Lustre file share, you will need select the specific performance and capacity parameters that are most suited for your application requirements.

* FSx for Windows File Server is an AWS fully managed shared file system providing access to Windows compute environments. FSx for Windows File Server supports the SMB protocol. FSx for Windows File Server provides seamless integration with Microsoft Active Directory environments for managing access permissions.

* FSx for ONTAP provides fully managed, highly reliable, scalable, high-performing, shared storage for large organizations employing multi-protocol environments. FSx for ONTAP supports Linux, Windows, and MacOS workloads.

* With Amazon FSx for OpenZFS, you can launch, run, and scale fully managed file systems on AWS that replace the ZFS or other Linux-based file servers you run on premises. Amazon FSx for OpenZFS helps to provide better agility and lower costs.

> Amazon EFS removes the need for you to individually manage and monitor the centralized file servers when building out your own self-managed shared file system storage solution.

## Managed services and file storage management

One of the primary differences between a do-it-yourself shared file system that you set up and a managed service such as Amazon EFS is the necessity of operating your own individual centralized file servers. With a DIY setup, you need to create, manage, monitor, and maintain a set of EC2 instances capable of handling the network traffic to and from your clients.

You will need to manage the operating systems, networking, security group, and firewall settings for each of the individual file servers required to manage access to the shared storage. You will need to set up distinct file sharing resources in each Availability Zone you want to operate in. You must assume responsibility for the individual instances, guest operating systems (including updates and security patches), and file server software needed to keep your shared storage available.

A managed service such as Amazon EFS removes the need to individually manage and monitor the centralized file storage servers. Amazon EFS file systems are distributed across an unconstrained number of storage servers. So file systems can grow elastically to petabyte scale, allowing massively parallel access from multiple compute instances and AWS Lambda functions.

Additionally, a managed service like Amazon EFS removes the need to individually manage connections between file servers and the underlying block devices. Amazon EFS manages client permission checks and routes traffic to the underlying physical storage. It ensures that your data is durable and available.

Lastly, Amazon EFS removes the need to manage the underlying storage capacity. With Amazon EFS, you pay only for the capacity that you need to store your file data. You don't ever have to provision more capacity than you need at a given point in time.
