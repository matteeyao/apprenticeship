# Security overview of FSx for Windows File Server

## AWS shared responsibility model

At a high level, AWS is responsible for securing and managing the cloud infrastructure, while you, the customer, are responsible for securing whatever applications and data you put into the cloud. By working together to secure both parts of the cloud, you and AWS can ensure that the applications, data, operating systems, and infrastructure are secure and safe from outside threats.

## FSx for Windows File Server security features

> ### Data encryption
>
> FSx for Windows File Server automatically encrypts your data at rest and in transit.

> ### Identity-based authentication
>
> FSx for Windows File Server supports identity-based authentication through Microsoft Active Directory. Your users can authenticate themselves and access your file system by using their existing Active Directory credentials.

> ### File- and folder-level access control
>
> FSx for Windows File server supports fine-grained file- and folder-level access control using Windows access control lists (ACLs).

> ### Network traffic access control
>
> FSx for Windows File Server enables you to secure network access to your file system using VPC security groups.

> ### Administrative APIs access control
>
> FSx for Windows File Server enables you to control who can administer your file systems and backups using w/ AWS Identity and Access Management (IAM).

## Data encryption

> ### Encryption at rest
>
> FSx for Windows File Server encrypts your data at rest using keys you manage through AWS Key Management Service (AWS KMS). Using AWS KMS, you can create and manage cryptographic keys and control their use across your file systems. Encryption of data at rest is automatically enabled when you create a file system. FSx for Windows File Server uses the open standard AES-256 encryption algorithm to encrypt data at rest.

> ### Encryption in transit
>
> If you access your file system from clients that support SMB 3.0 and later, FSx for Windows File Server encrypts data in transit using SMB Kerberos session keys. FSx for Windows File Server also allows unencrypted connections from compute instances that do not support SMB 3.0. You can choose to enforce in-transit encryption on all connections to your file system by limiting access to only those clients that support SMB 3.0 to help meet compliance needs.

## Identity-based authentication with Microsoft Active Directory

FSx for Windows File Server supports identity-based authentication through Microsoft Active Directory. Active Directory is the Microsoft directory service to store information about objects on the network. It makes this information easy for administrators and users to find and use. These objects typically include user and computer accounts and shared resources such as file servers.

When you create a file system, you join it to your Active Directory domain. Your domain-joined compute clients can then authenticate themselves and access the file system using their existing identities in Active Directory. Users can also use their existing identities to control access to individual files and folders. You can choose from two options to integrate your file system with Active Directory. You can use AWS Directory Service for Microsoft Active Directory (AWS Managed Microsoft AD) or your self-managed Microsoft Active Directory. 

## Using FSx for Windows File Server with AWS Managed Microsoft AD

AWS Managed Microsoft AD is an AWS service that provides fully managed directories to use in your FSx for Windows File Server deployment. AWS Managed Microsoft AD enables you to set up and run Active Directories in the cloud. The service deploys each directory across multiple Availability Zones in a highly available topology and automatically detects and replaces domain controllers that fail. You do not have to install software. AWS handles all the patching and software updates. By integrating FSx for Windows File Server with AWS Managed Microsoft AD, you get a turnkey solution whereby AWS handles the seamless integration of the two services. 

![Fig. 1 Using Amazon FSx w/ Active Directory Service for Microsoft Active Directory](../../../../../img/SAA-CO2/storage-services/fsx-for-windows-file-server/security/diag01.png)

## Using FSx for Windows File Server with self-managed Microsoft AD

If your company uses a self-managed Active Directory (on premises or in the cloud), you can join your file system directly to your existing self-managed Active Directory domain. This integration offers a flexible, minimally disruptive option for your on-premises deployments that extend their existing Active Directory environment into the AWS Cloud.

## File- and folder-level access control using Windows ACLs

Your domain-joined compute instances access your file shares using Active Directory credentials. You can use Windows access control lists (ACLs) for fine-grained file- and folder-level access control. Your file system automatically verifies the credentials of users accessing it to enforce the Windows ACLs. The Windows ACLs for the default share on your file system allow read/write access to domain users.

The Windows ACLs also allow full control to the delegated administrators group in your Active Directory. If you integrate your file system with AWS Managed Microsoft AD, this group is AWS Delegated FSx Administrators. If you are integrating your file system with your AWS Managed Microsoft AD, this group can be Domain Admins or a custom-delegated administrators group. To change the ACLs, you can map the share as a user that is a member of the delegated administrators group.

## Network interface level access control with Amazon VPC security groups

You can control which resources within your VPC have access to your file system using VPC security groups. A security group is a virtual firewall that controls traffic to and from your file system elastic network interface. 

Security groups contain inbound and outbound rules. Inbound rules control incoming traffic whereas outbound rules control outgoing traffic from your file system. 

You can configure your security group to allow access from another security group, IP addresses, specific service ports, or IP protocols. 

You can modify the rules for a security group at any time. New and modified rules are automatically applied to all resources that are associated with the security group.

![Fig. 1 Using Amazon FSx w/ Active Directory Service for Microsoft Active Directory](../../../../../img/SAA-CO2/storage-services/fsx-for-windows-file-server/security/diag02.png)

The security group you associate with your file system must contain the following rules:

* Inbound and outbound rules to allow the following ports:

<table style="width:100%;"><tbody><tr><td class="fr-thick" style="width:50%;background-color:rgb(255, 255, 255);text-align:center;"><span style="font-size:17px;"><strong>Rules</strong></span></td><td class="fr-thick" style="width:50%;background-color:rgb(255, 255, 255);text-align:center;"><span style="font-size:17px;"><strong>Ports</strong></span></td></tr><tr><td class="fr-thick" style="width:50%;background-color:rgb(255, 255, 255);"><span style="font-size:17px;">UDP</span></td><td class="fr-thick" style="width:50%;background-color:rgb(255, 255, 255);"><span style="font-size:17px;">53, 88, 123, 389, 464</span></td></tr><tr><td class="fr-thick" style="width:50%;background-color:rgb(255, 255, 255);"><span style="font-size:17px;">TCP</span></td><td class="fr-thick" style="width:50%;background-color:rgb(255, 255, 255);"><span style="font-size:17px;">53, 88, 135, 389, 445, 464, 636, 3268, 3269, 9389, 49152–65535</span></td></tr></tbody></table>

* Inbound and outbound rules to allow IP addresses or security groups associated with the following resources:

  * Client compute instances from which you want to access your file system

  * Other file servers with which you expect your file system to communicate

* Outbound rules to allow all traffic to the Active Directory to which you are joining your file system. To do this, do one of the following:

  * Allow outbound traffic to the security group ID associated with your AWS Managed AD directory.

  * Allow outbound traffic to the IP addresses associated with your self-managed Active Directory domain controllers.

## Resource management control with AWS Identity and Access Management (IAM) 

FSx for Windows File Server uses security credentials to identify you and to grant you access to resources. You use AWS Identity and Access Management (IAM) to control which users, services, and applications can manage your FSx for Windows File Server resources, such as file systems and backups. By default, IAM identities (users, groups, and roles) do not have permission to create, modify, or delete AWS resources. 

To control the IAM identities that can manage your FSx for Windows File Server resources, you must create an IAM policy. An IAM policy grants permissions to use specific resources and perform API actions. You then attach the IAM policy to the IAM identity that requires access.
