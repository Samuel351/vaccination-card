namespace VaccinationCard.Domain.Entities
{
    public class Vaccine : EntityBase
    {
        public string Name { get; private set; }
        public List<Vaccination> VaccineRecords { get; private set; } = [];

        // Quantidade de doses que a vacina pode ter.
        public int RequiredDoses { get; set; }   

        public void Update(string name, int requiredDoses)
        {
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
