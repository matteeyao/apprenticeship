# Configuring TLS for an Ingress

* So far in this chapter, you've used the Ingress object to allow external HTTP traffic to your services

  * These days, however, you usually want to secure at least all external traffic w/ SSL/TLS

* You may recall that the `kiada` service provides both an HTTP and an HTTPS port

  * When you created the Ingress, you only configured it to forward HTTP traffic to the service, but not HTTPS

  * You'll do this now

* There are two ways to add HTTPS support:

  * You can either allow HTTPS to pass through the ingress proxy and have the backend terminate the TLS connection, or have the proxy terminate and connect to the backend pod through HTTP

## 12.3.1 [Configuring the Ingress for TLS passthrough](ingress-for-tls-passthrough/README.md)

## 12.3.2 [Terminating TLS at the ingress](terminate-tls-at-ingress/README.md)
