using Domain.Abstractions;

namespace VaccinationCard.Domain.Errors
{
    public static class PersonErrors
    {
        public static readonly Error NotFound = new(
            "Person.NotFound",
            "This person was not found");

        public static readonly Error NoContent = new(
            "Person.NoContent",
            "No person found");

        public static readonly Error EmailAlreadyExists = new(
            "Person.EmailAlreadyExists",
            "Email já cadastrado");

        public static readonly Error CPFAlreadyExists = new(
            "Person.CPFAlreadyExists",
            "CPF já cadastrado");
    }
}
