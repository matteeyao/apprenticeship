## Spread placement groups

> * A spread placement group is a group of instances that are each placed on distinct underlying hardware
>
> * Spread placement groups are recommended for applications that have a small number of critical instances that should be kept separate from each other
>
> * Think individual instances

* Group of instances that are each placed on distinct underlying hardware

* These will be on separate racks w/ separate network inputs as well as separate power requirements

* If you have one rack that fails, it's only going to affect that one EC2 instance

* We can have spread placement groups inside different availability zones within one region

* W/ a spread placement group, think of individual instances

* So if a rack does fail, it's only going to affect one instance

* Opposite of Clustered Placement Group

  * Clustered placement groups put all your EC2 instances very close together so we can get really high performing network throughput and low latency

  * Spread placement groups are designed to protect your EC2 instances from hardware failure; it's individual instances put on individual racks either in the same availability zone or different availability zones depending on configuration

> A spread placement group is a group of instances that are each placed on distinct racks, w/ each rack having its own network and power source.
>
> The following image shows seven instances in a single Availability Zone that are placed into a spread placement group. The seven instances are placed on seven different racks.
>
> ![Fig. 1 Placement Group Spread](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/images/placement-group-spread.png)
>
> Spread placement groups are recommended for applications that have a small number of critical instances that should be kept separate from each other. Launching instances in a spread placement group reduces the risk of simultaneous failures that might occur when instances share the same racks. Spread placement groups provide access to distinct racks, and are therefore suitable for mixing instance types or launching instances over time.
>
> A spread placement group can span multiple Availability Zones in the same Region. You can have a maximum of seven running instances per Availability Zone per group.
>
> If you start or launch an instance in a spread placement group and there is insufficient unique hardware to fulfill the request, the request fails. Amazon EC2 makes more distinct hardware available over time, so you can try your request later.
