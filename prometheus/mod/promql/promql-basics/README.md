# PromQL Basics

## Expression language data types

In Prometheus's expression language, an expression or sub-expression can evaluate to one of four types:

* **Instant vector** ▶︎ a set of time series containing a single sample for each time series, all sharing the same timestamp

* **Range vector** ▶︎ a set of time series containing a range of data points over time for each time series

* **Scalar** ▶︎ a simple numeric floating point value

* **String** ▶︎ a simple string value; currently unused

Depending on the use-case (e.g. when graphing vs/ displaying the output of an expression), only some of these types are legal as the result from a user-specified expression. For example, an expression that returns an instant vector is the only type that can be directly graphed.

All the metrics that we can query for are determined by the exporters that we're using and client libraries for our applications. Everything that is being ingested in, we can find in the `- insert metric at cursor - ` dropdown.

Example PromQL query using time range:

```
node_cpu_seconds_total{job="node-exporter", mode="idle"}[5m]
```

Example PromQL query using regex:

```
node_cpu_seconds_total{job=~".*-exporter"}
```

Example PromQL query filtering for job:

```zsh
container_cpu_load_average_10s{job=~""}
```