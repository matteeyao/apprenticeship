# Optimizing Cost and Performance

> Increasing knowledge, implementing best practices, and cost optimization strategies contribute to a successful deployment.

## Best practices: Volume and snapshot management

Volume storage is billed for only the amount of data stored on the volume, not the size of the volume you create. For the purposes of optimizing storage and costs, it's a best practice to delete older volumes and snapshots that are no longer needed. Deleting a volume does not automatically delete the associated snapshots.

## Delete older snapshots

If you have a backup policy that takes EBS volume snapshots daily or weekly, you quickly accumulate snapshots. Check for snapshots that are more than 30 days old and delete them to reduce storage costs. When you delete a snapshot, only the data that is referenced exclusively by that snapshot is removed. Unique data is only deleted if all the snapshots that reference it are deleted. Deleting previous snapshots of a volume does not affect your ability to create volumes from later snapshots of that volume.

Deleting a snapshot has no effect on the volume. Deleting a volume has no effect on the snapshots made from it.

## Automate backup policies using AWS Backup

AWS Backup simplifies and centralizes backup management, so you can set customizable scheduled backup policies that meet your backup requirements. Using AWS Backup, you create backup plans to set backup retention and expiration rules so you no longer need to develop custom scripts or manually manage the point-in-time backups of your Volume Gateway volumes. 

AWS Backup is directly integrated within the Storage Gateway console. You can manage backup and retention policies for cached and stored volumes of Volume Gateway through AWS Backup.

## Performance recommendations

> ### Optimize iSCSI settings
>
> You can optimize iSCSI settings on your iSCSI initiator to achieve higher I/O performance. We recommend choosing 256 KiB for `MaxReceiveDataSegmentLength` and `FirstBurstLength`, and 1 MiB for `MaxBurstLength`.

> ### Back gateway virtual disks w/ separate physical disks
>
> When you provision gateway disks, it is strongly recommended that you don't provision local disks for the upload buffer and cache storage that use the same underlying physical storage disk. For example, for VMware ESXi, the underlying physical storage resources are represented as a data store. When you deploy the gateway VM, you choose a data store on which to store the VM files. When you provision a virtual disk (for example, as an upload buffer), you can store the virtual disk in the same data store as the VM or a different data store.

> ### Change the volume configuration
>
> If you find that adding more volumes to a gateway reduces the throughput to the gateway, consider adding the volumes to a separate gateway. In particular, if a volume is used for a high-throughput application, consider creating a separate gateway for the high-throughput application. However, as a general rule, you should not use one gateway for all of your high-throughput applications and another gateway for all of your low-throughput applications. To measure your volume throughput, use the `ReadBytes` and `WriteBytes` metrics.
