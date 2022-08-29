# File storage performance

> ### Latency
>
> Latency is measured as the amount of time between making a request to the storage system and receiving the response. Latency is also often referred to as delay.
>
> * Latency across storage types can range from sub-1 millisecond to low two-digit millisecond response rates.
>
> * How the storage is connected to the compute system affects the response rates.
>
>   * The network connectivity in a cloud provider's network will impact round-trip communication times between cloud compute and storage resources.
>
>   * The connectivity between on-premises resources and cloud storage resources will also impact the latency you observe. 
>
> * EFS Standard offers 1.0 and 2.4 milliseconds, and write latencies between 3.6 ms and 11.5 ms.
>
> * EFS One Zone (discussed in the next lesson) offers 35% lower per-operation latencies than EFS Standard and 47% lower cost. 

> ### IOPS
>
> Input/output operations per second (IOPS) is a statistical storage measurement of the number of input/output (I/O) operations that can be performed per second. IOPS is also used to measure the number of operations at a given type of workload and operation size that can occur per second. IOPS is typically used to measure random I/O read-write activities. Random means that the information used for the read-write activity is usually small in size and the different information is not related to each other.
>
> * For General Purpose file systems, EFS offers 35,000 read IOPS, and 7,000 write IOPS.
>
> * For Max I/O file systems, EFS offers IOPS in excess of 500,000. 

> ### Throughput
>
> Throughput is a statistical storage measurement used to measure the performance associated with reading and writing large sequential data files. Large files, such as video files, must be read from beginning to end. Throughput operations are often measured in MB per second.
>
> Amazon EFS offers throughput rates of up to 10+ GB per second. Note that maximum throughput rates for EFS can vary depending upon the region in which the file system is deployed.
