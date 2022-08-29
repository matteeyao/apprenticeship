# Store and Access Data with Tape Gateway

## About Tape Gateway

Tape Gateway eliminates the need to manage physical tape infrastructure. Tape Gateway presents an iSCSI-based VTL comprised of virtual tape drives and a virtual media changer to your on-premises and cloud-based backup applications.

In AWS, the gateway stores your virtual tapes in Amazon Simple Storage Service (Amazon S3) and creates new ones automatically, simplifying management. Amazon S3 offers 11 9s of durability for your tape data. You don’t need to purchase tape libraries, handle magnetic tape media, clean tape cartridges, or deploy resources to manage them.

## Primary use cases

> ### Backup data to the cloud
>
> You can back up data from your on-premises backup application to a virtual tape library (VTL). Take backup data directly from your backup application and move it to **Tape Gateway**. **Tape Gateway** asynchronously uploads your backups to AWS so you can scale backup storage in the cloud with pay-as-you-go pricing.
>
> **Features**:
>
> * Quickly restore from local cache and tapes stored durably in Amazon S3.
>
> * iSCSI VTL interface is compatible with leading backup applications.
>
> * Data is encrypted and compressed in transit and at rest.
>
> * Fully managed virtual tape storage in Amazon S3.
>
> * Includes write once, read many (WORM) and tape retention lock.
>
> **Benefits**:
>
> * Replace physical tape libraries.
>
> * Maintain existing backup workflows.
>
> * Lower total storage costs of long-term retention data.
>
> * Use as a drop-in replacement for tape libraries, tape media, and archiving services.

> ### Disaster recovery and compliance
>
> Use Tape Gateway to archive data that needs to be stored at an offsite location for disaster recovery or regulatory compliance needs. You can replace magnetic tape libraries and physical tapes with AWS Cloud storage for long-term retention storage needs.
>
> **Features**:
>
> * Available tapes are stored in Amazon S3.
>
> * Archived tapes are stored in S3 Glacier Flexible Retrieval or S3 Glacier Deep Archive.
>
> * Data is encrypted and compressed in transit and at rest.
>
> * Includes WORM and tape retention lock.
>
> **Benefits**:
>
> * Protect backup archive from internal and external threats.
>
> * Optimizes long-term storage costs.
>
> * Simplifies lifecycle management of virtual tapes.

## Choosing Tape Gateway for your workloads

Review several key questions to help determine if Tape Gateway is right for your workloads.

1. Are you happy with your current backup application but want to get away from the management and cost associated with physical tapes?

2. Are you concerned about the integrity and quality of your existing tapes?

3. Do you need help to protect your data from malicious or accidental data deletion and comply with industry regulations for compliance purposes?

## How Tape Gateway works

**Tape Gateway** is deployed into your on-premises environment as a VM, a physical gateway hardware appliance, an offline device using AWS Snowball Edge, or in the AWS Cloud using an Amazon EC2 instance.

Once deployed and activated, **Tape Gateway** works with your existing backup application as a VTL, which consists of a virtual media changer and virtual tape drives.

**Tape Gateway** supports all leading backup applications and caches your virtual tapes on premises for low-latency data access. Your backup application communicates with the VTL devices on the gateway appliance using the iSCSI protocol.

![Fig. 1 Tape Gateway](../../../../../../img/SAA-CO2/storage-gateway/tape-gateway/hybrid-cloud-solutions/store-and-access-data-with-tape-gateway/diag01.png)

The cloud storage solution includes an on-premises gateway appliance; in this case, it's Tape Gateway, which interacts with the Storage Gateway service. It provides durable storage for your tape backup data in Amazon S3.

To prepare data for upload to Amazon S3, your gateway also stores incoming data in a staging area, referred to as an *upload buffer*. Your gateway uploads data from the upload buffer over an encrypted Secure Sockets Layer (SSL) connection to the Storage Gateway service running in the AWS Cloud. Storage Gateway then stores the tape data encrypted in Amazon S3.

Each virtual tape is stored in Amazon S3. When you no longer require access to data on virtual tapes, you can archive them from the VTL into an archive tape pool for long-term storage. Storage Gateway provides two tape pools: S3 Glacier Flexible Retrieval and S3 Glacier Deep Archive.

## Configure gateway local disks

The gateway requires additional local disk storage.

