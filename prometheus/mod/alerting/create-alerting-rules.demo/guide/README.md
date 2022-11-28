# Creating Alerting Rules

## The Scenario

After deploying a Prometheus environment to our Kubernetes cluster, the team has decided to test its monitoring capabilities by configuring alerting of our Redis deployment. We have been tasked with writing two alerting rules. The first rule will fire an alert if any of the Redis pods are down for 10 minutes. The second alert will fire if there are no pods available for 1 minute.

## Logging In and Setting up the Environment

Use the IP address and credentials provided on the hands-on lab overview page, and log in with SSH to the server. Once we're in, become `root` first and navigate into `/root/prometheus`. In that directory, there is a `bootstrap.sh` script that we need to execute. Once we've done that, we can use `kubectl` to show running pods. There should be three, and they all should be in a running state. Here are all of those commands:

```zsh
sudo su -
cd /root/prometheus
./bootstrap.sh
kubectl get pods -n monitoring
```

## Create a ConfigMap That Will Be Used to Manage the Alerting Rules

Edit `prometheus-rules-config-map.yml` and add the Redis alerting rules. It should look like this when we're done:

```yaml
apiVersion: v1
kind: ConfigMap
metadata:
  creationTimestamp: null
  name: prometheus-rules-conf
  namespace: monitoring
data:
  redis_rules.yml: |
    groups:
    - name: redis_rules
      rules:
      - record: redis:command_call_duration_seconds_count:rate2m
        expr: sum(irate(redis_command_call_duration_seconds_count[2m])) by (cmd, environment)
      - record: redis:total_requests:rate2m
        expr: rate(redis_commands_processed_total[2m])
  redis_alerts.yml: |
    groups:
    - name: redis_alerts
      rules:
      - alert: RedisServerDown
        expr: redis_up{app="media-redis"} == 0
        for: 10m
        labels:
          severity: critical
        annotations:
          summary: Redis Server {{ $labels.instance }} is down!
      - alert: RedisServerGone
        expr:  absent(redis_up{app="media-redis"})
        for: 1m
        labels:
          severity: critical
        annotations:
          summary: No Redis servers are reporting!
```

## Apply the Changes Made to `prometheus-rules-config-map.yml`

Now, apply the changes that were made to `prometheus-rules-config-map.yml`:

```zsh
kubectl apply -f prometheus-rules-config-map.yml
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

3. In a new browser tab, navigate to the Expression browser:

```
http://<IP>:8080
```

4. Click on the **Alerts** link to verify that the two Redis alerts are showing as green.
