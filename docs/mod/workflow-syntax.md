# Workflow syntax for GitHub Actions

A workflow is a configurable automated process made up of one ore more jobs.

You must create a YAML file to define your workflow configuration.

You must store workflow files in the `.github/workflows` directory of your repository.

`name`

The name of your workflow. GitHub displays the names of your workflows on your repository's actions page. If you omit `name`, GitHub sets it to the workflow file path relative to the root of the repository.

`on`

**Required**. The name of the GitHub event that triggers the workflow.

You can provide a single event `string`, `array` of events, `array` of event `types`, or an event configuration `map` that schedules a workflow or restricts the execution of a workflow to specific files, tags, or branch changes. For a list of available events, see "[Events that trigger workflows](https://docs.github.com/en/articles/events-that-trigger-workflows)."

## Example: Using a single event

```yaml
# Triggered when code is pushed to any branch in a repository
on: push
```

## Example: Using a list of events

```yaml
# Triggers the workflow on push or pull request events
on: [push, pull_request]
```

## Example: Using multiple events w/ activity types or configuration

If you need to specify activity types or configuration for an event, you must configure each event separately. You must append a colon (`:`) to all events, including events w/o configuration.

```yaml
on:
  # Trigger the workflow on push or pull request,
  # but only for the main branch
  push:
    branches:
      - main
  pull_request:
    branches:
      - main
  # Also trigger on page_build, as well as release created events
  page_build:
  release:
    types: # This configuration does not affect the page_build event above
      - created
```

## `on.<event_name>.types`

Selects the types of activity that will trigger a workflow run.

Most GitHub events are triggered by more than one type of activity.

For example, the event for the release resource is triggered when a release is `published`, `unpublished`, `created`, `edited`, `deleted`, or `prereleased`.

The `types` keyword enables you to narrow down activity that causes the workflow to run. When only one activity triggers a webhook event, the `types` keyword is unnecessary.

You can use an array of event `types`. For more information about each event and their activity types, see "[Events that trigger workflows](https://docs.github.com/en/articles/events-that-trigger-workflows#webhook-events)."

```yaml
# Trigger the workflow on release activity
on:
  release:
    # Only use the types keyword to narrow down the activity types that will trigger your workflow.
    types: [published, created, edited]
```

## `on.<push|pull_request.<branches|tags>`

When using the `push` and `pull_request` events, you can configure a workflow to run on specific branches or tags. For a `pull_request` event, only branches and tags on the base are evaluated.

If you define `only` tags or only `branches`, the workflow won't run for events affecting the undefined Git ref.

The `branches`, `branches-ignore`, `tags`, and `tags-ignore` keywords accept glob patterns that use characters like `*`, `**`, `+`, `?`, `!` and others to match more than one branch or tag name.

