using FluentAssertions;
using OxyKit.Observability;

namespace OxyKit.Observability.Tests;

public class CorrelationContextTests
{
    [Fact]
    public void Current_IsAmbientPerAsyncFlow()
    {
        CorrelationContext.Current = "abc";
        CorrelationContext.Current.Should().Be("abc");
        CorrelationContext.Current = null;
        CorrelationContext.Current.Should().BeNull();
    }

    [Fact]
    public void HeaderName_IsStable()
    {
        CorrelationHeaders.HeaderName.Should().Be("X-Correlation-Id");
    }
}
