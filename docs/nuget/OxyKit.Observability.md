# OxyKit.Observability

JSON console logging and `X-Correlation-Id` helpers for OxyKit ASP.NET Core hosts (`net10.0`).

```bash
dotnet add package OxyKit.Observability
```

```csharp
builder.AddOxyKitJsonConsole();
app.UseOxyKitCorrelation();
```

Docs: [oxykit.dev/guide/observability](https://oxykit.dev/guide/observability) · Source: [oxykit-dev/OxyKit.Observability](https://github.com/oxykit-dev/OxyKit.Observability)
