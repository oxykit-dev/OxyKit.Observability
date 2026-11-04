namespace OxyKit.Observability;

/// <summary>Ambient correlation id for the current async flow (HTTP or background work).</summary>
public static class CorrelationContext
{
    private static readonly AsyncLocal<string?> CurrentId = new();

    public static string? Current
    {
        get => CurrentId.Value;
        set => CurrentId.Value = value;
    }
}
