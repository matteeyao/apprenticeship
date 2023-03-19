# Accessing cluster-internal services

* The `ClusterIP` services you created in the previous section are accessible only within the cluster, from other pods and from the cluster nodes

  * You can't access them from your own machine

  * To see if a service is actually working, you must either log in to one of the nodes w/ `ssh` and connect to the service from there, or use the `kubectl exec` command to run a command like `curl` in an existing pod and get it to connect to the service

> [!NOTE]
> 
> You can also use the `kubectl port-forward svc/my-service` command to connect to one of the pods backing the service. However, this command doesn't connect to the service. It only uses the Service object to find a pod to connect to. The connection is then made directly to the pod, bypassing the service.

## Connecting to services from pods

* To use the service from a pod, run a shell in the `quote-001` pod as follows:

```zsh
$ kubectl exec -it quote-001 -c nginx -- sh
/#
```

* Now check if you can access the two services

  * Use the cluster IP addresses of the services that `kubectl get services` displays

  * In my case, the `quiz` service uses cluster IP `10.96.136.190`, whereas the `quote` service uses IP `10.96.74.151`

  * From the `quote-001` pod, we can connect to the two services as follows:

```zsh
/ # curl http://10.96.136.190                           # ← A
This is the quiz service running in pod quiz

/ # curl http://10.96.74.151                            # ← B
This is the quote service running in pod quote-canary

# ← A ▶︎ This is the cluster IP of the quiz service, as shown by `kubectl get services`.
# ← B ▶︎ This is the cluster IP of the quote service.
```

> [!NOTE]
> 
> You don't need to specify the port in the curl command, b/c you set the service port to 80, which is the default for HTTP.

* If you repeat the last command several times, you'll see that the service forwards the request to a different pod each time:

```zsh
/ # while true; do curl http://10.96.74.151; done
This is the quote service running in pod quote-canary
This is the quote service running in pod quote-003
This is the quote service running in pod quote-001
...
```

* The service acts as a load balancer

  * It distributes requests to all the pods that are behind it

> ### Configuring session affinity on services
> 
> * You can configure whether the service should forward each connection to a different pod, or whether it should forward all connections from the same client to the same pod
> 
>   * You do this via the `spec.sessionAffinity` field in the Service object
> 
>   * Only two types of service session affinity are supported: `None` and `ClientIP`
> 
> * The default type is `None`, which means there's no guarantee to which pod each connection will be forwarded
> 
>   * However, if you set the value to `ClientIP`, all connections originating from the same IP will be forwarded to the same pod
> 
>   * In the `spec.sessionAffinityConfig.clientIP.timeoutSeconds` field, you can specify how long the session will persist
> 
>   * The default value is 3 hours
> 
> * It may surprise you to learn that K8s doesn't provide cookie-based session affinity
> 
>   * However, considering that K8s services operate at the transport layer of the OSI network model (UDP and TCP) not at the application layer (HTTP), they don't understand HTTP cookies at all

## Resolving services via DNS

* Kubernetes clusters typically run an internal DNS server that all pods in the cluster are configured to use

  * In most clusters, this internal DNS service is provided by CoreDNS, whereas some clusters use kube-dns

  * You can see which one is deployed in your cluster by listing the pods in the `kube-system` namespace

* No matter which implementation runs in your cluster, it allows pods to resolve the cluster IP address of a service by name

  * Using the cluster DNS, pods can therefore connect to the `quiz` service like so:

```zsh
/ # curl http://quiz                          # ← A
This is the quiz service running in pod quiz

# ← A ▶︎ The service name is used instead of cluster IP.
```

* A pod can resolve any service defined in the same namespace as the pod by simply pointing to the name of the service in the URL

  * If a pod needs to connect to a service in a different namespace, it must append the namespace of the Service object to the URL

  * For example, to connect to the `quiz` service in the `kiada` namespace, a pod can use the URL `http://quiz.kiada/` regardless of which namespace it's in

* From the `quote-001` pod where you ran the shell command, you can also connect to the service as follows:

```zsh
/ # curl http://quiz.kiada                    # ← A
This is the quiz service running in pod quiz

# ← A ▶︎ The name of the service is quiz; kiada is the namespace.
```

