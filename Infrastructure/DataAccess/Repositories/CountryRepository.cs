using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Interfaces;
using Shared.Dtos.Country;
using System.Data.SqlClient;

namespace Infrastructure.DataAccess.Repositories
{
    public class CountryRepository : IRepository<Country, CountryResponse>
    {
        private readonly IMultipleUserDbContext _dbContext;

        public CountryRepository(
            IMultipleUserDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddAsync(Country entity)
        {
            string query = "INSERT INTO Countries (CountryName) VALUES (@CountryName)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CountryName", entity.CountryName),
            };

            await _dbContext.ExecuteQueryAsync(query, parameters);
        }

        public async Task<IEnumerable<CountryResponse>> GetAllAsync()
        {
            string query = "SELECT * FROM Countries";

            using (var reader = await _dbContext.ExecuteQueryAsync(query))
            {
                var countries = new List<CountryResponse>();
                while (await reader.ReadAsync())
                {
                    countries.Add(new CountryResponse
                    {
                        Id = (int)reader["Id"],
                        CountryName = reader["CountryName"].ToString(),
                    });
                }
                return countries;
            }
        }

        public Task AddMultipleAsync(IEnumerable<Country> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CountryResponse>> GetByAsync(string value)
        {
            throw new NotImplementedException();
        }
    }
}
