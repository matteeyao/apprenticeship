# Elastic Fabric Adapter

A network device that you can attach to your Amazon EC2 instance to accelerate High Performance Computing (HPC) and machine learning applications

EFA brings the scalability, flexibility, and elasticity of the cloud to tightly-coupled HPC (High-performance computing) applications

W/ EFA, tightly-coupled HPC applications have access to lower and more consistent latency and higher throughput than traditional TCP channels, enabling them to scale better

An EFA is an Elastic Network Adapter (ENA) w/ added capabilities. It provides all of the functionality of an ENA, w/ additional **OS-bypass functionality**

## What is an Elastic Fabric Adapter?

* An **Elastic Fabric Adapter (EFA)** is a network device that you can attach to your Amazon EC2 instance to accelerate High Performance Computing (HPC) and machine learning applications

* EFA provides lower and more consistent latency and higher throughput than the TCP transport traditionally used in cloud-based HPC systems

* EFA can use OS-bypass. OS-bypass enables HPC and machine learning applications to bypass the operating system kernel and to communicate directly w/ the EFA device. It makes it a lot faster w/ a lot lower latency. Not supported w/ Windows currently, only Linux

    * OS-bypass is an access model that allows HPC and machine learning applications to communicate directly w/ the network interface hardware to provide low-latency, reliable transport functionality

## Learning summary

> * **Elastic Fabric Adapter**
>
>   * For when you need to accelerate High Performance Computing (HPC) and machine learning applications or if you need to do an OS by-pass. If you see a scenario question mentioning HPC or ML and asking what network adapter you want, choose EFA
