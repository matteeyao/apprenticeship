# Specifying the IngressClass in the Ingress object

* When you create an Ingress object, you can specify the class of the ingress using the `ingressClassName` field in the `spec` section of the Ingress object, as in the following listing | **Ingress object referencing a specific IngressClass:**

```yaml
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: kiada
spec:
  ingressClassName: nginx         # ← A
  rules:
  ...

# ← A ▶︎ This is where the class of this Ingress object is specified.
```

* The Ingress object in the listing indicates that its class should be `nginx`

  * Since this IngressClass specifies `k8s.io/ingress-nginx` as the controller, the Ingress from this listing is processed by the Nginx ingress controller

## Setting the default IngressClass

* If multiple ingress controllers are installed in the cluster, there should be multiple IngressClass objects

  * If an Ingress object doesn't specify the class, K8s applies the default IngressClass, marked as such by setting the `ingressclass.kubernetes.io/is- default-class` annotation to `"true"`
