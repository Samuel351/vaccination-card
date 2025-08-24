using Domain.Abstractions;
using MediatR;

namespace VaccinationCard.Application.Persons.Commands.DeletePerson
{
    public sealed record DeletePersonCommand(Guid personId) : IRequest<Result>;
}
