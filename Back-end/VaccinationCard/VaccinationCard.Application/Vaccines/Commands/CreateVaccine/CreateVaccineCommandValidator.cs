using FluentValidation;
using VaccinationCard.Domain.Errors;

namespace VaccinationCard.Application.Vaccines.Commands.CreateVaccine
{
    internal class CreateVaccineCommandValidator : AbstractValidator<CreateVaccineCommand>
    {
        public CreateVaccineCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage(VaccineErrors.NameIsToLong)
                .NotEmpty().WithMessage(VaccineErrors.NameIsObligatory);

            RuleFor(x => x.RequiredDoses)
                .GreaterThanOrEqualTo(1)
                .WithMessage(VaccineErrors.InvalidRequiredDoses);
        }
    }
}
