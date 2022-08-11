# Migration options for FSx for Windows File Server

## Migrating existing file storage using AWS DataSync

You can use **AWS DataSync** to migrate files from your existing on-premises or cloud file systems to FSx for Windows File Server. This service automatically handles many of the tasks related to data transfers, including the following:

* Handling encryption

* Managing scripts

* Optimizing the network

* Scheduling and monitoring transfers

* Validating data integrity

To transfer data using DataSync, deploy an on-premises AWS DataSync agent, which is a virtual machine that connects your shared file systems to AWS DataSync. DataSync connects to your storage systems using the Network File System (NFS) or Server Message Block (SMB) protocols. During data transfer, DataSync automatically encrypts your data and accelerates transfer over the WAN. After data is received, DataSync connects to FSx for Windows File Server to copy data to and from FSx for Windows File Server.

## Migrating existing file storage using Robocopy

As an alternative solution, you can use Robust File Copy, or Robocopy. Robocopy is a command line directory and file replication command set for Microsoft Windows.
