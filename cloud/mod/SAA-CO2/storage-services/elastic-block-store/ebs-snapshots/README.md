# EBS Snapshots

* EBS Snapshots are incremental, point-in-time copies of volumes. You can think of Snapshots as photographs of the disk's current state and the state of everything within it.

* A snapshot is constrained to the region where it was created.

* Snapshots only capture the state of change from when the last snapshot was taken. This is what is recorded in each new snapshot, not the entire state of the server.

* B/c of this, it may take some time for your first snapshot to be created. This is b/c the very first snapshot's change of the state is the entire new volume. Only afterwards will the delta be captured b/c there will then be something previous to compare against.

* EBS snapshots occur asynchronously which means that a volume can be used as normal while a snapshot is taking place.

* When creating a snapshot for a future root device, it is considered best practice to stop the running instance where the original device is before taking the snapshot.

* The easiest way to move an EC2 instance and a volume to another availability zone is to take a snapshot.

* When creating an image from a snapshot, if you want to deploy a different volume type for the new image (e.g. General Purpose SSD → Throughput Optimized HDD) then you must make sure that the virtualization for the image is hardware-assisted.

* A short summary for creating copies of EC2 instances: Old instances → Snapshot → Image (AMI) → New instance

* You cannot delete a snapshot of an EBS Volume that is used as the root device of a registered AMI. If the original snapshot was deleted, then the AMI would not be able to use it as the basis to create new instances. For this reason, AWS protects you from accidentally deleting the EBS Snapshot, since it could be critical to your systems. To delete an EBS Snapshot attached to a registered AMI, first remove the AMI, then the snapshot can be deleted.

* EBS Snapshots are a fully managed backup service built in w/ Amazon EBS.

  * EBS Snapshots are low-cost, by using incremental snapshots to back up only your data blocks.

  * Your snapshots are protected. Your snapshot data is stored in Amazon S3 and provides eleven 9s (99.999999999) of data durability.

  * W/ snapshots, your snapshots add data protection and agility. You can quickly restore EBS volumes and copy EBS volumes across Availability Zones within a Region.
