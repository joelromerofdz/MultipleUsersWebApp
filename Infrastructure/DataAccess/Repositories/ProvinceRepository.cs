using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Interfaces;
using Shared.Dtos.Province;
using System.Data.SqlClient;

namespace Infrastructure.DataAccess.Repositories
{
    public class ProvinceRepository : IRepository<Province, ProvinceResponse>
    {
        private readonly IMultipleUserDbContext _dbContext;

        public ProvinceRepository(
            IMultipleUserDbContext dbContext)
        {
            this._dbContext = dbContext;

        }

        public async Task AddAsync(Province entity)
        {
            string query = "INSERT INTO Provinces (ProvinceName, CountryId) VALUES (@ProvinceName, @CountryId)";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@ProvinceName", entity.ProvinceName),
            new SqlParameter("@CountryId", entity.CountryId),
            };

            await _dbContext.ExecuteQueryAsync(query, parameters);
        }

        public async Task<IEnumerable<ProvinceResponse>> GetAllAsync()
        {
            string query = "SELECT * FROM Provinces";

            using (var reader = await _dbContext.ExecuteQueryAsync(query))
            {
                var provinces = new List<ProvinceResponse>();
                while (await reader.ReadAsync())
                {
                    provinces.Add(new ProvinceResponse
                    {
                        Id = (int)reader["Id"],
                        ProvinceName = reader["ProvinceName"].ToString(),
                    });
                }
                return provinces;
            }
        }

        public Task AddMultipleAsync(IEnumerable<Province> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProvinceResponse>> GetByAsync(string value)
        {
            throw new NotImplementedException();
        }
    }
}
