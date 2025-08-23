using MediatR;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.Persons.Queries.GetPersonById
{
    internal class GetPersonByIdQueryHandler(IBaseRepository<Person> personRepository) : IRequestHandler<GetPersonByIdQuery, Result<PersonResponse>>
    {

        private readonly IBaseRepository<Person> _personRepository = personRepository;

        public async Task<Result<PersonResponse>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.personId);
            if (person == null) return Result<PersonResponse>.Failure(PersonErrors.PersonNotFound(request.personId), ResultCode.NotFound);

            return Result<PersonResponse>.Success(new PersonResponse(person.EntityId, person.Name));
        }
    }
}
