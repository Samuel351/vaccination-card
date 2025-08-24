using FluentValidation;

namespace VaccinationCard.Application.Vaccinations.Commands.CreateVaccination
{
    internal class CreateVaccinationCommandValidator : AbstractValidator<CreateVaccinationCommand>
    {
        public CreateVaccinationCommandValidator()
        {
            RuleFor(x => x.DoseNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Dose da vacina deve ser maior ou igual a 1");
        }
    }
}
