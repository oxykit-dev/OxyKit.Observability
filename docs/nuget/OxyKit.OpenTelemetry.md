# OxyKit.OpenTelemetry

Optional OpenTelemetry OTLP wiring for OxyKit ASP.NET Core hosts (`net10.0`). Off by default — enable with `OpenTelemetry:Enabled=true` or `OTEL_EXPORTER_OTLP_ENDPOINT`.

```bash
dotnet add package OxyKit.OpenTelemetry
```

Used by **Mail** and **Flow** only (keep off Render for AOT/size). Pair with [`OxyKit.Observability`](https://www.nuget.org/packages/OxyKit.Observability) for JSON logs + correlation.

Docs: [oxykit.dev/guide/observability](https://oxykit.dev/guide/observability) · Source: [oxykit-dev/OxyKit.Observability](https://github.com/oxykit-dev/OxyKit.Observability)
