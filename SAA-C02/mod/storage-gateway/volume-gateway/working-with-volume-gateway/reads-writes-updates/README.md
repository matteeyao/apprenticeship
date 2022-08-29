# Reads, Writes, and Updates

> Volume data is durably stored in Amazon Simple Storage Service (Amazon S3) and is part of the AWS Storage Gateway service.

## Volume Gateway data transfers

Volume Gateway routes your block storage data through the on-premises Volume Gateway appliance to and from either storage area network (SAN) or virtual volumes. Your volume mode determines how reads and writes occur.

All data transferred between the gateway and Amazon Web Services (AWS) storage is encrypted using Secure Sockets Layer (SSL). Data transfers are done through HTTPS. Objects are encrypted with Amazon S3 server-side encryption (S3-SSE) keys or optionally with AWS Key Management Service (AWS KMS) managed keys using SSE-KMS.

Your data is compressed and also encrypted before it is transmitted. Therefore, it is encrypted in transit and at rest.

### Volume reads

> ### Cached volume - reads
>
> Think of cached volume as a *virtual volume* or **thin-provisioned volume**. With cached volume, you have some amount of cache in your Storage Gateway virtual machine (VM) on premises. 
>
> For read requests, if the data is in the cache volume, it is served locally from the cache and there is no latency. Everything happens directly inside your data center.
>
> If the data is not in the cache, the Storage Gateway service retrieves the compressed data from Amazon S3 and sends it to the gateway appliance. The gateway appliance receives the data, decompresses it, stores in the local cache, and provides it to the application.
>
> This is known as a *read-through* cache, where the application always requests data from the cache. If the cache does not have the requested data, it must retrieve the data from the backend data store. After the data is retrieved, the cache stores it and returns the data to the calling application.
>
> ![Fig. 1 Cached volume - reads](../../../../../../img/SAA-CO2/storage-gateway/volume-gateway/working-with-volume-gateway/reads-writes-updates/diag01.png)

> ### Stored volume - reads
>
> One hundred percent of the data on your volumes is stored locally. Therefore, any reads to this data come directly from the virtual appliance or SAN from that local disk.
>
> This volume type is beneficial for workloads that are extremely sensitive to latency and need immediate access to all of that data.
>
> ![Fig. 2 Stored volume - reads](../../../../../../img/SAA-CO2/storage-gateway/volume-gateway/working-with-volume-gateway/reads-writes-updates/diag02.png)

### Volume writes

Data writes for the volume are also dependent upon the volume type. Both require an upload buffer, where the data is then transferred to AWS. 

> ### Cached volume - writes
>
> Data written gets stored in the local volume cache in its native format. Volume Gateway then compresses and encrypts the data as it moves the data from the cache into the upload buffer.
>
> The upload buffer is provided so that performance is not impacted when reading and writing to the cache. As a write happens, the data goes into the cache and it is *write-back*. That means you get the local acknowledgement quickly, just as you would if you were writing to any enterprise storage array. From the upload buffer, the data is then transferred to AWS.
>
> A write-back cache is one in which write operations are directed to cache and completion is immediately acknowledged to the application. The result is low latency and high throughput for applications that perform a lot of write operations.

> ### Stored volume - writes
>
> Stored volume data writes happen directly to that disk where they are immediately acknowledged locally.
>
> When you create a snapshot of one of your volumes, the data will then be moved to AWS. Data is compressed and encrypted as it is moved to the upload buffer. From there, data is securely transferred to Amazon S3 in AWS.

![Fig. 3 Cached or Stored volume writes](../../../../../../img/SAA-CO2/storage-gateway/volume-gateway/working-with-volume-gateway/reads-writes-updates/diag03.png)

Volume Gateway uses a local cache store to store the data locally in its native format. Data is then compressed and encrypted as it is moved to the upload buffer. From there, it is transferred over the wire to AWS and managed by the Storage Gateway service.

### Working with iSCSI initiators

The iSCSI protocol is an IP-based storage networking standard for linking data storage facilities. It provides block-level access to storage devices by carrying Small Computer System Interface (SCSI) commands over a Transmission Control Protocol/Internet Protocol (TCP/IP) network using the default port 3260. iSCSI is used to facilitate data transfers over intranets and to manage storage over long distances. With this protocol, applications (called *initiators*) can send SCSI commands to storage devices (called *targets*) on remote servers.

For Volume Gateway, the iSCSI targets are volumes. As part of managing volumes, you do such tasks as connecting to those targets, customizing iSCSI settings, and configuring Challenge-Handshake Authentication Protocol (CHAP).

To learn about the client and server components, expand each of the following two iSCSI terms.

> ### iSCSI initiator
>
> The initiator is the client component of an iSCSI network. The initiator sends requests to the iSCSI target. Initiators can be implemented in software or hardware. Storage Gateway only supports software initiators.

> ### iSCSI target
>
> The target is the server component of the iSCSI network that receives and responds to requests from initiators. Each of your volumes is exposed as an iSCSI target. Connect only one iSCSI initiator to each iSCSI target.

Choose the following numbered markers for instructions to connect your volume to the iSCSI initiator.

> ### 1. Volumes
>
> From the Storage Gateway console, select **Volumes**. Choose the desired volume to connect to your on-premises application using iSCSI.

> ### 2. iSCSI connector
>
> Choose to connect your volume to either a Linux or Microsoft Windows iSCSI initiator.

>[!IMPORTANT]
>
> To connect to your volume target, your gateway must have an upload buffer configured. If an upload buffer is not configured for your gateway, then the status of your volumes is displayed as UPLOAD BUFFER NOT CONFIGURED.
