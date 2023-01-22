# 5.4 Running multiple containers in a pod

* As of now, the Kiada application you deployed only supports HTTP

  * Add TLS support so it can also serve clients over HTTPS

  * You could do this by adding code to the `app.js` file, but an easier option exists where you don't need to touch the code

* You can run a reverse proxy alongside the Node.js application in a sidecar container and let it handle HTTPS requests on behalf of the application

  * A popular software package that can provide this functionality is _Envoy_

  * The Envoy proxy is a high-performance open source service proxy originally built by Lyft tha has since been contributed to the Cloud Native Computing Foundation

    * Let's add it to our pod
