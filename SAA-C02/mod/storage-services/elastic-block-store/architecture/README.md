# Architecture

## Basic architecture

The basic Amazon EBS architecture consists of EBS volumes attached to an EC2 instance. 

* You can have multiple EBS volumes and different EBS volume types attached to the same instance.

* The EC2 instance and the attached EBS volumes reside in a single Availability Zone. You cannot attach EBS volumes to EC2 instances in a different Availability Zone.

* You can create Amazon EBS Snapshots for each of your EBS volumes. The snapshots are stored in an AWS managed Amazon S3 bucket within the same AWS Region where your EBS volume resides.

* Access to the EC2 instances and the EBS volumes is controlled using AWS Identity and Access Management (IAM) service. Users, groups, and roles must have permissions to access the EC2 instance and the EBS volumes.

* You can use any connectivity method to access your resources in your virtual private cloud (VPC) in the AWS Region.  

![Fig. 1 Basic Amazon EBS architecture](../../../../../img/SAA-CO2/storage-services/elastic-block-storage/architecture/diag01.png)

## Advanced architecture - Multi-Attach

Multi-Attach Amazon EBS architecture consists of multiple EC2 instances connected to a single EBS volume.

* Multi-Attach is only supported with Provisioned IOPS SSD (io1 and io2) EBS volume types.

* The EC2 instances and the attached EBS volume reside in a single Availability Zone. You cannot attach EBS volumes to EC2 instances in a different Availability Zone.

* Amazon EBS does not manage data consistency for multiple writers. Your application or operating system environment must manage data consistency operations.

* You can create EBS snapshots for your Multi-Attach EBS volume. The snapshots are stored in an AWS managed Amazon S3 bucket within the same AWS Region where your EBS volume resides.

* Access to the EC2 instances and the EBS volumes is controlled using IAM service. Users, groups, and roles must have permissions to access the EC2 instance and the EBS volumes.

* You can use any connectivity method to access your resources in your VPC in the AWS Region.  

![Fig. 2 EBS Volume multi-attached to EC2 instances architecture](../../../../../img/SAA-CO2/storage-services/elastic-block-storage/architecture/diag02.png)

## Advanced architecture - Striped volumes

Amazon EBS architecture consists of multiple EBS volumes in a striped redundant array of independent disks (RAID) type configuration. The striped configuration uses a RAID 0 style process to increase the volume size and increase performance for the combined EBS volumes. 

* In a striped configuration, the volumes operate as a single EBS volume.

* A striped EBS volume attaches to a single EC2 instance.

* The EC2 instances and the attached EBS volume reside in a single Availability Zone. You cannot attach EBS volumes to EC2 instances in a different Availability Zone.

* You can create EBS snapshots for your striped EBS volume. The snapshots are stored in an AWS managed Amazon S3 bucket within the same AWS Region where your EBS volume resides.

* Access to the EC2 instances and the EBS volumes is controlled using IAM service. Users, groups, and roles must have permissions to access the EC2 instance and the EBS volumes.

* You can use any connectivity method to access your resources in your Virtual Private Cloud (VPC) in the AWS Region.  

![Fig. 3 EBS volumes configured for striped RAID architecture](../../../../../img/SAA-CO2/storage-services/elastic-block-storage/architecture/diag03.png)
