using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Interfaces.Repositories
{
    public interface IVaccineRepository : IBaseRepository<Vaccine>
    {
        /// <summary>
        /// Verify if the vaccine name is already registred
        /// </summary>
        /// <param name="VaccineName"></param>
        /// <returns></returns>
        Task<bool> NameExists(string VaccineName);
    }
}
