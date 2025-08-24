using Domain.Abstractions;
using MediatR;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Persons.Queries.GetAllPersonPaginated
{
    public sealed record GetAllPersonPaginatedQuery(int PageNumber, int PageSize, string? Query = null) : IRequest<Result<PaginatedResponse<PersonResponse>>>;
 
}
