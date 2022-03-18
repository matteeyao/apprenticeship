# Cross region replication

A way of replicating objects across regions (in the name), but you can also replicate them within the same region.

* Versioning must be enabled on both the source and destination buckets.

* Files in an existing bucket are not replicated automatically.

* All subsequent updated files will be replicated automatically.

* Delete markers are not replicated.

    * So if you delete an object in one bucket, it's not going to be deleted in the other.

* Deleting individual versions or delete markers will not be replicated.

* Understand what Cross Region Replication is at a high level.
