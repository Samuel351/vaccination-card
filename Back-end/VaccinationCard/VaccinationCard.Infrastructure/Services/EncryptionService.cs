using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces;
using VaccinationCard.Infrastructure.Utils;

namespace VaccinationCard.Infrastructure.Services
{
    internal class EncryptionService : IEncryptionService
    {
        public bool VerifyPassword(User user, string passwordToCompare)
        {
            return PasswordHasherUtils.Verify(user, passwordToCompare);
        }

        public string EncryptPassword(User user)
        {
            return PasswordHasherUtils.Hash(user);
        }
    }
}
