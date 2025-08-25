using FluentValidation;

namespace VaccinationCard.Application.Persons.Commands.CreatePerson
{
    internal class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(80).WithMessage("Nome muito longo")
                .NotEmpty().WithMessage("O nome da pessoa é obrigatório")
                .MinimumLength(3).WithMessage("O nome da pessoa ter no mínimo 3 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório")
                .EmailAddress().WithMessage("Email deve ser válido");

            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Idade deve ser maior ou igual a 0");

            RuleFor(x => x.CPF)
                .NotEmpty()
                .WithMessage("CPF é obrigatório")
                .Length(11, 11)
                .WithMessage("Tamanho do CPF inválido");
        }
    }
}
