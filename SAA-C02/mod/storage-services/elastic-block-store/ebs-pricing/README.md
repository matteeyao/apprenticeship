# Amazon EBS pricing

With Amazon EBS, you pay only for what you use. Pricing for EBS volumes is based on the provisioned volume size, and the IOPS and throughput performance that you provision. EBS volume pricing varies based on the EBS volume type and the Availability Zone where it resides. The pricing for Amazon EBS Snapshots is based on the actual amount of storage space that you use.

## General pricing structure

Your costs are calculated based on the provisioned amounts. Pricing rates are quoted based on a per gigabyte (GB) per month for volumes, IOPS per month for provisioned IOPS, and MB/s per month for provisioned throughput. Your actual billing is based on the number of seconds of actual use. A month is a 30-day period and not the actual calendar month in which the use occurs. Each EBS volume is billed at the rate corresponding to the AWS Region or Availability Zone where it resides.

> ### Provisioned volume size
>
> For volume size, your provisioned volume size is used in the calculations. 
>
> * Provisioned volume size is used in cost calculations for all EBS volume types.
>
> * For example, a volume provisioned as 100 GB for 30 days would be charged at 100 GB times the per GB-month rate.
>
>   * `((Provisioned GB volume size) * (Price per GB-month)) / (1 month)`

> ### Provisioned IOPS
>
> For provisioned IOPS, your provisioned IOPS amount in excess of the baseline or included IOPS is used to calculate your costs. If the volume type includes a tiered pricing structure, further calculation per usage tier are performed.
>
> * Provisioned IOPS is used in cost calculations for gp3, io1, and io2 EBS volume types.
>
> * Tiered pricing is used in cost calculations for io2 EBS volume types.
>
> * For example, gp3 volumes include 3,000 provisioned IOPS. If you provision 8,000 IOPS, your cost is based on the 5,000 IOPS that are in excess of the 3,000 IOPS base amount.
>
>   * `(((Provisioned IOPS amount) - (Base IOPS amount)) * (Price per IOPS-month)) / (1 month)`

> ### Provisioned throughput
>
> For provisioned throughput, the provisioned throughput amount in excess of the baseline or included throughput amount is used to calculate your costs.
>
> * Provisioned throughput is used in cost calculations for gp3 EBS volume types.
>
> * For example, gp3 volumes include 125 MB/s of provisioned throughput. If you provision 500 MB/s throughput, your costs are based on the 375 MB/s in excess of the 125 MB/s base throughput amount.
>
>   * `(((Provisioned MB/s throughput) - (Base MB/s throughput)) * (Price per MB/s-month)) / (1 month)`

## Rate calculations per unit of time 

You can calculate costs on a per day, per hour, per minute, or per second basis. A unit-month can be a GB-month for volume size, IOPS-month for provisioned IOPS, or MB/s-month for provisioned throughput. You can use the following formulas to determine the rate per unit of time.

* `Per-day = (Rate per unit-month) / (30 days per month)`

* `Per-hour = (Rate per unit-month) / ((30 days per month) * (24 hours per day))`

  * `(Rate per unit-month) / (720 hours per month)`

* `Per-minute = (Rate per unit-month) / ((30 days per month) * (24 hours per day) * (60 minutes per hour))`

  * `(Rate per unit-month) / (43,200 minutes per month)`

* `Per-second = (Rate per unit-month) / ((30 days per month) * (24 hours per day) * (60 minutes per hour) * (60 seconds per minute))`

  * `(Rate per unit-month) / (2,592,000 seconds per month)`

When calculating costs for usage period less than a full month, you also need to add the unit duration into the usage calculations. 

* For example, if you are calculating the usage for 10 days, you must include the 10 days in the usage portion of the calculations.

  * `((Provisioned GB volume size) * (Price per GB-month) * (10 days)) / (30 days)`

## SSD-backed EBS volume types

SSD-backed EBS volume types include General Purpose SSD (gp2 and gp3) volumes types and Provisioned IOPS SSD (io1 and io2) volume types. The pricing structure for each volume type varies.

