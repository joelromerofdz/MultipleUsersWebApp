using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.Users;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IRepository<User, UserResponse>, ILoginRepository
    {
        private readonly IMultipleUserDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly string _connectionString;

        public UserRepository(
            IMultipleUserDbContext dbContext,
            IPasswordHasher passwordHasher,
            IConfiguration configuration)
        {
            this._dbContext = dbContext;
            this._passwordHasher = passwordHasher;
            this._connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }

        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            using (var reader = await _dbContext.ExecuteQueryAsync("SELECT U.Id, U.FirstName, U.LastName, U.Email, C.CountryName, P.ProvinceName, CT.CityName " +
                "FROM [User] AS U " +
                "INNER JOIN Countries AS C ON (U.CountryId = C.Id) " +
                "INNER JOIN Provinces AS P ON (U.ProvinceId = P.Id) " +
                "INNER JOIN Cities AS CT ON (U.CityId = CT.Id)"))
            {
                var users = new List<UserResponse>();
                while (await reader.ReadAsync())
                {
                    users.Add(new UserResponse
                    {
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        //PasswordUser = reader["PasswordUser"].ToString(),
                        CountryName = reader["CountryName"].ToString(),
                        ProvinceName = reader["ProvinceName"].ToString(),
                        CityName = reader["CityName"].ToString()
                    });
                }

                return users;
            }
        }

        public async Task AddMultipleAsync(IEnumerable<User> entities)
        {
            if (entities == null || !entities.Any())
            {
                return;
            }

            DataTable table = new DataTable();
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("LastName", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("PasswordUser", typeof(string));
            table.Columns.Add("CountryId", typeof(int));
            table.Columns.Add("ProvinceId", typeof(int));
            table.Columns.Add("CityId", typeof(int));

            foreach (var entity in entities)
            {
                string hashedPassword = _passwordHasher.HashPassword(entity.PasswordUser);
                table.Rows.Add(entity.FirstName, entity.LastName, entity.Email, hashedPassword, entity.CountryId, entity.ProvinceId, entity.CityId);
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(_connectionString))
            {
                bulkCopy.DestinationTableName = "[User]";

                foreach (DataColumn column in table.Columns)
                {
                    bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }

                await bulkCopy.WriteToServerAsync(table);
            }
        }

        public async Task<IEnumerable<UserResponse>> GetByAsync(string country)
        {
            const string query =
                "SELECT U.Id, U.FirstName, U.LastName, U.Email, C.CountryName, P.ProvinceName, CT.CityName " +
                "FROM [User] AS U " +
                "INNER JOIN Countries AS C ON (U.CountryId = C.Id) " +
                "INNER JOIN Provinces AS P ON (U.ProvinceId = P.Id) " +
                "INNER JOIN Cities AS CT ON (U.CityId = CT.Id) " +
                "WHERE C.CountryName LIKE '%' + @Country + '%'";

            using (var reader = await _dbContext.ExecuteQueryAsync(query, new SqlParameter("@Country", country)))
            {
                var users = new List<UserResponse>();
                while (await reader.ReadAsync())
                {
                    users.Add(new UserResponse
                    {
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        CountryName = reader["CountryName"].ToString(),
                        ProvinceName = reader["ProvinceName"].ToString(),
                        CityName = reader["CityName"].ToString()
                    });
                }

                return users;
            }
        }

        public async Task<UserResponse?> GetByEmailAsync(string email, string password)
        {
            string hashedPassword = _passwordHasher.HashPassword(password);

            const string query =
                "SELECT U.Id, U.FirstName, U.LastName, U.Email, C.CountryName, P.ProvinceName, CT.CityName " +
                "FROM [User] AS U " +
                "WHERE U.Email = @Email AND U.PasswordUser = @Password";

            using (var reader = await _dbContext.ExecuteQueryAsync(query,
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", hashedPassword)))
            {
                UserResponse userResponse = new UserResponse();
                if (await reader.ReadAsync())
                {
                    userResponse.FirstName = reader["FirstName"].ToString();
                    userResponse.LastName = reader["LastName"].ToString();
                    userResponse.Email = reader["Email"].ToString();
                    userResponse.CountryName = reader["CountryName"].ToString();
                    userResponse.ProvinceName = reader["ProvinceName"].ToString();
                    userResponse.CityName = reader["CityName"].ToString();
                }

                return null;
            }
        }


        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
