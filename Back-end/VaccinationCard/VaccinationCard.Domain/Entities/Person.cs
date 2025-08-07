using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Domain.Entities
{
    public class Person : EntityBase
    {
        public List<VaccinationRecord> VaccinationRecords { get; set; } = [];
    }
}
