using Infrastructure.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure
{
    public class MultipleUserDbContext : IMultipleUserDbContext
    {
        private readonly string connectionString;
        private SqlConnection connection;

        public MultipleUserDbContext(string connectionString)
        {
            this.connectionString = connectionString;
            this.connection = new SqlConnection(connectionString);
        }

        public async Task OpenConnectionAsync()
        {
            if (connection.State == ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public async Task<SqlDataReader> ExecuteQueryAsync(string query)
        {
            await OpenConnectionAsync();

            SqlCommand command = new SqlCommand(query, connection);
            return await command.ExecuteReaderAsync();
        }

        public async Task<SqlDataReader> ExecuteQueryAsync(string query, params SqlParameter[] parameters)
        {
            await OpenConnectionAsync();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
        }

        public void Dispose()
        {
            CloseConnection();
            connection.Dispose();
        }
    }
}
