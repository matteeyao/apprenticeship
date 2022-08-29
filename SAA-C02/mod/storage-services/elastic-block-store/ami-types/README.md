# AMI Types (EBS vs Instance Store)

You can select your Amazon Machine Image (AMI) based on:

* Region (see Regions and Availability Zones)

* Operating system

* Architecture (32-bit or 64-bit)

* Launch Permissions

* Storage for the Root Device (Root Device Volume)

    * Instance Store (**EPHEMERAL STORAGE**)

    * EBS Backed Volumes

## EBS vs Instance store volumes

All AMIs are categorized as either backed by Amazon EBS or backed by Instance Store

For **EBS Volumes**: The root device for an instance launched from the AMI is an Amazon EBS volume created from an Amazon EBS snapshot

For **Instance Store Volumes**: The root device for an instance launched from the AMI is an instance store volume created from a template stored in Amazon S3

## Learning summary

> * Instance Store Volumes are sometimes called Ephemeral Storage
>
> * Instance Store Volumes cannot be stopped. If the underlying host fails, you will lose your data
>
> * EBS backed instances can be stopped. You will not lose the data on this instance if it is stopped
>
> * You can reboot both, you will not lose your data
>
> * By default, both ROOT volumes will be deleted on termination. However, w/ EBS volumes, you can tell AWS to keep the root device volume
