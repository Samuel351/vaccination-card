using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Infrastructure.Data;

namespace VaccinationCard.Infrastructure.Repositories
{
    internal class BaseRepository<T>(AppDbContext appDbContext) : IBaseRepository<T> where T : EntityBase
    {

        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly DbSet<T> _dbSet = appDbContext.Set<T>();

        public async Task<T?> GetByIdAsync(Guid id) =>
            await _dbSet.SingleOrDefaultAsync(e => e.EntityId == id);

        public async Task AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _dbSet.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is not null)
            {
                _dbSet.Remove(entity);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}

