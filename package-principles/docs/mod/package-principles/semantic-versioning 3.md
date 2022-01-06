# [Semantic Versioning 2.0.0](https://semver.org/)

## Summary

Given a version number `MAJOR.MINOR.PATCH`, increment the:

1. `MAJOR` version when you make incompatible API changes,

2. `MINOR` version when you add functionality in a backwards compatible manner, and

3. `PATCH` version when you make backwards compatible bug fixes

Additional labels for pre-release and build metadata are available as extensions to the `MAJOR.MINOR.PATCH` format

## Introduction

In systems w/ many dependencies, releasing new package versions can quickly become a nightmare

If the dependency specifications are too tight, you are in danger of version lock (the inability to upgrade a package w/o having to release new versions of every dependent package)

If dependencies are specified too loosely, you will inevitably be bitten by version promiscuity

Version lock and/or version promiscuity can prevent you from easily and safely moving your project forward

Once you identify your public API, you communicate changes to it w/ specific increments to your version number

Consider a version format `X.Y.Z`

Bug fixes not affecting the API increment the patch version, backwards compatible API additions/changes increment the minor version, and backwards incompatible API changes increment the major version

## Semantic Versioning Specification (SemVer)

1. Software using Semantic Versioning MUST declare a public API. This API could be declared in the code itself or exist strictly in documentation. However it is dont, it SHOULD be precise and comprehensive

4. Major version zero (0.y.z) is for initial development. Anything may change at any time. The public API SHOULD NOT be considered stable

5. Version 1.0.0 defines the public API. The way in which the version number is incremented after this release is dependent on this public API and how it changes

6. Patch version Z (x.y.Z | x > 0) MUST be incremented if only backwards compatible bug fixes are introduced. A bug fix is defined as an internal change that fixes incorrect behavior

7. Minor version Y (x.Y.z | x > 0) MUST be incremented if new, backwards compatible functionality is introduced to the public API. It MUST be incremented if any public API functionality is marked as deprecated. It MAY be incremented if substantial new functionality or improvements are introduced within the private code. It MAY include patch level changes. Patch version MUST be reset to 0 when minor version is incremented

8. Major version X (X.y.z | X > 0) MUST be incremented if any backwards incompatible changes are introduced to the public API. It MAY also include minor and patch level changes. Patch and minor versions MUST be reset to 0 when major version is incremented
