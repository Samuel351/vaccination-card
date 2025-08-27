using MediatR;
using System.Net;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler(IPersonRepository personRepository) : IRequestHandler<UpdatePersonCommand, Result>
    {

        private readonly IPersonRepository _personRepository = personRepository;

        public async Task<Result> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId);
            if (person == null)
            {
                return Result.Failure(PersonErrors.NotFound, HttpStatusCode.NotFound);
            }

            var cpfOwner = await _personRepository.GetPersonByCPF(request.CPF);
            if (cpfOwner != null && cpfOwner.EntityId != request.PersonId)
            {
                return Result.Failure(PersonErrors.CPFAlreadyExists);
            }
                

            var emailOwner = await _personRepository.GetPersonByEmail(request.Email);
            if (emailOwner != null && emailOwner.EntityId != request.PersonId)
            {
                return Result.Failure(PersonErrors.EmailAlreadyExists);
            }

            person.Update(
                request.Name,
                request.CPF,
                request.Email,
                request.PhoneNumber,
                request.Gender,
                request.Age
            );

            await _personRepository.UpdateAsync(person);
            return Result.Success();
        }
    }
}
