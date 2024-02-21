using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace TopSaladSolution.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet!.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null)
            {
                throw new ArgumentNullException("Id null");
            }

            return result;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).FirstOrDefaultAsync();
            return await query.FirstOrDefaultAsync();
        }

        public async Task Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entity");
            }
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        #region[Execute-Store-Procedure]
        /// <summary>
        /// ExecuteListReaderAsync
        /// Example :
        /// <para>var result = await ExecuteListReaderAsync("store Procedure Name)</para>
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <returns>The List<Entity></returns>
        public async Task<List<T>> ExecuteListReaderAsync(string storeProcedureName)
        {
            var result = await _dbSet.FromSqlRaw(storeProcedureName).ToListAsync();
            return result;
        }

        /// <summary>
        /// ExecuteListReaderAsync return the list data
        /// Example :
        /// <para>var param = new SqlParameter("@ProductId", ProductId)</para>
        /// <para>var result = await ExecuteListReaderAsync("Sp_GetProductById @ProductId", param)</para>
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public Task<List<T>> ExecuteListReaderAsync(string storeProcedureName, SqlParameter parameter)
        {
            var result = _dbSet.FromSqlRaw($@"exec {storeProcedureName}", parameter).ToListAsync();
            return result;
        }

        /// <summary>
        /// ExecuteSingleReaderAsync return the Single Object
        /// Example : var result = await ExecuteSingleReaderAsync("store Procedure Name) 
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <returns></returns>
        public async Task<T> ExecuteSingleReaderAsync(string storeProcedureName)
        {
            var result = await _dbSet.FromSqlRaw(storeProcedureName).FirstOrDefaultAsync();
            return result;
        }

        /// <summary>
        /// ExecuteSingleReaderAsync return the Single Object
        /// Example :
        /// <para>var param = new SqlParameter("@ProductId", ProductId)</para>
        /// <para>var result = await ExecuteSingleReaderAsync("Sp_GetProductById @ProductId", param)</para>
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<T> ExecuteSingleReaderAsync(string storeProcedureName, SqlParameter parameter)
        {
            var result = await _dbSet.FromSqlRaw($@"exec {storeProcedureName}", parameter).FirstOrDefaultAsync();
            return result;
        }

        /// <summary>
        /// ExecuteNonQueryAsync execute query for Insert,Update or Delete.Return the number
        /// Example:
        /// <para>var parameter = new List<SqlParameter>()</para>
        /// <para> parameter.Add(new SqlParameter("@ProductName", product.ProductName))</para>
        /// <para>var result = await ExecuteListReaderAsync("Sp_GetProductById @ProductId", parameter)</para>
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<int> ExecuteNonQueryAsync(string storeProcedureName, List<SqlParameter> parameter)
        {
            var result = await _dbContext.Database.ExecuteSqlRawAsync($@"exec {storeProcedureName}", parameter.ToArray());
            return result;
        }
        #endregion
    }
}
