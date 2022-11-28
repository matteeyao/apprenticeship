# Adding a Grafana Dashboard

Import the `grafana/dashboard/Kubernetes All Nodes.json`:

```zsh
git clone https://github.com/linuxacademy/content-kubernetes-prometheus-env.git
```

Execute `wget`:

```zsh
wget https://raw.githubusercontent.com/linuxacademy/content-kubernetes-prometheus-env/master/grafana/dashboard/Kubernetes%20All%20Nodes.json
```

Got to Grafana endpoint and click on **Upload json**.

Set Name to "Kubernetes All Nodes" and Prometheus to "Prometheus" data set we created in the **expression-browser** module.
