namespace VaccinationCard.Domain.Entities
{
    public class EntityBase
    {
        public Guid EntityId { get; private set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
