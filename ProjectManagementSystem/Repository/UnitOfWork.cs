using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace ProjectManagementSystem.Business
{
    public class UnitOfWork : IUnitOfWork
    {

        private MySqlConnection? connection;
        private readonly IConfiguration _configuration;
        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public MySqlConnection GetConnection()
        {
            connection = new MySqlConnection(GetConnectionString());
            connection.Open();
            return connection;
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("dbConnection");
        }

        public IEnumerable<T> Query<T>(string query, object? param = null, IDbTransaction? transaction = null, int? commandTimeOut = null, CommandType? commandType = null)
        {
            try
            {
                if (connection == null) connection = GetConnection();
                return connection.Query<T>(query, param, transaction, true, commandTimeOut, commandType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<T> ExecuteQuery<T>(string query)
        {
            return ExecuteQuery<T>(query, null);
        }

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
    }
}
