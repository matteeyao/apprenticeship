# Ingress

An API object that manages external access to the services in a cluster, typically HTTP.

Ingress may provide load balancing, SSL termination and name-based virtual hosting.

## Accept traffic from outside the cluster

So far, your service has accepted traffic from frontends within the cluster, using the Service's hostname. But these cluster IPs and DNS records only make sense in the cluster. This means other services within your company can route to the Kangaroo backend just fine. But how can you accept traffic from outside the cluster, e.g. internet traffic from your customers?

You deploy two kinds of resource: an **Ingress** and an **IngressController**.

1. An Ingress _defines_ rules for mapping cluster-external HTTP/S traffic to cluster-internal Services. From there, your Service will forward it to a Pod, as discussed above.

2. An IngressController _executes_ those rules. Your company has probably already set up an Ingress Controller for your cluster. The Kubernetes project supports and maintains three particular IngressControllers (AWS, GCE and Nginx) but there are other popular ones like Contour, which I use.

Setting up an IngressController is probably the job of a specialized Platform team who maintain your k8s cluster, or by the cloud platform you're using. So we won't cover those. Instead we'll just talk about the Ingress itself, which defines rules for mapping outside traffic into your services.

> [!NOTE]
> 
> You can define Ingress rules that work abstractly with any particular IngressController, so various projects can offer competing controllers which are all compatible with the same Kubernetes API.

```yaml
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: minimal-ingress
spec:
  ingressClassName: kangaroo
  rules:
  - http:
      paths:
      - path: /api
        pathType: Prefix
        backend:
          service:
            name: kangaroo-backend
            port:
              number: 8080
```

This creates an Ingress, using the default ingress controller for our cluster. The Ingress has one rule, which maps traffic whose path starts with /api to our kangaroo backend. We could define more rules if we want to, for example, map /admin to some admin service. Traffic that doesn't match any rules gets handled by your IngressController's defaults.


