# Understanding Virtualization

## Virtual Machines

* Acts as a Virtual computer

* Runs on the host Operating system

* Provides virtual hardware to the guest operating system:

  * From the Guest OS perspective, the VM is a real physical machine

* **Hypervisor** handles the CPU, memory, hard drive and networking for the VM

* Can run multiple operating systems

## Hypervisors

* Type 1: Runs directly on the system hardware - "Bare-metal hypervisor".

* Type 2: Runs on a host operating system that provides virtualization services.

## LXC

* Lets users create and manage system or application containers.

  * Chroot

  * Kernel Namespaces

  * SELinux / Apparmor

  * Seccomp policies

## SELinux / Apparmor

SELinux stands for Security-Enhanced Linux and provides a mechanism for supporting access control policies. It is a kernel modification and userspace tool that limits the privileges a process has to the minimum required work.

App Armor is a Linux kernel security module, available in SUSE Linux enterprise servers and Debian based platforms, that allows you to restrict a program's capabilities using profiles.
