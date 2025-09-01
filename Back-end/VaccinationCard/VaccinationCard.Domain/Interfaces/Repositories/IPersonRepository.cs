using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Domain.Interfaces.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<List<Vaccination>> GetPersonVaccinations(Guid PersonId, CancellationToken cancellationToken = default);

        Task<bool> CPFExists(string CPF, CancellationToken cancellationToken = default);

        Task<bool> EmailExists(string email, CancellationToken cancellationToken = default);

        Task<Person?> GetPersonByCPF(string CPF, CancellationToken cancellationToken = default);

        Task<Person?> GetPersonByEmail(string email, CancellationToken cancellationToken = default);

    }
}
