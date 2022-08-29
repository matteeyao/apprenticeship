# Creating a Basic VPC in AWS

## Learning objectives

[ ] Create CPV

[ ] Create Subnets

[ ] Create Routes and Internet Gateway

[ ] Launch instances in Subnet

[ ] Add Network ACL

> **About this lab**
>
> In this hands-on lab scenario, you're a cloud network engineer tasked w/ setting up the security and network architecture for your organization's production environment. You'll have the opportunity to explore and understand the relationship networking components. We will create a virtual private cloud (VPC), subnets across multiple availability zones (AZs), routes and an internet gateway, as well as adding security using security groups and network access control lists (NACLs). These services are the foundation of networking architecture inside of AWS and cover concepts such as infrastructure, design, routing, and security.

![Fig. 1 Configuring a Basic VPC in AWS](../../../../../img/SAA-CO2/virtual-private-cloud/virtual-private-cloud.demo/module01/diagram.png)

## Guide

> ### Create a VPC
>
> Navigate to the VPC
>
> [!NOTE]
> Do not use the VPC Wizard to create your VPC; instead configure your VPC from scratch.
>
> 1. Select **Your VPCs**.
>
> 2. Click **Create VPC**, and set the following values:
>
>   * HoLVPC
>
>   * 10.0.0.0/16
>
>   * No IPv6 CIDR block
>
>   * Default Tenancy
>
> 3. Click **Create.**

> ### Create Subnets
>
> Build two subnets for your VPC. One will be public to allow access from the Internet and one will be private. Ensure you are assigning the valid CIDR blocks when creating your subnets.
>
> **Create Public Subnet**
>
> 1. Select **Subnets**.
>
> 2. Click **Create subnet**.
>
> 3. Enter the following values:
>
>   * Name: "sn-public-a"
>
>   * VPC: "HoLVPC"
>
>   * Availability Zone: `us-east-1a`
>
>   * IPv4 CIDR Block: 10.0.1.0/24
>
> [!NOTE]
> Although the name of our subnet is "hol-public-a", it is not actually public just yet. By definition a public subnet must have an Internet Gateway. In the next tasks, we will add an Internet Gateway so that instances in this newly created public subnet can access the Internet.
>
> **Create Private Subnet**
>
> 1. Click **Create subnet**.
>
> 2. Enter the following values:
>
>   * Name:
>
>   * VPC: "HoLVPC"
>
>   * Availability Zone: `us-east-1a`
>
>   * IPv4 CIDR Block: 10.0.1.0/24
>
> [!NOTE]
> By default, all subnets are private. If there is no route to the Internet via an Internet Gateway, instances running in the subnet can only be reached by other instances in the VPC.

### Create Routes and Internet Gateway

> ### Auto-assign public IPv4 address**
>
> Automatically request a public IPv4 address for instances launched into the public subnet.
>
> 1. Select **Subnets**.
>
> 2. Select **sn-public-a**, **Actions**, and **Modify auto-assign IP settings**.
>
> 3. Enable **Enable auto-assign public IPv4 address**.

> ### Configure Internet Gateway**
>
> An internet gateway enables communication over the internet.
>
> 1. Select **Internet Gateways**, and click **Create internet gateway**.
>
> 2. Set the name tag as **hol-VPCIGW**, and click **Create internet gateway**.
>
> 3. Select the newly created IGW, click **Actions** and then **Attach to VPC**.
>
> 4. Select **HoLVPC** and click **Attach internet gateway**.

> ### Configure Routing
>
> Create a new route table for **HolVPC** to tell traffic in the public subnet, **sn-public-a**, how to get to the internet.
>
> [!NOTE]
> You may notice there is already a default route table created for you associated w/ your main network. This route allows traffic from the 10.0.0.0/16 network to pass to other nodes within the network, but it does not allow traffic to go outside of the network, such as, to the public internet. Each VPC you create by default is associated w/ this main route table; therefore, the main route table shouldn't allow traffic out to the public Internet so we'll create a new one specifically for public Internet traffic.
>
> 1. Create **Route Tables**.
>
> 2. Click **Create route table**.
>
> 3. Set the name tag as **publicRT** and the VPC as **HoLVPC**.
>
> 4. Click **Create**.
>
> 5. Click **Close**.
>
> 6. Select your newly created route table.
>
> 7. Click **Routes** tab.
>
> 8. Click **Edit routes**, and **Add route**.
>
> 9. Set the destination as **0.0.0.0/0**, target as **Internet Gateway**, and select **hol-VPCIGW**.
>
> 10. Click **Save routes**.
>
> 11. Click **Close**.

> ### Associate w/ Subnets
>
> 1. Select **publicRT**, and click the **Subnet Associations** tab.
>
> 2. Click **Edit subnet associations**.
>
> 3. Select **sn-public-a**.
>
> 4. Click **Save**.
>
> Great, now our public subnet will allow traffic within it to access the public Internet.

### Launch Instances in Subnet

> ### Launch EC2 Instance in Public Subnet
>
> 1. Navigate to the EC2 Dashboard
>
> 2. Select **Instances**
>
> 3. Select **Launch instances**
>
> 4. Choose Amazon Linux 2, check **64-bit (x86)**, and click **Select**
>
> 5. Choose **t2.micro**, and click **Next: Configure Instance Details**
>
> 6. Leave all as defaults, except set the subnet to **sn-public-a** and make sure *Auto-assign Public IP* is **Use subnet setting (Enable)**
>
> 7. Click **Next: Add Storage**
>
> 8. Click **Next: Add Tags**
>
> 9. Click **Next: Configure Security Group**
>
> 10. For security group, create a new one w/ the name and description **holpubSG**
>
> 11. Click **Review and Launch**
>
> 12. Click **Launch**, select to **Create a new key pair**, call it **vpcpubhol**, and click **Download Key Pair**
>
> 13. Click **Launch Instances** and then **View Instances**

