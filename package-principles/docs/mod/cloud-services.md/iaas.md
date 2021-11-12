# Infrastructure as a Service

Cloud infrastructure services, known as Infrastructure as a Service (IaaS), are made of highly scalable and automated compute resources. IaaS is fully self-service for accessing and monitoring computers, networking, storage, and other services. IaaS allows businesses to purchase resources on-demand and as-needed instead of having to buy hardware outright

## IaaS Delivery

IaaS delivers cloud computing infrastructure, including servers, network, operating systems, and storage, through virtualization technology. These cloud servers are typically provided to the organization through a dashboard or an API, giving IaaS clients complete control over the entire infrastructure. IaaS provides the same technologies and capabilities as a traditional data center w/o having to physically maintain or manage all of it. IaaS clients can still access their servers and storage directly, but it is all outsourced through a "virtual data center" in the cloud

As opposed to SaaS or PaaS, IaaS clients are responsible for managing aspects such as applications, runtime, OSes, middleware, and data. However, providers of the IaaS manage the servers, hard drives, networking, virtualization, and storage. Some providers even offer more services beyond the virtualization layer, such as databases or message queuing

## IaaS Advantages

* The most flexible cloud computing model

* Easy to automate deployment of storage, networking, servers, and processing power

* Hardware purchases can be based on consumption

* Clients retain complete control of their infrastructure

* Resources can be purchased as-needed

* Highly scalable

## IaaS Characteristics

* Resources are available as a service

* Cost varies depending on consumption

* Services are highly scalable

* Multiple users on a single piece of hardware

* Organization retain complete control of the infrastructure

* Dynamic and flexible

## When to use IaaS

* **Startups and small companies** may prefer IaaS to avoid spending time and money on purchasing and creating hardware and software

* **Larger companies** may prefer to retain complete control over their applications and infrastructure, but they want to purchase only what they actually consume or need

* **Companies experiencing rapid growth** like the scalability of IaaS, and they can change out specific hardware and software easily as their needs evolve

## IaaS Limitations and Concerns

Many limitations associated w/ SaaS and PaaS models - such as data security, cost overruns, vendor lock-in and customization issues - also apply to the IaaS model. Particular limitations to IaaS include:

* **Security**

  * While the customer is in control of the apps, data, middleware, and the OS platform, security threats can still be sourced from the host or other virtual machines (VMs). Insider threat or system vulnerabilities may expose data communication btwn the host infrastructure and VMs to unauthorized entities

* **Legacy Systems operating in the cloud**

  * While customers can run legacy apps in the cloud, the infrastructure may not be designed to deliver specific controls to secure the legacy apps. Minor enhancement to legacy apps may be required before migrating them to the cloud, possibly leading to new security issues unless adequately tested for security and performance in the IaaS systems

* **Internal resources and training**

  * Additional resources and training may be required for the workforce to learn how to effectively manage the infrastructure. Customers will be responsible for data security, backup, and business continuity. Due to inadequate control into the infrastructure however, monitoring and management of the resources may be difficult w/o adequate training and resources available inhouse

* **Multi-tenant security**

  * Since the hardware resources are dynamically allocated across users as made available, the vendor is required to ensure that other customers cannot access data deposited to storage assets by previous customers. Similarly, customers must rely on the vendor to ensure that VMS are adequately isolated within the multi-tenant cloud architecture
