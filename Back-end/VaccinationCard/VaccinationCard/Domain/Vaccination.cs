namespace VaccinationCard.Domain
{
    public class Vaccination
    {
        public Guid PersonId { get; set; }

        public Person? Person { get; set; }

        public Guid VaccineId { get; set; }

        public Vaccine? Vaccine { get; set; }

    }
}
