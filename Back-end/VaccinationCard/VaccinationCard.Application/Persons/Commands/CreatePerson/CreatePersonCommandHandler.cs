using MediatR;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.Persons.Commands.CreatePerson
{
    internal class CreatePersonCommandHandler(IBaseRepository<Person> personRepository) : IRequestHandler<CreatePersonCommand, Result<bool>>
    {
        IBaseRepository<Person> _personRepository = personRepository;
        public async Task<Result<bool>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            await _personRepository.AddAsync(new Person(request.Name, request.CPF, request.Email, request.PhoneNumber, request.Gender, request.BirthDate));

            return Result<bool>.Success(true, ResultCode.Created);
        }
    }
}
