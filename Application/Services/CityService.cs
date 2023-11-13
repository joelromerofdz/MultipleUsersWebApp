using Application.Services.Interfaces.ICityService;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DataAccess.Repositories;
using Shared.Dtos.City;
using Shared.Dtos.Country;

namespace Application.Services
{
    public class CityService : ICityService
    {
        private readonly IRepository<City, CityResponse> _cityRepository;

        public CityService(IRepository<City, CityResponse> cityRepository)
        {
            this._cityRepository = cityRepository;
        }

        public async Task AddCityAsync(CityRequest cityRequest)
        {
            if (cityRequest == null)
            {
                return;
            }

            var city = new City
            {
                CityName = cityRequest.CityName,
                ProvinceId = cityRequest.ProvinceId,
            };

            await _cityRepository.AddAsync(city);
        }

        public async Task<IEnumerable<CityResponse>> GetCitiesAsync()
        {
            var cities = await _cityRepository.GetAllAsync();

            var cityResponses = cities.Select(city => new CityResponse
            {
                Id = city.Id,
                CityName = city.CityName
            });

            return cityResponses;
        }
    }
}
