# DTR Security Features

## Security Scanning w/ DTR

Docker Trusted Registry has the ability to scan images for security vulnerabilities.

You can enable security scanning via the DTR web UI.

## Vulnerability Scanning

Docker Trusted Registry can scan our images for security vulnerabilities. We can enable this option via the DTR UI.

By default, we must initiate scans manually, but we can choose to have images scanned automatically on push in the repository settings.

We can also mark repositories as immutable, which prevents users from overwriting existing tags with a new image.

## Immutable Tags

You can set **Immutability** on a repository at any time. This prevents users from overwriting existing tags.
