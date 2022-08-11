# Enhanced Networking (EN)

> * Next generation of enhanced networking
>
>   * Hardware checksums
>
>   * Multi-queue support
>
>   * Receive side steering
>
> * 25 Gbps in a placement group
>
> * Open-source Amazon network driver

Enhanced Networking. Uses single root I/O virtualization (SR-IOV) to provide high-performance networking capabilities on supported instance types.

Elastic Network Adapter (ENA), the next-generation network interface and accompanying drivers that provide Enhanced Networking on EC2 instances.

ENA is a custom network interface optimized to deliver high throughput and packet per second (PPS) performance, and consistently low latencies on EC2 instances. Using ENA, customers can utilize up to 25 Gbps of network bandwidth on certain EC2 instance types.

### What is Enhanced Networking?

When ENI is insufficient to meet the demands of more intense workloads, Enhanced Networking (EN) becomes a viable alternative.

* It uses **single root I/O virtualization (SR-IOV)** to provide high-performance networking capabilities on supported instance types. SR-IOV is a method of device virtualization that provides higher I/O performance and lower CPU utilization when compared to traditional virtualized network interfaces

* Enhanced networking provides higher bandwidth, higher packet per second (PPS) performance, and consistently lower inter-instance latencies. There is no additional charge for using enhanced networking

* Use where you want good network performance

Depending on your instance type, enhanced networking can be enabled using:

* **Elastic Network Adapter (ENA)**, which supports network speeds of up to **100 Gbps** for supported instance types.

or

* Intel 82599 **Virtual Function (VF)** interface, which supports network speeds of up to **10 Gbps** for supported instance types. This is typically used on older instances

> [!NOTE]
> In any scenario question, you probably want to choose ENA over VF if given the option.

### Use cases

* Instances where higher bandwidth and lower inter-instance latency are required

* Supported for limited instance types (HVM only)
