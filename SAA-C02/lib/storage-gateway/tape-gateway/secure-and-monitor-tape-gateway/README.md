# Secure and Monitor your Gateway

1. Which are true statements about Tape Gateway? (Select two)

[ ] All Tape Gateways must use the Veeam Backup application.

[x] Data is compressed and encrypted in transit and at rest.

[ ] Tape Gateways can only be deployed offline.

[x] Virtual tape storage is backed durably in Amazon S3, S3 Glacier Flexible Retrieval, and S3 Glacier Deep Archive.

[ ] The maximum number of tapes per gateway is 1,000.

**Explanation**: Virtual tape data is compressed and encrypted in transit and at rest and durably backed by Amazon S3, S3 Glacier Flexible Retrieval, and S3 Glacier Deep Archive.

2. Which Amazon CloudWatch metrics should you use if you need to monitor throughput between your Tape Gateway and the AWS Cloud? (Select two)

[ ] HealthNotifications

[x] CloudBytesDownloaded

[x] CloudBytesUploaded

[ ] CachePercentDirty

[ ] CachePercentUsed

**Explanation**: Use the CloudBytesDownloaded and CloudBytesUploaded metrics to monitor throughput between your Tape Gateway and the AWS Cloud.

3. What kind of data is written to the Storage Gateway cache but has not been uploaded to AWS?

**Dirty**
