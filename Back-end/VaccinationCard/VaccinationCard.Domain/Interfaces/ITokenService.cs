using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateTokenForUser(User user);
    }
}
