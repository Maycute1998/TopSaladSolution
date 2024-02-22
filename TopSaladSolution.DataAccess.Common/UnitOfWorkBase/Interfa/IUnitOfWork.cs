using Microsoft.EntityFrameworkCore;
using TopSaladSolution.DataAccess.Common.RepositoryBase.Interfa;

namespace TopSaladSolution.DataAccess.Common.UnitOfWorkBase.Interfa
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        ISQLHelpers SQLHelper();
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}
