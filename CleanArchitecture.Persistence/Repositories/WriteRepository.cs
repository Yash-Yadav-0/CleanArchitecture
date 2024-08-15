using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Persistence.Context;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityBase, new()
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _table;
        public WriteRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _table = dbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _table.AddAsync(entity);
        }
        public async Task AddRangeAsync(IList<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            if (entities.Count == 0) return; // No need to add an empty list
            await _table.AddRangeAsync(entities);
        }
        public async Task<T> UpdateAsync(int id, T entity)
        {
            //await Task.Run(() => table.Update(entity));
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var existingEntity = await _table.FindAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _table.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IList<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            if (entities.Count == 0) return; // No need to remove an empty list
            _table.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}
