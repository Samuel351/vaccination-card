using MediatR;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Persons.Commands.CreatePerson
{
    public sealed record CreatePersonCommand(string Name, string CPF, string Email, string PhoneNumber, string Gender, int Age) : IRequest<Result>;
}
