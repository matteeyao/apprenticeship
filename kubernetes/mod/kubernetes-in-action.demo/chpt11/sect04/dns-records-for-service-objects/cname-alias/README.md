# Creating a CNAME alias for an existing service

* In the previous sections, you learned how to create `A` and `AAAA` records in the cluster DNS

  * To do this, you create Service objects that either specify a label selector to find the service endpoints, or you define them manually using the Endpoints and EndpointSlice objects

* There's also a way to add `CNAME` records to the cluster DNS

  * In Kubernetes, you add `CNAME` records to DNS by creating a Service object, just as you do for `A` and `AAAA` records

> [!NOTE]
> 
> A `CNAME` record is a DNS record that maps an alias to an existing DNS name instead of an IP address.

## Creating an ExternalName service

* To create a service that serves as an alias for an existing service, whether it exists inside or outside the cluster, you create a Service object whose `type` field is set to `ExternalName`

  * The following listing shows an example of this type of service | An `ExternalName`-type service:

```yaml
apiVersion: v1
kind: Service
metadata:
  name: time-api
spec:
  type: ExternalName              # ← A
  externalName: worldtimeapi.org  # ← B

# ← A ▶︎ Serice type is set to ExternalName.
# ← B ▶︎ This is the fully qualified domain name that the CNAME record will point to.
```

* In addition to setting the `type` to `ExternalName`, the service manifest must also specify in the `externalName` field external name to which this service resolves

  * No Endpoints or EndpointSlice object is required for ExternalName services

## Connecting to an ExternalName service from a pod

* After the service is created, pods can connect to the external service using the domain name `time-api.<namespace>.svc.cluster.local` (or `time-api` if they're in the same namespace as the service) instead of using the actual FQDN of the external service, as shown in the following example:

```zsh
$ kubectl exec -it kiada-001 -c kiada -- curl http://time-api/api/timezone/CET
```

## Resolving ExternalName services in DNS

* B/c `ExternalName` services are implemented at the DNS level (only a `CNAME` recod is created for the service), clients don't connect to the service through the cluster IP, as is the case w/ non-headless ClusterIP services

  * They connect directly to the external service

  * Like headless services, `ExternalName` services have no cluster IP, as the following output shows:

```zsh
$ kubectl get svc time-api
NAME      TYPE          CLUSTER-IP  EXTERNAL-IP       PORT(S)   AGE
time-api  ExternalName  <none>      worldtimeapi.org  80/TCP    4m51s   # ← A

# ← A ▶︎ ExternalName services get no cluster IP.
```

* As a final exercise in this section on DNS, you can try resolving the `time-api` service in the `dns-test` pod as follows:

```zsh
/ # nslookup time-api
Server:         10.96.0.10
Address:        10.96.0.10#53

time-api.kiada.svc.cluster.local        canonical name = worldtimeapi.org.  # ← A
Name: worldtimeapi.org                                                      # ← B
Address: 213.188.196.246                                                    # ← B
Name: worldtimeapi.org                                                      # ← B
Address: 2a09:8280:1::3:e                                                   # ← B

# ← A ▶︎ The time-api service maps to worldtimeapi.org
# ← B ▶︎ The address worldtimeapi.org resolves to an IPv4 and an IPv6 address.
```

* You can see that `time-api.kiada.svc.cluster.local` points to `worldtimeapi.org`

  * This concludes this section on DNS records for K8s services

  * You can now exit the shell in the `dns-test` pod by typing `exit` or pressing Control-D

  * The pod is deleted automatically
