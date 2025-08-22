using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Domain.Entities
{
    public class Vaccine : EntityBase
    {
        public string Name { get; set; }
        public List<VaccinationRecord> VaccineRecords { get; set; } = [];

        public List<VaccineDoseType> VaccineDoseTypes { get; set; } = [];

        public Vaccine() { }

        public Vaccine(string name, List<Guid> doseTypeIds)
        {
            Name = name;
            doseTypeIds.ForEach(doseTypeId =>
            {
                VaccineDoseTypes.Add(new VaccineDoseType(this, doseTypeId));
            });
        }
    }
}
