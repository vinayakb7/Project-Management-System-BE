using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace ProjectManagementSystem.Business
{
    public class UnitOfWork : IUnitOfWork
    {

        private MySqlConnection connection;
        private readonly IConfiguration _configuration;
        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
            this.connection = GetConnection();
        }

        // <summary>
        /// Execute Query with dynamic parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteQuery<T>(string query, DynamicParameters? dynamicParameters)
        {
            IEnumerable<T> model;
            using (var connection = GetConnection())
            {
                model = Query<T>(query, dynamicParameters, null, commandType: CommandType.Text);
                connection.Close();
            }

            return model;
        }

        /// <summary>
        /// Execute Query without dynamic parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteQuery<T>(string query)
        {
            return ExecuteQuery<T>(query, null);
        }

        /// <summary>
        /// Executes Query of any type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeOut"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string query, object? param = null, IDbTransaction? transaction = null, int? commandTimeOut = null, CommandType? commandType = null)
        {
            try
            {
                return connection.Query<T>(query, param, transaction, true, commandTimeOut, commandType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns MySQL connection.
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetConnection()
        {
            connection = new MySqlConnection(_configuration.GetConnectionString("dbConnection"));
            connection.Open();
            return connection;
        }
    }
}
