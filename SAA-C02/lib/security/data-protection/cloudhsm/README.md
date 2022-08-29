# CloudHSM

1. A company requires all the data stored in the cloud to be encrypted at rest. To easily integrate this with other AWS services, they must have full control over the encryption of the created keys and also the ability to immediately remove the key material from AWS KMS. The solution should also be able to audit the key usage independently of AWS CloudTrail.

Which of the following options will meet this requirement?

[ ] Use AWS Key Management Service to create AWS-owned CMKs and store the non-extractable key material in AWS CloudHSM.

[ ] Use AWS Key Management Service to create a CMK in a custom key store and store the non-extractable key material in Amazon S3.

[ ] Use AWS Key Management Service to create AWS-managed CMKs and store the non-extractable key material in AWS CloudHSM.

[x] Use AWS Key Management Service to create a CMK in a custom key store and store the non-extractable key material in AWS CloudHSM.

**Explanation**: The **AWS Key Management Service (KMS)** custom key store feature combines the controls provided by **AWS CloudHSM** with the integration and ease of use of AWS KMS. You can configure your own CloudHSM cluster and authorize AWS KMS to use it as a dedicated key store for your keys rather than the default AWS KMS key store. When you create keys in AWS KMS you can choose to generate the key material in your CloudHSM cluster. CMKs that are generated in your custom key store never leave the HSMs in the CloudHSM cluster in plaintext and all AWS KMS operations that use those keys are only performed in your HSMs.

AWS KMS can help you integrate with other AWS services to encrypt the data that you store in these services and control access to the keys that decrypt it. Take note that each custom key store is associated with an AWS CloudHSM cluster in your AWS account. Therefore, when you create an AWS KMS CMK in a custom key store, AWS KMS generates and stores the non-extractable key material for the CMK in an AWS CloudHSM cluster that you own and manage. This is also suitable if you want to be able to audit the usage of all your keys independently of AWS KMS or AWS CloudTrail.

Since you control your AWS CloudHSM cluster, you have the option to manage the lifecycle of your CMKs independently of AWS KMS. There are four reasons why you might find a custom key store useful:

  1. You might have keys that are explicitly required to be protected in a single-tenant HSM or in an HSM over which you have direct control.

  2. You might have keys that are required to be stored in an HSM that has been validated to FIPS 140-2 level 3 overall (the HSMs used in the standard AWS KMS key store are either validated or in the process of being validated to level 2 with level 3 in multiple categories).

  3. You might need the ability to immediately remove key material from AWS KMS and to prove you have done so by independent means.

  4. You might have a requirement to be able to audit all use of your keys independently of AWS KMS or AWS CloudTrail.

> The option that says: **Use AWS Key Management Service to create a CMK in a custom key store and store the non-extractable key material in Amazon S3** is incorrect because Amazon S3 is not a suitable storage service to use in storing encryption keys. You have to use AWS CloudHSM instead.

> The options that say: **Use AWS Key Management Service to create AWS-owned CMKs and store the non-extractable key material in AWS CloudHSM** and **Use AWS Key Management Service to create AWS-managed CMKs and store the non-extractable key material in AWS CloudHSM** are both incorrect because the scenario requires you to have full control over the encryption of the created key. AWS-owned CMKs and AWS-managed CMKs are managed by AWS. Moreover, these options do not allow you to audit the key usage independently of AWS CloudTrail.

<br />

2. A news company is planning to use a Hardware Security Module (CloudHSM) in AWS for secure key storage of their web applications. You have launched the CloudHSM cluster but after just a few hours, a support staff mistakenly attempted to log in as the administrator three times using an invalid password in the Hardware Security Module. This has caused the HSM to be zeroized, which means that the encryption keys on it have been wiped. Unfortunately, you did not have a copy of the keys stored anywhere else.

How can you obtain a new copy of the keys that you have stored on Hardware Security Module?

[ ] Contact AWS Support and they will provide you a copy of the keys.

[ ] Use the Amazon CLI to get a copy of the keys.

[ ] The keys are lost permanently if you did not have a copy.

[ ] Restore a snapshot of the Hardware Security Module.

**Explanation**: Attempting to log in as the administrator more than twice with the wrong password zeroizes your HSM appliance. When an HSM is zeroized, all keys, certificates, and other data on the HSM is destroyed. You can use your cluster's security group to prevent an unauthenticated user from zeroizing your HSM.

Amazon does not have access to your keys nor to the credentials of your Hardware Security Module (HSM) and therefore has no way to recover your keys if you lose your credentials. Amazon strongly recommends that you use two or more HSMs in separate Availability Zones in any production CloudHSM Cluster to avoid loss of cryptographic keys.

Refer to the CloudHSM FAQs for reference: 

**Q: Could I lose my keys if a single HSM instance fails?**

Yes. It is possible to lose keys that were created since the most recent daily backup if the CloudHSM cluster that you are using fails and you are not using two or more HSMs. Amazon strongly recommends that you use two or more HSMs, in separate Availability Zones, in any production CloudHSM Cluster to avoid loss of cryptographic keys.

**Q: Can Amazon recover my keys if I lose my credentials to my HSM?**

No. Amazon does not have access to your keys or credentials and therefore has no way to recover your keys if you lose your credentials.

<br />
