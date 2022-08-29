# Planning and Designing an Amazon S3 File Gateway Deployment

## Deploying the solution

You can deploy the Storage Gateway hybrid storage solution as on-premises hardware or as a virtual appliance. It offers Server Message Block (SMB) or Network File System (NFS) access to data on Amazon Simple Storage Service (Amazon S3) with local caching.

To deploy the solution, you must create the file gateway and then add a file share to it.

## Creating the S3 File Gateway

To create the hybrid storage solution and use it, you first need to host the gateway appliance. Then, you securely connect it to the **Storage Gateway** service in your account.

### Hosting the gateway appliance

The on-premises component of the solution, the Storage Gateway appliance, must be hosted. Based on your on-premises infrastructure needs, you can deploy the gateway appliance in one of following ways:

* **On-premises**

  * Deploy it by downloading a virtual machine (VM) appliance from the AWS Management Console and deploying it on VMware ESXi Hypervisor, Microsoft Hyper-V, or Linux Kernel-based Virtual Machine (KVM). 

  * Deploy it on a purchased hardware appliance. A hardware appliance is a physical, standalone, validated server configuration for on-premises deployments. It comes preloaded with Storage Gateway software. And it provides all the required central processing unit (CPU), memory, network, and solid state drive (SSD) cache resources for creating and configuring S3 File Gateway.

* **On AWS**

  * Deploy it on an Amazon Machine Image (AMI) in Amazon Elastic Compute Cloud (Amazon EC2). Storage Gateway provides an AMI that contains the gateway VM image. Gateway types supported include file, cached volume, and tape. Deploying a gateway on Amazon EC2 does not provide the advantages of a hybrid infrastructure. But this deployment method can be useful for learning how to set up and operate Storage Gateway. Typical use cases include proof of concept, disaster recovery, and data mirroring.

After you deploy the Storage Gateway appliance, you activate it and configure its storage. At that point, you are ready to start using the cloud-based storage.

### Connectivity between the gateway appliance and service

When a Storage Gateway appliance is deployed, it must communicate back to the Storage Gateway service for both management and data movement.

![Fig. 1 Storage Gateway overview](../../../../../img/SAA-CO2/storage-gateway/s3-file-gateway/planning-and-desiging-s3-file-gateway-deployment/diag01.png)

> Diagram showing how the Storage Gateway appliance communicates back to the Storage Gateway managed service through service endpoints.

The connectivity between the gateway and the service is realized through service endpoints:

* **Public endpoint** ▶︎ Storage Gateway connects to a public endpoint over the internet.

* **VPC endpoint** ▶︎ Storage Gateway connects to Storage Gateway virtual private cloud (VPC) endpoints over a private connection to AWS using AWS Direct Connect or AWS Virtual Private Network (AWS VPN).

* **Federal Information Processing Standards (FIPS) 140-2 compliant endpoints** ▶︎ Storage Gateway connects to a public endpoint over the internet. This endpoint complies with FIPS standards to further protect sensitive information for regulated workloads in AWS GovCloud (US) AWS Regions.

### Creating and activating an S3 File Gateway

There are four basic steps for creating an S3 File Gateway.

> ### 1. Select the gateway type
>
> Name your gateway, select its time zone based on where the gateway appliance will be deployed, and choose the gateway type to deploy; in this case, S3 File Gateway.

> ### 2. Choose the host platform and deploy the gateway appliance
>
> Select a platform for hosting your S3 File Gateway appliance:
>
> * **On-premises**:
>
>   * Deploy a hardware appliance.
>
>   * Download and deploy a gateway VM.
>
> As you are deploying the gateway VM to create the gateway appliance, you will connect it to your network. You will also associate a disk for the local cache (150 GiB minimum).
>
> * **On AWS, using an EC2 instance**:
>
>   * **Storage Gateway** provides an AMI, which holds the gateway VM image.
>
> With this option, you lose the benefit of the local cache, but it is a useful way to quickly learn the technology and provide your local infrastructure storage on the cloud.

> ### 3. Connect to and activate the gateway
>
> You connect the hosted S3 File Gateway appliance to AWS through a service endpoint. If you choose the publicly accessible endpoint, you can also select FIPS-enabled endpoint if the connection must comply with FIPS. If you choose VPC hosted, you must specify an existing VPC endpoint, or provide its Domain Name System (DNS) name or IP address.
>
> You have two options when specifying how your deployed gateway appliance will be identified in the connection and securely associated with your AWS account.
>
> * You can provide an IP address that is either public or accessible from within your current network.
>
> * You can generate an activation key using the gateway's local console and use that instead of the IP address.

