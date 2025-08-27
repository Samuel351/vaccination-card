using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Domain.Errors
{
    public static class UserErrors
    {
        public static readonly Error UserNotFound = new(
            "User.UserNotFound",
            "Usuário não encontrado");

        public static readonly Error EmailAlreadyRegistred = new(
            "User.EmailAlreadyRegistred",
            "Email já está registrado");

        public static readonly Error EmailIsInvalid = new(
            "User.EmailIsInvalid",
            "Email é inválido");

        public static readonly Error EmailShouldNotBeEmpty = new(
            "User.EmailShouldNotBeEmpty",
            "Email é inválido");

        public static readonly Error PasswordShouldNotBeEmpty = new(
            "User.PasswordShouldNotBeEmpty",
            "Email é inválido");

        public static readonly Error PassowordInvalidLenght = new(
            "User.PassowordInvalidLenght",
            "Tamanho da senha é inválido, tamanho esperado é de 8 digitos");


    }
}
