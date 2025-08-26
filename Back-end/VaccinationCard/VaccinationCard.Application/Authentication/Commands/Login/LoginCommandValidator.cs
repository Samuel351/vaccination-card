using FluentValidation;
using VaccinationCard.Domain.Errors;

namespace VaccinationCard.Application.Authentication.Commands.Login
{
    internal class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(AuthenticationErrors.InvalidCredentials);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(AuthenticationErrors.InvalidCredentials);
        }
    }
}
