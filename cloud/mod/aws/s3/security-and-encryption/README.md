# S3 Security and Encryption

## S3 basics

By default, all newly created buckets are **PRIVATE**. You can setup access control to your buckets using:

* **Bucket Policies**

    * Work at a bucket level

* **Access Control Lists**

    * Go down to individual files or objects

S3 buckets can be configured to create access logs which log all requests made to the S3 bucket. This can be sent to another bucket and even another bucket in another account.

Encryption In Transit is achieved by

* **SSL/TLS**: think `https`. So long as you're using `https`, all the traffic, all the files that you're uploading are going to be encrypted.

Encryption At Rest (Server Side) is achieved by

* **S3 Managed Keys - Server Side Encryption S3 (SSE-S3)**

    * where S3 handles all the encryption for us

* **AWS Key Management Service, Managed Keys - SSE-KMS**

    * where we can start using keys from the KMS service

* **Server Side Encryption With Customer Provided Keys - SSE-C**

    * where you provide the keys and you manage the encryption

Client Side Encryption

* where you encrypt the objects and upload them to S3
