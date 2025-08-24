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
        public List<Vaccination> VaccineRecords { get; set; } = [];

        // Quantidade de doses que a vacina pode ter.
        public int RequiredDoses { get; set; }   

        public Vaccine() { }

        public Vaccine(string name, int requiredDoses)
        {
            Name = name;
            RequiredDoses = requiredDoses;
        }
    }
}
