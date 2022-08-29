# Managing the Gateway

Managing your gateway includes tasks such as monitoring volume status and transitions, cloning or  deleting a volume, creating snapshots, editing a snapshot schedule, and performing general maintenance.

## Confirming volume status

Details identifying the operational integrity of your volume are provided, such as volume status and attachment status, to assist you in managing your volumes.

> ### Volume status
>
> Each volume has an associated status that reflects its health. A volume that is functioning normally should have the *AVAILABLE* status all or most of the time it is in use.
>
> <table><thead><tr><th style="width:28.2633%;"><span style="font-size:17px;">Status</span><br></th><th style="width:71.632%;"><span style="font-size:17px;">Meaning</span><br></th></tr></thead><tbody><tr><td style="width:28.2633%;"><span style="font-size:17px;">AVAILABLE</span><br></td><td style="width:71.632%;"><span style="font-size:17px;">The volume is available for use. The <em>Available&nbsp;</em>status is the normal running status for a volume.</span><br></td></tr><tr><td style="width:28.2633%;"><span style="font-size:17px;">BOOTSTRAPPING</span><br></td><td style="width:71.632%;"><span style="font-size:17px;">The gateway is synchronizing data locally with a copy of the data stored in AWS. You typically don't need to take action for this status, because the storage volume automatically sees the <em>Available</em> status in most cases.</span><br></td></tr><tr><td style="width:28.2633%;"><span style="font-size:17px;">CREATING</span><br></td><td style="width:71.632%;"><span style="font-size:17px;">The volume is currently being created and is not ready for use. The <em>Creating</em> status is transitional. No action is required.</span><br></td></tr><tr><td style="width:28.2633%;"><span style="font-size:17px;">DELETING</span><br></td><td style="width:71.632%;"><span style="font-size:17px;">The volume is currently being deleted. The <em>Deleting&nbsp;</em>status is transitional. No action is required.</span><br></td></tr><tr><td style="width:28.2633%;"><span style="font-size:17px;">IRRECOVERABLE</span><br></td><td style="width:71.632%;"><span style="font-size:17px;">An error occurred from which the volume cannot recover. For information on what to do in this situation, see </span><a href="https://docs.aws.amazon.com/storagegateway/latest/vgw/troubleshoot-volume-issues.html" rel="noopener noreferrer" target="_blank"><span style="font-size:17px;">Troubleshooting volume issues</span></a><span style="font-size:17px;">.</span><br></td></tr><tr><td style="width:28.2633%;"><span style="font-size:17px;">PASS THROUGH</span><br></td><td style="width:71.632%;"><span style="font-size:17px;">Local data is out of sync with data stored in AWS. Data written to a volume while the volume is in <em>Pass Through</em> status remains in the cache until the volume status is <em>Bootstrapping</em>. This data starts to upload to AWS when <em><em>Bootstrapping</em>&nbsp;</em>status begins.</span><br></td></tr><tr><td style="width:28.2633%;"><span style="font-size:17px;">RESTORING</span><br></td><td style="width:71.632%;"><span style="font-size:17px;">The volume is being restored from an existing snapshot. This status applies only for stored volumes.</span><br></td></tr><tr><td style="width:28.2633%;"><span style="font-size:17px;">RESTORING PASS THROUGH</span><br></td><td style="width:71.632%;"><span style="font-size:17px;">The volume is being restored from an existing snapshot and has encountered an upload buffer issue. This status applies only for stored volumes.</span><br></td></tr><tr><td style="width:28.2633%;"><span style="font-size:17px;">UPLOAD BUFFER NOT CONFIGURED</span><br></td><td style="width:71.632%;"><span style="font-size:17px;">The volume cannot be created or used. No upload buffer is configured.</span><br></td></tr></tbody></table>

