# Adding a readiness probe to a pod

* To see readiness probes in action, create a new pod w/ a probe that you can switch from success to failure at will

  * This isn't a real-world example of how to configure a readiness probe, but it allows you to see how the outcome of the probe affects the pod's inclusion in the service

* The following listing shows the relevant part of the pod manifest file [`pod.kiada-mock-readiness.yaml`](pod.kiada-mock-readiness.yaml) | A readiness probe definition in a pod:

```yaml
apiVersion: v1
kind: Pod
...
spec:
  containers:
  - name: kiada
    ...
    readinessProbe:             # ← A
      exec:                     # ← B
        command:                # ← B
        -ls                     # ← B
        - /var/ready            # ← B
      initialDelaySeconds: 10   # ← C
      periodSeconds: 5          # ← C
      failureThreshold: 3       # ← C
      successThreshold: 2       # ← C
      timeoutSeconds: 2         # ← C
  ...

# ← A ▶ A readiness probe defined for the kiada container.︎
# ← B ▶ ︎The probe executes the ls command in the container.
# ← C ▶ ︎This defines when and how often the probe is executed, and how many times it must fail or succeed for the container's readiness state to change. It also sets the timeout for each invocation of the probe.
```

* The readiness probe periodically runs the `ls /var/ready` command in the `kiada` container

  * The `ls` command returns the exit code zero if the file exists, otherwise it's nonzero

  * Since zero is considered a success, the readiness probe succeeds if the file is present

* The reason to define such a strange readiness probe is so that you can change its outcome by creating or removing the file in question

  * When you create the pod, the file doesn't exist yet, so the pod isn't ready

  * Before you create the pod, delete all other kiada pods except `kiada-001`

  * This makes it easier to see the service endpoints change

## Observing the pods' readiness status

* After you create the pod from the manifest file, check its status as follows:

```zsh
$ kubectl get po kiada-mock-readiness
NAME                    READY   STATUS    RESTARTS    AGE
kiada-mock-readiness    1/2     Running   0           1m    # ← A

# ← A ▶︎ Only one of the pod's containers is ready.
```

* The `READY` column shows that only one of the pod's containers is ready

  * This is the `envoy` container, which doesn't define a readiness probe

  * Containers w/o a readiness probe are considered ready as soon as they're started

* Since the pod's containers aren't all ready, the pod shouldn't receive traffic sent to the service

  * You can check this by sending several requests to the kiada service

  * You'll notice that all requests are handled by the `kiada-001` pod, which is the only active endpoint of the service

  * This is evident from the Endpoints and EndpointSlice objects associated w/ the service

  * For example, the `kiada-mock-readiness` pod appears in the `notReadyAddresses instead of the addresses array in the Endpoints object:

```zsh
$ kubectl get endpoints kiada -o yaml
apiVersion: v1
kind: Endpoints
metadata:
  name: kiada
  ...
subsets:
- addresses:
  - ...
  notReadyAddresses:              # ← A
  - ip: 10.244.1.36               # ← A
    nodeName: kind-worker2        # ← A
    targetRef:                    # ← A
      kind: Pod                   # ← A
      name: kiada-mock-readiness  # ← A
      namespace: default          # ← A
    ...

# ← A ▶︎ The kiada-mock-readiness pod appears among the service's notReadyAddresses.
```

* In the EndpointSlice object, the endpoint's `ready` condition is `false`:

```zsh
$ kubectl get endpointslices -l kubernetes.io/service-name=kiada -o yaml
apiVersion: v1
items:
- addressType: IPv4
  apiVersion: discovery.k8s.io/v1
  endpoints:
  - addresses:
    - 10.244.1.36
    conditions:                     # ← A
      ready: false                  # ← A
    nodeName: kind-worker2
    targetRef:
      kind: Pod
      name: kiada-mock-readiness
      namespace: default
      ...

# ← A ▶︎ The kiada-mock-readiness pod's ready condition is false.
```

> [!NOTE]
> 
> In some cases, you may want to disregard the readiness status of pods. This may be the case if you want all pods in a group to get `A`, `AAAA`, and `SRV` records even though they aren't ready. If you set the `publishNotReadyAddresses` field in the Service object's `spec` to `true`, non-ready pods are marked as ready in both the Endpoints and EndpointSlice objects. Components like the cluster DNS treat them as ready.

* For the readiness probe to succeed, create the `/var/ready` file in the container as follows:

```zsh
$ kubectl exec kiada-mock-readiness -c kiada -- touch /var/ready
```

* The `kubctl exec` command runs the `touch` command in the `kiada` container of the `kiada-mock-readiness` pod

  * The `touch` command creates the specified file

  * The container's readiness probe will now be successful

  * All the pod's containers should now show as ready

  * Verify that this is the case as follows:

```zsh
$ kubectl get po kiada-mock-readiness
NAME                    READY   STATUS    RESTARTS  AGE
kiada-mock-readiness    1/2     Running   0         10m
```

* Surprisingly, the pod is still not ready

  * Is something wrong or is this the expected result?

  * Take a closer look at the pod w/ `kubectl describe`

  * In the output you'll find the following line:

```zsh
Readiness: exec [ls /var/ready] delay=10s timeout=2s period=5s #success=2 #failure=3
```

* The readiness probe defined in the pod is configured to check the status of the container every 5 seconds

  * However, it's also configured to require two consecutive probe attempts to be successful before setting the status of the container to ready

  * Therefore, it takes about 10 seconds for the pod to be ready after you create the `/var/ready` file

* When this happens, the pod should become an active endpoint for the service

  * You can verify this is the case by examining the Endpoints or EndpointSlice objects associated w/ the service, or by simply accessing the service a few times and checking to see if the `kiada-mock-readiness` pod receives any of the requests you send

* If you want to remove the pod from the service again, run the following command to remove the `/var/ready` file from the container:

```zsh
$ kubectl exec kiada-mock-readiness -c kiada -- rm /var/ready
```

* This mockup of a readiness probe is just to show how readiness probes work

  * In the real world, the readiness probe shouldn't be implemented in this way

  * If you want to manually remove pods from a service, you can do so by either deleting the pod or changing the pod's labels rather than manipulating the readiness probe outcome

> [!TIP]
> 
> If you want to manually control whether a pod is included in a service, add a label key such as `enabled` to the pod and set its value to `true`. Then add the label selector `enabled=true` to your service. Remove the label from the pod to remove the pod from the service.
