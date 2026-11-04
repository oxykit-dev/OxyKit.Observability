using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OxyKit.Observability;

public sealed class CorrelationIdMiddleware(
    RequestDelegate next,
    ILogger<CorrelationIdMiddleware> logger
)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = ResolveCorrelationId(context);
        context.Response.Headers[CorrelationHeaders.HeaderName] = correlationId;
        CorrelationContext.Current = correlationId;
        Activity.Current?.SetTag("correlation.id", correlationId);

        using (
            logger.BeginScope(
                new Dictionary<string, object> { ["CorrelationId"] = correlationId }
            )
        )
        {
            try
            {
                await next(context);
            }
            finally
            {
                CorrelationContext.Current = null;
            }
        }
    }

    private static string ResolveCorrelationId(HttpContext context)
    {
        if (
            context.Request.Headers.TryGetValue(CorrelationHeaders.HeaderName, out var values)
            && !string.IsNullOrWhiteSpace(values.ToString())
        )
        {
            return values.ToString().Trim();
        }

        return Guid.NewGuid().ToString("N");
    }
}
