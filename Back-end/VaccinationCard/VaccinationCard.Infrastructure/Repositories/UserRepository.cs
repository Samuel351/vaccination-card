using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Infrastructure.Data;

namespace VaccinationCard.Infrastructure.Repositories
{
    internal class UserRepository(AppDbContext appDbContext) : BaseRepository<User>(appDbContext), IUserRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<bool> EmailExists(string email)
        {
            return await _appDbContext.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _appDbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
