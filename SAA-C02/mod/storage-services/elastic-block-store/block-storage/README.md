# What is block storage?

## Primary data storage types

Whether on premises or in a cloud environment, you have three primary types of storage: block, file, and object. Different storage hardware manufacturers and cloud service providers implement these storage types differently. However, the fundamentals for each storage type are basically the same, regardless of where the storage type is located, who manufactures the hardware, or who provides the service. The specific features and functionality differ based on how the manufacturer or service provider implements the storage.

> ### Block storage overview
>
> *Block* storage is raw storage in which the hardware storage device or drive is presented, as either disk or volume, to be formatted and attached to the compute system for use. The storage is formatted into predefined continuous segments on the storage device. These segments are called blocks, which is where this storage type gets its name. The blocks are the basic fixed storage units used to store data on the device.
>
> Storage devices can be hard disk drives (HDDs), solid state drives (SSDs), or newer types of storage devices, such as Non-Volatile Memory Express (NVMe). In addition to individual storage devices, block storage can be deployed on storage area network (SAN) systems.
>
> The storage device is presented to be used by the operating system or an application that has the capabilities to directly manage block storage. In cases where the application manages the block storage, the application often shares management w/ an operating system.

> ### File storage overview
>
> *File storage* is built on top of block storage, typically serving as a file share or file server. File storage is created using an operating system that formats and manages the reading and writing of data to the block storage devices. The name file storage comes from the primary use of storing data as files typically in a directory tree hierarchy.
>
> The two most common storage protocols for file storage are **Server Message Bloc (SMB)** and **Network File System (NFS)**, which are network protocols that enable users to communicate w/ remote computers and servers. The protocols enable users to use the servers' resources or share, open, and edit files.
>
> The operating system manages the storage protocol and the operation of the file system. The file system can be Windows Server, Linux, or a specialized operating system used on network attached storage (NAS) devices or clustered NAS systems.

> ### Object storage overview
>
> *Object storage* is also built on top of block storage. Object storage is created using an operating system that formats and manages the reading and writing of data to the block storage devices. The name object storage comes from the primary use of storing the data within a binary object. Unlike file storage, object storage does not differentiate between types of data. The type of data or the file type becomes part of the data's metadata.
>
> An object is made up of a larger set of blocks organized using a predetermined size. For example, one object storage system uses binary object sizes of 128 megabytes (MB). Smaller files or data are stored at a binary level within the object. Larger data files are stored by spreading the data across multiple objects.
>
> Object storage is recognized for its inherent availability of the file objects. Some systems support file versioning, file tracking, and file retention.

## Block storage architecture

The basic block storage architecture consists of three components: the block storage, the compute system, and the operating system running on the compute system. The block storage is either physically or logically attached to the compute system. The operating system, which runs on the compute system, then recognizes the block storage as available. The operating system formats or makes the block storage ready for use. In some situations, an application running on the compute system can act as the managing entity rather than the operating system. However, for most use cases, the operating system running on the computer system manages the block storage.

## Block storage management

The operating system or application manages the block storage. Some of the aspects that the operating system manages are configuring the block size; creating and managing metadata; managing read, modify, and write activity; and managing file-level and block-level locking.

> ### Block size
>
> The operating system must first format the block storage. When you format the block storage, you select the block size that best meets your use case. For some applications, small block sizes are more appropriate, and larger block sizes better serve other applications. Block size flexibility is a fundamental differentiator for block storage. You have the ability to format the storage to best service your application requirements.

> ### Metadata management
>
> When data is stored, the operating system creates metadata. Metadata is data about the data. THe metadata information helps to manage a data resource, such as resource type, permissions, and the time and way it was created. The metadata includes the information that the operating system and users need to identify and track the data.
>
> * Metadata includes timestamp tracking for the data such as:
>
>   * The creation or birth time (ctime), which is when the data was first created.
>
>   * The modification time (mtime), which is when the data or metadata was last changed or modified.
>
>   * The access time (atime), which is when the data was last accessed.
>
> * Metadata tracks where the data is stored on the storage system. It tracks which blocks on the drives were used to store the data.
>
> * Metadata also contains information about the data owner and access permissions: who owns the data and who can read or modify the data.

