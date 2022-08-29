# Deploying a Basic Amazon VPC

## Starting with a basic Amazon VPC architecture

1. **Amazon VPCs live in the AWS Cloud**

Amazon VPCs are hosted entirely within the AWS Cloud, gaining all of the security, cost, performance, and availability benefits of the AWS Global Infrastructure.

2. **An Amazon VPC lives in one Region**

A single Amazon VPC can't live in more than one Region. Choose carefully: The Region in which you place your infrastructure impacts costs and, depending on where your end users live, latency. 

Also, check that the services your applications need are available in the Region you select. Some Regions (especially newer ones) don't have every AWS service available.

To check service availability by Region, see AWS Regional Services.

3. **A subnet can only live in one Availability Zone**

![Fig. 1 One Subnet = One AZ](../../../../img/SAA-CO2/virtual-private-cloud/barebones-vpc.demo/diag01.png)

While an Amazon VPC can span more than one Availability Zone within one Region, a subnet is restricted to one Availability Zone.

4. **Some AWS resources must be launched into an Amazon VPC**

Most AWS resources can be launched within an Amazon VPC. That means those resources live within a specific Region, just like their Amazon VPC does, and become unavailable if that Amazon VPC becomes unavailable for any reason.

5. **Internet gateways let your Amazon VPC resources reach the internet**

![Fig. 2 Internet Gateway](../../../../img/SAA-CO2/virtual-private-cloud/barebones-vpc.demo/diag02.png)

If you route a subnet to an internet gateway, that subnet becomes a public subnet. With the right configuration, resources within that subnet can then reach and be reached by the internet.

Internet gateways are:

* Horizontally scaled

* Redundant

* Highly available

This means that even though each Amazon VPC has a single internet gateway, this internet gateway is not a bottleneck nor a single point of failure.

6. **Route tables control the routing of traffic related to your Amazon VPCs**

![Fig. 3 Route tables](../../../../img/SAA-CO2/virtual-private-cloud/barebones-vpc.demo/diag03.png)

Route tables direct traffic to targets based on the IP address the traffic is seeking. Each Amazon VPC comes with its own route table called the *main route table*, which handles all traffic by default. By creating custom route tables and associating them with subnets, you can further customize how traffic is handled on a per-subnet basis.

In this example, the subnet is associated with a route table. The first row takes all traffic from that subnet, intended to remain within the VPC (`10.0.0.0/16`), and routes it within the Amazon VPC (local). The second row takes all traffic coming from that subnet and directs it to the internet gateway (igw-id). This association is what makes this subnet public.

However, because `10.0.0.0/16` is a more specific range than `0.0.0.0/0`, the route table knows to direct all of that traffic to local, overriding the route in the second row. When destinations overlap, the more specific destination IP range is the one that is carried out.

## Additional Amazon VPC considerations

* The only architectural difference between a *public* and *private* subnet is that a public subnet has a route to an internet gateway.

* By default, DNS is handled by Amazon VPC. It is possible, however, to use Amazon Route 53 to create your own DNS inside an Amazon VPC with private hosted zones.

* All traffic is unicast and Amazon VPCs do not require the Address Resolution Protocol (ARP).

* By default, all subnets in an Amazon VPC can access each other. You can use network network ACLs to restrict traffic into and out of your subnets.

* All traffic between two points in the same Amazon VPC is forwarded directly.

## Deploying a Simple VPC via the AWS Management Console Demo

First, open the AWS Management Console ▶︎ click under Find Services, and type **VPC**. It's important when opening a regionally scoped service like VPC that you be aware of what Region you're in. In the console, you can see that in the upper right-hand corner over here. So as you can see, we're in `us-west-2`, the Oregon Region. You have one default subnet in each default VPC in each Availability Zone in each Region. So you'll also see default route tables and a lot of other default VPC resources in every Region.

To create a VPC from scratch, go to **Your VPCs**. As mentioned before, there is already a default VPC in the region, `us-west-2`. And you can tell which one of your VPCs is the default VPC because there's this handy column here that indicates Default VPC.

You need to be careful when you're working with your VPCs because if you delete your default VPC, you can't recreate it without filing a ticket with Support first and getting their help. It's really easy just to select it and accidentally delete it, and maybe you miss the warning pop-up, and then you're out a default VPC for that Region until you get Customer Support to put it back. Now, you don't need a default VPC for anything, but it is handy to have when you just feel like launching some new resources to play with, and you don't feel like creating a whole new separate VPC just for them. It'll save you time and energy if you use it, but just be mindful that it's really only suitable for experimenting, or learning new services, or launching simple things like a blog or a simple website.