> ### Attachment status
>
> You can detach a volume from a gateway or attach it to a gateway by using the Storage Gateway console or application programming interface (API).
>
> <table><thead><tr><th style="width:23.4076%;"><span style="font-size:17px;">Status</span><br></th><th style="width:76.4877%;"><span style="font-size:17px;">Meaning</span><br></th></tr></thead><tbody><tr><td style="width:23.4076%;"><span style="font-size:17px;">ATTACHED</span><br></td><td style="width:76.4877%;"><span style="font-size:17px;">The volume is attached to a gateway.</span><br></td></tr><tr><td style="width:23.4076%;"><span style="font-size:17px;">DETACHED</span><br></td><td style="width:76.4877%;">&nbsp;<span style="font-size:17px;">The volume is detached from a gateway.</span><br></td></tr><tr><td style="width:23.4076%;"><span style="font-size:17px;">DETACHING</span><br></td><td style="width:76.4877%;"><span style="font-size:17px;">The volume is being detached from a gateway. When you are detaching a volume and the volume doesn't have data on it, you might not see this status.</span></td></tr></tbody></table>

To learn where to locate the volume status from the Storage Gateway console, choose each of the following numbered markers.

> ### 1. Volume status
>
> The volume is available for use. This status is the normal running status for a volume.

> ### 2. Attachment status
>
> You can detach a volume from a gateway or attach it to a gateway by using the Storage Gateway console or API.
>
> In this example, both volumes are attached to a gateway. The name of the gateway is displayed in the far right column.

> ### 3. EBS snapshots
>
> This displays the number of snapshots created for this volume.

> ### 4. Gateway
>
> This lists the name of the gateway this volume is attached to.

## Volume actions

After creating a volume, you can perform additional actions such as creating an on-demand backup plan with AWS Backup, create an EBS snapshot, edit the snapshot schedule, configure CHAP authentication, delete a volume, connect the volume to the client (iSCSI initiator), and add optional tags.

From the Storage Gateway console, choose **Volumes** from the Storage Gateway navigation menu. Select the desired **Volume ID** to view expanded details. Locate and choose the **Actions** menu to perform a volume action.

## Working with snapshots

Snapshots represent a point-in-time copy of the volume at the time the snapshot is requested. They contain all the information needed to restore your data (from the time the snapshot was taken) to a new volume.

### Creating a one-time snapshot

You can take a one-time snapshot of your volume at any time. By doing this, you can back up your storage volume immediately without waiting for the next scheduled snapshot. 

From the Volumes **Actions** menu, choose **Create EBS** snapshot. The **Create Snapshot** dialog displays. Enter a snapshot description, optional tags, and then choose **Save changes**.

## Editing a snapshot schedule

* By default, stored volumes are assigned a snapshot schedule of once a day. This schedule can be edited by specifying either the time the snapshot occurs each day or the frequency (every 1, 2, 4, 8, 12, or 24 hours), or both. 

* Storage Gateway doesn't create a default snapshot schedule for cached volumes. A default schedule is not needed because your data is durably stored in Amazon S3. You can create a snapshot schedule for a cached volume at any time if you want.

>[!IMPORTANT]
>
> You can't remove the default snapshot schedule for stored volumes, as they require at least one snapshot schedule.

>[!IMPORTANT]
>
> There are no limits to the number of snapshots or the amount of snapshot data a single gateway can produce.

## Managing bandwidth for the gateway

By default, an activated gateway has no rate limits on upload or download. You can limit (or throttle) the upload throughput from the gateway to AWS or the download throughput from AWS to your gateway.

### Bandwidth rate limit

Using bandwidth rate limit helps you to control the amount of network bandwidth used by your gateway.

In the gateway **Actions** menu, choose **Edit bandwidth rate limit** and then choose **With limit**. The minimum rate for download is 100 Kilobits (Kib) per second and the minimum rate for upload is 50 Kib per second.

You can also assign bandwidth rate limits on a schedule using the **Edit bandwidth rate limit schedule** action.
