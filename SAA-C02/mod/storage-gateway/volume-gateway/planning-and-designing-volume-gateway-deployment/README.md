# Planning and Designing a Volume Gateway Deployment

> Storage Gateway provides a strategic, four-step approach to gateway creation, helping customers quickly deploy and implement the hybrid storage service.

## Deploying the solution

The Volume Gateway hybrid storage solution can be deployed as an on-premises appliance or as an Amazon EC2 instance that provides an Internet Small Computer System Interface (iSCSI) mount target to your applications, caches the data locally, and manages the secure transfer of the data up to Amazon Web Services (AWS).

To deploy the solution, create the Volume Gateway and then add volumes.

## Creating the Volume Gateway

To create the hybrid storage solution and use it, you will first deploy the gateway appliance. After it is created, then you securely connect it to the Storage Gateway service in your AWS account.

### Choosing the right configuration

Volume Gateway has two different modes called *cached* and *stored*.

> ### Cached volume
>
> With cached volume, you can use Amazon Simple Storage Service (Amazon S3) as your primary data storage. Frequently accessed data is cached on premises for local use while all of the data is stored in Amazon S3 as its primary location.
>
> The gateway maintains a cache of just the small working set of data that your application needs, minimizing the need to download older data.
>
> Cached volumes can range from 1 gibibyte (GiB) to 32 tebibytes (TiB) in size. Each gateway that is configured for cached volumes can support up to 32 volumes for a total maximum storage volume of 1,024 TiB [1 pebibyte (PiB)].
>
> **Use cases**:
>
> * Custom file shares
>
> * Migrating application data into Amazon S3 and transitioning to Amazon EC2
>
> **Example**:
>
> This Volume Gateway mode is good for applications where you have a large dataset, but your application only needs low-latency access to a subset of that data at any given time. The gateway will manage that subset and cache based on how much local storage the gateway has. If the gateway or your data center has a problem and goes off-line, you can recover that data back from Amazon S3 at any time.
>
> ![Fig. 1 Cached volume](../../../../../img/SAA-CO2/storage-gateway/volume-gateway/planning-and-designing-volume-gateway-deployment/diag01.png)

> ### Stored volume
>
> Using the stored volume, all of your primary data is stored locally and backed up asynchronously to Amazon S3. This ensures that the volume is durably backed up to AWS.
>
> You can take one-time or scheduled snapshots of that volume, which give you point-in-time backups that you can restore from, in the case of failure or data corruption. This gateway mode is a good way to take existing volumes and provide durable off-site backups. You can quickly restore an EBS snapshot to Amazon EC2.
>
> Stored volumes can range from 1 GiB to 16 TiB in size. Each gateway that is configured for stored volumes can support up to 32 volumes, with a total volume storage of 512 TiB (0.5 PiB).
>
> **Use cases**:
>
> * Block storage backups
>
> * Migrations or phased migrations
>
> * Cloud-based disaster recovery
>
> **Example**:
>
> You have an existing dataset that has a large working set. Your application does not have a good, cacheable, smaller working set. Furthermore, there are lots of random reads and writes, and you need full access to the data at low latency at all times.
>
> In this example, Volume Gateway provides the ability to keep that data on premises and have the data asynchronously and constantly backed up to AWS in case of a failure or loss.
>
> ![Fig. 2 Stored volume](../../../../../img/SAA-CO2/storage-gateway/volume-gateway/planning-and-designing-volume-gateway-deployment/diag02.png)

### Deploying the gateway appliance

Based on your on-premises infrastructure needs, the gateway appliance can be deployed either on premises or in the cloud on AWS. 

> ### On-premises
>
> Choose from virtual or physical appliance options to deploy your on-premises gateway appliance:
>
> * Download a **virtual machine (VM) appliance** from the console and deploy it on one of the following:
>
>   * VMware ESXi Hypervisor
>
>     * Integrates w/ VMware vSphere High Availability (VMware HA)
>
>   * Microsoft Hyper-V
>
>   * Linux Kernel-based Virtual Machine (KVM)
>
> * Purchase a **hardware appliance**. A hardware appliance is a physical, standalone appliance with validated server configuration for on-premises deployments. It comes preloaded with Storage Gateway software and provides all the required central processing unit (CPU), memory, network, and solid state drive (SSD) cache resources for creating and configuring Volume Gateway.

> ### On AWS
>
> You can choose to deploy your gateway appliance in the cloud instead of on premises.
>
> * Deploy as an Amazon Machine Image (AMI) in Amazon EC2. Storage Gateway provides an AMI that contains the gateway VM image.
>
> * Cached volume supports this option. Stored volume is only available for on-premises host platform options.
>
> * Deploying a gateway on Amazon EC2 can be useful for learning how to set up and operate a Storage Gateway solution.
>
> **Use cases**:
>
> * Proof of concept
>
> * Disaster recovery
>
> * Data mirroring

