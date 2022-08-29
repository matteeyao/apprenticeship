# Encrypting Data

1. For data privacy, a healthcare company has been asked to comply with the Health Insurance Portability and Accountability Act (HIPAA). The company stores all its backups on an Amazon S3 bucket. It is required that data stored on the S3 bucket must be encrypted.

What is the best option to do this? (Select TWO.)

[ ] Enable Server-Side Encryption on an S3 bucket to make use of AES-128 encryption.

[x] Before sending the data to Amazon S3 over HTTPS, encrypt the data locally first using your own encryption keys.

[ ] Enable Server-Side Encryption on an S3 bucket to make use of AES-256 encryption.

[x] Store the data in encrypted EBS snapshots.

[ ] Store the data on EBS volumes w/ encryption enabled instead of using Amazon S3.

**Explanation**: Server-side encryption is about data encryption at rest—that is, Amazon S3 encrypts your data at the object level as it writes it to disks in its data centers and decrypts it for you when you access it. As long as you authenticate your request and you have access permissions, there is no difference in the way you access encrypted or unencrypted objects. For example, if you share your objects using a pre-signed URL, that URL works the same way for both encrypted and unencrypted objects.

![Fig. 1 Server-Side Encryption](../../../../../../img/SAA-CO2/storage-services/simple-storage-service/security-and-encryption/encrypting-data/fig01.png)

You have three mutually exclusive options depending on how you choose to manage the encryption keys:

  1. Use Server-Side Encryption with Amazon S3-Managed Keys (SSE-S3)

  2. Use Server-Side Encryption with AWS KMS-Managed Keys (SSE-KMS)

  3. Use Server-Side Encryption with Customer-Provided Keys (SSE-C)

The options that say: **Before sending the data to Amazon S3 over HTTPS, encrypt the data locally first using your own encryption keys** and **Enable Server-Side Encryption on an S3 bucket to make use of AES-256 encryption** are correct because these options are using client-side encryption and Amazon S3-Managed Keys (SSE-S3) respectively. *Client-side encryption* is the act of encrypting data before sending it to Amazon S3 while SSE-S3 uses AES-256 encryption.

> **Storing the data on EBS volumes with encryption enabled instead of using Amazon S3** and **storing the data in encrypted EBS snapshots** are incorrect because both options use EBS encryption and not S3.

> **Enabling Server-Side Encryption on an S3 bucket to make use of AES-128 encryption** is incorrect as S3 doesn't provide AES-128 encryption, only AES-256.

<br />

2. All objects uploaded to an Amazon S3 bucket must be encrypted for security compliance. The bucket will use server-side encryption with Amazon S3-Managed encryption keys (SSE-S3) to encrypt data using 256-bit Advanced Encryption Standard (AES-256) block cipher.

Which of the following request headers must be used?

[ ] `x-amz-server-side-encryption-customer-key-MD5`

[ ] `x-amz-server-side-encryption-customer-algorithm`

[ ] `x-amz-server-side-encryption`

[ ] `x-amz-server-side-encryption-customer-key`

**Explanation**: **Server-side encryption** protects data at rest. If you use Server-Side Encryption with Amazon S3-Managed Encryption Keys (SSE-S3), Amazon S3 will encrypt each object with a unique key and as an additional safeguard, it encrypts the key itself with a master key that it rotates regularly. Amazon S3 server-side encryption uses one of the strongest block ciphers available, 256-bit Advanced Encryption Standard (AES-256), to encrypt your data.

If you need server-side encryption for all of the objects that are stored in a bucket, use a bucket policy. For example, the following bucket policy denies permissions to upload an object unless the request includes the `x-amz-server-side-encryption` header to request server-side encryption.

However, if you choose to use server-side encryption with customer-provided encryption keys (SSE-C), you must provide encryption key information using the following request headers:

  * `x-amz-server-side-encryption-customer-algorithm`

  * `x-amz-server-side-encryption-customer-key`

  * `x-amz-server-side-encryption-customer-key-MD5`

Hence, using the `x-amz-server-side-encryption` header is correct as this is the one being used for Amazon S3-Managed Encryption Keys (SSE-S3).

All other options are incorrect since they are used for SSE-C.

<br />

3. A company is generating confidential data that is saved on their on-premises data center. As a backup solution, the company wants to upload their data to an Amazon S3 bucket. In compliance with its internal security mandate, the encryption of the data must be done before sending it to Amazon S3. The company must spend time managing and rotating the encryption keys as well as controlling who can access those keys.

Which of the following methods can achieve this requirement? (Select TWO.)

[ ] Set up Server-Side Encryption (SSE) w/ EC2 key pair.

[ ] Set up Client-Side Encryption w/ Amazon S3 managed encryption keys.

[ ] Set up Server-Side Encryption w/ keys stored in a separate S3 bucket.

[x] Set up Client-Side Encryption using a client-side master key.

[x] Set up Client-Side Encryption w/ a customer master key stored in AWS Key Management Service (AWS KMS).

**Explanation**: Data protection refers to protecting data while in-transit (as it travels to and from Amazon S3) and at rest (while it is stored on disks in Amazon S3 data centers). You can protect data in transit by using SSL or by using client-side encryption. You have the following options for protecting data at rest in Amazon S3:

**Use Server-Side Encryption** ▶︎ You request Amazon S3 to encrypt your object before saving it on disks in its data centers and decrypt it when you download the objects.

* Use Server-Side Encryption with Amazon S3-Managed Keys (SSE-S3)

