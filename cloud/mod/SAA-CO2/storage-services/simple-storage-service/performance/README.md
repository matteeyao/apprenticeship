# S3 Performance

## What is a prefix w/ S3?

* `mybucketname/folder1/subfolder1/myfile.jpg` → **/folder1/subfolder1**

* `mybucketname/folder2/subfolder1/myfile.jpg` → **/folder2/subfolder1**

* `mybucketname/folder3/myfile.jpg` → **/folder3**

* `mybucketname/folder4/myfile.jpg` → **/folder4/subfolder4**

## S3 performance

S3 has extremely low latency. You can get the first byte out of S3 within **100-200 milliseconds**

You can also achieve a high number of requests: **3,500 PUT/COPY/POST/DELETE** and **5,500 GET/HEAD** requests per second per prefix.

1. You can get better performance by spreading your reads across **different prefixes**. For example, if you are using **two prefixes**, you can achieve **11,000 requests per second**.

2. If we used all **four prefixes** in the last example, you would achieve **22,000 requests per second**.

The more prefixes we have, the better performance we can achieve.

## S3 limitations when using KMS

* If you are using **SSE-KMS** to encrypt your objects in S3, you must keep in mind the **KMS limits**.

* When you **upload** a file, you will call **GenerateDataKey** in the KMS API.

* When you **download** a file, you will call **Decrypt** in the KMS API.

The KMS is going to have a certain number of limits.

* Uploading/downloading will count toward the **KMS quota**.

* Quota is region-specific, however, it's either **5,500**, **10,000**, or **30,000** requests per second.

* Currently, you **cannot** request a quota increase for KMS.

So, if you are using server-side encryption for KMS, bear in mind that when you're encrypting and decrypting data, you're going to hit these hard limits which are region-specific.

## S3 performance: uploads

### Multipart uploads

* Recommended for files **over 100 MB**

* Required for files **over 5 GB**

* Parallelize uploads (increases **efficiency**)

## S3 performance: downloads

### S3 Byte-range fetches

* Parallelize **downloads** by specifying byte ranges.

* If there's a failure in the download, it's only for a specific byte range.

### S3 Byte-range fetches

* Can be used to **speed up** downloads

* Can be used to just download **partial amounts of the file** (e.g., header information)

## Exam tips

* S3 performance comes from prefixes. Prefixes are the pathway between your bucket name and your file.

* The more prefixes you use, the better performance you're going to get out of S3. You can achieve a high number of requests: **3,500 PUT/COPT/POST/DELETE** and **5,500 GET/HEAD** requests per second per prefix.

* You can get better performance by spreading your reads across **different prefixes**. For example, if you are using **two prefixes**, you can achieve **11,000 requests per second**.

* If you are using SSE-KMS to encrypt your objects in S3, you must keep in mind the KMS limits.

    * Uploading/downloading will count toward the **KMS quota**

    * Region-specific, however, it's either **5,500**, **10,000**, or **30,000** requests per second

    * Currently, you **cannot** request a quota increase for KMS

* Use **multipart uploads** to increase performance when **uploading files** to S3.

* Should be used for any files **over 100 MB** and must be used for any file **over 5 GB**.

* Use **S3 byte-range fetches** to increase performance when **downloading files** to S3.

