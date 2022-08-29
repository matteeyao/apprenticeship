# EBS Encryption

1. An application is hosted on an EC2 instance with multiple EBS Volumes attached and uses Amazon Neptune as its database. To improve data security, you encrypted all of the EBS volumes attached to the instance to protect the confidential data stored in the volumes. 

Which of the following statements are true about encrypted Amazon Elastic Block Store volumes? (Select TWO.)

[ ] Only the data in the volume is encrypted and not all the data moving between the volume and the instance.

[ ] The volumes created from the encrypted snapshot are not encrypted.

[ ] Snapshots are not automatically encrypted.

[x] All data moving between the volume and the instance are encrypted.

[x] Snapshots are automatically encrypted.

**Explanation**: When you create an encrypted EBS volume and attach it to a supported instance type, the following types of data are encrypted:

* Data at rest inside the volume

* All data moving between the volume and the instance

* All snapshots created from the volume

* All volumes created from those snapshots

Encryption operations occur on the servers that host EC2 instances, ensuring the security of both data-at-rest and data-in-transit between an instance and its attached EBS storage. You can encrypt both the boot and data volumes of an EC2 instance.

<br />

2. A company needs to launch an Amazon EC2 instance with persistent block storage to host its application. The stored data must be encrypted at rest.

Which of the following is the most suitable storage solution in this scenario?

[ ] Encrypted Amazon EC2 Instance Store using AWS KMS

[ ] Amazon EBS volume w/ server-side encryption (SSE) enabled.

[x] Encrypted Amazon EBS volume using AWS KMS

[ ] Amazon EC2 Instance Store w/ SSL encryption

**Explanation**: **Amazon Elastic Block Store (Amazon EBS)** provides block-level storage volumes for use with EC2 instances. EBS volumes behave like raw, unformatted block devices. You can mount these volumes as devices on your instances. EBS volumes that are attached to an instance are exposed as storage volumes that persist independently from the life of the instance.

Amazon EBS is the persistent block storage volume among the options given. It is mainly used as the root volume to store the operating system of an EC2 instance. To encrypt an EBS volume at rest, you can use AWS KMS customer master keys for the encryption of both the boot and data volumes of an EC2 instance.

Hence, the correct answer is: **Encrypted Amazon EBS volume using AWS KMS**.

> The options that say: **Amazon EC2 Instance Store with SSL encryption** and **Encrypted Amazon EC2 Instance Store using AWS KMS** are both incorrect because the scenario requires persistent block storage and not temporary storage. Also, enabling SSL is not a requirement in the scenario as it is primarily used to encrypt data in transit.

> The option that says: **Amazon EBS volume with server-side encryption (SSE) enabled** is incorrect because EBS volumes are only encrypted using AWS KMS. Server-side encryption (SSE) is actually an option for Amazon S3, but not for Amazon EC2.

<br />
