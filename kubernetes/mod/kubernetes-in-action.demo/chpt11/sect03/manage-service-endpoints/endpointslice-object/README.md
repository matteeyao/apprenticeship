# Introducing the EndpointSlice object

* As you can imagine, the size of an Endpoints object becomes an issue when a service contains a very large number of endpoints

  * Kubernetes control plane components need to send the entire object to all cluster nodes every time a change is made

  * In large clusters, this leads to noticeable performance issues

  * To counter this, the EndpointSlice object was introduced, which splits the endpoints of a single service into multiple slices

* While an Endpoints object contains multiple endpoint subsets, each EndpointSlice contains only one

  * If two groups of pods expose the service on different ports, they appear in two different EndpointSlice objects

  * Also, an EndpointSlice object supports a maximum of 1000 endpoints, but by default K8s only adds up to 100 endpoints to each slice

  * The number of ports in a slice is also limited to 100

  * Therefore, a service w/ hundreds of endpoints or many ports can have multiple EndpointSlices objects associated w/ it

* Like Endpoints, EndpointSlices are created and managed automatically

## Listing EndpointSlice objects

* In addition to the Endpoints objects, K8s creates the EndpointSlice objects for your three services

  * You can see them w/ the `kubectl get endpointslices` command:

```zsh
$ kubectl get endpointslices
NAME            ADDRESSTYPE   PORTS       ENDPOINTS                                     AGE
kiada-m24zq     IPv4          8080,8443   10.244.1.7,10.244.1.8,10.244.1.9 + 1 more...  80m
quiz-qbckq      IPv4          8080        10.244.1.11                                   79m
quote-5dqhx     IPv4          80          10.244.2.8,10.244.1.10,10.244.2.9 + 1 more... 79m     
```

* You'll notice that unlike Endpoint objects, whose names match the names of their respective Service objects, each EndpointSlice object contains a randomly generated suffix after the service name

  * This way, mane EndpointSLice objects can exist for each service

## Listing EndpointSlices for a particular service

* To see only the EndpointSlice objects associated w/ a particular service, you can specify a label selector in the `kubectl get` command

  * To list the EndpointSlice objects associated w/ the `kiada` service, use the label selector `kubernetes.io/service-name=kiada` as follows:

```zsh
$ kubectl get endpointslices -l kubernetes.io/service-name=kiada
NAME          ADDRESSTYPE   PORTS       ENDPOINTS                                     AGE
kiada-m24zq   IPv4          8080,8443   10.244.1.7,10.244.1.8,10.244.1.9 + 1 more...  88m
```

## Inspecting an EndpointSlice

* To examine an EndpointSlice object in more detail, use `kubectl describe`

  * Since the `describe` command doesn't require the full object name, and all EndpointSlice objects associated w/ a service begin w/ the service name, you can see them all by specifying only the service name, as shown here:

```zsh
$ kubectl describe endpointslice kiada
Name:           kiada-m24zq
Namespace:      kiada
Labels:         endpointslice.kubernetes.io/managed-by=endpointslice-controller.k8s.io
                kubernetes.io/service-name=kiada
Annotations:    endpoints.kubernetes.io/last-change-trigger-time: 2021-10-30T08:36:21Z
AddressType:    IPv4
Ports:                                                                                  # ← A
  Name  Port  Protocol                                                                  # ← A
  ----  ----  --------                                                                  # ← A
  http  8080  TCP                                                                       # ← A
  https 8443  TCP                                                                       # ← A
Endpoints:
  - Addresses: 10.244.1.7                                                               # ← B
    Conditions:
      Ready:    true
    Hostname:   <unset>
    TargetRef: Pod/kiada-002                                                            # ← C
    Topology: kubernetes.io/hostname=kind-worker                                        # ← D
...

# ← A ▶︎ The ports exposed by the endpoints in this slice.
# ← B ▶︎ The IP address of the first endpoint.
# ← C ▶︎ The kiada-002 pod represents this service endpoint.
# ← D ▶︎ Topology information for this endpoint. Explained later in this chapter.
```

> [!NOTE]
> 
> If multiple EndpointSlices match the name you provide to `kubectl describe`, the command will print all of them.

* The information in the output of the `kubectl describe` command isn't much different from the information in the Endpoint object you saw earlier

  * The EndpointSlice object contains a list of ports and endpoint addresses, as well as information about the pods that represent those endpoints

  * This includes the pod's topology information, which is used for topology-aware traffic routing
