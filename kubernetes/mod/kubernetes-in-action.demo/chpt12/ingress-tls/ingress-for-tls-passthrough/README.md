# Configuring the Ingress for TLS passthrough

* You may be surprised to learn that K8s doesn't provide a standard way to configure TLS passthrough in Ingress objects

  * If the ingress controller supports TLS passthrough, you can usually configure it by adding annotations to the Ingress object

  * In the case of the Nginx ingress controller, you add the annotation shown in the following listing | **Enabling SSL passthrough in an Ingress when using the Nginx Ingress controller:**

```yaml
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: kiada-ssl-passthrough
  annotations:
    nginx.ingress.kubernetes.io/ssl-passthrough: "true"   # ← A
spec:
  ...

# ← A ▶︎ Enables SSL passthrough for this Ingress.
```

* SSL passthrough support in the Nginx controller isn't enabled by default

  * To enable it, the controller must be started w/ the `--enable-ssl-passthrough` flag

* Since this is a non-standard feature that depends heavily on which ingress controller you're using, let's not delve into it any further

  * For more information on how to enable passthrough in your case, see the documentation of the controller you're using

* Instead, let's focus on terminating the TLS connection at the ingress proxy

  * This is a standard feature provided by most Ingress controllers and therefore deserves a closer look
