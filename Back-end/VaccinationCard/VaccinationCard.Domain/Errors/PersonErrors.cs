using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Domain.Errors
{
    public static class PersonErrors
    {
        public static readonly Error NotFound = new(
            "Person.NotFound",
            "Pessoa não encontrada");

        public static readonly Error NoContent = new(
            "Person.NoContent",
            "Sem pessoas encontradas");

        public static readonly Error EmailAlreadyExists = new(
            "Person.EmailAlreadyExists",
            "Email já cadastrado");

        public static readonly Error CPFAlreadyExists = new(
            "Person.CPFAlreadyExists",
            "CPF já cadastrado");

        public static readonly Error EmailIsRequired = new(
            "Person.EmailIsRequired",
            "Email é obrigatório");

        public static readonly Error EmailIsInvalid = new(
            "Person.EmailIsInvalid",
            "Email deve ser válido");

        public static readonly Error NameIsToLong = new(
            "Person.NameIsToLong",
            "Nome muito longo");

        public static readonly Error NameIsObligatory = new(
            "Person.NameIsObligatory",
            "Nome da pessoa é obrigatório");

        public static readonly Error NameInvalidMinimumLength = new(
            "Person.NameMinimumLength",
            "O nome da pessoa ter no mínimo 3 caracteres.");

        public static readonly Error InvalidAge = new(
            "Person.InvalidAge",
            "Idade deve ser maior ou igual a 0");

        public static readonly Error CPFIsObligatory = new(
            "Person.CPFIsObligatory",
            "CPF é obrigatório");

        public static readonly Error InvalidCPFLength = new(
            "Person.InvalidCPFLength",
            "Tamanho do CPF inválido");
      
    }
}
