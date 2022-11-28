# Expression Browser

Prometheus UI is referred to as the Expression Browser.

See the runtime and build information for our Prometheus setup:

**Status** ▶︎ **Runtime & Build Information**

**Status** ▶︎ **Command-Line Flags** are the flags that were used as well as the defaults that were used when we went and created our Prometheus container.

**Status** ▶︎ **Configuration**

**Status** ▶︎ **Targets** displays list of targets we defined using service discovery. Inc.:

  * `kubernetes-apiservers`

  * `kubernetes-cadvisor`

  * `kubernetes-nodes`

  * `kubernetes-service-endpoints`

  * `node-exporter`

**Status** ▶︎ **Rules** displays list of targets we defined using service discovery. Inc.:

**Graph**  ▶︎︎ "- insert metric at cursor -" will show all the metrics we're currently collecting on Prometheus, for instance `container_cpu_load_average_10s`.

Example PromQL query to graph memory usage:

```promql
((sum(node_memory_MemTotal_bytes) - sum(node_memory_MemFree_bytes) - sum(node_memory_Buffers_bytes) - sum(node_memory_Cached_bytes))/sum(node_memory_MemTotal_bytes)) * 100
```
