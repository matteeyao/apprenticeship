# AWS Directory Service

> * Family of managed services
>
> * Connect AWS resources w/ on-premises Microsoft Active Directory (AD)
>
> * Standalone directory in the cloud
>
> * Use existing corporate credentials
>
> * SSO to any domain-joined EC2 instance

* Single sign-on (SS)

* So if you have a fleet of EC2 instances all joined to an Active Directory domain, you don't need to manage the credentials on any individual EC2 instances.

## What is Active Directory?

> * On-premises directory service
>
> * Hierarchical database of users, groups, computers - **trees** and **forests**
>
> * Group policies
>
> * LDAP and DNS

* Lightweight Directory Access Protocol (LDAP) and Domain Name Service (DNS)

> * Supports Kerberos, LDAP, and NTLM authentication
>
> * Intended to be configured in a highly available configuration requiring multiple servers

* The downside is that there is typically lots of management overhead, which is one reason why you would want to use a managed service

## AWS Managed Microsoft AD

> * AD domain controllers (DCs) running Windows Server
>
> * Reachable by applications in your VPC
>
> * Add additional DCs for High Availability (HA) and performance
>
> * Exclusive access to DCs

* No other AWS user will share those Domain Controllers (DCs) so you can be confident about the security

> * Extend existing AD to on-premises using **AD Trust**

| **AWS**                   | **Customer**                      |
|---------------------------|-----------------------------------|
| * Multi-AZ deployment     | * Users, groups, GPOs             |
| * Patch, monitor, recover | * Standard AD tools               |
| * Instance rotation       | * Scale out DCs                   |
| * Snapshot and restore    | * Trusts (resource forest)        |
|                           | * Certificate authorities (LDAPS) |
|                           | * Federation                      |

## Simple AD

> * Standalone managed directory
>
> * Basic AD features
>
> * Small <= 500; Large <= 5,000 users
>
> * Easier to manage EC2

* where you want to use your existing corporate credentials to log in to those EC2 instances, rather than having to provision usernames and passwords on all of those instances or manage any kind of keys 

> * Linux workloads that need LDAP
>
> * Does not support **trusts** (can't join on-premises AD)

* In other words, you can't join Simple AD to your on premises AD infrastructure.

## AD Connector

> * Directory gateway (proxy) for on-premises AD

* So AD Connector is a directory gateway or proxy for your on premises AD. This helps you avoid caching information in the cloud

> * Avoid caching information in the cloud
>
> * Allow on-premises users to log in to AWS using AD
>
> * Join EC2 instances to your existing AD domain
>
> * Scale across multiple AD Connectors

## Cloud Directory

> * Directory-based store for **developers**
>
> * Multiple hierarchies w/ hundreds of millions of objects
>
> * Use cases: org charts, course catalogs, device registries
>
> * Fully managed service

* So no infrastructure needs to be managed

## Amazon Cognito User Pools

> * Managed user directory for SaaS applications
>
> * Sign-up and sign-in for web or mobile
>
> * Works w/ **social media** identities

## AD versus Non-AD Compatible Services

* The AD Compatible solutions also enable users to sign into AWS applications like Amazon workspaces and QuickSight w/ your Active Directory credentials.

* So if you see an exam question that asks you about logging into workspaces or QuickSight w/ AD credentials, think about the AD compatible services here.

* For the Non-AD Compatible services, if you're a developer and you don't need Active Directory, you can use Cloud Directory to create directories that organize and manage hierarchical information and Cognito user pools work w/ mobile and web applications

| ✔️ **AD Compatible**                                                              | ❌ **Customer**       |
|----------------------------------------------------------------------------------|----------------------|
| * Managed Microsoft AD (a.k.a, Directory Service for Microsoft Active Directory) | * Cloud Directory    |
| * AD Connector                                                                   | * Cognito user pools |
| * Simple AD                                                                      |                      |

## Learning summary

> * Active Directory
>
> * Connect AWS resources w/ on-premises AD
>
> * SSO to any domain-joined EC2 instance
>
> * AWS Managed Microsoft AD

* These are real Active Directory domain controllers, running windows server, running inside AWS

> * AD Trust

* This is where you can extend your existing Active Directory to on-premises AD using AD trust

* We'll use AD Trust to extend existing Active Directory inside AWS to your on-premises environment

> * AWS versus customer responsibility

* Now, w/ these managed services, you'll want to understand where the responsibilities are divided between AWS and you as a customer. So for example, things like patching, scale-out, user and group management, etc.

> * Simple AD

* We referred to Simple AD as the baby brother to manage Microsoft AD and it does support a lot of AD compatible features, but one feature it does not support are trusts, which means that you can't join Simple AD to your on-premises AD.

> * Does not support trusts
>
> * AD Connector

* If you want to do that, you'll need to use AD Connector. This is a directory gateway or proxy for your on-premises AD.

> * Cloud Directory

* This is a service for developers looking to work w/ hierarchical data

* Cloud Directory has nothing to do w/ Microsoft AD

* Another service we covered that also has nothing to do w/ AD is...

> * Cognito user pools

* This is a managed user directory that works w/ social media identities.

* All of the services listed above are distinct, some are AD-compatible and some are not

> * AD vs. Non-AD compatible services
