# Demonstration: Deployment of the Tape Gateway

In this walk-through, Storage Gateway is used to create a hybrid storage solution. Tape Gateway is deployed on an Amazon EC2 instance that will function as a VTL.

## Step 1: EC2 instance setup

Choose an instance type. It is recommended to select at least an **m5.xlarge** for the gateway appliance.

Choose **Next** to configure instance details. Locate the network setting and select the **VPC** that you want your EC2 instance to run in. Optionally, select a subnet. For auto assigned public IP, select **Enable**.

Next, add storage. Choose **Add New Volume** to add storage to your gateway instance. For this deployment of Tape Gateway, you will need at least two Amazon EBS volumes in addition to the root volume. One Amazon EBS volume for the local cache store. The second Amazon EBS volume for the upload buffer. Both will need a minimum size of 150 gibibytes. 

Configure volume type, IOPS, and throughput based on your use case. 

Optionally, add tags to your EC2 instance.

Next, configure security groups to add firewall rules to allow clients to connect to your EC2 instance. Tape Gateway requires **HTTP** and **iSCSI** access. Configure the source for this access. 

Next, review and launch your EC2 instance. Notice that the Storage Gateway AMI defaulted to create the instance. Review your settings and then choose **Launch**.

Navigate to the EC2 Instances page. Confirm that your new instance is running. Locate the details tab. Copy the **IP address** of the new EC2 instance.

Return to the Storage Gateway wizard to proceed.  To continue Step 1 of the gateway setup, expand the following two settings.

> ### Gateway appliance confirmation
>
> Confirm you have completed the gateway appliance set up.

> ### Backup application settings
>
> Next, choose your **backup application** from the supported list. If your backup application is not listed, choose **Other**. Then choose, **Next** to review your settings.

## Step 2: Set up gateway

The first step to setting up your gateway is to deploy the gateway appliance, on premises or on AWS.  In this walk-through, you launch and configure a cloud-based software appliance on Amazon EC2 to host your gateway appliance.

From within the Storage Gateway console, select **Create gateway**. This will start the wizard that will guide you through the setup process. The wizard will take you through four main steps to set up and configure your new Tape Gateway.

> ### How it works
>
> Confirm you are in the desired Region for your gateway setup and then continue to the Gateway settings section.

> ### Gateway settings
>
> First, you will give your gateway a name. Here, we are creating a new gateway with the name `tapedemo1`.

> ### Gateway options
>
> Choose your desired gateway type. In this walk-through, you create a *Tape gateway*.

> ### Platform options and instructions
>
> Next, select the host platform. Setup instructions to deploy the host will display.
>
> * Choose the host platform **Amazon EC2**.
>
> * Choose **Launch instance**.
>
> At this time, set up the new EC2 instance to host your gateway appliance following the provided instructions. The AMI for Storage Gateway will default when you choose **Launch instance** from the setup wizard.
>
> You will return back to this wizard after the instance is running to confirm and continue with the gateway setup.

>[!IMPORTANT]
>
> When deploying to VMware, Microsoft Hyper-V, and Linux KVM, you must synchronize the virtual machine's time with the host time before you can successfully activate your gateway. Make sure that your host clock is set to the correct time and synchronize it with an NTP server.

## Step 3: Connect to AWS

You successfully deployed an Amazon EC2 instance using the Storage Gateway AMI in Step 1 for the gateway appliance.

In Step 3, you connect your gateway appliance to the Storage Gateway service. Service endpoints, connection options, and IP address of the appliance are needed for the connection.

> ### Service endpoint
>
> Choose how your gateway appliance will connect to Storage Gateway. Available options are **Publicly accessible** or **VPC hosted**.
>
> For this walk-through, choose **publicly accessible**.

> ### FIPS enabled
>
> If your connection should comply w/ FIPS, choose the option for **FIPS enabled endpoint**.
>
> The FIPS service endpoint is only available in some AWS Regions.

> ### Connection options
>
> For **Connections options**, choose **IP address**. If the IP is not available for the gateway appliance, you can use the **Activation key**.

> ### IP address
>
> Enter the IP address for the gateway appliance.

## Step 4: Review and activate

Review and confirm the new gateway settings you entered in Step 3. Choose **Edit** for either step if you need to make any changes. Once you are ready to activate, choose **Next**.

Step 3 completes the activation phase of the gateway setup.

> ### Gateway details
>
> Confirm the **Gateway options** setup details. Choose the **Edit** button if you need to return and make necessary changes.

> ### Connection settings
>
> Review all **Connection settings** and edit, if needed.

## Step 5: Configure gateway

In Step 5 of the gateway setup, you enter configuration settings for the cache and upload buffer.  

CloudWatch log group, CloudWatch alarms, and optional tags can be configured at this time.

Choose each of the following four tabs to review the Gateway configuration settings.

> ### Cache and Upload Buffer
>
> Best practice is to allocate at least one local disk for each of the following:
>
> * Cache storage – a minimum of 150 GiB
>
> * Upload buffer storage – a minimum of 150 GiB

> ### CloudWatch log group
>
> Choose a **CloudWatch log group** option for monitoring the health of your gateway.

> ### CloudWatch Alarms
>
> Set up **CloudWatch alarms** to be notified when specified metrics vary from their ideal settings.

> ### Tags
>
> Add optional tags if desired to categorize your gateway.

Your new Tape Gateway is now successfully created. Details including status, alarm state, and gateway type are displayed from the Storage Gateway console.

The Tape Gateway deployment continues by performing the following tasks:

* Create a new tape associated with your Tape Gateway.

* Specify type, capacity, barcode prefix, and pool for your tape.

* Mount the newly created tape drive on a client specifying the iSCSI configuration.
