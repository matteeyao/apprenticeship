# Control Groups

`cgroups` isolate a process's ability to be able to have access to a resource.

A control group is a Linux kernel feature that limits and isolates the resource usage of a collection of processes.

## Control group subsystems

A subsystem is a kernel component that modifies the behavior of the processes in a cgroup.

* The `blkio` subsystem lets you limit and measure the amount of I/Os for each group of processes. It allows you to set throttle limits for each of the groups.

* The `cpu` subsystem allows you to monitor CPU usage by a group of processes enabling you to set weights and keep track of usage per CPU.

* The `cpuacct` subsystem generates automatic reports on CPU resources used by tasks in a `cgroup`.

* The `cpuset` subsystem allows you to pin groups of processes to one CPU or to groups of a process.

* The `device` subsystem allows or denies access to devices by tasks in a `cgroup`.

* The `freezer` subsystem suspends or resumes tasks in a `cgroup`. The `sigstop` signal is sent to the whole container.

* The `memory` subsystem sets limits on memory use by tasks in a `cgroup` and generates automatic reports on memory resources.

* The `net_cls` subsystem tags network packets w/ a `classid` that allows the identification of packets originating from a particular `cgroup` task.

* The `net_prio` subsystem provides a way to set the priority of network traffic dynamically.

## Learning summary

Cgroups allow you to allocate resources among groups of processes running on a system.

A subsystem is a kernel component that modifies the behavior of the processes in a cgroup.

* The `blkio` subsystem lets you limit and measure the amount of i/os for each group of process.

* The `cpuset` subsystem allows you to pin groups of processes to one cpu.

* The `freezer` subsystem suspends or resumes tasks in a cgroup.
