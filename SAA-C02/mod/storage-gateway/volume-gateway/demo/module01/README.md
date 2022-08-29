# Module 1 - Deploy EC2 Initiator Instance resources

In this module you will use AWS CloudFormation to deploy the resources needed for the workshop in a single region.

The CloudFormation template will create the following resources:

* A public subnet with an internet gateway.

* The EC2 Initiator Instance with a no ingress security group.

* SSM Session Manager roles to connect to the EC2 Initiator Instance.

* A Security Group for the Volume Gateway.

## Deploy CloudFormation Template

### Deploy AWS resources

Create stack using AWS CloudFormation Template. Wait for the CloudFormation stack to reach the **CREATE_COMPLETE** state before proceeding to the next steps. It should take about 5 minutes for the CloudFormation stack to complete. You can refresh the Events section to see the progress.

> [!NOTE]
>
> If a stack fails to deploy because an EC2 Instance type is not available in a particular availability zone, delete the stack and retry in the same region or in a different region.

### Stack Outputs

Upon completion, each CloudFormation stack will have a list of **Outputs**. This will contain the link you can use to connect via SSM Session Manager to the iSCSI EC2 Initiator Instance. You can either copy these values elsewhere or keep the page open in your browser and refer to them as you go through the workshop.

From the same **Outputs** window you will now need to update the **Security Group** values to your IP.

### Update EC2 Security Group to your IP

1. Click on the **Value** (URL) associated with the Key **SecurityGroupUrl**.

2. On the **Inbound rules** tab, choose **Edit Inbound rules**.

  1. Choose **Add rule**

  2. For Type, Choose **HTTP**

  3. For source, choose **My IP** to automatically populate the field w/ the public IPv4 address of your local computer.

  4. Alternatively, for Source, choose Custom and enter the public IPv4 address of your computer or network in CIDR notation. For example, if your IPv4 address is 203.0.113.25, enter 203.0.113.25/32 to list this single IPv4 address in CIDR notation. If your company allocates addresses from a range, enter the entire range, such as 203.0.113.0/24.

3. Click **Save rules**.

## Connect to the EC2 Initiator Instance

### Connect to EC2 Initiator Instance

1. Either **click on the URL for the Session Manger** from the previous CloudFormation Outputs

...or...

2. From the AWS console, click **Services** and select **EC2**.

3. Select **Instances** from the menu on the left.

4. Right-click on the **EC2 Initiator Instance** and select **Connect** from the menu.

5. Click **Connect**. A new tab will be opened in your browser w/ a command line interface (CLI) to the Application server.

6. Run the following commands on the EC2 Initiator Instance:

```zsh
sudo yum update -y

sudo yum install iscsi-initiator-utils -y

sudo yum install fio -y

mkdir /home/ssm-user/fio

cat /etc/iscsi/initiatorname.iscsi
```

Make a note of the output from **"cat /etc/iscsi/initiatorname.iscsi"**. This will be needed in **Module 2**.

In this example the Initiator Name is: **iqn.1994-05.com.redhat:9e341b192e**

**Module Summary**

In this module you deployed the EC2 Initiator Instance, installed iSCSI utilities and installed FIO which will be used to perform load testing on the iSCSI volume.

In the next module, you will create the Volume Gateway and connect the volume to the EC2 Initiator Instance.
