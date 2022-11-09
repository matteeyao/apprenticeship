# Using Docker Inspect

Docker inspect is a command that allows you to get detailed information about Docker objects, such as containers, images, services, etc.

We can use the general form `docker inspect <OBJECT>` or an object-type-specific form `docker container inspect <CONTAINER>`.

```
docker inspect <OBJECT_ID>
```

If you know what kind of object you are inspecting, you can also use an alternate form of the command:

```
docker container inspect <CONTAINER>

docker service inspect <SERVICE>
```

This form allows you to specify an object name instead of an ID.

For some object types, you can also supply the `--pretty` flag to get a more readable output.

## Docker Inspect --format

Use the `--format` flag with Docker inspect to retrieve a specific subsection of the data fields using a Go template:

```
docker service inspect --format='{{.ID}}' <SERVICE>
```
