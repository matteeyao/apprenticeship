# Troubleshooting K8s Networking Issues

## kube-proxy and DNS

In addition to checking on your K8s networking plugin, it may be a good idea to look at kube-proxy and the K8s DNS if you are experiencing issues within the K8s cluster network.

In a kubeadm cluster, the K8s DNS and kube-proxy run as Pods in the `kube-system` namespace.

## netshoot

> [!TIP]
> 
> You can run a container in the cluster that you can use to run commands to test and gather information about network functionality.

The `nicolaka/netshoot` image is a great tool for this. This image contains a variety of networking exploration and troubleshooting tools.

Create a container running this image, and then use `kubectl exec` to explore away.

## Hands-on demonstration

Log into the control plane node.

```zsh
kubectl get pods -n kube-system
```

Grab the logs for `kube-proxy` pod:

```zsh
kubectl logs -n kube-system kube-proxy-4mpxf
```

```zsh
kubectl get pods -n kube-system
```

Let's take a look at the netshoot troubleshooting tool:

```zsh
vi nginx-netshoot.yml
```

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: nginx-netshoot
  labels:
    app: nginx-netshoot
spec:
  containers:
  - name: nginx
    image: nginx:1.19.1

---

apiVersion: v1
kind: Service
metadata:
  name: svc-netshoot
spec:
  type: ClusterIP
  selector:
    app: nginx-netshoot
  ports:
    - protocol: TCP
      port: 80
      targetPort: 80
```

```zsh
kubectl create -f nginx-netshoot.yml
```

```zsh
vi netshoot.yml
```

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: netshoot
spec:
  containers: 
  - name: netshoot
    image: nicolaka/netshoot
    command: ['sh', '-c', 'while true; do sleep 5l done']
```

```zsh
kubectl create -f netshoot.yml
```

```zsh
kubectl get pods
```

Create an interactive shell to the netshoot pod:

```zsh
kubectl exec --stdin --tty netshoot -- /bin/sh
/ # curl svc-netshoot
/ # ping svc-netshoot
/ # nslookup svc-netshoot
```
