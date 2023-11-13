using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Interfaces;
using Shared.Dtos.City;
using System.Data.SqlClient;

namespace Infrastructure.DataAccess.Repositories
{
    public class CityRepository : IRepository<City, CityResponse>
    {
        private readonly IMultipleUserDbContext _dbContext;

        public CityRepository(IMultipleUserDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddAsync(City entity)
        {
            string query = "INSERT INTO Cities (CityName, ProvinceId) VALUES (@CityName, @ProvinceId)";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@CityName", entity.CityName),
            new SqlParameter("@ProvinceId", entity.ProvinceId),
            };

            await _dbContext.ExecuteQueryAsync(query, parameters);
        }

        public async Task<IEnumerable<CityResponse>> GetAllAsync()
        {
            string query = "SELECT * FROM Cities";

            using (var reader = await _dbContext.ExecuteQueryAsync(query))
            {
                var cities = new List<CityResponse>();
                while (await reader.ReadAsync())
                {
                    cities.Add(new CityResponse
                    {
                        Id = (int)reader["Id"],
                        CityName = reader["CityName"].ToString()
                    });
                }
                return cities;
            }
        }

        public Task AddMultipleAsync(IEnumerable<City> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CityResponse>> GetByAsync(string value)
        {
            throw new NotImplementedException();
        }
    }
}
