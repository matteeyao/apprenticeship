# Configuring the Ingress using annotations

* In the previous chpt, you learned that K8s services only support client IP-based session affinity

  * Cookie-based session affinity isn't supported b/c services operate at Layer 4 of the OSI network model, whereas cookies are part of Layer 7 (HTTP)

  * However, b/c Ingresses operate at L7, they can support cookie-based session affinity

  * This is the case w/ the Nginx ingress controller used in the following example

## Using annotations to enable cookie-based session affinity in Nginx ingresses

* The following listing shows an exa,ple of using Nginx-ingress-specific annotations to enable cookie-based session affinity and configure the session cookie name

  * Using annotations to configure session affinity in an Nginx ingress ([`ing.kiada.nginx-affinity.yaml`](ing.kiada.affinity-nginx.yaml)):

```yaml
apiVersion: networking.k8s.io/v1
kind: Ingress
metadata:
  name: kiada
  annotations:
    nginx.ingress.kubernetes.io/affinity: cookie                      # ← A
    nginx.ingress.kubernetes.io/session-cookie-name: SESSION_COOKIE   # ← B
spec:
  ...

# ← A ▶︎ This annotation enables the cookie-based session affinity.
# ← B ▶︎ This overrides the default HTTP cookie name.
```

* In the listing, you can see the annotations `nginx.ingress.kubernetes.io/affinity` and `nginx.ingress.kubernetes.io/session-cookie-name`

  * The first annotation enables cookie-based session affinity, and the second sets the cookie name

  * The annotation key prefix indicates that these annotations are specific to the Nginx ingress controller and are ignored by other implementations

## Testing the cookie-based session affinity

* If you want to see session affinity in action, first apply the manifest file, wait until the Nginx configuration is updated, and then retrieve the cookie as follows:

```zsh
$ curl -I http://kiada.example.com --resolve kiada.example.com:80:11.22.33.44 HTTP/1.1 200 OK
Date: Mon, 06 Dec 2021 08:58:10 GMT
Content-Type: text/plain
Connection: keep-alive
Set-Cookie: SESSION_COOKIE=1638781091; Path=/; HttpOnly     # ← A

# ← A ▶︎ This is the session cookie that Nginx adds to the HTTP response.
```

* You can now include this cookie in your request by specifying the `Cookie` header:

```zsh
$ curl -H "Cookie: SESSION_COOKIE=1638781091" http://kiada.example.com \ --resolve kiada.example.com:80:11.22.33.44
```

* If you run this command several times, you'll notice that the HTTP request is always forwarded to the same pod, which indicates that the session affinity is using the cookie
