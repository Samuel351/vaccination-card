namespace VaccinationCard.Domain.Entities
{
    public class Person : EntityBase
    {
        public string Name { get; private set; }

        public string CPF { get; private set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }

        public string Gender { get; private set; }

        public DateTime BirthDate { get; private set; }
        
        public List<Vaccination> Vaccinations { get; private set; } = [];

        public Person() { }


        public Person(string name, string cpf, string email, string phoneNumber, string gender, DateTime birthDate)
        {
            Name = name;
            CPF = cpf;
            Email = email;
            PhoneNumber = phoneNumber;
            Gender = gender;
            BirthDate = birthDate;
        }

        public void Update(Guid personId, string name, string cpf, string email, string phoneNumber, string gender, DateTime birthDate)
        {
            EntityId = personId;
            Name = name;
            CPF = cpf;
            Email = email;
            PhoneNumber = phoneNumber;
            Gender = gender;
            BirthDate = birthDate;
        }
    }
}
