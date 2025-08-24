using Domain.Abstractions;
using MediatR;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Persons.Queries.GetAllPersonPaginated
{
    internal class GetAllPersonPaginatedQueryHandler(IPersonRepository personRepository) : IRequestHandler<GetAllPersonPaginatedQuery, Result<PaginatedResponse<PersonResponse>>>
    {

        private readonly IPersonRepository _personRepository = personRepository;

        public async Task<Result<PaginatedResponse<PersonResponse>>> Handle(GetAllPersonPaginatedQuery request, CancellationToken cancellationToken)
        {
            var paginatedPersons = await _personRepository.GetAllPersonPaginated(request.PageNumber, request.PageSize, request.Query);

            if (!paginatedPersons.Items.Any()) return Result<PaginatedResponse<PersonResponse>>.Failure(PersonErrors.NoContent);

            return Result<PaginatedResponse<PersonResponse>>.Success(new PaginatedResponse<PersonResponse>(paginatedPersons.Items.Select(x => new PersonResponse(x.EntityId, x.Name, x.CPF, x.Email, x.PhoneNumber, x.Gender, x.BirthDate)), paginatedPersons.TotalPages, paginatedPersons.PageNumber, paginatedPersons.PageSize));
        }
    }
}
