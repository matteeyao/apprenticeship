# EBS

> Amazon Elastic Block Store (EBS) provides persistent block storage volumes for use w/ Amazon EC2 instances in the AWS Cloud. Each Amazon EBS volume is automatically replicated within its Availability Zone to protect you from component failure, offering high availability and durability

* Essentially, a virtual hard disk drive in the cloud

## Five different types of EBS storage:

* General Purpose (SSD)

* Provisioned IOPS (SSD)

* Throughput Optimized Hard Disk Drive

* Cold Hard Disk Drive

* Magnetic

## Comparison of EBS types

<table>
    <thead>
        <tr>
            <th colspan=3>Solid-State Drives (SSD)</th>
            <th colspan=3>Hard disk Drives (HDD)</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Volume Type</td>
            <td><b>General Purpose SSD</b></td>
            <td><b>Provisioned IOPS SSD</b></td>
            <td><b>Throughput Optimized HDD</b></td>
            <td><b>Cold HDD</b></td>
            <td><b>EBS Magnetic</b></td>
        </tr>
        <tr>
            <td>Description</td>
            <td>General purpose SSD volume that balances price and performance for a wide variety of transactional workloads</td>
            <td>Highest-performance SSD volume designed for mission-critical applications</td>
            <td>Low cost HDD volume designed for frequently accessed, throughput-intensive workloads</td>
            <td>Lowest cost HDD volume designed for less frequently accessed workloads</td>
            <td>Previous generation HDD</td>
        </tr>
        <tr>
            <td>Use Cases</td>
            <td><b>Most Work Loads</b></td>
            <td><b>Databases</b></td>
            <td><b>Big Data & Dara Warehouses</b></td>
            <td><b>File Servers</b></td>
            <td><b>Workloads where data is infrequently accessed</b></td>
        </tr>
        <tr>
            <td>API Name</td>
            <td>gp2</td>
            <td>io1</td>
            <td>st1</td>
            <td>sc1</td>
            <td>Standard</td>
        </tr>
        <tr>
            <td>Volume Size</td>
            <td>1 GiB - 16 TiB</td>
            <td>4 GiB - 16 TiB</td>
            <td>500 GiB - 16 TiB</td>
            <td>500 GiB - 16 TiB</td>
            <td>1 GiB - 1 TiB</td>
        </tr>
        <tr>
            <td>Max. IOPS**/Volume</td>
            <td>16,000</td>
            <td>64,000</td>
            <td>500</td>
            <td>250</td>
            <td>40-200</td>
        </tr>
    </tbody>
</table>

* If you need to optimize throughput, choose Throughput Optimized HDD. If you just want the lowest cost storage available, use Cold Hard Disk Drive.

## EBS Volumes and Snapshots

EBS volumes will always be in the same availability zone as EC2 instance

When you terminate an EC2 instance, by default, the root device volume will also be terminated. However, additional volumes that are attached to that EC2 instance will continue to persist.

## Learning summary

> * Volumes exist on EBS. Think of EBS as a virtual hard disk.

> * Snapshots exist on S3. Think of snapshots as a photograph of the disk.

> * Snapshots are point in time copies of Volumes.

> * *Snapshots are incremental* - this means that only the blocks that have changed since your last snapshot are moved to S3.

> * If this is your first snapshot, it may take some time to create.

* If it is a second snapshot, it will only replicate the Delta, or the changes.

> * To create a snapshot for Amazon EBS volumes that serve as root devices, you should stop the instance before taking the snapshot
>
>   * However, you can take a snap while the instance is running

> * You can create AMI's from Snapshots

> * You can change EBS volume sizes on the fly, including changing the size and storage type

> * Volumes will **ALWAYS** be in the same availability zone as the EC2 instance

* So you cannot have an EX2 instance in one availability zone and an EBS volume in another availability zone

> * To move an EC2 volume from one AZ to another, take a snapshot of it, create an AMI from the snapshot and then use the AMI to launch the EC2 instance in a new AZ

> * To move an EC2 volume from one region to another, take a snapshot of it, create an AMI from the snapshot and then copy the AMI from one region to the other. Then use the copied AMI to launch the new EC2 instance in the new region.

## EC2 takeaways

> * Termination Protection is **turned off** by default, you must turn it on.

* If you want to protect your EC2 instances from being accidentally deleted by your developers or system administrators, ensure Termination Protection is turned on

> * On an EBS-backed instance, the **default action is for the root EBS volume to be deleted** when the instance is terminated

* So if you do go in and terminate your EC2 instances, you are going to delete that root device volume automatically but if you add additional attached volumes to that EC2 instance, those additionally attached volumes won't be deleted automatically, unless you go in and ensure Termination Protection is checked

> * EBS Root Volumes of your DEFAULT AMI's **CAN** be encrypted. You can also use a third party tool (such as bit locker etc) to encrypt the root volume, or this can be done when creating AMI's (remember the lab) in the AWS console or using the API.

> * Additional volumes can be encrypted
