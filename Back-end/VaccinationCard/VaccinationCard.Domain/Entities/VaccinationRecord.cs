using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Domain.Entities
{
    public class VaccinationRecord 
    {
        public DateTime VaccinationDate { get; set; }

        public Guid VaccineId { get; set; }

        public required Vaccine Vaccine { get; set; } 

        public Guid PersonId { get; set; }

        public required Person Person { get; set; }

        public Guid VaccineDoseTypeId { get; set; }

        public required DoseType VaccineDoseType { get; set; }
    }
}
