using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Application.DTOs.Responses
{
    public sealed record VaccineDoseResponse(Guid VaccinationId, DateTime ApplicationDate, int DoseNumber);
}
