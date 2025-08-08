using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Domain.Entities
{
    public class VaccineDoseType
    {
        public Guid VaccineId { get; set; }

        public Vaccine Vaccine { get; set; }

        public Guid DoseTypeId { get; set; }

        public DoseType DoseType { get; set; }
    }
}
