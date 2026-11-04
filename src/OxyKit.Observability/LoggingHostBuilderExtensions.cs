using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OxyKit.Observability;

public static class LoggingHostBuilderExtensions
{
    /// <summary>
    /// JSON console in non-Development (and when <c>Logging:JsonConsole=true</c>).
    /// Testing stays human-readable unless forced.
    /// </summary>
    public static WebApplicationBuilder AddOxyKitJsonConsole(this WebApplicationBuilder builder)
    {
        var forced = builder.Configuration.GetValue<bool?>("Logging:JsonConsole");
        var useJson =
            forced
            ?? (
                !builder.Environment.IsDevelopment()
                && !string.Equals(
                    builder.Environment.EnvironmentName,
                    "Testing",
                    StringComparison.OrdinalIgnoreCase
                )
            );

        if (!useJson)
            return builder;

        builder.Logging.ClearProviders();
        builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
        builder.Logging.AddJsonConsole(options =>
        {
            options.IncludeScopes = true;
            options.TimestampFormat = "yyyy-MM-dd'T'HH:mm:ss.fff'Z'";
            options.UseUtcTimestamp = true;
        });
        return builder;
    }
}
