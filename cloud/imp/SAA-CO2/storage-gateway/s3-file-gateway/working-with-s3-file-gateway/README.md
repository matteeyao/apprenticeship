# Working w/ S3 File Gateway

1. What happens when the client reads data from the AWS Storage Gateway appliance?

**Read requests from the client first check to see if the data is in the cache. If not, Storage Gateway retrieves the data from Amazon S3, stores the data in the local cache, and provides it to the client.**

**Explanation**: Read requests from the client, first check to see if the data is in the cache. If the data is not in the cache, it is fetched from the S3 bucket. The Storage Gateway service retrieves the data from Amazon S3 and sends it to the Storage Gateway appliance. The Storage Gateway appliance receives the data, stores it in the local cache, and provides it to the client.

2. What happens when the client writes data to the AWS Storage Gateway appliance?

**Write requests from the client, are written to the cache first, and then asynchronously uploaded to the S3 bucket.**

**Explanation**: Write requests from the client are written to the cache first and then asynchronously uploaded to the S3 bucket. To reduce data transfer overhead, the gateway uses multipart uploads and *copy put*, so only changed data in your files is uploaded to Amazon S3. Then, data that is already in the cloud is used to create a new version of the object.

3. What is the meaning of upload notification with respect to the Amazon S3 File Gateway?

**It notifies you when all the files written to a file share are uploaded to Amazon S3.**

**Explanation**: The file upload notification provides a notification for each individual file that is uploaded to Amazon S3 through S3 File Gateway.

4. A solutions architect is working on optimizing a legacy document management application running on multiple Microsoft Windows servers in an on-premises data center. The application accesses a large number of files on a network file share which is running out of space. The chief information officer wants to migrate the on-premises storage to AWS but still must be able to support the legacy application.

What should the solutions architect do to meet these requirements?

**Use an AWS Storage Gateway and the option of Amazon S3 File Gateway**

**Explanation**: EFS does not integrate w/ Windows. Using AWS Storage Gateway and the option of volume gateway is not an option since volume gateway will provide you cloud-backed storage volumes that you can mount ISCSI devices to your on-premises servers. A volume gateway is for offloading not for file systems.
