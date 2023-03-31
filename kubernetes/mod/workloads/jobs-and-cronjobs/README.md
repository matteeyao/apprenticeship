# Jobs and CronJobs

## Job

* Creates a pod that can run at a specific time, in parallel with another job

* Create a job named j1 that prints out the image's environment variables:

```zsh
kubectl run j1 --image=busybox --restart=OnFailure --command \
--dry-run -o yaml -- printenv > j1.yaml
```

* W/ `kubectl run`, setting `--restart=OnFailure` will turn this into a job

* The `--comand` extension will just add `printenv` under `command` and not `args`

* In K8s, `command` is equivalent to shell commands and `args` is equivalent to arguments passed to the command

```yaml
apiVersion: batch/v1
kind: Job
metadata:
  labels:
    run: j1
  name: j1
spec:
  template:
    metadata:
      labels:
        run: j1
    spec:
      containers:
      - command:
        - printenv
        image: busybox
        name: j1
      restartPolicy: OnFailure
```

* After running this job, you should see a pod, so let's query for that pod using its label:

```zsh
kubectl get po -l run
```

* Now check the logs for that pod to confirm the environment variables were printed to STDOUT:

```zsh
kubectl logs <J1_YOUR_POD'S_UNIQUE_HASH>
```

* We'll add a few features to the spec of the job now

  * If you are curious to see all the options available:

```zsh
kubectl explain job.spec
```

* We'll add 3 additional spec:

  1. Completions ▶︎ declares how many times to run the job

  2. Parallelism ▶ declares how many pods can run at the same time

  3. activeDeadlineSeconds ▶ declares how long the job can run

```yaml
apiVersion: batch/v1
kind: Job
metadata:
  labels:
    run: j1
  name: j1
spec:
  completions: 10
  parallelism: 2
  activeDeadlineSeconds: 15
  template:
    metadata:
      labels:
        run: j1
    spec:
      containers:
      - command:
        - printenv
        image: busybox
        name: j1
      restartPolicy: OnFailure
```

* B/c this job is named the same as the previous created job, let's delete all jobs:

```zsh
kubectl delete job --all
```

* Now run the job and check the pods:

```zsh
kubectl get pods
```

* You should see two pods running at a time, which was determined by the **parallelism** spec

  * However, we set a **deadline** of only 15 seconds to create 10 pods w/ only 2 running at a time

  * That just isn't going to happen, so let's check out the description of the job:

```zsh
kubectl get job
---
NAME   COMPLETIONS   DURATION   AGE
j1     4/10          116s       116s
```

* We only had 4 complete out of the 10

  * Let's also check out the description:

```zsh
kubectl describe job j1
---
Warning  DeadlineExceeded  2m8s   job-controller  Job was active longer than specified deadline
```

* That makes sense-the job was stopped b/c we set a deadline

## CronJob

* Similar to Job, but CronJob runs based on the "* * * * *" format you choose

```zsh
kubectl run cj1 --image=busybox --command --dry-run --schedule="*/1 * * * *" \
-o yaml -- printenv > cj1.yaml
```

```yaml
apiVersion: batch/v1beta1
kind: CronJob
metadata:
  labels:
    run: cj1
  name: cj1
spec:
  concurrencyPolicy: Allow
  jobTemplate:
    spec:
      template:
        metadata:
          labels:
            run: cj1
        spec:
          containers:
          - command:
            - printenv
            image: busybox
            name: cj1
          restartPolicy: Always
  schedule: '*/1 * * * *'
```

* If you're creating this template from scratch, watch out for `spec` as cron jobs have 3 different locations for `spec`: `CronJob` → `Job` → `Pod` 
