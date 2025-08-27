using MediatR;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Persons.Queries.GetAllPersons
{
    public sealed record GetAllPersonsQuery() : IRequest<Result<List<PersonResponse>>>;
 
}
