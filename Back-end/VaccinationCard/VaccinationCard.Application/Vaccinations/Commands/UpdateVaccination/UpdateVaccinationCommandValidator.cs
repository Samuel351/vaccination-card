using FluentValidation;
using VaccinationCard.Domain.Errors;

namespace VaccinationCard.Application.Vaccinations.Commands.UpdateVaccination
{
    internal class UpdateVaccinationCommandValidator : AbstractValidator<UpdateVaccinationCommand>
    {
        public UpdateVaccinationCommandValidator()
        {
            RuleFor(x => x.DoseNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage(VaccinationErrors.InvalidDoseNumber);
        }
    }
}
