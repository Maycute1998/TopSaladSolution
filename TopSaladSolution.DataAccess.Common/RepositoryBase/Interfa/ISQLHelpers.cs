using System.Data;

namespace TopSaladSolution.DataAccess.Common.RepositoryBase.Interfa
{
    /// <summary title="Interface ISQLHelpers">
    /// Creator : Chung Thành Phước
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISQLHelpers
    {
        /// <summary>
        /// Creates a new SQL command.
        /// </summary>
        /// <param name="commandType">The type of command to create.</param>
        /// <returns>An ISQLHelpers object representing the new SQL command.</returns>
        ISQLHelpers CreateNewSqlCommand(CommandType commandType = CommandType.StoredProcedure);

        /// <summary>
        /// Adds a parameter to the SQL command.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An ISQLHelpers object representing the SQL command with the added parameter.</returns>
        ISQLHelpers AddParameter(string paramName, object value);

        /// <summary>
        /// Executes a SQL command and returns a list of specified data transfer objects (DTOs).
        /// </summary>
        /// <typeparam name="TDTO">The type of data transfer object.</typeparam>
        /// <param name="sProcName">The name of the stored procedure to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>A list of specified data transfer objects (DTOs).</returns>
        Task<List<TDTO>> ExecuteListReaderAsync<TDTO>(string sProcName, CommandType commandType = CommandType.StoredProcedure) where TDTO : new();

        /// <summary>
        /// Executes a SQL command and returns a list of specified data transfer objects (DTOs) along with total counts and total pages.
        /// </summary>
        /// <typeparam name="TDTO">The type of data transfer object.</typeparam>
        /// <param name="sProcName">The name of the stored procedure to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="TotalCounts">Output parameter for total counts.</param>
        /// <param name="TotalPages">Output parameter for total pages.</param>
        /// <returns>A list of specified data transfer objects (DTOs).</returns>
        List<TDTO> ExecuteListReader<TDTO>(string sProcName, CommandType commandType, out int TotalCounts, out double TotalPages) where TDTO : new();

        /// <summary>
        /// Executes a SQL command and returns the result as a DataTable.
        /// </summary>
        /// <param name="sProcName">The name of the stored procedure to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>A DataTable containing the result of the SQL command.</returns>
        DataTable ExecuteListReader(string sProcName, CommandType commandType);

        /// <summary>
        /// Executes a SQL command and returns the result as a DataTable along with total counts and total pages.
        /// </summary>
        /// <param name="sProcName">The name of the stored procedure to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="TotalCounts">Output parameter for total counts.</param>
        /// <param name="TotalPages">Output parameter for total pages.</param>
        /// <returns>A DataTable containing the result of the SQL command.</returns>
        DataTable ExecuteReader(string sProcName, CommandType commandType, out int TotalCounts, out double TotalPages);

        /// <summary>
        /// Executes a SQL command that does not return any result (e.g., INSERT, UPDATE, DELETE) and provides status and message as output parameters.
        /// </summary>
        /// <param name="sProcName">The name of the stored procedure to execute.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="Status">Output parameter for the status of the execution.</param>
        /// <param name="Msg">Output parameter for any message related to the execution.</param>
        void ExecuteNonQuery(string sProcName, CommandType commandType, out bool Status, out string Msg);

        /// <summary>
        /// Executes a SQL command with a specified data transfer object (DTO) and provides status and message as output parameters.
        /// </summary>
        /// <typeparam name="TDTO">The type of data transfer object.</typeparam>
        /// <param name="sProcName">The name of the stored procedure to execute.</param>
        /// <param name="tDTO">The data transfer object to be used in the execution.</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <param name="Status">Output parameter for the status of the execution.</param>
        /// <param name="Msg">Output parameter for any message related to the execution.</param>
        /// <param name="isDisableOutput">Flag to indicate whether to disable output.</param>
        void ExecuteNonQuery<TDTO>(string sProcName, TDTO tDTO, CommandType commandType, out bool Status, out string Msg, bool isDisableOutput = false);

        /// <summary>
        /// Asynchronously executes a SQL command that does not return any result (e.g., INSERT, UPDATE, DELETE) and provides status and message as output parameters.
        /// </summary>
        void ExecuteNonQueryAsync(string sProcName, CommandType commandType, out bool Status, out string Msg);
    }
}
