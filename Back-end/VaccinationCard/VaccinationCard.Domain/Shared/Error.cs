namespace Domain.Abstractions;

public sealed record Error(string Code, string? Description = null, List<string>? Details = null)
{
    public static readonly Error None = new(string.Empty);
}