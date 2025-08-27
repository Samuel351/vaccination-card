namespace VaccinationCard.Domain.Shared;

public sealed record Error(string Code, string Description, List<string>? Details = null)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static implicit operator string(Error error) => error.Description;
}