# Encrypted Root Device Volumes and Snapshots

> Root Device Volumes Can Now Be Encrypted. If you have an un-encrypted root device volume that needs to be encrypted do the following:
>
> * Create a Snapshot of the un-encrypted root device volume
>
> * Create a copy of the Snapshot and select the encrypt option
>
> * Create an AMI from the encrypted snapshot
>
> * Use that AMI to launch new encrypted instances


## Learning summary

> * Snapshots of encrypted volumes are encrypted automatically

> * Volumes restored from encrypted snapshots are encrypted automatically

> * You can share snapshots, but only if they are unencrypted

> * These snapshots can be shared w/ other AWS accounts or made public

> Process for making an unencrypted root device volume encrypted:
>
> * Create a Snapshot of the unencrypted root device volume
>
> * Create a copy of the Snapshot and select the encrypt option
>
> * Create an AMI from the encrypted snapshot
>
> * Use that AMI to launch new encrypted instances
