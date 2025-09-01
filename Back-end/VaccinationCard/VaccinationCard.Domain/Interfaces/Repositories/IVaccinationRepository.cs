using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Interfaces.Repositories
{
    public interface IVaccinationRepository : IBaseRepository<Vaccination>
    {
        Task<List<Vaccination>> GetVaccinations(Guid vaccineId, Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Verifies If a vaccine is being in use
        /// </summary>
        /// <param name="vaccineId"></param>
        /// <returns></returns>
        Task<bool> IsVaccineBeingUsed(Guid vaccineId, CancellationToken cancellationToken = default);
    }
}
