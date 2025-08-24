using Domain.Abstractions;
using MediatR;
using VaccinationCard.Application.DTOs.Responses;

namespace VaccinationCard.Application.Persons.Queries.GetPersonById
{
    public sealed record GetPersonByIdQuery(Guid personId) : IRequest<Result<PersonResponse>>;
}
