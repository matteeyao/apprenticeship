## Spread Placement Groups

> * A **Spread Placement Group** is a group of instances that are each placed on distinct underlying hardware
>
> * **Spread Placement Groups** are recommended for applications that have a small number of critical instances that should be kept separate from each other
>
> * *Think individual instances*

* Group of instances that are each placed on distinct underlying hardware

* These will be on separate racks w/ separate network inputs as well as separate power requirements

* If you have one rack that fails, it's only going to affect that one EC2 instance

* We can have **Spread Placement Groups** inside different availability zones within one region

* W/ a **Spread Placement Group**, think of individual instances

* So if a rack does fail, it's only going to affect one instance

* Opposite of **Clustered Placement Group**

  * **Clustered Placement Groups** put all your EC2 instances very close together so we can get really high performing network throughput and low latency

  * **Spread Placement Groups** are designed to protect your EC2 instances from hardware failure; it's individual instances put on individual racks either in the same availability zone or different availability zones depending on configuration

> A **Spread Placement Group** is a group of instances that are each placed on distinct racks, w/ each rack having its own network and power source.
>
> The following image shows seven instances in a single Availability Zone that are placed into a spread placement group. The seven instances are placed on seven different racks.
>
> ![Fig. 1 Placement Group Spread](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/images/placement-group-spread.png)
>
> **Spread Placement Groups** are recommended for applications that have a small number of critical instances that should be kept separate from each other. Launching instances in a **Spread Placement Group** reduces the risk of simultaneous failures that might occur when instances share the same racks. **Spread Placement Groups** provide access to distinct racks, and are therefore suitable for mixing instance types or launching instances over time.
>
> A **Spread Placement Group** can span multiple Availability Zones in the same Region. You can have a maximum of seven running instances per Availability Zone per group.
>
> If you start or launch an instance in a **Spread Placement Group** and there is insufficient unique hardware to fulfill the request, the request fails. Amazon EC2 makes more distinct hardware available over time, so you can try your request later.
