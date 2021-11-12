# Orbs

Orbs are reusable snippets of code that help automate repeated processes, speed up project setup, and make it easy to integrate w/ third-party tools

The following illustration demonstrates a simplified configuration w/ the Maven orb. Here, the orb will setup a default executor that can execute steps w/ maven and run a common job (`maven/test`)

`.circleci/config.yml`

```yml
version 2.1

# Invoke an orb → This orb's built-in jobs, commands and
# elements can be used throughout the config.
orb:
  maven: circleci/maven@0.0.12
  
# In-built job → The Maven orb provides a "test" job
# to use, cleaning up our workflow.
workflows:
  maven_test:
    jobs:
      - maven/test
```
