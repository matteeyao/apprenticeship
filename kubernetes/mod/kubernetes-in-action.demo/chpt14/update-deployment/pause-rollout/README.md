# 14.2.4 Pausing the rollout process

* The rolling update process is fully automated

  * Once you update the Pod template in the Deployment object, the rollout process begins and doesn't end until all Pods are replaced w/ the new version

  * However, you can pause the rolling update at any time

  * You may want to do this to check the behaviour of the system while both versions of the application are running, or to see if the first new Pod behaves as expected before replacing the other Pods

## Pausing the Rollout

* To pause an update in the middle of the rolling update process, use the following command:

```zsh
$ kubectl rollout pause deployment kiada
deployment.apps/kiada paused
```

* This command sets the value of the `paused` field in the Deployment's `spec` section to `true`

  * The Deployment controller checks this field before any change to the underlying ReplicaSets

* Try the update from version 0.6 to version 0.7 again and pause the Deployment when the first Pod is replaced

  * Open the application in your web browser and observe its behavior

> ## Be careful when using rolling updates w/ a web application
> 
> * If you pause the update while the Deployment is running both the old and new versions of the application and access it through your web browser, you'll notice an issue that can occur when using this strategy w/ web applications
> 
> * Refresh the page in your browser several times and watch the colors and version numbers displayed in the four boxes in the lower right corner
> 
>   * You'll notice that you get version 0.6 for some resources and version 0.7 for others
> 
>   * This is b/c some requests sent by your browser are routed to Pods running version 0.6 and some are routed to those running version 0.7
> 
>   * For the Kiada application, this doesn't matter, b/c there aren't any major changes in the CSS, JavaScript, and image files between the two versions
> 
>   * However, if this were the case, the HTML could be rendered incorrectly
> 
> * To prevent his, you could use session affinity or update the application in two steps
> 
>   * First, you'd add the new features to the CSS and other resources, but maintain backwards compatibility
> 
>   * After you've fully rolled out this version, you can then roll out the version w/ the changes to the HTML
> 
>   * Alternatively, you can use the blue-green deployment strategy, explained later in this chapter

## Resuming the rollout

* To resume a paused rollout, execute the following command:

```zsh
$ kubectl rollout resume deployment kiada
deployment.apps/kiada resumed
```

## Using the pause feature to block rollouts

* Pausing a Deployment can also be used to prevent updates to the Deployment from immediately triggering the update process

  * This allows you to make multiple changes to the Deployment and not start the rollout until you've made all the necessary changes

  * Once you're ready for the changes to take effect, you resume the Deployment and the rollout process begins
