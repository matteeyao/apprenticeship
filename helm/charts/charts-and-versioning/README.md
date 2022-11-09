# Charts and Versioning

Every chart must have a version number. A version must follow the `SemVer 2` standard. Unlike Helm Classic, Helm v2 and later uses version numbers as release markers. Packages in repositories are identified by name plus version.

For example, an `nginx` chart whose version field is set to `version: 1.2.3` will be named:

```
nginx-1.2.3.tgz
```

More complex SemVer 2 names are also supported, such as `version: 1.2.3-alpha.1+ef365`. But non-SemVer names are explicitly disallowed by the system.

The `version` field within the `Chart.yaml` is used by many of the Helm tools, including the CLI. When generating a package, the `helm package` command will use the version that it finds in the `Chart.yaml` as a token in the package name. The system assumes that the version number in the chart package name matches the version number in the `Chart.yaml`. Failure to meet this assumption will cause an error.
