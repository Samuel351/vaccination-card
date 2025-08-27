using Domain.Abstractions;
using MediatR;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Errors;
using VaccinationCard.Domain.Interfaces.Repositories;

namespace VaccinationCard.Application.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandHandler(IPersonRepository personRepository) : IRequestHandler<CreatePersonCommand, Result>
    {
        private readonly IPersonRepository _personRepository = personRepository;
        public async Task<Result> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {

            if(await _personRepository.CPFExists(request.CPF))
            {
                return Result.Failure(PersonErrors.CPFAlreadyExists);
            }

            if(await _personRepository.EmailExists(request.Email))
            {
                return Result.Failure(PersonErrors.EmailAlreadyExists);
            }

            await _personRepository.AddAsync(new Person(request.Name, request.CPF, request.Email, request.PhoneNumber, request.Gender, request.Age));

            return Result.Success();
        }
    }
}