> ### 4. Configure local disks
>
> When you deploy the VM for the Storage Gateway appliance on premises, you allocate a local disk as the cache of the S3 File Gateway. You also need to configure your Storage Gateway to use this disk as cache.
>
> When deciding on the size of the cache storage, remember that a larger local cache means that more data is readily available on premises, decreasing the cost of retrieving the data from Amazon S3. Generally, you should allocate at least 20 percent of your existing file store size as cache storage. As your application changes, you can increase the gateway's cache storage capacity by adding new disks in your host. 
>
> This cache will be used by all the file shares on the S3 File Gateway appliance. The maximum supported size of the local cache for a gateway running on a VM is 64 TiB. You can configure one or more local drives for your cache, up to the maximum capacity. When adding cache to an existing gateway, create new disks in your host. Do not change the size of previously allocated disks.
>
> To optimize gateway performance, consider adding high-performance disks such as solid state drives (SSDs) or an NVMe controller, or attach a virtual disk to your VM directly. A high-performance disk generally results in better throughput and more input/output operations per second (IOPS).
>
> ![Fig. 2 S3 File Gateway appliance](../../../../../img/SAA-CO2/storage-gateway/s3-file-gateway/planning-and-desiging-s3-file-gateway-deployment/diag02.png)

## Adding file shares to the gateway appliance

After you create and activate the S3 File Gateway, you can create file shares that can be mounted on your Linux or Windows servers. Each file share is associated with a unique S3 bucket or a unique prefix on the same bucket. It is accessed by a specified protocol, SMB for Windows and NFS for Linux. Currently file metadata, such as ownership, stored as S3 object metadata cannot be mapped across different protocols. A file gateway, however, can host one or more file shares of different types.

![Fig. 3 S3 File Gateway file share protocols](../../../../../img/SAA-CO2/storage-gateway/s3-file-gateway/planning-and-desiging-s3-file-gateway-deployment/diag03.png)

> S3 File Gateway appliance with two file shares. Each file share is associated with a bucket, and it writes objects to a specific Amazon S3 storage class. Objects are accessed with either NFS or SMB protocols.

## Additional considerations

### Region

Make sure that your Storage Gateway and the Storage Gateway hardware appliance are supported in your desired Regions.

![Fig. 4 Storage Gateway region consideration](../../../../../img/SAA-CO2/storage-gateway/s3-file-gateway/planning-and-desiging-s3-file-gateway-deployment/diag04.png)

> AWS Storage Gateway can store file data in any Region where the S3 buckets for your file shares are located.

Select a Region before you deploy your gateway, and your gateway is then activated in that Region. However, the S3 File Gateway stores file data in any Region where the S3 buckets for your file shares are located.

### Amazon S3 storage classes

Storage Gateway supports various Amazon S3 storage classes, with the default being the Amazon S3 Standard. The Amazon S3 storage classes are purpose built, optimizing cost based on different access patterns.

When setting up your file share, you select the storage class for your objects. Available classes are S3 Standard, S3 Intelligent-Tiering, S3 Standard-Infrequent Access (S3 Standard-IA), and S3 One Zone-Infrequent Access (S3 One Zone-IA).

Here is a short summary of the use cases of each storage class:

![Fig. 4 S3 storage classes](../../../../../img/SAA-CO2/storage-gateway/s3-file-gateway/planning-and-desiging-s3-file-gateway-deployment/diag05.png)

We recommend that you write objects directly from a file share to the S3 Standard storage class and use a lifecycle policy to transition the objects to other storage classes. It is, however, possible to write directly to S3 Standard-IA, S3 One Zone-IA, and S3 Intelligent-Tiering storage classes directly from the file share. Objects stored in any of these storage classes can be transitioned to S3 Glacier Flexible Retrieval, or other storage classes, using a lifecycle policy.

## Working with objects

How will objects be accessed and for what purpose? Storage Gateway makes it possible for applications in distributed locations to have on-demand, low-latency access to data stored in AWS for application workflows that can span the globe. After objects are stored in Amazon S3, they can be accessed in AWS for in-cloud workloads without requiring S3 File Gateway. The data can be processed in the cloud, and the gateway cache can be refreshed for up-to-date results. You can refresh a cache at the bucket or prefix level.
