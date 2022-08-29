# Instance Metadata

1. `SSH` into our EC2 instance

```script
ssh ec2-user@<PUBLIC_IPV4_ADDRESS> -i MyUSE1KP.pem
```

2. In the EC2 instance, elevate privileges to root:

```script
sudo su
```

3. To view the bootstrap script that launched w/ this EC2 instance:

```script
curl http://169.254.169.254/latest/user-data
```

Execute the following command to output the metadata to `bootstrap.txt`

```script
curl http://169.254.169.254/latest/user-data > bootstrap.txt
```

View the file w/ this command:

```script
cat bootstrap.txt
```

4. To get your EC2 instance information:

```script
curl http://169.254.169.254/latest/meta-data/
```

To view local-ipv4 address:

```script
curl http://169.254.169.254/latest/meta-data/local-ipv4
```

To view public-ipv4 address:

```script
curl http://169.254.169.254/latest/meta-data/public-ipv4
```

Likewise you can output this information to a text file:

```script
curl http://169.254.169.254/latest/meta-data/public-ipv4 > myip.txt
```

## Learning summary

* Used to get information about an instance (such as public ip)

```script
curl http://169.254.169.254/latest/meta-data/
```

```script
curl http://169.254.169.254/latest/user-data/
```
