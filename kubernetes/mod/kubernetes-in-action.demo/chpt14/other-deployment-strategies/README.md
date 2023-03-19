# 14.3 Implementing other deployment strategies

* In the previous sections, you learned how the `Recreate` and `RollingUpdate` strategies work

  * Although these are the only strategies supported by the Deployment controller, you can also implement other well-known strategies, but w/ a little more effort

  * You can do this manually or have a higher-level controller automate the process

  * At the time of writing, K8s doesn't provide such controllers, but you can find them in projects like [Flagger](github.com/fluxcd/flagger) and [Argo Rollouts](argoproj.github.io/argo-rollouts)

* In this section, I'll just give you an overview of how the most common deployment strategies are implemented

  * The following table provides overviews of common deployment strategies, while the subsequent sections explain how they're implemented in K8s

| **Strategy**     | **Description**                                                                                                                                                                                                                                                                                                                                       |
|------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Recreate`       | Stop all Pods running the previous version, then create all Pods w/ the new version                                                                                                                                                                                                                                                                   |
| `Rolling update` | Gradually replace the old Pods w/ the new ones, either one by one or multiple at the same time. This strategy is also known as _Ramped_ or _Incremental_.                                                                                                                                                                                             |
| `Canary`         | Create one or a very small number of new Pods, redirect a small amount of traffic to those Pods to make sure they behave as expected. Then replace all the remaining Pods.                                                                                                                                                                            |
| `A/B testing`    | Create a small number of new Pods and redirect a subset of users to those Pods based on some condition. A single user is always redirected to the same version of the application. Typically, you use this strategy to collect data on how effective each version is at achieving certain goals.                                                      |
| `Blue/Green`     | Deploy the new version of the Pods in parallel w/ the old version. Wait until the new Pods are ready, and then switch all traffic to the new Pods. Then delete the old Pods.                                                                                                                                                                          |
| `Shadowing`      | Deploy the new version of the Pods alongside the old version. Forward each request to both versions, but return only the old version's response to the user, while discarding the new version's response. This way, you can see how the new version behaves w/o affecting users. This strategy is also known as _Traffic mirroring_ or _Dark launch_. |

* As you know, the `Recreate` and `RollingUpdate` strategies are directly supported by K8s, but you could also consider the Canary strategy as partially supported

## 14.3.1 [The Canary deployment strategy](canary-deployment-strategy/README.md)

## 14.3.2 [The A/B strategy](ab-strategy/README.md)

## 14.3.3 [The Blue/Green strategy](blue-green-strategy/README.md)

## 14.3.4 [Traffic shadowing](traffic-shadowing/README.md)
