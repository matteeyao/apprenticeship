# Injecting pod metadata into environment variables

* At the beginning of this chapter, a new version of the Kiada application was introduced

  * The application now includes the pod and node names and their IP addresses in the HTTP response
  
  * You'll make this information available to the application through the Downward API

## Injecting pod object fields

* The application expects the pod's name and IP, as well as the node name and IP, to be passed in via the environment variables `POD_NAME`, `POD_IP`, `NODE_NAME`, and `NODE_IP`, respectively

  * You can find a pod manifest that uses the Downward API to provide these variables to the container in the [`pod.kiada-ssl.downward-api.yaml`](pod.kiada-ssl.downward-api.yaml) file

  * The contents of this file are shown in the following listing:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: kiada-ssl
spec:
  ...
  containers:
  - name: kiada
    image: luksa/kiada:0.4
    env:                            # ← A
    - name: POD_NAME                # ← B
      valueFrom:                    # ← B
        fieldRef:                   # ← B
          fieldPath: metadata.name  # ← B
    - name: POD_IP                  # ← C
      valueFrom:                    # ← C
        fieldRef:                   # ← C
          fieldPath: status.podIP   # ← C
    - name: NODE_NAME               # ← D
      valueFrom:                    # ← D
        fieldRef:                   # ← D
          fieldPath: spec.nodeName  # ← D
    - name: NODE_IP                 # ← E
      valueFrom:                    # ← E
        fieldRef:                   # ← E
          fieldPath: status.hostIP  # ← E
    ports:
    ...

# ← A ▶︎ These are the environment variables for this container.
# ← B ▶︎ The POD_NAME environment variable gets its value from the Pod object's metadata.name field.
# ← C ▶︎ The POD_IP environment variable gets the value from the Pod object's status.podIP field.
# ← D ▶︎ The NODE_NAME variable gets the value from the spec.nodeName field.
# ← E ▶︎ The NODE_IP variable is initialized from the status.hostIP field.
```

* After you create this pod, you can examine its log using `kubectl logs`

  * The application prints the values of the three environment variables at startup

  * You can also send a request to the application and you should get a response like the following:

```zsh
Request processed by Kiada 0.4 running in pod "kiada-ssl" on node "kind-worker".
Pod hostname: kiada-ssl; Pod IP: 10.244.2.15; Node IP: 172.18.0.4. Client IP:
       ::ffff:127.0.0.1.
```

* Compare the values in the response w/ the field values in the YAML definition of the Pod object by running the command `kubectl get pod kiada-ssl -o yaml`

  * Alternatively, you can compare them w/ the output of the following commands:

```zsh
$ kubectl get po kiada-ssl -o wide
NAME    READY   STATUS    RESTARTS    AGE     IP            NODE          ...
kiada   1/1     Running   0           7m41s   10.244.2.15   kind-worker   ...

$ kubectl get node kind-worker -o wide
NAME          STATUS    ROLES     AGE   VERSION   INTERNAL-IP   ...
kind-worker   Ready     <none>    26h   v1.19.1   172.18.0.4    ...
```

* You can also inspect the container's environment by running `kubectl exec kiada-ssl --env`

## Injecting container resource fields

* Even if you haven't yet learned how to constrain the compute resources available to a container, let's take a quick look at how to pass those constraints to the application when it needs them

* Chpt 20 explains how to set the number of CPU cores and the amount of memory a container may consume

  * These settings are called CPU and memory resource *limits*

  * K8s ensures that the container can't use more than the allocated amount

* Some applications need to know how much CPU time and memory they have been given to run optimally within the given constraints

  * That's another thing the Downward API is for

  * The following listing shows how to expose the CPU and memory limits in environment variables ▶︎ Using the downward API to inject the container's compute resource limits

```yaml
env:
  - name: MAX_CPU_CORES           # ← A
    valueFrom:                    # ← A
      resourceFieldRef:           # ← A
        resource: limits.cpu      # ← A
  - name: MAX_MEMORY_KB           # ← B
    valueFrom:                    # ← B
      resourceFieldRef:           # ← B
        resource: limits.memory   # ← B
        divisor: 1k               # ← B

# ← A ▶︎ The MAX_CPU_CORES environment variable will contain the CPU resource limit.
# ← B ▶︎ The MAX_MEMORY_KB variable will contain the memory limit in kilobytes.
```

* To inject container resource fields, the field `resourceFieldRef` is used

  * The `resource` field specifies the resource value that is injected

* Each `resourceFieldRef` can also specify a `divisor`

  * It specifies which unit to use for the value

  * In the listing, the `divisor` is set to `1k`

  * This means that the memory limit value is divided by 1000 and the result is then stored in the environment variable

  * So, the memory limit value in the environment variable will use kilobytes as the unit

  * If you don't specify a divisor, as is the case in the `MAX_CPU_CORES` variable definition in the listing, the value defaults to 1

* The divisor for the memory limits/requests can be `1` (byte), `1k` (kilobyte) or `1Ki` (kibibyte), `1M` (megabyte) or `1Mi` (mebibyte), and so on

  * The default divisor for CPU is `1`, which is a whole core, but you can also set it to `1m`, which is a milli core or a thousandth of a core

* B/c environment variables are defined w/ a container definition, the resource constraints of the enclosing container are used by default

  * In cases where a container needs to know the resource limits of another container in the pod, you can specify the name of the other container using the `containerName` field within `resourceFieldRef`
