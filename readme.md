# Kubernetes Testing Cases

This repo contains testing cases for different Kubernetes observability and tooling setups.

Each testing case lives inside `kubernetes/testing-cases/` and has its own `README` with setup instructions, prerequisites, and how to run it.

## Testing Cases

| Case | Description |
|------|-------------|
| [grafana-cloud](kubernetes/testing-cases/grafana-cloud/REDME.md) | Auto-instrumented .NET APIs exporting traces, metrics and logs to Grafana Cloud via the OpenTelemetry Operator |

## Dev Container

This repo includes a [Dev Container](.devcontainer/) with all required tools pre-installed. It is the recommended way to work with this repo — just open it in VS Code and reopen in container.

## Prerequisites

If not using the Dev Container, you will need:

- [Docker](https://docs.docker.com/get-docker/)
- [kind](https://kind.sigs.k8s.io/docs/user/quick-start/#installation)
- [kubectl](https://kubernetes.io/docs/tasks/tools/)
- [Helm](https://helm.sh/docs/intro/install/)
- [Task](https://taskfile.dev/installation/) — used to run all tasks in this repo

## Running a test case

All tasks are accessible from the repo root via [Taskfile.yml](Taskfile.yml). Refer to the README inside each testing case folder for the specific steps.
