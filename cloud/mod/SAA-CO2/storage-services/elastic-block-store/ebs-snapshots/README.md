# EBS Snapshots

Amazon EBS Snapshots creates backup copies of your data in your EBS volumes. Snapshots are stored in a protected part of Amazon S3 as part of the managed service. Storing snapshots on Amazon S3 protects your data with eleven 9's of durability and provides you Regional access and availability.

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

When you delete a snapshot, only the data unique to that snapshot is removed. Any information contained in that snapshot that is required by other snapshots remains available and is not deleted.

EBS Snapshot events are tracked through CloudWatch events. An event is generated each time you create a single snapshot or multiple snapshots, copy a snapshot, or share a snapshot.

With EBS Snapshots, you can create backup copies of critical workloads, such as a large database or a file system that spans across multiple EBS volumes. Multi-volume snapshots let you take exact point-in-time, data-coordinated, and crash-consistent snapshots across multiple EBS volumes attached to an EC2 instance.

> ### How incremental snapshots work
>
> Incremental snapshots provide the point-in-time current state for your EBS volumes. Incremental snapshots use pointers to reference previous data that remains current in the incremental snapshot. Only the current data is retained per incremental snapshot.
>
> 1. An initial point-in-time snapshot is created containing all of the data within your EBS volume.
>
> 2. For each incremental snapshot after that:
>
>   a. All new data is copied to the new incremental snapshot.
>
>   b. Previously existing and unchanged data is not copied to the incremental snapshot. The new snapshot references the previously existing unchanged data. Any previously existing data that has been changed or deleted is not included in the new incremental snapshot.
>
> Any saved snapshot contains all of the data or references necessary to restore to that point in time.

> ### Available snapshot actions
>
> EBS Snapshots service provides you the capabilities to manage your snapshots manually or using an automated process.
>
> * **Create snapshots** - You can create manual snapshots or create snapshot schedules. Snapshots occur asynchronously; the point-in-time snapshot is created immediately, but the status of the snapshot is **pending** until the snapshot is complete. The snapshot is complete when all modified blocks are transferred to Amazon S3.
>
> * **Delete snapshots** - You can delete any snapshot whether it is a full or incremental snapshot. When you delete a snapshot, only the data that is referenced exclusively by that snapshot is removed. Unique data is only deleted if all of the snapshots that reference it are deleted.
>
> * **Copy a snapshot** - You can copy a completed snapshot within the same Region or from one AWS Region to another. The snapshot copy receives an ID that is different from the ID of the original snapshot.
>
> * **View snapshot information** - You can view detailed information about your snapshot using the **describe-snapshots** AWS CLI command or **Get-EC2Snapshot** command using AWS Tools for Windows PowerShell. You can filter the results based on various fields including tags, specific volume, date ranges, and snapshot owner.
>
> * **Share a snapshot** - By modifying the permissions of a snapshot, you can share it with other AWS accounts that you specify. Authorized users can use the shared snapshots as the basis for creating their own EBS volumes. Shared snapshot copies can be modified by the authorized user; however, your original snapshot remains unaffected.

## Copy and share snapshots

A snapshot is constrained to the AWS Region where it was created. After you create a snapshot of an EBS volume, you can use it to create new volumes in the same Region. You can also copy snapshots across AWS Regions, making it possible to use multiple Regions for geographical expansion, data center migration, and disaster recovery. You can copy any accessible snapshot that has a **completed** status.

You can share a snapshot across AWS accounts by modifying its access permissions. You can also make copies of snapshots that have been shared with you.

## Encryption support

EBS Snapshots fully support EBS encryption. To modify volume encryption, you must own the volume or have access to it. Snapshot encryption follows a set of prescribed rules to provide a predictable experience. 

* Snapshots of encrypted volumes are automatically encrypted.

* Volumes created from encrypted snapshots are automatically encrypted.

* Volumes created from an unencrypted snapshot can be encrypted during the creation process.

* When you copy an unencrypted snapshot, you can encrypt it during the copy process.

* When you copy an encrypted snapshot, you can re-encrypt it with a different encryption key during the copy process.

* The first snapshot taken of an encrypted volume that was created from an unencrypted snapshot is always a full snapshot.

* The first snapshot taken of a re-encrypted volume that has a different encryption key from the source snapshot is always a full snapshot.

> [!NOTE]
>
> A new full snapshot copies all of the data in your EBS volume, which results in additional storage costs and can result in additional delay.

