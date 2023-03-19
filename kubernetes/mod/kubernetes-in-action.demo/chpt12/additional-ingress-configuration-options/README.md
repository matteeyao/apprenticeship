# Additional Ingress configuration options

* Don't forget that you can use the `kubectl explain` command to learn more about a particular API object type, and to use it regularly

  * If not, now is a good time to use it to see what else you can configure in an Ingress object's `spec` field

  * Inspect the output of the following command:

```zsh
$ kubectl explain ingress.spec
```

* In addition to the `defaultBackend`, `rules`, and `tls` fields explained in the previous sections, only one other field is supported, namely `ingressClassName`

  * This field is used to specify which ingress controller should process the Ingress object

* The reason you don't see any other fields for specifying these options is that it would be nearly impossible to include all possible configuration options for every possible ingress implementation in the Ingress object's schema

  * Instead, these custom options are configured via annotations or in separate custom K8s API objects

* Each ingress controller implementation supports its own set of annotations or objects

  * We mentioned earlier that the Nginx ingress controller uses annotations to configure TLS passthrough

  * Annotations are also used to configure HTTP authentication, session affinity, URL rewriting, redirects, Cross-Origin Resource Sharing (CORS), and more

  * The list of supported annotations can be found at https://kubernetes.github.io/ingress-nginx/user- guide/nginx-configuration/annotations/

## 12.4.1 [Configuring the Ingress using annotations](configure-ingress-using-annotations/README.md)

## 12.4.2 [Configuring the Ingress using additional API objects](configure-ingress-using-api-objects/README.md)
