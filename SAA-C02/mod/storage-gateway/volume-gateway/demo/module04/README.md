# EBS Snapshots

In this module you will create a file system, add some data and then snapshot the volume. You will then clone this snapshot into a new EBS volume and present it directly to an EC2 instance.

## Creating a file system

1. From the **AWS console**, click **Services** and select **EC2**.

2. Select **Instances** from the menu on the left.

3. Right-click on the **EC2 Initiator Instance** and select **Connect** from the menu.

4. From the dialog box, select the **Session Manager** option.

5. Click **Connect**. A new tab will be opened in your browser with a command line interface (CLI) to the EC2 Initiator Instance.

6. Run the following command on the EC2 Initiator Instance to list disks attached:

```zsh
`ls -l /dev/disk/by-path`
```

You should get an output similar to the below where the iSCSI disk has been mounted to **sda**.

7. To confirm there is no filesystem already on the disk run the following command, where device name is taken from the previous output.

```zsh
`sudo file -s /dev/<DEVICENAME>`
```

8. Next we will run a series of commands to create a file system on the disk using **mkfs**. Starting with the below we can install mkfs:

```zsh
`sudo yum install xfsprogs -y`
```

9. Then run the the mkfs command to create the file system. Note this may take a few seconds to complete.

```zsh
`sudo mkfs -t xfs /dev/<DEVICENAME>`
```

10. We are then going to mount this new file system under data1.

```zsh
`sudo mkdir /data1`

`sudo mount /dev/<DEVICENAME> /data1`
```

11. Next we are going to create 5*1GB files, this may take a few minutes:

```zsh
`for i in {1..5}; do sudo dd if=/dev/urandom bs=1048576  count=1024 of=/data1/file$i; done`
```

12. This can be confirmed with the following two commands. To see the list of files: `ls -l /data1`

...and to see the amount of data used on the disk: `df -h`

## Creating a snapshot and a volume

1. We now need to monitor the upload buffer on the Volume Gateway to confirm that all the data has been uploaded before we create a snapshot.

2. From the AWS console, click **Services**, under Storage select **Storage Gateway**.

3. On the left panel click **Gateways** and select your Volume Gateway. The **Upload buffer usage** will be displayed and it can take up to 5 minutes for this to reach 0% used, you can keep hitting refresh to check.

4. On the left panel click **Volumes** and then select the volume you created earlier.

5. Click **Actions**

6. **Create EBS snapshot**

7. Enter in the description **Clone for EBS Volume** and click **Create EBS Snapshot**.

8. You should see a green box with a hyperlink to the snapshot, click on the hyperlink **snap-xxxxxxx**.

9. You will now see the progress of your snapshot. This may take at least 10 minutes to complete, you can hit refresh to check.

10. Once completed, click **Actions** ▶︎ **Create Volume** (you could also create an image if this was an iSCSI boot disk to create an AMI) and enter the following details:

  * Volume Type **io1**

  * Size **500 GB**

  * IOPS **25,000**

11. Click **Create Volume**.

12. Click on **Volumes** just above the **Snapshots** option.

13. Locate and select the volume we just cloned and click **Actions** ▶︎ **Attach Volume**

14. Select the **EC2 Initiator Instance** and it should auto populate the next Device ID, note it down. In this instance it is **/dev/sdf**. Click **Attach**.

## Mount and prep volumes

1. From the **AWS console**, click **Services** and select **EC2**.

2. Select **Instances** from the menu on the left.

3. Right-click on the **EC2 Initiator Instance** and select **Connect** from the menu.

4. From the dialog box, select the **Session Manager** option

5. Click **Connect**. A new tab will be opened in your browser with a command line interface (CLI) to the EC2 Initiator Instance.

6. Run the command: `lsblk`. Note the device of the 500GB volume we have attached. Providing no other EBS volumes have been attached, it will be **nvme[0-26]n1**. We can also see previous 200GB volume as sda, take a note of this.

> [!NOTE]
>
> The device names that you specify for NVMe EBS volumes in a block device mapping are renamed using NVMe device names (/dev/nvme[0-26]n1). The block device driver can assign NVMe device names in a different order than you specified for the volumes in the block device mapping.

7. We are going to unmount the 200GB Volume Gateway Volume under data1 and mount the 500GB EBS Volume. To start, run the following commands:

```zsh
sudo umount /data1

sudo mount /dev/<devicename> /data1
```

8. There should be the 1GB files pre-populated on the volume. This can be confirmed with the following two commands. To see the list of files: `ls -l /data1`. To check the amount of data used on /dev/[devicename] `df -h`.

> [!NOTE]
>
> The disk partition has not been expanded on **nvme[0-26]n1** / the snapshot clone volume, so this will still show as 200GB in size.

## Performance tests on the EBS volume

1. From the **AWS console**, click **Services** and select **EC2**.

2. Select **Instances** from the menu on the left.

3. Right-click on the **EC2 Initiator Instance** and select **Connect** from the menu.

4. From the dialog box, select the **Session Manager** option

5. Click **Connect**. A new tab will be opened in your browser with a command line interface (CLI) to the EC2 Initiator Instance.

6. Run the command: `cd /home/ssm-user/fio`

7. Edit the **sequentialread.fio** text file: `nano sequentialread.fio`. replace **<devicename>** with the 500GB EBS volume from the previous module e.g. nvme1n1 `filename=/dev/<devicename>`

**Save** and **exit** the nano editor.

8. Run the following command: `sudo fio sequentialread.fio`. This test will run for 2 minutes and provide a summary.

9. We get an unexpected result of around **2000 IOPs**. This is due to us hitting the bandwidth limit of the c5 instance. To prove this we are going to change the block size. Edit the sequentialread.fio text file as follows:

```zsh
nano sequentialread.fio
```

...and replace **bs=256K** with **bs=16K**

```zsh
bs=16K
```

10. **Save and exit** and then run the following command: `sudo fio sequentialread.fio`. This test will run for 2 minutes and provide a summary.

11. We hit **20,000 IOPs** due to the limit of the c5 instance type.

12. To prove this we are going to change the EC2 instance type. Navigate to the **EC2 console**.

13. Select **Instances** from the menu on the left.

14. In the top panel, right-click on the **EC2 Initiator Instance** and select **Instance State** ▶︎ **Stop** from the menu.

15. Wait for the instance to stop, and then right-click on the **EC2 Initiator Instance** and select **Instance Settings** ▶︎ **Change Instance Type** from the menu.

16. Select **c5n.9xlarge** and click **Apply**. Notice the **EBS-optimized** tick box is ticked as it is enabled by default for this instance type.

17. Right-click on the **EC2 Initiator Instance** and select **Instance State** ▶︎ **Start** from the menu.

18. Wait for the instance to start.

19. In the top panel then right-click on the **EC2 Initiator Instance** and select **Connect** from the menu.

20. From the dialog box, select the **Session Manager** option

21. Click **Connect**. A new tab will be opened in your browser with a command line interface (CLI) to the **EC2 Initiator Instance**.

22. Run the following command: `sudo fio /home/ssm-user/fio/sequentialread.fio`

This test will run for 2 minutes and provide a summary similar to below:

23. We can see we now reach 25,000 IOPs which was the limit we set on the volume (Module 4.2, Create a snapshot, step 10.)

## Learning Summary

In this module you have created an EBS volume from a snapshot of the Volume Gateway volume. This can be useful for migration, testing or even disaster recovery. You then continued to run performance tests on the EBS volume and understand the limits imposed by both the instance type and the volume.

Now all that's left is to tidy up the environment in Module 5.
