using Shared.Dtos.City;
using Shared.Dtos.Province;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.ICityService
{
    public interface ICityService
    {
        Task<IEnumerable<CityResponse>> GetCitiesAsync();
        Task AddCityAsync(CityRequest cityRequests);
    }
}
