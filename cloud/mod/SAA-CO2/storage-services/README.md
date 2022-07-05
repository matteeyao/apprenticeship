# Storage Types

## Block storage

A block is a range of bytes or bits on a storage device. Block storage files are divided into blocks and written directly to empty blocks on a physical drive. Each block is assigned a unique identifier and then written to the disk in the most efficient manner possible. Since blocks are assigned identifiers, they do not need to be stored in adjacent sections of the disk. Indeed they can be spread across multiple disks or environments. You can retrieve the individual blocks separately from the rest of the file, which makes block storage excellent for technology like relational databases.

W/ relational databases, you might only need to retrieve a single piece of file, such as an inventory tracking number, or one specific employee ID, rather than retrieving the entire inventory listing or whole employee repository.

## File storage

Historically, operating systems save data in hierarchical file systems organized in the form of directories, sub-directories and files, or folders, sub-folders, and files depending on the operating system.

For example, if you are troubleshooting an issue on a Linux distribution, you may need to look in /var/log or /etc/config. Once inside one of these directories, you need to identify which file to explore and open. When using a file-based system, you must know the exact path and location of the files you need to work w/ or have a way to search the entire structure to find the file you need.

## Object storage

Unlike the hierarchical structure used in file-based storage, object storage is a flat structure where the data, called an object, is located in a single repository known as a bucket. Objects can be organized to imitate a hierarchy by attaching key name prefixes and delimiters. Prefixes and delimiters allow you to group similar items to help visually organize and easily retrieve your data. In the user interface, these prefixes give the appearance of a folder and subfolder structure but in reality, the storage is still a flat structure.

> ### Object storage in Amazon S3
>
> Object storage is a flat storage structure where objects are stored in buckets. You can create a pseudo folder structure using prefixes. In Amazon S3 object storage, you can organize objects to imitate a hierarchy by using key name prefixes and delimiters. Prefixes and delimiters allow you to group similar items to help visually organize and easily retrieve your data.

## AWS Storage Types - S3, EFS, & EBS

Ultimately all storage has "block storage" at the lowest level (even SSDs which emulate disk blocks). The terminology used is not as important as understanding the distinctions in how the storage is interacted with. Rather than trying  to memorize that this service has this title, or that service has that title, it is more useful to understand how they are used and the advantages and limitations that each impose.

## S3

Files on S3 can only be addressed as objects. You cannot interact w/ the contents of the object until you have extracted the object from S3 (GET). It cannot be edited in-place, you must extract it, change it, and then put it back to replace the original (PUT). What this comes down to is that there is no user "locking" functionality as might be offered by a 'file system'. This is why it is called "Object storage"

## EFS

EFS is basically NFS (Network File System) which is an old and still viable Unix technology. As the title implies, it is a "File System" and offers more capabilities than "Object Storage". The key to these is grades of 'File Locking' which makes it suitable for shared use. This is what makes it suitable for a share NETWORK file system. It is important to note that like Object Stores, you are still restricted to handling the file as a complete object. You can lock it so that you can write back to it, but you are restricted in the extent that you do partial content updates based on blocks. This gets a bit grey as there are ways to do partial updates, but they are limited.

File Storage has a structure. EFS is a ready-made file system w/ a structure, so you can create folders and access your files, but you cannot boot from file storage b/c you're just given access to a file system over the network. File Storage can be used for mounting, but not for booting. You can create folders, File Storage can be mounted, but EFS is not boot-able.

If you seek to share a file system across multiple servers, then file storage EFS is...

## EBS

EBS is closer to locally attacked disk whether that be IDE, SAS, SCSI (or its close cousin iSCSI/Fibre Channel, which is in reality just SCSI over a pipe). W/ Locally attached disk you have better responsiveness and addressing. This allows you to use File Systems that can address the disk at a block level. This includes part reads, part writes, read ahead, delayed writes, file system maintenance where you swap block under the file, block level cloning, thin provisioning, de-duplication, etc. As noted above, Block Storage sits underneath both NFS and Object stores, but as a consumer you are abstracted away and cannot make direct use of them.

EBS is not as fast as Instance Store, but EBS is highly resilient and it's separate from the instance hardware. So issues w/ the EC2 host will not impact EBS. The term `ephemeral storage` refers to storage that is temporary, not persistent. The Instance Store storage is an example of ephemeral storage whereas EBS is an example of persistent storage.

EBS is a volume that is presented to the operating system as a collection of blocks but there is no structure, just a collection of addressable blocks. These blocks can be mounted which means that file systems can be created on top of the block storage. You can even boot off an EBS volume, which is the reason why most EC2 instances use an EBS volume, or block storage, as their boot volume. EBS is what stores the operating system. Block storage does not have a structure and is boot-able. Block storage is used if you want to use storage to boot and seek high performance storage inside your operating system.
