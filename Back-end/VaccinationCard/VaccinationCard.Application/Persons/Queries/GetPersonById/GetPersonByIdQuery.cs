using MediatR;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Persons.Queries.GetPersonById
{
    public sealed record GetPersonByIdQuery(Guid personId) : IRequest<Result<PersonResponse>>;
}
