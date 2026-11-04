using Microsoft.AspNetCore.Builder;

namespace OxyKit.Observability;

public static class CorrelationApplicationBuilderExtensions
{
    public static IApplicationBuilder UseOxyKitCorrelation(this IApplicationBuilder app) =>
        app.UseMiddleware<CorrelationIdMiddleware>();
}
