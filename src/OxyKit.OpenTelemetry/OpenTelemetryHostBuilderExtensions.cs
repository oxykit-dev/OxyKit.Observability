using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace OxyKit.OpenTelemetry;

public static class OpenTelemetryHostBuilderExtensions
{
    /// <summary>
    /// Optional OTLP traces + metrics. Off unless <c>OpenTelemetry:Enabled</c> or
    /// <c>OTEL_EXPORTER_OTLP_ENDPOINT</c> is set. Misconfiguration logs a warning and continues.
    /// </summary>
    public static WebApplicationBuilder AddOxyKitOpenTelemetry(
        this WebApplicationBuilder builder,
        Action<OxyKitOpenTelemetryOptions>? configure = null
    )
    {
        var options = new OxyKitOpenTelemetryOptions();
        builder.Configuration.GetSection(OxyKitOpenTelemetryOptions.SectionName).Bind(options);
        configure?.Invoke(options);

        var otlpEndpoint = Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT");
        var enabled = options.Enabled || !string.IsNullOrWhiteSpace(otlpEndpoint);
        if (!enabled)
            return builder;

        var serviceName =
            Environment.GetEnvironmentVariable("OTEL_SERVICE_NAME")
            ?? (
                string.IsNullOrWhiteSpace(options.ServiceName) ? "OxyKit" : options.ServiceName
            );

        try
        {
            builder
                .Services.AddOpenTelemetry()
                .ConfigureResource(r => r.AddService(serviceName))
                .WithTracing(tracing =>
                {
                    foreach (var source in options.ActivitySources)
                    {
                        if (!string.IsNullOrWhiteSpace(source))
                            tracing.AddSource(source);
                    }

                    tracing
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddOtlpExporter();
                })
                .WithMetrics(metrics =>
                {
                    foreach (var meter in options.Meters)
                    {
                        if (!string.IsNullOrWhiteSpace(meter))
                            metrics.AddMeter(meter);
                    }

                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddRuntimeInstrumentation()
                        .AddOtlpExporter();
                });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(
                $"OxyKit.OpenTelemetry: registration failed; continuing without exporters. {ex.Message}"
            );
        }

        return builder;
    }
}
