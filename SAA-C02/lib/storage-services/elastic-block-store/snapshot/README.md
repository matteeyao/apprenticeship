# EBS Snapshots

1. A company has several unencrypted EBS snapshots in their VPC. The Solutions Architect must ensure that all of the new EBS volumes restored from the unencrypted snapshots are automatically encrypted.

What should be done to accomplish this requirement?

[ ] Enable the EBS Encryption By Default feature for the AWS Region.

[ ] Launch new EBS volumes and encrypt them using an asymmetric customer master key (CMK).

[ ] Launch new EBS volumes and specify the symmetric customer master key (CMK) for encryption.

[ ] Enable the EBS Encryption By Default feature for specific EBS volumes.

**Explanation**: You can configure your AWS account to enforce the encryption of the new EBS volumes and snapshot copies that you create. For example, Amazon EBS encrypts the EBS volumes created when you launch an instance and the snapshots that you copy from an unencrypted snapshot.

Encryption by default has no effect on existing EBS volumes or snapshots. The following are important considerations in EBS encryption:

* **Encryption by default** is a Region-specific setting. If you enable it for a Region, you cannot disable it for individual volumes or snapshots in that Region.

* When you enable encryption by default, you can launch an instance only if the instance type supports EBS encryption.

* Amazon EBS does not support asymmetric CMKs.

When migrating servers using AWS Server Migration Service (SMS), do not turn on encryption by default. If encryption by default is already on and you are experiencing delta replication failures, turn off encryption by default. Instead, enable AMI encryption when you create the replication job.

You cannot change the CMK that is associated with an existing snapshot or encrypted volume. However, you can associate a different CMK during a snapshot copy operation so that the resulting copied snapshot is encrypted by the new CMK.

Although there is no direct way to encrypt an existing unencrypted volume or snapshot, you can encrypt them by creating either a volume or a snapshot. If you enabled encryption by default, Amazon EBS encrypts the resulting new volume or snapshot using your default key for EBS encryption. Even if you have not enabled encryption by default, you can enable encryption when you create an individual volume or snapshot. Whether you enable encryption by default or in individual creation operations, you can override the default key for EBS encryption and use symmetric customer-managed CMK.

Hence, the correct answer is: **Enable the EBS Encryption By Default feature for the AWS Region.**

> The option that says: **Launch new EBS volumes and encrypt them using an asymmetric customer master key (CMK)** is incorrect because Amazon EBS does not support asymmetric CMKs. To encrypt an EBS snapshot, you need to use symmetric CMK.

> The option that says: **Launch new EBS volumes and specify the symmetric customer master key (CMK) for encryption** is incorrect. Although this solution will enable data encryption, this process is manual and can potentially cause some unencrypted EBS volumes to be launched. A better solution is to enable the EBS Encryption By Default feature. It is stated in the scenario that all of the new EBS volumes restored from the unencrypted snapshots must be automatically encrypted.

> The option that says: **Enable the EBS Encryption By Default feature for specific EBS volumes** is incorrect because the Encryption By Default feature is a Region-specific setting and thus, you can't enable it to selected EBS volumes only.

<br />
