using System.Reflection;
using System.Xml.Linq;

namespace VaccinationCard.Domain.Entities
{
    public class Vaccination : EntityBase
    {
        public DateTime ApplicationDate { get; private set; }

        public Guid VaccineId { get; private set; }

        public Vaccine Vaccine { get; private set; } 

        public Guid PersonId { get; private set; }

        public Person Person { get; private set; }

        public int DoseNumber { get; private set; } 

        public Vaccination(Guid vaccineId, Guid personId, int doseNumber, DateTime applicationDate)
        {
            VaccineId = vaccineId;
            PersonId = personId;
            DoseNumber = doseNumber;
            ApplicationDate = applicationDate;
        }
        public void Update(int doseNumber, DateTime applicationDate)
        {
            DoseNumber = doseNumber;
            ApplicationDate = applicationDate;
        }
    }
}
