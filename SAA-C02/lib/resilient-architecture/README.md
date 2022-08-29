# Resilient Architecture

1. Your web service has a performance SLA to respond to 99% of requests in <1 second. Under normal and heavy operations, distributing requests over four instances meets performance requirements. What architecture ensures cost efficient fault-tolerant operation of your service if an Availability Zone becomes unreachable?

[ ] Deploy the service on four servers in a single Availability Zone

[ ] Deploy the service on six servers in a single Availability Zone

[ ] Deploy the service on four servers across two Availability Zones

[x] Deploy the service on eight servers across two Availability Zones
