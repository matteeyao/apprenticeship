# Pipelines

A CircleCI pipeline is the full set of processes you run when you trigger work on your projects. Pipelines encompass your workflows, which in turn coordinate your jobs → all defined in your project configuration file

Pipelines represent methods for interacting w/ your configuration:

* Use the new API endpoint to trigger a pipeline

* Use pipeline parameters to trigger conditional workflows

* Access to `version 2.1` config, which provides:

  * Reusable config elements, including executors, commands and jobs

  * Packaged reusable config, known as `orbs`

  * Improved config validation error messages

  * Option to enable auto-cancel, within **Advanced Settings**, to abort workflows when new build are triggered on non-default branches
  
