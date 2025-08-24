using Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.Application.DTOs.Responses;

namespace VaccinationCard.Application.Vaccines.Queries.GetVaccineById
{
    public sealed record GetVaccineByIdQuery(Guid VaccineId) : IRequest<Result<VaccineResponse>>;
}
