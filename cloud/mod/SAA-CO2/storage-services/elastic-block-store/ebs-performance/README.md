# Understanding Amazon EBS performance

Several factors, including I/O characteristics and the configuration of your EC2 instances and volumes, can affect the performance of EBS volumes.

On a given volume configuration, certain I/O characteristics drive the performance behavior of your EBS volumes.

* SSD-backed volumes-General Purpose SSD (gp2 and gp3) and Provisioned IOPS SSD (io1 and io2)-deliver consistent performance for random or sequential I/O operations.

* HDD-backed volumes-Throughput Optimized HSS (st1) and Cold HSS (sc1)-deliver optimal performance only when I/O operations are large and sequential.

> ### IOPS
>
> IOPS are a unit of measure representing I/O operations per second. The operations are measured in KiB, and the underlying drive technology determines the maximum amount of data that a volume type counts as a single I/O.
>
> * I/O size is capped at 256 kibibyte (KiB) for SSD volumes.
>
> * I/O size is capped at 1,024 KiB for HDD volumes.
>
> SSD volumes handle small or random I/O much more efficiently than HDD volumes. When small I/O operations ar physically contiguous, Amazon EBS attempts to merge them into a single I/O operation up to the maximum size.
>
> * Large sequential I/O operations are divided into separate I/O operations up to the maximum I/O size. A single 1,024 KiB operation would count as four operations on SSDs and one operation on HDDs.
>
> * Noncontiguous I/O operations are not merged and handled as separate I/O operations.

> ### Throughput
>
> Throughput is the measurement of the volume of data transferred. Throughput is generally used to measure transfer performance in regard to large sequential files. Each EBS  volume storage type has both IOPS and throughput limitations.
>
> * SSD-backed volumes w/ large I/O sizes may experience a smaller number of IOPS than you provisioned b/c you are hitting the throughput limit for the volume.
>
> * HDD-backed volumes w/ sequential I/O workloads may experience a higher than expected number of IOPS as measured from inside your EC2 instance. This happens when the instance operating system merges sequential I/Os and counts them in 1,024 KiB-sized units.
>
> * HDD-backed volumes used for small or random I/O workloads can experience a lower throughput than expected. This is b/c we count each random, nonsequential I/O toward the total IOPS count, which can cause you to hit the volume's IOPS limit sooner than expected.

> ### Burst balance
>
> For some SSD-backed and HDD-backed EBS volume types, you are able to burst your performance above your provisioned baseline limits.
>
> * When you operate within your normal baseline range, you accumulate burst credits.
>
> * When your workload uses IOPS or throughput above your baseline range, you use your accumulated burst credits.
>
> * If your burst balance is depleted, you are unable to burst, and operations are limited yo your provisioned baseline limits.

> ### Latency
>
> Latency is the true round trip time of an I/O operation or the elapsed time, between sending an I/O to Amazon EBS and receiving an acknowledgement from Amazon EBS that the I/O read or write operation is complete.
>
> * The expected average latency for SSD-backed volumes ranges from sub-1 millisecond to single-digit millisecond performance depending on the SSD volume type.
>
> * The expected average latency for HDD-backed volumes is two-digit millisecond performance. Latency for HDD volumes is highly dependent on the EBS volume type and the workload. 
>
> Volume queue length can affect latency. The volume queue length is the number of pending I/O requests for a device. Queue length must be correctly calibrated with I/O size and latency to avoid creating bottlenecks, either on the guest operating system or on the network link to Amazon EBS.
>
> * Optimal queue length varies for each workload depending on your particular application's sensitivity to IOPS and latency. If your workload is not delivering enough I/O requests to fully use the performance available to your EBS volume, your volume might not deliver the IOPS or throughput that you have provisioned.
>
> * Transaction-intensive applications are sensitive to increased I/O latency and are well suited for SSD-backed volumes. You can maintain high IOPS while keeping latency down by maintaining a low queue length and a high number of IOPS available to the volume.
>
> * Throughput-intensive applications are less sensitive to increased I/O latency and are well suited for HDD-backed volumes. You can maintain high throughput to HDD-backed volumes by maintaining a high queue length when performing large, sequential I/O.