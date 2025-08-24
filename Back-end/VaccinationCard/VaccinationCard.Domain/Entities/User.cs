namespace VaccinationCard.Domain.Entities
{
    public class User : EntityBase
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

    }
}
