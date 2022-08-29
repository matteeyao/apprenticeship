# Spot instances and Spot Fleets

## What is an EC2 Spot Instance?

**Amazon EC2 Spot Instances** let you take advantage of unused EC2 capacity in the AWS Cloud. Spot instances are available at up to a 90% discount compared to On-Demand prices. You can use **Spot Instances** for various stateless, fault-tolerant, or flexible applications, such as big data, containerized workloads, CI/CD, web servers, high-performance computing (HPC), and other test and development workloads.

### Spot prices

To use **Spot Instances**, you must first decide on your maximum Spot price. The instance will be provisioned so long as the Spot price is **BELOW** your maximum Spot price

* The **hourly Spot price** varies depending on capacity and region

* If the Spot price goes above your maximum, you have **two minutes** to choose whether to stop or terminate your instance

### Spot blocks

You may also use a **Spot block** to stop your Spot Instances from being terminated even if the Spot price goes over your max Spot price. You can set Spot blocks for between **one to six hours** currently

### Use cases

Spot Instances are useful for the following tasks:

* Big data and analytics

* Containerized workloads

* CI/CD and testing

* Web services

* Image and media rendering

* High-performance computing

Spot Instances are not good for:

* Persistent workloads

* Critical jobs

* Databases

### Terminate spot instances

How to terminate Spot Instances

![Spot Lifecycle](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/images/spot_lifecycle.png)

![Spot Request states](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/images/spot_request_states.png)

## What are spot fleets?

A Spot Fleet is a collection of Spot Instances and, optionally, On-Demand Instances.

The **Spot Fleet** attempts to launch the number of Spot Instances and On-Demand Instances to meet the target capacity you specified in the Spot Fleet request. The request for Spot Instances is fulfilled if there is available capacity and the **maximum price you specified in the request exceeds the current Spot price**. The Spot Fleet also attempts to maintain its target capacity fleet if your Spot Instances are interrupted.

### Launch pools

Spot Fleets will try and match the target capacity w/ your price restraints

1. Set up different launch pools. Define things like **EC2** instance types, operating system, and Availability Zone

2. You can have **multiple** pools, and the fleet will choose the best way to implement depending on the strategy you define

3. Spot fleets will **stop launching instances** once you reach your price threshold or capacity desire

### Strategies

You can have the following strategies w/ Spot Fleets:

**capacityOptimized**: The Spot Instances come from the pool w/ optimal capacity for the number of instances launching

    * Guaranteeing you have the certain amount of capacity

**lowestPrice**: The Spot Instances come from the pool w/ the lowest price. This is the default strategy

**diversified**: The Spot Instances are distributed across all pools

**InstancePoolsToUseCount**: The Spot Instances are distributed across the number of Spot Instance pools you specify. This parameter is valid only when used in combination w/ `lowestPrice`

    * Combination of **diversified** w/ **lowestPrice**, but w/ **diversified**, it's using all the pools. W/ **InstancePoolsToUseCount**, you define which pools that you want. It will launch in those pools at the lowest price.

## Learning summary

* Spot Instances save up to **90%** of the cost of On-Demand Instances

* Useful for any type of computing where you don't need **persistent storage**

* You can block Spot Instances from terminating by using **Spot block**

* A Spot Fleet is a collection of Spot Instances and, optionally, On-Demand Instances
