# Optimizing Cost and Performance

> Increasing knowledge, implementing best practices, and cost optimization strategies contribute to a successful deployment.

## Best practices: Archive management

For the purpose of optimizing costs, remember that archiving tapes lowers the cost of storage. 

* It's important to eject or export your tapes from the backup application once backup jobs are completed. This will automatically archive tapes, thus moving the tape from S3 Standard to S3 Glacier Flexible Retrieval or S3 Glacier Deep Archive.

* You only pay for how much storage is actually consumed, not storage that is provisioned. You can configure a tape of 5 TiB in size, but if you only write 100 GiB to it, you will only pay for 100 GiB instead of 5 TiB.

## Performance recommendations

### Optimize iSCSI settings

You can optimize iSCSI settings on your iSCSI initiator to achieve higher I/O performance. We recommend choosing 256 KiB for MaxReceiveDataSegmentLength and FirstBurstLength, and 1 MiB for MaxBurstLength.

### Back gateway virtual disks with separate physical disks

When you provision gateway disks, it is strongly recommended that you don't provision local disks for the upload buffer and cache storage that use the same underlying physical storage disk. For example, for VMware ESXi, the underlying physical storage resources are represented as a data store. When you deploy the gateway VM, you choose a data store on which to store the VM files. When you provision a virtual disk (for example, as an upload buffer), you can store the virtual disk in the same data store as the VM or a different data store.

### Use a larger block size for tape drives

Use large block sizes (configurable within the backup application) to write backups to your Tape Gateway.

For a Tape Gateway, the default block size for a tape drive is 64 KB. However, you can increase the block size up to 1 MB to improve I/O performance.

The block size that you choose depends on the maximum block size that your backup application supports. We recommend that you set the block size of the tape drives to a size that is as large as possible. However, this block size must not be greater than the 1 MB maximum size that the gateway supports.

### Write backup jobs to multiple tape drives

To achieve better write throughput, set up your backup application to write backup jobs to multiple tape drives at the same time.

AWS recommends that you configure backup jobs in your backup application to use at least four virtual tape drives simultaneously on the Tape Gateway.

Tape Gateway comes with a set of 10 tape drives and all 10 can be written to simultaneously.
