using MediatR;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Authentication.Commands.Login
{
    public sealed record LoginCommand(string Email, string Password) : IRequest<Result<TokenResponse>>;
}
