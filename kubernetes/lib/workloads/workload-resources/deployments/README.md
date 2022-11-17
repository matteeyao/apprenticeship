# Deployments

1. What does a Deployment's template do?

> Provides a specification which will be used to create new Pods.

2. Which term refers to changing the number of replicas in a Deployment.

> Scaling ▶︎ Scaling means changing a Deployment's replica count.

3. What Kubernetes object allows you to specify a desired state for a set of replica Pods?

> Deployment ▶︎ Deployments provide desires state configuration for ReplicaSets.

4. You have performed a rolling update of one of your apps, but there are issues with the new code. How can you return to the previous, working state?

> Perform a rollback on the Deployment ▶︎ Rollbacks allow you to easily revert a Deployment to a previous state.

5. Which command(s) can be used to scale a deployment? (select all that apply)

> `kubectl edit deployment` and `kubectl scale`
