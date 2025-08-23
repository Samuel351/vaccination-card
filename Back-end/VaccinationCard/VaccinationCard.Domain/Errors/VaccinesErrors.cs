using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Domain.Errors
{
    public static class VaccinesErrors
    {
        public static string NoVaccines() =>
            $"No vaccines found.";
    }
}
