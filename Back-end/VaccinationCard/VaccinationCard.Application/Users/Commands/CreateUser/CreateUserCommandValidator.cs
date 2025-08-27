using FluentValidation;
using VaccinationCard.Domain.Errors;

namespace VaccinationCard.Application.Users.Commands.CreateUser
{
    internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(UserErrors.EmailShouldNotBeEmpty)
                .EmailAddress().WithMessage(UserErrors.EmailShouldNotBeEmpty);

            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage(UserErrors.PassowordInvalidLenght)
                .NotEmpty().WithMessage(UserErrors.PasswordShouldNotBeEmpty);
        }
    }
}
