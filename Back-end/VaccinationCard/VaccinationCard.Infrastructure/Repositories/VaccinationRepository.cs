using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Infrastructure.Data;

namespace VaccinationCard.Infrastructure.Repositories
{
    internal class VaccinationRepository(AppDbContext appDbContext) : BaseRepository<Vaccination>(appDbContext), IVaccinationRepository
    {

        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<List<Vaccination>> GetVaccinations(Guid vaccineId, Guid personId, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Vaccination.Where(x => x.VaccineId == vaccineId && x.PersonId == personId).ToListAsync(cancellationToken);
        }

        public async Task<bool> IsVaccineBeingUsed(Guid vaccineId, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Vaccination.AnyAsync(x => x.VaccineId == vaccineId, cancellationToken);
        }
    }
}
