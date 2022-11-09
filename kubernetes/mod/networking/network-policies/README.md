# Network Policies

If you want to control traffic flow at the IP address or port level (OSI layer 3 or 4), then you might consider using Kubernetes NetworkPolicies for particular applications in your cluster. NetworkPolicies are an application-centric construct which allow you to specify how a pod is allowed to communicate with various network "entities" (we use the word "entity" here to avoid overloading the more common terms such as "endpoints" and "services", which have specific Kubernetes connotations) over the network. NetworkPolicies apply to a connection with a pod on one or both ends, and are not relevant to other connections.

The entities that a Pod can communicate with are identified through a combination of the following 3 identifiers:

1. Other pods that are allowed (exception: a pod cannot block access to itself)

2. Namespaces that are allowed

3. IP blocks (exception: traffic to and from the node where a Pod is running is always allowed, regardless of the IP address of the Pod or the node)

When defining a pod- or namespace- based NetworkPolicy, you use a selector to specify what traffic is allowed to and from the Pod(s) that match the selector.

Meanwhile, when IP based NetworkPolicies are created, we define policies based on IP blocks (CIDR ranges).

## Limit networking within the cluster

To mitigate damage caused by an intrusion, use a Kubernetes Network Policy to limit your project's networking permissions. You use Network Policies to restrict:

* Ingress (who your service can receive traffic from)

* Egress (who your service can send traffic to)

The best security practice is to default deny all networking within your namespace, and then open up specific exceptions for the networking you know your service needs. Here's an example enabling some networking for kangaroo. Note that there are separate rules for ingress and egress. Each ingress rule has two parts:

1. `from` says which kinds of services can send traffic

2. `ports` says which ports they can send traffic to

Egress rules have `to` and `ports`, configuring which destinations your service can send traffic to, and which ports on those destinations are allowed.

```yaml
apiVersion: networking.k8s.io/v1
kind: NetworkPolicy
metadata:
  name: kangaroo-network-policy
  labels:
    app: kangaroo
    role: network-policy
spec:
  podSelector:
    matchLabels:
      app: kangaroo
      role: backend
  policyTypes:
    - Ingress
    - Egress
  # Each element of `ingress` opens some port of Kangaroo to receive requests from a client.
  ingress:
    # Anything can ingress to the public API port
    - ports:
        - protocol: TCP
          port: 80
    # Hypothetical example: say your Platform team has configured cluster-wide metrics,
    # you'll need to grant it access to your pod's metrics server. Your company will
    # have examples for this. Assume kangaroo-backend serves metrics on TCP :81
    - from:
        - namespaceSelector:
            matchLabels:
              project: monitoring-system
      ports:
        - protocol: TCP
          port: 81
  # Each element of `egress` opens some port of Kangaroo to send requests to some server
  egress:
    # Say the Kangaroo backend calls into a membership microservice.
    # You'll need to allow egress to it!
    - ports:
        - port: 443
      to:
        - namespaceSelector:
            matchLabels:
              project: membership
              role: api
```

