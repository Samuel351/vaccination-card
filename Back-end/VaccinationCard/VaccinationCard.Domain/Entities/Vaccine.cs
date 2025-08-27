namespace VaccinationCard.Domain.Entities
{
    /// <summary>
    /// Represents a vaccine, including its name, required doses, and associated vaccination records.
    /// </summary>
    public class Vaccine : EntityBase
    {
        /// <summary>
        /// The name of the vaccine.
        /// </summary>
        public string Name { private set; get; }

        /// <summary>
        /// The collection of vaccination records that reference this vaccine.
        /// </summary>
        public List<Vaccination> VaccineRecords { private set; get; } = [];

        /// <summary>
        /// The total number of doses required to complete this vaccine's treatment schedule.
        /// </summary>
        public int RequiredDoses { private set; get; }

        /// <summary>
        /// Updates the vaccine details, including its name and required doses.
        /// </summary>
        /// <param name="name">The new name of the vaccine.</param>
        /// <param name="requiredDoses">The updated total number of doses required.</param>
        public void Update(string name, int requiredDoses)
        {
            Name = name;
            RequiredDoses = requiredDoses;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Vaccine"/> class.
        /// </summary>
        /// <param name="name">The name of the vaccine.</param>
        /// <param name="requiredDoses">The total number of doses required for full immunization.</param>
        public Vaccine(string name, int requiredDoses)
        {
            Name = name;
            RequiredDoses = requiredDoses;
        }

        public Vaccine() { }
    }
}
