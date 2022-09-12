# Automated Scaling

Automated Scaling refers to automatically provisioning resources in response to real-time data metrics.

W/o automated scaling, you must provision enough resources to cover your peak resource needs at all times.

W/ automated scaling, you can automatically detect (or even predict) increase in usage. The automated system creates new servers to handle the peak usage time, then removes those servers when usage returns to normal levels.

## Containers and automated scaling

Automated scaling depends on the ability to spin up new instances quickly and efficiently.

Since containers are small and can start up quickly, they are ideal for this purpose. This means that if the system detects an increase in usage, it can spin up new containers in a few seconds.

This increases stability and reduces cost. Your users see less downtime due to high loads, and you don't use (and pay for) resources unnecessarily.
