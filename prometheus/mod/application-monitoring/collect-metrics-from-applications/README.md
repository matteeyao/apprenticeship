# Collecting Metrics from Applications

Node application `dockerfile`:

```dockerfile
FROM node:alpine
RUN mkdir /var/node
WORKDIR /var/node
ADD . /var/node
RUN npm install
EXPOSE 3000
CMD npm start
```

`deployment.yml`:

```yaml
apiVersion: v1
kind: Service
metadata:
  name: combicbox-service
spec:
  selector:
    app: comicbox
  type: NodePort
  ports:
    - port: 3000
      targetPort: 3000
      nodePort: 8001
---
apiVersion: extensions/v1beta1
kind: Deployment
metadata:
  name: comicbox
spec:
  replicas: 1
  template:
    metadata:
      labels:
        app: comicbox
      annotations:
        prometheus.io/scrape: "true"
        prometheus.io/path: "swagger-stats/metrics"
        prometheus.io/port: "3000"
    spec:
      containers:
        - name: comicbox
          image: rivethread42/comicbox
          ports:
            - containerPort: 3000
```

Clone `linuxacademy/content-kubernetes-prometheus-app`:

```zsh
git clone https://github.com/linuxacademy/content-kubernetes-prometheus-app.git
```

Navigate to project we just downloaded:

```zsh
cd content-kubernetes-prometheus-app/
```

Create our docker image and push to Docker Hub:

```zsh
docker build -t rivethead42/comicbox .
```

Login into Docker Hub:

```zsh
docker login
```

```zsh
docker push rivethead42/comicbox
```

Deploy:

```zsh
kubectl apply -f deployment.yml
```

```zsh
kubectl get pods
```

Verify that `comicbox-service` has been created:

```zsh
kubectl get services
```

Available endpoints:

* `<SERVER_ADDRESS>:<PORT>/status`

* `<SERVER_ADDRESS>:<PORT>/swagger-stats/ui`

* `<SERVER_ADDRESS>:<PORT>/swagger-stats/metrics`

* `<SERVER_ADDRESS>:<PORT>/graph`

* Grafana ▶︎

  * Grafana.net Dashboard: 3091

  * Name: swagger-stats dashboard release

  * Prometheus: Prometheus
