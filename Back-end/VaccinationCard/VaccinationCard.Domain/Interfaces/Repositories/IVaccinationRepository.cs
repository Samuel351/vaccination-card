using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Interfaces.Repositories
{
    public interface IVaccinationRepository : IBaseRepository<Vaccination>
    {
        Task<List<Vaccination>> GetVaccinations(Guid vaccineId, Guid personId);
    }
}
