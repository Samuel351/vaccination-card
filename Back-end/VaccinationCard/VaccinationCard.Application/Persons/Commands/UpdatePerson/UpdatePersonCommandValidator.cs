using FluentValidation;
using VaccinationCard.Domain.Errors;

namespace VaccinationCard.Application.Persons.Commands.UpdatePerson
{
    internal class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(80).WithMessage(PersonErrors.NameIsToLong)
                .NotEmpty().WithMessage(PersonErrors.NameIsObligatory)
                .MinimumLength(3).WithMessage(PersonErrors.NameInvalidMinimumLength);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(PersonErrors.EmailIsRequired)
                .EmailAddress().WithMessage(PersonErrors.EmailIsInvalid);

            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(0)
                .WithMessage(PersonErrors.InvalidAge);

            RuleFor(x => x.CPF)
                .NotEmpty()
                .WithMessage(PersonErrors.CPFIsObligatory)
                .Length(11)
                .WithMessage(PersonErrors.InvalidCPFLength);
        }
    }
}
