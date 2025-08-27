using MediatR;
using System.Net;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Persons.Commands.DeletePerson
{
    public sealed record DeletePersonCommandHandler(IBaseRepository<Person> personRepository) : IRequestHandler<DeletePersonCommand, Result>
    {

        private readonly IBaseRepository<Person> _personRepository = personRepository;

        public async Task<Result> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.personId);

            if (person == null) return Result.Failure(PersonErrors.NotFound, HttpStatusCode.NotFound);

            await _personRepository.DeleteAsync(request.personId);

            return Result.Success();
        }
}
    }
