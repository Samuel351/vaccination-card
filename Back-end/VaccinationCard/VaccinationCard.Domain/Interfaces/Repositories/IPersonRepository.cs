using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Interfaces.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<List<Vaccination>> GetPersonVaccinations(Guid PersonId);

        Task<bool> CPFExists(string CPF);

        Task<bool> EmailExists(string email);

        Task<Person?> GetPersonByCPF(string CPF);

        Task<Person?> GetPersonByEmail(string email);

    }
}
