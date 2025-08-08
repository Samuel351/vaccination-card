namespace VaccinationCard.Domain.Entities
{
    public class VaccineDoseType : EntityBase
    {
        public int Order { get; set; }

        public List<VaccinationRecord> VaccinationRecords { get; set; } = [];
    }
}
