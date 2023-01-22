# Deleting objects defined in manifest files

* Whenever you create objects from a file, you can also delete them by passing the file to the `delete` command instead of specifying the name of the pod

## Deleting objects by specifying the manifest file

* You can delete the `kiada-ssl` pod, which you created from the `pod.kiada-ssl.yaml` file, w/ the following command:

```zsh
$ kubectl delete -f pod.kiada-ssl.yaml
pod "kiada-ssl" deleted
```

* In your case, the file contains only a single pod object, but you'll typically come across files that contain several objects of different types that represent a complete application

  * This makes deploying and removing the application as easy as executing `kubectl apply -f app.yaml` and `kubectl delete -f app.yaml`, respectively

## Deleting objects from multiple manifest files

* Sometimes, an application is defined in several manifest files

  * You can specify multiple files by separating them w/ a comma

  * For example:

```zsh
$ kubectl delete -f pod.kiada.yaml,pod.kiada-ssl.yaml
```

> [!NOTE]
> 
> You can also apply several files at the same time using this syntax (for example: `kubectl apply -f pod.kiada.yaml,pod.kiada-ssl.yaml`).

* You can also deploy all the manifest files from a file directory by specifying the directory name instead of the names of individual files

  * For example, you can deploy all the pods you created in this chapter again by running the following command in the base directory of this book's code archive:

```zsh
$ kubectl apply -f Chapter05/
```

* This applies to all files in the directory that have the correct file extension (`.yaml`, `.json`, and similar). You can then delete the pods using the same method:

```zsh
$ kubectl delete -f Chapter05/
```

> [!NOTE]
> 
> If your manifest files are stored in subdirectories, you must use the `--recursive` flag (or `-R`).
