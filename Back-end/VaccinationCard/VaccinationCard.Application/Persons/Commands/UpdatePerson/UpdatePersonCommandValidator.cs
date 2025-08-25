using FluentValidation;

namespace VaccinationCard.Application.Persons.Commands.UpdatePerson
{
    internal class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
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
                .Length(11)
                .WithMessage("Tamanho do CPF inválido");
        }
    }
}
