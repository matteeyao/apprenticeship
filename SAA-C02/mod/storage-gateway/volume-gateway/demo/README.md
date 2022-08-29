# Volume Gateway Performance Demonstration

## Learning Objectives

1. Deploy an Volume Gateway on Amazon EC2 with iSCSI volumes.

2. Attach the iSCSI volumes to an Amazon Linux EC2 Instance.

3. Use Flexible IO Tester (FIO) to generate performance data on Volume Gateway.

4. Take a snapshot of an iSCSI volume attached to the Volume Gateway.

5. Mount the snapshot to an alternative Amazon Linux EC2 Instance and measure the throughput.

![Fig. 1 Demo Architecture](../../../../../img/SAA-CO2/storage-gateway/volume-gateway/demo/fig01.png)

> [!NOTE]
>
> We will be using the Amazon EC2 service to host both the initiator (**EC2 Initiator Instance**) and the target (**Volume Gateway**) in this demonstration. Saves you having to find a data centre.
>
> In our demonstration, the initiator will be labelled throughout as the **EC2 Initiator Instance**. The target will be the **Volume Gateway**.

## Modules

* **Module 1** - Deploy EC2 Initiator Instance resources

* **Module 2** - Deploy Volume Gateway

* **Module 3** - Storage Performance Testing with FIO

* **Module 4** - Snapshot the volume and create an EBS volume

* **Module 5** - Cleanup
