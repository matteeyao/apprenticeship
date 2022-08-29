# Reads, Writes, and Updates

> Tape data is durably stored in Amazon Simple Storage Service (Amazon S3) and is part of the AWS Storage Gateway service.

## Tape Gateway data transfers

Tape Gateway routes your tape backup data through the on-premises or cloud-based compute Tape Gateway appliance to and from your backup application. The virtual tape library (VTL) media changer and tape drives are available to your existing backup application as iSCSI devices.

All data transferred between the gateway and Amazon Web Services (AWS) storage is encrypted using Secure Sockets Layer (SSL). Data transfers are done through HTTPS. Objects are encrypted with Amazon S3 server-side encryption (S3-SSE) keys or optionally with AWS Key Management Service (AWS KMS) managed keys using SSE-KMS.

Your data is compressed and also encrypted before it is transmitted. Therefore, it is encrypted in transit and at rest.

### Tape Gateway: Reads and writes

Your VTL includes the collection of stored virtual tapes. An existing backup application writes data to these virtual tapes. The media changer then loads and unloads the virtual tapes into the virtual tape drives for read and write operations.

As your backup application writes data to the Tape Gateway, the gateway stores data locally and then asynchronously uploads it to virtual tapes in your VTL stored durably in Amazon S3.

The gateway copies data to both the local cache storage and the upload buffer. It then acknowledges completion of the write operation to your backup application.

* If your application reads data from a virtual tape, the gateway saves the data to the local cache storage. The gateway stores recently accessed data in the cache storage for low-latency access. 

* If your application requests tape data, the gateway first checks the cache storage for the data before downloading the data from AWS.

* The upload buffer provides a staging area for the gateway before it uploads the data to a virtual tape. The upload buffer is also critical for creating recovery points that you can use to recover tapes from unexpected failures.

![Fig. 1 Tape Gateway architecture](../../../../../../img/SAA-CO2/storage-gateway/tape-gateway/working-with-tape-gateway/read-writes-and-updates/diag01.png)

Tape Gateway uses a local cache to store the data locally. Data is then compressed and encrypted as it is moved to the upload buffer. From there, it is transferred over the wire to AWS and managed by the AWS Storage Gateway service.

## Accessing tape data

Virtual tape data is not accessed directly using Amazon S3 or Amazon S3 Glacier application programming interfaces (APIs). However, you can use the Tape Gateway APIs to manage your VTL and your virtual tape shelf (VTS).

The virtual tape containing your data must be stored in a VTL before it can be accessed. Access to virtual tapes in your VTL is instant. If the virtual tape containing your data is archived, you can retrieve the virtual tape using the AWS Management Console or API. After the virtual tape is available in the VTL, you can use your backup application to make use of the virtual tape to restore data.

## Working with iSCSI initiators and targets

iSCSI protocol is an IP-based storage networking standard for linking data storage facilities. It provides block-level access to storage devices by carrying Small Computer System Interface (SCSI) commands over a Transmission Control Protocol/Internet Protocol (TCP/IP) network using the default port 3260. iSCSI is used to facilitate data transfers over intranets and to manage storage over long distances. The protocol allows applications (called *initiators*) to send SCSI commands to storage devices (called *targets*) on remote servers.

For Tape Gateway, your existing backup application is the iSCSI initiator and the iSCSI targets are virtual tape drives. As part of managing tapes, you do such tasks as connecting to those targets and customizing iSCSI settings.

> ### iSCSI initiator
>
> The initiator is the client component of an iSCSI network. The initiator sends requests to the iSCSI target. Initiators can be implemented in software or hardware. Storage Gateway only supports software initiators.

> ### iSCSI target
>
> The target is the server component of the iSCSI network that receives and responds to requests from initiators. Each of your tape drives is exposed as an iSCSI target. Connect only one iSCSI initiator to each iSCSI target.

## Connect your tape drives to the iSCSI initiator

> ### Gateways
>
> From the Storage Gateway console, select **Gateways**. Choose the desired Tape Gateway to connect to your backup application using iSCSI.

> ### VTL devices
>
> Open the Tape Gateway to locate the iSCSI target information.

> ### iSCSI connector
>
> Locate the **Target name** column to identify the target for a specific tape drive.

>[!IMPORTANT]
>
> To connect to your tape drive target, your gateway must have an upload buffer configured. If an upload buffer is not configured for your gateway, then the status is displayed as UPLOAD BUFFER NOT CONFIGURED.

In this module, you have examined the data workflow for Tape Gateway, including how the iSCSI protocol is used to connect tape drives to your on-premises or AWS Cloud applications. Next, you review how to work with and manage tapes in AWS.
