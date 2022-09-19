# Network Namespace

The network namespace allows for containers to have their own copy of the network stack.

```
cat /etc/issue
```

```
sudo ip netns add sample1
```

```
sudo ip netns list
```

```
iptables -L
sudo iptables -L
```

```
sudo ip netns exec sample1 iptables -L
```

```
sudo ip netns exec sample1 bash
iptables -L
```

```
iptables -A INPUT -p tcp --dport 80 -j ACCEPT
```

```
iptables -L
```

```
exit
```

```
iptables -L
sudo iptables -L
```
