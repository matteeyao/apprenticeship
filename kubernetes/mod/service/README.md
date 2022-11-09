# Service

## Routing traffic to all pods in a deployment

Each pod gets its own Cluster IP address (an IP address that is only meaningful within this Kubernetes cluster -- other clusters, or the wider internet, don't know anything about these IPs). So you can send traffic from the kangaroo frontend to the kangaroo backend using this IP address.

This works just fine until your Pod restarts, or you have a Deployment with multiple Pods, because they'll each have different IP addresses. How do your frontends know which IP addresses they should send kangaroo-backend traffic to? It's time for a new Kubernetes kind called a Service.

```yaml
# The kangaroo-backend Service.
# Other services inside the Kubernetes cluster will address the
# kangaroo-backend containers using this service.
apiVersion: v1
kind: Service
metadata:
  name: kangaroo-backend
  labels:
    app: kangaroo-backend
spec:
  ports:
    # Port 8080 of the Service will be forwarded to port 80 of one of the Pods.
    - port: 8080
      protocol: TCP
      targetPort: 80
  # Select which pods the service will proxy to
  selector:
    app: kangaroo
    role: backend
```

Deploying this Service creates a DNS record in your Kubernetes cluster, something like `my-svc.my-namespace.svc.cluster-domain.example`, or `kangaroo-backend.kangaroo-team.svc.mycompany.com`. The Service object will monitor which pods match its selectors. Whenever the service gets a TCP packet on its port, it forwards it to the `targetPort` on some matching pod.

So, now the kangaroo frontend will just send its API requests to that cluster-internal hostname `kangaroo-backend.kangaroo-team.svc.mycompany.com`, and the cluster's DNS resolver will map it to the kangaroo Service, which will forward it to some available Pod. If you have a Deployment, it'll ensure that you've got enough Pods to handle the traffic.

> [!NOTE]
>
> Both Services and Deployments select pods with a certain set of labels, but they have different responsibilities. Services handle balancing traffic and discovery, Deployments ensure your pods exist in the right number and right configuration. Generally backends/servers I deploy in K8s have 1 Service and 1 Deployment, selecting the same labels.

## Load Balancing across pods

If you're using a k8s Service to route traffic to your app, then you get basic load balancing for free. If a pod goes down, the Service will stop routing traffic to it, and start routing traffic to the other pods with the matching labels (probably pods in the same Deployment). As long as the other pods in the cluster use the Service's DNS hostname, their requests will be routed to an available Pod.
