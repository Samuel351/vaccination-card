using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> EmailExists(string email, CancellationToken cancellationToken = default);
        Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken = default);
    }
}
