# Managing service endpoints

* So far, you've learned that services are backed by pods, but that's not always the case

  * The endpoints to which a service forwards traffic can be anything that has an IP address

## Introducing the Endpoints object

* A service is typically backed by a set of pods whose labels match the selector defined in the Service object

  * Apart from the label selector, the Service object's `spec` or `status` section doesn't contain the list of pods that are part of the service

  * However, if you use `kubectl describe` to inspect the service, you'll see the IPs of the pods under `Endpoints` as follows:

```zsh
$ kubectl describe svc kiada
Name:                 kiada
...
Port:                 http 80/TCP
TargetPort:           8080/TCP
NodePort:             http 30080/TCP
Endpoints:            10.244.1.7:8080,10.244.1.8:8080,10.244.1.9:8080 + 1 more...   # ← A
...

# ← A ▶︎ The list of endpoints (pod IPs and ports) for this service.
```

* The `kubectl describe` command collects this data not from the Service object, but from an Endpoints object whose name matches that of the service

  * The endpoints of the `kiada` service are specified in the `kiada` Endpoints object

### Listing Endpoints objects

* You can retrieve Endpoints objects in the current namespace as follows:

```zsh
$ kubectl get endpoints
NAME    ENDPOINTS                                                     AGE
kiada   10.244.1.7:8443,10.244.1.8:8443,10.244.1.9:8443 + 5 more...   25m
quiz    10.244.1.11:8080                                              66m
quote   10.244.1.10:80,10.244.2.10:80,10.244.2.8:80 + 1 more...       66m
```

> [!NOTE]
> 
> The shorthand for `endpoints` is `ep`. Also, the object kind is Endpoints (plural form) not Endpoint. Running `kubectl get endpoint` fails w/ an error.

* As you can see, there are three Endpoints objects in the namespace, one for each service

  * Each endpoint object contains a list of IP and port combinations that represent the endpoints for the service

### Inspecting an Endpoints object more closely

* To see which pods represent these endpoints, use `kubectl get -o yaml` to retrieve the full manifest of the Endpoints object as follows:

```zsh
$ kubectl get ep kiada -o yaml
apiVersion: v1
kind: Endpoints
metadata:
  name: kiada                                       # ← A
  namespace: kiada                                  # ← A
  ...
subsets:
- addresses:
  - ip: 10.244.1.7                                  # ← B
    nodeName: kind-worker                           # ← C
    targetRef:
      kind: Pod
      name: kiada-002                               # ← D
      namespace: kiada                              # ← D
      resourceVersion: "2950"
      uid: 18cea623-0818-4ff1-9fb2-cddcf5d138c3
  ...                                               # ← E
  ports:                                            # ← F
  - name: https                                     # ← F
    port: 8443                                      # ← F
    protocol: TCP                                   # ← F
  - name: http                                      # ← F
    port: 8080                                      # ← F
    protocol: TCP                                   # ← F

# ← A ▶︎ The name and namespace of this Endpoints object. These always match the name and namespace of the associated Service object.
# ← B ▶︎ The IP address of the first endpoint (a pod that matches the label selector).
# ← C ▶︎ The name of the cluster node on which the pods run.
# ← D ▶︎ The name and namespace of the pod.
# ← E ▶︎ The entries for other pods that match the selector are not shown.
# ← F ▶︎ The list of ports that these endpoints expose. Matches the ports defined in the Service.
```

* As you can see, each pod is listed as an element of the `addresses` array

  * In the `kiada` Endpoints object, all endpoints are in the same endpoint subset, b/c they all use the same port numbers

  * However, if one group of pods uses port 8080, for example, and another uses port 8088, the Endpoint object would contain two subsets, each w/ its own ports

## Understanding who manage the Endpoints object

* You didn't create any of the three Endpoints objects

  * They were created by Kubernetes when you created the associated Service objects

  * These objects are fully managed by Kubernetes

  * Each time a new pod appears or disappears that matches the Service's label selector, Kubernetes updates the Endpoint object to add or remove the endpoint associated w/ the pod

  * You can also manage a service's endpoints manually, which you'll learn to do later

## 11.3.2 Introducing the EndpointSlice object

* See [endpointslice-object](endpointslice-object/README.md)

## 11.3.3 Managing service endpoints manually

* See [manage-service-endpoints-manually](manage-service-endpoints-manually/README.md)