> ### Launch EC2 Instance in Private Subnet
>
> 1. Navigate to the EC2 Dashboard
>
> 2. Select **Instances**
>
> 3. Select **Launch instances**
>
> 4. Choose Amazon Linux 2, check **64-bit (x86)**, and click **Select**
>
> 5. Choose **t2.micro**, and click **Next: Configure Instance Details**
>
> 6. Leave all as defaults, except set the subnet to **sn-private-b** and make sure *Auto-assign Public IP* is **Use subnet setting (Enable)**
>
> 7. Click **Next: Add Storage**
>
> 8. Click **Next: Add Tags**
>
> 9. Click **Next: Configure Security Group**
>
> 10. For security group, create a new one with the name and description **holprivSG**
>
> 11. Click **Review and Launch**
>
> 12. Click **Launch**, select to **Create a new key pair**, call it **vpcprivhol**, and click **Download Key Pair**
>
> 13. Click **Launch Instances** and then **View Instances**

> ### Access Instances
>
> After the state on both instances show as **Running** and has 2/2 status checks continue w/ these steps. You may have to refresh the screen to see the updated status.
>
> **SSH to public instance**
>
> Let's try to SSH into the public instance using it's Public IP.
>
> 1. Right click on the instance running in the public subnet.
>
> 2. Click **Connect**, select the SSH client tab, and copy the connection command.
>
> 3. Open your SSH client
>
> 4. Locate your private key file, **vpcpubhol.pem**, that you downloaded. This key file is used to launch this instance.
>
> 5. Run this command `chmod 400 vpchol.pem`, if necessary, to ensure your key is not publicly viewable.
>
>   * Linux/macOS users will need to run a `chmod 400 vpclab.pem` command first to avoid errors.
>
>   * Windows users can connext using this as a [guide](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/putty.html).
>
> 6. Connect using the copied connection string.
>
> 7. Answer **yes** to any prompts.
>
> **SSH to private instance**
>
> Now that we are inside the public instance we should be able to SSH to the private instance since by default instances within the same VPC can communciate w/ each other.
>
> Go to your SSH client and from the same tab that is logged into instance 1:
>
> 1. Create a private key as that will be used for the SSH connection. Type `vi vpcprivhol.pem` to open VIM and create a new blank .pem file. Press `i` to enter insert mode of VIM.
>
> 2. Copy the contents of your downloaded **vpcprivhol.pem** to the new .pem file. You can open the downloaded file using your favorite text editor and you can paste in the terminal using `Command + V`
>
> 3. Press Escape key to exit the insert mode and type `:wq` to save the file and quit
>
> 4. Run this command `chmod 400 vpcprivhol.pem`, if necessary, to ensure your key is not publicly viewable.
>
> 5. Type the necessary command to SSH to your instance (using the same steps as before) and answer **yes** to any prompts.
>
> 6. Wrap up by closing the connection to your public instance.
>
> Success! You're able to SSH to the private instance.

> ### Add Network ACL
>
> AWS provides two features that you can use to increase security in your VPC: security groups and network ACLs. Security groups control inbound and outbound traffic for your instances, and network ACLs control inbound and outbound traffic for your subnets. Remember that NACLs are stateless, so both inbound and outbound rules will be required.
>
> By default, all traffic is allowed via Network ACLs.
>
> If you want to block SSH for an entire subnet, you could add a DENY entry for TCP port 22 in the subnet's network ACL. Otherwise, you would need to block SSH on each instance's security group.
>
> 1. In the AWS console, navigate to **VPC** ▶︎ **Network ACLs**
>
> 2. Select the default NACL, and click **Edit inbound rules**
>
> 3. Click **Add Rule**, and set the following values:
>
>   * Rule #: **50**
>
>   * Type: **ALL Traffic**
>
>   * Source: Your IP address (which you can get by googling "what is my IP" in a new browser tab), and append **/32** at the end
>
>   * Allow/Deny: **DENY**
>
> 4. Click **Save**
>
> 5. In the terminal, try to log in to the public EC2 instance host.
>
> You should recieve an error.
>
> 6. In the AWS console, remove the NACL's rule #50 to remove the explicit DENY.
>
> 7. In the terminal, try connecting to the public EC2 instance.
>
> It should work this time.

## Notes

* When you create a subnet, specify the CIDR block for the subnet, which is a subnet of the VPC CIDR block. Each subnet resides within one availability zone and does not span multiple availability zones.

* By default, all subnets within a custom VPC are private w/ no internet access by default.

* Routing within the VPC defines the movement of traffic within subnets to and from the internet. Typically, there is 1 route table per VPC (the main or default route table), and it's automatically associated w/ all subnets in the VPC. The default route table is automatically associated w/ all subnets and this route table contains one route that allows traffic to pass to other nodes within this network, but it does not allow traffic to go outside of the network-to the public internet, for example.

* B/c all VPCs we create are automatically associated w/ the main route table by default, we'll have to create a route table for public internet traffic.

* An internet gateway enables communication over the internet.

* The public route table needs to be associated w/ the public subnet. Our public subnet will then allow traffic within it access to the public internet.

* By default, instances within the same VPC can communicate w/ each other.

* AWS provides two feature used to increase security in our VPC: security groups and network ACLs. Security groups control the inbound and outbound traffic for our instances and network ACLs control inbound and outbound traffic for our subnets.

* By default, all traffic is allowed via the network ACL. Use network ACLs to configure security on the subnet level.
