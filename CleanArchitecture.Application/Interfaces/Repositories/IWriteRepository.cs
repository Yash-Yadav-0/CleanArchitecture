using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> where T : class, IEntityBase, new()
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IList<T> entities);
        Task<T> UpdateAsync(int id, T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IList<T> Entities);
    }
}
