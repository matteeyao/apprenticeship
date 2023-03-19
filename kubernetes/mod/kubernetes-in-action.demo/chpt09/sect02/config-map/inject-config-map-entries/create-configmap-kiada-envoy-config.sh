#!/usr/bin/env bash

kubectl create configmap kiada-envoy-config \
    --from-file envoy.yaml=./envoy.yaml
    --from-file=dummy.bin \
            --dry-run=client -o yaml > cm.kiada-envoy-config.yaml
