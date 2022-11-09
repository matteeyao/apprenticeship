# Storage with PersistentVolumes

1. A Kubernetes Pod is using a PersistentVolumeClaim for storage. The Pod is deleted, along with the PersistentVolumeClaim. Which of the following configurations would allow the PersistentVolume to be re-used without manual intervention?

[x] A `Recycle` reclaimPolicy on the PersistentVolume.

[ ] A `Recycle` reclaimPolicy on the PersistentVolumeClaim.

[ ] A `Retain` reclaimPolicy on the PeristentVolume.

[ ] A `Delete` reclaimPolicy on the PeristentVolume.

> A Recycle reclaimPolicy automatically cleans up data on the volume, allowing it to be re-used.
