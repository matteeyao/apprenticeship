# DNS

![Fig. 1 DNS](../../../../../img/automation-orchestration-tools/kubernetes/orchestration/dns/diag01.png)

All services that are defined in the cluster get a DNS record. This is true for the DNS service as well.

Pods search DNS relative to their own namespace.

The DNS server schedules a DNS pod on the cluster and configures the kubelets to set the containers to use the cluster's DNS service.

PodSpec DNS policies determine the way that a container uses DNS. Options include `Default`, `ClusterFirst`, or `None`.

See all namespaces in your cluster:

```
kubectl get namespaces
```

Webserver is in a different namespace than Logprocessor. In order to locate Logprocessor, Webserver would need to look up "Logprocessor.web_logs".

```
kubectl get pods
```

Look at pod's resolve configuration:

```
kubectl exec -it <POD_NAME> /bin/bash
```

```
cat etc/resolv.conf
```
