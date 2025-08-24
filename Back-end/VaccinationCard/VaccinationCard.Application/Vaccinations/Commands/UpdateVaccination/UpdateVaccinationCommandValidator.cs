using FluentValidation;

namespace VaccinationCard.Application.Vaccinations.Commands.UpdateVaccination
{
    internal class UpdateVaccinationCommandValidator : AbstractValidator<UpdateVaccinationCommand>
    {
        public UpdateVaccinationCommandValidator()
        {
            RuleFor(x => x.DoseNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Dose da vacina deve ser maior ou igual a 1");
        }
    }
}
