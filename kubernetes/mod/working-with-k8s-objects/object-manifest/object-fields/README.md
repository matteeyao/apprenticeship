# 4.2.2 Understanding individual object fields

```zsh
kubectl explain
```

## Using Kubectl explain to explore API object fields

* The kubectl tool has a nice feature that allows you to look up the explanation of each field for each object type (kind) from the command line

  * Usually, you start by asking it to provide the basic description of the object kind by running `kubectl explain <kind>`, as shown here:

```zsh
$ kubectl explain nodes
```

The command prints the explanation of the object and lists the top-level fields that the object can contain.

## Drilling deeper into an API object's structure

* You can then drill deeper to find subfields under each specific field. For example, you can use the following command to explain the node’s `spec` field:

```zsh
$ kubectl explain node.spec
```

* Please note the API version given at the top. As explained earlier, multiple versions of the same kind can exist

  * Different versions can have different fields or default values

  * If you want to display a different version, specify it with the `--api-version` option

> [!NOTE]
> 
> If you want to see the complete structure of an object (the complete hierarchical list of fields without the descriptions), try `kubectl explain pods --recursive`.
