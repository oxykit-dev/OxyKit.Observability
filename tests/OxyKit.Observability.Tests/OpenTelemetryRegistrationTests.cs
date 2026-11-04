using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using OxyKit.OpenTelemetry;

namespace OxyKit.Observability.Tests;

public class OpenTelemetryRegistrationTests
{
    [Fact]
    public void AddOxyKitOpenTelemetry_WhenDisabled_IsNoOp()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Configuration["OpenTelemetry:Enabled"] = "false";

        var act = () =>
            builder.AddOxyKitOpenTelemetry(o =>
            {
                o.ServiceName = "OxyKit.Observability.Tests";
                o.ActivitySources = ["test"];
                o.Meters = ["test"];
            });

        act.Should().NotThrow();
    }

    [Fact]
    public void AddOxyKitOpenTelemetry_WhenEnabled_RegistersWithoutThrowing()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Configuration["OpenTelemetry:Enabled"] = "true";
        builder.Configuration["OpenTelemetry:ServiceName"] = "OxyKit.Observability.Tests";

        var act = () =>
            builder.AddOxyKitOpenTelemetry(o =>
            {
                o.ActivitySources = ["OxyKit.Observability.Tests"];
                o.Meters = ["OxyKit.Observability.Tests"];
            });

        act.Should().NotThrow();
        using var app = builder.Build();
        app.Should().NotBeNull();
    }
}
