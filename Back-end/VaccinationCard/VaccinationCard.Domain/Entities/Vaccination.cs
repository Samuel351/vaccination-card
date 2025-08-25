namespace VaccinationCard.Domain.Entities
{
    /// <summary>
    /// Represents a vaccination record associated with a specific person and vaccine.
    /// </summary>
    public class Vaccination : EntityBase
    {
        /// <summary>
        /// he date when the vaccine was applied.
        /// </summary>
        public DateTime ApplicationDate { get; private set; }

        /// <summary>
        /// The unique identifier of the vaccine administered.
        /// </summary>
        public Guid VaccineId { get; private set; }

        /// <summary>
        /// The vaccine entity associated with this vaccination record.
        /// </summary>
        public Vaccine Vaccine { get; private set; }

        /// <summary>
        /// The unique identifier of the person who received the vaccination.
        /// </summary>
        public Guid PersonId { get; private set; }

        /// <summary>
        /// The person entity associated with this vaccination record.
        /// </summary>
        public Person Person { get; private set; }

        /// <summary>
        /// The dose number of the vaccine (e.g., 1 for first dose, 2 for second dose).
        /// </summary>
        public int DoseNumber { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vaccination"/> class.
        /// </summary>
        /// <param name="vaccineId">The unique identifier of the vaccine administered.</param>
        /// <param name="personId">The unique identifier of the person receiving the vaccination.</param>
        /// <param name="doseNumber">The sequence number of the dose applied.</param>
        /// <param name="applicationDate">The date when the vaccine was applied.</param>
        public Vaccination(Guid vaccineId, Guid personId, int doseNumber, DateTime applicationDate)
        {
            VaccineId = vaccineId;
            PersonId = personId;
            DoseNumber = doseNumber;
            ApplicationDate = applicationDate;
        }

        /// <summary>
        /// Updates the vaccination details, such as dose number and application date.
        /// </summary>
        /// <param name="doseNumber">The new dose number.</param>
        /// <param name="applicationDate">The new application date of the vaccine.</param>
        public void Update(int doseNumber, DateTime applicationDate)
        {
            DoseNumber = doseNumber;
            ApplicationDate = applicationDate;
        }
    }
}
