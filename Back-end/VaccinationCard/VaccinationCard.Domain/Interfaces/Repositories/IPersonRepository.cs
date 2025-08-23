using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Interfaces.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<List<Vaccination>> GetPersonVaccinationRecords(Guid PersonId);
    }
}
