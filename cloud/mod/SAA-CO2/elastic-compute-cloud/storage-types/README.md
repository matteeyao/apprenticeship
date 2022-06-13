# AWS Storage Types - S3, EFS, & EBS

Ultimately all storage has "block storage" at the lowest level (even SSDs which emulate disk blocks). The terminology used is not as important as understanding the distinctions in how the storage is interacted with. Rather than trying  to memorize that this service has this title, or that service has that title, it is more useful to understand how they are used and the advantages and limitations that each impose.

## S3

Files on S3 can only be addressed as objects. You cannot interact w/ the contents of the object until you have extracted the object from S3 (GET). It cannot be edited in-place, you must extract it, change it, and then put it back to replace the original (PUT). What this comes down to is that there is no user "locking" functionality as might be offered by a 'file system'. This is why it is called "Object storage"

## EFS

EFS is basically NFS (Network File System) which is an old and still viable Unix technology. As the title implies, it is a "File System" and offers more capabilities than "Object Storage". The key to these is grades of 'File Locking' which makes it suitable for shared use. This is what makes it suitable for a share NETWORK file system. It is important to note that like Object Stores, you are still restricted to handling the file as a complete object. You can lock it so that you can write back to it, but you are restricted in the extent that you do partial content updates based on blocks. This gets a bit grey as there are ways to do partial updates, but they are limited.

## EBS

EBS is closer to locally attacked disk whether that be IDE, SAS, SCSI (or its close cousin iSCSI/Fibre Channel, which is in reality just SCSI over a pipe). W/ Locally attached disk you have better responsiveness and addressing. This allows you to use File Systems that can address the disk at a block level. This includes part reads, part writes, read ahead, delayed writes, file system maintenance where you swap block under the file, block level cloning, thin provisioning, de-duplication, etc. As noted above, Block Storage sits underneath both NFS and Object stores, but as a consumer you are abstracted away and cannot make direct use of them.
