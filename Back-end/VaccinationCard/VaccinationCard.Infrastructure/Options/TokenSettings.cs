namespace VaccinationCard.Infrastructure.Options
{
    public class TokenSettings
    {
        public const string TokenConfiguration = "TokenConfiguration";
        public required string JwtSecret { get; set; }

        public required string Issuer { get; set; }

        public required string Audience { get; set; }

        public required double TokenLifetimeHours { get; set; }
    }
}
