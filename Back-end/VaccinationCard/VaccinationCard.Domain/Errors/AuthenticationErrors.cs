using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Domain.Errors
{
    public class AuthenticationErrors
    {
        public static readonly Error InvalidCredentials = new(
            "Authentication.NotFound",
            "Email ou senha inválida");

        public static readonly Error InvalidAuthorization = new(
            "Authentication.InvalidAuthorization",
            "Não autorizado");
    }
}
