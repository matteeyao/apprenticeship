# 13.3.2 Understanding how the ReplicaSet controller reacts to Pod changes

* You've seen how the controller responds immediately to changes in the ReplicaSet's `replicas` field

  * However, that's not the only way the desired number and the actual number of Pods can differ

  * What if no one touches the ReplicaSet, but the actual number of Pods changes?

  * The ReplicaSet controller's job is to make sure that the number of Pods always matches the specified number

  * Therefore, it should also come into action in this case

## Deleting a Pod managed by a ReplicaSet

* Let's look at what happens if you delete one of the Pods managed by the ReplicaSet

  * Select one and delete it w/ `kubectl delete`:

```zsh
$ kubectl delete pod kiada-z9dp2  # ← A
pod "kiada-z9dp2" deleted

# ← A ▶︎ Replace the Pod name w/ one of your own Pods.
```

* Now list the Pods again:

```zsh
$ kubectl get pods -l app=kiada
NAME          READY   STATUS    RESTARTS    AGE
kiada-dl7vz   2/2     Running   0           34m
kiada-dn9fb   2/2     Running   0           34m
kiada-rfkqb   2/2     Running   0           47s   # ← A

# ← A ▶︎ Newly created Pod.
```

* The Pod you deleted is gone, but a new Pod has appeared to replace the missing Pod

  * The number of Pods again matches the desired number of replicas set in the ReplicaSet object

  * Again, the ReplicaSet controller reacted immediately and reconciled the actual state w/ the desired state

* Even if you delete all kiada Pods, three new ones will appear immediately so that they can serve your users

  * You can see this by running the following command:

```zsh
$ kubectl delete pod -l app=kiada
```

## Creating a Pod that matches the ReplicaSet's label selector

* Just as the ReplicaSet controller creates new Pods when it finds that there are fewer Pods than needed, it also deletes Pods when it finds too many

  * You've already seen this happen when you reduced the desired number of replicas, but what if you manually create a Pod that matches the ReplicaSet's label selector?

  * From the controller's point of view, one of the Pods must disappear

* Let's create a Pod called `one-kiada-too-many`

  * The name doesn't match the prefix that the controller assigns to the ReplicaSet's Pods, but the Pod's labels match the ReplicaSet's label selector

  * You can find the Pod manifest in the file [`pod.one-kiada-too-many.yaml`](pod.one-kiada-too-many.yaml)

  * Apply the manifest w/ `kubectl apply` to create the Pod, and then immediately list the `kiada` Pods as follows:

```zsh
$ kubectl get po -l app=kiada
NAME                  READY   STATUS        RESTARTS  AGE
kiada-jp4vh           0/2     Terminating   0         11m
kiada-r4k9f           2/2     Running       0         11m
kiada-shfgj           2/2     Running       0         11m
one-kiada-too-many    2/2     Running       0         3s    # ← A

# ← A ▶︎ Although you just created this Pod, it's already being removed.
```

* As expected, the ReplicaSet controller deletes the Pod as soon as it detects it

  * The controller doesn't like it whn you create Pods that match the label selector of a ReplicaSet

  * As shown, the name of the Pod doesn't matter

  * Only the Pod's labels matter

## What happens when a node that runs a ReplicaSet's Pod fails?

* In the previous examples, you saw how a ReplicaSet controller reacts when someone tampers w/ the Pods of a ReplicaSet

  * Although these examples do a good job of illustrating how the ReplicaSet controller works, they don't really show the true benefit of using a ReplicaSet to run Pods

  * The best reason to create Pods via a ReplicaSet instead of directly is that the Pods are automatically replaced when your cluster nodes fail

> [!WARNING]
> 
> In the next example, a cluster node is caused to fail. In a poorly configured cluster, this can cause the entire cluster to fail. Therefore, you should only perform this exercise if you're willing to rebuild the cluster from scratch if necessary.

* To see what happens when a node stops responding, you can disable its network interface

  * If you created your cluster w/ the kind tool, you can disable the network interface of the `kind-worker2` node w/ the following command:

```zsh
$ docker exec kind-worker2 ip link set eth0 down
```

> [!NOTE]
> 
> Pick a node that has at least one of your kiada Pods running on it. List the Pods w/ the `-o wide` option to see which node each Pod runs on.

> [!NOTE]
> 
> If you're using GKE, you can log into the node w/ the `gcloud compute ssh` command and shut down its network interface w/ the `sudo ifconfig eth0 down` command. The ssh session will stop responding, so you'll need to close it by pressing Enter, followed by "~." (tilde and dot, w/o the quotes).

* Soon, the status of the Node object representing the cluster node changes to `NotReady`:

```zsh
$ kubectl get node
NAME                  STATUS    ROLES                   AGE   VERSION
kind-control-plane    Ready     control-plane,master    2d3h  v1.21.1
kind-worker           Ready     <none>                  2d3h  v1.21.1
kind-worker2          NotReady  <none>                  2d3h  v1.21.1   # ← A

# ← A ▶︎ This node is no longer online.
```

* This status indicates that the Kubelet running on the node hasn't contacted the API server for some time

  * Since this isn't a clear sign that the node is down, as it could just be a temporary network glitch, this doesn't immediately affect the status of the Pods running on the node

  * They'll continue to show as `Running`

  * However, after a few minutes, K8s realizes that the node is down and marks the Pods for deletion

