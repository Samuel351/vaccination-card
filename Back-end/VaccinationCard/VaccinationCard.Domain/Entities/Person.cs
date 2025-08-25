namespace VaccinationCard.Domain.Entities
{
    public class Person : EntityBase
    {
        public string Name { get; private set; }

        public string CPF { get; private set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }

        public string Gender { get; private set; }

        public int Age { get; private set; }
        
        public List<Vaccination> Vaccinations { get; private set; } = [];

        public Person() { }

        public Person(string name, string cpf, string email, string phoneNumber, string gender, int age)
        {
            Name = name;
            CPF = cpf;
            Email = email;
            PhoneNumber = phoneNumber;
            Gender = gender;
            Age = age;
        }

        public void Update(string name, string cpf, string email, string phoneNumber, string gender, int age)
        {
            Name = name;
            CPF = cpf;
            Email = email;
            PhoneNumber = phoneNumber;
            Gender = gender;
            Age = age;
        }
    }
}
