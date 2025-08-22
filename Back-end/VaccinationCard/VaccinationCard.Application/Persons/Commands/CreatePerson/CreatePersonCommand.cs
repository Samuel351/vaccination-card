using MediatR;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.Persons.Commands.CreatePerson
{
    public sealed record CreatePersonCommand(string Name) : IRequest<Result<bool>>;
}