If a name contains any of these characters and you want a literal match, you need to *escape* each of these special characters w/ `\`.

For more information about glob patterns, see the "[Filter pattern cheat sheet](https://docs.github.com/en/actions/learn-github-actions/workflow-syntax-for-github-actions#filter-pattern-cheat-sheet)."

### Example: Including branches and tags

The patterns defined in `branches` and `tags` are evaluated against the Git ref's name.

For example, defining the pattern `mona/octocat` in `branches` will match the `refs/heads.mona/octocat` Git ref.

The pattern `releases/**` will match the `refs/heads/releases/10` Git ref.

```yaml
on:
  push:
    # Sequence of patterns matched against refs/heads
    branches:    
      # Push events on main branch
      - main
      # Push events to branches matching refs/heads/mona/octocat
      - 'mona/octocat'
      # Push events to branches matching refs/heads/releases/10
      - 'releases/**'
    # Sequence of patterns matched against refs/tags
    tags:        
      - v1             # Push events to v1 tag
      - v1.*           # Push events to v1.0, v1.1, and v1.9 tags
```

### Example: Ignoring branches and tags

Anytime a pattern matches the `branches-ignore` or `tags-ignore` pattern, the workflow will not run. The patterns defined in `branches-ignore` and `tags-ignore` are evaluated against the Git ref's name.

For example, defining the pattern `mona/octocat` in `branches` will match the `refs/head/mona/octocat` Git ref.

The pattern `releases/**-alpha` in `branches` will match the `refs/releases/beta/3-alpha` Git ref.

```yaml
on:
  push:
    # Sequence of patterns matched against refs/heads
    branches-ignore:
      # Do not push events to branches matching refs/heads/mona/octocat
      - 'mona/octocat'
      # Do not push events to branches matching refs/heads/releases/beta/3-alpha
      - 'releases/**-alpha'
    # Sequence of patterns matched against refs/tags
    tags-ignore:
      - v1.*           # Do not push events to tags v1.0, v1.1, and v1.9
```

### Excluding branches and tags

You can use two types of filters to prevent a workflow from running on pushes and pull requests to tag branches.

* `branches` or `branches-ignore` - You cannot use both the `branches` and `branches-ignore` filters for the same event in a workflow. Use the `branches` filter when you need to filter branches for positive matches and exclude branches. Use the `branches-ignore` filter when you only need to exclude branch names.

* `tags` or `tags-ignore` - You cannot use both the `tags` and `tags-ignore` filters for the same event in a workflow. Use the `tags` filter when you need to filter tags for positive matches and exclude tags. Use the `tags` filter when you need to filter tags for positive matches and exclude tags. Use the `tags-ignore` filter when you only need to exclude tag names.

### Example: Using positive and negative patterns

You can exclude `tags` and `branches` using the `!` character. The order that you define patterns matters.

* A matching negative pattern (prefixed w/ `!`) after a positive match will exclude the Git ref.

* A matching positive pattern after a negative match will include the Git ref again.

The following workflow will run on pushes to `releases/10` or `releases/beta/mona`, but not on `releases/10-alpha` or `releases/beta/3-alpha` b/c the negative pattern `!releases/**-alpha` follows the positive pattern.

```yaml
on:
  push:
    branches:    
      - 'releases/**'
      - '!releases/**-alpha'
```

## `on.<push|pull_request>.paths`

When using the `push` and `pull_request` events, you can configure a workflow to run when at least one file does not match `paths-ignore` or at least one modified file matches the configured `paths`.

Path filters are not evaluated for pushes to tags.

The `paths-ignore` and `paths` keywords accept glob patterns that use the `*` and `**` wildcard characters to match more than one path name.

For more information, see the "[Filter pattern cheat sheet](https://docs.github.com/en/actions/learn-github-actions/workflow-syntax-for-github-actions#filter-pattern-cheat-sheet)."

### Example: Ignoring paths

When all the path names match patterns in `paths-ignore`, the workflow will not run. GitHub evaluates patterns defined in `paths-ignore` against the path name.

A workflow w/ the following path filter will only run on `push` events that include at least one file outside the `docs` directory at the root of the repository.

```yaml
on:
  push:
    paths-ignore:
      - 'docs/**'
```

### Example: Including paths

If at least one path matches a pattern in the `paths` filter, the workflow runs.

To trigger a build anytime you push a JavaScript file, you can use a wildcard pattern.

```yaml
on:
  push:
    paths:
      - '**.js'
```

### Excluding paths

You can exclude paths using two types of filters. You cannot use both of these filters for the same event in a workflow.

* `paths-ignore` - Use the `paths-ignore` filter when you only need to exclude path names.

* `paths` - Use the `paths` filter when you need to filter paths for positive matches and exclude paths.

### Example: Using positive and negative patterns

You can exclude `paths` using the `!` character. The order that you define patterns matters:

* A matching negative pattern (prefixed w/ `!`) after a positive match will exclude the path.

* A matching positive pattern after a negative match will include the path again.

This example runs anytime the `push` even indicates a file in the `sub-project` directory or its subdirectories, unless the file is in the `sub-project/docs` directory.

For example, a push that changed `sub-project/index.js` or `sub-project/src/index.js` will trigger a workflow run, but a push changing only `sub-project/docs/readme.md` will not.

```yaml
on:
  push:
    paths:
      - 'sub-project/**'
      - '!sub-project/docs/**'
```

### Git diff comparisons

The filter determines if a workflow should run by evaluating the changed files and running them against the `paths-ignore` or `paths` list.

If there are no files changed, the workflow will not run.

GitHub generates the list of changed files using two-dot diffs for pushes and three-dot diffs for pull requests:

* **Pull requests**: Three-dot diffs are a comparison btwn the most recent version of the topic branch and the commit where the topic branch was last synced w/ the base branch.

* **Pushes to existing branches**: A two-dot diff compares the head and base SHAs directly w/ each other.

* **Pushes to new branches**: A two-dot diff against the parent of the ancestor of the deepest commit pushed.

Diffs are limited to 300 files. If there are files changed that aren't matched in the first 300 files returned by the filter, the workflow will not run.

You may need to create more specific filters so that the workflow will run automatically.

## `on.workflow_call.inputs`

When using the `workflow_call` keyword, you can optionally specify inputs that are passed to the called workflow from the caller workflow.

Inputs for reusable workflows are specified w/ the same format as action inputs.

For more information about the `workflow_call` keyword, see "[Events that trigger workflows](https://docs.github.com/en/actions/learn-github-actions/events-that-trigger-workflows#workflow-reuse-events)."

In addition to the standard input parameters that are available, `on.workflow_call.inputs` requires a `type` parameter.

For more information, see [`on.workflow_call.<input_id>.type`](https://docs.github.com/en/actions/learn-github-actions/workflow-syntax-for-github-actions#onworkflow_callinput_idtype).

If a `default` parameter is not set, the default value of the input is `false` for a boolean, `0` for a number, and `""` for a string.

Within the called workflow, you can use the `inputs` context to refer to an input.

If a caller workflow passes an input that is not specified in the called workflow, this results in an error.

**Example**

```yaml
on:
  workflow_call:
    inputs:
      username:
        description: 'A username passed from the caller workflow'
        default: 'john-doe'
        required: false
        type: string
  
jobs:
  print-username:
    runs-on: ubuntu-latest

    steps:
      - name: Print the input name to STDOUT
        run: echo The username is ${{ inputs.username }}
```

## `on.workflow_call.<input_id>.type`

Required if input is defined for the `on.workflow_call` keyword.

The value of this parameter is a string specifying the data type of the input.

This must be one of: `boolean`, `number`, or `string`.

## `on.workflow_call.secrets`

A map of the secrets that can be used in the called workflow.

Within the called workflow, you can use the `secrets` context to refer to a secret.

If a caller workflow passes a secret that is not specified in the called workflow, this results in an error.

**Example**

```yaml
on:
  workflow_call:
    secrets:
      access-token:
        description: 'A token passed from the caller workflow'
        required: false
  
jobs:
  pass-secret-to-action:
    runs-on: ubuntu-latest

    steps:  
      - name: Pass the received secret to an action
        uses: ./.github/actions/my-action@v1
        with:
          token: ${{ secrets.access-token }}
```

## `on.workflow-call.secrets.<secret_id>`

A string identifier to associate w/ the secret.

## `on.workflow_call.secrets.<secret_id>.required`

A boolean specifying whether the secret must be supplied.

## `on.workflow_dispatch.inputs`

When using the `workflow_dispatch` event, you can optionally specify inputs that are passed to the workflow.

Workflow dispatch inputs are specified w/ the same format as action input.

```yaml
on: 
  workflow_dispatch:
    inputs:
      logLevel:
        description: 'Log level'     
        required: true
        default: 'warning'
      tags:
        description: 'Test scenario tags'
        required: false
```

The triggered workflow receives the inputs in the `github.event.inputs` context. For more information, see "[Contexts](https://docs.github.com/en/actions/learn-github-actions/contexts#github-context)."

## `on.schedule`

You can schedule a workflow to run at specific UTC times using `POSIX cron syntax`.

Scheduled workflows run on the latest commit on the default or base branch.

The shortest interval you can run scheduled workflows is once every 5 minutes.

This example triggers the workflow every day at 5:30 and 17:30 UTC:

```yaml
on:
  schedule:
    # * is a special character in YAML so you have to quote this string
    - cron:  '30 5,17 * * *'

```

## `permissions`

You can modify the default permissions granted to the `GITHUB_TOKEN`, adding or removing access as required, so that you only allow the minimum required access. For more information, see "[Authentication in a workflow](https://docs.github.com/en/actions/reference/authentication-in-a-workflow#permissions-for-the-github_token)."

You can use `permissions` either as a top-level key, to apply to all jobs in the workflow, or within specific jobs.

When you add the `permissions` key within a specific job, all actions and run commands within that job that use the `GITHUB_TOKEN` gain the access rights you specify.

For more information, see [`jobs.<job_id>.permissions`](https://docs.github.com/en/actions/learn-github-actions/workflow-syntax-for-github-actions#jobsjob_idpermissions).

Available scopes and access values:

```yaml
permissions:
  actions: read|write|none
  checks: read|write|none
  contents: read|write|none
  deployments: read|write|none
  issues: read|write|none
  discussions: read|write|none
  packages: read|write|none
  pull-requests: read|write|none
  repository-projects: read|write|none
  security-events: read|write|none
  statuses: read|write|none
```

If you specify the access for any of these scopes, all of those that are not specified are set to `none`.

You can use the following syntax to define read or write access for all of the available scopes:

```yaml
permissions: read-all|write-all
```

You can use the `permissions` key to add and remove read permissions for forked repositories, but typically you can't grant write access.

The exception to this behavior is where an admin user has selected the **Send write tokens to workflows from pull requests** option in the GitHub Actions settings.

For more information, see "[Managing GitHub Actions settings for a repository](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/enabling-features-for-your-repository/managing-github-actions-settings-for-a-repository#enabling-workflows-for-private-repository-forks)."

**Example**

This example shows permissions being set for the `GITHUB_TOKEN` that will apply to all jobs in the workflow.

All permissions are granted read access.

```yaml
name: "My workflow"

on: [ push ]

permissions: read-all

jobs:
  ...
```

## `env`

A `map` of environment variables that are available to the steps of all jobs in the workflow.

You can also set environment variables that are only available to the steps of a single job or to a single step.

For more information, see [`jobs.<job_id>.env`](https://docs.github.com/en/actions/learn-github-actions/workflow-syntax-for-github-actions#jobsjob_idenv) and [`jobs.<job_id>.steps[*].env`](https://docs.github.com/en/actions/learn-github-actions/workflow-syntax-for-github-actions#jobsjob_idstepsenv).

When more than one environment variable is defined w/ the same name, GitHub uses the most specific environment variable.

For example, an environment variable defined in a step will override job and workflow variables w/ the same name, while the step executes.

A variable defined for a job will override a workflow variable w/ the same name, while the job executes.

**Example**

```yaml
env:
  SERVER: production
```

## `defaults`

A `map` of default settings that will apply to all jobs in the workflow.

You can also se default settings that are only available to a job.

For more information, see [`jobs.<job_id>.defaults`](https://docs.github.com/en/actions/learn-github-actions/workflow-syntax-for-github-actions#jobsjob_iddefaults).

When more than one default setting is defined w/ the same name, GitHub uses the most specific default setting.

For example, a default setting defined in a job will override a default setting that has the same name defined in a workflow.
