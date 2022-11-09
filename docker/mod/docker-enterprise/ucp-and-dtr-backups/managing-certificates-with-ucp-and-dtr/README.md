# Managing Certificates with UCP and DTR

## External Certificates

Your UCP infrastructure generates its own self-signed certificates by default, but you can also supply your own certificates.

You can apply your own certificates to both **UCP** and **DTR** using their web interfaces.

## Client Bundles

**Client bundles**: A package containing client certificates and setup scripts. We can download client bundles from UCP and use them to authenticate with UCP as a Docker client.

They include client certificates which allow you to authenticate w/ UCP from the Docker command line.

We can provide our own certificates for UCP and DTR via their respective web UIs.
