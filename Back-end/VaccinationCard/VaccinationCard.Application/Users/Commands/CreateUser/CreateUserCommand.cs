using MediatR;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(string Email, string Password) : IRequest<Result>;
}
