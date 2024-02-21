using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TopSaladSolution.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           bool disableTracking = true);

        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task Update(T entity);
        Task Remove(T entity);
        Task Delete(T entity);

        #region [Execute-Store-Procedure]
        /// <summary>
        /// ExecuteListReaderAsync
        /// Example :
        /// <para>var result = await ExecuteListReaderAsync("store Procedure Name)</para>
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <returns>The List<Entity></returns>
        Task<List<T>> ExecuteListReaderAsync(string storeProcedureName);

        /// <summary>
        /// ExecuteListReaderAsync
        /// Example :
        /// <para>var param = new SqlParameter("@ProductId", ProductId)</para>
        /// <para>var result = await ExecuteListReaderAsync("Sp_GetProductById @ProductId", param)</para>
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameter"></param>
        /// <returns>The List<Entity></returns>
        Task<List<T>> ExecuteListReaderAsync(string storeProcedureName, SqlParameter parameter);

        /// <summary>
        /// ExecuteSingleReaderAsync
        /// Example : var result = await ExecuteSingleReaderAsync("store Procedure Name) 
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <returns>Object</returns>
        Task<T> ExecuteSingleReaderAsync(string storeProcedureName);

        /// <summary>
        /// ExecuteSingleReaderAsync
        /// Example :
        /// <para>var param = new SqlParameter("@ProductId", ProductId)</para>
        /// <para>var result = await ExecuteSingleReaderAsync("Sp_GetProductById @ProductId", param)</para>
        /// </summary>
        /// <param name="storeProcedureName"></param>
        /// <param name="parameter"></param>
        /// <returns>Single Object</returns>
        Task<T> ExecuteSingleReaderAsync(string storeProcedureName, SqlParameter parameter);

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
        Task<int> ExecuteNonQueryAsync(string storeProcedureName, List<SqlParameter> parameter);
        #endregion
    }
}
