using Domain.Abstractions;
using MediatR;
using VaccinationCard.Application.DTOs.Responses;

namespace VaccinationCard.Application.Authentication.Commands.Login
{
    public sealed record LoginCommand(string Email, string Password) : IRequest<Result<TokenResponse>>;
}
