# OxyKit.Observability

Shared host helpers for the [OxyKit](https://oxykit.dev) suite.

**Public docs:** [oxykit.dev/guide/observability](https://oxykit.dev/guide/observability)  
**NuGet:** [`OxyKit.Observability`](https://www.nuget.org/packages/OxyKit.Observability) · [`OxyKit.OpenTelemetry`](https://www.nuget.org/packages/OxyKit.OpenTelemetry)

| Package | Role |
|---------|------|
| **OxyKit.Observability** | JSON console logging + `X-Correlation-Id` middleware / ambient context / outbound `DelegatingHandler` |
| **OxyKit.OpenTelemetry** | Optional OTLP traces + metrics (off by default) — **not** referenced by Render |

```bash
dotnet add package OxyKit.Observability
dotnet add package OxyKit.OpenTelemetry   # Mail / Flow hosts only
```

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

Publish: [`docs/NUGET_PUBLISH.md`](./docs/NUGET_PUBLISH.md) (Trusted Publishing). Agent map: [`AGENTS.md`](./AGENTS.md).

```bash
dotnet test --project tests/OxyKit.Observability.Tests/OxyKit.Observability.Tests.csproj
```
