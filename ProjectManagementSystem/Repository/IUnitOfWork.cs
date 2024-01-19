using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProjectManagementSystem.Business
{
    public interface IUnitOfWork
    {
        MySqlConnection GetConnection();

        void Begin();

        void Commit();

        void RollBack();

        public IEnumerable<T> ExecuteQuery<T>(string query);

        public IEnumerable<T> ExecuteQuery<T>(string query, DynamicParameters dynamicParameters);

        IEnumerable<T> Query<T>(string query, object? param = null, IDbTransaction? transaction = null, int? commandTimeOut = null, CommandType? commandType = null);
    }
}
