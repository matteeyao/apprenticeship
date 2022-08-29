# Key Management Service (KMS)

Server-side encryption is the encryption of data at its destination by the application or service that receives it. AWS Key Management Service (AWS KMS) is a service that combines secure, highly available hardware and software to provide a key management system scaled for the cloud. Amazon S3 uses AWS KMS customer master keys (CMKs) to encrypt your Amazon S3 objects. SSE-KMS encrypts only the object data. Any object metadata is not encrypted. If you use customer-managed CMKs, you use AWS KMS via the AWS Management Console or AWS KMS APIs to centrally create encryption keys, define the policies that control how keys can be used, and audit key usage to prove that they are being used correctly. You can use these keys to protect your data in Amazon S3 buckets.

> **What is KMS?**
>
> * **Regional** secure key management and encryption and decryption
>
> * Manages **customer master keys** (CMKs)
>
> * Ideal for S3 objects, database passwords, and API keys stored in Systems Manager Parameter Store
>
> * Encrypt and decrypt data up to **4 KB** in size
>
> * Integrated w/ most AWS services
>
> * Pay per API call
>
> * Audit capability using CloudTrail-logs delivered to S3
>
> * **FIPS 140-2 Level 2**
>
> * Level 3 is CloudHSM

A customer master key (CMK) is a logical representation of a master key. The CMK includes metadata, such as the key ID, creation date, description, and key state. The CMK also contains the key material used to encrypt and decrypt data. You can use a CMK to encrypt and decrypt up to 4 KB (4096 bytes) of data. Typically, you use CMKs to generate, encrypt, and decrypt the data keys that you use outside of AWS KMS to encrypt your data. This strategy is known as **envelope encryption**. It's a pointer or reference to some underlying cryptographic material. The CMKs you create exist in a region in AWS and never leave that region or KMS at all.

CMKs can encrypt and decrypt data up to four kilobytes in size.

Pay per API call. So making API calls such as listing your keys, encrypting data, decrypting, or re-encrypting data will require you to pay per each of these API calls.

FIPS is a US government computer security standard used to approve cryptographic modules. Level Two means you just have to show evidence of tampering. There is a level three, which is supported by the cloud HSM product. Level Three has even more stringent security mechanisms than level Two. On the exam, if you see FIPs 140-2 level Two, the answer probably has something to do w/ KMS

## Integrating AWS KMS

Services integrated w/ AWS KMS:

* Amazon EBS

* Amazon S3

* Amazon RDS

* Amazon Redshift

* Amazon Elastic Transcoder

* Amazon WorkMail

* Amazon EMR

## Types of CMKs

![Fig. 1 Types of CMKs](../../../../img/aws/security/key-mgmt-service/types-of-cmks.png)

The first is an AWS managed CMK and these CMKs are free. These are created automatically when you first create an encrypted resource in an AWS service. You can track the usage of an AWS managed CMK, but the lifecycle and permissions of the key are managed on your behalf  by KMS.

Next, we have Customer Managed CMKs. These are ones that only you can create. Customer Managed CMKs give you full control over the lifecycle and permissions that determine who can use the key and under which conditions. Cryptographic best practices discourage extensive reuse of encryption keys so key rotation is important.

Finally, we have AWS Owned CMK. These are a collection of CMKs that an AWS service owns and manages for use in multiple AWS accounts. These are not in your AWS account, but an AWS service can use its AWS owned CMKs to protect the resources in your account, but you cannot view, use, track, or audit any of these.

## Symmetric vs. Asymmetric CMKs

| **Symmetric**                                                 | **Asymmetric**                                                      |
|---------------------------------------------------------------|---------------------------------------------------------------------|
| * **Same** key used for encryption and decryption             | * Mathematically related public/private key pair                    |
| * **AES-256**                                                 | * **RSA** and **elliptic-curve cryptograph (ECC)**                  |
| * Never leaves AWS unencrypted                                | * **Private** key never leaves AWS unencrypted                      |
| * Must call the KMS APIs to use                               | * Must call the KMS. APIs to use **private** key                    |
| * AWS services integrated w/ KMS use symmetric CMKs           | * **Download** the public key and use outside AWS                   |
| * Encrypt, decrypt, and re-encrypt data                       | * Used outside AWS by users who can't call KMS APIs                 |
| * Generate data keys, data key pairs, and random byte strings | * AWS services integrated w/ KMS **do not support** asymmetric CMKs |
| * **Import** your own key material                            | * Sign messages and verify signatures                               |

## Key Policies

So when you create a CMK programmatically w/ the KMS API, including through the AWS SDKs and command line tools, you have the option of providing the key policy for the new CMK. if you don't provide one, KMS creates one for you.

**Default Key Policy**

```
{
  "Sid": "Enable IAM User Permissions",
  "Effect": "Allow",
  "Principal", {"AWS": "arn:aws:iam::111122223333:root"},
  "Action": "kms:*",
  "Resource": "*"
}
```

Grants AWS account (root user) **full access** to the CMK

This default key policy has one policy statement that gives the AWS account, the root user that owns the CMK, full access to the CMK and enables IAM policies in the account to allow access to the CMK.

Here's an example policy that you can attach to a KMS key.

**Example Policy**

```
{
  "Sid": "Allow use of the key",
  "Effect": "Allow",
  "Principal": {"AWS": "arn:aws:iam::111122223333:role/EncryptionApp"},
  "Action": [
    "kms:DescribeKey",
    "kms:GenerateDataKey*",
    "kms:Encrypt",
    "kms:ReEncrypt*",
    "kms:Decrypt"
  ],
  "Resource": "*"
}
```

Grants IAM role access to crypto actions for encrypting and decrypting data

In this policy, we're granting a role called encryption app access to a number of actions on this key. Principles that can assume this role are allowed to perform the actions listed in the policy statement, which are the cryptographic actions for encrypting and decrypting data w/ a CMK.

```zsh
aws kms create-key --description "ACG Demo CMK"

aws kms create-alias --target-key-id <KeyId> --alias-name "alias/acgdemo"

aws kms list-keys
```

```zsh
echo "this is a secret message" > topsecret.txt

cat topsecret.txt

aws kms enecrypt --key-id "alias/acgdemo" --plaintext file://topsecret.txt --ouptut text --query CiphertextBlob

aws kms enecrypt --key-id "alias/acgdemo" --plaintext file://topsecret.txt --ouptut text --query CiphertextBlob | base64 --decode > topsecret.txt.encrypted

cat topsecret.txt.encrypted

aws kms decrypt --ciphertext-blob fileb://topsecret.txt.encrypted --output text --query Plaintext

aws kms decrypt --ciphertext-blob fileb://topsecret.txt.encrypted --output text --query Plaintext | base64 --decode
```

What if you want to encrypt a data file larger than four kilobytes in size? To do that, we can use something called a data encryption key or DEK. 

```zsh
aws kms generate-data-key --key-id "alias/acgdemo" --key-spec AES_256
```

Envelope encryptions reduces the network load since only the request and delivery of the much smaller data key go over the network. The data key is used locally in your application or encrypting AWS service, avoiding the need to send the entire block of data to KMS and suffer network latency.
