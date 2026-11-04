# Changelog

Format: [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## [Unreleased]

## [0.1.0] - 2026-11-04

### Added

- `OxyKit.Observability` — JSON console + `X-Correlation-Id` middleware, ambient context, outbound propagating handler
- `OxyKit.OpenTelemetry` — optional OTLP registration (`OpenTelemetry:Enabled` or `OTEL_EXPORTER_OTLP_ENDPOINT`)
- Packable packages (`net10.0`) with symbol packages + package READMEs
- Trusted Publishing release workflow (`.github/workflows/release-nuget.yml`); CI
