# Amazon CloudWatch

Amazon CloudWatch metrics are statistical data that you can use to view, analyze, and set alarms on the operational behavior of your volumes.

Data is available automatically in 1-minute periods at no charge.

When you get data from CloudWatch, you can include a **Period request parameter** to specify the granularity of the returned data. This is different than the period that we use when we collect the data (1-minute periods). We recommend that you specify a period in your request that is equal to or greater than the collection period to ensure that the returned data is valid.

You can get the data using the CloudWatch API or the Amazon EC2 console. The console takes the raw data from the CloudWatch API and displays a series of graphs based on the data. Depending on your needs, you might prefer to use the data from the API or the graphs in the console.

## Amazon EBS metrics

Amazon EBS sends data points to CloudWatch for several metrics. All EBS volume types automatically send 1-minute metrics to CloudWatch. Data is reported to CloudWatch only when the volume is attached to an instance.

<table style="width:100%;"><thead><tr><th style="width:37.1949%;background-color:rgb(23, 87, 121);"><span style="color:rgb(255, 255, 255);font-weight:bold;">Metric</span></th><th style="width:62.7175%;background-color:rgb(23, 87, 121);"><span style="color:rgb(255, 255, 255);font-weight:bold;">Description</span></th></tr></thead><tbody><tr><td style="text-align:left;width:37.1949%;"><span style="font-size:16px;">VolumeReadBytes&nbsp;</span></td><td style="text-align:left;width:62.7175%;"><span style="font-size:16px;">Provides information on the read operations in a specified period of time</span></td></tr><tr><td style="text-align:left;width:37.1949%;"><span style="font-size:16px;">VolumeWriteBytes&nbsp;</span></td><td style="text-align:left;width:62.7175%;"><span style="font-size:16px;">Provides information on the write operations in a specified period of time&nbsp;</span></td></tr><tr><td style="text-align:left;width:37.1949%;"><span style="font-size:16px;">VolumeReadOps <br></span></td><td style="text-align:left;width:62.7175%;"><span style="font-size:16px;">The total number of read operations in a specified period of time <br></span></td></tr><tr><td style="text-align:left;width:37.1949%;"><span style="font-size:16px;">VolumeWriteOps <br></span></td><td style="text-align:left;width:62.7175%;"><span style="font-size:16px;">The total number of write operations in a specified period of time <br></span></td></tr><tr><td style="text-align:left;width:37.1949%;"><span style="font-size:16px;">VolumeTotalReadTime <br></span></td><td style="text-align:left;width:62.7175%;"><p><span style="font-size:16px;"><strong>Note:&nbsp;</strong>This metric is not supported with Multi-Attach enabled volumes</span></p><p><span style="font-size:16px;">The total number of seconds spent by all read operations that completed in a specified period of time.&nbsp;</span><span style="font-size:16px;">If multiple requests are submitted at the same time, this total could be greater than the length of the period.<br></span></p></td></tr><tr><td style="text-align:left;width:37.1949%;"><span style="font-size:16px;">VolumeTotalWriteTime <br></span></td><td style="text-align:left;width:62.7175%;"><p><span style="font-size:16px;"><strong>Note:&nbsp;</strong>This metric is not supported with Multi-Attach enabled volumes</span></p><p><span style="font-size:16px;">The total number of seconds spent by all write operations that completed in a specified period of time.&nbsp;</span><span style="font-size:16px;">If multiple requests are submitted at the same time, this total could be greater than the length of the period.<br></span></p></td></tr><tr><td style="text-align:left;width:37.1949%;"><span style="font-size:16px;">VolumeIdleTime <br></span></td><td style="text-align:left;width:62.7175%;"><p><span style="font-size:16px;"><strong>Note:</strong> This metric is not supported with Multi-Attach enabled volumes</span></p><p><span style="font-size:16px;">The total number of seconds in a specified period of time when no read or write operations were submitted<br></span></p></td></tr><tr><td style="text-align:left;width:37.1949%;"><span style="font-size:16px;">VolumeQueueLength <br></span></td><td style="text-align:left;width:62.7175%;"><span style="font-size:16px;">The number of read and write operation requests waiting to be completed in a specified period of time. <br></span></td></tr><tr><td style="text-align:left;width:37.1949%;"><span style="font-size:16px;">VolumeThroughputPercentage</span><br></td><td style="text-align:left;width:62.7175%;"><p><span style="font-size:16px;"><strong>Note:</strong> This metric is not supported with Multi-Attach enabled volumes.&nbsp;</span></p><p><span style="font-size:16px;">Used with Provisioned IOPS SSD volumes only. The percentage of I/O operations per second (IOPS) delivered of the total IOPS provisioned for an Amazon EBS volume. Provisioned IOPS SSD volumes deliver their provisioned performance 99.9 percent of the time.</span></p></td></tr><tr><td style="text-align:left;width:37.1949%;"><span style="font-size:16px;">VolumeConsumedReadWriteOps <br></span></td><td style="text-align:left;width:62.7175%;"><span style="font-size:16px;">Used with Provisioned IOPS SSD volumes only</span><br><br><span style="font-size:16px;">The total amount of read and write operations (normalized to 256K capacity units) consumed in a specified period of time <br></span></td></tr><tr><td style="text-align:left;width:37.1949%;"><span style="font-size:16px;">BurstBalance <br></span></td><td style="text-align:left;width:62.7175%;"><span style="font-size:16px;">Used with General Purpose SSD (gp2), Throughput Optimized HDD (st1), and Cold HDD (sc1) volumes only&nbsp;</span><br><br><span style="font-size:16px;">Provides information about the percentage of I/O credits (for gp2) or throughput credits (for st1 and sc1) remaining in the burst bucket.&nbsp;</span><span style="font-size:16px;">Data is reported to CloudWatch only when the volume is active. If the volume is not attached, no data is reported. <br></span></td></tr></tbody></table>

## Amazon CloudWatch events for Amazon EBS

Amazon EBS emits notifications based on Amazon CloudWatch events for a variety of volume, snapshot, and encryption status changes. 

With CloudWatch events, you can establish rules that start programmatic actions in response to a change in volume, snapshot, or encryption key state. For example, when a snapshot is created, you can run an AWS Lambda function to share the completed snapshot with another account or copy it to another Region for disaster-recovery purposes.

Amazon EBS volume events

* `createVolume`

* `deleteVolume`

* `attachVolume`

* `reattachVolume`

* `modifyVolume`

Amazon EBS Snapshots events

* `createSnapshot`

* `createSnapshots`

* `copySnapshot`

* `shareSnapshot`
