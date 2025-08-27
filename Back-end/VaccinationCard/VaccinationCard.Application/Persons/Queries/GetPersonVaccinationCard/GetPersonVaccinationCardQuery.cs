using MediatR;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Persons.Queries.GetPersonVaccinationCard
{
    public sealed record GetPersonVaccinationCardQuery(Guid PersonId) : IRequest<Result<List<VaccinationCardResponse>>>;
}
