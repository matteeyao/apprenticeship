# Ingress Controllers

In order for the Ingress resource to work, the cluster must have an ingress controller running.

Unlike other types of controllers which run as part of the `kube-controller-manager` binary, Ingress controllers are not started automatically with a cluster.

## Getting metrics for your service

You probably want to collect metrics about your service's latency, number of HTTP 200/400/500-class responses, etc. You can instrument your server with something like Prometheus or OpenMetrics or Honeycomb yourself. That works OK, but you'll run into two problems:

1. When your service goes down, you can't get metrics from it

2. You'll be writing the same metrics in every service you ever deploy. This becomes repetitive and boilerplaty.

Instead, most IngressControllers will collect a bunch of metrics. For example, Contour will monitor every HTTP request that it forwards to your service, and track the latency and the HTTP status. It then exposes these in Prometheus metrics that you can scrape and graph. This is really convenient, because you save time by not implementing these metrics yourself. And when your service goes down, you'll know, because you'll see the Contour metrics for this namespace/service showing spikes in latency or HTTP 5xx responses.
