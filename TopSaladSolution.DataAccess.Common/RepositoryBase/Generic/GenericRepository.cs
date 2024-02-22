using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;
using TopSaladSolution.DataAccess.Common.RepositoryBase.Interfa;

namespace TopSaladSolution.DataAccess.Common.RepositoryBase.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;

        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public GenericRepository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }

        /// <summary>
        /// Asynchronously checks if any element of the sequence satisfies a condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a value that indicates whether any element in the source sequence passed the test in the specified predicate function.</returns>
        public async Task<bool> GetAnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsQueryable().AnyAsync(predicate);
        }

        /// <summary>
        /// Gets a list of elements of the sequence that satisfy a specified condition.
        /// </summary>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to sort the elements.</param>
        /// <param name="include">A function to include related data.</param>
        /// <param name="disableTracking">Indicates whether the entities should be tracked by the context.</param>
        /// <returns>A queryable list of elements.</returns>
        public IQueryable<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return orderBy != null ? orderBy(query).Select(selector) : query.Select(selector);
        }

        /// <summary>
        /// Gets a list of elements of the sequence that satisfy a specified condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to sort the elements.</param>
        /// <param name="include">A function to include related data.</param>
        /// <param name="disableTracking">Indicates whether the entities should be tracked by the context.</param>
        /// <returns>A queryable list of elements.</returns>
        public IQueryable<T> GetList(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return orderBy != null ? orderBy(query) : query;
        }

        /// <summary>
        /// Asynchronously gets the first element of the sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to sort the elements.</param>
        /// <param name="include">A function to include related data.</param>
        /// <param name="disableTracking">Indicates whether the entities should be tracked by the context.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the first element of the sequence that satisfies the specified condition, or a default value if no such element is found.</returns>
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).FirstOrDefaultAsync();
            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Asynchronously adds a single entity to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public async Task Add(T entity) => await _dbSet.AddAsync(entity);

        /// <summary>
        /// Adds multiple entities to the database.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns></returns>
        public Task Add(params T[] entities) => _dbSet.AddRangeAsync(entities);

        /// <summary>
        /// Adds a collection of entities to the database.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns></returns>
        public Task Add(IEnumerable<T> entities) => _dbSet.AddRangeAsync(entities);

        /// <summary>
        /// Updates a single entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// Updates multiple entities in the database.
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        public void Update(params T[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        /// <summary>
        /// Updates a collection of entities in the database.
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        public void Update(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        /// <summary>
        /// Deletes a single entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Deletes an entity from the database by its id.
        /// </summary>
        /// <param name="id">The id of the entity to delete.</param>
        public void Delete(object id)
        {
            var typeInfo = typeof(T).GetTypeInfo();
            var key = _dbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<T>();
                property.SetValue(entity, id);
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = _dbSet.Find(id);
                if (entity != null) Delete(entity);
            }
        }

        /// <summary>
        /// Deletes multiple entities from the database.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        public void Delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Deletes a collection of entities from the database.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