* Use Server-Side Encryption with AWS KMS-Managed Keys (SSE-KMS)

* Use Server-Side Encryption with Customer-Provided Keys (SSE-C)

**Use Client-Side Encryption** ▶︎ You can encrypt data client-side and upload the encrypted data to Amazon S3. In this case, you manage the encryption process, the encryption keys, and related tools.

* Use Client-Side Encryption with AWS KMS–Managed Customer Master Key (CMK)

* Use Client-Side Encryption Using a Client-Side Master Key

![Fig. 1 S3 Encryption](../../../../../img/storage-services/simple-storage-service/security-and-encryption/encrypting-data/fig02.png)

Hence, the correct answers are:

* Set up Client-Side Encryption with a customer master key stored in AWS Key Management Service (AWS KMS).

* Set up Client-Side Encryption using a client-side master key.

> The option that says: **Set up Server-Side Encryption with keys stored in a separate S3 bucket** is incorrect because you have to use AWS KMS to store your encryption keys or alternatively, choose an AWS-managed CMK instead to properly implement Server-Side Encryption in Amazon S3. In addition, storing any type of encryption key in Amazon S3 is actually a security risk and is not recommended.

> The option that says: **Set up Client-Side encryption with Amazon S3 managed encryption keys** is incorrect because you can't have an Amazon S3 managed encryption key for client-side encryption. As its name implies, an Amazon S3 managed key is fully managed by AWS and also rotates the key automatically without any manual intervention. For this scenario, you have to set up a customer master key (CMK) in AWS KMS that you can manage, rotate, and audit or alternatively, use a client-side master key that you manually maintain.

> The option that says: **Set up Server-Side encryption (SSE) with EC2 key pair** is incorrect because you can't use a key pair of your Amazon EC2 instance for encrypting your S3 bucket. You have to use a client-side master key or a customer master key stored in AWS KMS.

<br />

4. A company is looking to store their confidential financial files in AWS S3 which are accessed every week. The Architect was instructed to set up the storage system which uses envelope encryption and automates key rotation. It should also provide an audit trail that shows who used the encryption key and by whom for security purposes.

Which combination of actions should the Architect implement to satisfy the requirement in the most cost-effective way? 

[ ] Configure Server-Side Encryption w/ Amazon S3-Managed Keys (SSE-S3).

[ ] Configure Server-Side Encryption w/ AWS KMS-Managed Keys (SSE-KMS).

[ ] Configure Server-Side Encryption w/ Customer-Provided Keys (SSE-C).

**Explanation**: Server-side encryption is the encryption of data at its destination by the application or service that receives it. AWS Key Management Service (AWS KMS) is a service that combines secure, highly available hardware and software to provide a key management system scaled for the cloud. Amazon S3 uses AWS KMS customer master keys (CMKs) to encrypt your Amazon S3 objects. SSE-KMS encrypts only the object data. Any object metadata is not encrypted. If you use customer-managed CMKs, you use AWS KMS via the AWS Management Console or AWS KMS APIs to centrally create encryption keys, define the policies that control how keys can be used, and audit key usage to prove that they are being used correctly. You can use these keys to protect your data in Amazon S3 buckets.

A customer master key (CMK) is a logical representation of a master key. The CMK includes metadata, such as the key ID, creation date, description, and key state. The CMK also contains the key material used to encrypt and decrypt data. You can use a CMK to encrypt and decrypt up to 4 KB (4096 bytes) of data. Typically, you use CMKs to generate, encrypt, and decrypt the data keys that you use outside of AWS KMS to encrypt your data. This strategy is known as **envelope encryption**.

You have three mutually exclusive options depending on how you choose to manage the encryption keys:

* **Use Server-Side Encryption with Amazon S3-Managed Keys (SSE-S3)** ▶︎ Each object is encrypted with a unique key. As an additional safeguard, it encrypts the key itself with a master key that it regularly rotates. Amazon S3 server-side encryption uses one of the strongest block ciphers available, 256-bit Advanced Encryption Standard (AES-256), to encrypt your data.

* **Use Server-Side Encryption with Customer Master Keys (CMKs) Stored in AWS Key Management Service (SSE-KMS)** ▶︎ Similar to SSE-S3, but with some additional benefits and charges for using this service. There are separate permissions for the use of a CMK that provides added protection against unauthorized access of your objects in Amazon S3. SSE-KMS also provides you with an audit trail that shows when your CMK was used and by whom. Additionally, you can create and manage customer-managed CMKs or use AWS managed CMKs that are unique to you, your service, and your Region.

* **Use Server-Side Encryption with Customer-Provided Keys (SSE-C)** ▶︎ You manage the encryption keys and Amazon S3 manages the encryption, as it writes to disks, and decryption when you access your objects.

In the scenario, the company needs to store financial files in AWS which are accessed every week and the solution should use envelope encryption. This requirement can be fulfilled by using an Amazon S3 configured with Server-Side Encryption with AWS KMS-Managed Keys (SSE-KMS). Hence, using Amazon S3 to store the data and configuring Server-Side Encryption with AWS KMS-Managed Keys (SSE-KMS) are the correct answers.

> **Configuring Server-Side Encryption with Customer-Provided Keys (SSE-C)** and **configuring Server-Side Encryption with Amazon S3-Managed Keys (SSE-S3)** are both incorrect. Although you can configure automatic key rotation, these two do not provide you with an audit trail that shows when your CMK was used and by whom, unlike Server-Side Encryption with AWS KMS-Managed Keys (SSE-KMS).

<br />
