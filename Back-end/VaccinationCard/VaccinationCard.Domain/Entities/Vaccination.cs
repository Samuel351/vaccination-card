using System.Reflection;
using System.Xml.Linq;

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
