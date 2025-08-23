using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Application.Vaccines.DTOs.Requests
{
    public sealed record CreateVaccineRequest(string Name, int DoseQuantity);
}
