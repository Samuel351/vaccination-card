using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Infrastructure.Data;

namespace VaccinationCard.Infrastructure.Repositories
{
    internal class UserRepository(AppDbContext appDbContext) : BaseRepository<User>(appDbContext), IUserRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<bool> EmailExists(string email, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Users.AnyAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            return await _appDbContext.Users.SingleOrDefaultAsync(x => x.Email == email, cancellationToken = default);
        }
    }
}
