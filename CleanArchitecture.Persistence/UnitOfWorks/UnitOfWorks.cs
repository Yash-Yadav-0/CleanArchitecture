using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.Repositories;

namespace CleanArchitecture.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();
        public int SaveChange() => dbContext.SaveChanges();
        public async Task<int> SaveChangeAsync() => await dbContext.SaveChangesAsync();
        IReadRepository<T> IUnitOfWork.readRepository<T>() => new ReadRepository<T>(dbContext);
        IWriteRepository<T> IUnitOfWork.writeRepository<T>() => new WriteRepository<T>(dbContext);
    }
}
