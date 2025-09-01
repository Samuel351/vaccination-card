using Microsoft.EntityFrameworkCore;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Infrastructure.Data;

namespace VaccinationCard.Infrastructure.Repositories
{
    internal class PersonRepository(AppDbContext appDbContext) : BaseRepository<Person>(appDbContext), IPersonRepository
    {

        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<bool> CPFExists(string CPF, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Persons.AnyAsync(x => x.CPF == CPF, cancellationToken);   
        }

        public async Task<bool> EmailExists(string email, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Persons.AnyAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<Person?> GetPersonByCPF(string CPF, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Persons.SingleOrDefaultAsync(x => x.CPF == CPF, cancellationToken);
        }

        public async Task<Person?> GetPersonByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Persons.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<List<Vaccination>> GetPersonVaccinations(Guid PersonId, CancellationToken cancellationToken = default)
        {
            var person = await _appDbContext.Persons
                .AsNoTracking()
                .Include(x => x.Vaccinations)
                .ThenInclude(x => x.Vaccine)
                .SingleOrDefaultAsync(x => x.EntityId == PersonId, cancellationToken);

            var person2 = await _appDbContext.Vaccination
                .Where(x => x.EntityId == PersonId)
                .GroupBy(x => new { x.VaccineId, x.Vaccine.Name })
                .Select(r => new VaccinationCardResponse(r.Key.VaccineId, r.Key.Name, r.Select(c => new VaccineDoseResponse(c.EntityId, c.ApplicationDate, c.DoseNumber)).ToList())).ToListAsync(cancellationToken);


            if (person == null) return [];

            return person.Vaccinations;
        }
    }
}
