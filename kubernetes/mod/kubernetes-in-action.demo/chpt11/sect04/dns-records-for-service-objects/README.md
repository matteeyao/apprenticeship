# Understanding DNS records for Service objects

* An important aspect of K8s services is the ability to look them up via DNS

  * This is something that deserves to be looked at more closely

* You know that a service is assigned to an internal cluster IP address that pods can resolve through the cluster DNS

  * This is b/c each service gets an `A` record in DNS (or an `AAAA` record for IPv6)

  * However, a service also receives an `SRV` record for each of the ports it makes available

* Let's take a closer look at these DNS records

  * First, run a one-off pod like this:

```zsh
$ kubectl run -it --rm dns-test --image=giantswarm/tiny-tools
/#
```

* This command runs a pod named `dns-test` w/ a container based on the container image `giantswarm/tiny-tools`

  * This image contains the `host`, `nslookup`, and `dig` tools that you can use to examine DNS records

  * When you run the `kubectl run` command, your terminal will be attached to the shell process running in the container (the `-it` option does this)

  * When you exit the shell, the pod will be removed (by the `--rm` option)

## 11.4.1 Inspecting a service's A and SRV records in DNS

* See [a-and-srv-records](a-and-srv-records/README.md)

## 11.4.2 Using headless services to connect to pods directly

* See [headless-services](headless-services/README.md)

## 11.4.3 Creating a CNAME alias for an existing service

* See [cname-alias](cname-alias/README.md)
