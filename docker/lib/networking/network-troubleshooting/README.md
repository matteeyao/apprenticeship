# Network Troubleshooting

1. What command should we use if we want to view logs for all of the tasks in a service called `my-service`?

[ ] docker task logs my-service

[x] docker service logs my-service

[ ] docker container logs my-service

[ ] docker logs my-service

> This command will retrieve logs for all of the tasks in the service.

2. Amanda is having some network issues and needs to do some troubleshooting. How can she inject a `nicolaka/netshoot` container into the sandbox of an existing container called `nginx-container`?

[ ] Amanda can use `docker run --network nginx-container nicolaka/netshoot`.

[ ] Amanda can use `docker run --network-debug nginx-container nicolaka/netshoot`.

[ ] Amanda can use `docker run --inject-container nginx-container nicolaka/netshoot`.

[x] Amanda can use `docker run --network container:nginx-container nicolaka/netshoot`.

> This command will inject the `netshoot` container into the sandbox of the existing container.