### Gateway appliance sizing

As you plan your Volume Gateway deployment, evaluate the following sizing considerations:

1. Determine the number of total volumes and capacity you need to support:

  * Each Volume Gateway, *cached mode*, supports up to 32, 32 TiB cached volumes.

  * Each Volume Gateway, *stored mode*, supports up to 32, 16 TiB stored volumes.

2. Estimate your application and workload volume. The minimum requirement is to allocate at least one local disk for each of the following:

  * Cache storage – a minimum of 150 GiB (cached volume)

  * Upload buffer storage – a minimum of 150 GiB (cached and stored volume)

Best practice for increased performance is to allocate multiple local disks for cache storage with at least 150 GiB each. There is no cache storage allocated for stored volumes.

3. Deploy additional gateway appliances to increase overall throughput (if required).

>[!IMPORTANT]
>
> Underlying physical storage resources are represented as a data store in VMware. When you deploy the gateway VM, you choose a data store on which to store the VM files. When you provision a local disk (for example, to use as cache storage or an upload buffer), you have the option to store the virtual disk in the same data store as the VM or a different data store. If you have more than one data store, we strongly recommend that you choose one data store for the cache storage and another for the upload buffer.

### Connectivity between gateway appliance and service

When Storage Gateway is deployed, it must communicate back to the Storage Gateway service for both management and data movement.

![Fig. 3 Storage Gateway](../../../../../img/SAA-CO2/storage-gateway/volume-gateway/planning-and-designing-volume-gateway-deployment/diag03.png)

The connectivity between the gateway and the service is realized through service endpoints:

* **Public endpoint** ▶︎ Storage Gateway connects to a public endpoint over the internet.

* **Virtual private cloud (VPC) endpoint** ▶︎ Storage Gateway connects to Storage Gateway VPC endpoints over a private connection to AWS [AWS Direct Connect or AWS Virtual Private Network (AWS VPN)].

* **Federal Information Processing Standards (FIPS) 140-2 compliant endpoints** ▶︎ Storage Gateway connects to a public endpoint over the internet. This endpoint complies with FIPS standards, to further protect sensitive information for regulated workloads in AWS GovCloud (US) Regions.

### Network considerations

Volume Gateway requires the following ports for its operation.

<table style="margin-right:calc(0%);width:100%;"><thead><tr><th style="width:14.9912%;"><span style="font-size:17px;">From</span><br></th><th style="width:15.5536%;"><span style="font-size:17px;">To</span><br></th><th style="width:18.3808%;"><span style="font-size:17px;">Protocol</span><br></th><th style="width:12.8666%;"><span style="font-size:17px;">Port</span><br></th><th style="width:38.1137%;"><span style="font-size:17px;">Usage</span><br></th></tr></thead><tbody><tr><td style="width:14.9912%;text-align:center;vertical-align:top;"><span style="font-size:17px;">Storage Gateway VM</span><br></td><td style="width:15.5536%;text-align:center;vertical-align:top;"><span style="font-size:17px;">AWS</span><br></td><td style="width:18.3808%;text-align:center;vertical-align:top;"><span style="font-size:17px;">Transmission Control Protocol (TCP)</span><br></td><td style="width:12.8666%;text-align:center;vertical-align:top;"><span style="font-size:17px;">443 (HTTPS)</span><br></td><td style="width:38.1137%;vertical-align:top;"><span style="font-size:17px;">Used to communicate from a Storage Gateway outbound VM to an AWS service endpoint.</span><br></td></tr><tr><td style="width:14.9912%;text-align:center;vertical-align:top;"><span style="font-size:17px;">Web Browser</span><br></td><td style="width:15.5536%;text-align:center;vertical-align:top;"><span style="font-size:17px;">Storage Gateway VM</span><br></td><td style="width:18.3808%;text-align:center;vertical-align:top;"><span style="font-size:17px;">TCP</span><br></td><td style="width:12.8666%;text-align:center;vertical-align:top;"><span style="font-size:17px;">80<br>(HTTP)</span><br></td><td style="width:38.1137%;vertical-align:top;"><p><span style="font-size:17px;">Used by local systems to obtain the Storage Gateway activation key. Port 80 is used only during activation of a Storage Gateway appliance.</span></p><p><span style="font-size:17px;">A Storage Gateway VM doesn't require port 80 to be publicly accessible.</span></p></td></tr><tr><td style="width:14.9912%;text-align:center;vertical-align:top;"><span style="font-size:17px;">Storage Gateway VM</span><br></td><td style="width:15.5536%;text-align:center;vertical-align:top;"><span style="font-size:17px;">Domain Name System (DNS) server</span><br></td><td style="width:18.3808%;text-align:center;vertical-align:top;"><span style="font-size:17px;">User Datagram Protocol (UDP)</span><br></td><td style="width:12.8666%;text-align:center;vertical-align:top;"><span style="font-size:17px;">53<br>(DNS)</span><br></td><td style="width:38.1137%;vertical-align:top;"><span style="font-size:17px;">For communication between a Storage Gateway VM and the DNS server.</span><br></td></tr><tr><td style="width:14.9912%;text-align:center;vertical-align:top;"><span style="font-size:17px;">Storage Gateway VM</span><br></td><td style="width:15.5536%;text-align:center;vertical-align:top;"><span style="font-size:17px;">AWS</span><br></td><td style="width:18.3808%;text-align:center;vertical-align:top;"><span style="font-size:17px;">TCP</span><br></td><td style="width:12.8666%;text-align:center;vertical-align:top;"><span style="font-size:17px;">22 (Support channel)</span><br></td><td style="width:38.1137%;vertical-align:top;"><span style="font-size:17px;">Allows AWS Support to access your gateway to help you with troubleshooting gateway issues. You don't need this port open for the normal operation of your gateway, but it is required for troubleshooting.</span><br></td></tr><tr><td style="width:14.9912%;text-align:center;vertical-align:top;"><span style="font-size:17px;">Storage Gateway VM</span><br></td><td style="width:15.5536%;text-align:center;vertical-align:top;"><span style="font-size:17px;">Network Time Protocol (NTP) server</span><br></td><td style="width:18.3808%;text-align:center;vertical-align:top;"><span style="font-size:17px;">UDP</span><br></td><td style="width:12.8666%;text-align:center;vertical-align:top;"><span style="font-size:17px;">123<br>(NTP)</span><br></td><td style="width:38.1137%;vertical-align:top;"><p><span style="font-size:17px;">Local systems use this protocol to synchronize VM time to the host time.</span></p></td></tr><tr><td style="width:14.9912%;text-align:center;vertical-align:top;"><span style="font-size:17px;">iSCSI initiators</span><br></td><td style="width:15.5536%;text-align:center;vertical-align:top;"><span style="font-size:17px;">Storage Gateway VM</span><br></td><td style="width:18.3808%;text-align:center;vertical-align:top;"><span style="font-size:17px;">TCP</span><br></td><td style="width:12.8666%;text-align:center;vertical-align:top;"><span style="font-size:17px;">3260 (iSCSI)</span><br></td><td style="width:38.1137%;vertical-align:top;"><span style="font-size:17px;">Used by local systems to connect to iSCSI targets exposed by a gateway.</span><br></td></tr></tbody></table>