For the majority of our production environments, we should create a custom VPC for resources. Create VPC ▶︎ provide a name `DemoVPC` and a CIDR block for the VPC.

We'll make this a medium-sized VPC with a `/22` CIDR block with a private IP range. We're gonna start with the classic `10.0.0.0/22`.  This is going to reserve all the IPs from `10.0.0.0` through `10.0.3.255` for this VPC, and that spans a total of 1,024 IPs. Your CIDR block for an IPv4 VPC in the `10.0.0.0` to `10.255.255.255` range has to be from `/16` to `/28` in size. So `/16` would cover that entire range, and a `/28` would be the smallest you could have, and that would be just 16 addresses in size.

Now a good rule here for sizing is that when you're trying to figure out how many IPs you might need in a VPC or a subnet, figure out how many you're gonna need now, and then estimate how many you might need two years from now. You're gonna need to make sure your applications have room to grow.

So we're gonna specify No IPv6 CIDR Block, as mentioned, and then we get to tenancy. Tenancy is about where your VPC's instances are run. Dedicated tenancy in a VPC means any instances that are launched into that VPC by any user will be forced to be dedicated instances. Dedicated instances run on hardware that's dedicated to a single customer. So dedicated instances in one AWS account are actually physically isolated from instances in other AWS accounts. So if you leave the VPC tenancy as `Default`, like we're actually gonna do here, that means that when an instance is launched into that VPC, the person launching it can determine at that time if they want to use dedicated hardware or not. So again, setting it as Dedicated here will force any instances launched inside of the VPC to be dedicated instances. But `Default` means that each instance can either be shared tenancy or dedicated tenancy. You would just choose what you want when you launch the instance with the default.

We could also create more tags here. That's helpful if you want to add more things that you could search for later. Like say if you want to tag all the resources associated with your web application, right? All of the resources across all the different services. Then when you need to find everything related to that one thing, you just search for that tag, and you can find it all.

You'll see there's four Availability Zones, four default subnets. So we're gonna create a subnet for our new VPC. Click Create subnet ▶︎ provide a name `Demo-PublicSubnet1`.

And then this is pretty handy. It's got a VPC dropdown menu. And as you can see, this is one place where it's helpful to have a friendly name. We can already see it's populated the list with all of the VPCs in this Region. And this is the default. We know that because it's the only other one, but the `DemoVPC`, the one we want, is right here, so we select that.

And we're gonna leave Availability Zone as No preference because this is just gonna be the first subnet we create, so we don't need to worry about that. But if you were to say, create a second public subnet and you were gonna do a Multi-AZ deployment, you would want to make sure here to select a different Availability Zone so that they're in different AZs.

So next we're gonna specify the CIDR block for this subnet. So this is a public subnet, and we aren't gonna need most of our IPs to be here because of that. We'll talk more about why that is in a later lesson, but for this, for what's gonna be just this, you know, one public subnet that we're gonna create out of this whole VPC that we might want to add things to later, we're gonna restrict it to a small range of IPs. So we're gonna give it a `/25` and we're just gonna start it at `10.0.0.0`, and then we'll give it `/25`. And that's gonna be a range of 128 IPs.

So important, when you create a subnet, the first four IPs and the last IP in that subnet are reserved by AWS. So when you're planning for how many IPs you're gonna have in your subnet, you need to remember that you need to subtract five IPs from each of those subnets. This is one reason why generally it's better to have a few large subnets rather than a lot of small subnets, because the more subnets you create, the more IPs that are reserved by us. So once you've got that all set, click **Create**. Then we're gonna click **Close** to go back to the subnet dashboard.

Now you'll see `Demo-PublicSubnet1` is available now. So now we've got a VPC, and we've got a subnet, but we need that subnet to be able to get to the internet. It's not actually like that by default. So we're gonna need an Internet Gateway. So this is actually pretty straightforward. Click Internet Gateways on the left. Click Create Internet Gateway. And creating it's pretty easy, as you see. Just got to give it a name. So we're gonna call it `Demo-IGW`, or Internet Gateway. Automatically populates the tags. Click Create Internet Gateway. And it's created very quickly. 