* A service is resolvable under the following DNS names:

  * `<service-name>`, if the service is in the same namespace as the pod performing the DNS lookup,

  * `<service-name>.<service-namespace>` from any namespace, but also under

  * `<service-name>.<service-namespace>.svc`, and

  * `<service-name>.<service-namespace>.svc.cluster.local`

> [!NOTE]
> 
> The default domain suffix is `cluster.local` but can be changed at the cluster level.

* The reason you don't need to specify the fully qualified domain name (FQDN) when resolving the service through DNS is b/c of the `search` line in the pod's `/etc/resolv.conf` file

  * For the `quote-001` pod, the file looks like this:

```zsh
/ # cat /etc/resolv.conf
search kiada.svc.cluster.local svc.cluster.local cluster.local localdomain
nameserver 10.96.0.10
options ndots:5
```

* When you try to resolve a service, the domain names specified in the `search` field are appended to the name until a match is found

  * If you're wondering what the IP address is in the `nameserver` line, you can list all the services in your cluster to find out:

```zsh
$ kubectl get svc -A
NAMESPACE     NAME          TYPE        Cluster-IP      EXTERNAL-IP   PORT(S)
default       kubernetes    ClusterIP   10.96.0.1       <none>        443/TCP
kiada         quiz          ClusterIP   10.96.136.190   <none>        80/TCP
kiada         quote         ClusterIP   10.96.74.151    <none>        80/TCP
kube-system   kube-dns      ClusterIP   10.96.0.10      <none>        53/UDP...    # ← A

# ← A ▶︎ Here's the IP address you're looking for.
```

* The nameserver in the pod's `resolv.conf` file points to the `kube-dns` service in the `kube-system` namespace

  * This is the cluster DNS service that the pods use

  * As an exercise, try to figure out which pod(s) this service forwards traffic to

> ### Configuring the pod's DNS policy
> 
> * Whether a pod uses the internal DNS server can be configured using the `dnsPolicy` field in the pod's `spec`
> 
>   * The default value is `ClusterFirst`, which means that the pod uses the internal DNS first and then the DNS configured for the cluster node
> 
>   * Other valid values are `Default` (use the DNS configured for the node), `None` (no DNS configuration is provided by Kubernetes; you must configure the pod's DNS settings using the `dnsConfig` field explained in the next paragraph), and `ClusterFirstWithHostNet` (for special pods that use the host's network instead of their own-this is explained later in the book)
> 
> * Setting the `dnsPolicy` field affects how Kubernetes configures the pod's `resolv.conf` file
> 
>   * You can further customize this file through the pod's `dnsConfig` field
> 
>   * The [`pod-with-dns-options.yaml`](pod-with-dns-config.yaml) file demonstrates the use of this field

## Discovering services through environment variables

* Virtually every K8s cluster offers the cluster DNS service

* When a container is started, K8s initializes a set of environment variables for each service that exists in the pod's namespace

  * Let's see what these environment variables look like by looking at the environment of one of your running pods

* Since you created your pods before the services, you won't see any environment variables related to the services except those fo the `kubernetes` service, which exists in the `default` namespace

> [!NOTE]
> 
> The `kubernetes` service forwards traffic to the API server.

* To see the environment variables for the two services that you created, you must restart the container as follows:

```zsh
$ kubectl exec quote-001 -c nginx -- kill 1
```

* When the container is restarted, its environment variables contain the entries for the `quiz` and `quote` services

  * Display them w/ the following command:

```zsh
$ kubectl exec -it quote-001 -c nginx -- env | sort
...
QUIZ_PORT_80_TCP_ADDR=10.96.136.190                     # ← A
QUIZ_PORT_80_TCP_PORT=80                                # ← A
QUIZ_PORT_80_TCP_PROTO=tcp                              # ← A
QUIZ_PORT_80_TCP=tcp://10.96.136.190:80                 # ← A
QUIZ_PORT=tcp://10.96.136.190:80                        # ← A
QUIZ_SERVICE_HOST=10.96.136.190                         # ← A
QUIZ_SERVICE_PORT=80                                    # ← A
QUOTE_PORT_80_TCP_ADDR=10.96.74.151                     # ← B
QUOTE_PORT_80_TCP_PORT=80                               # ← B
QUOTE_PORT_80_TCP_PROTO=tcp                             # ← B
QUOTE_PORT_80_TCP=tcp://10.96.74.151:80                 # ← B
QUOTE_PORT=tcp://10.96.74.151:80                        # ← B
QUOTE_SERVICE_HOST=10.96.74.151                         # ← B
QUOTE_SERVICE_PORT=80                                   # ← B

# ← A ▶︎ The environment variables describing the quiz service
# ← B ▶︎ The environment variables describing the quote service
```

