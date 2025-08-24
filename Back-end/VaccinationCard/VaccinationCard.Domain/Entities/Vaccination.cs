namespace VaccinationCard.Domain.Entities
{
    public class Vaccination : EntityBase
    {
        public DateTime ApplicationDate { get; set; }

        public Guid VaccineId { get; set; }

        public Vaccine Vaccine { get; set; } 

        public Guid PersonId { get; set; }

        public Person Person { get; set; }

        public int DoseNumber { get; set; } 

        public Vaccination() { }

        public Vaccination(Guid vaccineId, Guid personId, int doseNumber, DateTime vaccinationDate)
        {
            VaccineId = vaccineId;
            PersonId = personId;
            DoseNumber = doseNumber;
            ApplicationDate = vaccinationDate;
        }

    }
}
