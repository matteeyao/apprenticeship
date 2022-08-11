# Working with Tapes

> Each Tape Gateway can support up to 1,500 available tapes and virtually unlimited archived tapes. After the Tape Gateway is activated and running, you add tapes.

## Adding a tape

Once your Tape Gateway is running, you can add tapes to associate with that gateway. Virtual tapes are stored in an Amazon S3 bucket maintained by the Storage Gateway service. Your tapes are accessible for I/O operations through Storage Gateway. You cannot directly access them using Amazon S3 console or API actions.

Tape Gateway supports write once, read many (WORM) on virtual tapes to help ensure that the data on active tapes can't be overwritten or erased. Enabling WORM protection for virtual tapes helps protect those tapes while they are in active use, before they are ejected and archived. Additional tape security is reviewed in more detail in the Protecting your data module later in this course.

## Steps for creating a new tape

> ### Choose a gateway
>
> Associate the tape with the desired gateway. The Tape Gateway will need to be running.

> ### Tape type
>
> You can prevent virtual tapes from being overwritten or erased by enabling Write Once, Read Many (WORM) tape protection.
>
> WORM configuration can only be set when tapes are created, and that configuration can't be changed after the tapes are created.
>
> Choose either **Standard** or write once, read many (**WORM**) as the tape type.

> ### Number of tapes
>
> Select the desired number of tapes between 1 and 10.

> ### Configure capacity
>
> Specify the tape capacity between a minimum of 100 GiB and maximum of 5 TiB each.

> ### Barcode prefix
>
> Virtual tapes are uniquely identified by a bardcode, and you can add a prefix to the barcode.
>
> Prefixes assist in searching and identifying tapes.
>
> The prefix must be uppercase letters (A-Z) and must be one to four characters long.

> ### Assign an archive pool
>
> Select either the **Glacier Pool** (for S3 Glacier Flexible Retrieval) or **Deep Archive Pool** (for S3 Glacier Deep Archive) as the storage class the tape will be moved to when it is ejected from the backup application.
>
> * Glacier Pool typically allows retrieval within 3-5 hours.
>
> * Deep Archive Pool typically allows retrieval within 12 hours.

> ### Add tags
>
> Optionally enter tags to categorize and label your tape.

## Automating tape creation

Tape Gateway automates creating new virtual tapes, helping decrease the need for manual tape management and making your large deployments more straightforward. Auto-create tapes to maintain the minimum number of available tapes that you configure.

The Tape Gateway spawns a new tape automatically when it has fewer tapes than the minimum number of available tapes specified for automatic tape creation. A new tape is spawned when the following occurs:

* A tape is imported from an import/export slot.

* A tape is imported to the tape drive.

## Retrieving a virtual tape backup

There are multiple recovery options available for your tape backups including the following:

* Identify a tape for restore within your backup application (using barcode).

* Run restore job within backup application.

* Search for a tape using the AWS Management Console.

* Retrieve a tape from S3 Glacier Flexible Retrieval or Glacier Deep Archive back to Amazon S3. You can retrieve an archived type to **any** tape gateway in the same Region.

* Run an import or inventory command on your backup application to refresh contents of the 1,500 tape slots provided by the VTL.
