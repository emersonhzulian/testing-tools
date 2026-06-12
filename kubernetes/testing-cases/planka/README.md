# Planka on KIND

Self-hosted [Planka](https://planka.app/) (open-source Trello alternative) running on a local KIND cluster.

## Prerequisites

- KIND cluster running (`task` from the repo root to set it up if needed)
- `kubectl` configured against the cluster (`kubeconfig` at repo root)
- `task` (Taskfile runner)
- `openssl` (for secret key generation)

## Quick Start

### 1. Create your `.env`

```sh
cp .env.example .env
```

Generate a proper secret key and paste it into `.env`:

```sh
task generate-secret-key
```

Edit `.env` and fill in all values:

```env
PLANKA_SECRET_KEY=<output from above>
PLANKA_ADMIN_EMAIL=admin@example.com
PLANKA_ADMIN_PASSWORD=yourpassword
PLANKA_ADMIN_NAME=Your Name
```

### 2. Deploy everything

```sh
task full-setup
```

This runs in order:
1. Creates the `planka` namespace
2. Creates the `planka-config` Kubernetes Secret from your `.env`
3. Applies PVCs, Postgres, and Planka manifests
4. Waits for both deployments to be ready
5. Port-forwards Planka to `http://localhost:3000`

### 3. Open Planka

Browse to **http://localhost:3000** and log in with the email and password from your `.env`.

---

## Individual Tasks

| Task | Description |
|---|---|
| `task generate-secret-key` | Print a random 64-byte hex key to use as `PLANKA_SECRET_KEY` |
| `task create-namespace` | Create the `planka` namespace |
| `task create-secret` | Create the `planka-config` secret from `.env` |
| `task deploy` | Apply all manifests (PVCs, Postgres, Planka) |
| `task wait` | Wait for Postgres + Planka rollouts to complete |
| `task port-forward` | Forward `localhost:3000` → Planka service |
| `task teardown` | **Destructive** — delete the `planka` namespace and all data |

---

## What Gets Deployed

| Resource | Details |
|---|---|
| Namespace | `planka` |
| Postgres | `postgres:16-alpine`, PVC-backed (2Gi) |
| Planka | `ghcr.io/plankanban/planka:latest`, PVC-backed (1Gi) |
| Secret | `planka-config` — holds `SECRET_KEY` and admin credentials |

## Notes

- Credentials are stored in a Kubernetes Secret (`planka-config`) and never hardcoded in manifests.
- The admin user is bootstrapped automatically via `DEFAULT_ADMIN_*` env vars — no manual `npm run db:create-admin-user` step needed.
- Data is persisted in PVCs. Deleting the namespace (`task teardown`) will destroy all data.
- `TRUST_PROXY=true` is set so Planka correctly reads client IPs behind the cluster's internal routing.
