using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;
using System.Reflection;
using TopSaladSolution.DataAccess.Common.RepositoryBase.Interfa;
using TopSaladSolution.DataAccess.Common.DataReaderExtension;

namespace TopSaladSolution.DataAccess.Common.EntityHelpers
{
    /// <summary title="Common EntityHelpers Using Stored Procedure">
    /// Creator : Chung Thanh Phuoc
    /// Desc    : EntityHelpers Help interaction with stored procedure
    /// </summary>
    public class EntityHelpers : ISQLHelpers
    {
        #region[Define]
        DbCommand cmd;
        #endregion

        #region[Constructor]
        protected readonly DbContext _dbContext;
        public EntityHelpers(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
        }
        #endregion

        #region[Execute-Stored-Procedure]
        /// <summary title="Create Command connect with database">
        /// CreateNewSqlCommand Create Command connect with database
        /// </summary>
        /// <param name="commandType"></param>
        public ISQLHelpers CreateNewSqlCommand(CommandType commandType = CommandType.StoredProcedure)
        {
            cmd = _dbContext.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = commandType;
            cmd.Connection = _dbContext.Database.GetDbConnection();
            return this;
        }

        /// <summary title="Add new Parameter">
        /// AddParameter add new Parameter
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ISQLHelpers AddParameter(string paramName, object value)
        {
            var parameter = new SqlParameter
            {
                ParameterName = "@" + paramName,
                Value = value
            };
            cmd.Parameters.Add(parameter);
            return this;
        }

