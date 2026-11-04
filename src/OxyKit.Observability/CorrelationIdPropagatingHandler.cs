namespace OxyKit.Observability;

/// <summary>Adds ambient <see cref="CorrelationContext"/> to outbound HTTP as <c>X-Correlation-Id</c>.</summary>
public sealed class CorrelationIdPropagatingHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        var correlationId = CorrelationContext.Current;
        if (
            !string.IsNullOrWhiteSpace(correlationId)
            && !request.Headers.Contains(CorrelationHeaders.HeaderName)
        )
        {
            request.Headers.TryAddWithoutValidation(
                CorrelationHeaders.HeaderName,
                correlationId
            );
        }

        return base.SendAsync(request, cancellationToken);
    }
}
