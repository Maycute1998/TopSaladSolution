using Microsoft.EntityFrameworkCore;
using TopSaladSolution.DataAccess.Common.RepositoryBase.Generic;
using TopSaladSolution.DataAccess.Common.RepositoryBase.Interfa;
using TopSaladSolution.DataAccess.Common.UnitOfWorkBase.Interfa;

namespace TopSaladSolution.DataAccess.Common.UnitOfWorkBase.Uow
{
    public class UnitOfWork<TContext> : IGenericRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext
    {
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new GenericRepository<TEntity>(Context);
            return (IGenericRepository<TEntity>)_repositories[type];
        }


        public TContext Context { get; }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }

        public ISQLHelpers SQLHelper()
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();
            var type = typeof(ISQLHelpers);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new EntityHelpers.EntityHelpers(Context);
            return (ISQLHelpers)_repositories[type];
        }
    }
}
