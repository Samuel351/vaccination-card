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

        public Person(string name, string cpf, string email, string phoneNumber, string gender, DateTime birthDate) : this(name)
        {
            CPF = cpf;
            Email = email;
            PhoneNumber = phoneNumber;
            Gender = gender;
            BirthDate = birthDate;
        }
    }
}
