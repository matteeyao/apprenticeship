# PromQL Operations and Functions

## Binary operators

Prometheus's query language supports basic logical and arithmetic operators. For operations between two instant vectors, the matching behavior can be modified.

### Arithmetic binary operators

The following binary arithmetic operators exist in Prometheus:

* `+` (addition)

* `-` (subtraction)

* `*` (multiplication)

* `/` (division)

* `%` (modulo)

* `^` (power/exponentiation)

Binary arithmetic operators are defined between scalar/scalar, vector/scalar, and vector/vector value pairs.

**Between two scalars,** the behavior is obvious: they evaluate to another scalar that is the result of the operator applied to both scalar operands.

**Between an instant vector and a scalar,** the operator is applied to the value of every data sample in the vector. E.g. if a time series instant vector is multiplied by 2, the result is another vector in which every sample value of the original vector is multiplied by 2.

**Between two instant vectors,** a binary arithmetic operator is applied to each entry in the left-hand side vector and its matching element in the right-hand vector. The result is propagated into the result vector and the metric name is dropped. Entries for which no matching entry in the right-hand vector can be found are not part of the result.

## Aggregation operators

Prometheus supports the following built-in aggregation operators that can be used to aggregate the elements of a single instant vector, resulting in a new vector of fewer elements w/ aggregated values:

* `sum` (calculate sum over dimensions)

* `min` (select minimum over dimensions)

* `max` (select maximum over dimensions)

* `avg` (calculate the average over dimensions)

* `stddev` (calculate population standard deviation over dimensions)

* `stdvar` (calculate population standard variance over dimensions)

* `count` (count number of elements in the vector)

* `count_values` (count number of elements w/ the same value)

* `bottomk` (smallest k elements by sample value)

* `topk` (largest k elements by sample value)

* `quantile`

These operators can either be used to aggregate over **all** label dimensions or preserve distinct dimensions by including a `without` or `by` clause.

```
<aggr-op>([parameter,] <vector expression>) [without|by (<label list>)]
```

`parameter` is only required for `count_values`, `quantile`, `topk`, and `bottomk`. `without` removes the listed labels from the result vector, while all other labels are preserved the output. `by` does the opposite and drops labels that are not listed in the `by` clause, even if their label values are identical between all elements of the vector.

`count_values` outputs one time series per unique sample value. Each series has an additional label. The name of that label is given by the aggregation parameter, and the label value is the unique sample value. The value of each time series is the number of times that sample value was present.

`topk` and `bottomk` are different from other aggregators in that a subset of the input samples, including the original labels, are returned in the result vector. `by` and `without` are only used to bucket the input vector.

Let's say we want to get the total memory for our cluster, not just on an individual node basis. That's where the `sum` execution aggregator comes into play:

```
sum(node_memory_MemTotal_bytes)
```

Let's get the total percentage of RAM.

```
((sum(node_memory_MemTotal_bytes) - sum(node_memory_MemFree_bytes) - sum(node_memory_Buffers_bytes) - sum(node_memory_Cached_bytes)) / sum(node_memory_MemTotal_bytes)) * 100
```

Above, we subtract sum amount of free memory in bytes, amount of buffer, and cache memory from the sum of the total amount of memory free, which includes both of our nodes. We divide everything by the total memory.

## Functions

### `irate()`

`irate(v range-vector)` calculates the per-second instant rate of increase of the time series in the range vector. This is based on the last two data points. Breaks in monotonicity (such as counter resets due to target restarts) are automatically adjusted for.

The following example expression returns the per-second rate of HTTP requests looking up to 5 minutes back for the two most recent data points, per time series in the range vector:

```
irate(http_requests_total{job="api-server"}[5m])
```

`irate` should only be used when graphing volatile, fast-moving counters. Use `rate` for alerts and slow-moving counters, as brief changes in the rate can reset the `FOR` clause and graphs consisting entirely of rare spikes are hard to read.

Note that when combining `irate()` w/ an aggregation operator (e.g. `sum()`) or a function aggregating over time (any function ending in `_over_time`), alwats take a `irate()` first, then aggregate. Otherwise `irate()` cannot detect counter resets when your target restarts.

Example `irate()` function:

```
irate(node_cpu_seconds_total{job="node-exporter",mode="idle"}[5m]
```

Let's add an aggregation operator:

```
avg(irate(node_cpu_seconds_total{job="node-exporter",mode="idle"}[5m])
```

We can also group our query. Below we group by instance:

```
avg(irate(node_cpu_seconds_total{job="node-exporter",mode="idle"}[5m]) by (instance)
```
