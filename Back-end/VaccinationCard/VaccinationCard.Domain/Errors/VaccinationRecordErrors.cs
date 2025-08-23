using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Domain.Errors
{
    public static class VaccinationRecordErrors
    {
        public static string NoVaccinationRecord() =>
            $"No vaccination record found.";
    }
}