> ### Read-write activity
>
> The operating system determines what controls, if any, are in place to manage access to the data.
>
> * Often the operating system uses metadata permissions to control who can access, modify, or delete the data. The operating system can also reference external sources such as Microsoft Active Directory or Lightweight Directory Access Protocol (LDAP) to determine these permissions.
>
> * The operating system also manages how client access is managed when reading data and modifying data. The operating system determines how clients or applications are notified when another client or application is accessing data.
>
> * The operating system manages where and when data is written to the block storage. It determines how writes are cached and staged before writing the blocks and which blocks to write to.
>
> * The operating system also manages the caching or read activity: how are reads cached, what order reads are performed, and how are multiple read requests are managed.

> ### Locking control
>
> The operating system also manages data integrity when data is being modified or deleted. The operating system can prevent other clients from modifying the data by applying a lock on the entire data file or on specific portions or blocks being specified. These are called file-level locking and block-level locking, respectively. Some operating systems can also place locks on certain block ranges.

## Presented as volumes

The block storage can be a single device, or it can be several devices that are combined together before being presented to the compute system. A block storage device is typically referred to as a disk or drive. Combining drives together using a hardware-controlled redundant array of independent disks (RAID) controller or using a SAN system is an example of combining multiple devices.

The physical storage capacity of a block storage device can be divided into smaller logical units, or smaller block storage devices can be combined into larger logical units. These units are called volumes. A volume is a logical storage construct that can be created from a single drive or using multiple drives. You can create multiple volumes that are the right size for your requirements and later allocate the remainder of the storage for other uses.

As an example, you could have three 2-tebibyte (TiB) SSDs w/ a total raw capacity of 6 TiB on the storage device. For your application, you need only one 50-gigabytes (GB) volume and two 500-GB volumes. Of the available 6 TiB, you would use 1,050 GB for your use case. The remainder of the raw capacity is available to be allocated as volumes for other use.

Depending on the type of block storage, for instance w/ a local drive, the storage could be available to only the local compute system. For most shared block systems, such as SANs, the available space can be allocated to attach to other compute systems.

## Fast performance

Block storage is relatively fast compared to most file and object storage systems and services. Block storage does not need to read and write the entire file. The operating system or application can read or write just the blocks it needs for the operation.

For cloud services, some of the performance differences are becoming less significant. In measuring performance, different metrics apply depending on the type of operation. Read operations and write operations have different metrics associated w/ them.

> ### Latency
>
> Latency is measured as the amount of time between making a request to the storage system and receiving the response. Latency is often referred to as delay. Low-latency performance is a primary benefit for block storage.
>
> * Latency can range from sub-1 millisecond (ms) to low two-digit millisecond response rates.
>
> * How the storage is connected to the compute system affects the response rates.
>
>   * Local onsite storage does not traverse any network, which can reduce any external network delay.
>
>   * Onsite SAN storage includes a fast low-latency network; however, traversing the network can add to the latency.
>
>   * The network connectivity in a cloud provider's network impacts cloud storage.
>
> * Block storage does not have additional network protocol overhead. As a direct connection, the overhead is only what the operating system adds.
>
>   * File storage processes include overhead for processing the storage protocol. Requests are processed from the clients, and this processing time adds latency.
>
>   * Object storage processes include overhead to process the Hypertext Transfer Protocol (HTTP) requests using REST-APIs. The storage's operating system then processes the HTTP requests. The process adds latency to the request time.

> ### IOPS
>
> Input/output operations per second (IOPS) is a statistical storage measurement of the number of input/output (I/O) operations that can be performed per second. IOPS is also used to measure the number of operations at a given type of workload and operation size can occur per second. IOPS is typically used to measure random I/O read-write activities. Random means that the information used for read-write activity is usually very small in size and the different information is not related to each other.
>
> Along w/ the low latency, block storage can often obtain higher IOPS than other types of storage. SSDs are best suited for applications that require high IOPs. SSDs provide the benefit of low latency and higher read and write performance for random I/O. SSDs do not have the seek times required for HDDs and are able to perform operations much faster.

> ### Throughput
>
> Throughput is a statistical storage measurement used to measure the performance associated w/ reading large sequential data files. Large files, such as video files, must be read from beginning to end. Throughput operations are measured in megabytes per second (MB/s).
>
> HDDs are also referred to as spinning disks. HDDs are best suited for applications that require sustained read throughput. Applications that read and write large sequential files, such as video files, are well suited for HDDs. Random read operations are slower due to the seek times to locate the blocks and read them. Write operations to HDDs are also slower b/c of the seek time to locate and write to the blocks.