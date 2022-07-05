# CloudHSM

So we have scenarios where data protection solutions that use encryption keys or digital signatures requiring the use of private keys as critical. Hardware security modules, or HSMs provide a tamper resistant environment for managing these keys. In this module, we'll talk about CloudHSM, which is Amazon's solution to scenarios where your own encryption keys are subject to corporate or regulatory requirements and therefore need validated control.

> **What is CloudHSM?**
>
> * **Dedicated** hardware security module (HSM)
>
> * **FIPS 140-2 Level 3**
>
> * Level 2 is KMS
>
> * Manage your own keys
>
> * **No access** to the AWS-managed component
>
> * Runs within a VPC in your account
>
> * Single tenant, dedicated hardware, multi-AZ cluster
>
> * Industry-standard APIs-**no AWS APIs**
>
> * **PKCS#11**
>
> * **Java Cryptography Extensions (JCE)**
>
> * **Microsoft CryptoNG (CNG)**
>
> * Keep your keys safe - **irretrievable** if lost

FIPS is a U.S. Government computer security standard that's used to approve cryptographic modules and it has a number of different compliance levels. Now Compliance Level 3 is where physical security mechanisms might include the use of strong enclosures and tamper detection or response circuitry that zeroes out all of your plain text cryptographic security providers when the removable doors or covers of the cryptographic module inside are opened up. Now if you remember when we talked about KMS, KMS is Level Two compliant, which simply needs to show evidence of tampering. This is key for the exam b/c if you see any questions that reference FIPS 140-2 Level Three, the answer is always going to be CloudHSM.

Now, the difference between KMS and CloudHSM is that you manage your own keys w/ CloudHSM. CloudHSM offers a single tenant multi-AZ cluster. It's dedicated to you. KMS is multi-tenant. It uses HSMs internally, but those are shared across customer accounts.

Now b/c it's a managed service, you don't have any access to the AWS managed component of CloudHSM. You keep control over your access keys and AWS themselves don't have any access to your keys and CloudHSM runs within a VPC in your account.

Also note that you have to keep your keys safe w/ CloudHSM. If you lose your keys, they're irretrievable.

## CloudHSM Architecture

![Fig. 1 CloudHSM](../../../../img/SAA-CO2/security/cloudhsm/cloudhsm.png)

W/ CloudHSM, you first need to create a cluster in either an existing VPC or a new VPC. How this works is that CloudHSM will actually operate inside its own VPC, dedicated to CloudHSM from a security isolation standpoint. CloudHSM will then project ENIs or Elastic Network Interfaces into the VPC of your choosing and this is how your applications communicate w/ the CloudHSM cluster. Inside the cluster you'll create specific instances of HSMs. Now CloudHSM is not highly available by default. You'll need to explicitly provision HSMs across availability zones. If any of these HSMs fail, or if any AZ becomes unavailable, you'll still have the other HSM instances. Ideally, you'll want to place one HSM per subnet in each availability zone w/ a minimum of two AZs like you see here in the diagram, which is what's recommended by AWS.

## Learning summary

> * Regulatory compliance requirements
>
> * FIPS 140-2 Level 3
