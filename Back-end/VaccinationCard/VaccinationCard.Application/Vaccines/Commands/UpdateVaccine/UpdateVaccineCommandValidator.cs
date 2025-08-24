using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Application.Vaccines.Commands.UpdateVaccine
{
    internal class UpdateVaccineCommandValidator : AbstractValidator<UpdateVaccineCommand>
    {
        public UpdateVaccineCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Nome da vacina é obrigatório");

            RuleFor(x => x.RequiredDoses)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A vacina precisa pelo menos ter uma dose");
        }
    }
}
