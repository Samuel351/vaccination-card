namespace VaccinationCard.Domain.Entities
{
    public class Person : EntityBase
    {
        public string Name { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }
        
        public List<Vaccination> Vaccinations { get; set; } = [];

        public Person() { }

        public Person(string name)
        {
            Name = name;
        }
    }
}
