# Deploying a file system

## Creating a file system

When you create a new file system, FSx for Windows File Server prompts you to specify the following settings.

### Required settings

1. A name for your file system. You can use up to 256 letters, white space, and numbers. You can also use the special characters + - = . _ : /

2. The deployment mode for your file system (Single-AZ or Multi-AZ).

3. The storage type your file system will use (SSD or HDD). 

4. The desired storage capacity, in GiB. If you're using SSD storage, you can enter any whole number in the range of 32–65,536. If you're using HDD storage, you can enter any whole number in the range of 2,000–65,536.

5. The desired throughput capacity, in MB/s. You can manually specify a throughput value, or you can use the AWS recommended throughput (which is based on the storage capacity). 

6. The virtual private cloud (VPC) and subnets in which the file system should reside.

7. The Microsoft Active Directory that you want to use for your file system authentication and access control (AWS Managed Microsoft Active Directory or self-managed Microsoft Active Directory).

8. The encryption key you want to use to protect your file system data at rest. 

### Optional settings

> ### Tags
>
> To help you easily identify or organize the multiple file systems you create, you can add tags to your file systems. A tag is a case-sensitive key-value pair that helps you manage, filter, and search for file systems. You can create tags to identify a department or an application.

> ### Maintenance window
>
> FSx for Windows File Server performs routine software patching for the software it manages. You can specify a maintenance window to control when this patching occurs. Patching should require only a fraction of your 30-minute maintenance window. During these few minutes of time, your Single-AZ file system is temporarily unavailable, and Multi-AZ file systems may fail over and fail back.

> ### Backups
>
> You can configure an automatic daily backup schedule and retention period. We recommend that you choose a convenient time of the day for your backup window. Choose a time outside of the normal operating hours for the applications that use your file system. Retention period refers to the period of time that FSx for Windows File Server retains your backups. You can specify a retention period in the range of 0–35 days. You can turn off automatic daily backup by setting the retention period to 0 days.

## Connecting to your file system

Next, connect a compute instance to a file share in your file system. The process to connect file shares to a compute instance varies depending on the type of instance and the operating system you are using.

After you connect a file share to an instance, your applications and users can access the contents of your file shares as if they are local files and folders. 

## Demonstration: Create a file system and connect it to an Amazon EC2 instance

> ### Introduction
>
> In this FSx for Windows File Server demonstration, you learn how to set up a Multi-AZ file system using the AWS Management Console. This demonstration also walks you through connecting the file system to Amazon EC2 instances running Windows and Linux operating systems.
>
> Before you can follow this demo, you must already have two EC2 instances configured: A Microsoft Windows–based Amazon EC2 instance and a Linux-based Amazon EC2 instance. Both instances must be joined to your AWS Directory Service directory.

### Create a file system

1. Sign in to the AWS console.

2. Select **Services** ▶︎ **Amazon FSx** ▶︎ **Create file system** ▶︎ **Amazon FSx for Windows File Server**

3. Enter the following file system details:

  * A name for your file system.

  * Deployment type: Multi-AZ

  * Storage type: SSD

  * Storage capacity: 32 GiB

  * Throughput capacity: 64 MB/s.

  * The virtual private cloud (VPC) and subnets in which the file system should reside.

  * A VPC security group to associate with your file system.

  * For Active Directory, select your Active Directory environment from the drop-down list.

  * Leave encryption, maintenance preferences, and tags settings at their default values.

4. Review the settings you configured and select **Create file system**.

### Map file system on a Microsoft Windows EC2 instance

1. Optionally, review your Windows EC2 instance Active Directory configuration.

2. Connect to the Windows EC2 instance using the Active Directory administrator credentials.

3. From the Start menu, open File Explorer. Select **My Computer** ▶︎ **Map Network Drive**.

4. Choose the **Z** drive letter. For **Folder**, enter your file system DNS name and default share name. You can find the DNS name in the Amazon FSx console ▶︎ **Network & Security** section. Select **Finish**.

5. Use the Notepad text editor to create test files within your file share.

6. Optional steps to create new file shares: Search for **fsmgmt.msc** to open the **Shared Folders** tool. In the Shared Folders tool, choose **Shares** in the left pane to see the active shares for your file system. Choose **New Share** and complete the **Create a Shared Folder** wizard to create a new file share.

### Mount file system to a Linux EC2 instance

1. Connect to your Linux instance. For more information, see [Connecting to your Linux instance using Session Manager](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/session-manager.html) in the Amazon Elastic Compute Cloud User Guide for Linux Instances.

2. Run the following command to install the cifs-util package: `sudo yum install cifs-utils`

3. Mount your file share with the following command. You need to include the DNS name for your file system and the credentials for your Active Directory admin user. The system will also prompt you for your password.  

```
sudo mount –t cifs //file_system_dns_name/share /mnt/fsx -o vers=2.0, user=admin@file_system_dns_name
```

Optionally, navigate to the /mnt/fsx directory and list the directory contents. You should see the files you created from your Windows EC2 instance.
