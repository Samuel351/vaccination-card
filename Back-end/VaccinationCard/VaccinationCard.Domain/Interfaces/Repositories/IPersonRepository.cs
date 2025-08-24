using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Domain.Interfaces.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<List<Vaccination>> GetPersonVaccinations(Guid PersonId);

        Task<PaginatedResponse<Person>> GetAllPersonPaginated(int PageNumber, int PageSize, string? Query = null);

        Task<bool> CPFExists(string CPF);

        Task<bool> EmailExists(string email);
    }
}