Now you'll see it's **Detached**. It is not attached to any of the VPCs. There are multiple ways you can get from, you know, go to attach it. You can click here, Attach to a VPC. You can click Attach to a VPC from here, the Actions menu. But if you want to mess with it later, you just go back to your Internet Gateways dashboard, and you'll see you have it checked. Actions, Attach to VPC. All those options are gonna take you to the same place. So then the same thing here, you'll see the dropdown list pops up with your VPCs. `DemoVPC` is what we want.

The AWS Command Line Interface command is a handy way to generate the CLI commands you'd need to attach this IGW to this VPC. So you select what you're using the CLI with from this list. And then it automatically generates the command that you would run, and you can copy it and paste it and run it into the AWS CLI. And that's gonna launch the same action that we are gonna do here. So instead, we're just gonna go ahead and click **Attach Internet Gateway** here.

So now you'll see the internet gateway is Attached, and we can see that it's attached to `DemoVPC`, but we still need to actually turn that subnet into a public subnet. So there's multiple steps involved here. Now that we have an internet gateway attached to our VPC, we can actually do that.

So click **Subnets** to the left, and then we're gonna select the check box for our subnet so that we can see the details for it. So the problem here is, even though we have an internet gateway, even though we have a VPC, we have a public subnet, we need a route that will connect them. So we've got a route table. By default, new subnets are associated with the main route table for the VPC. As you can see here, it's got one entry, and that is showing that any traffic that is headed for something within the VPC, the target is local.

We can edit this route table, but that would change the VPC's main route table and affect any other resources associated now or in the future with that route table. So instead of editing the main route table, we're gonna need to create a new one. So we're gonna go to Route Tables. As you can see, there are two route tables here already. That's the main route table for `DemoVPC` and the main route table for the default VPC. So we're gonna need to create a new one. We're gonna click **Create Route Table**. We're gonna give it a name to make it easy to identify. And then select the VPC from the dropdown list. All right. And then click Create, and then Close.

Now we've created this route table, but it still needs a route. There's a few things we can do here with this. From this Actions menu, you can see, you can set it as a main route table. But we're not gonna do that. We're gonna leave the main route table as it is, and we're just going to use associations to connect this new route table with the subnets we need to connect it with. We can delete it obviously. Edit subnet associations is what we're gonna go to next.

So as you can see, it populates it with the subnets within this VPC already. If there were other subnets, they'd all be in this list. It shows that it's currently using the main route table. So we're gonna associate it with this `DemoVPCPublicRouteTable`. I'm gonna click Save and you'll see it'll pop up here under subnet associations. It's now associated with our subnet. And the subnet itself is no longer associated with the main route table. And that's because a subnet can only be associated with a single route table.

So now we have the new route table associated with the subnet, but we still need a route that will actually get traffic to and from the internet gateway. So just go through these again. So we did the subnet associations. There's also edge associations, which let you associate route tables with edge resources, like if you wanted to put a route table associated directly with an internet gateway or with like CloudFront distributions, that's where you would go to do that. And there's also Edit route propagation, which is for when you're dealing with virtual private gateways, like you would use to set up a VPN connection with your VPC. Now the only one we need here is **Edit routes**.

So we've already got our default route. We're gonna add a route to the internet. So we put in zero, and as you see, it's actually populating with the most common ones there. So we're gonna select 0.0.0.0/0. And we're going to set the Target as Internet Gateway. And it handily pops up with all the internet gateways that are associated with this VPC. So we select that, and we're just gonna click **Save routes**.

So again, you'll see the routes for this route table have been updated. We can go back to the subnet, select it, and look at the route table. So now we have this subnet associated with the route table that has an entry that goes to the internet gateway, which means that the subnet is what we call a public subnet.

And this would be a place to put your public-facing resources. So you could use your Application Load Balancers, your NAT resolution solutions, those kinds of things.

## Deploying a Simple VPC via the AWS Command Line Interface Demo

In this demo, we'll be launching a simple VPC via the AWS Command Line Tool or CLI.

First, install the AWS CLI and configure it on your machine so that it has access to your AWS account. Open up the command prompt. Run `AWS configure` and make sure you've got your access key and default region. Leave default output format as JSON.

We'll move onto VPC commands. All CLI commands start with `aws`, and then the VPC commands are actually run under the `ec2` option, not the `vpc` option. There is no `vpc` option. The command we're gonna run is `aws ec2 create-vpc`. Use parameter `--cidr-block 10.0.0.0/22`. We'll add another global parameter, `--region us-east-2`.

