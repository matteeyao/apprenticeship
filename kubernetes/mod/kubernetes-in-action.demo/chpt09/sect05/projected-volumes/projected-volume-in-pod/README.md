# Using a projected volume in a pod

* In the final exercise of this chpt, you'll modify the `kiada-ssl` pod to use a single `projected` volume in the `envoy` container

  * The previous version of the pod used a `configMap` volume mounted in `/etc/envoy` to inject the `envoy.yaml` config file and a `secret` volume mounted in `/etc/certs` to inject the TLS certificate and key files

  * You'll now replace these two volumes w/ a single `projected` volume

  * This will allow you to keep all three files in the same directory (`/etc/envoy`)

* First, you need to change the TLS certificate path in the `envoy.yaml` configuration file inside the `kiada-envoy-config` config map so that the certificate and key are read from the same directory

  * After editing, the lines in the config map should look like this:

```yaml
    tls_certificates:
    - certificate_chain:
        filename: "/etc/envoy/example-com.crt"    # ← A
      private_key:
        filename: "/etc/envoy/example-com.key"    # ← B

# ← A ▶ ︎This used to be "/etc/certs/example-com.crt"
# ← B ▶ ︎This used to be "/etc/certs/example-com.key"
```

* You can find the pod manifest w/ the projected volume in the file [`pod.kiada-ssl.projected-volume.yaml`](pod.kiada-ssl.projected-volume.yaml)

  * The relevant parts are shown in the next listing ▶︎ Using a projected volume instead of a configMap and secret volume

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: kiada-ssl
spec:
  volumes:
  - name: etc-envoy                   # ← A
    projected:                        # ← A
      sources:                        # ← A
      - configMap:                    # ← B
          name: kiada-envoy-config    # ← B
      - secret:                       # ← C
          name: kiada-tls             # ← C
          items:                      # ← C
          - key: tls.crt              # ← C
            path: example-com.crt     # ← C
          - key: tls.key              # ← C
            path: example-com.key     # ← C
            mode: 0600                # ← D
  containers:
  - name: kiada
    image: luksa/kiada:1.2
    env:
    ...
  - name: envoy
    image: envoyproxy/envoy:v1.14.1
    volumeMounts:                     # ← E
    - name: etc-envoy                 # ← E
      mountPath: /etc/envoy           # ← E
      readOnly: true                  # ← E
    ports:
    ...

# ← A ▶︎ A single projected volume is defined.
# ← B ▶︎ The first volume source is the config map.
# ← C ▶︎ The second source is the secret.
# ← D ▶︎ Set restricted file permissions for the private key file.
# ← E ▶︎ The volume is mounted into the envoy container at /etc/envoy.
```

* The listing shows that a single `projected` volume named `etc-envoy` is defined in the pod

  * Two sources are used for this volume:

    * The first is the `kiada-envoy-config` config map

      * All entries in this config map become files in the projected volume

    * The second source is the `kiada-tls` secret

      * Two of its entries become files in the projected volume-the value fo the `tls.crt` key becomes file `example-com.crt`, whereas the value of the `tls.key` key becomes file `example-com.key` in the volume

      * The volume is mounted in read-only mode in the `envoy` container at `/etc/envoy`

* As you can see, the source definitions in the `projected` volume are not much different from the `configMap` and `secret` volumes you created in the previous sections

  * Therefore, further explanation of the projected volumes is unnecessary

  * Everything you learned about the other volumes also applies to this new volume type
  