# Working w/ Volume Gateway

1. What is the status of a Volume Gateway when it is synchronizing data locally with a copy of the data stored in AWS?

**Bootstrapping**

**Explanation**: When Volume Gateway is synchronizing data locally with a copy of the data stored in AWS, the status reflected is BOOTSTRAPPING.

2. What is defined as a point in time in which all data in the volume is consistent and from which you can create a snapshot or clone a volume?

**Volume recovery point**

**Explanation**: A volume recovery point is a point in time in which all data in the volume is consistent and from which you can create a snapshot or clone a volume.

3. How often does the default snapshot schedule automatically take a snapshot for stored volumes?

**Once a day**

**Explanation**: Stored volumes are assigned a default snapshot schedule to automatically create a snapshot once a day. The snapshot schedule for this volume type can be edited but not deleted.
