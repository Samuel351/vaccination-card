using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Infrastructure.Data;

namespace VaccinationCard.Infrastructure.Repositories
{
    internal class VaccineRepository(AppDbContext appDbContext) : BaseRepository<Vaccine>(appDbContext), IVaccineRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<bool> NameExists(string VaccineName, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Vaccines.AnyAsync(x => x.Name == VaccineName, cancellationToken);
        }
    }
}
