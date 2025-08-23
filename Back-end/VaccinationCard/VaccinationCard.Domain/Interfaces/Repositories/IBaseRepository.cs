using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        Task<T?> GetByIdAsync(Guid id);

        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
