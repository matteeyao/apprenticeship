# Docker vs. Virtual Machines

Virtual machines contain a complete operating system and applications

Hypervisor-based virtualization is resource intensive, and a Virtual Machine can take up several GB depending on the OS

In general, VMs provide an environment w/ more resources than pretty much any application needs

Whereas Docker containers are executed w/ the Docker engine

*Containers* are lightweight b/c they don't need the extra load of a hypervisor, but run directly within the host machine's kernel

This means you can run more containers on a given hardware combination than if you were using virtual machines

## Hardware and the Kernel

Every computer is comprised of essentially 4 physical components

1. Processor (CPU)

2. Memory (RAM)

3. Storage (HDD / SSD)

4. The Network Card (NIC)

One of the main tasks of any operating system is to manage those 4 resources btwn applications running on your computer

The *Kernel*, simply put, is a part of the OS that controls the hardware

The *Kernel* is the first program loaded when the computer is turned on, right after the bootloader, and then it handles the rest of the startup process

The majority of the time that it takes to turn on the computer is b/c of the Kernel

Each operating systems has its own implementation of the kernel, but in fact they all do the same thing: they control the hardware

So knowing that, let's talk about how a virtual machine actually runs

## Hypervisor

The *Hypervisor* is what enables virtualization (the running of several operating systems on one physical computer)

It allows the host computer to share its resources btwn VMs

There are two main types of *Hypervisors*:

* **Bare Metal Hypervisor**: this software is installed right on top of the underlying machine's hardware (so, in this case, there is no Host OS, there are only Guest OS's). You would do this on a machine on which the whole purpose was to run many virtual machines. Bare Metal Hypervisors have their own device drivers and interact w/ hardware directly. That's what makes them faster, simpler, and hence more stable

* **Hosted Hypervisor**: this is a program that is installed on top of the operating system. You are probably more familiar w/ it if you've ever installed a VM like VirtualBox. This type of hypervisor is something like a "translator" that translates the guest operating system's calls into the host operating system's calls

### Hosted Hypervisor

The system calls (`syscalls`) are a way in which a program requests a service from the Kernel for underlying hardware

An upside of a *Hosted Hypervisor* is that in this case we don't have to worry about underlying hardware and its drivers

We really just need to delegate the job to the host OS, which will manage everything for us

The downside is that it creates a resource overhead, and multiple layers sitting on top of each other make things complicated and lowers overall performance

![Bare Metal (Type 1) and Hosted (Type 2) Hypervisors](../../img/bare-metal-and-hosted-hypervisors.png)

### Downsides of Virtual Machines

The first and most obvious downside of using a virtual machine is inefficient resource management

In the case of a virtual machine, we have a full-blown virtual computer, in its entirety, w/ its own dedicated Kernel

We allocate `RAM` for it, we allocate `memory` for it, and we interact w/ it as if it were a standalone computer

Once you allocate some resources for a VM, it's going to hold onto them as long as it's running

Another problem is boot up time

Since the VM has its own Kernel, if you need to restart your machine, it will need to boot up an entire Kernel

## Containers Use the Host Kernel

Simply put, a container is kind of like an application for your phone w/ app-scoped resources

A Container uses the Kernel of the Host operating system

Containers have their own allocated file system and IP. Libraries, binaries, and services are installed inside a container, however, all the system calls and Kernel functionality comes from the underlying host OS

Containers are **super** lightweight

The booting up and redeploying of containers happens in seconds, b/c they don't need to start up the Kernel every time

## Containers vs. VMs

A Container's purpose is to run processes in an isolated environment

Meaning that in Docker you'd run one container for every single process you needed

VMs are for emulating an entire machine

Nowadays only Linux and Windows containers exist, but there are all kinds of hypervisors to emulate and kind of operating system

Containers are awesome b/c of their lightweight nature, but there are definitely situations where using a virtual machine becomes worth the associated resource cost

Containers make system calls directly to the Kernel, which opens up a whole variety of vulnerabilities, meaning that in general - VMs are more secure overall than just using Docker

Often you see Docker containers running inside virtual machines in a production environment
