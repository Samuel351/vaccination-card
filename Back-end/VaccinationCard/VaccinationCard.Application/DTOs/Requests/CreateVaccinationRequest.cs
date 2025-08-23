using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Application.DTOs.Requests
{
    public sealed record CreateVaccinationRequest(Guid VaccineId, Guid PersonId, int DoseNumber, DateTime VaccinationDate);
}
