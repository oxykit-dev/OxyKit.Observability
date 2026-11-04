# OxyKit.Observability — agent guide

Public operator docs: [`../oxykit-site/docs/guide/observability.md`](../oxykit-site/docs/guide/observability.md) → [oxykit.dev/guide/observability](https://oxykit.dev/guide/observability).

## What it is

Two packable libs for suite HTTP hosts. **No Serilog / Sentry.** Product meters (`MailTelemetry`, `FlowTelemetry`) stay in each host.

| Project | Consumers |
|---------|-----------|
| `OxyKit.Observability` | Render, Mail, Flow |
| `OxyKit.OpenTelemetry` | Mail, Flow only (keep off Render for AOT/size) |

## Layout

```text
src/OxyKit.Observability/     # JSON console, correlation
src/OxyKit.OpenTelemetry/     # optional OTLP host wiring
tests/OxyKit.Observability.Tests/
```

## Constraints

- Keep APIs tiny: `AddOxyKitJsonConsole`, `UseOxyKitCorrelation`, `AddOxyKitOpenTelemetry`
- OTel packages live **only** in `OxyKit.OpenTelemetry` — never add them to `OxyKit.Observability`
- Pin `OpenTelemetry.*` at suite shared version (`AGENTS.md` root)

## Packaging

- Both projects packable · versions must match · Trusted Publishing: [`docs/NUGET_PUBLISH.md`](docs/NUGET_PUBLISH.md)
- Workflow: `.github/workflows/release-nuget.yml` (tag `vX.Y.Z`)

## Suite wiring

Cores use **NuGet** `PackageReference` (`OxyKit.Observability` / `OxyKit.OpenTelemetry`). Local sibling ProjectReference only when actively developing these libs.
