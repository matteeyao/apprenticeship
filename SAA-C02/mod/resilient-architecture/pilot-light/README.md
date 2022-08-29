# Pilot Light

The term pilot light is often used to describe a disaster recovery scenario in which a minimal version of an environment is always running in the cloud.

The idea of the pilot light is an analogy that comes from the gas heater. In a gas heater, a small flame that’s always on and can quickly ignite the entire furnace to heat up a house. This scenario is similar to a backup-and-restore scenario.

For example, with AWS you can maintain a pilot light by configuring and running the most critical core elements of your system in AWS. When the time comes for recovery, you can rapidly provision a full-scale production environment around the critical core that has always been running.
