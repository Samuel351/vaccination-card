namespace VaccinationCard.Domain.Entities
{
    public class DoseType : EntityBase
    {
        public string Name { get; set; }
        public int Order { get; set; }

        public List<VaccinationRecord> VaccinationRecords { get; set; } = [];

        public List<VaccineDoseType> VaccineDoseTypes { get; set; } = [];

    }
}
