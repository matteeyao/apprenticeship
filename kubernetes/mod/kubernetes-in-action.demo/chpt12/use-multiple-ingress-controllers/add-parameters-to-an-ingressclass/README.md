# Adding parameters to an IngressClass

* In addition to using IngressClasses to specify which ingress controller to use for a particular Ingress object, IngressClasses can also be used w/ a single ingress controller if it can provide different ingress flavors

  * This is achieved by specifying different parameters in each IngressClass

## Specifying parameters in the IngressClass object

* The IngressClass object doesn't provide any fields for you to set the parameters within the object itself, as each ingress controller has its own specifics and would require a different set of fields

  * Instead, the custom configuration of an IngressClass is typically stored in a separate custom K8s object type that's specific to each ingress controller implementation

  * You create an instance of this custom object type and reference it in the IngressClass object

* For example, AWS provides an object w/ kind `IngressClassParams` in API group `elbv2.k8s.aws`, version `v1beta1`

  * To configure the parameters in an IngressClass object, you reference the IngressClassParams object instances as shown in the following listing | **Referring to a custom parameters object in the IngressClass:**

```yaml
apiVersion: networking.k8s.io/v1
kind: IngressClass                  # ← A
metadata:
  name: custom-ingress-class
spec:
  controller: ingress.k8s.aws/alb   # ← B
  parameters:                       # ← C
    apiGroup: elbv2.k8s.aws         # ← C
    kind: IngressClassParams        # ← C
    name: custom-ingress-params     # ← C

# ← A ▶︎ This is a standard IngressClass object.
# ← B ▶︎ The AWS Load Balancer controller is used to provision ingresses of this class.
# ← C ▶︎ The parameters to be used when deploying an ingress of this class are stored in the IngressClassParams object named custom-ingress-params.
```

* In the listing, the IngresClassParams object instance that contains the parameters for this IngressClass is named `custom-ingress-params`

  * The object `kind` and `apiGroup` are also specified

## Example of a custom API object type used to hold parameters for the IngressClass

* The following listing shows an example of an IngressClassParams object | **Example IngressClassParams object manifest:**

```yaml
apiVersion: elbv2.k8s.aws/v1beta1   # ← A
kind: IngressClassParams            # ← A
metadata:
  name: custom-ingress-params       # ← B
spec:
  scheme: internal                  # ← C
  ipAddressType: dualstack          # ← C
  tags:                             # ← C
  - key: org                        # ← C
    value: my-org                   # ← C

# ← A ▶︎ This is custom object kinds that's available in AWS.
# ← B ▶︎ The object name corresponds to the name referenced in the parameter field of the IngressClass object.
# ← C ▶︎ These fields contain the configuration for the ingress.
```

* W/ the IngressClass and IngressClassParams object in place, cluster users can create Ingress objects w/ the `ingressClassName` set to `custom-ingress-class`

  * The objects are processed by the `ingress.k8s.aws/alb` controller (the AWS Load Balancer controller)

  * The controller reads the parameters from the IngressClassParams object and uses them to configure the load balancer

* K8s doesn't care about the contents of the IngressClassParams object

  * They're only used by the ingress controller

  * Since each implementation uses its own object type, you should refer to the controller's documentation or use `kubectl explain` to learn more about each type
