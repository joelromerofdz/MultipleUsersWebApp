using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Interfaces
{
    public interface IMultipleUserDbContext : IDisposable
    {
        Task OpenConnectionAsync();
        void CloseConnection();

        Task<SqlDataReader> ExecuteQueryAsync(string query);
        Task<SqlDataReader> ExecuteQueryAsync(string query, params SqlParameter[] parameters);
    }
}
