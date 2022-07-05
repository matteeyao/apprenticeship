# Elastic File Storage

Amazon Elastic File System (Amazon EFS) is a file storage service for Amazon Elastic Compute Cloud (Amazon EC2) instances. Amazon EFS is easy to use and provides a simple interface that allows you to create and configure file systems quickly and easily. W/ Amazon EFS, storage capacity is elastic, growing and shrinking automatically as you add and remove files, so your applications have the storage they need, when they need it.

Similar Elastic Block Store (EBS), but with EBS, you can only mount your virtual disk to one EC2 instance. We cannot have two EC2 instances sharing an EBS volume. However, you can have them sharing an EFS volume.

If you provision an EFS instance, storage needs will grow dynamically according to our needs. We won't need to pre-provision storage like we do with EBS. EFS is great for file servers and sharing files between different EC2 instances.

File storage has a structure. You can create folders and it can be mounted, but it is not boot-able. If you want to share a file system across multiple servers, then file storage EFS is a great solution.

## Learning summary

> * Supports the Network File System version 4 (NDSv4) protocol
>
> * You only pay for the storage you use (no pre-provisioning required)
>
> * Can scale up to the petabytes
>
> * Can support thousands of concurrent NFS connections
>
> * Data is stored across multiple AZ's within a region
>
> * Read After Write Consistency
