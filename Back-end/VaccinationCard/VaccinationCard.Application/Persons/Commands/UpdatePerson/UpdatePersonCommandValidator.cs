using FluentValidation;
using VaccinationCard.Application.Persons.Commands.CreatePerson;

namespace VaccinationCard.Application.Persons.Commands.UpdatePerson
{
    internal class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome da pessoa é obrigatório")
            .MinimumLength(3).WithMessage("O nome da pessoa ter no mínimo 3 caracteres.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email deve ser válido");
        }
    }
}
