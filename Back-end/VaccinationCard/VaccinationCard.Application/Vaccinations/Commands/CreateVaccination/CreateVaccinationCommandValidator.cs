using FluentValidation;
using VaccinationCard.Domain.Errors;

namespace VaccinationCard.Application.Vaccinations.Commands.CreateVaccination
{
    internal class CreateVaccinationCommandValidator : AbstractValidator<CreateVaccinationCommand>
    {
        public CreateVaccinationCommandValidator()
        {
            RuleFor(x => x.DoseNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage(VaccinationErrors.InvalidDoseNumber);
        }
    }
}
