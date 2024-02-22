using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace TopSaladSolution.DataAccess.Common.RepositoryBase.Interfa
{
    /// <summary title="Interface IGenericRepository">
    /// Creator : Chung Thành Phước
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Check data has been exists
        /// Parameter is lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> GetAnyAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get single object
        /// Parameters is lambda expression,order by lambda expression,include lambda expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true);

        /// <summary>
        /// Get list
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        IQueryable<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true);

        /// <summary>
        /// Get list all
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="include"></param>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        IQueryable<T> GetList(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool disableTracking = true);

        /// <summary>
        /// Adds a single entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Add(T entity);

        /// <summary>
        /// Adds multiple entities.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Add(params T[] entities);

        /// <summary>
        /// Adds multiple entities.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Add(IEnumerable<T> entities);

        /// <summary>
        /// Deletes a single entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        void Delete(T entity);

        /// <summary>
        /// Deletes an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        void Delete(object id);

        /// <summary>
        /// Deletes multiple entities.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        void Delete(params T[] entities);

        /// <summary>
        /// Deletes multiple entities.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Updates a single entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        void Update(T entity);

        /// <summary>
        /// Updates multiple entities.
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        void Update(params T[] entities);

        /// <summary>
        /// Updates multiple entities.
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        void Update(IEnumerable<T> entities);
    }
}
