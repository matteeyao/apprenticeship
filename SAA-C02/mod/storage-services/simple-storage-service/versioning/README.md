# S3 Versioning

Using **Versioning** w/ S3:

* Versioning stores all versions of an object (including all writes and even if you delete an object)

* Great backup

* Once enabled, **Versioning cannot be disabled**, only suspended

* Integrates w/ **Lifecycle** rules

* Versioning's **MFA Delete** capability, which uses multi-factor authentication, can be used to provide an additional layer of security

To control the versions of files that are served from your distribution, you can either invalidate files or give them versioned file names. If you want to update your files frequently, AWS recommends that you primarily use file versioning for the following reasons:

* Versioning enables you to control which file a request returns even when the user has a version cached either locally or behind a corporate caching proxy. If you invalidate the file, the user might continue to see the old version until it expires from those caches.

* CloudFront access logs include the names of your files, so versioning makes it easier to analyze the results of file changes.

* Versioning provides a way to serve different versions of files to different users.

* Versioning simplifies rolling forward and back between file revisions.

* Versioning is less expensive. You still have to pay for CloudFront to transfer new versions of your files to edge locations, but you don't have to pay for invalidating files.
