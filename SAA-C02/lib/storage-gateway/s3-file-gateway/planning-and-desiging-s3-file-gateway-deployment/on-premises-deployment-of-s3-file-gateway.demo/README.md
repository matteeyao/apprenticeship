# Planning and Designing an Amazon S3 File Gateway Deployment

1. Which statement is true about configuring local disks for the Amazon S3 File Gateway?

**You must allocate at least one disk for the cache.**

**Explanation**: Associate a disk size for the local cache of at least 150 GiB. The maximum
supported size of the local cache for a gateway running on a VM is 64 TiB. You can configure one or more local drives for your cache, up to the maximum capacity.

2. Which components are supported as host platforms for AWS Storage Gateway? (Select THREE.)

[ ] Amazon Lightsail instance

[x] Amazon EC2 instance

[x] Hardware appliance

[x] VMware ESXi virtual machine

**Explanation**: You can deploy the Storage Gateway appliance using the following configurations:

– On premises as a VM appliance on a VMware ESXi Hypervisor, Microsoft Hyper-V, or Linux KVM

– On a purchased hardware appliance

– As an AMI in Amazon EC2 on AWS

3. Which statements are true about Amazon S3 File Gateway? (Select TWO.) 

[x] File metadata is stored in object metadata.

[ ] It uses an upload buffer.

[ ] The RefreshCache application programming interface (API) is not supported.

[x] There is a one-to-one mapping from Network File Share (NFS) files to S3 objects.

**Explanation**: Files written to the file share become objects in the S3 bucket, with a one-to-one mapping between files and objects. Metadata, such as ownership and timestamps, are stored with the object. File paths become part of the object's key, and thus maintain consistent name space.
