using MediatR;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.Persons.Commands.CreatePerson
{
    public sealed record CreatePersonCommand(string Name, string CPF, string Email, string PhoneNumber, string Gender, DateTime BirthDate) : IRequest<Result<bool>>;
}