## Adding volumes to the gateway appliance

After you create and activate the gateway appliance, on premises or on AWS, you can then create gateway storage *volumes*.

* For a cached volume, use the AWS Management Console to provision storage volumes backed by Amazon S3. You can also provision storage volumes programmatically using the Storage Gateway application programming interface (API) or the AWS Software Development Kit (SDK) libraries. Then mount these storage volumes to your on-premises application servers as iSCSI devices.

* For a stored volume, map them to on-premises direct-attached storage (DAS) or storage area network (SAN) disks. You can start w/ either new disks or disks already holding data. You can then mount these storage volumes to your on-premises application servers as iSCSI devices.

![Fig. 4 Adding volumes to the gateway appliance](../../../../../img/SAA-CO2/storage-gateway/volume-gateway/planning-and-designing-volume-gateway-deployment/diag04.png)

Volume Gateway uses the Internet Small Computer System Interface (iSCSI) protocol to transport block-level data between your on-premises application server and the gateway appliance. Storage Gateway supports authentication between your gateway and iSCSI initiators by using Challenge-Handshake Authentication Protocol (CHAP). Data is transferred over the internet, secured by Secure Sockets Layer (SSL)/Transport Layer Security (TLS), from the gateway appliance to AWS.

## Additional considerations

### Region

Assure your Storage Gateway and the gateway appliance are supported in your desired Regions.

![Fig. 5 Adding volumes to the gateway appliance](../../../../../img/SAA-CO2/storage-gateway/volume-gateway/planning-and-designing-volume-gateway-deployment/diag05.png)

Select an AWS Region before you deploy your gateway. Your gateway will then be activated in that Region. Although Volume Gateway stores your Amazon Elastic Block Store (Amazon EBS) snapshots initially in the AWS Region where you create it, you can choose to back up these snapshots in other AWS Regions using cross-Region copy in AWS Backup.

## Working with snapshots

You can take incremental backups of your storage volumes called *snapshots*. The gateway stores these snapshots in Amazon S3 as Amazon EBS snapshots. When you take a new snapshot, only the data that has changed since your last snapshot is stored. All snapshot storage is also compressed to minimize your storage charges. Initiate snapshots on a schedule or one-time basis. When you delete a snapshot, only the data not needed for any other snapshot is removed. Restore from an Amazon EBS snapshot to an on-premises gateway storage volume if you need to recover a backup of your data. You can also use the snapshot as a starting point for a new Amazon EBS volume, which you can then attach to an Amazon EC2 instance.
