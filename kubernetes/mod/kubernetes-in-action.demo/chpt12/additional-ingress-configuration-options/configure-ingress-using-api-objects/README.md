# Configuring the Ingress using additional API objects

* Some ingress implementations don't use annotations for additional ingress configuration, but instead provide their own object kinds

  * In the previous section, you saw how to use annotations to configure session affinity when using the Nginx ingress controller

  * In the current section, you'll learn how to do the same in Google Kubernetes Engine

## Using the BackendConfig object type to enable cookie-based session affinity in GKE

* In clusters running on GKE, a custom object of type BackendConfig can be found in the Kubernetes API

  * You create an instance of this object and reference it by name in the Service object to which you want to apply the object

  * You reference the object using the `cloud.google.com/backend-config` annotations, as shown in the following listing | **Referring to a BackendConfig in a Service object in GKE:**

```yaml
apiVersion: v1
kind: Service
metadata:
  name: kiada
  annotations:
    cloud.google.com/backend-config: '{"default": "kiada-backend-config"}'  # ← A
spec:

# ← A ▶︎ This annotation specifies the name of the BackendConfig object that applies to this service.
```

* You can use the BackendConfig object to configure many things

  * Use `kubectl explain backendconfig.spec` to learn more about it, or see the GKE documentation

* As a quick example of how custom objects are used to configure ingresses, we'll show you how to configure cookie-based session affinity using the BackendConfig object | **Using GKE-specific BackendConfig object to configure session affinity:**

```yaml
apiVersion: cloud.google.com/v1     # ← A
kind: BackendConfig                 # ← A
metadata:
  name: kiada-backend-config
spec:
  sessionAffinity:                  # ← B
    affinityType: GENERATED_COOKIE  # ← B

# ← A ▶ ︎This is a custom K8s API object that's only available in Google Kubernetes Engine. 
# ← B ▶ ︎This enables cookie-based session affinity for the service that references this BackendConfig.
```

* In the listing, the session affinity type is set to `GENERATED_COOKIE`

  * Since this object is referenced in the `kiada` service, whenever a client accesses the service through the ingress, the request is always routed to the same backend pod

* In this and the previous section, you saw two ways to add custom configuration to an Ingress object

  * Since the method depends on which ingress controller you're using, see its documentation for more more information
