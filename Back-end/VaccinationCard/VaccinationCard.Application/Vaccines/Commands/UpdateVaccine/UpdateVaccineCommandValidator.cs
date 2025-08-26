using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.Domain.Errors;

namespace VaccinationCard.Application.Vaccines.Commands.UpdateVaccine
{
    internal class UpdateVaccineCommandValidator : AbstractValidator<UpdateVaccineCommand>
    {
        public UpdateVaccineCommandValidator()
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
