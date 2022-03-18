# Global Tables

> **Managed Multi-Master, Multi-Region Replication**
>
> * Globally distributed applications
>
> * Based on DynamoDB streams
>
> * Multi-region redundancy for Disaster Recovery (DR) or High Availability (HA) applications
>
> * No application rewrites
>
> * Replication latency under **one second**

* Recall **streams** are time-ordered running change log of items in DynamoDB. Global tables use the streams in order to replicate data across regions, so we have to `enable streams`.