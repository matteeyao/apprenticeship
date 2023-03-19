# Inspecting a service's A and SRV records in DNS

## Looking up a service's A record

* To determine the IP address of the `quote` service, run the `nslookup` command in the shell running in the container of the `dns-test` pod like so:

```zsh
/ # nslookup quote
Server:         10.96.0.10
Address:        10.96.0.10#53

Name:     quote.kiada.svc.cluster.local   # ← A
Address:  10.96.161.97                    # ← B

# ← A ▶︎ The service's fully qualified domain name
# ← B ▶︎ The service's cluster IP
```

> [!NOTE]
> 
> You can use `dig` instead of `nslookup`, but you must either use the `+search` option or specify the fully qualified domain name of the service for the DNS lookup to succeed (run either `dig +search quote` or `dig quote.kiada.svc.cluster.local`).

* Now look up the IP address of the `kiada` service

  * Although this service is ot type `LoadBalancer` and thus has both an internal cluster IP and an external IP (that of the load balancer), the DNS returns only the cluster IP

  * This is to be expected since the DNS server is internal and is only used within the cluster

## Looking up SRV records

* A service provides one or more ports

  * Each port is given an `SRV` record in DNS

  * Use the following command to retrieve the `SRV` records for the `kiada` service:

```zsh
/ # nslookup -query=SRV kiada
Server:       10.96.0.10
Address:      10.96.0.10#53

kiada.kiada.svc.cluster.local   service = 0 50 80 kiada.kiada.svc.cluster.local.    # ← A
kiada.kiada.svc.cluster.local   service = 0 50 443 kiada.kiada.svc.cluster.local.   # ← B

# ← A ▶︎ SRV record for the http port 80
# ← B ▶︎ SRV record for the https port 443
```

> [!NOTE]
> 
> As of this writing, GKE still runs kube-dns instead of CoreDNS. Kube-dns doesn't support all the DNS queries shown in this section.

* A smart client running in a pod could look up the `SRV` records of a service to find out what ports are provided by the service

  * If you define the names for those ports in the Service object, they can even be looked up by name

  * The `SRV` record has the following form:

```zsh
_port-name._port-protocol.service-name.namespace.svc.cluster.local
```

* The names of the two ports in the `kiada` service are `http` and `https`, and both define TCP as the protocol

  * To get the `SRV` record for the `http` port, run the following command:

```zsh
/ # nslookup -query=SRV _http._tcp.kiada
Server:         10.96.0.10
Address:        10.96.0.10#53
_http._tcp.kiada.kiada.svc.cluster.local        service = 0 100 80
        kiada.kiada.svc.cluster.local.
```

> [!TIP]
> 
> To list all services and the ports they expose in the `kiada` namespace, you can run the command `nslookup -query=SRV any.kiada.svc.cluster.local`. To list all services in the cluster, use the name `any.any.svc.cluster.local`.

* You'll probably never need to look for `SRV` records, but some Internet protocols, such as SIP and XMPP, depend on them to work.
