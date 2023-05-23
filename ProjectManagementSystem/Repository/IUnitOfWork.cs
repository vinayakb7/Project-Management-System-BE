using MySql.Data.MySqlClient;
using System.Data;

namespace ProjectManagementSystem.Business
{
    public interface IUnitOfWork
    {
        MySqlConnection GetConnection();

        IEnumerable<T> Query<T>(string query, object? param = null, IDbTransaction? transaction = null, int? commandTimeOut = null, CommandType? commandType = null);
    }
}
