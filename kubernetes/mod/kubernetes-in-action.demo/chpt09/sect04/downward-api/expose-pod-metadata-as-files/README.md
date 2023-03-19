# Using a downwardAPI volume to expose pod metadata as files

* As w/ config maps and secrets, pod metadata can also be projected as files into the container's filesystem using the `downwardAPI` volume type

* Suppose you want to expose the name of the pod in the `/pod-metadata/pod-name` file inside the container

  * The following listing shows the `volume` and `volumeMount` definitions you'd add to the pod ▶︎ Injecting pod metadata into the container's filesystem:

```yaml
...
  volumes:                            # ← A
  - name: pod-meta                    # ← A
    downwardAPI:                      # ← A
      items:                          # ← B
      - path: pod-name.txt            # ← B
        fieldRef:                     # ← B
          fieldPath: metadata.name    # ← B
  containers:
  - name: foo
    ...
    volumeMounts:                     # ← C
    - name: pod-meta                  # ← C
      mountPath: /pod-metadata        # ← C

# ← A ▶︎ This defines a downwardAPI volume w/ the name pod-meta.
# ← B ▶︎ A single file will appear in the volume. The name of the file is pod-name.txt and it contains the name of the pod.
# ← C ▶︎ The volume is mounted into the /pod-metadata path in the container.
```

* The pod manifest in the listing contains a single volume of type `downwardAPI`

  * The volume definition contains a single file named `pod-name.txt`, which contains the name of the pod read from the `metadata.name` field of the pod object

  * This volume is mounted in the container's filesystem at `/pod-metadata`

* As w/ environment variables, each item in a `downwardAPI` volume definition uses either `fieldRef` to refer to the pod object's fields, or `resourceFieldRef` to refer to the container's resource fields

  * For resource fields, the `containerName` field must be specified b/c volumes are defined at the pod level and it isn't obvious which container's resources are being referenced

  * As w/ environment variables, a `divisor` can be specified to convert the value into the expected unit

* As w/ `configMap` and `secret` volumes, you can set the default file permissions using the `defaultMode` field or per-file using the `mode` field, as explained earlier
