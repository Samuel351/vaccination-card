using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Domain.Shared;
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

        public async Task<PaginatedResponse<Person>> GetAllPersonPaginated(int pageNumber, int pageSize, string? Query = null)
        {
            var query = _appDbContext.Persons.AsQueryable();

            // Busca case-insensitive com ILIKE (PostgreSQL)
            if (!string.IsNullOrWhiteSpace(Query))
            {
                query = query.Where(u => EF.Functions.Like(u.Name, $"%{Query}%") || EF.Functions.Like(u.CPF, $"%{Query}%"));
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResponse<Person>(items, totalItems, pageNumber, pageSize);
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
