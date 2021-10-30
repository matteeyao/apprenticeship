# I. Codebase

One codebase tracked in revision control, many deploys

*code repository* - copy of the revision tracking database

A *codebase* is any single repo (in a centralized revision control system like Subversion), or any set of repos who share a root commit (in a decentralized revision control system like Git).

One-to-one correlation btwn the codebase and the app:

* If multiple codebases, it's not an app - it's a distributed system. Each component in a distributed system is an app, and each can individually comply w/ twelve-factor.

* Multiple apps sharing the same code is a violation of twelve-factor. Instead, factor shared code into libraries which can be included through the **dependency manager**.

Only one codebase per app, but many deploys of the app.

A *deploy* is a running instance of the app, typically a production site, and one or more staging sites.

Additionally, every dev has a copy of the app running in their local development environment, each of which also qualifies as a deploy.

The codebase is the same across all deploys, although different versions may be active in each deploy.

For example, a dev has some commits not yet deployed to staging; staging has some commits not yet deployed to production.

But they all share the same codebase, thus making them identifiable as different deploys of the same app.
