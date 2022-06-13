# Bootstrap Scripts

> * Bootstrap scripts run when an EC2 instance first boots.
>
> * Can be a powerful way of automating software installs and updates.

Bootstrap scripts automate AWS EC2 deployment. Runs through the command line when your AWS EC2 instance first boots. You run individual commands - updates, installations, anything you can do in the command line and it will run as a script as that EC2 instance first boots up

`#!/bin/bash` is the path to our interpreter, which takes the commands and runs them at the root level when we first provision our EC2 instance.

The following bashscript:

```script
#!/bin/bash
yum update -y
yum install httpd -y
service httpd start
chkconfig httpd on
cd /var/www/html
echo "<html><h1>Hello Cloud Gurus Welcome To My Webpage</h1></html>" > index.html
aws s3 mb s3://aws-cli-ec2-bootstrap-script-demo
aws s3 cp index.html s3://aws-cli-ec2-bootstrap-script-demo
```

* Performs an update

* Install Apache

* Start the Apache service

* `chkconfig httpd on` ensures that the Apache service turns on if there is a reboot

* Change directories to `/var/www/html` dir

* Create a little webpage

* And then it's going to create an S3 bucket

* And then move the contents of our web page over to S3 as well, essentially performing a backup
