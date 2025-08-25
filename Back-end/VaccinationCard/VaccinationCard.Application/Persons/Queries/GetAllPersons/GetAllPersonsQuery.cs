using Domain.Abstractions;
using MediatR;
using VaccinationCard.Application.DTOs.Responses;

namespace VaccinationCard.Application.Persons.Queries.GetAllPersons
{
    public sealed record GetAllPersonsQuery() : IRequest<Result<List<PersonResponse>>>;
 
}
