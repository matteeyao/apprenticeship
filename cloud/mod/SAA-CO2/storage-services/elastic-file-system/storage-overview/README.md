# Primary data storage types

> ### Block storage overview
>
> *Block storage* is raw storage in which a hardware device is presented, as a disk or volume, to be formatted and attached to the compute system for use. The storage is formatted into predefined segments on the storage device. These segments are divided into *block addresses* that are used to direct and store data, which is where this storage type gets its name. Blocks are the basic fixed storage units used to store data on the device.
>
> Storage devices can be hard disk drives (HDD) or solid state drives (SSDs). In addition to individual storage devices, block storage can be pooled on a storage area network (SAN) and allocated to compute systems as logical units.
>
> Block storage is presented to operating systems and applications that have the capability to directly manage it. It is possible to use specialized operating systems and components that allow multiple compute systems to access the same block storage device. However, block storage is most commonly used when a single compute system will be accessing the stored data. For example, the operating system of a personal computer will typically be stored on a block storage device. When you need to provide dedicated block storage for a single Amazon Elastic Compute Cloud (Amazon EC2) instance, Amazon Elastic Block Store (Amazon EBS) volumes provide an excellent storage option.

> ### File storage overview
>
> When file storage is made accessible to multiple compute systems, it is known as a shared file system. File storage is created using an operating system that formats and manages the reading and writing of data to the individual blocks of an underlying storage device. The name *file storage* comes from the mapping of a collection of data blocks into an individual file, which is then stored in a directory that grants access permissions to the files it contains.
>
> To manage the traffic to and from multiple compute systems, a communication protocol is required. The two most common protocols for shared file storage are Server Message Block (SMB) and Network File System (NFS), which allow access to a storage system over a shared network.
>
> For shared file systems, a file server operating system manages the storage protocol. It routes data to and from the block devices, as well as permissions to access the individual files. The file server operating system can be Windows Server, Linux, or a specialized operating system used on a network attached storage (NAS) device or in the cloud.
>
> Shared file storage is most commonly used when you have data that you want to share w/ a select number of compute systems that will all need both read and write access to the data. For example, if you have several monthly reports that must be accessed and updated by many of your EC2 instances, you can use Amazon EFS to create shared file storage for these monthly reports. Similarly, if your have multiple image files that need to be accessed and updated by many individual AWS functions for Machine Learning, Amazon EFS provides an optimal shared storage solution.

> ### Object storage overview
>
> A storage object includes the primary data itself, a variable amount of metadata, and a globally unique identifier. Like file storage, object storage is also created using an operating system that formats and manages the reading and writing of data to individual block storage devices. Unlike file storage, object storage does not permit you to make small changes to a given set of data w/o creating a new storage object. You can make changes to a file while it remains  the same file. When you make changes to an object, you essentially create a new object.
>
> The versatility and use cases for object storage are continually evolving. However, object storage is particularly well suited for storing data that is generally unchanging and that you want to grant read access to widely. An example would be a collection of completed video files. If you have a collection of marketing videos that you want to make available for customer viewing around the globe, Amazon Simple Storage Service (Amazon S3) provides an ideal object storage solution.

