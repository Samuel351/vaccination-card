using MediatR;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.Persons.Commands.DeletePerson
{
    public sealed record DeletePersonCommandHandler(IBaseRepository<Person> personRepository) : IRequestHandler<DeletePersonCommand, Result<bool>>
    {

        private readonly IBaseRepository<Person> _personRepository = personRepository;

        public async Task<Result<bool>> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {

            var person = await _personRepository.GetByIdAsync(request.personId);

            if (person == null) return Result<bool>.Failure(PersonErrors.PersonNotFound(request.personId), ResultCode.NotFound);

            await _personRepository.DeleteAsync(request.personId);

            return Result<bool>.Success(true);
        }
    }
}
