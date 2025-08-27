using Microsoft.AspNetCore.Identity;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Infrastructure.Utils
{
    public static class PasswordHasherUtils
    {
        private static readonly PasswordHasher<User> _passwordHasher = new();

        public static string Hash(User user)
        {
            return _passwordHasher.HashPassword(user, user.Password);
        }

        public static bool Verify(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            return result == PasswordVerificationResult.Success;
        }
    }

}
