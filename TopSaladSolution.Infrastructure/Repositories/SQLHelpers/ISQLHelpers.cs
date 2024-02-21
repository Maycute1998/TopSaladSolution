using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.Common;

namespace TopSaladSolution.Infrastructure.Repositories.SQLHelpers
{
    public interface ISQLHelpers
    {
        ISQLHelpers CreateNewSqlCommand(CommandType commandType = CommandType.StoredProcedure);
        ISQLHelpers AddParameter(string paramName, object value);
        Task<List<TDTO>> ExecuteReaderAsync<TDTO>(string sProcName, CommandType commandType = CommandType.StoredProcedure) where TDTO : new();
        Task<List<TDTO>> ExecuteReaderAsync<TDTO>(string sProcName, List<SqlParameter> externalParameter, CommandType commandType = CommandType.StoredProcedure) where TDTO : new();
        List<TDTO> ExecuteReader<TDTO>(string sProcName, CommandType commandType, out int TotalCounts, out double TotalPages) where TDTO : new();
        DataTable ExecuteReader(string sProcName, CommandType commandType);
        DataTable ExecuteReader(string sProcName, CommandType commandType, out int TotalCounts, out double TotalPages);
        void ExecuteNonQuery(string sProcName, CommandType commandType, out bool Status, out string Msg);
        void ExecuteNonQuery<TDTO>(string sProcName, TDTO tDTO, CommandType commandType, out bool Status, out string Msg, bool isDisableOutput = false);
        void ExecuteNonQueryAsync(string sProcName, CommandType commandType, out bool Status, out string Msg);
        DbCommand GetDbCommand { get; }
    }
}
