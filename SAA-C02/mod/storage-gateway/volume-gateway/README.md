# Volume Gateway

Volume Gateway presents cloud-backed Internet Small Computer System Interface (iSCSI) block storage volumes to your applications to provide seamless integration between your IT environment and the Amazon Web Services (AWS) storage infrastructure for your data protection, recovery, and migration needs.

## Volume Gateway in cached mode

By using cached volumes, you can use Amazon S3 as your primary data storage, while retaining frequently accessed data locally in your storage gateway. Cached volumes minimize the need to scale your on-premises storage infrastructure, while still providing your applications with low-latency access to frequently accessed data. You can create storage volumes up to 32 TiB in size and afterward, attach these volumes as iSCSI devices to your on-premises application servers. When you write to these volumes, your gateway stores the data in Amazon S3. It retains the recently read data in your on-premises storage gateway's cache and uploads buffer storage.

Cached volumes can range from 1 GiB to 32 TiB in size and must be rounded to the nearest GiB. Each gateway configured for cached volumes can support up to 32 volumes for a total maximum storage volume of 1,024 TiB (1 PiB).

## Volume Gateway in stored mode

Stored Volumes are used if you need low-latency access to your entire dataset.
