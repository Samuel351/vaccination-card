using FluentValidation;

namespace VaccinationCard.Application.Persons.Commands.CreatePerson
{
    internal class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da pessoa é obrigatório")
                .MinimumLength(3).WithMessage("O nome da pessoa ter no mínimo 3 caracteres.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email deve ser válido");
        }
    }
}
