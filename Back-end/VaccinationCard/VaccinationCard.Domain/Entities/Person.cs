namespace VaccinationCard.Domain.Entities
{
    public class Person : EntityBase
    {
        public string Name { get; set; }
        
        public List<VaccinationRecord> VaccinationRecords { get; set; } = [];

        public Person() { }

        public Person(string name)
        {
            Name = name;
        }
    }
}
