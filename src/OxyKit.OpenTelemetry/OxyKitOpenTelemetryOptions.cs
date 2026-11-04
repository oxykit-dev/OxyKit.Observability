namespace OxyKit.OpenTelemetry;

public sealed class OxyKitOpenTelemetryOptions
{
    public const string SectionName = "OpenTelemetry";

    /// <summary>When true, register OTLP traces + metrics. Also enabled if <c>OTEL_EXPORTER_OTLP_ENDPOINT</c> is set.</summary>
    public bool Enabled { get; set; }

    public string ServiceName { get; set; } = "OxyKit";

    /// <summary>Custom <see cref="System.Diagnostics.ActivitySource"/> names to register.</summary>
    public string[] ActivitySources { get; set; } = [];

    /// <summary>Custom <see cref="System.Diagnostics.Metrics.Meter"/> names to register.</summary>
    public string[] Meters { get; set; } = [];
}
