using Domain.Abstractions;
using MediatR;
using VaccinationCard.Application.DTOs.Responses;

namespace VaccinationCard.Application.Vaccines.Queries.GetAllVaccines
{
    public sealed record GetAllVaccineQuery() : IRequest<Result<List<VaccineResponse>>>;
}
