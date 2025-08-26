using Domain.Abstractions;
using MediatR;

namespace VaccinationCard.Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(string Email, string Password) : IRequest<Result>;
}
