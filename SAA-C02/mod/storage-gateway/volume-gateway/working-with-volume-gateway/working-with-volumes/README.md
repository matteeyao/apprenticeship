# Working with Volumes

> Each Volume Gateway can support up to 32 volumes. After the Volume Gateway is activated and running, you add volumes.

## Adding a volume

Once your Volume Gateway is running, you can add volumes to associate with that gateway. Cached volumes and snapshots are stored in an Amazon S3 bucket maintained by Storage Gateway. Your volumes are accessible for I/O operations through Storage Gateway. You cannot directly access them using the Amazon S3 console or API actions.

Choose each of the six numbered markers to learn the steps for creating a new volume.

> ### 1. Choose a gateway
>
> Associate the volume w/ the desired gateway. Volume Gateway will need to be running.

> ### 2. Configure capacity
>
> Cached volumes support up to 32 tebibytes (TiB) of data.
>
> Stored volumes support up to 15 TiB of data.

> ### 3. Configure volume contents
>
> Select the starting point for the new volume:
>
> * Create a new empty volume.
>
> * Create based on an existing EBS snapshot.
>
> * Clone from last volume recovery point.

> ### 4. Enter an iSCSI target name
>
> Enter a name for the iSCSI target.

> ### 5. Add tags
>
> Optionally enter tags to categorize and label your volume.

## Volume recovery point

Storage Gateway provides recovery points for each volume in a cached volume gateway architecture. A volume recovery point is a point in time in which all data of the volume is consistent and from which you can create a snapshot or clone a volume. To clone a cached volume, choose **Clone from last volume recovery point** in the **Configure volume** dialog, and then select the volume to use as the source. A sample is provided on the Volume Clone tab in the next topic.

## Recovery options

Volume Gateway offers multiple recovery options for your volumes. 

To review differences between volume clones and EBS snapshots, choose each of the following two tabs.

> ### Volume Clone
>
> Instant clones offer rapid recovery of data to a gateway on premises. Cloning from an existing volume is faster and more cost effective than creating an Amazon EBS snapshot. Cloning does a byte-to-byte copy of your data from the source volume to the new volume, using the most recent recovery point from the source volume.
>
> You can create a new volume from any existing cached volume in the same AWS Region. The new volume is created from the most recent recovery point of the selected volume.

> ### EBS Snapshot
>
> Snapshots represent a point-in-time copy of the volume at the time the snapshot is requested. Data written to the volume by your application prior to taking the snapshot, but not yet uploaded to AWS, will be included in the snapshot.
>
> Snapshots are assigned an ID and are visible in the AWS Management Console and AWS Command Line Interface (AWS CLI) immediately, but initially are in a *Pending status*. When all data written to the volume prior to the snapshot request has been uploaded from the gateway into Amazon Elastic Block Store (Amazon EBS), the status will change to *Available*. At this point, you can use the snapshot as the base for a volume.
>
> * **Cached volume** ▶︎ When taking a new snapshot, only the data that has changed since your last snapshot is stored. If you have a volume with 100 GB of data, but only 5 GB of data have changed since your last snapshot, only the 5 additional GB of snapshot data will be stored in Amazon S3. When you delete a snapshot, only the data not needed for any other snapshot is removed.
>
> * **Stored volume** ▶︎ Data written to the volume by your application prior to taking the snapshot, but not yet uploaded to AWS, will be included in the snapshot.
>
> Enter the Snapshot ID of the  EBS volume for the starting point of your new volume.

Using the AWS Management Console, you can create a new volume from a snapshot you’ve stored in Amazon S3. You can then mount this volume as an iSCSI device to your on-premises or AWS Cloud based application server.

## Expanding the size of a volume

As your application needs grow, you might want to expand your volume size instead of adding more volumes to your gateway. Automatic resizing of a volume is not supported with Volume Gateway. In this case, you can choose from the following two options:

* Create a snapshot of the volume you want to expand and then use the snapshot to create a new volume of a larger size.

* Use the cached volume you want to expand to clone a new volume of a larger size.
