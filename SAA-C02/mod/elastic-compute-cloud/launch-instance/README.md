# Launch our first EC2 instance

## Objective

* Provision our very first EC2 instance

* Turn EC2 instance into a web server

## Getting started

* Go to **Services** ▶︎ **Compute** ▶︎ **EC2**

* In the **EC2** console, click on `Launch instance`

* `Step 1: Choose an Amazon Machine Image (AMI)`

    * An Amazon machine image is a virtual machine

* `Select` Amazon Linux 2 AMI (HVM), SSD Volume Type

* Check `t2.micro` Free tier eligible

* Check `Protect against accidental termination` ▶︎ **Next:Add Storage**

    * Stops us from accidentally deleting our EC2 instances

    * **Tenancy** provides options of shared hardware, a dedicated instance, or a dedicated host

    * Within **Advanced Details**, we can pass bootstrap scripts

* Launch the **Root** device volume

    * EBS volume types: `General Purpose SSD (gp2)`, `Provisioned IOPS SSD (io1)`, `Magnetic (standard)`

    * If we `Add New Volume`, more options appear since this is not the **Root** device volume. The **Root** device volume can only launch on SSD or Magnetic (standard), but you can have additional volumes and additional volumes include `General Purpose SSD (gp2)`, `Provisioned IOPS SSD (io1)`, `Cold HDD (sc1)`, `Throughput Optimized HDD (st1)`, and `Magnetic (standard)`

    * When we refer to **Root** device volume or EBS, all we're referring to is a virtual disk on the cloud, where our operating system is going to be installed.

* Add tags

    * `Name`: `WebServer01`

    * `Department`: `Developers`

    * `EmployeeID`: `323432`

    * The tags are a method of keeping track of our EC2 and our AWS infrastructure
    
    
* Configure Security Group

    * Security group is a virtual firewall, allows communication over particular ports. This is how you enable traffic on the various different ports. So HTTP goes across port 80. SSH goes across port 22. So you're just allowing various different types of communication to your EC2 instance through your security group.


* Set `Security group name:` to `WebDMZ`. This is the security group that all my web servers are going to go into

* Add a role, allowing port 80, so HTTP, and set **Source** to 0.0.0.0/0, ::/0, a CIDR address range. Essentially when these are all zeros, you're opening the port up to the whole world.

* Hit **Review and Launch** ▶︎ **Launch**

* Create a key pair

    * Think of public key and private keys → asymmetric encryption. So it means you need two → asymmetric. W/ symmetric encryption it's just one. So your public key will go on your EC2 instance. The private key will save locally on your machine and you will use that private key to be able to SSH into your EC2 instance.

* Change permissions to lock key down

```zsh
mkdir SSH
mv MyUSE1KP.pem SSH
CHMOD 400 MyUSE1KP.pem
```

* Connect to EC2 instance

```zsh
ssh ec2-user@54.146.34.68 -i MyUSE1KP.pem
```

* Elevate privileges to Root, changing the user

```zsh
sudo su
```

* Apply some operating system updates:

```zsh
yum update -y
```

* Install Apache

```script
yum install httpd -y
```

    * Apache turns our EC2 instance into a web server

```script
cd /var/www/html
```

    * Anything that you put in here will be basically a website, will be accessible over port 18


    * As soon as we turn on the Apache service or the httpd service, this web server will become a web server and anyone will be able to go to our `index.HTML`

* Turn it on: 

```script
service httpd start
chkconfig on
```

    * `chkconfig` will start the httpd service if your EC2 instance accidentally reboots, so you don't need to go in and manually go in and turn it on at the next reboot.

## Learning summary

* Termination Protection is **turned off** by default, you must turn it on

* On an EBS-backed instance, the **default action is for the root EBS volume to be deleted** when the instance is terminated.

    * But any additional volumes, by default, won't be deleted 

* EBS Root Volumes of your DEFAULT AMI's **CAN** be encrypted. You can also use a third party tool (such as bit locker etc) to encrypt the root volume, or this can be done when creating AMI's in the AWS console or using the API.

* Additional volumes can be encrypted
