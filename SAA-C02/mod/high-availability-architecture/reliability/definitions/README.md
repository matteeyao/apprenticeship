# Definitions

## Resiliency, and the components of Reliability

Reliability of a workload in the cloud depends on several factors, the primary of which is *Resiliency*:

* **Resiliency** is the ability of a workload to recover from infrastructure or service disruptions, dynamically acquire computing resources to meet demand, and mitigate disruptions, such as misconfigurations or transient network issues.

The other factors impacting workload reliability are:

* Operational Excellence, which includes automation of changes, use of playbooks to respond to failures, and Operational Readiness Reviews (ORRs) to confirm that applications are ready for production operations.

* Security, which includes preventing harm to data or infrastructure from malicious actors, which would impact availability. For example, encrypt backups to ensure that data is secure.

* Performance Efficiency, which includes designing for maximum request rates and minimizing latencies for your workload.

* Cost Optimization, which includes trade-offs such as whether to spend more on EC2 instances to achieve static stability, or to rely on automatic scaling when more capacity is needed.

## Availability

*Availability* (also known as *service availability*) is both a commonly used metric to quantitatively measure resiliency, as well as target resiliency objective.

* **Availability** is the percentage of time that a workload is available for use.

*Available for use* means that it performs its agreed function successfully when required.
