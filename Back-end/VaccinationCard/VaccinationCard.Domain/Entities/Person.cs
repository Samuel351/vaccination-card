namespace VaccinationCard.Domain.Entities
{
    /// <summary>
    /// Represents a person who may receive vaccinations, including their personal details and vaccination history.
    /// </summary>
    public class Person : EntityBase
    {
        /// <summary>
        /// The full name of the person.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The CPF (Cadastro de Pessoas Físicas) identifier of the person.
        /// </summary>
        public string CPF { get; private set; }

        /// <summary>
        /// The email address of the person.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// The phone number of the person.
        /// </summary>
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// The gender of the person.
        /// </summary>
        public string Gender { get; private set; }

        /// <summary>
        /// The age of the person.
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// The list of vaccinations associated with the person.
        /// </summary>
        public List<Vaccination> Vaccinations { get; private set; } = [];

        /// <summary>
        /// Initializes an empty instance of the <see cref="Person"/> class.
        /// Required for ORM.
        /// </summary>
        public Person() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class with the specified details.
        /// </summary>
        /// <param name="name">The full name of the person.</param>
        /// <param name="cpf">The CPF identifier of the person.</param>
        /// <param name="email">The email address of the person.</param>
        /// <param name="phoneNumber">The phone number of the person.</param>
        /// <param name="gender">The gender of the person.</param>
        /// <param name="age">The age of the person.</param>
        public Person(string name, string cpf, string email, string phoneNumber, string gender, int age)
        {
            Name = name;
            CPF = cpf;
            Email = email;
            PhoneNumber = phoneNumber;
            Gender = gender;
            Age = age;
        }

        /// <summary>
        /// Updates the details of the person.
        /// </summary>
        /// <param name="name">The updated full name of the person.</param>
        /// <param name="cpf">The updated CPF of the person.</param>
        /// <param name="email">The updated email address of the person.</param>
        /// <param name="phoneNumber">The updated phone number of the person.</param>
        /// <param name="gender">The updated gender of the person.</param>
        /// <param name="age">The updated age of the person.</param>
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
