using MediatR;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Vaccines.Queries.GetAllVaccines
{
    public sealed record GetAllVaccineQuery() : IRequest<Result<List<VaccineResponse>>>;
}
