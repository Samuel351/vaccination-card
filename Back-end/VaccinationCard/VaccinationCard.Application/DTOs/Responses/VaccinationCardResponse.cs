using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Application.DTOs.Responses
{
    public sealed record VaccinationCardResponse(Guid VaccineId, string VaccineName, DateTime ApplicationDate, int DoseNumber);
    
}
