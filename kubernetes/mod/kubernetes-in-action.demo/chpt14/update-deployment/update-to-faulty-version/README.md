# 14.2.5 Updating to a faulty version

* When you roll out a new version of an application, you can use the `kubectl rollout pause` command to verify that the Pods running the new version are working as expected before you resume the rollout

  * You can also let K8s do this for you automatically

## Understanding Pod availability

* In chapter 11, you learned what it means for a Pod and its containers to be considered ready

  * However, when you list Deployments w/ `kubectl get deployments`, you see both how many Pods are ready and how many are available

  * For example, during a rolling update, you might see the following output:

```zsh
$ kubectl get deploy kiada
NAME    READY   UP-TO-DATE  AVAILABLE   AGE
kiada   3/3     1           2           50m   # ← A

# ← A ▶︎ Three Pods are ready, but only two are available.
```

* Although three Pods are ready, not all three are available

  * For a Pod to be available, it must be ready for a certain amount of time

  * This time is configurable via the `minReadySeconds` field that we mentioned briefly when introducing the `RollingUpdate` strategy

> [!NOTE]
> 
> A Pod that's ready but not yet available is included in your services and thus receives client requests.

## Delaying Pod availability w/ minReadySeconds

* When a new Pod is created in a rolling update, the Deployment controller waits until the Pod is available before continuing the rollout process

  * By default, the Pod is considered available when it's ready (as indicated by the Pod's readiness probe)

  * If you specify `minReadySeconds`, the Pod isn't considered available until the specified amount of time has elapsed after the Pod is ready

  * If the Pod's containers crash or fail their readiness probe during this time, the timer is reset

* In one of the previous sections, you set `minReadySeconds` to `10` to slow down the rollout so you could track it more easily

  * In practice, you can set this property to a mich higher value to automatically pause the rollout for a longer period after the new Pods are created

  * For example, if you set `minReadySeconds` to `3600`, you ensure that the update won't continue until the first Pods w/ the new version prove that they can operate for a full hour w/o problems

* Although you should obviously test your application in both at test and staging environment before moving it to production, using `minReadySeconds` is like an airbag that helps avoid disaster if a faulty version slips through all the tests

  * The downside is that it slows down the entire rollout, not just the first stage

## Deploying a broken application version

* To see how the combination of a readiness probe and `minReadySeconds` can save you from rolling out a faulty application version, you'll deploy version 0.8 of the Kiada service

  * This is a special version that returns `500 Internal Server Error` responses a while after the process starts

  * The time is configurable via the `FAIL_AFTER_SECONDS` environment variable

* To deploy this version, apply the [`deploy.kiada.0.8.minReadySeconds60.yaml`](deploy.kiada.0.8.minReadySeconds60.yaml) manifest file

  * The relevant parts of the manifest w/ a readiness probe and minReadySeconds:

```yaml
apiVersion: apps/v1
kind: Deployment
...
spec:
  strategy:
    type: RollingUpdate
    rollingUpdate:
      maxSurge: 0
      maxUnavailable: 1
  minReadySeconds: 60                 # ← A
  ...
  template:
    ...
    spec:
      containers:
      - name: kiada
        image: luksa/kiada:0.8        # ← B
        env:
        - name: FAIL_AFTER_SECONDS    # ← C
          value: "30"                 # ← C
        ...
        readinessProbe:               # ← D
          initialDelaySeconds: 0      # ← D
          periodSeconds: 10           # ← D
          failureThreshold: 1         # ← D
          httpGet:                    # ← D
            port: 8080                # ← D
            path: /healthz/ready      # ← D
            scheme: HTTP              # ← D

# ← A ▶︎ Each Pod must be ready for 60s before it is considered available.
# ← B ▶︎ Version 0.8 of the Kiada service is a special version that fails after a given amount of time.
# ← C ▶︎ The application reads this environment variable and fails this many seconds after it starts.
# ← D ▶︎ The readiness probe is configured to run on startup and then every 10 seconds.
```

* As you can see in the listing, `minReadySeconds` is set to `60`, whereas `FAIL_AFTER_SECONDS` is set to `30`

  * The readiness probe runs every `10` seconds

  * The first Pod created in the rolling update process runs smoothly for the first thirty seconds

  * It's marked ready and therefore receives client requests

  * But after the 30 seconds, those requests and the requests made as part of the readiness probe fail

  * The Pod is marked as not ready and is never considered available due to the `minReadySeconds` setting

  * This causes the rolling update to stop

* Initially, some responses that clients receive will be sent by the new version

  * Then, some requests will fail, but soon afterward, all responses will come from the old version again

* Setting `minReadySeconds` to `60` minimizes the negative impact of the faulty version

  * Had you not set `minReadySeconds`, the new Pod would have been considered available immediately and the rollout would have replaced all the old Pods w/ the new version

  * All these new Pods would soon fail, resulting in a complete service outage

  * If you'd like to see this yourself, you can try applying the [`deploy.kiada.0.8.minReadySeconds0.yaml`](deploy.kiada.0.8.minReadySeconds0.yaml) manifest file later

  * But first, let's see what happens when the rollout gets stuck for a long time

## Checking whether the rollout is progressing

* The Deployment object indicates whether the rollout process is progressing via the `Progressing` condition, which you can find in the object's `status.conditions` list

  * If no progress is made for 10 minutes, the status of this condition changes to `false` and the reason changes to `ProgressDeadlineExceeded`

  * You can see this by running the `kubectl describe` command as follows:

```zsh
$ kubectl describe deploy kiada
...
Conditions:
  Type          Status  Reason
  ----          ------  ------
  Available     True    MinimumReplicasAvailable
  Progressing   False   ProgressDeadlineExceeded    # ← A

# ← A ▶︎ The deployment process is not progressing.
```

> [!NOTE]
> 
> You can configure a different progress deadline by setting the `spec.progressDeadlineSeconds` field in the Deployment object. If you increase `minReadySeconds` to more than `600`, you must set the `progressDeadlineSeconds` field accordingly.

* If you run the `kubectl rollout status` command after you trigger the update, it prints a message that the progress deadline has been exceeded, and terminates

```zsh
$ kubectl rollout status deploy kiada
Waiting for "kiada" rollout to finish: 1 out of 3 new replicas have been updated...
error: deployment "kiada" exceeded its progress deadline
```

* Other than reporting that the rollout has stalled, K8s takes no further action

  * The rollout process never stops completely

  * If the Pod becomes ready and remains so for the duration of `minReadySeconds`, the rollout process continues

  * If the Pod never becomes ready again, the rollout process simply doesn't continue

  * You can cancel the rollout as explained in the next section [Rolling back a Deployment](../rollback-deployment/README.md)
