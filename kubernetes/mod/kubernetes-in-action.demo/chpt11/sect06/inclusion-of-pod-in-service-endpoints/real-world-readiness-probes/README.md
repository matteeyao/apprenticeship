# Implementing real-world readiness probes

* If you don't define a readiness probe in your pod, it becomes a service endpoint as soon as it's created

  * This means that every time you create a new pod instance, connections forwarded by the service to that new instance will fail until the application in the pod is ready to accept them

  * To prevent this, you should always define a readiness probe for the pod

* In the previous section, you learned how to add a mock readiness probe to a container to manually control whether the pod is a service endpoint or not

  * In the real world, the readiness probe result should reflect the ability of the application running in the container to accept connections

## Defining a minimal readiness probe

* For containers running an HTTP server, it's much better do define a simple readiness probe that checks whether the server responds to a simple `GET /` request, such as the one in the following snippet, than to have no readiness probe at all

```yaml
readinessProbe:
  httpGet:        # ← A
    port: 8080    # ← A
    path: /       # ← B
    scheme: HTTP  # ← B

# ← A ▶︎ The probe sends an HTTP GET request to pot 8080 of the container.
# ← B ▶︎ The probe requests the root URL path over HTTP (as opposed to HTTPS).
```

* When K8s invokes this readiness probe, it sends the `GET /` request to port `8080` of the container and checks the returned HTTP response code

  * If the response code is greater than or equal to `200` and less than `400`, the probe is successful, and the pod is considered ready

  * If the response code is anything else (for example, `404` or `500`) or the connection attempt fails, the readiness probe is considered failed and the pod is marked as not ready

* This simple probe ensures that the pod only becomes part of the service when it can actually handle HTTP requests, rather than immediately when the pod is started

## Defining a better readiness probe

* A simple readiness probe like the one shown in the previous section isn't always sufficient

  * Take the Quote pod, for example

  * You may recall that it runs two containers:

    * The `quote-writer` container selects a random quote from this book and writes it to a file called `quote` in the volume shared by the two containers

    * The `nginx` container serves files from this shared volume

  * Thus, the quote itself is available at the URL path `/quote`

* The purpose of the Quote pod is clearly to provide a random quote from the book

  * Therefore, it shouldn't be marked ready until it can serve this quote

  * If you direct the readiness probe to the URL path `/`, it'll succeed even if the `quote-writer` container hasn't yet created the `quote` file

  * Therefore, the readiness probe in the Quote pod should be configured as shown in the following snippet from the [`pod.quote-readiness.yaml`](pod.quote-readiness.yaml) file:

```yaml
readinessProbe:
  httpGet:
    port: 80
    path: /quote        # ← A
    scheme: HTTP
  failureThreshold: 1   # ← B

# ← A ▶︎ The Quote pod is ready when it can serve the quote.
# ← B ▶︎ Set the failure threshold to one, so that the pod is immediately marked as not ready if the probe fails.
```

* If you add this readiness probe to your Quote pod, you'll see that the pod is only ready when the `quote` file exists

  * Try deleting the file from the pod as follows:

```zsh
$ kubectl exec quote-readiness -c quote-writer -- rm /var/local/output/quote
```

* Now check the pod's readiness status w/ `kubectl get pod` and you'll see that one of the containers is no longer ready

  * When the `quote-writer` recreates the file, the container becomes ready again

  * You can also inspect the endpoints of the `quote` service w/ `kubectl get endpoints quote` to see that the pod is removed and then re-added

## Implementing a dedicated readiness endpoint

* As you saw in the previous example, it may be sufficient to point the readiness probe to an existing path served by the HTTP server, but it's also common for an application to provide a dedicated endpoint, such as `healthz/ready` or `/readyz`, through which it reports its readiness status

  * When the application receives a request on this endpoint, it can perform a series of internal checks to determine its readiness status

* Let's take the Quiz service as an example

  * The Quiz pod runs both an HTTP server and a MongoDB container

  * As you can see in the following listing, the `quiz-api` server implements the `/healthz/ready` endpoint

  * When it receives a request, it checks if it can successfully connect to MongoDB in the other container

    * If so, it responds w/ a `200 OK`

    * If not, it returns `500 Internal Server Error`

  * The readiness endpoint in the quiz-api application:

