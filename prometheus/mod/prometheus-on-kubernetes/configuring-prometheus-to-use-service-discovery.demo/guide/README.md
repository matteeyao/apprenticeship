# Configuring Prometheus to Use Service Discovery

## The Scenario

Recently, our team has deployed Prometheus to the company's Kubernetes cluster. Now it is time to use service discovery to find targets for cAdvisor and the Kubernetes API. We have been tasked with modifying the Prometheus Configuration Map that is used to create the `prometheus.yml` file. Create the scrape configuration and add the jobs for `kubernetes-apiservers` and `kubernetes-cadvisor`. Then, propagate the changes to the Prometheus pod.

## Logging In and Setting up the Environment

Use the IP address and credentials provided on the hands-on lab overview page, and log in with SSH to the server. Once we're in, become `root` right off and navigate into `/root/prometheus`. In that directory, there is a `bootstrap.sh` script that we need to execute. Once we've done that, we can use `kubectl` to show running pods. There should be two, and they both should be in a running state. Here are all of those commands:

```zsh
sudo su -
cd /root/prometheus
./bootstrap.sh
kubectl get pods -n monitoring
```

## Configure the Service Discovery Targets

Edit `prometheus-config-map.yml` and add in the two service discovery targets:

```zsh
vi prometheus-config-map.yml
```

When we're done, the whole file should look like this:

```yaml
apiVersion: v1
kind: ConfigMap
metadata:
  name: prometheus-server-conf
  labels:
    name: prometheus-server-conf
  namespace: monitoring
data:
  prometheus.yml: |-
    global:
      scrape_interval: 5s
      evaluation_interval: 5s

    scrape_configs:
      - job_name: 'kubernetes-apiservers'

        kubernetes_sd_configs:
        - role: endpoints
        scheme: https

        tls_config:
          ca_file: /var/run/secrets/kubernetes.io/serviceaccount/ca.crt
        bearer_token_file: /var/run/secrets/kubernetes.io/serviceaccount/token

        relabel_configs:
        - source_labels: [__meta_kubernetes_namespace, __meta_kubernetes_service_name, __meta_kubernetes_endpoint_port_name]
          action: keep
          regex: default;kubernetes;https

      - job_name: 'kubernetes-cadvisor'

        scheme: https

        tls_config:
          ca_file: /var/run/secrets/kubernetes.io/serviceaccount/ca.crt
        bearer_token_file: /var/run/secrets/kubernetes.io/serviceaccount/token

        kubernetes_sd_configs:
        - role: node

        relabel_configs:
        - action: labelmap
          regex: __meta_kubernetes_node_label_(.+)
        - target_label: __address__
          replacement: kubernetes.default.svc:443
        - source_labels: [__meta_kubernetes_node_name]
          regex: (.+)
          target_label: __metrics_path__
          replacement: /api/v1/nodes/${1}/proxy/metrics/cadvisor
```

* `kubernetes_sd_configs` corresponds to Service Discovery

## Apply the Changes to the Prometheus Configuration Map

Now, apply the changes that were made to `prometheus-config-map.yml`:

```zsh
kubectl apply -f prometheus-config-map.yml
```

## Delete the Prometheus Pod

1. List the pods to find the name of the Prometheus pod:

```zsh
kubectl get pods -n monitoring
```

2. Delete the Prometheus pod:

```zsh
kubectl delete pods <POD_NAME> -n monitoring
```

3. Open up a new web browser tab, and navigate to the Expression browser. This will be at the public IP of the lab server, on port 8080:

```zsh
http://<IP>:8080
```

4. Click on **Status**, and select **Target** from the dropdown. We should see two targets in there.
