using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Infrastructure.Data;

namespace VaccinationCard.Infrastructure.Repositories
{
    internal class PersonRepository(AppDbContext appDbContext) : BaseRepository<Person>(appDbContext), IPersonRepository
    {

        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<List<VaccinationRecord>> GetPersonVaccinationRecords(Guid PersonId)
        {
            var person = await _appDbContext.Persons
                .AsNoTracking()
                .Include(x => x.VaccinationRecords)
                .ThenInclude(x => x.Vaccine)
                .FirstOrDefaultAsync(x => x.EntityId == PersonId);

            if (person == null) return [];

            return person.VaccinationRecords;
        }
    }
}
