using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.Application.Vaccines.DTOs.Responses;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.Vaccines.Queries.GetAllVaccines
{
    public sealed record GetAllVaccineQuery() : IRequest<Result<List<VaccineResponse>>>;
}
