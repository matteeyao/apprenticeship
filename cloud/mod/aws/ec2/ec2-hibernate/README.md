# EC2 Hibernate

## EBS behaviors reviewed

We have learned so far we can stop and terminate EC2 instances. If we stop the instance, the **data is kept on the disk (w/ EBS)** and will remain on the disk until the EC2 instance is started. If the instance is terminated, then by default **the root device volume will also be terminated**.

When we start our EC2 instance, the following happens:

* **Operating system** boots up

    * loads Windows or Linux

* User data script is run (**bootstrap scripts**)

    * A script that runs when booted; could install Apache for example to turn your EC2 instance into a web server

* **Applications start** (can take some time)

## EC2 Hibernate

When you hibernate an EC2 instance, the operating system is told to perform hibernation (suspend-to-disk). Hibernation **saves the contents** from the instance memory (RAM) to your Amazon EBS root volume. We persist the instance's Amazon EBS root volume and any attached Amazon EBS data volumes.

Essentially, takes the RAM and saves it to our EBS root volume, so that when we reboot, load-up times are faster

## Starting your EC2 instance w/ EC2 Hibernate

When you start your instance out of hibernation:

* The **Amazon EBS** root volume is restored to its previous state

* The **RAM** contents are reloaded

* The processes that were previously running on the instance are resumed, so the OS doesn't have to be started-up again

* Previously attached data volumes are **reattached and the instance retains its instance ID**

![Hibernation flow](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/images/hibernation-flow.png)

W/ **EC2 Hibernate**, the instance boots much faster. The operating system does not need to reboot b/c the in-memory state (RAM) is preserved. This is useful for:

1. Long-running processes

2. Services that take time to initialize

## Learning summary

* **EC2 Hibernate** preserves the in-memory RAM on persistent storage (EBS)

* Much faster to boot up b/c you **do not need to reload the operating system**

* Instance RAM must be less than **150 GB**

* Instance families include C3, C4, C5, M3, M4, M5, R3, R4, and R5

* Available for Windows, Amazon Linux 2 AMI, and Ubuntu

* Instances can't be hibernated for more than 60 days

* Available for **On-Demand instances** and **Reserved instances**
