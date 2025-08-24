using Domain.Abstractions;
using MediatR;
using System.Net;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;

namespace VaccinationCard.Application.Persons.Commands.UpdatePerson
{
    internal class UpdatePersonCommandHandler(IBaseRepository<Person> personRepository) : IRequestHandler<UpdatePersonCommand, Result>
    {

        private readonly IBaseRepository<Person> _personRepository = personRepository;

        public async Task<Result> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            // verificar se existe 
            var person = await _personRepository.GetByIdAsync(request.PersonId);

            if(person == null)
            {
                return Result.Failure(PersonErrors.NotFound, HttpStatusCode.NotFound);
            }

            person.Update(request.PersonId, request.Name, request.CPF, request.Email, request.PhoneNumber, request.Gender, request.BirthDate);

            await _personRepository.UpdateAsync(person);

            return Result.Success();
        }
    }
}
