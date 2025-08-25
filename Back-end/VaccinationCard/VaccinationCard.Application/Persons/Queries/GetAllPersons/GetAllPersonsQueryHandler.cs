using Domain.Abstractions;
using MediatR;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Application.Persons.Queries.GetAllPersons;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;

namespace VaccinationCard.Application.Persons.Queries.GetAllPersonPaginated
{
    internal class GetAllPersonsQueryHandler(IPersonRepository personRepository) : IRequestHandler<GetAllPersonsQuery, Result<List<PersonResponse>>>
    {

        private readonly IPersonRepository _personRepository = personRepository;

        public async Task<Result<List<PersonResponse>>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            var paginatedPersons = await _personRepository.GetAllAsync();

            if (paginatedPersons.Count == 0) return Result<List<PersonResponse>>.Failure(PersonErrors.NoContent);

            return Result<List<PersonResponse>>.Success([.. paginatedPersons.Select(x => new PersonResponse(x.EntityId, x.Name, x.CPF, x.Email, x.PhoneNumber, x.Gender, x.Age))]);
        }
    }
}
