# OxyKit.Observability

Shared host helpers for the [OxyKit](https://oxykit.dev) suite.

**Public docs:** [oxykit.dev/guide/observability](https://oxykit.dev/guide/observability)

| Package | Role |
|---------|------|
| **OxyKit.Observability** | JSON console logging + `X-Correlation-Id` middleware / ambient context / outbound `DelegatingHandler` |
| **OxyKit.OpenTelemetry** | Optional OTLP traces + metrics (off by default) — **not** referenced by Render |

```csharp
builder.AddOxyKitJsonConsole();
app.UseOxyKitCorrelation();

// Mail / Flow only:
builder.AddOxyKitOpenTelemetry(o =>
{
    o.ServiceName = "OxyKit.Mail";
    o.ActivitySources = ["OxyKit.Mail"];
    o.Meters = ["OxyKit.Mail"];
});
```

Enable OTel with `OpenTelemetry:Enabled=true` or `OTEL_EXPORTER_OTLP_ENDPOINT`.

Suite checkout: cores use **ProjectReference** to this folder. NuGet publish TBD (`oxykit-dev/OxyKit.Observability`). Agent map: [`AGENTS.md`](./AGENTS.md).

```bash
dotnet test --project tests/OxyKit.Observability.Tests/OxyKit.Observability.Tests.csproj
```
