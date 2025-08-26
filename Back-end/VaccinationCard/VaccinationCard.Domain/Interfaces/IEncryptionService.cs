using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Interfaces
{
    public interface IEncryptionService
    {
        string EncryptPassword(User user);

        bool VerifyPassword(User user, string passwordToCompare);
    }
}
