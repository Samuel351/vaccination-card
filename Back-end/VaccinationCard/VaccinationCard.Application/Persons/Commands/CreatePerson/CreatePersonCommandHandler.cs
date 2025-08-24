using Domain.Abstractions;
using MediatR;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Application.Persons.Commands.CreatePerson
{
    internal class CreatePersonCommandHandler(IBaseRepository<Person> personRepository) : IRequestHandler<CreatePersonCommand, Result>
    {
        private readonly IBaseRepository<Person> _personRepository = personRepository;
        public async Task<Result> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            await _personRepository.AddAsync(new Person(request.Name, request.CPF, request.Email, request.PhoneNumber, request.Gender, request.BirthDate));

            return Result.Success();
        }
    }
}