> ### Cache Storage
>
> The cache store acts as the on-premises durable store for data that is pending upload to Amazon S3 from the upload buffer and is required for a Tape Gateway.
>
> Cache storage provides low-latency access to your data. As your application requests data, the gateway first checks the cache storage for the data before downloading the data from AWS.

> ### Upload Buffer
>
> The upload buffer provides a staging area for the data before the gateway uploads the data to Amazon S3. Your gateway uploads this buffer data over an encrypted SSL connection to AWS.
>
> The upload buffer is also critical for creating recovery points that you can use to recover tapes from unexpected failures.

## Tape Gateway components

> ### Virtual tapes
>
> Think of a virtual tape just like you would a physical tape cartridge. However, virtual tape data is stored in AWS. Like physical tapes, virtual tapes can be blank or can have data written on them.
>
> You create virtual tapes either by using the Storage Gateway console or programmatically by using the Storage Gateway application programming interface (API).
>
> * Each gateway can contain up to 1,500 tapes or up to a maximum of 1 pebibyte (PiB) of total tape data at a time.
>
> * Each tape can store between 100 gibibytes (GiB) and 5 tebibytes (TiB) in size.
>
> * You only pay for the amount of data stored on each tape, and not for the size of the tape.
>
> The virtual tapes that you create appear in your gateway's VTL.

> ### Virtual tape library
>
> A VTL is like a physical tape library available on premises with robotic arms and tape drives. Your VTL includes the collection of virtual tapes. Each Tape Gateway comes with one VTL.
>
> Tapes in the VTL are stored by Amazon S3 Standard. Data in Amazon S3 is managed by the Storage Gateway service and you manage, manually or automatically, the tapes to add, retrieve, or archive.
>
> A VTL consists of the following two key components:
>
> *Tape drive* – A VTL tape drive is comparable to a physical tape drive that can perform I/O and seek operations on a tape. Each VTL comes with a set of 1 to 10 tape drives, which are available to your backup application as iSCSI devices.
>
> *Media changer* – A VTL media changer is comparable to a robot that moves tapes around in a physical tape library's storage slots and tape drives. Each VTL comes with one media changer, which is available to your backup application as an iSCSI device.

> ### Virtual tape shelf
>
> Archiving a virtual tape is comparable to having an offsite tape-holding facility. Tapes that do not need instant retrieval from Amazon S3 can be exported from VTL and moved to a virtual tape shelf (VTS) for low-cost storage for data archiving, backup, and long-term data retention.
>
> The VTS is backed by S3 Glacier Flexible Retrieval or S3 Glacier Deep Archive. When you need them, you can retrieve tapes from the archive back to your gateway's VTL.
>
> Key tasks that are performed from the VTS are the following:
>
> * *Archiving tapes* – Your backup software ejects a tape and the gateway moves the tape to the VTS archive for long-term storage. The archive is located in the AWS Region in which you activated the gateway.
>
> * *Retrieving tapes* – To read an archived tape, you must first retrieve it to your Tape Gateway from the VTS by using either the Storage Gateway console or the Storage Gateway API.

> ### Tape pool
>
> A tape pool defines the archive storage class that you want tapes to be archived into when they are ejected to the VTS.
>
> Storage Gateway provides two standard tape pools:
>
> * *Glacier pool* – When your backup software ejects the tape, it is automatically archived in S3 Glacier Flexible Retrieval. You use S3 Glacier Flexible Retrieval for more active archives where you can retrieve the tapes typically within 3 – 5 hours.
>
> * *Deep Archive pool* – When your backup software ejects the tape, the tape is automatically archived in S3 Glacier Deep Archive. You use S3 Glacier Deep Archive for long-term data retention and digital preservation where data is accessed once or twice a year. You can retrieve tapes archived in S3 Glacier Deep Archive typically within 12 hours.
>
> Tapes archived in S3 Glacier Flexible Retrieval can be moved to S3 Glacier Deep Archive at a later time for additional storage cost savings.
>
> You can also create a custom tape pool if you would like to apply a *retention lock* to your archived tape. Compliance or governance retention locks can be applied to your custom tape pool to prevent archived tapes from being deleted or moved to another pool for a fixed amount of time, up to 100 years. This includes locking permission controls on who can delete tapes or modify retention settings.
