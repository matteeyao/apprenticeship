# Using a custom object to configure Ingress routing

* The Citrix ingress controller provides the HTTPRoute custom object type, which allows you to configure where the ingress should route HTTP requests

  * As you can see in the following manifest, you don't specify a Service object as the backend, but you instead specify the `kind`, `apiGroup`, and `name` of the HTTPRoute object that contains the routing rules

  * **Example Ingress object using a resource backend:**

```yaml
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: my-ingress
spec:
  ingressClassName: citrix
  rules:
  - host: example.com
    http:
      paths:
      - pathType: ImplementationSpecific
        backend:                          # ← A
          resource:                       # ← A
            apiGroup: citrix.com          # ← B
            kind: HTTPRoute               # ← B
            name: my-example-route        # ← C

# ← A ▶︎ The ingress backend for this rule isn't a Service, but a custom K8s resource.
# ← B ▶︎ The resource kind and API group are specified here.
# ← C ▶︎ This is the name of the HTTPRoute object instance that contains the HTTP routing rules.
```

* The Ingress object in the listing specifies a single rule

  * It states that the ingress controller should forward traffic destined for the host `example.com` according to the configuration specified in the object of the kind `HTTPRoute` (from the API group `citrix.com`) named `my-example-route`

  * Since the HTTPRoute object isn't part of the K8s API, its contents are beyond the scope of this book, but you can probably guess that it contains rules like those in the Ingress object but specified diferently and w/ additional configuration options
