# On-Demand Instance

1. A company uses Reserved Instances to run its data-processing workload. The nightly job typically takes 7 hours to run and must finish within a 10-hour time window. The company anticipates temporary increases in demand at the end of each month that will cause the job to run over the time limit w/ the capacity of the current resources. Once started, the processing job cannot be interrupted before completion. The company wants to implement a solution that would allow it to provide increased capacity as cost-effectively as possible. What should a solutions architect do to accomplish this?

**A) Deploy On-Demand Instances during periods of high demand.**

B) Create a second Amazon EC2 reservation for additional instances.

C) Deploy Spot Instances during periods of high demand.

D) Increase the instance size of the instances in the Amazon EC2 reservation to support the increased workload.

**Explanation**: A – While Spot Instances would be the least costly option, they are not suitable for jobs that cannot be interrupted or must complete within a certain time period. On-Demand Instances would be billed for the number of seconds they are running. Spot Instances are not suitable for jobs that cannot be interrupted or disrupted.

<br />

2. You are automating the creation of EC2 instances in your VPC. Hence, you wrote a python script to trigger the Amazon EC2 API to request 50 EC2 instances in a single Availability Zone. However, you noticed that after 20 successful requests, subsequent requests failed.

What could be a reason for this issue and how would you resolve it?

[ ] By default, AWS allows you to provision a maximum of 20 instances per region. Select a different region and retry the failed request.

[ ] There was an issue w/ the Amazon EC2 API. Just resend the requests and these will be provisioned successfully.

[x] There is a vCPU-based On-Demand Instance limit per region which is why subsequent requests failed. Just submit the limit increase form to AWS and retry the failed requests once approved.

[ ] By default, AWS allows you to provision a maximum of 20 instances per Availability Zone. Select a different Availability Zone and retry the failed request.

**Explanation**: You are limited to running On-Demand Instances per your vCPU-based On-Demand Instance limit, purchasing 20 Reserved Instances, and requesting Spot Instances per your dynamic Spot limit per region. New AWS accounts may start with limits that are lower than the limits described here.

If you need more instances, complete the Amazon EC2 limit increase request form with your use case, and your limit increase will be considered. Limit increases are tied to the region they were requested for.

Hence, the correct answer is: **There is a vCPU-based On-Demand Instance limit per region which is why subsequent requests failed. Just submit the limit increase form to AWS and retry the failed requests once approved.**

> The option that says: **There was an issue with the Amazon EC2 API. Just resend the requests and these will be provisioned successfully** is incorrect because you are limited to running On-Demand Instances per your vCPU-based On-Demand Instance limit. There is also a limit of purchasing 20 Reserved Instances and requesting Spot Instances per your dynamic Spot limit per region hence, there is no problem with the EC2 API.

> The option that says: **By default, AWS allows you to provision a maximum of 20 instances per region. Select a different region and retry the failed request** is incorrect. There is no need to select a different region since this limit can be increased after submitting a request form to AWS.

> The option that says: **By default, AWS allows you to provision a maximum of 20 instances per Availability Zone. Select a different Availability Zone and retry the failed request** is incorrect because the vCPU-based On-Demand Instance limit is set per region and not per Availability Zone. This can be increased after submitting a request form to AWS.

<br />
