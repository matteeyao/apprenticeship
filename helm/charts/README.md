# Charts

Helm uses a packaging format called `charts`. A chart is a collection of files that describe a related set of Kubernetes resources. A single chart might be used to deploy something simple, like a memcached pod, or something complex, like a full web app stack with HTTP servers, databases, caches, and so on.

Charts are created as files laid out in a particular directory tree. They can be packaged into versioned archives to be deployed.

If you want to download and look at the files for a published chart, without installing it, you can do so with `helm pull chartrepo/chartname`.
