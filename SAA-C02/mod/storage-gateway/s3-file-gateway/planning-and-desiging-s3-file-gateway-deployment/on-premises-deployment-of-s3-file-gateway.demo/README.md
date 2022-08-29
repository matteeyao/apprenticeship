# Demonstration: On-Premises Deployment of the Amazon S3 File Gateway

In this example, Storage Gateway is used to create a hybrid storage solution. S3 File Gateway is deployed on premises in a VMware environment and a file share is added to it.

The demonstration follows this basic outline:

1. Download and set up the VM.

2. Activate the gateway.

3. Create resources.

4. Connect to the file share.

## How to deploy an Amazon S3 File Gateway into an existing vSphere environment

Using AWS Storage Gateway, your on-premises applications can access data stored on the cloud using standard storage protocols. Data is cached locally and moved to the cloud w/ optimized data transfers.

Follow along to learn hot to deploy an Amazon S3 File Gateway into an existing VMWare vSphere environment. In this example, we will be joining a Windows Active Directory domain to give authenticated users the same experience that they have on premises. We will create a Server Message Block (SMB) file share on an S3 File Gateway and mount it on a Windows client.

### 1. Download and set up the VM

Let's get started by downloading and setting up the virtual machine (VM), which is also referred as the S3 File Gateway appliance, or simple, the gateway.

From within the Storage Gateway service, choose **Create gateway**. This will start the wizard that will guide you through the process. The wizard will take you through these key phases:

1. Create gateway - Activation ▶︎ Set up your gateway and connect it to Amazon Web Services (AWS) to activate it.

2. Create gateway - Configuration ▶︎ Configure your activated gateway so that you can start using it.

3. Create resource ▶︎ After creating your gateway through this wizard, create storage resources on your gateway to start storing and accessing your data in Storage Gateway.

4. After you complete the wizard steps, you will need to connect the file share by mapping it to a new network drive on the Windows client.

You are in the activation phase where you set up your gateway and connect it to AWS to activate it.

* From within the wizard, give your gateway a name. Here, the gateway is named `FileGatewayDemo`.

* Choose the Gateway type of **Amazon S3 File Gateway** b/c we will be storing files in Amazon Simple Storage Service (Amazon S3).

* Choose the host platform **VMware ESXi**

* Download the Open Virtualization Format (OVF) template.

At this time, you need to set up the OVF template that you download. You will return to the wizard after you deploy the VM image to vSphere.

You are logged in to your vSphere client. Here you will deploy the OVF template that you downloaded from the Storage Gateway console.

* To do this, choose (right-click) **Datacenter** and choose **Deploy OVF Template**.

Run through the deployment wizard. Basically, the wizard runs you through these steps:

1. Select and upload the OVF file that you downloaded from AWS.

2. Give the VM a name-for example, `FileGatewayDemo`-and folder.

3. Select a compute resource; for example, cluster.

4. Review the template details.

5. Select storage.

6. Select networks.

7. Finish the deployment.

By deploying the VM, you have deployed the S3 File Gateway appliance. After it is deployed, you need to add a virtual hard disk to the VM to be used as a local cache.

* To edit the VM settings, in the navigation pane, select the VM, and then choose **Actions** ▶︎ **Edit** ▶︎ **Setting**.

* Select **Add New Device** ▶︎ **Hard Disk**.

* In the New Hard disk field, enter the size for the disk. For this demo, the minimum of **150 GB** is entered. Choose **OK**.

* Finally, choose **Edit Settings** ▶︎ **Options** ▶︎ **VMware Tools**. Under **Advanced**, select the **Synchronize quest time with the host** option and choose **OK**.

Power on the VM. Take note of the IP address of the gateway VM. You use this IP address to activate your gateway.

### 2. Activate the gateway

Back in the Storage Gateway console, confirm that the OVF template was downloaded and deployed.

The activation process associates your gateway w/ your AWS account. Your gateway VM must be running for activation to succeed.

* Select the **Service endpoint** type that controls how the service will communicate w/ AWS.

* Enter the local IP address of the gateway VM.

* Choose **Next**.

* Review the gateway details, and then, to activate the gateway, select **Next**.

When activated, finalize the setup by configuring the local cache disks that were added to the VM earlier.

Then, configure logging. For the demo, an existing Amazon CloudWatch log group is selected. Choose **Save** and **Continue**.

Finally, b/c this is VMware, select **Verify VM** and continue to verify that VMWare high availability is working as expected.

### 3. Create resources - Adding a file share

After creating your gateway through this wizard, create storage resources on your gateway so that you can start storing and accessing your data in AWS. The resource is a file share A gateway can support up to 10 file shares.

Now that the initial setup fo the gateway is complete, let's create an SMB share. Each share will use an S3 bucket, which is where the files are stored in the cloud. A share can be either Network File System (NFS) or SMB. For this demo, an existing S3 bucket is selected, and an SMB share is created by ensuring that **Service Message Block (SMB)** is selected.

Choose **Next**.

Accept the default Amazon S3 storage setting, and then provide an AWS Identity and Access Management (IAM) role for access to your S3 bucket. Choose **Next** again.

To join the Active Directory domain, enter the domain information and choose **Next**.

After the domain join has completed, review the configuration. When ready, choose **Create**.

Now the file share is created and associated w/ an S3 bucket. You can select the share and view its details.

The Storage Gateway console gives an example command that you can use to connect to the file share. Take note of the host and path information.

### 4. Connect to the file share

To use this file share, you need to map a new network drive on the Windows client.

As you can see, it is just like any other SMB file share. But b/c this is backed by the AWS Cloud, you have virtually unlimited storage space.

In Windows Explorer, notice that there is nearly 8 EB of free space on this share. The S3 File Gateway handles encryption and optimizes data transfer between the gateway and the cloud.

## Learning summary

In this example, you saw how to deploy the S3 File Gateway type of Storage Gateway on an on-premises VMware environment. Other deployment options are also available. These include Microsoft Hyper-V, Linux Kernel-based Virtual Machine (KVM), Amazon Elastic Compute Cloud (Amazon EC2), and a hardware appliance in your data center.
