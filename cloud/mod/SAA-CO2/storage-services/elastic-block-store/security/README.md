# Data security

## AWS Identity and Access Management

AWS uses security credentials to identify you and to grant you access to your AWS resources. You can use features of IAM to allow other users, services, and applications to use your AWS resources fully or in a limited way, without sharing your security credentials. IAM for Amazon EBS resources is closely associated with Amazon EC2 resources.

By default, IAM identities (users, groups, and roles) don't have permission to create, view, or modify AWS resources. To allow users, groups, and roles to access Amazon EC2 and Amazon EBS resources and interact with the EC2 console and API, create an IAM policy. The IAM policy grants permission to use the specific resources and API actions they will need. You then attach the policy to the IAM identity that requires access.

AWS addresses many common use cases by providing standalone IAM policies that are created and administered by AWS. Managed policies grant necessary permissions for common use cases so that you can avoid having to investigate what permissions are needed. You can create specific IAM policies for your Amazon EBS resources.

Using security policies, you can restrict access and limit management capabilities for your EBS volumes.

## Amazon EBS encryption

![Fig. 1 AWS Key Management Service](../../../../../img/SAA-CO2/storage-services/elastic-block-storage/security/diag01.png)

Use Amazon EBS encryption as a straight-forward encryption solution for your EBS resources associated with your EC2 instances. With Amazon EBS encryption, you aren't required to build, maintain, and secure your own key management infrastructure. 

* Amazon EBS encryption uses AWS Key Management Service (AWS KMS) or customer managed keys (CMK) when creating encrypted volumes and snapshots. 

* Encryption operations occur on the servers that host the EC2 instances, ensuring the security of both data at rest and data in transit between an EC2 instance and its attached EBS storage.

* You can attach both encrypted and unencrypted volumes to an instance simultaneously.

### How EBS encryption works

You can encrypt both the EBS boot and data volumes of an EC2 instance. When you create an encrypted EBS volume and attach it to a supported instance type, the following types of data are encrypted:

* Data at rest inside the volume

* All data moving between the EBS volume and the EC2 instance

* All snapshots created from the EBS volume

* All EBS volumes created from those snapshots

EBS encrypts your volume with a data key using the industry-standard AES-256 algorithm. Your data key is stored on-disk with your encrypted data, but not before EBS encrypts it with your CMK. Your data key never appears on disk in plaintext. The same data key is shared by snapshots of the volume and any subsequent volumes created from those snapshots.