## Amazon Data Lifecycle Manager

You can use Amazon Data Lifecycle Manager (Amazon DLM) to automate the creation, retention, and deletion of snapshots that you use to back up your EBS volumes and Amazon EBS-backed Amazon Machine Images (AMIs).

Amazon DLM uses a combination of elements to automate the lifecycle management process.

* EBS snapshots are one of the primary resource and lifecycle policy types for Amazon DLM. 

* EBS-backed AMIs provide the information that's required to launch an EC2 instance. EBS-backed AMIs are also primary resources and lifecycle policy types for Amazon DLM.

* Target resource tags are used to identify the resources to back up. Tags are customizable metadata that you assign to your AWS resources including EC2 instances, EBS volumes, and snapshots.

* Amazon DLM tags are specific tags applied to all snapshots and AMIs created by a lifecycle policy. These tags distinguish them from snapshots and AMIs created by any other means. You can also specify custom tags to be applied to snapshots and AMIs on creation.

* Lifecycle policies are created using core policy settings to define the automated policy action and behavior.

* Policy schedules determine when and how often the lifecycle policy is ran.

> ### Lifecycle policies
>
> Lifecycle policy consists of these core settings:
>
> * **Policy type** - Defines the type of resources that the policy can manage. Amazon DLM supports two types of lifecycle policies:
>
>   * **Snapshot lifecycle policy** - Used to automate the lifecycle of EBS snapshots. These policies can target EBS volumes and instances.
>
>     * **Cross-account copy event policy** - Used to automate the copying of snapshots across accounts. This policy type should be used in conjunction with an EBS snapshot policy that shares snapshots across accounts.
>
>   * **EBS-backed AMI lifecycle policy** - Used to automate the lifecycle of EBS-backed AMIs. These policies can target instances only.
>
> * **Resource type** - Defines the type of resources that are targeted by the policy. Snapshot lifecycle policies can target instances or volumes. Use **VOLUME** to create snapshots of individual volumes, or use **INSTANCE** to create multi-volume snapshots of all of the volumes that are attached to an instance. AMI lifecycle policies can target instances only. One AMI is created that includes snapshots of all of the volumes that are attached to the target instance.
>
> * **Target tags** - Specifies the tags that must be assigned to an EBS volume or an Amazon EC2 instance for it to be targeted by the policy.
>
> * **Schedules** - The start times and intervals for creating snapshots or AMIs. The first snapshot or AMI creation operation starts within one hour after the specified start time. Subsequent snapshot or AMI creation operations start within one hour of their scheduled time.
>
> * **Retention** - Specifies how snapshots or AMIs are to be retained. You can retain snapshots or AMIs based on their total count (count-based) or their age (age-based).
>
>   * For snapshot policies, when the retention threshold is reached, the oldest snapshot is deleted.
>
>   * For AMI policies, when the retention threshold is reached, the oldest AMI is deregistered and its backing snapshots are deleted.

> ### Policy schedules
>
> Policy schedules define when snapshots or AMIs are created by the policy. Policies can have up to four schedules - one mandatory schedule and up to three optional schedules.
>
> Adding multiple schedules to a single policy lets you create snapshots or AMIs at different frequencies using the same policy. For example, you can create a single policy that creates daily, weekly, monthly, and yearly snapshots. This eliminates the need to manage multiple policies. 
>
> For each schedule, you can define the frequency, fast snapshot restore settings (*fsnapshot lifecycle policies only*), cross-Region copy rules, and tags. The tags that are assigned to a schedule are automatically assigned to the snapshots or AMIs that are created when the schedule is activated. In addition, Amazon DLM automatically assigns a system-generated tag based on the schedule's frequency to each snapshot or AMI.
>
> Each schedule is activated individually based on its frequency. If multiple schedules are activated at the same time, Amazon DLM creates only one snapshot or AMI and applies the retention settings of the schedule that has the highest retention period. The tags of all of the activated schedules are applied to the snapshot or AMI.

## Learning summary

> Amazon EBS Snapshots create incremental point-in-time copies of your data. As a managed AWS service, your snapshots are stored in a managed Amazon S3 bucket. CloudEndure Disaster Recover provides a disaster recovery service for on-premises and cloud-based systems. AWS DataSync provides data copy services between on-premises storage and AWS Cloud storage. AWS CloudTrail monitors and logs API calls to AWS services.
