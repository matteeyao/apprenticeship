# Configuring Prometheus

W/ our Kubernetes cluster setup and Prometheus successfully installed on the cluster, we can configure Prometheus.

We can configure Prometheus by using command line flags or by creating a configuration file.

* `scrape_interval`: how often Prometheus will go and scrape endpoints.

* `scrape_timeout`: how long a scrape should take before Prometheus goes and times out. The default is set to 10s.

* `evaluation_interval`: how often we should go and evaluate rules that are being set within Prometheus.

* `scrape_config`: includes `job_name`, `scrape_interval`, `scrape_timeout`

* `metrics_path`: the HTTP resource path which Prometheus is going to fetch the metrics from. The default value is `/metrics`.

* `scheme`: the protocol to access the data ▶︎ `http` or `https`.

* `static_config`: allows us to specify targets that Prometheus will be going and scraping. The private IP addresses of Kubernetes nodes will be specified here.

    * `targets`: YAML array of the hosts that we're going to be scraping. Depending on how your target is configured, it could just be the IP or it could be the IP and the port.

* `labels`: key-value pairs of the label name along w/ the label value.

* `alerting`: where we will define our alert manager, configuration data. This is what will send us a notification once an alerting rule has gone and fired.

## Service Discovery

`<kubernetes_sd_config>`

When we use Service Discovery, we are configuring our targets by using `kubernetes_rest_api`.

```yaml
- job_name: kubernetes-apiservers
  scrape-interval: 5s
  scrape_timeout: 5s
  metrics_path: /metrics
  scheme: https
  kubernetes_sd_configs:
  - api_server: null
    role: endpoints
    namespaces:
      names: []
  bearer_token_file: /var/run/secrets/kubernetes.io/serviceaccount/token
  tls_config:
    ca_file: /var/run/secrets/kubernetes.io/serviceaccount/ca.crt
    insecure_skip_verify: false
  relabel_configs:
    - source_labels: [__meta_kubernetes_namespace, __meta_kubernetes_service_name, __meta_kubernetes_endpoint_name]
      separator: ;
      regex: default;kubernetes;https
      replacement: $1
      action: keep
...
- job_name: kubernetes-cadvisor
  scrape_interval: 5s
  scrape_timeout: 5s
  metrics_path: /metrics
  scheme: https
  kubernetes_sd_configs:
  - api_server: null
    role: node
    namespaces:
      names: []
  bearer_token_file: /var/run/secrets/kubernetes.io/serviceaccount/token
  tls_config:
    ca_file: /var/run/secrets/kubernetes.io/serviceaccount/ca.crt
    insecure_skip_verify: false
  relabel_configs:
  - separator: ;
    regex: __meta_kubernetes_node_label_(.+)
    replacement: $1
    action: labelmap
  - separator: ;
    regex: (.*)
    target_label: __address__
    replacement: kubernetes.default.svc:443
    action: replace
  - source_labels: [__meta_kubernetes_node_name]
    separator: ;
    regex: (.+)
    target_label: __metrics_path__
    replacement: /api/v1/nodes/${1}/metrics/cadvisor
    action: replace
...
```

Kubernetes roles you need to be aware of:

* `node`

* `endpoint`

* `service`

* `pod`

* `ingress`

Prometheus will go and use the `kubernetes_api` to go and discover these targets. Each of these roles are going to have their own metadata or meta labels. Some of these will be defined within the `source_labels: __meta_kubernetes_namespace`. Each role we have will have metadata associated w/ it.

The `tls_config` will contain the path to your `kubernetes_ca_cert` file, which is going to be located within `/var/run/secrets/kubernetes.io/serviceaccount/ca.crt`.

Under `relabel_configs`, `source_labels` select values from existing labels. Their content is going to be concatenated using the configured separator, and matched against the configured regular expression and the action will be either `replace`, `keep`, or `drop`.

This allows us to filter targets from `kubernetes-apiservers` or `kubernetes-cadvisor`. For example, for `kubernetes-cadvisor` we're replacing the IP address w/ `kubernetes.default.svd:443`. We are then looking for `__metrics_path__`. Using the `source_label`, we're going to find the node name and replace the metrics path with it. So `${1}` will actually equal the node name. We're going to replace our `target_label` which is `__metrics_path__`, which translates over to `/metrics` w/ `/api/v1/nodes/${1}/metrics/cadvisor` or `/api/v1/nodes/<NODE_NAME>/metrics/cadvisor`.
