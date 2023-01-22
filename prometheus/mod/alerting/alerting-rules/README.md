 # Alerting Rules

Previously, we discussed AlertManager. In this module, we're going to stand up a Redis pod to resolve this alert.

Alerting rules allow you to define alert condition based on Prometheus expression language expressions and to send notifications about firing alerts to an external service. Whenever the alert expression results in one or more vector elements at a given point in time, the alert counts as active for these elements' label sets.

## Defining alerting rules

Alerting rules are configured in Prometheus the same way as recording rules.

An example rules file w/ an alert would be:

```yaml
groups:
- name: example
  rules:
  - alert: HighErrorRate
    expr: job:request_latency_seconds:mean5m{job="myjob"} > 0.5
    for: 10m
    labels:
      severity: page
    annotations:
      summary: High request latency
```

The optional `for` clause causes Prometheus to wait for a certain duration between first encountering a new expression output vector element and counting an alert as firing for this element. In this case, Prometheus will check that the alert continues to be active during each evaluation for 10 minutes before firing the alert. Elements that are active, but not firing yet, are in the pending state.

The `labels` clause allows specifying a set of additional labels to be attached to the alert. Any existing conflicting labels will be overwritten. The label values can be templated.

Be aware that you are able to match a severity level w/ a routing rule, which can be sent to a ticketing system, an email, or page someone.

The `annotations` clause specifies a set of informational labels that can be used to store longer additional information such as alert descriptions or runbook links. The annotation values can be templated.

Next, navigate to `content-kubnetes-prometheus-env/alertmanager/` and take a look at `prometheus-rules-config-map.yml` ▶︎ [YAML file](https://github.com/linuxacademy/content-kubernetes-prometheus-env/blob/master/alertmanager/prometheus-rules-config-map.yml)

```yaml
...
    - alert: RedisServerGone
      expr: absent(redis_up{app="media-redis"})
      for: 1m
      labels:
        severity: critical
      annotations:
        summary: No Redis servers are reporting!
        description: Werner Heisenberg says - there is no uncertainty about the Redis server being gone.
...
```

Let's deploy a Redis pod by running...

```zsh
kubectl apply -f redis.yml
```

... on [redis.yml](https://github.com/linuxacademy/content-kubernetes-prometheus-env/blob/master/redis/redis.yml).
