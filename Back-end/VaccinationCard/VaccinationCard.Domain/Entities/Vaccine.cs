namespace VaccinationCard.Domain.Entities
{
    public class Vaccine : EntityBase
    {
        public string Name { get; set; }
        public List<Vaccination> VaccineRecords { get; set; } = [];

        // Quantidade de doses que a vacina pode ter.
        public int RequiredDoses { get; set; }   

        public void Update(Guid vaccineId , string name, int requiredDoses)
        {
            EntityId = vaccineId;
            Name = name;
            RequiredDoses = requiredDoses;
        }

        public Vaccine(string name, int requiredDoses)
        {
            Name = name;
            RequiredDoses = requiredDoses;
        }
    }
}
