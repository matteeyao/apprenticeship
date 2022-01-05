# Steps

Steps are actions that need to be taken to complete your job. Steps are usually a collection of executable commands. For example, the `checkout` step, which is a *built-in* step available across all CircleCI projects, checks out the source code for a job over SSH. Then, the `run` step allows you to run custom commands, such as executing the command `make test` using a non-login shell by default. Commands can also be defined outside the job declaration, making them reusable across your config

```yml
#...
jobs: # job name
  build:  # Specifies the primary container image,
    docker:
      - image: <image-name-tag>
        auth:
          username: mydockerhub-user
          password: $DOCKERHUB_PASSWORD  # context / project UI env-var reference
    steps:
      - checkout # Special step to checkout your source code
      - run: # Run step to execute commands, see
      # circleci.com/docs/2.0/configuration-reference/#run
          name: Running tests
          command: make test # executable command run in
          # non-login shell with /bin/bash -eo pipefail option
          # by default.
#...
```
