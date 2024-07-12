using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IReadRepository<T> readRepository<T>() where T : class, IEntityBase, new();
        public IWriteRepository<T> writeRepository<T>() where T : class, IEntityBase, new();
        public Task<int> SaveChangeAsync();
        public int SaveChange();
    }
}