        /// <summary title="Reader Data from database and mapping to properties">
        /// ExecuteReader Reader Data from database and mapping to properties
        /// </summary>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="sProcName"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<List<TDTO>> ExecuteListReaderAsync<TDTO>(string sProcName, CommandType commandType = CommandType.StoredProcedure) where TDTO : new()
        {
            cmd.CommandText = sProcName;
            cmd.CommandType = commandType;
            cmd.Connection = _dbContext.Database.GetDbConnection();
            _dbContext.Database.OpenConnection();
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                return reader.MapToList<TDTO>();
            };
        }
        /// <summary title="Reader Data from database and mapping to properties and pagging">
        /// ExecuteReader Reader Data from database and mapping to properties and pagging
        /// </summary>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="sProcName"></param>
        /// <param name="commandType"></param>
        /// <param name="TotalCounts"></param>
        /// <param name="TotalPages"></param>
        /// <returns></returns>
        public List<TDTO> ExecuteListReader<TDTO>(string sProcName, CommandType commandType, out int TotalCounts, out double TotalPages) where TDTO : new()
        {
            cmd.CommandText = sProcName;
            cmd.CommandType = commandType;
            cmd.Connection = _dbContext.Database.GetDbConnection();
            _dbContext.Database.OpenConnection();
            cmd.Parameters.Add(new SqlParameter
            {
                DbType = DbType.Int32,
                Size = 255,
                ParameterName = "@TotalCounts",
                Direction = ParameterDirection.Output
            });
            cmd.Parameters.Add(new SqlParameter
            {
                DbType = DbType.Double,
                Size = 255,
                ParameterName = "@TotalPages",
                Direction = ParameterDirection.Output
            });
            List<TDTO> list;
            using (var reader = cmd.ExecuteReader())
            {
                list = reader.MapToList<TDTO>();
            };
            TotalCounts = (int)cmd.Parameters["@TotalCounts"].Value;
            TotalPages = (double)cmd.Parameters["@TotalPages"].Value;
            return list;
        }
        /// <summary>
        /// Execute Reader with datatable
        /// </summary>
        /// <param name="sProcName"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public DataTable ExecuteListReader(string sProcName, CommandType commandType)
        {
            var dataTable = new DataTable();
            cmd.CommandText = sProcName;
            cmd.CommandType = commandType;
            cmd.Connection = _dbContext.Database.GetDbConnection();
            _dbContext.Database.OpenConnection();
            using (var adapter = new SqlDataAdapter((SqlCommand)cmd))
            {
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
        /// <summary>
        /// Execute Reader with datatable and output params
        /// </summary>
        /// <param name="sProcName"></param>
        /// <param name="commandType"></param>
        /// <param name="TotalCounts"></param>
        /// <param name="TotalPages"></param>
        /// <returns></returns>
        public DataTable ExecuteReader(string sProcName, CommandType commandType, out int TotalCounts, out double TotalPages)
        {
            var dataTable = new DataTable();
            cmd.CommandText = sProcName;
            cmd.CommandType = commandType;
            cmd.Connection = _dbContext.Database.GetDbConnection();
            _dbContext.Database.OpenConnection();
            cmd.Parameters.Add(new SqlParameter
            {
                DbType = DbType.Int32,
                Size = 255,
                ParameterName = "@TotalCounts",
                Direction = ParameterDirection.Output
            });
            cmd.Parameters.Add(new SqlParameter
            {
                DbType = DbType.Double,
                Size = 255,
                ParameterName = "@TotalPages",
                Direction = ParameterDirection.Output
            });
            using (var adapter = new SqlDataAdapter((SqlCommand)cmd))
            {
                adapter.Fill(dataTable);
            }
            TotalCounts = (int)cmd.Parameters["@TotalCounts"].Value;
            TotalPages = (double)cmd.Parameters["@TotalPages"].Value;
            return dataTable;
        }

        /// <summary title="Execute insert/update/delete to database">
        /// ExecuteNonQuery Execute insert/update/delete to database with custom parameters
        /// </summary>
        /// <param name="sProcName"></param>
        /// <param name="commandType"></param>
        /// <param name="Status"></param>
        /// <param name="Msg"></param>
        public void ExecuteNonQuery(string sProcName, CommandType commandType, out bool Status, out string Msg)
        {
            Status = false;
            Msg = string.Empty;
            try
            {
                cmd.CommandText = sProcName;
                cmd.CommandType = commandType;
                cmd.Connection = _dbContext.Database.GetDbConnection();
                _dbContext.Database.OpenConnection();

                cmd.Parameters.Add(new SqlParameter
                {
                    DbType = DbType.String,
                    Size = 255,
                    ParameterName = "@MSG",
                    Direction = ParameterDirection.Output
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    DbType = DbType.Boolean,
                    Size = 255,
                    ParameterName = "@STATUS",
                    Direction = ParameterDirection.Output
                });
                cmd.ExecuteNonQuery();
                Msg = cmd.Parameters["@MSG"].Value.ToString();
                Status = (bool)cmd.Parameters["@STATUS"].Value;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary title="Execute insert/update/delete to database">
        /// ExecuteNonQuery Execute insert/update/delete to database with automapper properties 
        /// </summary>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="sProcName"></param>
        /// <param name="tDTO"></param>
        /// <param name="commandType"></param>
        /// <param name="Status"></param>
        /// <param name="Msg"></param>
        /// <param name="isDisableOutput"></param>
        public void ExecuteNonQuery<TDTO>(string sProcName, TDTO tDTO, CommandType commandType, out bool Status, out string Msg, bool isDisableOutput = false)
        {
            Status = false;
            Msg = string.Empty;
            var entity = typeof(TDTO);
            var propDict = new Dictionary<string, PropertyInfo>();
            var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);

            try
            {
                cmd.CommandText = sProcName;
                cmd.CommandType = commandType;
                cmd.Connection = _dbContext.Database.GetDbConnection();
                _dbContext.Database.OpenConnection();

                foreach (var p in GetListParam(sProcName))
                {
                    if (propDict.ContainsKey(p.ParameterName))
                    {
                        var info = propDict[p.ParameterName];
                        if ((info != null) && info.CanWrite)
                        {
                            var val = info.GetValue(tDTO);
                            var parameter = new SqlParameter
                            {
                                ParameterName = string.Concat("@", p.ParameterName),
                                Value = (val == DBNull.Value) ? null : val
                            };
                            cmd.Parameters.Add(parameter);
                        }
                    }
                }
                if (isDisableOutput)
                {
                    cmd.ExecuteNonQuery();
                    Msg = "No output";
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.String,
                        Size = 255,
                        ParameterName = "@MSG",
                        Direction = ParameterDirection.Output
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.Boolean,
                        Size = 255,
                        ParameterName = "@STATUS",
                        Direction = ParameterDirection.Output
                    });
                    cmd.ExecuteNonQuery();
                    Msg = cmd.Parameters["@MSG"].Value.ToString();
                    Status = (bool)cmd.Parameters["@STATUS"].Value;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary title="Execute async insert/update/delete to database">
        /// ExecuteNonQueryAsync Execute async insert/update/delete to database
        /// </summary>
        /// <param name="sProcName"></param>
        /// <param name="commandType"></param>
        /// <param name="Status"></param>
        /// <param name="Msg"></param>
        public void ExecuteNonQueryAsync(string sProcName, CommandType commandType, out bool Status, out string Msg)
        {
            Status = false;
            Msg = string.Empty;
            try
            {
                cmd.CommandText = sProcName;
                cmd.CommandType = commandType;
                cmd.Connection = _dbContext.Database.GetDbConnection();
                _dbContext.Database.OpenConnection();

                cmd.Parameters.Add(new SqlParameter
                {
                    DbType = DbType.String,
                    Size = 255,
                    ParameterName = "@MSG",
                    Direction = ParameterDirection.Output
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    DbType = DbType.Boolean,
                    Size = 255,
                    ParameterName = "@STATUS",
                    Direction = ParameterDirection.Output
                });
                cmd.ExecuteNonQueryAsync();
                Msg = cmd.Parameters["@MSG"].Value.ToString();
                Status = (bool)cmd.Parameters["@STATUS"].Value;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region[Helper]
        private List<SqlParameter> GetListParam(string sProcName)
        {
            DbCommand _cmdHelper;
            var ListParams = new List<SqlParameter>();
            _cmdHelper = _dbContext.Database.GetDbConnection().CreateCommand();
            _cmdHelper.CommandText = sProcName;
            _cmdHelper.CommandType = CommandType.StoredProcedure;
            _cmdHelper.Connection = _dbContext.Database.GetDbConnection();
            _dbContext.Database.OpenConnection();

            SqlCommandBuilder.DeriveParameters((SqlCommand)_cmdHelper);
            foreach (SqlParameter p in _cmdHelper.Parameters)
            {
                ListParams.Add(new SqlParameter
                {
                    ParameterName = p.ParameterName.Replace("@", string.Empty).ToUpper(),
                    DbType = p.DbType,
                    SqlDbType = p.SqlDbType,
                    IsNullable = p.IsNullable
                });
            }

            return ListParams
                    .AsQueryable()
                    .Where(x => !x.ParameterName.Equals("RETURN_VALUE", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.ParameterName)
                    .ToList();
        }
        #endregion
    }
}
