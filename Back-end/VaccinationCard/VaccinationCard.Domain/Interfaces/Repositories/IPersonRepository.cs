using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Domain.Interfaces.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<List<Vaccination>> GetPersonVaccinationRecords(Guid PersonId);

        Task<PaginatedResponse<Person>> GetAllPersonPaginated(int PageNumber, int PageSize, string? Query = null);
    }
}