![Fig. 1 SSD-backed EBS volume types](../../../../../img/SAA-CO2/storage-services/elastic-block-storage/ebs-pricing/diag01.png)

> ### General Purpose SSD gp2 volumes
>
> Volume storage for General Purpose SSD gp2 volumes is charged by the amount you provision in GB per month until you release the storage. Provisioned storage for gp2 volumes will be billed in per-second increments, with a minimum of 60 seconds. I/O is included in the price of the volumes, so you pay only for each GB of storage you provision. 
>
> Performance for gp2 volumes is scaled by increasing or decreasing the provisioned volume size. gp2 volumes do not support separate provisioned IOPS or provisioned throughput options.
>
> Pricing example:
>
> * **Scenario**: You provision a 2,000 GB gp2 volume for 12 hours (43,200 seconds) in a 30-day month and the price is $0.10 per GB-month for the AWS Region you select. 
>
> * Your costs are calculated for the volume size using the formula for hours.
>
>   * `((Rate per GB-month) * (Volume size) * (Time period)) / (Time period units per month)`
>
>   * `($0.10 per GB-month * 2,000 GB * 12 hours) / (720 hours per month) = $3.33 for the volume`

> ### General Purpose SSD gp3 volumes
>
> Volume storage for General Purpose SSD gp3 volumes is charged by the amount you provision in GB per month until you release the storage. All gp3 volumes include a free baseline performance of 3,000 provisioned IOPS and 125 provisioned MB/s throughput. 
>
> Additional IOPS and throughput can be provisioned independently from the volume size and are charged by the amount you provision in IOPS per month and MB/s per month until you release the IOPS or throughput. 
>
> Provisioned storage, provisioned IOPS, and provisioned throughput for gp3 volumes are billed in per-second increments, with a minimum of 60 seconds.
>
> Pricing example:
>
> **Scenario**: You provision a 2,000 GB gp3 volume for 12 hours (43,200 seconds) in a 30-day month and the price is $0.08 per GB-month for the AWS Region you select. Additionally, you provision 10,000 IOPS at $0.005 per provisioned IOPS-month and provision 500 MB/s at $0.06 per provisioned MB/s-month.
>
> * Your costs are calculated for the volume size, the provisioned IOPS, and the provisioned throughput using the formula for hours.
>
>   * `((Calculate the volume pricing for time period) + (Provisioned IOPS pricing for time period) + (Provisioned throughput pricing for time period)) / (Time period units per month)`
>
>     * `((Rate per GB-month) * (Volume size) * (Time period)) + ((Rate per IOPS-month) * ((Provisioned IOPS) - (Base IOPS)) * (Time period)) + ((Rate per MB/s-month) * ((Provisioned MB/s) - (Base MB/s)) * (Time period)) / (Time period units per month)`
>
>     * `(($0.08 per GB-month * 2,000 GB * 12 hours) + ($0.005 * (10,000 IOPS - 3,000 IOPS) * 12 hours) + ($0.06 * (500 MB/s - 125 MB/s) * 12 hours)) / (720 hours per month) = ($1,920 + $420 + $270) / (720 hour per month) = $3.63 for the volume`

