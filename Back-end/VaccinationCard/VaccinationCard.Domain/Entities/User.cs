namespace VaccinationCard.Domain.Entities
{
    public class User : EntityBase
    {
        public string Email { get; set; } 

        public string Password { get; set; }

        public Guid? PersonId { get; set; }

        public Person? Person { get; set; }

    }
}
