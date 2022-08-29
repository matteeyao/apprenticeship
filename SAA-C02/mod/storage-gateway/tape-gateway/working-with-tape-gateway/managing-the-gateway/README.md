# Managing the Gateway

Managing your gateway includes tasks such as adding or deleting a tape, monitoring gateway and tape status and transitions, and performing general maintenance.

## Confirming tape status

Details identifying the operational integrity of your tape are provided, such as tape status and attachment status, to assist you in managing your tapes.

### Tape Status in a VTL

Each tape has an associated status that reflects its health. A tape's status must be AVAILABLE for you to read or write to the tape.

>[!NOTE]
>
> Screen readers should enter table mode to read the following table.

<table><thead><tr><th style="width:26.9176%;"><span style="font-size:17px;">Status</span><br></th><th style="width:72.9395%;"><span style="font-size:17px;">Meaning</span><br></th></tr></thead><tbody><tr><td style="width:26.9176%;"><span style="font-size:17px;">ARCHIVING</span><br></td><td style="width:72.9395%;"><span style="font-size:17px;">The virtual tape is being moved by your Tape Gateway to the archive, which is backed by S3 Glacier Flexible Retrieval or S3 Glacier Deep Archive.</span><br></td></tr><tr><td style="width:26.9176%;"><span style="font-size:17px;">AVAILABLE</span><br></td><td style="width:72.9395%;"><span style="font-size:17px;">The virtual tape is created and ready to be loaded into a tape drive.</span><br></td></tr><tr><td style="width:26.9176%;"><span style="font-size:17px;">CREATING</span><br></td><td style="width:72.9395%;"><span style="font-size:17px;">The virtual tape is being created. The tape can't be loaded into a tape drive, because the tape is being created.</span><br></td></tr><tr><td style="width:26.9176%;"><span style="font-size:17px;">DELETED</span><br></td><td style="width:72.9395%;"><span style="font-size:17px;">The virtual tape has been successfully deleted.</span><br></td></tr><tr><td style="width:26.9176%;"><span style="font-size:17px;">DELETING</span><br></td><td style="width:72.9395%;"><span style="font-size:17px;">The virtual tape is being deleted. The DELETING status is transitional.</span><br></td></tr><tr><td style="width:26.9176%;"><span style="font-size:17px;">IN TRANSIT TO VTS</span><br></td><td style="width:72.9395%;"><span style="font-size:17px;">The virtual tape has been ejected and is being uploaded for archive. At this point, your Tape Gateway is uploading data to AWS. If the amount of data being uploaded is small, this status might not appear. When the upload is completed, the status changes to ARCHIVING.</span><br></td></tr><tr><td style="width:26.9176%;"><span style="font-size:17px;">IRRECOVERABLE</span><br></td><td style="width:72.9395%;"><span style="font-size:17px;">The virtual tape can't be read from or written to. This status indicates an error in your Tape Gateway.</span><br></td></tr><tr><td style="width:26.9176%;"><span style="font-size:17px;">RECOVERED</span><br></td><td style="width:72.9395%;"><p><span style="font-size:17px;">The virtual tape is recovered and is read-only.&nbsp;</span></p><span style="font-size:17px;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</span><p><span style="font-size:17px;">When your Tape Gateway is not accessible for any reason, you can recover virtual tapes associated with that Tape Gateway to another Tape Gateway. To recover the virtual tapes, first disable the inaccessible Tape Gateway.</span></p><br></td></tr><tr><td style="width:26.9176%;"><span style="font-size:17px;">RETRIEVING</span><br></td><td style="width:72.9395%;"><span style="font-size:17px;">The virtual tape is being retrieved from the archive to your Tape Gateway.</span><br></td></tr><tr><td style="width:26.9176%;"><span style="font-size:17px;">RETRIEVED</span><br></td><td style="width:72.9395%;"><span style="font-size:17px;">The virtual tape is retrieved from the archive. The retrieved tape is write-protected.</span><br></td></tr></tbody></table>

### Tape Status in an Archive

Each archived tape has an associated status which reflects its health.

>[!NOTE]
>
> Screen readers should enter table mode to read the following table.

<table><thead><tr><th style="width:23.7732%;"><span style="font-size:17px;">Status</span><br></th><th style="width:76.0837%;"><span style="font-size:17px;">Meaning</span><br></th></tr></thead><tbody><tr><td style="width:23.7732%;"><span style="font-size:17px;">ARCHIVED</span><br></td><td style="width:76.0837%;"><span style="font-size:17px;">The virtual tape has been ejected and is uploaded to the archive.</span><br></td></tr><tr><td style="width:23.7732%;"><span style="font-size:17px;">RETRIEVING</span><br></td><td style="width:76.0837%;"><span style="font-size:17px;">The virtual tape is being retrieved from the archive.</span><br></td></tr><tr><td style="width:23.7732%;"><span style="font-size:17px;">RETRIEVED</span><br></td><td style="width:76.0837%;"><span style="font-size:17px;">The virtual tape has been retrieved from the archive. The retrieved tape is read-only.</span></td></tr></tbody></table>

## Locate the tape status and details from the Storage Gateway console

> ### Barcode
>
> Each tape is assigned a unique barcode w/ an optional prefix.
>
> Use the barcode to search to quickly locate a specific tape.

> ### Tape status
>
> The tape is available for us. A tape's status must be **AVAILABLE** for you to read or write to the tape.

> ### Capacity used
>
> Quickly locate the capacity used for a tape w/ the Storage Gateway console.

> ### Size
>
> Size reflects the space allocated for the tape.

> ### Archived status
>
> The status will reflect when tapes are archived. There are three archive statuses:
>
> * Archived
>
> * Retrieving
>
> * Retrieved

> ### Pool
>
> Choose from S3 Glacier Flexible Retrieval, S3 Deep Archive, or create a custom tape pool.
>
> Custom tape pools provide additional features such as tape retention lock.

> ### Gateway
>
> The name of the gateway the tape is attached to.

## Searching for tapes

Options for searching across your VTL and VTS:

> ### Search filter
>
> Search across available and archived tapes using the following filters:
>
> * Barcode
>
> * Status
>
> * Created
>
> * Archived
>
> * Gateway ID
>
> * Pool ID

> ### Search criteria
>
> Enter search details based on the selected filter.

## Tape actions

After creating a tape, you can perform additional actions such as delete a tape, assign a tape to a tape pool, and retrieve a tape from the VTS (archive).

From the Storage Gateway console, from the Storage Gateway navigation menu, under **Tape Library**, choose **Tapes**. Select the desired **Barcode** to view expanded details. Locate and choose the **Actions** menu to perform a tape action.

## Managing bandwidth for the Tape Gateway

By default, an activated gateway has no rate limits on upload or download. You can limit (or throttle) the upload throughput from the gateway to AWS or the download throughput from AWS to your gateway.

### Bandwidth rate limit

Using bandwidth rate limit helps you to control the amount of network bandwidth used by your gateway.

In the gateway **Actions** menu, choose **Edit bandwidth rate limit** and then choose **With limit**. The minimum rate for download is 100 Kilobits (Kib) per second and the minimum rate for upload is 50 Kib per second.