We'll have three parameters dealing w/ the IPv6 CIDR block:

* `--amazon-provided-ipv6-cidr-block` | `--no-amazon-provided-ipv6-cidr-block`

* `--ipv6-pool <value>`

* `--ipv6-cidr-block <value>`

* `--dry-run` or `--no-dry-run` ▶︎ an option that is gonna run the command and then let you know if you have the required permissions to do that action. Running `--dry-run` gives you a chance to test and see if you could run this command, if you have the permission set up correctly to run this command, but it doesn't actually go through with it.

* `--instance-tenancy <value>` ▶︎ you can set instance tenancy to Dedicated or Default. Default means that any instances that are launched into that VPC can be either dedicated or shared tenancy, and it's just determined at the time at which you launched the instance whereas dedicated instance tenancy on your VPC means that all instances that get launched into that VPC are gonna use dedicated hardware for your account.

* `--tag-specifications` ▶︎ used to add tags to the VPC. So start with `ResourceType`. Since we're launching a VPC, we'll set `vpc` as `ResourceType`.

* `--cli-input-json` | `--cli-input-yaml`, `--generate-cli-skeleton <value>` ▶︎ deal w/ creating and using skeletons in json or yaml to launch your VPC w/

We'll add the tag-specifications ▶︎ `aws ec2 create-vpc --cidr-block 10.0.0.0/22 --region us-east-2 --tag-specifications ResourceType=vpc,Tags=[{Key=Name,Value=DemoVPC2}]`. We'll receive our output in JSON. We can perform a describe VPCs command to check and see if our VPC is active ▶︎ `aws ec2 describe-vpcs --region us-east-2`.

Next, we'll create an internet gateway ▶︎ `aws ec2 create-internet-gateway --region us-east-2 --tag-specifications ResourceType=internet-gateway,Tags=[{Key=Name,Value=DemoIGW2}]`. Now the Internet Gateway is in that region, but not yet attached to a VPC. Run the command to attach it ▶︎ `aws ec2 attach-internet-gateway --region us-east-2 --internet-gateway-id <IGW ID> --vpc-id <VPC_ID>`. We will not need to tag as we are attaching a resource, not creating.

`aws ec2 describe-internet-gateways --region us-east-2`

Next, create the subnet ▶︎ `aws ec2 create-subnet --region us-east-2 --tag-specifications ResourceType=subnet,Tags=[{Key=Name,Value=PublicSubnet2DemoVPC}]`. The tags are helpful for tearing the resources down later on.

Specify the tags. Specify the --cidr-block for that subnet ▶︎ `aws ec2 create-subnet --region us-east-2 --tag-specifications ResourceType=subnet,Tags=[{Key=Name,Value=PublicSubnet2DemoVPC}] --cidr-block 10.0.0.0/25 --vpc-id <VPC ID>`. Provide a `/25` range, and then specify the VPC in which the subnet will be launched.  You can specify the Availability Zone that you're gonna launch your subnet into. You can either specify it by the name of the Availability Zone or by the actual ID of the Availability Zone.

There's also `--outposts-arn <value>`, which is in case you're setting the subnet up on an outpost from AWS Outposts.

Let's get our JSON response, and let's go ahead and grab that `SubnetId`. We're gonna move on to `create-route-table` ▶︎ `aws ec2 create-route-table --region us-east-2 --vpc-id <VPC ID> --tag-specifications ResourceType=route-table,Tags=[{Key=Name,Value=DemoVPC2RouteTable}]`. We'll add the vpc-id for where this route table will go. We've got our route table, and we've got a `RouteTableId`.

We only have one route in the route table. Private destinations within the VPC are gonna be directed to local, but we don't have anything that routes to the internet gateway. So now we need to create a route inside of that route ▶︎ `aws ec2 crate-route --region us-east-2 --destination-cidr-block 0.0.0.0/0 --gateway-id <IGW ID> --route-table <RTB ID>>`. Add the `--destination-cidr-block`, which in this case is 0.0.0.0/0 so all internet traffic. And then add the ID for the internet gateway so that it knows where to route internet traffic.

The final step is to associate the route table with our subnet in order to make it public ▶︎ `aws ec2 associate-route-table --region us-east-2 --route-table-id <RTB ID> --subnet-id <SUBNET ID>`. So, now we've built that simple VPC with a single subnet and an internet gateway and a route to the internet.
