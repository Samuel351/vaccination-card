using Domain.Abstractions;
using MediatR;
using VaccinationCard.Application.DTOs.Responses;

namespace VaccinationCard.Application.Persons.Queries.GetPersonVaccinationCard
{
    public sealed record GetPersonVaccinationCardQuery(Guid PersonId) : IRequest<Result<List<VaccinationCardResponse>>>;
}
