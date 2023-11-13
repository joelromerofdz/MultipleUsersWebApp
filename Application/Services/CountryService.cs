using Application.Services.Interfaces.Country;
using Domain.Entities;
using Domain.Repositories;
using Shared.Dtos.Country;

namespace Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country, CountryResponse> _countryRepository;

        public CountryService(IRepository<Country, CountryResponse> countryRepository)
        {
            this._countryRepository = countryRepository;
        }

        public async Task<IEnumerable<CountryResponse>> GetCountriesAsync()
        {
            return await _countryRepository.GetAllAsync();
        }

        public async Task AddCountryAsync(CountryRequest countryRequest)
        {
            if (countryRequest == null)
            {
                return;
            }

            Country country = new Country()
            {
                CountryName = countryRequest.CountryName
            };

            await _countryRepository.AddAsync(country);
        }
    }
}