> ### Provisioned IOPS SSD io1 volumes
>
> Volume storage for Provisioned IOPS SSD io1 volumes is charged by the amount you provision in GB per month until you release the storage. 
>
> With Provisioned IOPS SSD io1 volumes, you are also charged by the amount you provision in IOPS per month. With Provisioned IOPS SSD io1 volumes, no baseline IOPS is provided. You are charged for your total provisioned IOPS amount.
>
> Provisioned storage and provisioned IOPS for io1 volumes are billed in per-second increments, with a minimum of 60 seconds.
>
> Pricing example:
>
> * **Scenario**: You provision a 2,000 GB io1 volume for 12 hours (43,200 seconds) in a 30-day month and the price is $0.125 per GB-month for the AWS Region you select. Additionally, you provision 1,000 IOPS at $0.065 per provisioned IOPS-month.
>
> * Your costs are calculated for the volume size, the provisioned IOPS, and the provisioned throughput using the formula for hours.
>
>   * `((Calculate the volume pricing for time period) + (Provisioned IOPS pricing for time period) / (Time period units per month)`
>
>     * `(((Rate per GB-month) * (Volume size) * (Time period)) + ((Rate per IOPS-month) * (Provisioned IOPS) * (Time period))) / (Time period units per month)`
>
>     * `(($0.125 per GB-month * 2,000 GB * 12 hours) + ($0.065 * 1,000 IOPS * 12 hours))  / (720 hours per month) = ($3,000 + $780) / (720 hour per month) = $5.25 for the volume`

> ### Provisioned IOPS SSD io2 volumes
>
> Volume storage for Provisioned IOPS SSD io2 volumes is charged by the amount you provision in GB per month until you release the storage. 
>
> With Provisioned IOPS SSD io2 volumes, you are also charged by the amount you provision in IOPS per month. The provisioned IOPS charges for io2 volumes are tiered. Therefore, as you provision higher IOPS on a single volume, the effective provisioned IOPS charges decrease, making it more economical to scale IOPS on a single volume.
>
> io2 provisioned IOPS tiers:
>
> * Tier 1 - Up to 32,000 IOPS
>
> * Tier 2 - 32,001 IOPS to 64,000 IOPS
>
> Provisioned storage and provisioned IOPS for io2 volumes are billed in per-second increments, with a minimum of 60 seconds.
>
> Pricing examples:
>
> * **Scenario 1**: You provision a 2,000 GB io2 volume for 12 hours (43,200 seconds) in a 30-day month and the price is $0.125 per GB-month for the AWS Region you select. Additionally, you provision 1,000 IOPs at $0.065 per provisioned IOPS-month for the first 32,000 IOPS.
>
> * Your costs are calculated for the volume size, the provisioned IOPS, and the provisioned throughput using the formula for hours.
>
>   * `((Calculate the volume pricing for time period) + (Provisioned IOPS pricing for time period) / (timer period units per month)`
>
>     * `(((Rate per GB-month) * (Volume size) * (Time period)) + ((Rate per IOPS-month) * (Provisioned IOPS) * (Time period))) / (Time period units per month)`
>
>     * `(($0.125 per GB-month * 2,000 GB * 12 hours) + ($0.065 * 1,000 IOPS * 12 hours))  / (720 hours per month) = ($3,000 + $780) / (720 hour per month) = $5.25 for the volume`
>
> * **Scenario 2**: You provision a 2,000 GB io2 volume for 12 hours (43,200 seconds) in a 30-day month and the price is $0.125 per GB-month for the AWS Region you select. Additionally, you provision 60,000 IOPS for 12 hours. Your rate is $0.065 per provisioned IOPS-month for the first 32,000 IOPS and $0.046 for the 28,000 IOPS.
>
> * Your costs are calculated for the volume size, the provisioned IOPS, and the provisioned throughput using the formula for hours.
>
>   * `((Calculate the volume pricing for time period) + (Tier 1 provisioned IOPS pricing for time period) + (Tier 2 provisioned IOPS pricing for time period)) / (Time period units per month)`
>
>     * `(((Rate per GB-month) * (Volume size) * (Time period)) + ((Tier 1 rate per IOPS-month) * (Provisioned IOPS) * (Time period)) + ((Tier 2 rate per IOPS-month) * (Provisioned IOPS) * (Time period))) / (Time period units per month)`
>
>     * `(($0.125 per GB-month * 2,000 GB * 12 hours) + ($0.065 * 32,000 IOPS * 12 hours) + ($0.046 * 28,000 IOPS * 12 hours))  / (720 hours per month) = ($3,000 + $24,960 + $15,456) / (720 hour per month) = $60.30 for the volume`

