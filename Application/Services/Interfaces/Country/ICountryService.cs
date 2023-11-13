using Shared.Dtos.Country;

namespace Application.Services.Interfaces.Country
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryResponse>> GetCountriesAsync();
        Task AddCountryAsync(CountryRequest countryRequests);
    }
}