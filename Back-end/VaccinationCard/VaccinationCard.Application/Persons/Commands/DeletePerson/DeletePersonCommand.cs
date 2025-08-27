using MediatR;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Persons.Commands.DeletePerson
{
    public sealed record DeletePersonCommand(Guid personId) : IRequest<Result>;
}
