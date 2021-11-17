# HTTP headers

**HTTP headers** let the client and the server pass additional information w/ an HTTP request or response. An HTTP header consists of its case-insensitive name followed by a colon (`:`), then by its value.

**End-to-end headers**

* These headers *must* be transmitted to the final recipient of the message: the server for a request, or the client for a response. Intermediate proxies must retransmit these headers unmodified and caches must store them

**Hop-by-hop headers**

* These headers are meaningful only for a single transport-level connection, and *must not* be retransmitted by proxies or cached. Note that only hop-by-hop headers may be set using the `Connection` header
