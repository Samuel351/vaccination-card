namespace VaccinationCard.Infrastructure.Options
{
    public class TokenSettings
    {
        public const string TokenConfiguration = "TokenConfiguration";
        public required string JwtSecret { get; set; }
    }
}