> [!NOTE]
> 
> The time that elapses between a node becoming unavailable and its Pods being deleted can be configured using the _Taints and Tolerations_ mechanism, which is explained in chapter 23.

* Once the Pods are marked for deletion, the ReplicaSet controller creates new Pods to replace them

  * You can see this in the following output:

```zsh
$ kubectl get pods -l app=kiada -o wide
NAME          READY   STATUS        RESTARTS    AGE   IP            NODE
kiada-ffstj   2/2     Running       0           35s   10.244.1.150  kind-worker   # ← A
kiada-l2r85   2/2     Terminating   0           37m   10.244.2.173  kind-worker2  # ← B
kiada-n98df   2/2     Terminating   0           37m   10.244.2.174  kind-worker2  # ← B
kiada-vnc4b   2/2     Running       0           37m   10.244.1.148  kind-worker
kiada-wkpsn   2/2     Running       0           35s   10.244.1.151  kind-worker   # ← A

# ← A ▶︎ New Pods that were created to replace the ones on the failed node.
# ← B ▶︎ The two Pods on the failed node.
```

* As you can see in the output, the two Pods on the `kind-worker2` node are marked as `Terminating` and have been replaced by two new Pods scheduled to the healthy node `kind-worker`

  * Again, three Pod replicas ar running as specified in the ReplicaSet

* The two Pods that are being deleted remain in the `Terminating` state until the node comes back online

  * In reality, the containers in those Pods are still running b/c the Kubelet on the node can't communicate w/ the API server and therefore doesn't know that they should be terminated

  * However, when the node's network interface comes back online, the Kubelet terminates the containers, and the Pod objects are deleted

  * The following command restores the node's network interface:

```zsh
$ docker exec kind-worker2 ip link set eth0 up
$ docker exec kind-worker2 ip route add default via 172.18.0.1
```

* Your cluster may be using a gateway IP other than `172.18.0.1`

  * To find it, run the following command:

```zsh
$ docker network inspect kind -f '{{ (index .IPAM.Config 0).Gateway }}'
```

> [!NOTE]
> 
> If you're using GKE, you must remotely reset the node w/ the `gcloud compute instances reset <node-name>` command

## When do pods not get replaced?

* The previous sections have demonstrated that the ReplicaSet controller ensures that there are always as many healthy Pods as specified in the ReplicaSet object

  * But is this always the case?

  * Is it possible to get into a state where the number of Pods matches the desired replica count, but the Pods can't provide the service to their clients?

* Remember the liveness and readiness probes?

  * If a container's liveness probe fails, the container is restarted

  * If the probe fails multiple times, there's a significant time delay before the container is restarted

  * This is due to the exponential backoff mechanism explained in chapter 6

  * During the backoff delay, the container isn't in operation

  * However, it's assumed that the container will eventually be back in service

  * If the container fails the readiness rather than the liveness probe, it's also assumed that the problem will eventually be fixed

* For this reason, Pods whose containers continually crash or fail their probes are never automatically deleted, even though the ReplicaSet controller could easily replace them w/ Pods that might run properly

  * Therefore, be aware that a ReplicaSet doesn't guarantee that you'll always have as many healthy replicas as you specify in the ReplicaSet object

* You can see this for yourself by failing one of the Pods' readiness probes w/ the following command:

```zsh
$ kubectl exec rs/kiada -c kiada -- curl -X POST localhost:9901/healthcheck/fail
```

> [!NOTE]
> 
> If you specify the ReplicaSet instead of the Pod name when running the `kubectl exec` command, the specified command is run in one of the Pods, not all of them, just as w/ `kubectl logs`.

* After about thirty seconds, the `kubectl get pods` command indicates that one of the Pod's containers is no longer ready:

```zsh
$ kubectl get pods -l app=kiada
NAME          READY   STATUS    RESTARTS  AGE
kiada-78j7m   1/2     Running   0         21m   # ← A 
kiada-98lmx   2/2     Running   0         21m
kiada-wk99p   2/2     Running   0         21m

# ← A ▶︎ The READY column shows that only one of two containers in the Pod is ready.
```

* The Pod no longer receives any traffic from the clients, but the ReplicaSet controller doesn't delete and replace it, even though it's aware that only two of the three Pods are ready and accessible, as indicated by the ReplicaSet status:

```zsh
$ kubectl get rs
NAME    DESIRED   CURRENT   READY   AGE
kiada   3         3         2       2h    # ← A

# ← A ▶︎ Only two of the three Pods are ready.
```

> [!IMPORTANT]
> 
> A ReplicaSet only ensures that the desired number of Pods are present. It doesn't ensure their containers are actually running and ready to handle traffic.

* If this happens in a real production cluster and the remaining Pods can't handle all the traffic, you'll have to delete the bad Pod yourself

  * But what if you want to find out what's wrong w/ the Pod first?

  * How can you quickly replace the faulty Pod w/o deleting it so you can debug it?

* You could scale the ReplicaSet up by one replica, but then you'll have to scale back down when you finish debugging the Pod

  * See 13.3.3 [Removing a Pod from the ReplicaSet's control](../remove-pod-from-replicaset-control/README.md)
