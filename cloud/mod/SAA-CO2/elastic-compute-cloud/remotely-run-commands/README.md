# Remotely Run Commands on an EC2 Instance

In our example scenario, as a System Administrator, you need to update the packages on your EC2 instances. To complicate this normally simple admin task, your security team does not allow you to direct access production servers via SSH or allow you use bastion hosts. Fortunately, you can use Systems Manager to remotely run commands, like update packages, on your EC2 instances.

To solve this challenging scenario, you will create an Identity and Access Management (IAM) role, enable an agent on your instance that communicates with Systems Manager, then follow best practices by running the AWS-UpdateSSMAgent document to upgrade your Systems Manager Agent, and finally use Systems Manager to run a command on your instance.

## Step 1. Create an Identity and Access Management (IAM) role

In this step, you will create an IAM role that will be used to give Systems Manager permission to perform actions on your instances.

a. Open the IAM console.

b. In the left navigation pane, choose **Roles**, and then choose **Create role**.

c. On the **Select type of trusted entity** page, under **AWS Service**, choose **EC2**, and then choose **Next: Permissions**.

d. On the **Attached permissions policy** page, in the search bar type `AmazonEC2RoleforSSM` then from the policy list select **AmazonEC2RoleforSSM**, and then choose **Next: Review**.

e. On the **Review** page, in the **Role name** box type in `EnablesEC2ToAccessSystemsManagerRole`. In the **Role description** box, type in `Enables an EC2 instance to access Systems Manager`. Choose **Create role**.

## Step 2. Create an EC2 instance

In this step, you will create an EC2 instance using the `EnablesEC2ToAccessSystemsManagerRole` role. This will allow the EC2 instance to be managed by the Systems Manager.

a. Open the **Amazon EC2 console**. From the EC2 console select your preferred **region**. Systems Manager is supported in all AWS Regions. Now choose **Launch Instance**.

b. Select the **Amazon Linux AMI**. Make sure you select Amazon Linux base AMI dated 2017.09 or later which includes the Systems Manager Agent by default. You can also install the Systems Manager Agent on your own Windows or Linux system.

c. On the **Step 2: Choose an Instance Type** page, choose the t2.micro instance type and then click **Next: Configure Instance Details**.

d. On the **Step 3: Configure Instance Details** page, in the **IAM role** dropdown choose the `EnablesEC2ToAccessSystemsManagerRole` role you created earlier. Leave everything else as default. Choose **Review and Launch**.

e. On the Step 7: Review Instance Launch page, choose **Launch** to launch your instance.

f. Next the **Select an existing keypair or create a new key pair** dialog will appear. You will not need a keypair to use Systems Manager to remotely run commands. From the **Choose an existing pair** dropdown, choose **Proceed w/o a key pair** and tick the **I acknowledge that...** checkbox.

Next select **Launch Instance**.

## Step 3. Update the Systems Manager Agent

Now that you have an EC2 instance running the Systems Manager agent, you can automate administration tasks and manage the instance. In this step, you run a pre-packaged command, called a document, that will upgrade the agent. It is best practice to update the Systems Manager Agent when you create a new instance.

a. In the op menu, click on **Services**. Then, under Management Tools, select **Systems Manager** to open the Systems Manager console.

b. Under the **Shared Resources** section on the left navigation bar, choose **Managed Instances**.

c. On the Managed instances page, in the **Actions** drop down select **Run Command**.

d. On the **Run a command** page, click in the search bar and select, **Document name prefix**, then click on **Equal**, then type in `AWS-UpdateSSMAgent`.

Now click on the radio button on the left of **AWS-UpdateSSMAgent**. This document will upgrade Systems Management agent on the instance.

Scroll down to the **Targets** panel and click the check box next to your managed EC2 instance.

Finally, scroll down and select **Run**.

e. Next, you will see page documenting your running command then and overall success in green. Congrats, you have just run your first remote command using Systems Manager.

## Step 4. Run a Remote Shell Script

Now that your EC2 instance has the latest Systems Manager Agent, you can upgrade the packages on the EC2 instance. In this step, you will run a shell script through Run Command.

a. From the **Systems Management** console, in the left nav under **Shared Resources**, select **Manged instances**. Then in the **Actions** menu, select the **Run Command** menu item.

b. On the **Run a command** page, click in the search bar and select, **Document name prefix**, then click on **Equal**, then type in `AWS-RunShellScript`.

Now, click on the radio button on the left of **AWS-RunShellScript**. This document will upgrade Systems Management agent on the instance.

Scroll down to the **Targets** panel and click the check box next to your managed EC2 instance.

c. Scroll down to the **Command Parameters** panel and insert the following command in the **Commands** text box:

```
sudo yum update -y
```

Finally, scroll down and select **Run**.

d. While your script is running remotely on the managed EC2 instance, the **Overall status** will be **In Progress**. Soon the **Overall status** will turn to Success. When it does, scroll down to the **Targets and outputs** panel and click on the Instance ID of your instance. Your Instance ID will be different than the one pictured.

e. From the **Output on: i-XX** page click on the header of the **Step 1 - Output** panel to view the output of the update command from the instance.

## Step 5. Terminate your Resources

In this step, you will terminate your Systems Manager and EC2 related resources.

> [!IMPORTANT]
> Terminating resources that are not actively being used reduces cost and is best practice. Not terminating your resources can result in a charge.

a. Open the Amazon EC2 console and from the left nav under the **Instances** heading select **Instances**.

b. Select your instance's checkbox and click **Actions**, then **Instance State**, then **Terminate**. This will terminate your instance completely.

## Learning Summary

You have successfully created a managed instance and remotely run a command using AWS Systems Manager. You first set up the correct permissions through IAM. Next you launched an Amazon Linux instance that was pre-installed w/ the Systems Manager agent. Finally, you used Run Command to update the agent and remotely perform a yum update.

Systems Manager is a good choice when you need to view operation data for groups of resources, automate operational actions, understand and control the current state of your resources, manage hybrid environments, and maintain security and compliance.