> [!NOTE]
>
> With Elastic Volumes, volume sizes can only be increased within the same volumes. To decrease a volume size, you must copy the EBS volume data to a new smaller EBS volume. 

## HDD-backed EBS volume types

HDD-backed EBS volume types include Throughput Optimized HDD st1 volumes type and Cold HDD sc1 volume type. The pricing structure for each volume type varies.

![Fig. 2 HDD-backed EBS volume types](../../../../../img/SAA-CO2/storage-services/elastic-block-storage/ebs-pricing/diag02.png)

> ### Throughput Optimized HDD st1 volumes
>
> Volume storage for Throughput Optimized HDD (st1) volumes is charged by the amount you provision in GB per month until you release the storage. Provisioned storage for st1 volumes will be billed in per-second increments with a minimum of 60 seconds. I/O is included in the price of the volumes, so you pay only for each GB of storage you provision.
>
> Performance for st1 volumes is scaled by increasing or decreasing the provisioned volume size. st1 volumes do not support separate provisioned IOPS or provisioned throughput options.
>
> Pricing example:
>
> * **Scenario**: You provision a 2,000 GB st1 volume for 12 hours (43,200 seconds) in a 30-day month and the price is $0.045 per GB-month for the AWS Region you select.
>
> * Your costs are calculated for the volume size using the formula for hours.
>
>   * `((Rate per GB-month) * (Volume size) * (Time period)) / (Time period units per month)`
>
>   * `($0.045 per GB-month * 2,000 GB * 12 hours) / (720 hours per month) = $1.50 for the volume`

> ### Cold HDD sc1 volumes
>
> Volume storage for Cold HDD (sc1) volumes is charged by the amount you provision in GB per month until you release the storage. Provisioned storage for sc1 volumes will be billed in per-second increments, with a minimum of 60 seconds. I/O is included in the price of the volumes, so you pay only for each GB of storage you provision.
>
> Performance for sc1 volumes is scaled by increasing or decreasing the provisioned volume size. sc1 volumes do not support separate provisioned IOPS or provisioned throughput options.
>
> Pricing example:
>
> * **Scenario**: You provision a 2,000 GB st1 volume for 12 hours (43,200 seconds) in a 30-day month and the price is $0.015 per GB-month for the AWS Region you select.
>
> * Your costs are calculated for the volume size using the formula for hours.
>
>   * `((Rate per GB-month) * (Volume size) * (Time period)) / (Time period units per month)`
>
>   * `($0.015 per GB-month * 2,000 GB * 12 hours) / (720 hours per month) = $0.50 for the volume`

> [!NOTE]
>
> With Elastic Volumes, volume sizes can only be increased within the same volumes. To decrease a volume size, you must copy the EBS volume data to a new smaller EBS volume.

## EBS Snapshots

Your EBS Snapshot storage cost is based on the amount of space your data consumes in Amazon S3. You are billed for only the total space that your data consumes. Your cost is not based on the provisioned size of your EBS volumes. 

Amazon EBS Snapshots are a point-in-time copy of your block data. EBS Snapshots are stored incrementally, which means you are billed only for the changed blocks stored. For the first snapshot of a volume, Amazon EBS saves a full copy of your data to Amazon S3. For each incremental snapshot, only the changed part of your Amazon EBS volume is saved.

Copying EBS snapshots is charged for the data transferred across AWS Regions. After the snapshot is copied, standard EBS Snapshot charges apply for storage in the destination Region. 

Pricing example:

**Scenario**: You initially have 70 GB of data in your provisioned 2,000 GB gp3 volume on day 1 in a 30 day month. You add 30 GB of data on day 15 of the month. The price is $0.05 per GB-month of data stored for the AWS Region you select.

* Your costs are calculated for the volume size using the formula for days.

  * `((Rate per GB-month) * (Stored data size) * (Time period)) / (Time period units per month)`

  * `($0.05 per GB-month * 70 GB * 30 days) + ($0.05 per GB-month * 30 GB * 15 days) / (30 days per month) = $4.25 for the data storage`
