using Domain.Abstractions;

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
    }
}