```go
func (s *HTTPServer) ListenAndServe(listenAddress string) {
  router := mux.NewRouter()
	router.Methods("GET").Path("/").HandlerFunc(s.handleRoot)
	router.Methods("GET").Path("/healthz/ready").HandlerFunc(s.handleReadiness)       // ← A
	...
}
func (s *HTTPServer) handleReadiness(res http.ResponseWriter, req *http.Request) {
	conn, err := s.db.Connect()                                                       // ← B
  if err!=nil {                                                                     // ← C
    res.WriteHeader(http.StatusInternalServerError)                                 // ← C
    _, _ = fmt.Fprintf(res, “ERROR: %v\n”, err.Error())                             // ← C
	  return                                                                          // ← C
  }
  defer conn.Close()
	res.WriteHeader(http.StatusOK)                                                    // ← D
	_, _ = res.Write([]byte("Readiness check successful"))                            // ← D
}

// ← A ▶︎ The /healthz/ready endpoint invokes the handleReadiness() function.
// ← B ▶︎ Try to connect to MongoDB.
// ← C ▶︎ If the connection fails, the 500 Internal Server Error response code is returned.
// ← D ▶︎ If the connection succeeds, the 200 OK response code is returned
```

* The readiness probe defined in the Quiz pod ensures that everything the pod needs to provide its services is present and working

  * As additional components are added to the quiz-api application, further checks can be added to the readiness check code

  * An example of this is the addition of an internal cache

  * The readiness endpoint could check to see if the cache is warmed up, so that only then is the pod exposed to clients

## Checking dependencies in the readiness probe

* In the Quiz pod, the MongoDB database is an internal dependency of the `quiz-api` container

  * The Kiada pod, on the other hand, depends on the Quiz and Quote services, which are external dependencies

  * What should the readiness probe check in the Kiada pod?

  * Should it check whether it can reach the Quote and Quiz services?

* The answer to this question is debatable, but any time you check dependencies in a readiness probe, you must consider what happens if a transient problem, such as a temporary increase in network latency, causes the probe to fail

* Note that the `timeoutSeconds` field in the readiness probe definition limits the time the probe has to respond
  
  * The default timeout is only one second

  * The container must respond to the readiness probe in this time

* If the Kiada pod calls the other two services in its readiness check, but their responses are only slightly delayed due to a transient network disruption, its readiness probe fails and the pod is removed from the service endpoints

  * If this happens to all Kiada pods at the same time, there will be no pods left to handle client requests

  * The disruption may only last a second, but the pods may not be added back to the service until dozens of seconds later, depending on how the `periodSeconds` and `successThreshold` properties are configured

* When you check external dependencies in your readiness probes, you should consider what happens when these types of transient network problems occur

  * Then you should set your periods, timeouts, and thresholds accordingly

> [!TIP]
> 
> Readiness probes that try to be too smart can cause more problems than they solve. As a rule of thumb, readiness probes shouldn't test external dependencies, but can test dependencies within the same pod.

* The Kiada application also implements the `/healthz/ready` endpoint instead of having the readiness probe use the `/` endpoint to check its status

  * This endpoint simply responds w/ the HTTP response code `200 OK` and the word `Ready` in the response body

  * This ensures that the readiness probe only checks that the application itself is responding, w/o also connecting to the Quiz or Quote services

  * You can find the pod manifest in the [`pod.kiada-readiness.yaml`](pod.kiada-readiness.yaml)

## Understanding readiness probes in the context of pod shutdown

* As you know, readiness probes are most important when the pod starts, but they also ensure that the pod is taken out of service when something causes it to no longer be ready during normal operation

  * But what about when the pod is terminating?

  * A pod that's in the process of shutting down shouldn't be part of any services

  * Do you need to consider that when implementing the readiness probe?

* Fortunately, when you delete a pod, K8s not only sends the termination signal to the pod's containers, but also removes the pod from all services

  * This means you don't have to make any special provisions for terminating pods in your readiness probes

  * You don't have to make sure that the probe fails when your application receives the termination signal
