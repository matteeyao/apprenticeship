# Shared Responsibility Model

## Overview of the Shared Responsibility Model and the Well-Architected Framework

![Fig. 1 Overview of the Shared Responsibility Model and the Well-Architected Framework](../../../../img/SAA-CO2/resilient-architecture/shared-responsibility-model/diag01.png)

> **What is the Shared Responsibility Model?**
>
> The Shared Responsibility Model is how AWS provides clarity around which areas of system security are theirs and which are owned by the customers. AWS provides us the Shared Responsibility Model so that we are clear which elements of the infrastructure it manages and what elements we are responsible for managing.
>
> At a very high level, AWS is responsible for the security of the cloud, and we are responsible for the security of our data, our applications, etc. in the cloud. AWS handles the provisioning, security, compute, networking storage, etc. needed for this instance as well as the software for the EC2 instance, e.g. user interface, hypervisor.
>
> Customers are responsible for the operating system and upwards. As customers, we are responsible for our customer data - for securing that data and definitely backing up that data.
