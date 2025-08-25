using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Infrastructure.Data;

namespace VaccinationCard.Infrastructure.Repositories
{
    internal class PersonRepository(AppDbContext appDbContext) : BaseRepository<Person>(appDbContext), IPersonRepository
    {

        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<bool> CPFExists(string CPF)
        {
            return await _appDbContext.Persons.AnyAsync(x => x.CPF == CPF);   
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _appDbContext.Persons.AnyAsync(x => x.Email == email);
        }

        public async Task<Person?> GetPersonByCPF(string CPF)
        {
            return await _appDbContext.Persons.SingleOrDefaultAsync(x => x.CPF == CPF);
        }

        public async Task<Person?> GetPersonByEmail(string email)
        {
            return await _appDbContext.Persons.SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<Vaccination>> GetPersonVaccinations(Guid PersonId)
        {
            var person = await _appDbContext.Persons
                .AsNoTracking()
                .Include(x => x.Vaccinations)
                .ThenInclude(x => x.Vaccine)
                .SingleOrDefaultAsync(x => x.EntityId == PersonId);

            if (person == null) return [];

            return person.Vaccinations;
        }
    }
}
