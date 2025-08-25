using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Application.Vaccines.Commands.CreateVaccine
{
    internal class CreateVaccineCommandValidator : AbstractValidator<CreateVaccineCommand>
    {
        public CreateVaccineCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage("Nome muito longo")
                .NotEmpty().WithMessage("Nome da vacina é obrigatório");

            RuleFor(x => x.RequiredDoses)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A vacina precisa pelo menos ter uma dose");
        }
    }
}
