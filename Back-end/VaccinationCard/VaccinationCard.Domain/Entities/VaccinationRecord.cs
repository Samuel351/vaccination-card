namespace VaccinationCard.Domain.Entities
{
    public class VaccinationRecord : EntityBase
    {
        public DateTime VaccinationDate { get; set; }

        public Guid VaccineId { get; set; }

        public Vaccine Vaccine { get; set; } 

        public Guid PersonId { get; set; }

        public Person Person { get; set; }

        public int DoseNumber { get; set; } 

        public VaccinationRecord() { }

        public VaccinationRecord(Guid vaccineId, Guid personId, int doseNumber, DateTime vaccinationDate)
        {
            VaccineId = vaccineId;
            PersonId = personId;
            DoseNumber = doseNumber;
            VaccinationDate = vaccinationDate;
        }

    }
}
