# Snowball

> Essentially a big disk that you can use to move your data in and out of the AWS cloud.

Snowball is a petabyte-scale data transport solution that uses secure appliances to transfer large amounts of data into and out of AWS (export data). Using Snowball addresses common challenges w/ large-scale data transfers including high network costs, long transfer times, and security concerns. Transferring data w/ Snowball is simple, fast, secure, and can be as little as one-fifth the cost of high-speed internet.

Snowball comes in either a 50TB or 80TB size. Snowball uses multiple layers of security designed to protect your data including tamper-resistant enclosures, 256-bit encryption, and an industry-standard Trusted Platform Module (TPM) designed to ensure both security and full chain-of-custody of your data. Once the data transfer job has been processed and verified, AWS performs a software erasure of the Snowball appliance.

## What is Snowball Edge?

AWS Snowball Edge is a 100TB data transfer device w/ on-board storage and compute capabilities. You can use Snowball Edge to move large amounts of data into and out of AWS, as a temporary storage tier for large local datasets, or to support local workloads in remote or offline locations.

You can run Lambda functions off of Snowball Edge, for example. Essentially, provides you w/ compute and storage.

Snowball Edge connects to your existing applications and infrastructure using standard storage interfaces, streamlining the data transfer process and minimizing setup and integration. Snowball Edge can cluster together to form a local storage tier and process your data on-premises, helping ensure your applications continue to run even when they are not able to access the cloud.

## What is Snowmobile?

AWS Snowmobile is an Exabyte-scale data transfer service used to move extremely large amounts of data to AWS. You can transfer up to 100PB per Snowmobile, a 45-foot long ruggedized shipping container, pulled by a semi-trailer truck. Snowmobile makes it easy to move massive volumes of data to the cloud, including video libraries, image repositories, or even a complete data center migration. Transferring data w/ Snowmobile is secure, fast, and cost effective.

## When should I used Snowball?

| **Available Internet Connection** | **Theoretical Min. Number of Days to Transfer 100TB at 80% Network Utilization** | **When to Consider AWS Import/Export Snowball?** |
|-----------------------------------|----------------------------------------------------------------------------------|--------------------------------------------------|
| T3 (44.736Mbps)                   | 269 days                                                                         | 2TB or more                                      |
| 100Mbps                           | 120 days                                                                         | 5TB or more                                      |
| 1000Mbps                          | 12 days                                                                          | 60TB or more                                     |

## Learning Recap

* Snowball can...

    * Import to S3

    * Export from S3
