# EBS Encryption

* EBS encryption offers a straight-forward encryption solution for EBS resources that don't require you to build, maintain, and secure your own key management infrastructure.

* It uses AWS Key Management Service (AWS KMS) customer master keys (CMK) when creating encrypted volumes and snapshots.

* You can encrypt both the root device and secondary volumes of an EC2 instance. When you create an encrypted EBS volume and attach it to a supported instance type, the following types of data are encrypted:

  * Data at rest inside the volume

  * All data moving between the volume and the instance

  * All snapshots created from the volume

  * All volumes created from those snapshots

* EBS encrypts your volume w/ a data key using the AES-256 algorithm

* Snapshots of encrypted volumes are naturally encrypted as well. Volumes restored from encrypted snapshots are also encrypted. You can only share un-encrypted snapshots.

* The old way of encrypting a root device was to create a snapshot of a provisioned EC2 instance. While making a copy of that snapshot, you then enabled encryption during the copy's creation. Finally, once the copy was encrypted, you then created an AMI from the encrypted copy and used to have an EC2 instance w/ encryption on the root device. B/c of how complex this is, you can now simply encrypt root devices as part of the EC2 provisioning options.
