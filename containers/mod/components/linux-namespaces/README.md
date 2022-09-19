# Linux Namespaces

Namespaces allow the partitioning of kernel resources, ensuring that one set of processes sees only the resources allocated to it; while another set of processes see a different set of resources.

There are currently six Linux namespaces; although some consider `Cgroups` to be the seventh.

* **User** ▶︎ The user namespace is a key security feature; as each namespace can be given its own distinct set of UIDs and GUIDs.

* **IPC** ▶︎ IPC stands for Inter-Process Communications. This namespace isolates system resources from a process, while giving processes created in an IPC namespace visibility to each other allowing for interprocess communication.

* **UTC** ▶︎ The UTS namespace allows a single system to appear to have a different host and domain names to different process.

* **Mount** ▶︎ The Mount namespace controls the mountpoints that are visible to each container.

* **PID** ▶︎ The PID namespace provides processes w/ an independent set of process IDs (PIDs).

* **Network** ▶︎ The network namespace virtualizes the network stack.

Namespaces limit what you can see. Cgroups limit what you can access.

## Learning summary

Namespaces are a feature of the Linux kernel that partitions kernel resources such that one set of processes sees one set of resources while another set of processes sees a different set of resources.

* The IPC namespace offers a way for multiple processes to exchange data by creating separate message queues for each namespace.

* The Unix time sharing (UTC) namespace was implemented to allow the isolation of the hostname for each container.

* The PID namespace allows for the isolation of the process ID numbers + provides processes w/ an independent set of process IDs (PIDs) from other namespaces.

* The network namespace creates another copy of the network stack.
