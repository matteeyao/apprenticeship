# Chapter 9. Configuration via ConfigMaps, Secrets, and the Downward API

## Learning objectives

- [ ] Set the command and arguments for the container's main process

- [ ] Set environment variables

- [ ] Store configuration in config maps

- [ ] Store sensitive information in secrets

- [ ] Use the Downward API to expose pod metadata to the application

- [ ] Use configMap, secret, downwardAPI and projected volumes

* You've now learned how to use Kubernetes to run an application process and attach file volumes to it

  * In this chapter, you'll learn how to configure the application-either in the pod manifest itself, or by referencing other API objects within it

  * You'll also learn how to inject information about the pod itself into the application running inside it

## Sections

* 9.1 [Setting the command, arguments, and environment variables](sect01/set-command-args-and-env-vars/README.md)

* 9.2 [Using a config map to decouple configuration from the pod](sect02/config-map/README.md)

* 9.3 [Using secrets to pass sensitive data to containers](sect03/secrets/README.md)

* 9.4 [Passing pod metadata to the application via the Downward API](sect04/downward-api/README.md)

* 9.5 [Using projected volumes to combine volumes into one](sect05/projected-volumes/README.md)

## Learning summary

* This wraps up this chapter on how to pass configuration data to containers

* You've learned that:

  * The default command and arguments defined in the container image can be overridden in the pod manifest

  * Environment variables for each container can also be set in the pod manifest

    * Their values can be hardcoded in the manifest or can come from other Kubernetes API objects

  * Config maps are Kubernetes API objects used to store configuration data in the form of key/value pairs

    * Secrets are another similar type of object used to store sensitive data such as credentials, certificates, and authentication keys

  * Entries of both config maps and secrets can be exposed within a container as environment variables or as files via the `configMap` and `secret` volumes

  * Config maps and other API objects can be edited in place using the `kubectl edit` command

  * The Downward API provides a way to expose the pod metadata to the application running within it

    * Like config maps and secrets, this data can be injected into environment variables or files

  * Projected volumes can be used to combine multiple volumes of possibly different types into a composite volume that is mounted into a single directory, rather than being forced to mount each individual volume into its own directory

* You've now seen that an application deployed in K8s may require many additional objects

  * If you are deploying many applications in the same cluster, you need organize them so that everyone can see what fits where

  * In the next chapter, you'll learn how to do just that