* For services w/ multiple ports, the number of variables is even larger

  * An application running in a container can use these variables to find the IP address and port(s) of a particular service

> [!NOTE]
> 
> In the environment variable names, the hyphens in the service name are converted to underscores and all letters are uppercased.

* Nowadays, applications usually get this information through DNS, so these environment variables aren't as useful as in the early days

  * They can even cause problems

  * If the number of services in a namespace is too large, any pod you create in that namespace will fail to start

  * The container exits w/ exit code 1 and you see the following error message in the container's log:

```zsh
standard_init_linux.go:228: exec user process caused: argument list too long
```

* To prevent this, you can disable the injection of service information into the environment by setting the `enableServiceLinks` field in the pod's `spec` to `false`

## Understanding why you can't ping a service IP

* You've learned how to verify that a service is forwarding traffic to your pods

* But what if it doesn't?

  * In that case, you might want to try pinging the service's IP

  * Why don't you try that right now?

  * Ping the `quiz` service from the `quote-001` pod as follows:

```zsh
$ kubectl exec -it quote-001 -c nginx -- ping quiz
PING quiz (10.96.136.190): 56 data bytes
^C
--- quiz ping statistics ---
15 packets transmitted, 0 packets received, 100% packet loss
command terminated with exit code 1
```

* Wait a few seconds and then interrupt the process by pressing **Control-C**

  * As you can see, the IP address was resolved correctly, but none of the packets got through

  * This is b/c the IP address of the service is virtual and has meaning only in conjunction w/ one of the ports defined in the service

  * For now, remember that you can't ping services

## Using services in a pod

* Now that you know that the Quiz and Quote services are accessible from pods, you can deploy the Kiada pods and configure them to use the two services

  * The application expects the URLs of these services in the environment variables `QUIZ_URL` and `QUOTE_URL`

  * These aren't environment variables that K8s adds on its own, but variables that you set manually so that the application knows where to find the two services

  * Therefore, the `env` field of the `kiada` container must be configured as in the following listing ▶︎ Configuring the service URLs in the kiada pod:

```yaml
...
    env:
    - name: QUOTE_URL             # ← A
      value: http://quote/quote   # ← A
    - name: QUIZ_URL              # ← B
      value: http://quiz          # ← B
    - name: POD_NAME
      ....

# ← A ▶︎ The URL where the Quote service returns a quote from the book.
# ← B ▶︎ The base URL of the Quiz service.
```

* The environment variable `QUOTE_URL` is set to `http://quote/quote`

  * The hostname is the same as the name of the service you created in the previous section

  * Similarly, `QUIZ_URL` is set to `http://quiz`, where `quiz` is the name of the other service you created

* Deploy the Kiada pods by applying the manifest file `kiada-stable-and-canary.yaml` to your cluster using `kubectl apply`

  * Then run the following command to open a tunnel to one of the pods you just created:

```zsh
$ kubectl port-forward kiada-001 8080 8443
```

* You can now test the application at http://localhost:8080 or https://localhost:8443

  * If you use `curl`, you should see a response like the following:

```zsh
$ curl http://localhost:8080
==== TIP OF THE MINUTE
Kubectl options that take a value can be specified with an equal sign or with a space.
    Instead of -tail=10, you can also type --tail 10.
==== POP QUIZ
First question
0) First answer
1) Second answer
2) Third answer

Submit your answer to /question/1/answers/<index of answer> using the POST method.

==== REQUEST INFO
Request processed by Kubia 1.0 running in pod "kiada-001" on node "kind-worker2".
Pod hostname: kiada-001; Pod IP: 10.244.1.90; Node IP: 172.18.0.2; Client IP:
       ::ffff:127.0.0.1
       
HTML version of this content is available at /html
```

* On the browser, if you can see the quote and quiz question, it means that the `kiada-001` pod is able to communicate w/ the `quote` and `quiz` services

  * If you check the logs of the pods that back these services, you'll see that they are receiving requests

  * In the case of the `quote` service, which is backed by multiple pods, you'll see that each request is sent to a different pod
